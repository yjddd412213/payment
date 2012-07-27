<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Inywhere.PaymentGateway.MVCPresentation.Models.CCFormModel>" %>
<div class="oneColumnForm">
   <%-- <div>
        <label>
            We accept</label>
        <img style="display: inline-block" alt="Visa, MasterCard, American Express, JCB, Discover, Diners"
            src="Content/Resources/cards.png" />
    </div>--%>
    <div>
        <%=Html.LabelFor(m => m.CardNum) %>
        <%=Html.TextBoxFor(m => m.CardNum) %>
        *
        <%=Html.ValidationMessageFor(m => m.CardNum) %>
        <br />
    </div>
    <div>
        <label class="required" for="expires">
            Expires</label>
        <%=Html.DropDownListFor(m => m.ExpireMonth, Inywhere.PaymentGateway.MVCPresentation.Common.GatewayFormHelper.MonthList)%>
        /
        <%=Html.DropDownListFor(m => m.ExpireYear, Inywhere.PaymentGateway.MVCPresentation.Common.GatewayFormHelper.YearList)%>
        <br />
    </div>
    <div>
        <%=Html.LabelFor(m => m.SecurityCode) %>
        <%=Html.TextBoxFor(m => m.SecurityCode, new { style="width: 40px", size=4 }) %>
        *
        <%=Html.ValidationMessageFor(m => m.SecurityCode) %>
    </div>
    <div>
        <br />
    </div>
    <div>
        <%=Html.LabelFor(m => m.FirstName) %>
        <%=Html.TextBoxFor(m => m.FirstName, new { maxlenght="255"}) %>
        *
        <%=Html.ValidationMessageFor(m => m.FirstName) %>
    </div>
    <div>
        <%=Html.LabelFor(m => m.LastName) %>
        <%=Html.TextBoxFor(m => m.LastName, new { maxlenght="255"}) %>
        *
        <%=Html.ValidationMessageFor(m => m.LastName) %>
    </div>
    <div>
        <%=Html.LabelFor(m => m.Address) %>
        <%=Html.TextBoxFor(m => m.Address, new { maxlenght="255"}) %>
        *
        <%=Html.ValidationMessageFor(m => m.Address) %>
    </div>
    <div>
        <%=Html.LabelFor(m => m.Address2) %>
        <%=Html.TextBoxFor(m => m.Address2, new { maxlenght="255"}) %>
        <%=Html.ValidationMessageFor(m => m.Address2) %>
    </div>
    <div>
        <%=Html.LabelFor(m => m.City) %>
        <%=Html.TextBoxFor(m => m.City, new { maxlenght="24"}) %>
        *
        <%=Html.ValidationMessageFor(m => m.City) %>
    </div>
    <div>
        <%=Html.LabelFor(m => m.State) %>
        <%=Html.TextBoxFor(m => m.State, new { maxlenght="24"}) %>
        *
        <%=Html.ValidationMessageFor(m => m.State) %>
    </div>
    <div>
        <%=Html.LabelFor(m => m.PostalCode) %>
        <%=Html.TextBoxFor(m => m.PostalCode, new { maxlenght="10"}) %>
        *
        <%=Html.ValidationMessageFor(m => m.PostalCode) %>
    </div>
    <div>
        <%=Html.LabelFor(m => m.Country) %>
        <%=Html.DropDownListFor(m => m.Country, Inywhere.PaymentGateway.MVCPresentation.Common.GatewayFormHelper.GetCountryList())%>
    </div>
    <div>
        <label>
            <sup>*</sup> is required field.</label>
    </div>
    <div>
        <div id="fees" style="display: block">
            <br />
            Your credit card may apply additional fees for when exchanging from non-US currency.</div>
    </div>
    <div class="icosubmit right" style="margin: 15px 63px 15px 100px; cursor:hand;">
        <span>
            <input type="submit" name="Card" value="" style="border: none; background-image: url('Content/Resources/complete.jpg');" />
        </span>
    </div>
</div>
