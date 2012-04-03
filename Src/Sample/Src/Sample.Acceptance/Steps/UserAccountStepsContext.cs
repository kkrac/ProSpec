using System;
using Sample.Acceptance.Support.Queries;
using Sample.Data.Access;
using Sample.Data.Access.Queries;
using Sample.Domain.Model.User;
using TechTalk.SpecFlow;
using TwoK.Core.Security.Encryption;
using TwoK.IoC;

namespace Sample.Acceptance.Steps
{
    public abstract class UserAccountStepsContext : SampleStepsContext
    {
        protected void DeletePeople()
        {
            IPersons_DeleteAll deleteUsers = IoCProvider.Resolve<IPersons_DeleteAll>();
            deleteUsers.Execute();
        }

        protected void DeleteUsers()
        {
            IUsers_DeleteAll deleteUsers = IoCProvider.Resolve<IUsers_DeleteAll>();
            deleteUsers.Execute();
        }

        protected void DeleteTemporaryUsers()
        {
            IUsers_Temp_DeleteAll deleteTemporaryUsers = IoCProvider.Resolve<IUsers_Temp_DeleteAll>();
            deleteTemporaryUsers.Execute();
        }

        protected AuthenticatedUser GetUser(string userId)
        {
            IUser_SelectById selectUserById = IoCProvider.Resolve<IUser_SelectById>();

            return selectUserById.Execute(userId);
        }

        protected bool IsUserActive(string userId)
        {
            IUser_IsActive userIsActive = IoCProvider.Resolve<IUser_IsActive>();

            return userIsActive.Execute(userId);
        }

        protected void CreateAccount(string userId, string password, string token, string firstName, string lastName, string email, DateTime dateOfBirth)
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

        [Given(@"the following temporary accounts exist")]
        public void Given_The_Following_Temporary_Account_Exists(Table table)
        {
            int i = 0;

            foreach (var row in table.Rows)
            {
                string userName = row["UserName"];
                string token = row["Token"];

                CreateAccount(userName, "P@ssword1", token, "john" + i, "doe", "john" + i + "@doe.com", DateTime.Now);
            }
        }

        [Given(@"the following users exist")]
        public void Given_The_Following_Users_Exist(Table table)
        {
            IUserAccountDAO accountDAO = IoCProvider.Resolve<IUserAccountDAO>();
            IEncryptionService encryptionService = IoCProvider.Resolve<IEncryptionService>();
            string encryptedPassword;

            foreach (var row in table.Rows)
            {
                var user = new AuthenticatedUser
                (
                    row["UserName"],
                    row["FirstName"],
                    row["LastName"],
                    Convert.ToDateTime(row["DateOfBirth"]),
                    row["Email"]
                );

                encryptedPassword = encryptionService.Encrypt(row["Password"], user.Id);
                accountDAO.CreateAccount(user, encryptedPassword);
            }
        }
    }
}
