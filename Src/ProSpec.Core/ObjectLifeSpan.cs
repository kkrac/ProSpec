namespace ProSpec.Core
{
    /// <summary>
    /// Life span of an object in the context.
    /// </summary>
    public enum ObjectLifeSpan
    {
        /// <summary>
        /// Reference to the life span of an object that lasts in the context throughout the execution of all the tests.
        /// </summary>
        Global,
        /// <summary>
        /// Reference to the life span of an object that lasts in the context throughout the execution of the current feature.
        /// </summary>
        Feature,
        /// <summary>
        /// Reference to the life span of an object that lasts in the context throughout the execution of the current scenario.
        /// </summary>
        Scenario
    }
}
