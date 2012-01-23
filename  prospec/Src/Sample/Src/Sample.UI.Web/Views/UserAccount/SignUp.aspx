<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Sample.UI.Web.Models.UserAccount.UserAccountNewViewModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Create</title>
    <script src="/Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
    <link rel="Stylesheet" href="/Content/Site.css" type="text/css" />
</head>
<body>
<fieldset>

<% Html.EnableClientValidation(); %>
<%= Html.ValidationSummary("Please correct the errors and try again")%>

<% using (Html.BeginForm("Create", "UserAccount", FormMethod.Post))
   { %>
    <div class="editor-label">
        <%= Html.LabelFor(u => u.UserName)%>
    </div>
    <div class="editor-field">
        <%= Html.TextBoxFor(u => u.UserName) %>
        <%= Html.ValidationMessageFor(u => u.UserName) %>
    </div>
    <div class="editor-label">
        <%= Html.LabelFor(u => u.FirstName)%>
    </div>
    <div class="editor-field">
        <%= Html.TextBoxFor(u => u.FirstName) %>
        <%= Html.ValidationMessageFor(u => u.FirstName) %>
    </div>
    <div class="editor-label">
        <%= Html.LabelFor(u => u.LastName) %>
    </div>
    <div class="editor-field">
        <%= Html.TextBoxFor(u => u.LastName) %>
        <%= Html.ValidationMessageFor(u => u.LastName) %>
    </div>
    <div class="editor-label">
        <%= Html.LabelFor(u => u.DateOfBirth) %>
    </div>
    <div class="editor-field">
        <%= Html.TextBoxFor(u => u.DateOfBirth) %>
        <%= Html.ValidationMessageFor(u => u.DateOfBirth) %>
    </div>
    <div class="editor-label">
        <%= Html.LabelFor(u => u.Email) %>
    </div>
    <div class="editor-field">
        <%= Html.TextBoxFor(u => u.Email) %>
        <%= Html.ValidationMessageFor(u => u.Email) %>
    </div>
    <div class="editor-label">
        <%= Html.LabelFor(u => u.Password) %>
    </div>
    <div class="editor-field">
        <%= Html.PasswordFor(u => u.Password)%>
        <%= Html.ValidationMessageFor(u => u.Password) %>
    </div>
    <div class="editor-label">
        <%= Html.LabelFor(u => u.PasswordConfirmation)%>
    </div>
    <div class="editor-field">
        <%= Html.PasswordFor(u => u.PasswordConfirmation)%>
        <%= Html.ValidationMessageFor(u => u.PasswordConfirmation) %>
    </div>
    <div class="editor-label">
        <%= Html.LabelFor(u => u.AcceptsTerms)%>
    </div>
    <div class="editor-field">
        <%= Html.CheckBoxFor(u => u.AcceptsTerms)%>
        <%= Html.ValidationMessageFor(u => u.AcceptsTerms) %>
    </div>
    <input type="submit" id="CreateAccount" value="Submit" />
<% } %>

</fieldset>
</body>
</html>
