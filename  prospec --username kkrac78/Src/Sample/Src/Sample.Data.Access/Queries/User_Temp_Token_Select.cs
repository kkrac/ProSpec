namespace Sample.Data.Access.Queries {
   using System;
   using System.Data;
   using TwoK.Data.Access;
   using TwoK.Data.Access.Executers;
   
   
   /// <summary>
   /// Wraps the User_Temp_Token_Select Stored Procedure
   /// </summary>
   public interface IUser_Temp_Token_Select {
      
      IDataReader Execute(string userId);
   }
   
   public class User_Temp_Token_Select : QueryBase, IUser_Temp_Token_Select {
      
      public User_Temp_Token_Select(IDbCommandExecuter executer, IDbParameterFactory parameterFactory) : 
            base(executer, parameterFactory) {
      }
      
      public override String Text {
         get {
            return "User_Temp_Token_Select";
         }
      }
      
      public virtual IDataReader Execute(string userId) {
         IDataParameter _userId = ParameterFactory.CreateParameter("@UserId", userId);

         IDataReader objectToReturn = (IDataReader)Executer.Execute(Text, _userId);

         return objectToReturn;
      }
   }
}
