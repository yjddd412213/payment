using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Inywhere.PaymentGateway.DataContract;


namespace Inywhere.PaymentGateway.MVCPresentation.Models
{
    public abstract class BillingFormModelBase
    {
        public AccountModel InywhereAccount { get; set; }

        public Product Product { get; set; }

        [Required(ErrorMessage = " Requried")]
        [DataType(DataType.Text)]
        [DisplayName("Product terms")]
        public string ProductTermsId { get; set; }

        public BillingFormModelBase()
        {
            InywhereAccount = new AccountModel();
            Product = new Product();
        }
    }
    /// <summary>
    /// Credit card model
    /// </summary>
    public class CCFormModel : BillingFormModelBase
    {
        [Required(ErrorMessage = " Requried")]
        [DataType(DataType.Text)]
        [DisplayName("Credit card number")]
        public string CardNum { get; set; }

        [Required(ErrorMessage = " Requried")]
        [DataType(DataType.Text)]
        [DisplayName("Expire month")]
        public int ExpireMonth { get; set; }

        [Required(ErrorMessage = " Requried")]
        [DataType(DataType.Text)]
        [DisplayName("Expire year")]
        public int ExpireYear { get; set; }

        [Required(ErrorMessage = " Requried")]
        [DataType(DataType.Text)]
        [DisplayName("Security code")]
        public string SecurityCode { get; set; }

        [Required(ErrorMessage = " Requried")]
        [DataType(DataType.Text)]
        [DisplayName("First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = " Requried")]
        [DataType(DataType.Text)]
        [DisplayName("Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = " Requried")]
        [DataType(DataType.Text)]
        [DisplayName("Address")]
        public string Address { get; set; }
        
        [DataType(DataType.Text)]
        [DisplayName("Address2")]
        public string Address2 { get; set; }

        [Required(ErrorMessage = " Requried")]
        [DataType(DataType.Text)]
        [DisplayName("City")]
        public string City { get; set; }

        [Required(ErrorMessage = " Requried")]
        [DataType(DataType.Text)]
        [DisplayName("State/Province")]
        public string State { get; set; }

        [Required(ErrorMessage = " Requried")]
        [DataType(DataType.Text)]
        [DisplayName("ZIP/Postal code")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = " Requried")]
        [DataType(DataType.Text)]
        [DisplayName("Country")]
        public string Country { get; set; }
    }
    /// <summary>
    /// Paypal model
    /// </summary>
    public class PPFormModel : BillingFormModelBase { }
    /// <summary>
    /// Google check-out model
    /// </summary>
    public class GCFormModel : BillingFormModelBase { }


    public static class BillingModelHelper
    {
        public static PaymentTransactionData ToPaymentTransactionData(this BillingFormModelBase model)
        {
            PaymentTransactionData data = new PaymentTransactionData();
            if (model.GetType() == typeof(CCFormModel))
            {
                var ccModel = model as CCFormModel;
                data.AccountData = new CreditCardAccountData();
                data.AccountData.CardNumber = ccModel.CardNum;
                data.AccountData.CardType = Inywhere.PaymentGateway.CreditCardUtility.GetCardTypeFromNumber(ccModel.CardNum).Value;
                data.AccountData.ExpirationMonth = ccModel.ExpireMonth;
                data.AccountData.ExpirationYear = ccModel.ExpireYear;
                data.AccountData.SecurityCode = ccModel.SecurityCode;

                data.ProductData.ProductTermId = ccModel.ProductTermsId;
                data.InywhereId = ccModel.InywhereAccount.Account;

                data.CustomerData.CustomerId = ccModel.InywhereAccount.Account;
                data.CustomerData.FirstName = ccModel.FirstName;
                data.CustomerData.LastName = ccModel.LastName;
                data.CustomerData.AddressLine1 = ccModel.Address;
                data.CustomerData.AddressLine2 = ccModel.Address2;
                data.CustomerData.City = ccModel.City;
                data.CustomerData.State = ccModel.State;
                data.CustomerData.PostalCode = ccModel.PostalCode;
                data.CustomerData.Country = ccModel.Country;
            }
            return data;
        }

        public static T ToBillingFormModel<T>(this PaymentTransactionData data)
            where T : BillingFormModelBase
        { 
            T model = (T)Activator.CreateInstance(typeof(T));
            if(model is CCFormModel)
            {
                var ccModel = model as CCFormModel;
                ccModel.CardNum = data.AccountData.CardNumber;
                ccModel.ExpireMonth = data.AccountData.ExpirationMonth;
                ccModel.ExpireYear = data.AccountData.ExpirationYear;
                ccModel.SecurityCode = data.AccountData.SecurityCode;

                ccModel.ProductTermsId = data.ProductData.ProductTermId;

                ccModel.InywhereAccount.Account = data.CustomerData.CustomerId;
                ccModel.FirstName = data.CustomerData.FirstName;
                ccModel.LastName = data.CustomerData.LastName;
                ccModel.Address = data.CustomerData.AddressLine1;
                ccModel.Address2 = data.CustomerData.AddressLine2;
                ccModel.City = data.CustomerData.City;
                ccModel.State = data.CustomerData.State;
                ccModel.PostalCode = data.CustomerData.PostalCode;
                ccModel.Country = data.CustomerData.Country;
            }
            return model;
        }

    }
}