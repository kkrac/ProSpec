namespace ProSpec.Core
{
    /// <summary>
    /// Initializes and tears down the contexts.
    /// </summary>
    public interface ITestFixtureSetup
    {
        void SetupTests();
        void TearDownTests();
        void SetupFeature();
        void TearDownFeature();
        void SetupScenario();
        void TearDownScenario();
    }
}
