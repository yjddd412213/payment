<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Inywhere.PaymentGateway.MVCPresentation.Models.CCFormModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Inywhere credit card payment gateway.
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <!--[if IE 7]>
    <liNK href="../../Content/layout-ie7.css" type="text/css" rel="stylesheet"/>
    <![endif]-->
    <link media="all" href="../../Content/account.css" type="text/css" rel="stylesheet" />
    <!--[if IE]>
    <liNK href="../../Content/account-ie.css" type="text/css" rel="stylesheet"/>
    <![endif]-->
    <%--<link href="" type=image/x-icon rel="Shortcut Icon"/>--%>
    <style type="text/css">
        .oneColumnForm div LABEL
        {
            min-width: 130px;
            display: inline-block;
            font-weight: bold;
            margin-left: 10px;
            width: 130px;
            color: black;
        }
        
        .oneColumnForm div LABEL.error
        {
            color: red;
        }
        
        .error-status.oneColumnForm div
        {
            margin-left: 0px;
        }
        
        .oneColumnForm div
        {
            line-height: 24px;
        }
        
        .checkoutGoogleImage
        {
            float: left;
            width: 175px;
            text-align: center;
        }
        
        .checkoutPaypalImage
        {
            float: left;
            width: 155px;
            padding-top: 2px;
            text-align: center;
        }
        
        .checkoutPaymentdivider
        {
            float: left;
            margin-left: 25px;
            width: 1px;
            margin-right: 25px;
        }
        
        .checkoutPaypalFail
        {
            width: 100%;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <!-- CONTENT -->
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var value = $('#type').val();
            //alert(value);
            if (value == 'paypal')
                paypalSelected();
            else
                cardSelected();

            $(':radio[name="paymethod"]').change(function () {
                // read the value of the selected radio
                var value = $(this).val();
                $('#type').attr("value", value);
                if (value == 'paypal') {
                    paypalSelected();
                }
                else {
                    cardSelected();
                }
            });
        });

        function paypalSelected() {
            $('#div_card').hide();
            $('#div_paypal').show("slow");
            $('#CardNum').attr("value", '370000000000002');
            $('#SecurityCode').attr("value", '0');
            $('#FirstName').attr("value", '0');
            $('#LastName').attr("value", '0');
            $('#Address').attr("value", '0');
            $('#City').attr("value", '0');
            $('#State').attr("value", '0');
            $('#PostalCode').attr("value", '0');
        }

        function cardSelected() {
            $('#div_card').show("slow");
            $('#div_paypal').hide();
            $('#CardNum').attr("value", '');
            $('#SecurityCode').attr("value", '');
            $('#FirstName').attr("value", '');
            $('#LastName').attr("value", '');
            $('#Address').attr("value", '');
            $('#City').attr("value", '');
            $('#State').attr("value", '');
            $('#PostalCode').attr("value", '');
        }
    </script>
    <div id="col_wrap">
        <div id="col_wrap2">
            <div id="col_wrap3">
                <div id="col_wrap4">
                    <div id="banner" style="height: 55px;">
                        <div style="float: left; font-size: 1.80em;">
                            <img src="Content/Resources/title.png" height="50px" border="0" alt=""/>
                        </div>
                        <%--  <div id="banner_links">
                            <a href="https://www.inywhere.com">« Return to products</a>
                        </div>--%>
                        <input type="hidden" id="type" value="paypal" />
                    </div>
                    <!-- combined -->
                    <div id="content">
                        <div class="column" id="CheckoutBenefits">
                            <div class="colx2">
                                <!-- Premium template-->
                                <%
                                    if (!Model.Product.IsVip)
                                    {
                                        Html.RenderPartial("Template_Premium_" + Model.Product.ProductName);
                                    }
                                %>
                                <!-- END OF Premium template-->
                                <br />
                                <%Html.RenderPartial("Template_Vip"); %>
                            </div>
                        </div>
                        <!-- column -->
                        <div class="columnright" id="CheckoutDetails">
                            <!--  User's premium status is NONE, Pending or Canceled or Failed -->
                            <% using (Html.BeginForm("CCForm", "BillingForm"))
                               { %>
                            <%=Html.HiddenFor(m => m.Product.ProductId)%>
                            <%=Html.HiddenFor(m => m.Product.ProductName)%>
                            <%=Html.HiddenFor(m => m.Product.IsVip)%>
                            <div class="colx3" id="CheckoutCombinedDetails">
                                <div class="box">
                                    <%=Html.ValidationMessage(Inywhere.PaymentGateway.MVCPresentation.Common.ErrorType.TransactionProcessError.ToString())%>
                                    <%=Html.ValidationMessage(Inywhere.PaymentGateway.MVCPresentation.Common.ErrorType.InvalidCardNumber.ToString())%>
                                    <div class="box2">
                                        <h2>
                                            Provide your Inywhere account</h2>
                                        <%=Html.ValidationMessage(Inywhere.PaymentGateway.MVCPresentation.Common.ErrorType.AccountAuthenticateError.ToString())%>
                                        <%Html.RenderPartial("Input_Account"); %>
                                        <h2>
                                            Choose your payment plan</h2>
                                        <%Html.RenderPartial("Input_PaymentPlan"); %>
                                        <h2>
                                            Choose your payment method
                                        </h2>
                                        <div>
                                            <ul style="margin-left:20px">
                                                <li>
                                                    <input type="radio" id="paypal" checked="checked" name="paymethod" value="paypal"/>
                                                    <img src="https://www.paypal.com/en_US/i/logo/PayPal_mark_37x23.gif" style="display: inline-block;
                                                        margin: 0; padding: 0;" border="0" alt="PayPal Logo">
                                                </li>
                                                <li>
                                                    <input type="radio" id="card" name="paymethod" value="card" />
                                                    <%--<img style="display: inline-block" alt="Visa, MasterCard" src="Content/Resources/card_master.png" />
                                                    <img style="display: inline-block" alt="Visa, MasterCard" src="Content/Resources/card_visa.png" />--%>
                                                    <img style="display: inline-block" alt="Visa, MasterCard" src="Content/Resources/cards.png" />
                                                </li>
                                            </ul>
                                        </div>
                                        <div id="div_paypal">
                                            <%Html.RenderPartial("Input_PaymentOption"); %>
                                        </div>
                                        <div id="div_card" style="display: none;">
                                            <%Html.RenderPartial("Input_CCForm"); %>
                                        </div>
                                        <!-- box2 -->
                                    </div>
                                    <!-- box -->
                                </div>
                                <!-- colx2 -->
                            </div>
                            <% } %>
                            <!-- column -->
                        </div>
                    </div>
                    <!-- content -->
                    <!-- /combined -->
                </div>
                <!-- /col_wrap4 -->
            </div>
            <!-- /col_wrap3 -->
        </div>
        <!-- /col_wrap2 -->
    </div>
    <!-- /col_wrap -->
    <!-- /CONTENT -->
</asp:Content>
