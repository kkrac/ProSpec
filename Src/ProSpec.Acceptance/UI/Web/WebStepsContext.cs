﻿using ProSpec.Hosting;
using TechTalk.SpecFlow;
using TwoK.Core.DesignByContract;

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
    }
}
