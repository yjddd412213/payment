<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Inywhere.PaymentGateway.MVCPresentation.Models.CCFormModel>" %>
<div>
    <div class="oneColumnForm">
        <div>
            <%=Html.LabelFor(m => m.InywhereAccount.Account)%>
            <%=Html.TextBoxFor(m => m.InywhereAccount.Account, new { MaxLength = "24"})%> *
            <%=Html.ValidationMessageFor(m => m.InywhereAccount.Account)%>
        </div>
        <%--<div>
            <%=Html.LabelFor(m => m.InywhereAccount.Password)%>
            <%=Html.PasswordFor(m => m.InywhereAccount.Password, new { MaxLength = "24" })%> *
            <%=Html.ValidationMessageFor(m => m.InywhereAccount.Password)%>
        </div>--%>
    </div>
</div>
