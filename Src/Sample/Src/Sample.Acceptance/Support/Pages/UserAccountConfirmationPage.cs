using ProSpec.Core.UI.Web;
using ProSpec.WatiNIntegration;
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