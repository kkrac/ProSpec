using ProSpec.Core.UI.Web;
using ProSpec.WatiNIntegration;
using Sample.UI.Web.Models.UserAccount;
using Should;
using WatiN.Core;

namespace Sample.Acceptance.Support.Pages.UserAccount
{
    public class LoginPage : Page<WatiNBrowser>
    {
        private TextField UserNameField
        {
            get { return Browser.TextField("UserName"); }
        }

        private TextField PasswordField
        {
            get { return Browser.TextField("Password"); }
        }

        private Button LoginButton
        {
            get { return Browser.Button("LogIn"); }
        }

        private Link CreateAccountLink
        {
            get { return Browser.Link("CreateAccount"); }
        }

        private string UserName
        {
            set { UserNameField.TypeText(value); }
        }

        private string Password
        {
            set { PasswordField.TypeText(value); }
        }

        protected override string RelativeUrl
        {
            get { return "UserAccount/Login"; }
        }

        public void Submit(UserAccountLoginViewModel login)
        {
            UserName = login.UserName;
            Password = login.Password;
            
            Submit<HomePage>(
                () => LoginButton.Click()
            );
        }

        public void GoToSignUp()
        {
            Submit<SignUpPage>(
                () => CreateAccountLink.Click()
            );
        }
    }
}