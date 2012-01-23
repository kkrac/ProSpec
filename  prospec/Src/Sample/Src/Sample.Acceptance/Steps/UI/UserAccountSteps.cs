//#if Browser_Tests

using Sample.Acceptance.Support;
using Sample.Acceptance.Support.Pages;
using Sample.UI.Web.Models.UserAccount;
using Should;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Sample.Acceptance.Steps.UI
{
    [Binding]
    public class UserAccountSteps : UserAccountStepsContext
    {
        [Given(@"I am on the log in page")]
        public void Given_I_Am_On_The_Login_Page()
        {
            FlowManager.LoadAndGo<LoginPage>();
        }

        [Given(@"I am on the sign up page")]
        public void Given_I_Am_On_The_Sign_Up_Page()
        {
            FlowManager.LoadAndGo<UserAccountSignUpPage>();
        }

        [When(@"I confirm an account with the following information")]
        public void When_I_Confirm_An_Account_With_The_Following_Information(Table table)
        {
            string userId = table.Rows[0]["UserName"];

            CurrentUser = UserAccountStepsUtils.GetUser(userId);

            UserAccountConfirmationPage page = FlowManager.Load<UserAccountConfirmationPage>();

            UserAccountConfirmationViewModel accountConfirmation = table.CreateInstance<UserAccountConfirmationViewModel>();
            
            page.ConfirmAccount(accountConfirmation);
        }

        [When(@"I go to the sign up page")]
        public void When_I_Go_To_The_Sign_Up_Page()
        {
            Driver.OfType<LoginPage>().GoToSignUp();
        }

        [When(@"I submit the following user account information")]
        public void When_I_Submit_The_Following_User_Account_Information(Table table)
        {
            UserAccountNewViewModel account = table.CreateInstance<UserAccountNewViewModel>();

            Driver.OfType<UserAccountSignUpPage>().CreateAccount(account);
        }

        [When(@"I submit the following login information")]
        public void When_I_Submit_The_Following_Log_in_Information(Table table)
        {
            UserAccountLoginViewModel login = table.CreateInstance<UserAccountLoginViewModel>();

            Driver.OfType<LoginPage>().Login(login);
        }

        [Then(@"^(?:I should be redirected to|I should continue on) the home page")]
        public void Then_I_Should_Be_On_The_Home_Page()
        {
            Driver.ShouldBeType<HomePage>();
        }

        [Then(@"^(?:I should be redirected to|I should continue on) the account confirmation page")]
        public void Then_I_Should_Be_On_The_Account_Confirmation_Page()
        {
            Driver.ShouldBeType<UserAccountConfirmationPage>();
        }

        [Then(@"^(?:I should be redirected to|I should continue on) the log in page")]
        public void Then_I_Should_Be_On_The_Log_In_Page()
        {
            Driver.ShouldBeType<LoginPage>();
        }

        [Then(@"the account should become active")]
        public void Then_The_Account_Should_Become_Active()
        {
            bool isActive = UserAccountStepsUtils.IsUserActive(CurrentUser.Id);

            isActive.ShouldBeTrue();
        }

        [Then(@"^(?:I should be redirected to|I should continue on) the temporary account page")]
        public void Then_I_Should_Be_Redirected_To_The_Temporary_Account_Page()
        {
            ScenarioContext.Current.Pending();
        }
    }
}

//#endif