using Krac.Framework.Acceptance.UI.WatiNIntegration;
using Should;
using WatiN.Core;

namespace Sample.Acceptance.UI.Pages
{
    public class CreateAccountPage : WatiNPageBase, ILogin
    {
        public CreateAccountPage(Browser browser) : base(browser) 
        {
            UserNameField.Exists.ShouldBeTrue();
            UserNameField.GetAttributeValue("type").ToLower().ShouldEqual("text");

            PasswordField.Exists.ShouldBeTrue();
            PasswordField.GetAttributeValue("type").ToLower().ShouldEqual("password");

            FirstNameField.Exists.ShouldBeTrue();
            FirstNameField.GetAttributeValue("type").ToLower().ShouldEqual("text");

            LastNameField.Exists.ShouldBeTrue();
            LastNameField.GetAttributeValue("type").ToLower().ShouldEqual("text");

            DateOfBirthField.Exists.ShouldBeTrue();
            DateOfBirthField.GetAttributeValue("type").ToLower().ShouldEqual("text");

            EmailField.Exists.ShouldBeTrue();
            EmailField.GetAttributeValue("type").ToLower().ShouldEqual("text");

            AcceptTermsField.Exists.ShouldBeTrue();
            
            CreateAccountButton.Exists.ShouldBeTrue();
            CreateAccountButton.GetAttributeValue("type").ToLower().ShouldEqual("submit");
        }

        #region Fields

        private TextField UserNameField
        {
            get { return Browser.TextField("Id"); }
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

        private CheckBox AcceptTermsField
        {
            get { return Browser.CheckBox("AcceptsTerms"); }
        }

        private Button CreateAccountButton
        {
            get { return Browser.Button("CreateAccount"); }
        }

        #endregion Fields

        public string UserName
        {
            get { return UserNameField.Value; }
            set { UserNameField.TypeText(value); }
        }

        public string Password
        {
            get { return PasswordField.Value; }
            set { PasswordField.TypeText(value); }
        }

        public string PasswordConfirmation
        {
            get { return PasswordConfirmationField.Value; }
            set { PasswordConfirmationField.TypeText(value); }
        }

        public string FirstName
        {
            get { return FirstNameField.Value; }
            set { FirstNameField.TypeText(value); }
        }

        public string LastName
        {
            get { return LastNameField.Value; }
            set { LastNameField.TypeText(value); }
        }

        public string DateOfBirth
        {
            get { return DateOfBirthField.Value; }
            set { DateOfBirthField.TypeText(value); }
        }

        public string Email
        {
            get { return EmailField.Value; }
            set { EmailField.TypeText(value); }
        }

        public bool AcceptTerms
        {
            get { return AcceptTermsField.Checked; ; }
            set { AcceptTermsField.Checked = value; }
        }

        protected override string RelativeUrl
        {
            get { return "Account/Register"; }
        }

        public HomePage CreateAccount()
        {
            CreateAccountButton.Click();

            return new HomePage(Browser);
        }
    }
}
