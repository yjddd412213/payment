using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inywhere.PaymentGateway.MVCPresentation.Common
{
    public class ErrorInfoHelper : Dictionary<ErrorType, string>
    {
        public static ErrorInfoHelper Instance = new ErrorInfoHelper();

        private ErrorInfoHelper()
        {
            Add(ErrorType.AccountAuthenticateError, "Account validation error, please provide your account.");
            Add(ErrorType.TransactionProcessError, "Error happens while process transaction.");
            Add(ErrorType.ProductNotExist, "Sorry, the product you are looking for does not exist.");
            Add(ErrorType.InvalidCardNumber, "The credit card number you provided is not valid. Please verfiy.");
            Add(ErrorType.InvalidOperation, "Invalid Operation.");
            Add(ErrorType.ServiceError, "Sorry, the payment service encountered an error. Please contact operator at 000-0000.");
        }
    }
}