using System;
using TwoK.Core.IoC;
using Sample.Acceptance.Support.Queries;
using Sample.Data.Access.Queries;
using Sample.Domain.Model.User;
using TechTalk.SpecFlow;

namespace Sample.Acceptance.Support
{
    [Binding]
    public class UserAccountStepsUtils
    {
        [AfterScenario]
        public static void ClearTables()
        {
            DeleteTemporaryUsers();
            DeleteUsers();
            DeletePeople();
        }

        private static void DeletePeople()
        {
            IPersons_DeleteAll deleteUsers = IoCProvider.Resolve<IPersons_DeleteAll>();
            deleteUsers.Execute();
        }

        private static void DeleteUsers()
        {
            IUsers_DeleteAll deleteUsers = IoCProvider.Resolve<IUsers_DeleteAll>();
            deleteUsers.Execute();
        }

        private static void DeleteTemporaryUsers()
        {
            IUsers_Temp_DeleteAll deleteTemporaryUsers = IoCProvider.Resolve<IUsers_Temp_DeleteAll>();
            deleteTemporaryUsers.Execute();
        }

        public static AuthenticatedUser GetUser(string userId)
        {
            IUser_SelectById selectUserById = IoCProvider.Resolve<IUser_SelectById>();

            return selectUserById.Execute(userId);
        }

        public static bool IsUserActive(string userId)
        {
            IUser_IsActive userIsActive = IoCProvider.Resolve<IUser_IsActive>();

            return userIsActive.Execute(userId);
        }

        public static void CreateAccount(string userId, string password, string token, string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            ISampleUser user = new AuthenticatedUser(userId, firstName, lastName, dateOfBirth, email);

            IPerson_Insert insertPerson = IoCProvider.Resolve<IPerson_Insert>();

            int personId = (int)insertPerson.Execute
                                (
                                    user.FirstName,
                                    user.LastName,
                                    user.Email.ToString(),
                                    user.DateOfBirth,
                                    user.Id,
                                    DateTime.Now
                                );

            IUser_Insert insertUser = IoCProvider.Resolve<IUser_Insert>();

            insertUser.Execute
            (
                user.Id,
                personId,
                password,
                false,
                user.Id,
                DateTime.Now
            );

            IUser_Temp_Insert insertTemporaryAccount = IoCProvider.Resolve<IUser_Temp_Insert>();
            insertTemporaryAccount.Execute
            (
                userId,
                token
            );
        }
    }
}
