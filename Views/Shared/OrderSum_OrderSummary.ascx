<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Inywhere.PaymentGateway.MVCPresentation.Models.BillingFormModelBase>" %>
<%
    var productData = (List<Inywhere.PaymentGateway.DataContract.ProductData>)ViewData["ProductData"];
    var data = productData.Find(m => m.ProductTermId == Model.ProductTermsId);
%>
<table>
    <thead>
        <tr>
            <td>
                <label>
                    Items</label>
            </td>
            <td>
                <label>
                    Fees</label>
            </td>
        </tr>
        <tr>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>
                <label>
                    <%=data.Description %></label>
            </td>
            <td>
                $<%=data.Amount %>
            </td>
        </tr>
        <tr style="border-bottom: solid 2px gray">
            <td>
                <label>
                    Tax</label>
            </td>
            <td>
                $0.00
            </td>
        </tr>
        <tr>
            <td>
                Total
            </td>
            <td>
                $<%=data.Amount %>
            </td>
        </tr>
    </tbody>
</table>

