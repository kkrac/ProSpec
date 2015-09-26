namespace ProSpec.WatiNIntegration
{
    using System.Diagnostics;
    using System.Net;
    using ProSpec.Core.UI.Web;
    using SHDocVw;
    using WatiN.Core;
    using Page = ProSpec.Core.UI.Web.Page;

    /// <summary>
    /// Interface to the Internet Explorer WatiN browser object to access elements on a page.
    /// </summary>
    public class WatiNIEBrowser : IE, IBrowser
    {
        public WatiNIEBrowser()
        {
            observer = new NavigationObserver(this);
        }

        public override void Close()
        {
            base.Close();

            Process[] processes = Process.GetProcessesByName("iexplore");

            foreach (Process process in processes)
            {
                process.Kill();
            }
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
            private WatiNIEBrowser browser;
            private bool errorOccured;

            public NavigationObserver(WatiNIEBrowser browser)
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
