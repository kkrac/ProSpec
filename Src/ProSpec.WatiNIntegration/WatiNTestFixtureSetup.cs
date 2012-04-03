namespace ProSpec.WatiNIntegration
{
    using ProSpec.Core;
    using ProSpec.Core.UI.Web;
    using WatiN.Core;
    using WatiN.Core.Interfaces;

    /// <summary>
    /// Fixture to initialize and tear down components used in a set of tests of a web application using WatiN browser.
    /// </summary>
    public class WatiNTestFixtureSetup : WebTestFixtureSetup
    {
        /// <summary>
        /// Initializes the test fixture.
        /// </summary>
        /// <param name="browserSettings">Settings of the browser</param>
        /// <param name="browserLifeSpan">Life span of the browser</param>
        public WatiNTestFixtureSetup(ISettings browserSettings, ObjectLifeSpan browserLifeSpan) : base(browserLifeSpan)
        {
            Settings.Instance = browserSettings;
        }

        /// <summary>
        /// Initializes the test fixture.
        /// </summary>
        /// <param name="browserSettings">Settings of the browser</param>
        public WatiNTestFixtureSetup(ISettings browserSettings) : this(browserSettings, ObjectLifeSpan.Scenario) {}
    }
}
