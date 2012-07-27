<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Inywhere.PaymentGateway.MVCPresentation.Models.BillingFormModelBase>" %>

<% 
    if (Model is Inywhere.PaymentGateway.MVCPresentation.Models.CCFormModel)
    {
        Html.RenderPartial("OrderSum_CreditCard");
    }
    else if (Model is Inywhere.PaymentGateway.MVCPresentation.Models.PPFormModel)
    { }
    else if (Model is Inywhere.PaymentGateway.MVCPresentation.Models.GCFormModel)
    { }
    %>
