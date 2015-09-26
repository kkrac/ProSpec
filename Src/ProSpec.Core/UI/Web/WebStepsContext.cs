namespace ProSpec.Core.UI.Web
{
    using System;
    using System.Net;
    using ProSpec.Core.Hosting;
    using Should;
    using Should.Core.Exceptions;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Steps context implementation for a web scenario.
    /// </summary>
    public class WebStepsContext : StepsContextBase<Page>
    {
        /// <summary>
        /// Singleton instance of the context.
        /// </summary>
        protected internal static WebStepsContext Current
        {
            get
            {
                SpecFlowContext globalContext = GetContext(ObjectLifeSpan.Global);

                WebStepsContext stepsContext;

                if (!globalContext.TryGetValue<WebStepsContext>(out stepsContext))
                {
                    stepsContext = new WebStepsContext();

                    globalContext.Set<WebStepsContext>(stepsContext);
                }
                
                return stepsContext;
            }
        }

        internal IServer Server
        {
            get { return Get<IServer>(ObjectLifeSpan.Global); }
            set { Set<IServer>(value, ObjectLifeSpan.Global); }
        }

        internal ObjectLifeSpan BrowserScope
        {
            get; set;
        }

        internal IBrowser Browser
        {
            get
            {
                switch (BrowserScope)
                {
                    case ObjectLifeSpan.Global: return Get<IBrowser>(ObjectLifeSpan.Global);
                    case ObjectLifeSpan.Feature: return Get<IBrowser>(ObjectLifeSpan.Feature);
                    case ObjectLifeSpan.Scenario: return Get<IBrowser>();
                    default: return null;
                }
            }
            set
            {
                switch (BrowserScope)
                {
                    case ObjectLifeSpan.Global: Set<IBrowser>(value, ObjectLifeSpan.Global); break;
                    case ObjectLifeSpan.Feature: Set<IBrowser>(value, ObjectLifeSpan.Feature); break;
                    case ObjectLifeSpan.Scenario: Set<IBrowser>(value); break;
                }
            }
        }

        /// <summary>
        /// Reference to the page that is currently active.
        /// </summary>
        public override Page Driver
        {
            get { return Current.Get<Page>(); }
            internal set { Current.Set<Page>(value); }
        }
        
        /// <summary>
        /// Navigates to a page.
        /// </summary>
        /// <typeparam name="T">Type of the page to navigate to</typeparam>
        /// <returns>Page object</returns>
        protected override T GoTo<T>()
        {
            return (T)GoTo<T>(HttpStatusCode.OK);
        }

        /// <summary>
        /// Navigates to a page.
        /// </summary>
        /// <typeparam name="T">Type of the page to navigate to</typeparam>
        /// <returns>Page object</returns>
        protected T GoTo<T>(HttpStatusCode status) where T : Page
        {
            return (T)GoTo<T>(null, null, status);
        }

        /// <summary>
        /// Navigates to a page.
        /// </summary>
        /// <param name="RESTParameters"></param>
        protected T GoTo<T>(string[] RESTParameters) where T : Page
        {
            return (T)GoTo<T>(RESTParameters, HttpStatusCode.OK);
        }

        protected T GoTo<T>(string querystring) where T : Page
        {
            return (T)GoTo<T>(null, querystring);
        }

        protected T GoTo<T>(string[] RESTParameters, string querystring) where T : Page
        {
            return (T)GoTo<T>(RESTParameters, querystring, HttpStatusCode.OK);
        }

        protected T GoTo<T>(string[] RESTParameters, HttpStatusCode status) where T : Page
        {
            return (T)GoTo<T>(RESTParameters, null, status);
        }

        protected T GoTo<T>(string querystring, HttpStatusCode status) where T : Page
        {
            return (T)GoTo<T>(null, querystring, status);
        }

        protected T GoTo<T>(string[] RESTParameters, string querystring, HttpStatusCode status) where T : Page
        {
            Page page = CreatePage(typeof(T));

            string additionalParameters = ParametersHelper.ToString(RESTParameters, querystring);

            return (T)GoTo(page, additionalParameters, status);
        }

        internal Page GoTo(Page page, string additionalParameters)
        {
            return GoTo(page, additionalParameters, HttpStatusCode.OK);
        }

        internal Page GoTo(Page page, string additionalParameters, HttpStatusCode status)
        {
            page.RawUrl += additionalParameters;
        
            Current.Browser.GoTo(page.RawUrl);

            Browser.Status.AssertEquals(status, page.RawUrl);

            Current.Driver = page;

            return page;
        }

        private Page CreatePage(Type pageType)
        {
            Page page = Activator.CreateInstance(pageType) as Page;

            return page;
        }

        /// <summary>
        /// It forwards the request after an action on the page has finished executing.
        /// </summary>
        /// <typeparam name="TSuccessPage">Page to forward to if the action finishes successfully</typeparam>
        /// <typeparam name="TErrorPage">Page to forward to if the action fails</typeparam>
        /// <param name="parameters">Optional parameters to pass to the page the request is forwarded to</param>
        internal void Forward<TSuccessPage, TErrorPage>(string parameters)
            where TSuccessPage : Page
            where TErrorPage : Page
        {
            Page errorPage = CreatePage(typeof(TErrorPage));

            Forward<TSuccessPage>(errorPage, parameters);
        }

        /// <summary>
        /// It forwards the request after an action on the page has finished executing.
        /// </summary>
        /// <typeparam name="TPage">Page to forward to if the action finishes successfully</typeparam>
        /// <param name="errorPage">Page to forward to if the action fails</param>
        /// <param name="parameters">Optional parameters to pass to the page the request is forwarded to</param>
        internal void Forward<TPage>(Page errorPage, string parameters) where TPage : Page
        {
            Page successPage = CreatePage(typeof(TPage));

            successPage.RawUrl += parameters;

            SetCurrentPage(successPage, errorPage);
        }

        protected void ShouldBeAt<T>() where T : Page
        {
            try
            {
                Driver.ShouldBeType<T>();
            }
            catch (AssertException)
            {
                Page expectedPage = CreatePage(typeof(T));

                throw new ExpectedPageMismatchException(Driver, expectedPage, Browser.Url, expectedPage.RawUrl);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SetCurrentPage(Page successPage, Page errorPage)
        {
            if (Current.Browser.IsOnPage(successPage))
            {
                Current.Driver = successPage;
            }
            else
            {
                if (Browser.Status == HttpStatusCode.NotFound)
                {
                    Current.Driver = new PageNotFound();
                }
                else
                {
                    Current.Driver = errorPage;
                }
            }
        }
    }
}
