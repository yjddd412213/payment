<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Inywhere.PaymentGateway.MVCPresentation.Models.BillingFormModelBase>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Inywhere payment gateway order summary
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" language="javascript">
        javascript: window.history.forward(1);
    </script>
    <!--[if IE 7]>
    <liNK href="../../Content/layout-ie7.css" type="text/css" rel="stylesheet"/>
    <![endif]-->
   <%-- <link media="all" href="../../Content/account.css" type="text/css" rel="stylesheet" />--%>
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- CONTENT -->
    <script src="../../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var method = $("input[name='method'] ").attr("value");
            //alert(method);
            if (method == "Card") {
                $("#paymethod").show();
                $("#billing").show();
            }
            else {
                $("#paymethod").hide();
                $("#billing").hide();
            }
        });
    </script>
    <input type="hidden" id="method" name="method" value="<%= Session["method"] %>" />
    <div id="col_wrap">
        <div id="col_wrap2">
            <div id="col_wrap3">
                <div id="col_wrap4">
                    <div id="banner"  style=" height:55px;">
                        <div style="float: left; font-size: 1.80em;">
                              <img src="Content/resources/title.png" height="50px" border="0" />
                        </div>
                       <%-- <div id="banner_links">
                            <a href="https://www.evernote.com/Home.action">« Return to notes</a>
                        </div>--%>
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
                            <div class="colx3" id="CheckoutCombinedDetails">
                                <div class="box">
                                    <div class="box2">
                                        <div id="account">
                                            <h2>
                                                Inywhere Account</h2>
                                            <%Html.RenderPartial("OrderSum_Account"); %>
                                        </div>
                                        <div id="order">
                                            <h2>
                                                Order Summary</h2>
                                            <%Html.RenderPartial("OrderSum_OrderSummary"); %>
                                        </div>
                                        <div id="paymethod" style="display:none;">
                                            <h2>
                                                Payment Method</h2>
                                            <%Html.RenderPartial("OrderSum_PaymentMethod"); %>
                                        </div>
                                        <div id="billing" style="display:none;">
                                            <h2>
                                                Billing Address</h2>
                                            <%Html.RenderPartial("OrderSum_BillingAddress"); %>
                                        </div>
                                        <div class="icoorder right" style="margin: 15px 63px 15px 100px">
                                            <span>
                                                <% using (Html.BeginForm("OrderSummary", "BillingForm"))
                                                   { %>
                                                <input type="submit" value="" style="border:none; background-image:url('Content/Resources/order.jpg');" />
                                                <% } %>
                                            </span>
                                        </div>
                                       <%-- <h2>
                                            Choose Other Payment Method</h2>
                                        <%Html.RenderPartial("Input_PaymentOption"); %>--%>
                                        <!-- box2 -->
                                    </div>
                                    <!-- box -->
                                </div>
                                <!-- colx2 -->
                            </div>
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
