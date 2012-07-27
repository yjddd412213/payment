using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inywhere.PaymentGateway.DataContract;

namespace Inywhere.PaymentGateway.MVCPresentation
{
    public class PaymentInfoObj
    {
        private static PaymentTransactionData m_Obj;
        public static PaymentTransactionData Obj
        {
            get
            {
                if (m_Obj == null)
                {
                    m_Obj = new PaymentTransactionData()
                    {
                        AccountData = new CreditCardAccountData()
                    };

                    // Configure authorize.net options
                    m_Obj.GatewaySettings.EmailCustomer = false;
                    m_Obj.GatewaySettings.Username = "743AqpG3 ";                //Authorize.Net Login Id
                    m_Obj.GatewaySettings.Password = "8y73ur6HA5fDw3Bx "; //Authorize.Net Transaction Key
                    m_Obj.GatewaySettings.TestMode = true;                       // True = testing, False = Live processing

                    // Optionally set a url if it has changed from the compiled url
                    //data.GatewaySettings.UrlLive = "http://";
                    //data.GatewaySettings.UrlTest = "http://";



                    // Set Credit Card Info
                    m_Obj.AccountData.CardHolderName = "Minbo Qiu";
                    m_Obj.AccountData.CardNumber = "370000000000002";
                    m_Obj.AccountData.ExpirationMonth = 12;
                    m_Obj.AccountData.ExpirationYear = 2011;
                    m_Obj.AccountData.SecurityCode = "1234";

                    // Set customer billing data
                    m_Obj.CustomerData.EmailAddress = "qiuminbo@126.com";
                    m_Obj.CustomerData.FirstName = "Minbo";
                    m_Obj.CustomerData.LastName = "Qiu";
                    //data.Customer.Company = "Acme Corp.";
                    m_Obj.CustomerData.AddressLine1 = "123 Fake Street";
                    //data.Customer.AddressLine2 = "Suite 1234";
                    m_Obj.CustomerData.City = "MyCity";
                    m_Obj.CustomerData.State = "WA";
                    m_Obj.CustomerData.PostalCode = "100029";
                    m_Obj.CustomerData.Country = "US";                       // Use ISO Code
                    m_Obj.CustomerData.CustomerId = "qiuminbo@126.com"; // Choose something unique to this customer
                    m_Obj.CustomerData.IPAddress = "127.0.0.1";              // for tracking
                    //data.Customer.PhoneNumber = "1234567890";
                    //data.Customer.FaxNumber = "1234567890";


                    // Tranasction Settings
                    m_Obj.ProductData.Amount = 1.23M;                             // The actual amount to charge, refund, auth, capture or void
                    m_Obj.ProductData.ProductTermId = "";
                    m_Obj.TransactionSettings.MerchantDescription = "Order From MoneyBaby";  // a description for the transaction
                    m_Obj.TransactionSettings.MerchantInvoiceNumber = "ABC123";              // a unique transaction id from you
                    //data.Transaction.PreviousTransactionReferenceNumber = "123";  // Required for VOID and REFUND transactions. 

                    m_Obj.TransactionId = "3";
                    m_Obj.ChargeDate = DateTime.UtcNow;
                    m_Obj.InywhereId = "Minbo Qiu";
                    m_Obj.TransactionResult.TransactionReferenceNumber = "XX001";
                }
                return m_Obj;
            }
        }
    }
}
