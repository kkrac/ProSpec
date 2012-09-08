using System.Web.SessionState;
using MvcContrib.TestHelper.Fakes;
using ProSpec.Core;
using TechTalk.SpecFlow;
using TwoK.IoC;
using WatiN.Core;
using WatiN.Core.Interfaces;

namespace Sample.Acceptance.Features
{
    [Binding]
    public class TestFixture
    {
        private static ITestFixtureSetup fixture;

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            IoCProvider.Initialize();

            SessionStateItemCollection sessionItems = new SessionStateItemCollection();

            IoCProvider.RegisterHttpSession(() => new FakeHttpSessionState(sessionItems));

            fixture = IoCProvider.Resolve<ITestFixtureSetup>();

            Settings.Instance = IoCProvider.Resolve<ISettings>();

            fixture.SetupTests();
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            fixture.SetupFeature();
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            fixture.SetupScenario();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            fixture.TearDownScenario();
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            fixture.TearDownFeature();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            fixture.TearDownTests();
        }
    }
}
