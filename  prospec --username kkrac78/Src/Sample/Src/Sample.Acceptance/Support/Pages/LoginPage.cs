using ProSpec.Acceptance.UI.WatiNIntegration;
using ProSpec.Acceptance.UI.Web;
using Sample.UI.Web.Models.UserAccount;
using Should;
using WatiN.Core;

namespace Sample.Acceptance.Support.Pages
{
    public class LoginPage : Page<WatiNBrowser>
    {
        protected override void Validate()
        {
            UserNameField.Exists.ShouldBeTrue();
            UserNameField.GetAttributeValue("type").ToLower().ShouldEqual("text");

            PasswordField.Exists.ShouldBeTrue();
            PasswordField.GetAttributeValue("type").ToLower().ShouldEqual("password");

            LoginButton.Exists.ShouldBeTrue();
            LoginButton.GetAttributeValue("type").ToLower().ShouldEqual("submit");

            CreateAccountLink.Exists.ShouldBeTrue();
        }

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

        public void Login(UserAccountLoginViewModel login)
        {
            UserName = login.UserName;
            Password = login.Password;
            
            Submit<HomePage>(
                () => LoginButton.Click()
            );
        }

        public void GoToSignUp()
        {
            Submit<UserAccountSignUpPage>(
                () => CreateAccountLink.Click()
            );
        }
    }
}