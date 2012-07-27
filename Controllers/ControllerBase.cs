using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inywhere.PaymentGateway.MVCPresentation.Common;

namespace Inywhere.PaymentGateway.MVCPresentation.Controllers
{
    public class ControllerBase : Controller
    {
        protected void AddModelError(ErrorType type)
        {
            ModelState.AddModelError(type.ToString(), ErrorInfoHelper.Instance[type]);
        }

        protected void AddModelError(ErrorType type, string msgFormat, params object[] obj)
        {
            string message = string.Format(msgFormat, obj);
            ModelState.AddModelError(type.ToString(), string.Format("{0}, {1}", ErrorInfoHelper.Instance[type], message));
        }
    }
}