<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Sample.UI.Web.Models.UserAccount.UserAccountLoginViewModel>" %>
<%@ Import Namespace="Sample.UI.Web" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>log in</title>
    <link rel="Stylesheet" href="/Content/Site.css" type="text/css" />
</head>
<body>
<fieldset>
kevin!!!
<% Html.EnableClientValidation(); %>
<%= Html.ValidationSummary("Please correct the errors and try again") %>

    <% using (Html.BeginForm()) {%>
        <%= Html.EditorForModel() %><br />
        <input type="submit" id="LogIn" value="Log in" />
    <%} %>
 <br />
 <span id="noAccount">You don't have an account?</span>&nbsp;<%= Html.ActionLink("Create one", "SignUp", new { controller = "UserAccount" }, new { id = "CreateAccount" })%>
 
</fieldset>

</body>
</html>
