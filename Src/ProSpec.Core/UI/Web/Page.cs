using System;

namespace ProSpec.Core.UI.Web
{
    /// <summary>
    /// Page object base implementation.
    /// </summary>
    public abstract class Page : ITestDriver
    {
        /// <summary>
        /// 
        /// </summary>
        protected Page()
        {
            Uri = Context.Server.RootUrl + RelativeUrl;

            RawUrl = Uri;
        }

        internal static Page Current
        {
            get { return Context.Get<Page>(); }
            set { Context.Set<Page>(value); }
        }

        private static WebStepsContext Context
        {
            get { return WebStepsContext.Current; }
        }

        /// <summary>
        /// Relative Url of the page.
        /// </summary>
        protected abstract string RelativeUrl { get; }

        /// <summary>
        /// Complete Url including the parameters.
        /// </summary>
        public string RawUrl { get; internal set; }

        /// <summary>
        /// Url of the page without the parameters.
        /// </summary>
        public string Uri { get; private set; }

        /// <summary>
        /// Returns the page that is of a specific type.
        /// </summary>
        /// <typeparam name="T">Type of the page.</typeparam>
        /// <returns>Reference to the page</returns>
        public T OfType<T>() where T : ITestDriver
        {
            return (T)(this as object);
        }

        /// <summary>
        /// Used to validate the fields in a page.
        /// </summary>
        protected virtual void Validate()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RESTfulParameters"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        protected string ParametersToString(string[] RESTfulParameters, string queryString)
        {
            string parametersAsString = string.Empty;

            if (RESTfulParameters != null && RESTfulParameters.Length > 0)
            {
                parametersAsString = string.Join("/", RESTfulParameters);
            }

            if (queryString != null)
            {
                parametersAsString += queryString;
            }

            if (!string.IsNullOrEmpty(parametersAsString))
            {
                parametersAsString = string.Concat("/", parametersAsString);
            }

            return parametersAsString;
        }

        private PageFlowManager FlowManager
        {
            get { return PageFlowManager.Current; }
        }

        /// <summary>
        /// Determines if the page contains a certain text.
        /// </summary>
        /// <param name="text">Text to find on the page</param>
        /// <returns>true if the page contains the text, otherwise false</returns>
        public bool ContainsText(string text)
        {
            return Context.Browser.Text.Contains(text);
        }

        /// <summary>
        /// Navigates to the page.
        /// </summary>
        protected internal void Go()
        {
            Go(new string[] {}, null);
        }

        /// <summary>
        /// Navigates to the page.
        /// </summary>
        /// <param name="RESTfulParameters">Array of parameters of a RESTful call</param>
        protected internal void Go(params string[] RESTfulParameters)
        {
            Go(RESTfulParameters, null);
        }

        /// <summary>
        /// Navigates to the page.
        /// </summary>
        /// <param name="RESTfulParameters">Array of parameters of a RESTful call</param>
        /// <param name="queryString">Query string of the call</param>
        protected internal void Go(string[] RESTfulParameters, string queryString)
        {
            string parameters = ParametersToString(RESTfulParameters, queryString);

            FlowManager.Load(this, true, false, parameters);

            Validate();
        }

        /// <summary>
        /// Submits an action.
        /// </summary>
        /// <typeparam name="TPage">Page to which the request is forwarded when the action finishes its execution successfully</typeparam>
        /// <param name="action">A browser action that triggers a post to the server (eg: button click)</param>
        protected void Submit<TPage>(Action action) where TPage : Page
        {
            Submit<TPage>(action, null, null);
        }

        /// <summary>
        /// Submits an action.
        /// </summary>
        /// <typeparam name="TPage">Page to which the request is forwarded when the action finishes its execution successfully</typeparam>
        /// <param name="action">A browser action that triggers a post to the server (eg: button click)</param>
        /// <param name="RESTfulParameters">REST parameters to attach to the forwarded page URL. If no parameters to be sent, just leave this as null</param>
        /// <param name="queryString">Querystring parameters to attach to the forwarded page URL. If no parameters to be sent, just leave this as null</param>
        protected void Submit<TPage>(Action action, string[] RESTfulParameters, string queryString) where TPage : Page
        {
            action();

            string parameters = ParametersToString(RESTfulParameters, queryString);

            FlowManager.Forward<TPage>(this, parameters);
        }

        /// <summary>
        /// Submits an action.
        /// </summary>
        /// <typeparam name="TSuccessPage">Page to which the request is forwarded when the action finishes its execution successfully</typeparam>
        /// <typeparam name="TErrorPage">Page to which the request is forwarded when the action finishes its execution with an error. By default, it forwards to the current page</typeparam>
        /// <param name="action">A browser action that triggers a post to the server (eg: button click)</param>
        protected void Submit<TSuccessPage, TErrorPage>(Action action) where TSuccessPage : Page where TErrorPage : Page
        {
            Submit<TSuccessPage, TErrorPage>(action, null, null);
        }

        /// <summary>
        /// Submits an action.
        /// </summary>
        /// <typeparam name="TSuccessPage">Page to which the request is forwarded when the action finishes its execution successfully</typeparam>
        /// <typeparam name="TErrorPage">Page to which the request is forwarded when the action finishes its execution with an error. By default, it forwards to the current page</typeparam>
        /// <param name="action">A browser action that triggers a post to the server (eg: button click)</param>
        /// <param name="RESTfulParameters">REST parameters to attach to the forwarded page URL</param>
        /// <param name="queryString">Querystring parameters to attach to the forwarded page URL</param>
        protected void Submit<TSuccessPage, TErrorPage>(Action action, string[] RESTfulParameters, string queryString)
            where TSuccessPage : Page
            where TErrorPage : Page
        {
            action();

            string parameters = ParametersToString(RESTfulParameters, queryString);

            FlowManager.Forward<TSuccessPage, TErrorPage>(parameters);
        }
    }
}
