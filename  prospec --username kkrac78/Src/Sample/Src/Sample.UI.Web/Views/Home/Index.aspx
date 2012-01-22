<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <span id="Welcome">Welcome <%= TempData["userName"] %></span>
    <% using(Html.BeginForm("Logout", "UserAccount", FormMethod.Post, new { id = "logOutForm" })) { %>
        <input type="submit" id="logOut" value="Log out" />
    <%}%>
</asp:Content>
