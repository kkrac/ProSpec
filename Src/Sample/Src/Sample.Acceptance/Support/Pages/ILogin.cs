using Krac.Framework.Acceptance;

namespace Sample.Acceptance.UI.Pages
{
    public interface ILogin : IIdentifiableTestDriver
    {
        string UserName
        {
            get; set;
        }

        string Password
        {
            get; set;
        }
    }
}
