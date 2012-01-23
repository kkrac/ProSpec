namespace ProSpec.Acceptance.UI.Web
{
    /// <summary>
    /// Determines the life span of the browser.
    /// </summary>
    public enum BrowserScope
    {
        /// <summary>
        /// The browser remains active throughout the execution of the current scenario.
        /// </summary>
        Scenario,
        /// <summary>
        /// The browser remains active throughout the execution of the current feature.
        /// </summary>
        Feature,
        /// <summary>
        /// The same instance of the browser is used for all features and scenarios executed.
        /// </summary>
        Global
    }
}
