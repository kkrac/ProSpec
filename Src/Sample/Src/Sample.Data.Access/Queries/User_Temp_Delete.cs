namespace Sample.Data.Access.Queries {
   using System;
   using System.Data;
   using TwoK.Data.Access;
   using TwoK.Data.Access.Executers;
   
   
   /// <summary>
   /// Wraps the User_Temp_Delete Stored Procedure
   /// </summary>
   public interface IUser_Temp_Delete {
      
      Int32 Execute(string userId);
   }

   public class User_Temp_Delete : QueryBase, IUser_Temp_Delete {

       public User_Temp_Delete(IDbCommandExecuter executer, IDbParameterFactory parameterFactory) : 
            base(executer, parameterFactory) {
      }
      
      public override String Text {
         get {
             return "User_Temp_Delete";
         }
      }
      
      public virtual Int32 Execute(string userId) {
         IDataParameter _userId = ParameterFactory.CreateParameter("@UserId", userId);

         Int32 objectToReturn = (Int32)Executer.Execute(Text, _userId);

         return objectToReturn;
      }
   }
}
