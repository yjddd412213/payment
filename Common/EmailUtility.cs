using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using EmailSender;
using Inywhere.PaymentGateway.DataContract;


namespace Inywhere.PaymentGateway.MVCPresentation.Common
{
    public class EmailUtility
    {
        public static void SendEmail(PaymentTransactionData data)
        {
            EMailItem item = new EMailItem();
            item.SenderAddress = @"info@inywhere.com";
            item.SenderName = "Inywhere Products Center";
            item.Priority = MailPriority.High;
            item.ReplyTo = "test@inywhere.com";
            //item.CC = new string[] { "yjddd412213@hotmail.com" };   //"kuo_ren@hotmail.com"
            //item.BCC = new string[] { "yjddd412213@163.com" };
            item.To = new string[] { data.InywhereId }; // TODO:data.InywhereId
            item.Subject = "Thanks For Purchasing Inywhere Products.";
            item.Body = string.Format("Your order has been proceed. Thanks for purchasing {0}.", data.ProductData.Description); // TODO:

            item.EntityID = Guid.NewGuid().ToString();
            // item.Callback = new SendMailCallback(OnSent); // TODO: Define Send callback 
            //item.Context = context; // TODO: context can be used for send callback processing.
            item.IsHtmlBody = false;

            //MailSender.Instance.Send(item);

            SendGridMailSender.SendMail(item);
        }
    }
}