using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CatalogCommon;

namespace Inywhere.PaymentGateway.MVCPresentation.Models
{
    public class AccountModel
    {
        [DataType(DataType.Text)]
        [DisplayName("Inywhere Account")]
        [Required(ErrorMessage=" Required")]
        [Account(ErrorMessage="User Not Exist")]
        public string Account { get; set; }

        //[DataType(DataType.Password)]
        //[DisplayName("Password")]
        //[Required(ErrorMessage = " Requried")]
        //public string Password { get; set; }
    }

    public class AccountAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                AccountInfoProvider accountinfoprovider = new AccountInfoProvider();
                return accountinfoprovider.IsUserExist(value.ToString());
            }
            return true;
        }
    } 

    /// <summary>
    /// Fat model is suggested.
    /// </summary>
    public interface IAccountService
    {
        bool Validate(AccountModel account);
        object GetProductStatus(int productId);
    }

    /// <summary>
    /// Fat model is suggested.
    /// </summary>
    public class AccountService : IAccountService
    {
        public bool Validate(AccountModel account)
        {
            return true;
        }

        public object GetProductStatus(int productId)
        {
            throw new NotImplementedException();
        }
    }
}