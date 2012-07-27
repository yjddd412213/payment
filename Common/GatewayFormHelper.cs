using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inywhere.PaymentGateway.MVCPresentation.Common
{
    public class GatewayFormHelper
    {
        private const string DEFAULT_COUNTRY_ISO = "US";

        static GatewayFormHelper()
        {
            Initialize();
        }

        private static string[] m_MonthEnum;
        public static string[] MonthEnum
        {
            get { return m_MonthEnum; }
        }

        private static int[] m_YearEnum;
        public static int[] YearEnum
        {
            get
            {
                return m_YearEnum;
            }
        }

        private static Dictionary<string, string> m_CountryEnum;
        public static Dictionary<string, string> CountryEnum
        {
            get
            {
                return m_CountryEnum;
            }
        }

        private static List<SelectListItem> m_MonthList;
        public static List<SelectListItem> MonthList
        {
            get 
            {
                return m_MonthList;
            }
        }

        private static List<SelectListItem> m_YearList;
        public static List<SelectListItem> YearList
        {
            get
            {
                return m_YearList;
            }
        }

        public static IEnumerable<SelectListItem> GetCountryList(string defaultCountryISO)
        {
            var list = new Dictionary<string, SelectListItem>();
            foreach (var item in CountryEnum)
            {
                list.Add(item.Key, new SelectListItem() { Text = item.Value, Value = item.Key });
            }
            if (!string.IsNullOrEmpty(defaultCountryISO))
            {
                var item = list[defaultCountryISO];
                if (item != null)
                {
                    item.Selected = true;
                    return list.Values;
                }
            }
            list[DEFAULT_COUNTRY_ISO].Selected = true;
            return list.Values;
        }

        public static IEnumerable<SelectListItem> GetCountryList()
        {
            return GetCountryList(string.Empty);
        }

        private static void Initialize()
        {
            int currentYear = DateTime.Now.Year;
            m_MonthEnum = new string[] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
            m_YearEnum = new int[] { currentYear, currentYear++, currentYear++, currentYear++, currentYear++, currentYear++, currentYear++, currentYear++, currentYear++, currentYear++ };
            m_CountryEnum = new Dictionary<string, string>()
            { 
                {"AE", "United Arab Emirates"},
                {"AL", "Albania"},
                {"AR", "Argentina"},
                {"AT", "Austria"},
                {"AU", "Australia"},
                {"BA", "Bosnia and Herzegovina"},
                {"BE", "Belgium"},
                {"BG", "Bulgaria"},
                {"BH", "Bahrain"},
                {"BO", "Bolivia"},
                {"BR", "Brazil"},
                {"BY", "Belarus"},
                {"CA", "Canada"},
                {"CH", "Switzerland"},
                {"CL", "Chile"},
                {"CN", "China"},
                {"CO", "Colombia"},
                {"CR", "Costa Rica"},
                {"CS", "Serbia and Montenegro"},
                {"CY", "Cyprus"},
                {"CZ", "Czech Republic"},
                {"DE", "Germany"},
                {"DK", "Denmark"},
                {"DO", "Dominican Republic"},
                {"DZ", "Algeria"},
                {"EC", "Ecuador"},
                {"EE", "Estonia"},
                {"EG", "Egypt"},
                {"ES", "Spain"},
                {"FI", "Finland"},
                {"FR", "France"},
                {"GB", "United Kingdom"},
                {"GR", "Greece"},
                {"GT", "Guatemala"},
                {"HK", "Hong Kong"},
                {"HN", "Honduras"},
                {"HR", "Croatia"},
                {"HU", "Hungary"},
                {"ID", "Indonesia"},
                {"IE", "Ireland"},
                {"IL", "Israel"},
                {"IN", "India"},
                {"IQ", "Iraq"},
                {"IS", "Iceland"},
                {"IT", "Italy"},
                {"JO", "Jordan"},
                {"JP", "Japan"},
                {"KR", "South Korea"},
                {"KW", "Kuwait"},
                {"LB", "Lebanon"},
                {"LT", "Lithuania"},
                {"LU", "Luxembourg"},
                {"LV", "Latvia"},
                {"LY", "Libya"},
                {"MA", "Morocco"},
                {"ME", "Montenegro"},
                {"MK", "Macedonia"},
                {"MT", "Malta"},
                {"MX", "Mexico"},
                {"MY", "Malaysia"},
                {"NI", "Nicaragua"},
                {"NL", "Netherlands"},
                {"NO", "Norway"},
                {"NZ", "New Zealand"},
                {"OM", "Oman"},
                {"PA", "Panama"},
                {"PE", "Peru"},
                {"PH", "Philippines"},
                {"PL", "Poland"},
                {"PR", "Puerto Rico"},
                {"PT", "Portugal"},
                {"PY", "Paraguay"},
                {"QA", "Qatar"},
                {"RO", "Romania"},
                {"RS", "Serbia"},
                {"RU", "Russia"},
                {"SA", "Saudi Arabia"},
                {"SD", "Sudan"},
                {"SE", "Sweden"},
                {"SG", "Singapore"},
                {"SI", "Slovenia"},
                {"SK", "Slovakia"},
                {"SV", "El Salvador"},
                {"SY", "Syria"},
                {"TH", "Thailand"},
                {"TN", "Tunisia"},
                {"TR", "Turkey"},
                {"TW", "Taiwan"},
                {"UA", "Ukraine"},
                {"US", "United States"},
                {"UY", "Uruguay"},
                {"VE", "Venezuela"},
                {"VN", "Vietnam"},
                {"YE", "Yemen"},
                {"ZA", "South Africa"},
            };
            m_MonthList = new List<SelectListItem>();
            MonthList.Add(new SelectListItem() { Text = MonthEnum[0], Value = MonthEnum[0], Selected = true });
            m_YearList = new List<SelectListItem>();
            YearList.Add(new SelectListItem() { Text = YearEnum[0].ToString(), Value = YearEnum[0].ToString(), Selected = true });
            int i = 1;
            for (; i < 12; i++)
            {
                MonthList.Add(new SelectListItem() { Text = MonthEnum[i], Value = MonthEnum[i] });
            }
            for (i = 1; i < 10; i++)
            { 
                YearList.Add(new SelectListItem() { Text = YearEnum[i].ToString(), Value = YearEnum[i].ToString()});
            }
        }
    }
}