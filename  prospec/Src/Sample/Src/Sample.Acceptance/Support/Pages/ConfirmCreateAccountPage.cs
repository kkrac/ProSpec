using WatiN.Core;
using Krac.Framework.Acceptance.UI.WatiNIntegration;

namespace Sample.Acceptance.UI.Pages
{
    public class ConfirmCreateAccountPage : WatiNPageBase
    {
        public ConfirmCreateAccountPage(Browser browser) : base(browser) { }

        protected override string RelativeUrl
        {
            get { return "Account/Confirm"; }
        }
    }
}
