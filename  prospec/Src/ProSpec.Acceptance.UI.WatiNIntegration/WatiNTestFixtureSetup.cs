using ProSpec.Acceptance.UI.Web;
using WatiN.Core.Interfaces;

namespace ProSpec.Acceptance.UI.WatiNIntegration
{
    /// <summary>
    /// Fixture to initialize and tear down components used in a set of tests of a web application using WatiN browser.
    /// </summary>
    public class WatiNTestFixtureSetup : WebTestFixtureSetup
    {
        /// <summary>
        /// Initializes the test fixture.
        /// </summary>
        /// <param name="browserSettings">Settingd of the browser</param>
        /// <param name="browserScope">Type of scope of the browser</param>
        public WatiNTestFixtureSetup(ISettings browserSettings, BrowserScope browserScope) : base(browserScope)
        {
            WatiN.Core.Settings.Instance = browserSettings;
        }

        /// <summary>
        /// Initializes the test fixture.
        /// </summary>
        /// <param name="browserSettings">Settings of the browser</param>
        public WatiNTestFixtureSetup(ISettings browserSettings)
        {
            WatiN.Core.Settings.Instance = browserSettings;
        }
    }
}
