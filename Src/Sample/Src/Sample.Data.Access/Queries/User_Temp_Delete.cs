namespace Sample.Data.Access.Queries {
   using System;
   using System.Data;
   using TwoK.Data.Access;
   using TwoK.Data.Access.Executers;
   
   
   /// <summary>
   /// Wraps the User_Activate Stored Procedure
   /// </summary>
   public interface IUser_Activate {
      
      Int32 Execute(string userId);
   }
   
   public class User_Activate : QueryBase, IUser_Activate {
      
      public User_Activate(IDbCommandExecuter executer, IDbParameterFactory parameterFactory) : 
            base(executer, parameterFactory) {
      }
      
      public override String Text {
         get {
            return "User_Activate";
         }
      }
      
      public virtual Int32 Execute(string userId) {
         IDataParameter _userId = ParameterFactory.CreateParameter("@UserId", userId);

         Int32 objectToReturn = (Int32)Executer.Execute(Text, _userId);

         return objectToReturn;
      }
   }
}
