namespace Inywhere.PaymentGateway.MVCPresentation
{
	/// <summary>
	/// Summary description for Constants.
	/// </summary>
	public class Constants
	{
		/// <summary>
		/// Modify these values if you want to use your own profile.
		/// </summary>

		/* 
		 *                                                                         *
		 * WARNING: Do not embed plaintext credentials in your application code.   *
		 * Doing so is insecure and against best practices.                        *
		 *                                                                         *
		 * Your API credentials must be handled securely. Please consider          *
		 * encrypting them for use in any production environment, and ensure       *
		 * that only authorized individuals may view or modify them.               *
		 *                                                                         *
		 */

	     	
         	       	
        			
		public const string ENVIRONMENT = "sandbox";
		public const string PAYPAL_URL = "https://www.sandbox.paypal.com";
		public const string ECURLLOGIN = "https://developer.paypal.com";
		public const string SUBJECT = "";  

	
		public const string PROFILE_KEY = "Profile";
		public const string PAYMENT_ACTION_PARAM = "paymentAction";
		public const string PAYMENT_TYPE_PARAM = "paymentType";


		public const string STANDARD_EMAIL_ADDRESS = "sdk-seller@sdk.com";		
		public const string WEBSCR_URL = PAYPAL_URL + "/cgi-bin/webscr";
		
		///sandbox 3t credentials
        public const string PRIVATE_KEY_PASSWORD = "";
        //public const string API_USERNAME = "sdk-three_api1.sdk.com";
        //public const string API_PASSWORD = "QFZCWN5HZM8VBG7Q";
        //public const string API_SIGNATURE = "A.d9eRKfd1yVkRrtmMfCFLTqa6M9AyodL0SJkhYztxUi8W9pCXF6.4NI";
        public const string API_USERNAME = "yjddd_1340167964_biz_api1.gmail.com";
        public const string API_PASSWORD = "SCCCDNC3T6VD7EPL";
        public const string API_SIGNATURE = "ArNMEQaNesEaSa9IckMxemYO7sL1Av4-x1MY7d5Wn6.y.j06gg8xGcjt";
        public const string CERTIFICATE = "";

		//Permission
		public const string OAUTH_SIGNATURE = "";
		public const string OAUTH_TOKEN = "";
		public const string OAUTH_TIMESTAMP = "";
				
	}
}
