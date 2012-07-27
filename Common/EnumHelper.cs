using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inywhere.PaymentGateway.MVCPresentation.Common
{
    public class EnumHelper<TEnum>
    {
        public static TEnum ConvertToEnum(string value, TEnum defaultValue)
        {
            TEnum result = defaultValue;
            try
            {
                result = (TEnum)Enum.Parse(typeof(TEnum), value);
            }
            catch { }
            return result;
        }
    }
}