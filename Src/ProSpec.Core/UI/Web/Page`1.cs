namespace ProSpec.Core.UI.Web
{
    public abstract class Page<TBrowser> : Page where TBrowser : IBrowser
    {
        protected TBrowser Browser
        {
            get { return (TBrowser)WebStepsContext.Current.Browser; }
        }
    }
}