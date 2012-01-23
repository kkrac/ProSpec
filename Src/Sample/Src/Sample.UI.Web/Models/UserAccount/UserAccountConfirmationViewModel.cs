using System.ComponentModel;

namespace Sample.UI.Web.Models.UserAccount
{
    public class UserAccountConfirmationViewModel
    {
        [DisplayName("User name")]
        public string UserName { get; set; }
        [DisplayName("Token")]
        public string Token { get; set; }
    }
}
