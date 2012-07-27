<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ResultView
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <div style="height:600px;">
 <h2>Thanks for chosing Inywhere products. </h2>
    Your account will be actived with in few minutes.<br />
    An email has been sent to your mail box. Please check.
    <% Session.Remove("BillingModel"); %>
</div>
   
</asp:Content>
