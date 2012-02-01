using ProSpec.Hosting;
using TwoK.Core.IoC;

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
        public WebTestFixtureSetup(ObjectLifeSpan browserScope)
        {
            Context.BrowserScope = browserScope;
        }

        /// <summary>
        /// Initializes the test fixture.
        /// </summary>
        public WebTestFixtureSetup() : this(ObjectLifeSpan.Scenario) { }

        private WebStepsContext Context
        {
            get { return WebStepsContext.Current; }
        }

        private void InitializeServer()
        {
            IServer server = IoCProvider.Resolve<IServer>();

            server.Start();

            Context.Server = server;
        }

        private void DisposeServer()
        {
            IServer server = Context.Server;

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

            Context.Browser = browser;
        }

        /// <summary>
        /// Disposes the browser.
        /// </summary>
        protected void DisposeBrowser()
        {
            IBrowser browser = Context.Browser;

            IoCProvider.Release(browser);

            browser.Close();
        }

        /// <summary>
        /// Initializes components used throughout the execution of the tests.
        /// </summary>
        public virtual void SetupTests()
        {
            InitializeServer();

            if (Context.BrowserScope == ObjectLifeSpan.Global)
            {
                InitializeBrowser();
            }
        }

        /// <summary>
        /// Finalizes the components used throughout the execution of the tests. 
        /// </summary>
        public virtual void TearDownTests()
        {
            if (Context.BrowserScope == ObjectLifeSpan.Global)
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
            if (Context.BrowserScope == ObjectLifeSpan.Feature)
            {
                InitializeBrowser();
            }
        }

        /// <summary>
        /// Finalizes the components used throughout the execution of a feature.
        /// </summary>
        public virtual void TearDownFeature()
        {
            if (Context.BrowserScope == ObjectLifeSpan.Feature)
            {
                DisposeBrowser();
            }
        }

        /// <summary>
        /// Initializes components used throughout the execution of a scenario.
        /// </summary>
        public virtual void SetupScenario()
        {
            if (Context.BrowserScope == ObjectLifeSpan.Scenario)
            {
                InitializeBrowser();
            }
        }

        /// <summary>
        /// Finalizes the components used throughout the execution of a scenario.
        /// </summary>
        public virtual void TearDownScenario()
        {
            if (Context.BrowserScope == ObjectLifeSpan.Scenario)
            {
                DisposeBrowser();
            }
        }
    }
}