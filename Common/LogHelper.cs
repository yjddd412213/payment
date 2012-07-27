using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inywhere.Diagnostics;

namespace Inywhere.PaymentGateway.MVCPresentation.Common
{
    public class LogHelper
    {
        static LogHelper()
        {
            Instance = Logger.GetLogger(ConfigurationSettings.LoggerName);
        }

        private static Logger m_Instance;
        public static Logger Instance
        {
            get 
            {
                return m_Instance;
            }
            private set
            {
                m_Instance = value;
            }
        }
    }
}