namespace Sample.Data.Access.Queries {
   using System;
   using System.Data;
   using TwoK.Data.Access;
   using TwoK.Data.Access.Executers;
   
   
   /// <summary>
   /// Wraps the User_Temp_Insert Stored Procedure
   /// </summary>
   public interface IUser_Temp_Insert {
      
      Int32 Execute(string userId, string token);
   }
   
   public class User_Temp_Insert : QueryBase, IUser_Temp_Insert {
      
      public User_Temp_Insert(IDbCommandExecuter executer, IDbParameterFactory parameterFactory) : 
            base(executer, parameterFactory) {
      }
      
      public override String Text {
         get {
            return "User_Temp_Insert";
         }
      }
      
      public virtual Int32 Execute(string userId, string token) {
         IDataParameter _userId = ParameterFactory.CreateParameter("@UserId", userId);
         IDataParameter _token = ParameterFactory.CreateParameter("@Token", token);

         Int32 objectToReturn = (Int32)Executer.Execute(Text, _userId, _token);

         return objectToReturn;
      }
   }
}
