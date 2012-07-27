using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inywhere.PaymentGateway.DataContract;

namespace Inywhere.PaymentGateway.MVCPresentation.Common
{
    public class GatewaySettingsFactory
    {
        public static PaymentGatewaySettings Create(CreditCardProcessorType type)
        {
            PaymentGatewaySettings setting = new PaymentGatewaySettings();
            setting.EmailCustomer = ConfigurationSettings.EmailCustormer;
            setting.TestMode = ConfigurationSettings.IsTestMode;
            setting.Username = ConfigurationSettings.User_AuthorizeNET;
            setting.Password = ConfigurationSettings.Password_AuthorizeNET;
            return setting;
        }
    }
}