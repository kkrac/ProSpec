using TwoK.Core.IoC;
using ProSpec.Hosting;
using TwoK.Core.DesignByContract;
using TechTalk.SpecFlow;

namespace ProSpec.Acceptance.UI.Web
{
    /// <summary>
    /// Fixture to initialize and tear down components used in a set of tests of a web application.
    /// </summary>
    public class WebTestFixtureSetup : ITestFixtureSetup
    {
        /// <summary>
        /// Initializes the test fixture.
        /// </summary>
        /// <param name="browserScope">Type of scope of the browser</param>
        public WebTestFixtureSetup(BrowserScope browserScope)
        {
            Check.Require(browserScope == BrowserScope.Global ||
                          browserScope == BrowserScope.Feature ||
                          browserScope == BrowserScope.Scenario,
              string.Format("There is no context available to support BrowserScope '{0}'", browserScope));

            BrowserScope = browserScope;
        }

        /// <summary>
        /// Initializes the test fixture.
        /// </summary>
        public WebTestFixtureSetup() : this(BrowserScope.Scenario) { }

        private static BrowserScope BrowserScope;

        private void InitializeServer()
        {
            IServer server = IoCProvider.Resolve<IServer>();

            server.Start();

            WebStepsContext.Current.Set<IServer>(server, ObjectLifeSpan.Global);
        }

        private void DisposeServer()
        {
            IServer server = WebStepsContext.Current.Get<IServer>(ObjectLifeSpan.Global);

            IoCProvider.Release(server);

            server.Stop();
        }

        /// <summary>
        /// Initializes the browser.
        /// </summary>
        /// <returns></returns>
        protected void InitializeBrowser()
        {
            IBrowser browser = IoCProvider.Resolve<IBrowser>();

            WebStepsContext.Current.Browser = browser;
        }

        /// <summary>
        /// Disposes the browser.
        /// </summary>
        protected void DisposeBrowser()
        {
            IBrowser browser = WebStepsContext.Current.Browser;

            IoCProvider.Release(browser);

            browser.Close();
        }

        /// <summary>
        /// Initializes components used throughout the execution of the tests.
        /// </summary>
        public virtual void SetupTests()
        {
            InitializeServer();

            if (BrowserScope == BrowserScope.Global)
            {
                InitializeBrowser();
            }
        }

        /// <summary>
        /// Finalizes the components used throughout the execution of the tests. 
        /// </summary>
        public virtual void TearDownTests()
        {
            if (BrowserScope == BrowserScope.Global)
            {
                DisposeBrowser();
            }

            DisposeServer();

            IoCProvider.Reset();
        }

        /// <summary>
        /// Initializes components used throughout the execution of a feature.
        /// </summary>
        public virtual void SetupFeature()
        {
            if (BrowserScope == BrowserScope.Feature)
            {
                InitializeBrowser();
            }
        }

        /// <summary>
        /// Finalizes the components used throughout the execution of a feature.
        /// </summary>
        public virtual void TearDownFeature()
        {
            if (BrowserScope == BrowserScope.Feature)
            {
                DisposeBrowser();
            }
        }

        /// <summary>
        /// Initializes components used throughout the execution of a scenario.
        /// </summary>
        public virtual void SetupScenario()
        {
            if (BrowserScope == BrowserScope.Scenario)
            {
                InitializeBrowser();
            }
        }

        /// <summary>
        /// Finalizes the components used throughout the execution of a scenario.
        /// </summary>
        public virtual void TearDownScenario()
        {
            if (BrowserScope == BrowserScope.Scenario)
            {
                DisposeBrowser();
            }
        }
    }
}