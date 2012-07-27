<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Inywhere.PaymentGateway.MVCPresentation.Models.CCFormModel>" %>
<%
    var productData = Inywhere.PaymentGateway.MVCPresentation.Controllers.BillingFormController.GetProductDataById(Model.Product.ProductName);
    string key_vip = Inywhere.PaymentGateway.MVCPresentation.Controllers.BillingFormController.SL_VIP_PREFIX + Model.Product.ProductId;
    string key_premium = Inywhere.PaymentGateway.MVCPresentation.Controllers.BillingFormController.SL_PREMIUM_PREFIX + Model.Product.ProductId;
    SelectList sl_vip = (SelectList)Inywhere.PaymentGateway.MVCPresentation.Common.CacheHelper.Instance[key_vip];
    SelectList sl_premium = (SelectList)Inywhere.PaymentGateway.MVCPresentation.Common.CacheHelper.Instance[key_premium];

    if (sl_vip == null || sl_premium == null)
    {
        Inywhere.PaymentGateway.MVCPresentation.Controllers.BillingFormController.GenerateSelectList((List<Inywhere.PaymentGateway.DataContract.ProductData>)productData, ref sl_premium, ref sl_vip);
        Inywhere.PaymentGateway.MVCPresentation.Common.CacheHelper.Instance[key_vip] = sl_vip;
        Inywhere.PaymentGateway.MVCPresentation.Common.CacheHelper.Instance[key_premium] = sl_premium;
    }
%>
<div class="oneColumnForm">
    <div class="labelList">
        <%if (sl_premium != null)
          {
        %>
        <label>
            Premium plan</label>
        <ul type="circle">
            <%=Html.RadioButtonsFor(m => m.ProductTermsId, sl_premium)%>
        </ul>
        <%
            }
        %>
    </div>
    <div class="labelList">
        <%if (sl_vip != null)
          {
        %>
        <label>
            Vip plan</label>
        <ul type="circle">
            <%=Html.RadioButtonsFor(m => m.ProductTermsId, sl_vip)%>
        </ul>
        <%
            }
        %>
    </div>
    <div class="long-small-content">
        All prices are in US Dollars.</div>
</div>
