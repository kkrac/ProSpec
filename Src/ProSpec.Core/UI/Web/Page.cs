namespace ProSpec.Core.UI.Web
{
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Collections.Generic;

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

        internal static WebStepsContext Context
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
        /// Determines if the page contains a certain text.
        /// </summary>
        /// <param name="text">Text to find on the page</param>
        /// <returns>true if the page contains the text, otherwise false</returns>
        public bool ContainsText(string text)
        {
            return Context.Browser.Text.Contains(text);
        }

        /// <summary>
        /// Submits an action and forwards the request. If the action fails, it stays on the same page.
        /// </summary>
        /// <typeparam name="TPage">Page to which the request is forwarded when the action finishes its execution successfully</typeparam>
        /// <param name="action">A browser action that triggers a post to the server (eg: button click)</param>
        protected void Submit<TPage>(Action action) where TPage : Page
        {
            Submit<TPage>(action, null, null);
        }

        /// <summary>
        /// Submits an action and forwards the request. If the action fails, it stays on the same page.
        /// </summary>
        /// <typeparam name="TPage">Page to which the request is forwarded when the action finishes its execution successfully</typeparam>
        /// <param name="action">A browser action that triggers a post to the server (eg: button click)</param>
        /// <param name="RESTParameters">REST parameters to attach to the forwarded page URL</param>
        /// <param name="queryString">Querystring parameters to attach to the forwarded page URL</param>
        protected void Submit<TPage>(Action action, string[] RESTParameters, string queryString) where TPage : Page
        {
            action();

            Context.Browser.Status.AssertEquals(HttpStatusCode.OK);

            string parameters = ParametersHelper.ToString(RESTParameters, queryString);

            Context.Forward<TPage>(this, parameters);
        }

        /// <summary>
        /// Submits an action and forwards the request.
        /// </summary>
        /// <typeparam name="TSuccessPage">Page to which the request is forwarded when the action finishes its execution successfully</typeparam>
        /// <typeparam name="TErrorPage">Page to which the request is forwarded when the action finishes its execution with an error</typeparam>
        /// <param name="action">A browser action that triggers a post to the server (eg: button click)</param>
        protected void Submit<TSuccessPage, TErrorPage>(Action action)
            where TSuccessPage : Page
            where TErrorPage : Page
        {
            Submit<TSuccessPage, TErrorPage>(action, null, null);
        }

        /// <summary>
        /// Submits an action and forwards the request.
        /// </summary>
        /// <typeparam name="TSuccessPage">Page to which the request is forwarded when the action finishes its execution successfully</typeparam>
        /// <typeparam name="TErrorPage">Page to which the request is forwarded when the action finishes its execution with an error</typeparam>
        /// <param name="action">A browser action that triggers a post to the server (eg: button click)</param>
        /// <param name="RESTParameters">REST parameters to attach to the forwarded page URL</param>
        /// <param name="queryString">Querystring parameters to attach to the forwarded page URL</param>
        protected void Submit<TSuccessPage, TErrorPage>(Action action, string[] RESTParameters, string queryString)
            where TSuccessPage : Page
            where TErrorPage : Page
        {
            action();

            Context.Browser.Status.AssertEquals(HttpStatusCode.OK);

            string parameters = ParametersHelper.ToString(RESTParameters, queryString);

            Context.Forward<TSuccessPage, TErrorPage>(parameters);
        }
    }
}