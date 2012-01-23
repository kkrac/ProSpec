using System.Web.SessionState;
using ProSpec.Acceptance;
using TwoK.Core.IoC;
using MvcContrib.TestHelper.Fakes;
using TechTalk.SpecFlow;

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

            HttpSessionFactory.RegisterHttpSession(() => new FakeHttpSessionState(sessionItems));

            fixture = IoCProvider.Resolve<ITestFixtureSetup>();

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
