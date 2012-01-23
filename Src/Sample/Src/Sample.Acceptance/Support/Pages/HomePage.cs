//#if UI_Tests

using ProSpec.Acceptance.UI.WatiNIntegration;
using ProSpec.Acceptance.UI.Web;

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

//#endif