#if Browser_Tests

using ProSpec.Core.UI.Web;
using Sample.Acceptance.Support.Pages;
using TechTalk.SpecFlow;

namespace Sample.Acceptance.Steps.UI
{
    [Binding]
    public class UnexistingPageSteps : WebStepsContext
    {
        [When(@"I go to an unexisting page")]
        public void When_I_Go_To_An_Unexisting_Page()
        {
            GoTo<UnexistingPage>();
        }
    }
}

#endif