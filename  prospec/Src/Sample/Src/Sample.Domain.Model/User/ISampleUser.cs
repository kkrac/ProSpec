using System;
using TwoK.Core.Security;

namespace Sample.Domain.Model.User
{
    public interface ISampleUser : IUser
    {
        string FirstName { get; }
        string LastName { get; }
        DateTime DateOfBirth { get; }
        Email Email { get; }
    }
}
