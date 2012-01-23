using System;
using ProSpec.Core.Security;

namespace Sample.Domain.Model.User
{
    public class AnonymousUser : ISampleUser
    {
        public AnonymousUser(string firstName, string lastName, DateTime dateOfBirth, Email email)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.Email = email;
        }

        public string Id
        {
            get { return "guest"; }
        }

        public IRole Role
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsAuthenticated
        {
            get { return false; }
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
