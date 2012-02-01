using TechTalk.SpecFlow;
using ProSpec.Hosting;

namespace ProSpec.Acceptance.UI.Web
{
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
                SpecFlowContext context = GetContext(ObjectLifeSpan.Global);

                WebStepsContext webContext;

                if (!context.TryGetValue<WebStepsContext>(out webContext))
                {
                    webContext = new WebStepsContext();

                    context.Set<WebStepsContext>(webContext);
                }

                return webContext;
            }
        }

        /// <summary>
        /// Reference to the flow manager.
        /// </summary>
        protected PageFlowManager FlowManager
        {
            get { return PageFlowManager.Current; }
        }

        internal IServer Server
        {
            get { return Get<IServer>(ObjectLifeSpan.Global); }
            set { Set<IServer>(value, ObjectLifeSpan.Global); }
        }

        internal IBrowser Browser
        {
            get; set;
        }

        /// <summary>
        /// Reference to the page that is currently active.
        /// </summary>
        public override Page Driver
        {
            get { return Page.Current; }
            internal set { Page.Current = value; }
        }
    }
}
