namespace ProSpec.Core.UI.Web
{
    using System.Net;

    public interface IBrowser
    {
        void GoTo(string url);
        string Url { get; }
        bool IsOnPage(Page page);
        HttpStatusCode Status { get; }
        string Text { get; }
        void Close();
    }
}