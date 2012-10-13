namespace ProSpec.Core.UI.Web
{
    using System;
    using System.Net;
    using ProSpec.Core.Hosting;
    using Should;
    using Should.Sdk;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Steps context implementation for a web scenario.
    /// </summary>
    public class WebStepsContext : StepsContextBase<Page>
    {
        /// <summary>
        /// Singleton instance of the context.
        /// </summary>
        public static WebStepsContext Current
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
            get { return Page.Current; }
            internal set { Page.Current = value; }
        }

        /// <summary>
        /// Verifies the http status.
        /// </summary>
        /// <param name="statusCode">Http status of the last navigation</param>
        protected void AssertHttpStatusIs(int statusCode)
        {
            try
            {
                HttpStatusCode expectedStatusCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), statusCode.ToString());

                Current.Browser.Status.ShouldEqual(expectedStatusCode);
            }
            catch (EqualException ex)
            {
                string browserOutput = string.Empty;

                if (!string.IsNullOrEmpty(Current.Browser.Text))
                {
                    browserOutput = Environment.NewLine + Environment.NewLine;
                    browserOutput += Current.Browser.Text;
                }

                Exception exToThrow = new AssertException(ex.Message + browserOutput);

                throw exToThrow;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Navigates to a page.
        /// </summary>
        /// <typeparam name="T">Type of the page t navigate to</typeparam>
        /// <returns>Page object</returns>
        protected T GoTo<T>() where T : Page
        {
            Page page = CreatePage(typeof(T));

            return (T)GoTo(page, string.Empty);
        }

        internal Page GoTo(Page page, string additionalParameters)
        {
            page.RawUrl += additionalParameters;
        
            Current.Browser.GoTo(page.RawUrl);

            Current.Driver = page;

            return page;
        }

        private Page CreatePage(Type pageType)
        {
            Page page = Activator.CreateInstance(pageType) as Page;

            return page;
        }

        /// <summary>
        /// After an action finishes executing, it forwards the request to the same or to another page if the action finishes successfully. If the action fails, by default it forwards to the same page.
        /// </summary>
        /// <typeparam name="TPage">Page to forward to if the action completes successfully</typeparam>
        /// <param name="source">Default page to forward to if action completes with an error.</param>
        /// <param name="parameters">Parameters of the request</param>
        internal void Forward<TPage>(Page source, string parameters) where TPage : Page
        {
            Forward(typeof(TPage), source, parameters);
        }

        /// <summary>
        /// After an action finishes executing, it forwards the request to the same or to another page if the action finishes successfully.
        /// </summary>
        /// <typeparam name="TSuccessPage">Page to forward to if the action completes successfully</typeparam>
        /// <typeparam name="TErrorPage">Page to forward to if the action fails</typeparam>
        /// <param name="parameters">Parameters of the request</param>
        internal void Forward<TSuccessPage, TErrorPage>(string parameters)
            where TSuccessPage : Page
            where TErrorPage : Page
        {
            Forward(typeof(TSuccessPage), typeof(TErrorPage), parameters);
        }

        private void Forward(Type successPageType, Type errorPageType, string parameters)
        {
            Page errorPage = CreatePage(errorPageType);

            Forward(successPageType, errorPage, parameters);
        }

        private void Forward(Type successPageType, Page errorPage, string parameters)
        {
            Page successPage = CreatePage(successPageType);

            successPage.RawUrl += parameters;

            if (Current.Browser.IsOnPage(successPage))
            {
                Current.Driver = successPage;
            }
            else
            {
                Current.Driver = errorPage;
            }
        }
    }
}
