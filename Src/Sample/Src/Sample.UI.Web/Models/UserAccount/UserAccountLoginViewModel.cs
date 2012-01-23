using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sample.UI.Web.Models.UserAccount
{
    public class UserAccountLoginViewModel : IUserAccountLogin
    {
        [Required(ErrorMessage="The user name is required")]
        [DisplayName("User name")]
        public string UserName { get; set; }

        [Required(ErrorMessage="The password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
