using System;
using TwoK.Core.DesignByContract;
using TwoK.Core.Messaging;
using TwoK.Core.Messaging.Email;
using TwoK.Core.Security.Encryption;
using Sample.Data.Access;
using Sample.Domain.Model.User;

namespace Sample.Domain.Services.Implementations
{
    public class UserAccountService : IUserAccountService
    {
        public UserAccountService(IUserAccountDAO dao, IEncryptionService encryptionService, IMessageSender<EmailMessage> emailService)
        {
            this.dao = dao;
            this.encryptionService = encryptionService;
            this.emailService = emailService;
        }

        private IUserAccountDAO dao;
        private IEncryptionService encryptionService;
        private IMessageSender<EmailMessage> emailService;

        public void ValidateLogin(string userId, string password)
        {
            Check.Require(!string.IsNullOrEmpty(userId), "The user name is required");
            Check.Require(!string.IsNullOrEmpty(password), "The password is required");
            
            string passwordInDB = dao.GetPasswordForUser(userId);

            string encryptedPassword = encryptionService.Encrypt(password, userId);

            if (!encryptedPassword.Equals(passwordInDB))
            {
                throw new ApplicationException("The user name or the password are invalid");
            }            
        }

        public void CreateAccount(ISampleUser user, string password, string passwordConfirmation, bool acceptsTerms)
        {
            string encryptedPassword = encryptionService.Encrypt(password, user.Id);

            string token = dao.CreateAccount(user, encryptedPassword);

            string body = "Welcome " + user.FirstName + "! In order to confirm your account, an e-mail was sent to you before you can access.";

            EmailMessage message = new EmailMessage("support@sample.com", user.Email, string.Empty, string.Empty, body, false);

            emailService.SendMessage(message);
        }

        public bool IsTemporaryAccountValid(string userId, string token)
        {
            Check.Require(!string.IsNullOrEmpty(userId), "The user id cannot be empty");
            Check.Require(!string.IsNullOrEmpty(token), "The token cannot be empty");

            string tokenInDB = dao.GetTokenForUser(userId);

            return token.Equals(tokenInDB);
        }

        public void ActivateAccount(string userId)
        {
            dao.ActivateAccount(userId);
        }
    }
}
