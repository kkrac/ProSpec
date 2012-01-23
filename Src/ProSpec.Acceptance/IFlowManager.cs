namespace ProSpec.Acceptance
{
    /// <summary>
    /// Manages the flow of a scenario.
    /// </summary>
    /// <typeparam name="TestDriver"></typeparam>
    public interface IFlowManager<TestDriver> where TestDriver : ITestDriver
    {
        T Load<T>() where T : TestDriver;

        T LoadAndGo<T>() where T : TestDriver;

        void Forward<T>(TestDriver source, string parameters) where T : TestDriver;

        void Forward<T1, T2>(string parameters) where T1 : TestDriver where T2 : TestDriver;
    }
}