namespace Sample.Data.Access.Queries {
   using System;
   using System.Data;
   using TwoK.Data.Access;
   using TwoK.Data.Access.Executers;
   
   
   /// <summary>
   /// Wraps the User_Insert Stored Procedure
   /// </summary>
   public interface IUser_Insert {
      
      Int32 Execute(string userId, int? personId, string password, bool? active, string createdBy, DateTime? createdOn);
   }
   
   public class User_Insert : QueryBase, IUser_Insert {
      
      public User_Insert(IDbCommandExecuter executer, IDbParameterFactory parameterFactory) : 
            base(executer, parameterFactory) {
      }
      
      public override String Text {
         get {
            return "User_Insert";
         }
      }
      
      public virtual Int32 Execute(string userId, int? personId, string password, bool? active, string createdBy, DateTime? createdOn) {
         IDataParameter _userId = ParameterFactory.CreateParameter("@UserId", userId);
         IDataParameter _personId = ParameterFactory.CreateParameter("@PersonId", personId);
         IDataParameter _password = ParameterFactory.CreateParameter("@Password", password);
         IDataParameter _active = ParameterFactory.CreateParameter("@Active", active);
         IDataParameter _createdBy = ParameterFactory.CreateParameter("@CreatedBy", createdBy);
         IDataParameter _createdOn = ParameterFactory.CreateParameter("@CreatedOn", createdOn);

         Int32 objectToReturn = (Int32)Executer.Execute(Text, _userId, _personId, _password, _active, _createdBy, _createdOn);

         return objectToReturn;
      }
   }
}
