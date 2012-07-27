using System;
using System.ComponentModel;
using System.Web;
using com.paypal.sdk.profiles;
using log4net;

namespace Inywhere.PaymentGateway.MVCPresentation
{
	/// <summary>
	/// Summary description for Constants.
	/// </summary>
	public class SetProfile
	{
		
		/// <summary>
		/// Required designer variable.
		/// </summary
		  public static readonly IAPIProfile DefaultProfile =  CreateAPIProfile(Constants.API_USERNAME, Constants.API_PASSWORD, Constants.API_SIGNATURE,Constants.CERTIFICATE,Constants.PRIVATE_KEY_PASSWORD, "sandbox",Constants.SUBJECT,Constants.OAUTH_SIGNATURE,Constants.OAUTH_TOKEN,Constants.OAUTH_TIMESTAMP);
		
		public static IAPIProfile CreateAPIProfile(string apiUsername, string apiPassword, string signature,string CertificateFile_Cer,string PrivateKeyPassword_Cer, String stage,string subject,string oauth_Signature,string oauth_Token,string oauth_Timestamp)
		{

            if (MvcApplication.is3token == true)
			{
				IAPIProfile profile = ProfileFactory.createSignatureAPIProfile();
				profile.APIUsername = apiUsername;
				profile.APIPassword = apiPassword;			
				profile.Environment = stage;
				profile.Subject = subject;
				profile.APISignature = signature;
				return profile;
			}
            else if (MvcApplication.isunipay == true)
			{
				IAPIProfile profile = ProfileFactory.createUniPayAPIProfile();
				profile.getFirstPartyEmail = subject;
				profile.Environment=stage;
				return profile;
			}
            else if (MvcApplication.ispermission == true)
			{
				IAPIProfile profile = ProfileFactory.createPermissionAPIProfile();
				profile.Oauth_Signature = oauth_Signature;
				profile.Oauth_Token=oauth_Token;
				profile.Oauth_Timestamp=oauth_Timestamp;
				profile.Environment = stage;
				profile.CertificateFile= CertificateFile_Cer;
				profile.PrivateKeyPassword=PrivateKeyPassword_Cer;
				profile.APIUsername = apiUsername;
				return profile;
			}
			else
			{
				IAPIProfile profile = ProfileFactory.createSSLAPIProfile();
				profile.APIUsername = apiUsername;
				profile.APIPassword = apiPassword;		
				profile.Environment = stage;
				profile.CertificateFile= CertificateFile_Cer;
				profile.PrivateKeyPassword=PrivateKeyPassword_Cer;
				profile.Subject =subject;

				return profile;
			}


			
		}
		public static void SetDefaultProfile()
		{
			SessionProfile = DefaultProfile;
		}

		public static IAPIProfile SessionProfile
		{
			get
			{
				return (IAPIProfile) HttpContext.Current.Session[Constants.PROFILE_KEY];
			}
			set
			{
				HttpContext.Current.Session[Constants.PROFILE_KEY] = value;
			}
		}
	}
	

}
