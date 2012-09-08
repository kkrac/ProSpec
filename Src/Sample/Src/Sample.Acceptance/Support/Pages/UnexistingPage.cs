using ProSpec.Core.UI.Web;
using ProSpec.WatiNIntegration;

namespace Sample.Acceptance.Support.Pages
{
    public class UnexistingPage : Page<WatiNBrowser>
    {
        protected override string RelativeUrl
        {
            get { return "ThisURLDoesNotExist!"; }
        }
    }
}