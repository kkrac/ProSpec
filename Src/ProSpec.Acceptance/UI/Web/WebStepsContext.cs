using TechTalk.SpecFlow;
using ProSpec.Hosting;

namespace ProSpec.Acceptance.UI.Web
{
    /// <summary>
    /// Steps context implementation for a web scenario.
    /// </summary>
    public class WebStepsContext : StepsContextBase<Page>
    {
        private const string ID = "$__Context";

        /// <summary>
        /// Singleton instance of the context.
        /// </summary>
        public static WebStepsContext Current
        {
            get
            {
                SpecFlowContext context = GetContext(ObjectLifeSpan.Global);

                if (!context.ContainsKey(ID))
                {
                    context.Set<WebStepsContext>(new WebStepsContext(), ID);
                }

                return context[ID] as WebStepsContext;
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
