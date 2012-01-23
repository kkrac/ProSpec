using System;
using Sample.Domain.Model.User;

namespace Sample.Data.Access
{
    public interface IUserAccountDAO
    {
        void ActivateAccount(string userId);
        ISampleUser GetUser(string userId);
        string GetPasswordForUser(string userId);
        string CreateAccount(ISampleUser user, string password);
        string GetTokenForUser(string userId);
    }
}
