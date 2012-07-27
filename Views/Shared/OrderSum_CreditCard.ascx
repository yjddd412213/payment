<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Inywhere.PaymentGateway.MVCPresentation.Models.BillingFormModelBase>" %>
<div class="oneColumnForm">
    <div>
        <label>
            Your credit card type:
        </label>
        <% 
            var cardType = Inywhere.PaymentGateway.CreditCardUtility.GetCardTypeFromNumber(((Inywhere.PaymentGateway.MVCPresentation.Models.CCFormModel)Model).CardNum);
            string cardImg = string.Format("<img style=\"display: inline-block\" width=\"30px\" height=\"20px\" alt=\"{0}\" src=\"Content/Resources/card_{0}.png\" />", cardType.ToString().ToLower());
            Response.Write(cardImg);
        %>
    </div>
    <div>
        <%=Html.DisplayTextFor(m => ((Inywhere.PaymentGateway.MVCPresentation.Models.CCFormModel)m).FirstName) %>
        <%=Html.DisplayTextFor(m => ((Inywhere.PaymentGateway.MVCPresentation.Models.CCFormModel)m).LastName) %>
        :
        <%=Html.DisplayTextFor(m => ((Inywhere.PaymentGateway.MVCPresentation.Models.CCFormModel)m).CardNum) %>
        Exp :
        <%=Html.DisplayTextFor(m => ((Inywhere.PaymentGateway.MVCPresentation.Models.CCFormModel)m).ExpireMonth) %>
        /
        <%=Html.DisplayTextFor(m => ((Inywhere.PaymentGateway.MVCPresentation.Models.CCFormModel)m).ExpireYear) %>
    </div>
    <div>
        <div id="fees" style="display: block">
            <br />
            Your credit card may apply additional fees for when exchanging from non-US currency.</div>
    </div>
</div>
