namespace Sample.Domain.Model.User
{
    using System;

    public class User
    {
        public User(string id, string password, string firstName, string lastName, DateTime dateOfBirth, string email)
        {
            this.Id = id;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.Email = email;
        }

        public string Id { get; private set; }
        public string Password { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public Email Email { get; private set; }
    }
}
