using System;

namespace ProSpec.Core.UI.Web
{
    public class ExpectedPageMismatchException : Exception
    {
        public ExpectedPageMismatchException(Page currentPage, Page expectedPage, string currentUrl, string expectedUrl)
        {
            if (currentUrl.Equals(expectedUrl))
            {
                this.message = string.Format("The browser's actual URL and the expected page URL match, which would indicate that the problem is probably in the page you expected to forward the request to, specified in the Submit<>.{0}{0}", Environment.NewLine);               
            }
            else if (currentUrl.Equals(currentPage.RawUrl))
            {
                this.message = string.Format("The browser's actual URL and the current page URL match, which means that either:{0}", Environment.NewLine);
                this.message += string.Format("1. The post failed, and you were actually testing a scenario that meant to be successful{0}", Environment.NewLine);
                this.message += string.Format("2. Or the post was successful, but you have asserted the incorrect page in the method ShouldBeAt<>{0}{0}", Environment.NewLine);
            }

            this.message += string.Format("Current Page: {0}{1}", currentPage, Environment.NewLine);
            this.message += string.Format("Expected Page: {0}{1}", expectedPage, Environment.NewLine);
            this.message += string.Format("Current URL: {0}{1}", currentUrl, Environment.NewLine);
            this.message += string.Format("Expected URL: {0}{1}", expectedUrl, Environment.NewLine);
        }

        private string message;

        public override string Message
        {
            get { return this.message; }
        }
    }
}
