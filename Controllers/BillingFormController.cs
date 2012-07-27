using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CatalogCommon;
using com.paypal.sdk.util;
using Inywhere.PaymentGateway.DataContract;
using Inywhere.PaymentGateway.MVCPresentation.Common;
using Inywhere.PaymentGateway.MVCPresentation.Models;
using System.Configuration;
using Inywhere.Catalog;

namespace Inywhere.PaymentGateway.MVCPresentation.Controllers
{
    /// <summary>
    /// Optimization suggestion:
    /// It is better to put all productdata in cache.
    /// </summary>
    public class BillingFormController : ControllerBase
    {
        public const string PT_PREFIX = "pt_";
        public const string SL_VIP_PREFIX = "sl_vip_";
        public const string SL_PREMIUM_PREFIX = "sl_premium_";
        public const string PDS_PREFIX = "pds_";

        public BillingFormController()
        {
        }
        //
        // GET: /CCForm/
        //
        public ActionResult CCForm(string pname)
        {
            //int productId = ParseProductId(pid);
            var products = GetAllProducts();
            var productData = GetProductDataById(pname);
            var productItem = products.Find(m => m.ProductName == pname);
            if (productItem == null)
            {
                AddModelError(ErrorType.ProductNotExist);
                return View("Error");
            }

            CCFormModel model = new CCFormModel();
            //var model = PaymentInfoObj.Obj.ToBillingFormModel<CCFormModel>();
            model.Product = productItem;
            Session["BillingModel"] = model;
            return View(model);
        }

        public ActionResult Paypal(string token, string payerid)
        {
            CCFormModel model = (CCFormModel)Session["BillingModel"];
            var productData = GetProductDataById(model.Product.ProductName);
            ViewData["ProductData"] = productData;
            Session["TOKEN"] = token;
            Session["PAYERID"] = payerid;
            return View("OrderSummary", model);
        }

