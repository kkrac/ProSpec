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

        /// <summary>
        /// Returns the driver of a specific type.
        /// </summary>
        /// <typeparam name="T">Type of the driver</typeparam>
        /// <returns>Reference to the driver</returns>
        T OfType<T>() where T : ITestDriver;
    }
}