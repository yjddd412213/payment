using System;
using System.Collections;
using com.paypal.sdk.util;

namespace Inywhere.PaymentGateway.MVCPresentation
{
	/// <summary>
	/// Summary description for Util.
	/// </summary>
	public class Util
	{
	
		public static string GenerateCreditCardNumber(string cardType)
		{
			int[] cc_number = new int[16];
			int cc_len = 16;
			int start = 0;
			Random r = new Random();
			switch (cardType)
			{
				case "Visa":
					cc_number[start++] = 4;
					break;
				case "Discover":
					cc_number[start++] = 6;
					cc_number[start++] = 0;
					cc_number[start++] = 1;
					cc_number[start++] = 1;
					break;
				case "MasterCard":
					cc_number[start++] = 5;
					cc_number[start++] = (int)Math.Floor(r.NextDouble() * 5) + 1;
					break;
				case "Amex":
					cc_number = new int[15];
					cc_number[start++] = 3;
					cc_number[start++] = r.Next(2) == 1 ? 7 : 4 ;
					cc_len = 15;
					break;
			}
        
			for (int i = start; i < (cc_len - 1); i++) 
			{
				cc_number[i] = (int)Math.Floor(r.NextDouble() * 10);
			}
		
			int sum = 0;
			for (int j = 0; j < (cc_len - 1); j++) 
			{
				int digit = cc_number[j];
				if ((j & 1) == (cc_len & 1)) digit *= 2;
				if (digit > 9) digit -= 9;
				sum += digit;
			}
		
			int[] check_digit = new  int[]{0, 9, 8, 7, 6, 5, 4, 3, 2, 1};
			cc_number[cc_len - 1] = check_digit[sum % 10];
		
			string result = string.Empty;
			foreach (int digit in cc_number)
			{
				result += digit;
			}
			return result;
		}
		
		public static string BuildResponse(object response,string header1,string header2)
		{
			if(response!=null)
			{
				NVPCodec decoder = new NVPCodec();
				decoder= (NVPCodec)response;

				string res="<center>";
				if(header1!=null )
					res=res+ "<font size=2 color=black face=Verdana><b>" +header1 + "</b></font>";
				res=res+ "<br>";
				res=res+ "<br>";

				if(header2!=null)
					res=res+ "<b>"+header2+"</b><br>";

				res=res+ "<br>";

				res=res+"<table width=400 class=api>";

				for (int i=0; i<decoder.Keys.Count;i++)
				{
					res=res+ "<tr><td class=field> " +decoder.Keys[i].ToString() +":</td>";
					res=res+"<td>" +decoder.GetValues(i)[0] +"</td>";
					res=res+"</tr>";
					res=res+"<tr>";
				}
					 			
				res=res+"</table>";
				res=res+"</center>";
				return res;
			}
			else
			{
				return "Requested action not allowed";
			}
		}
	}
}
