using System.Net;
using ProSpec.Acceptance.UI.Web;
using SHDocVw;
using WatiN.Core;
using Page = ProSpec.Acceptance.UI.Web.Page;
using WatiN.Core.Interfaces;

namespace ProSpec.Acceptance.UI.WatiNIntegration
{
    /// <summary>
    /// Interface to the Internet Explorer WatiN browser object to access elements on a page.
    /// </summary>
    public class WatiNBrowser : IE, IBrowser
    {
        public WatiNBrowser()
        {
            observer = new NavigationObserver(this);
        }

        public static ISettings Settings
        {
            get { return WatiN.Core.Settings.Instance; }
            set { WatiN.Core.Settings.Instance = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public bool IsOnPage(Page page)
        {
            return Url.Equals(page.RawUrl);
        }

        /// <summary>
        /// 
        /// </summary>
        public HttpStatusCode Status
        {
            get; private set;
        }

        private NavigationObserver observer;

        private class NavigationObserver
        {
            private WatiNBrowser browser;
            private bool errorOccured;

            public NavigationObserver(WatiNBrowser browser)
            {
                this.browser = browser;
                InternetExplorer internetExplorer = (InternetExplorer)browser.InternetExplorer;
                internetExplorer.NavigateError += IeNavigateError;
                internetExplorer.NavigateComplete2 += internetExplorer_NavigateComplete2;
            }

            private void internetExplorer_NavigateComplete2(object pDisp, ref object URL)
            {
                if (!errorOccured)
                {
                    browser.Status = HttpStatusCode.OK;
                }

                errorOccured = false;
            }

            private void IeNavigateError(object pDisp, ref object URL, ref object Frame, ref object StatusCode, ref bool Cancel)
            {
                errorOccured = true;

                browser.Status = (HttpStatusCode)StatusCode;
            }
        }
    }
}
