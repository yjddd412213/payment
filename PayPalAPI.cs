using System;
using com.paypal.sdk.services;


namespace Inywhere.PaymentGateway.MVCPresentation
{
	/// <summary>
	/// Summary description for PayPalAPI.
	/// </summary>
	public class PayPalAPI
	{
		

		public PayPalAPI()
		{
		}

		public static NVPCallerServices PayPalAPIInitialize()
		{
			NVPCallerServices caller = new NVPCallerServices();
            caller.APIProfile = SetProfile.SessionProfile;
			return caller;

		}

	}
}
