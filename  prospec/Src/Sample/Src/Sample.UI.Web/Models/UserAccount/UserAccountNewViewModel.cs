using System;
using System.ComponentModel;
using Sample.Domain.Model;
using Sample.Domain.Model.User;

namespace Sample.UI.Web.Models.UserAccount
{
    public class UserAccountNewViewModel : IUserAccountNew, IViewModel<AuthenticatedUser>
    {
        [DisplayName("First Name")]
        public string FirstName {get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Date of birth")]
        public DateTime DateOfBirth { get; set; }
        [DisplayName("E-mail")]
        public string Email { get; set; }
        [DisplayName("Confirm password")]
        public string PasswordConfirmation { get; set; }
        [DisplayName("Accept terms of agreement")]
        public bool AcceptsTerms { get; set; }
        [DisplayName("User name")]
        public string UserName { get; set; }
        [DisplayName("Password")]
        public string Password { get; set; }

        public AuthenticatedUser ToModel()
        {
            return new AuthenticatedUser(UserName, FirstName, LastName, DateOfBirth, new Email(Email));
        }
    }
}
