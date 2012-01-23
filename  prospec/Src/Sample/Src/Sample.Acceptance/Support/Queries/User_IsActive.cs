namespace Sample.Acceptance.Support.Queries
{
   using System;
    using TwoK.Data.Access;
   using TwoK.Data.Access.Executers;
    using System.Data;
   
   
   /// <summary>
    /// Wraps the User_IsActive Query
   /// </summary>
   public interface IUser_IsActive {

       bool Execute(string userId);
   }

   public class User_IsActive : QueryBase, IUser_IsActive  {

       public User_IsActive(IDbCommandExecuter executer, IDbParameterFactory parameterFactory) : 
            base(executer, parameterFactory) {
      }
      
      public override String Text {
         get {
             return "SELECT Active FROM Users WHERE UserId = @UserId";
         }
      }

        public virtual bool Execute(string userId) {
            IDataParameter _userId = ParameterFactory.CreateParameter("@UserId", userId);
            bool isActive = false;

            using (IDataReader reader = (IDataReader)Executer.Execute(Text, _userId))
            {
                if (reader.Read())
                {
                    isActive = Convert.ToBoolean(reader["Active"]);
                }
            }

            return isActive;
      }
   }
}
