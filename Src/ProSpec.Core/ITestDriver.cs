namespace ProSpec.Core
{
    /// <summary>
    /// Interface that all test drivers should implement.
    /// </summary>
    public interface ITestDriver
    {
        /// <summary>
        /// Identifier of the test driver.
        /// </summary>
        string Uri { get; }
    }
}