using System;

namespace Sample.UI.Web.Models.UserAccount
{
    public interface IUserAccountNew : IUserAccountLogin
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        DateTime DateOfBirth { get; set; }
        string Email { get; set; }
        string PasswordConfirmation { get; set; }
        bool AcceptsTerms { get; set; }
    }
}
