using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using log4net;
using System.ComponentModel;

namespace Inywhere.PaymentGateway.MVCPresentation
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{pname}", // URL with parameters
                new { controller = "BillingForm", action = "CCForm", pname = UrlParameter.Optional } // Parameter defaults
            );
            routes.MapRoute("Premium_Template", "Premium_Template/");
            routes.MapRoute(
                "Paypal", // Route name
                "{controller}/{action}", // URL with parameters
                new { controller = "BillingForm", action = "Paypal"} // Parameter defaults
            );
        }

        protected void Application_Start()
        {
           log4net.Config.XmlConfigurator.Configure();
           AreaRegistration.RegisterAllAreas();

           RegisterRoutes(RouteTable.Routes);
        }


        public static readonly ILog log = LogManager.GetLogger("com.paypal.sdk.samples");
        public static bool is3token;
        public static bool isunipay;
        public static bool ispermission;

        private IContainer components = null;

        public void Global()
        {
            InitializeComponent();
        }

        #region Web Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new Container();
        }
        #endregion
    }
}