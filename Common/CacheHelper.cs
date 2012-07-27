using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Timers;
using System.Threading.Tasks;


namespace Inywhere.PaymentGateway.MVCPresentation.Common
{
    /// <summary>
    /// Great performace. 
    /// It's better to encapsulate the cache into WCF service and provide distributed cache service with multi servers.
    /// </summary>
    public class CacheHelper
    {
        static CacheHelper()
        {
            m_Instance = new CacheHelper();
        }

        private static CacheHelper m_Instance;
        public static CacheHelper Instance
        {
            get
            {
                return m_Instance;
            }
        }

        private CacheHelper() 
        {
            m_CacheContainer = new Dictionary<string, CacheItem>();
            m_LinkedItems = new LinkList<CacheItem>();
            m_Cleaner = new CacheCleaner();
            m_Cleaner.Start();
        }
        private CacheCleaner m_Cleaner;
        private Dictionary<string, CacheItem> m_CacheContainer;
        private LinkList<CacheItem> m_LinkedItems;
        public T GetData<T>(string index, bool needRefresh = false)
        {
            if (m_CacheContainer.ContainsKey(index))
            {
                var item = m_CacheContainer[index];
                if (needRefresh) item.Refresh();
                return (T)item.Data;
            }
            return default(T);
        }

        public object GetData(string index, bool needRefresh = false)
        {
            if (m_CacheContainer.ContainsKey(index))
            {
                var item = m_CacheContainer[index];
                if (needRefresh)
                {
                    item.Refresh();
                }
                return item.Data;
            }
            return null;
        }

        public object ExpireData(string index)
        {
            var item = m_CacheContainer[index];
            m_LinkedItems.Remove(item);
            m_CacheContainer.Remove(index);
            return item;
        }

        public void CacheData(string index, object obj)
        {
            if (m_CacheContainer.ContainsKey(index))
                m_CacheContainer[index].Data = obj;
            else
            {
                var cacheItem = new CacheItem(index, obj);
                m_CacheContainer.Add(index, cacheItem);
                m_LinkedItems.Add(cacheItem);
            }
        }

        public object this[string index]
        {
            get
            {
                return GetData(index);
            }
            set
            {
                CacheData(index, value);
            }
        }

        private sealed class CacheItem : ILinkNode<CacheItem>, IDisposable
        {
            private static TimeSpan DEFAULT_PERSIST = new TimeSpan(0, 30, 0);

            private object m_Data { get; set; }
            private DateTime m_LifeEndTime { get; set; }
            private TimeSpan m_PersitTime;
            private string m_Key;

            public CacheItem(string key, object data, TimeSpan persist)
            {
                m_Data = data;
                m_Key = key;
                m_PersitTime = persist;
                m_LifeEndTime = DateTime.Now.Add(m_PersitTime);
            }

            public CacheItem(string key, object data)
                : this(key, data, DEFAULT_PERSIST)
            {
            }

            public CacheItem()
            { }

            public object Data
            {
                get { return m_Data; }
                set { m_Data = value; }
            }

            public string Key
            {
                get { return m_Key; }
            }

            public bool IsValid()
            {
                if (DateTime.Now.CompareTo(m_LifeEndTime) > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            public void Refresh()
            {
                m_LifeEndTime = DateTime.Now.Add(m_PersitTime);
            }

            private ILinkNode<CacheItem> m_Next;
            public ILinkNode<CacheItem> Next
            {
                get
                {
                    return m_Next;
                }
                set
                {
                    m_Next = value;
                }
            }

            public void Dispose()
            {
                m_Data = null;
                m_Key = null;
                m_Next = null;
            }

            ~CacheItem()
            {
                Dispose();
            }

            public override bool Equals(object obj)
            {
                if (obj.GetType() == this.GetType())
                {
                    var item = (CacheItem)obj;
                    if (item.Key == this.Key)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            public override int GetHashCode()
            {
                return Key.GetHashCode();
            }
        }

        private sealed class CacheCleaner
        {
            /// <summary>
            /// 1 minute as default.
            /// </summary>
            private const double DEFAULT_INTERVAL = 60000f;
            private Timer m_Timer;
            public CacheCleaner()
            {
                m_Timer = new Timer(DEFAULT_INTERVAL);
                m_Timer.Elapsed += new ElapsedEventHandler(m_Timer_Elapsed);
            }

            public void Start()
            {
                m_Timer.Enabled = true;
            }
            public void Stop()
            {
                m_Timer.Enabled = false;
            }
            private void m_Timer_Elapsed(object sender, ElapsedEventArgs e)
            {
                //Executed by thread pool.
                cleanWork();
            }

            private void cleanWork()
            {
                Instance.m_LinkedItems.Foreach(
                    (cur, pre) =>
                    {
                        var cacheItem = (CacheItem)cur;
                        if (!cacheItem.IsValid())
                        {
                            Instance.ExpireData(cacheItem.Key);
                            pre.Next = cur.Next;
                            cur.Next = null;
                            cacheItem.Dispose();
                        }
                    });
            }
        }

        private interface ILinkNode<T>
        {
            ILinkNode<T> Next { get; set; }
        }

        private sealed class LinkList<T>
            where T : ILinkNode<T>
        { 
            private ILinkNode<T> m_Header;
            public LinkList()
            {
                m_Header = (ILinkNode<T>)Activator.CreateInstance(typeof(T));
            }
            public int Count()
            {
                int count = 0;
                var temp = m_Header.Next;
                while (temp != null)
                {
                    count++;
                }
                return count;
            }
            public void Add(ILinkNode<T> node)
            {
                if (m_Header.Next == null)
                {
                    m_Header.Next = node;
                }
                else
                {
                    node.Next = m_Header.Next;
                    m_Header.Next = node;
                }
            }
            public void Remove(ILinkNode<T> node)
            {
                this.Foreach(
                (cur, pre) =>
                {
                    if (cur.Equals(node))
                    {
                        pre.Next = cur.Next;
                        cur.Next = null;
                    }
                });
            }
            public void Remove(ICollection<ILinkNode<T>> nodes)
            {
                this.Foreach(
                (cur, pre) =>
                {
                    foreach (var item in nodes)
                    {
                        if (cur.Equals(item))
                        {
                            pre.Next = cur.Next;
                            cur.Next = null;
                        }
                    }
                });
            }
            /// <summary>
            /// Foreach loop.
            /// </summary>
            /// <param name="action">Action takes two parameters, first param - current node, sencond param - previous node.</param>
            public void Foreach(Action<ILinkNode<T>, ILinkNode<T>> action)
            {
                var cur_node = m_Header.Next;
                var pre_node = m_Header;
                while (cur_node != null)
                {
                    action(cur_node, pre_node);
                    cur_node = cur_node.Next;
                    pre_node = pre_node.Next;
                }
            }
        }
    }
}