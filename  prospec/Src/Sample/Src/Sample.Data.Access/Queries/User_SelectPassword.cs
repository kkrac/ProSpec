namespace Sample.Data.Access.Queries {
   using System;
   using System.Data;
   using TwoK.Data.Access;
   using TwoK.Data.Access.Executers;
   
   
   /// <summary>
   /// Wraps the User_SelectPassword Stored Procedure
   /// </summary>
   public interface IUser_SelectPassword {
      
      IDataReader Execute(string userId);
   }
   
   public class User_SelectPassword : QueryBase, IUser_SelectPassword {
      
      public User_SelectPassword(IDbCommandExecuter executer, IDbParameterFactory parameterFactory) : 
            base(executer, parameterFactory) {
      }
      
      public override String Text {
         get {
            return "User_SelectPassword";
         }
      }
      
      public virtual IDataReader Execute(string userId) {
         IDataParameter _userId = ParameterFactory.CreateParameter("@UserId", userId);

         IDataReader objectToReturn = (IDataReader)Executer.Execute(Text, _userId);

         return objectToReturn;
      }
   }
}
