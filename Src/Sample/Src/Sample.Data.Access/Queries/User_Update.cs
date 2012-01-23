namespace Sample.Data.Access.Queries {
   using System;
   using System.Data;
   using TwoK.Data.Access;
   using TwoK.Data.Access.Executers;
   
   
   /// <summary>
   /// Wraps the User_Update Stored Procedure
   /// </summary>
   public interface IUser_Update {
      
      Int32 Execute(string userId, string password, string modifiedBy, DateTime? modifiedOn);
   }
   
   public class User_Update : QueryBase, IUser_Update {
      
      public User_Update(IDbCommandExecuter executer, IDbParameterFactory parameterFactory) : 
            base(executer, parameterFactory) {
      }
      
      public override String Text {
         get {
            return "User_Update";
         }
      }
      
      public virtual Int32 Execute(string userId, string password, string modifiedBy, DateTime? modifiedOn) {
         IDataParameter _userId = ParameterFactory.CreateParameter("@UserId", userId);
         IDataParameter _password = ParameterFactory.CreateParameter("@Password", password);
         IDataParameter _modifiedBy = ParameterFactory.CreateParameter("@ModifiedBy", modifiedBy);
         IDataParameter _modifiedOn = ParameterFactory.CreateParameter("@ModifiedOn", modifiedOn);

         Int32 objectToReturn = (Int32)Executer.Execute(Text, _userId, _password, _modifiedBy, _modifiedOn);

         return objectToReturn;
      }
   }
}
