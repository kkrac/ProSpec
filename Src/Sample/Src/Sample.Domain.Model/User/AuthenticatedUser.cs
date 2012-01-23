using System;
using TwoK.Core.Security;

namespace Sample.Domain.Model.User
{
    public class AuthenticatedUser : ISampleUser
    {
        public AuthenticatedUser(string userId, string firstName, string lastName, DateTime dateOfBirth, Email email)
        {
            this.Id = userId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.Email = email;
        }

        public string Id
        {
            get; private set;
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }

        public IRole Role
        {
            get { throw new NotImplementedException(); }
        }

        public string FirstName
        {
            get; private set;
        }

        public string LastName
        {
            get; private set;
        }

        public DateTime DateOfBirth
        {
            get; private set;
        }

        public Email Email
        {
            get; private set;
        }
    }
}
