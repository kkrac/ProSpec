using Krac.Framework.Hosting;
using Should;
using WatiN.Core;
using Krac.Framework.Acceptance.SpecFlow;
using Krac.Framework.Acceptance.SpecFlow.UI.WatiN;

namespace Sample.Acceptance.Steps.UI.Pages
{
    public abstract class PageBase
    {
        protected PageBase(Browser browser)
        {
            browser.Url.ShouldEqual(Server.RootURL + RelativeURL);

            this.Browser = browser;
        }

        protected IServer Server
        {
            get { return TestsContext.Current.Get<IServer>(); }
        }

        protected Browser Browser
        {
            get; private set;
        }

        public abstract string RelativeURL
        {
            get;
        }

        public T OfType<T>() where T : PageBase
        {
            return (T)this;
        }
    }
}