        /// <summary>
        /// POST: /CCForm/
        /// </summary>
        /// <param name="model">CCFormModel</param>
        /// <returns>View</returns>
        [HttpPost]
        [MultiButton("Card")]
        public ActionResult Card(CCFormModel model)
        {
            if (ModelState.IsValid)
            {
                if (CreditCardUtility.IsValidNumber(model.CardNum))
                {
                    try
                    {
                        Session["method"] = "Card";
                        var productData = GetProductDataById(model.Product.ProductName);
                        Session["BillingModel"] = model;
                        ViewData["ProductData"] = productData;
                        return View("OrderSummary", model);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Instance.Fatal("Inywhere account service is not avaliable.");
                        AddModelError(ErrorType.ServiceError);
                        return View("Error");
                    }
                }
                else
                {
                    AddModelError(ErrorType.InvalidCardNumber);
                    ModelState.AddModelError("CardNum", "Invalid card num.");
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpPost]
        [MultiButton("Paypal")]
        public ActionResult Paypal(CCFormModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Session["method"] = "Paypal";
                    var productData = GetProductDataById(model.Product.ProductName);
                    var data = productData.Find(m => m.ProductTermId == model.ProductTermsId);
                    setProfile();

                    string url = Request.Url.Scheme + "://" + Request.UrlReferrer.Host + ":" + Request.UrlReferrer.Port + @"/";
                    string cancelURL = url + "?pname=" + model.Product.ProductName;

                    com.paypal.sdk.services.NVPCallerServices caller = PayPalAPI.PayPalAPIInitialize();
                    NVPCodec encoder = new NVPCodec();
                    encoder["METHOD"] = "SetExpressCheckout";
                    encoder["CANCELURL"] = cancelURL;
                    encoder["PAYMENTACTION"] = ConfigurationManager.AppSettings["paymentType"];
                    encoder["CURRENCYCODE"] = ConfigurationManager.AppSettings["currency"];

                    encoder["L_NAME0"] = model.ProductTermsId;
                    encoder["L_DESC0"] = "Inywhere payment";
                    encoder["L_AMT0"] = data.Amount.ToString();
                    decimal ft = data.Amount;
                    encoder["ITEMAMT"] = ft.ToString();
                    encoder["AMT"] = ft.ToString();
                    if (url.Contains("inywhere"))
                        url += "payment/";
                    string returnURL = url + "BillingForm/Paypal";
                    encoder["RETURNURL"] = returnURL;
                    encoder["NOSHIPING"] = "1";
                    encoder["REQCONFIRMSHIPPING"] = "0";

                    string pStrrequestforNvp = encoder.Encode();
                    string pStresponsenvp = caller.Call(pStrrequestforNvp);

                    NVPCodec decoder = new NVPCodec();
                    decoder.Decode(pStresponsenvp);

                    string strAck = decoder["ACK"];
                    if (strAck != null && (strAck == "Success" || strAck == "SuccessWithWarning"))
                    {
                        Session["BillingModel"] = model;
                        ViewData["ProductData"] = productData;

                        Session["TOKEN"] = decoder["TOKEN"];
                        string host = "www." + Session["stage"].ToString() + ".paypal.com";
                        string ECURL = "https://" + host + "/cgi-bin/webscr?cmd=_express-checkout" + "&token=" + decoder["TOKEN"];
                        Response.Redirect(ECURL);
                        return View("OrderSummary", model);
                    }
                    else
                    {
                        LogHelper.Instance.Fatal(decoder["L_LONGMESSAGE0"]);
                        AddModelError(ErrorType.TransactionProcessError);
                        return View(model);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Instance.Fatal("Inywhere account service is not avaliable.");
                    AddModelError(ErrorType.ServiceError);
                    return View("Error");
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult OrderSummary()
        {
            var ccModel = Session["BillingModel"] as CCFormModel;
            if (ccModel != null)
            {
                NVPCodec decoder = new NVPCodec();
                string cacheKey = PT_PREFIX + ccModel.Product.ProductId;
                var productData = CacheHelper.Instance[cacheKey];
                if (productData == null)
                {
                    productData = AccountInfoProvider.Instance.GetTermsByProduct(ccModel.Product.ProductName);
                    CacheHelper.Instance[cacheKey] = productData;
                }
                var transData = ccModel.ToPaymentTransactionData();
                transData.ProductData = ((List<ProductData>)productData).Find(m => m.ProductTermId == ccModel.ProductTermsId);
                //transData.GatewaySettings = GatewaySettingsFactory.Create(CreditCardProcessorType.AuthorizeNet);
                setProfile();
                if (Session["method"].ToString() == "Card")
                {
                    decoder = DirectPay(transData);
                }
                else
                {
                    decoder = DoExpressCheckOut(transData);
                }
                if (decoder["ACK"] == "Success" || decoder["ACK"] == "SuccessWithWarning")
                {
                    try
                    {
                        transData.TransactionId = decoder["TRANSACTIONID"];
                        transData.ChargeDate = Convert.ToDateTime(decoder["TIMESTAMP"]);
                        transData.TransactionResult.Suceeded = true;
                        AccountInfoProvider.Instance.SavePaymentTransactionData(transData);
                        PaymentInfo paymentInfo = AccountInfoProvider.PaymentTransactionDataToPaymentInfo(transData);

                        if (AccountInfoProvider.Instance.PaymentSummaryNotification(transData.InywhereId, paymentInfo))
                        {
                            EmailUtility.SendEmail(transData);
                            return View("ResultView");
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Instance.Fatal("Inywhere account service is not avaliable.");
                        AddModelError(ErrorType.TransactionProcessError);
                        return View("Error");
                    }
                }
            }
            AddModelError(ErrorType.InvalidOperation);
            return View("Error");
        }

        private NVPCodec DoExpressCheckOut(PaymentTransactionData transData)
        {
            NVPCodec decoder = new NVPCodec();
            com.paypal.sdk.services.NVPCallerServices caller = PayPalAPI.PayPalAPIInitialize();
            NVPCodec encoder = new NVPCodec();
            encoder["METHOD"] = "DoExpressCheckoutPayment";
            encoder["TOKEN"] = Session["TOKEN"].ToString();
            encoder["PAYERID"] = Session["PAYERID"].ToString();
            encoder["AMT"] = transData.ProductData.Amount.ToString();
            encoder["PAYMENTACTION"] = ConfigurationManager.AppSettings["paymentType"];
            encoder["CURRENCYCODE"] = ConfigurationManager.AppSettings["currency"];
            encoder["IPADDRESS"] = GetClientIP();

            string pStrrequestforNvp = encoder.Encode();
            string pStresponsenvp = caller.Call(pStrrequestforNvp);

            decoder.Decode(pStresponsenvp);
            return decoder;
        }

        private void setProfile()
        {
            MvcApplication.is3token = true;
            Session["stage"] = Constants.ENVIRONMENT;
            if (SetProfile.SessionProfile == null)
                SetProfile.SessionProfile = SetProfile.CreateAPIProfile(Constants.API_USERNAME, Constants.API_PASSWORD, Constants.API_SIGNATURE, "", "", Constants.ENVIRONMENT, Constants.SUBJECT, Constants.OAUTH_SIGNATURE, Constants.OAUTH_TOKEN, Constants.OAUTH_TIMESTAMP);
        }

        private NVPCodec DirectPay(PaymentTransactionData data)
        {
            com.paypal.sdk.services.NVPCallerServices caller = PayPalAPI.PayPalAPIInitialize();
            NVPCodec encoder = new NVPCodec();
            encoder["METHOD"] = "DoDirectPayment";
            //encoder["PAYMENTACTION"] = this.Request.QueryString[Constants.PAYMENT_TYPE_PARAM];
            encoder["PAYMENTACTION"] = "Sale";
            encoder["AMT"] = data.ProductData.Amount.ToString();
            encoder["CURRENCYCODE"] = "USD";
            encoder["CREDITCARDTYPE"] = CreditCardUtility.GetCardTypeFromNumber(data.AccountData.CardNumber).ToString();
            encoder["ACCT"] = data.AccountData.CardNumber;
            encoder["EXPDATE"] = data.AccountData.ExpirationMonth.ToString() + data.AccountData.ExpirationYear.ToString();
            encoder["CVV2"] = data.AccountData.SecurityCode;
            //encoder["FIRSTNAME"] = data.CustomerData.FirstName;
            //encoder["LASTNAME"] = data.CustomerData.LastName;
            //encoder["STREET"] = data.CustomerData.AddressLine1;
            //encoder["CITY"] = data.CustomerData.City;
            //encoder["STATE"] = data.CustomerData.State;
            //encoder["ZIP"] = data.CustomerData.PostalCode;
            //encoder["COUNTRYCODE"] = data.CustomerData.Country;

            encoder["IPADDRESS"] = GetClientIP();

            string pStrrequestforNvp = encoder.Encode();
            string pStresponsenvp = caller.Call(pStrrequestforNvp);

            NVPCodec decoder = new NVPCodec();
            decoder.Decode(pStresponsenvp);

            //string strAck = decoder["ACK"];
            //if (strAck != null && (strAck == "Success" || strAck == "SuccessWithWarning"))
            //{
            //    Session["result"] = decoder;
            //    //string pStrResQue = "API=" + "DoDirect Payment ";
            //    //Response.Redirect("DoDirectPaymentReceipt.aspx?" + pStrResQue);
            //    return decoder["ACK"];
            //}
            //else
            //{
            //    Session["errorresult"] = decoder;
            //    //string pStrResQue = "API=" + "Error Detail ";
            //    //Response.Redirect("APIError.aspx?" + pStrResQue);
            //    //ClientScript.RegisterStartupScript(this.GetType(), "a", "alert('" + decoder["L_LONGMESSAGE0"] + "')");
            //    return decoder["L_LONGMESSAGE0"];
            //}
            return decoder;
        }

        private string GetClientIP()
        {
            string result = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty)
            {
                result = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (null == result || result == String.Empty)
            {
                result = System.Web.HttpContext.Current.Request.UserHostAddress;
            }
            return result;
        }

        private int ParseProductId(string queryStr)
        {
            int productId = -1;
            if (!string.IsNullOrEmpty(queryStr))
            {
                int.TryParse(queryStr, out productId);
            }
            return productId;
        }

        public static void GenerateSelectList(List<ProductData> products, ref SelectList outPremium, ref SelectList outVip)
        {
            List<ProductData> data_premium = new List<ProductData>();
            List<ProductData> data_vip = new List<ProductData>();

            foreach (var item in products)
            {
                if (item.Term.TermType == "VIP")
                {
                    data_vip.Add(item);
                }
                else
                {
                    data_premium.Add(item);
                }
            }

            if (data_premium.Count() != 0)
            {
                outPremium = new SelectList(data_premium, "ProductTermId", "Description", data_premium[0].ProductTermId);
                outVip = new SelectList(data_vip, "ProductTermId", "Description");
            }
            else if (data_vip.Count() != 0)
            {
                outVip = new SelectList(data_vip, "ProductTermId", "Description", data_vip[0].ProductTermId);
            }
        }

        public static List<Product> GetAllProducts()
        {
            var productData = (List<Product>)CacheHelper.Instance[PDS_PREFIX];
            if (productData == null)
            {
                productData = AccountInfoProvider.Instance.GetProducts();
                CacheHelper.Instance[PDS_PREFIX] = productData;
            }
            return productData;
        }

        public static List<ProductData> GetProductDataById(string productName)
        {
            string key = PT_PREFIX + productName;
            var productData = (List<ProductData>)CacheHelper.Instance[key];
            if (productData == null)
            {
                productData = AccountInfoProvider.Instance.GetTermsByProduct(productName);
                CacheHelper.Instance[key] = productData;
            }
            return productData;
        }
    }

    public class MultiButtonAttribute : ActionNameSelectorAttribute
    {
        public string Name { get; set; }
        public MultiButtonAttribute(string name)
        {
            this.Name = name;
        }
        public override bool IsValidName(ControllerContext controllerContext,
            string actionName, System.Reflection.MethodInfo methodInfo)
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                return false;
            }
            return controllerContext.HttpContext.Request.Form.AllKeys.Contains(this.Name);
        }
    }
}
