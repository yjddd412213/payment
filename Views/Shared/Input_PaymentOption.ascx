<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Inywhere.PaymentGateway.MVCPresentation.Models.CCFormModel>" %>
<div class="othersubmit">
    <%--<div class="checkoutGoogleImage">
        <input type="image" src="Content/Resources/checkout.gif" value="Google" name="googleCheckout" />
    </div>
    <div class="checkoutPaymentdivider">
        <img src="Content/Resources/divider.gif" alt="" /></div>
    <div class="checkoutPaypalImage">
        <input type="image" src="Content/Resources/paypal_checkout.gif" value="Google" name="googleCheckout" />
    </div>--%>
    <div style="text-align:center; cursor:hand;">
        <input type="submit" style="background-image: url('https://www.paypal.com/en_US/i/btn/btn_xpressCheckout.gif'); width:145px; border:none; height:42px; background-repeat:no-repeat;" 
         value="" name="Paypal">
    </div>
</div>
