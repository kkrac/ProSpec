using System;
using System.Net;
using Should;
using Should.Sdk;
using TwoK.Core.DesignByContract;

namespace ProSpec.Acceptance.UI.Web
{
    /// <summary>
    /// Implementation of the flow manager for a web application.
    /// </summary>
    public class PageFlowManager : IFlowManager<Page>
    {
        private bool isFlowStarted;

        /// <summary>
        /// 
        /// </summary>
        public static PageFlowManager Current
        {
            get
            {
                PageFlowManager flowManager = Context.Get<PageFlowManager>();

                if (flowManager == null)
                {
                    flowManager = new PageFlowManager();

                    Context.Set<PageFlowManager>(flowManager);
                }

                return flowManager;
            }
        }

        private static WebStepsContext Context
        {
            get { return WebStepsContext.Current; }
        }
        
        /// <summary>
        /// Loads page but does not navigate to it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Load<T>() where T : Page
        {
            return Load<T>(false);
        }

        /// <summary>
        /// Loads page and navigates to it.
        /// </summary>
        /// <typeparam name="T">Type of page to return</typeparam>
        /// <returns>Page</returns>
        public T LoadAndGo<T>() where T : Page
        {
            return LoadAndGo<T>(HttpStatusCode.OK);
        }

        /// <summary>
        /// Loads page and navigates to it.
        /// </summary>
        /// <typeparam name="T">Type of page to return</typeparam>
        /// <param name="expectedStatusCode">Expected HttpStatusCode once the request completes. By default, the expected status is HttpStatusCode.OK</param>
        /// <returns>Page</returns>
        public T LoadAndGo<T>(HttpStatusCode expectedStatusCode) where T : Page
        {
            T page = Load<T>(true);

            ValidateBrowserStatus(expectedStatusCode);

            return page;
        }

        private T Load<T>(bool navigateToPage) where T : Page
        {
            return (T)Load(typeof(T), navigateToPage, true, string.Empty);
        }

        internal Page Load(Type pageType, bool navigateToPage, bool startingFlow, string additionalParameters)
        {
            Check.Require(!startingFlow || !isFlowStarted, "Flow has already been started!");
             
            Page page = LoadWithoutNavigating(pageType) as Page;

            return Load(page, navigateToPage, startingFlow, additionalParameters);
        }

        internal Page Load(Page page, bool navigateToPage, bool startingFlow, string additionalParameters)
        {
            Check.Require(!startingFlow || !isFlowStarted, "Flow has already been started!");

            page.RawUrl += additionalParameters;

            if (startingFlow)
            {
                isFlowStarted = true;
            }

            if (navigateToPage)
            {
                Context.Browser.GoTo(page.RawUrl);
            }

            Context.Driver = page;

            return page;
        }

        private void ValidateBrowserStatus(HttpStatusCode expectedStatusCode)
        {
            try
            {
                Context.Browser.Status.ShouldEqual(expectedStatusCode);
            }
            catch (EqualException ex)
            {
                string browserOutput = string.Empty;

                if (!string.IsNullOrEmpty(Context.Browser.Text))
                {
                    browserOutput = Environment.NewLine + Environment.NewLine;
                    browserOutput += Context.Browser.Text;
                }

                Exception exToThrow = new AssertException(ex.Message + browserOutput);

                throw exToThrow;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Page LoadWithoutNavigating(Type pageType)
        {
            Page page = Activator.CreateInstance(pageType) as Page;

            return page;
        }

        /// <summary>
        /// Forwards the request.
        /// </summary>
        /// <typeparam name="TPage">Page to forward to if the action completes successfully</typeparam>
        /// <param name="source">Source page when the action was executed. Default to forward if action completes with an error.</typeparam>
        /// <param name="parameters"></param>
        public void Forward<TPage>(Page source, string parameters) where TPage : Page
        {
            Page successPage = LoadWithoutNavigating(typeof(TPage));

            successPage.RawUrl += parameters;

            Forward(successPage, source);
        }

        /// <summary>
        /// Forwards the request.
        /// </summary>
        /// <typeparam name="TSuccessPage">Page to forward to if the action completes successfully</typeparam>
        /// <typeparam name="TErrorPage">Page to forward to if the action completes with an error. By default, it forwards to the same page</typeparam>
        /// <param name="parameters"></param>
        public void Forward<TSuccessPage, TErrorPage>(string parameters) where TSuccessPage : Page where TErrorPage : Page
        {
            TSuccessPage successPage = (TSuccessPage)LoadWithoutNavigating(typeof(TSuccessPage));
            TErrorPage errorPage = (TErrorPage)LoadWithoutNavigating(typeof(TErrorPage));

            successPage.RawUrl += parameters;

            Forward(successPage, errorPage);
        }

        private void Forward(Page successPage, Page errorPage)
        {
            if (Context.Browser.IsOnPage(errorPage))
            {
                Context.Driver = errorPage;
            }
            else
            {
                Context.Driver = successPage;
            }
        }

        internal void Forward(Type successPageType, Type errorPageType, string parameters)
        {
            Page successPage = LoadWithoutNavigating(successPageType);
            Page errorPage = LoadWithoutNavigating(errorPageType);

            successPage.RawUrl += parameters;

            Forward(successPage, errorPage);
        }
    }
}
