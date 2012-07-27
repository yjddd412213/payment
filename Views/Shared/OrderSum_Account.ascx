<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Inywhere.PaymentGateway.MVCPresentation.Models.BillingFormModelBase>" %>
<div>
    <div class="oneColumnForm">
        The account to be Charged : <%=Html.DisplayTextFor(m => m.InywhereAccount.Account )%>
    </div>
</div>
