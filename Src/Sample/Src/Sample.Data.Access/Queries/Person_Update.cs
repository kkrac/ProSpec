namespace Sample.Data.Access.Queries {
   using System;
   using System.Data;
   using TwoK.Data.Access;
   using TwoK.Data.Access.Executers;
   
   
   /// <summary>
   /// Wraps the Person_Update Stored Procedure
   /// </summary>
   public interface IPerson_Update {
      
      Int32 Execute(int? personId, string names, string lastName, string email, DateTime? modifiedOn, string modifiedBy);
   }
   
   public class Person_Update : QueryBase, IPerson_Update {
      
      public Person_Update(IDbCommandExecuter executer, IDbParameterFactory parameterFactory) : 
            base(executer, parameterFactory) {
      }
      
      public override String Text {
         get {
            return "Person_Update";
         }
      }
      
      public virtual Int32 Execute(int? personId, string names, string lastName, string email, DateTime? modifiedOn, string modifiedBy) {
         IDataParameter _personId = ParameterFactory.CreateParameter("@PersonId", personId);
         IDataParameter _names = ParameterFactory.CreateParameter("@Names", names);
         IDataParameter _lastName = ParameterFactory.CreateParameter("@LastName", lastName);
         IDataParameter _email = ParameterFactory.CreateParameter("@Email", email);
         IDataParameter _modifiedOn = ParameterFactory.CreateParameter("@ModifiedOn", modifiedOn);
         IDataParameter _modifiedBy = ParameterFactory.CreateParameter("@ModifiedBy", modifiedBy);

         Int32 objectToReturn = (Int32)Executer.Execute(Text, _personId, _names, _lastName, _email, _modifiedOn, _modifiedBy);

         return objectToReturn;
      }
   }
}
