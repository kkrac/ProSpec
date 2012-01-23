using ProSpec.Acceptance.UI.WatiNIntegration;
using ProSpec.Acceptance.UI.Web;
using Sample.UI.Web.Models.UserAccount;

namespace Sample.Acceptance.Support.Pages
{
    public class UserAccountConfirmationPage : Page<WatiNBrowser>
    {
        protected override string RelativeUrl
        {
            get { return "UserAccount/Confirm"; }
        }

        public void ConfirmAccount(UserAccountConfirmationViewModel confirmation)
        {
            Submit<LoginPage>(
                () => Go(confirmation.UserName, confirmation.Token)
            );
        }
    }
}