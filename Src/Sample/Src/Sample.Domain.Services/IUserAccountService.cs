using Sample.Domain.Model.User;

namespace Sample.Domain.Services
{
    public interface IUserAccountService
    {
        bool ActivateAccount(string userId, string token);
        void ValidateLogin(string userName, string password);
        void CreateAccount(ISampleUser user, string password, string passwordConfirmation, bool acceptsTerms);
    }
}
