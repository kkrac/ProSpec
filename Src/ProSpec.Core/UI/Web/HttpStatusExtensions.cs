using System;
using System.Net;
using Should;
using Should.Core.Exceptions;

namespace ProSpec.Core.UI.Web
{
    public static class HttpStatusExtensions
    {
        public static void AssertEquals(this HttpStatusCode statusCode, HttpStatusCode expectedStatusCode)
        {
            try
            {
                statusCode.ShouldEqual(expectedStatusCode);
            }
            catch (EqualException ex)
            {
                HandleEqualException(ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void AssertEquals(this HttpStatusCode statusCode, HttpStatusCode expectedStatusCode, string expectedUrl)
        {
            try
            {
                statusCode.ShouldEqual(expectedStatusCode, string.Format("Incorrect browser HttpStatus for URL: {1}", Environment.NewLine, expectedUrl));
            }
            catch (EqualException ex)
            {
                HandleEqualException(ex);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void HandleEqualException(EqualException ex)
        {
            IBrowser browser = WebStepsContext.Current.Browser;

            string browserOutput = string.Empty;

            if (!string.IsNullOrEmpty(browser.Text))
            {
                browserOutput = Environment.NewLine + Environment.NewLine;

                browserOutput += browser.Text;
            }

            Exception exToThrow = new AssertException(ex.Message + browserOutput);

            throw exToThrow;
        }
    }
}
