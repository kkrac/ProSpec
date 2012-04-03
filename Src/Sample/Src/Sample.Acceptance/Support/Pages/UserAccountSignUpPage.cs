using ProSpec.Core.UI.Web;
using ProSpec.WatiNIntegration;
using Sample.UI.Web.Models.UserAccount;
using Should;
using WatiN.Core;

namespace Sample.Acceptance.Support.Pages
{
    public class UserAccountSignUpPage : Page<WatiNBrowser>
    {
        #region Fields

        private TextField UserNameField
        {
            get { return Browser.TextField("UserName"); }
        }

        private TextField PasswordField
        {
            get { return Browser.TextField("Password"); }
        }

        private TextField PasswordConfirmationField
        {
            get { return Browser.TextField("PasswordConfirmation"); }
        }

        private TextField FirstNameField
        {
            get { return Browser.TextField("FirstName"); }
        }

        private TextField LastNameField
        {
            get { return Browser.TextField("LastName"); }
        }

        private TextField DateOfBirthField
        {
            get { return Browser.TextField("DateOfBirth"); }
        }

        private TextField EmailField
        {
            get { return Browser.TextField("Email"); }
        }

        private CheckBox AcceptsTermsField
        {
            get { return Browser.CheckBox("AcceptsTerms"); }
        }

        private Button CreateAccountButton
        {
            get { return Browser.Button("CreateAccount"); }
        }

        #endregion Fields

        private string UserName
        {
            set { UserNameField.TypeText(value); }
        }

        private string Password
        {
            set { PasswordField.TypeText(value); }
        }

        private string PasswordConfirmation
        {
            set { PasswordConfirmationField.TypeText(value); }
        }

        private string FirstName
        {
            set { FirstNameField.TypeText(value); }
        }

        private string LastName
        {
            set { LastNameField.TypeText(value); }
        }

        private string DateOfBirth
        {
            set { DateOfBirthField.TypeText(value); }
        }

        private string Email
        {
            set { EmailField.TypeText(value); }
        }

        private bool AcceptsTerms
        {
            set { AcceptsTermsField.Checked = value; }
        }

        protected override string RelativeUrl
        {
            get { return "UserAccount/SignUp"; }
        }

        protected override void Validate()
        {
            UserNameField.Exists.ShouldBeTrue("UserName field not found");
            UserNameField.GetAttributeValue("type").ToLower().ShouldEqual("text");

            PasswordField.Exists.ShouldBeTrue("Password field not found");
            PasswordField.GetAttributeValue("type").ToLower().ShouldEqual("password");

            FirstNameField.Exists.ShouldBeTrue("FirstName field not found");
            FirstNameField.GetAttributeValue("type").ToLower().ShouldEqual("text");

            LastNameField.Exists.ShouldBeTrue("LastName field not found");
            LastNameField.GetAttributeValue("type").ToLower().ShouldEqual("text");

            DateOfBirthField.Exists.ShouldBeTrue("DateOfBirth field not found");
            DateOfBirthField.GetAttributeValue("type").ToLower().ShouldEqual("text");

            EmailField.Exists.ShouldBeTrue("Email field not found");
            EmailField.GetAttributeValue("type").ToLower().ShouldEqual("text");

            AcceptsTermsField.Exists.ShouldBeTrue("AcceptsTerms field not found");

            CreateAccountButton.Exists.ShouldBeTrue("CreateAccount button not found");
            CreateAccountButton.GetAttributeValue("type").ToLower().ShouldEqual("submit");
        }

        public void CreateAccount(UserAccountNewViewModel account)
        {
            UserName            = account.UserName;
            FirstName           = account.FirstName;
            LastName            = account.LastName;
            DateOfBirth         = account.DateOfBirth.ToString();
            Email               = account.Email;
            AcceptsTerms        = account.AcceptsTerms;
            Password            = account.Password;
            PasswordConfirmation= account.PasswordConfirmation;

            //Forward(() => CreateAccountButton.Click(), this);
        }
    }
}