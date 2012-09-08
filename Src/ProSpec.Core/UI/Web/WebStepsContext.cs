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

        /// <summary>
        /// Reference to the flow manager.
        /// </summary>
        private PageFlowManager FlowManager
        {
            get { return PageFlowManager.Current; }
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

        protected T GoTo<T>() where T : Page
        {
            return FlowManager.LoadAndGo<T>();
        }

        protected T Load<T>() where T : Page
        {
            return FlowManager.Load<T>();
        }
    }
}
