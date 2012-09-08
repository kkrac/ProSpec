using ProSpec.Core.UI.Web;
using ProSpec.WatiNIntegration;
using Sample.UI.Web.Models.UserAccount;

namespace Sample.Acceptance.Support.Pages.UserAccount
{
    public class ConfirmationPage : Page<WatiNBrowser>
    {
        protected override string RelativeUrl
        {
            get { return "UserAccount/Confirm"; }
        }

        public void Submit(UserAccountConfirmationViewModel confirmation)
        {
            Submit<LoginPage>(
                () => Go(confirmation.UserName, confirmation.Token)
            );
        }
    }
}