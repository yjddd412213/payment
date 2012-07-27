using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inywhere.PaymentGateway.MVCPresentation.Common
{
    public enum ErrorType
    {
        AccountAuthenticateError,
        UserNotExist,
        TransactionProcessError,
        ProductNotExist,
        InvalidCardNumber,
        InvalidOperation,
        ServiceError,
    }
}