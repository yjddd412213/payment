using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Inywhere.PaymentGateway;

namespace Inywhere.PaymentGateway.MVCPresentation
{
    public class ConfigurationSettings
    {
        public static string AccountServiceFrontEnd = string.Empty;
        public static string PaymentInfoFrontEnd = string.Empty;
        public static bool EmailCustormer = false;
        public static bool IsTestMode = false;
        public static string User_AuthorizeNET = string.Empty;
        public static string Password_AuthorizeNET = string.Empty;
        public static CreditCardProcessorType CreditCardProcessorType = CreditCardProcessorType.AuthorizeNet;

        public static string LoggerName = string.Empty;

        static ConfigurationSettings()
        {
            AccountServiceFrontEnd = ConfigurationManager.AppSettings["AccountServiceFrontEnd"];
            PaymentInfoFrontEnd = ConfigurationManager.AppSettings["PaymentInfoFrontEnd"];
            bool.TryParse(ConfigurationManager.AppSettings["EmailCustormer"], out EmailCustormer);
            bool.TryParse(ConfigurationManager.AppSettings["IsTestMode"], out IsTestMode);
            User_AuthorizeNET = ConfigurationManager.AppSettings["AuthorizeNET.User"];
            Password_AuthorizeNET = ConfigurationManager.AppSettings["AuthorizeNET.Password"];
            LoggerName = ConfigurationManager.AppSettings["LoggerName"];
        }
    }
}