using System;

namespace Sample.Domain.Model.User
{
    public class TemporaryUser : User
    {
        public TemporaryUser(string userId, string password, string token, string firstName, string lastName, DateTime dateOfBirth, Email email)
            : base(userId, password, firstName, lastName, dateOfBirth, email)
        {
            this.Token = token;
        }

        public string Token { get; private set; }
    }
}
