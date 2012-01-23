using Sample.Domain.Model.User;

namespace Sample.Domain.Services
{
    public interface IUserAccountService
    {
        void ActivateAccount(string userId);
        void ValidateLogin(string userName, string password);
        void CreateAccount(ISampleUser user, string password, string passwordConfirmation, bool acceptsTerms);
        bool IsTemporaryAccountValid(string userName, string token);
    }
}
