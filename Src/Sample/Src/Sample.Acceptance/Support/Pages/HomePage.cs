using ProSpec.WatiNIntegration;
using ProSpec.Core.UI.Web;

namespace Sample.Acceptance.Support.Pages
{
    public class HomePage : Page<WatiNBrowser>
    {
        protected override string RelativeUrl
        {
            get { return "Home/Index"; }
        }
    }
}