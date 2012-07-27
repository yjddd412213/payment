<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Inywhere.PaymentGateway.MVCPresentation.Models.BillingFormModelBase>" %>
<div class="oneColumnForm">
    <div>
        <b>
        <%=Html.DisplayTextFor(m => ((Inywhere.PaymentGateway.MVCPresentation.Models.CCFormModel)m).FirstName) %>
        <%=Html.DisplayTextFor(m => ((Inywhere.PaymentGateway.MVCPresentation.Models.CCFormModel)m).LastName) %></b><br />
        <%=Html.DisplayTextFor(m => ((Inywhere.PaymentGateway.MVCPresentation.Models.CCFormModel)m).Address) %><br />
        <%if(string.IsNullOrEmpty(((Inywhere.PaymentGateway.MVCPresentation.Models.CCFormModel)Model).Address2))
    {
        Response.Write(((Inywhere.PaymentGateway.MVCPresentation.Models.CCFormModel)Model).Address2 + "<br/>");
    }
        %>
        <%=Html.DisplayTextFor(m => ((Inywhere.PaymentGateway.MVCPresentation.Models.CCFormModel)m).Country) %>
    </div>
    <div>
        <div id="fees" style="display: block">
            <br />
            Your credit card may apply additional fees for when exchanging from non-US currency.</div>
    </div>
</div>
