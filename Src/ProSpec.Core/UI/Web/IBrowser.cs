using System.Net;
using System;

namespace ProSpec.Core.UI.Web
{
    public interface IBrowser : IDisposable
    {
        void GoTo(string url);
        string Url { get; }
        bool IsOnPage(Page page);
        HttpStatusCode Status { get; }
        string Text { get; }
        void Close();
    }
}