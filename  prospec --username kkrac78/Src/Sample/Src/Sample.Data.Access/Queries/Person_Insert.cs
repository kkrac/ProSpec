namespace Sample.Data.Access.Queries {
   using System;
   using System.Data;
   using TwoK.Data.Access;
   using TwoK.Data.Access.Executers;
   
   
   /// <summary>
   /// Wraps the Person_Insert Stored Procedure
   /// </summary>
   public interface IPerson_Insert {
      
      Object Execute(string firstName, string lastName, string email, DateTime? dateOfBirth, string createdBy, DateTime? createdOn);
   }
   
   public class Person_Insert : QueryBase, IPerson_Insert {
      
      public Person_Insert(IDbCommandExecuter executer, IDbParameterFactory parameterFactory) : 
            base(executer, parameterFactory) {
      }
      
      public override String Text {
         get {
            return "Person_Insert";
         }
      }
      
      public virtual Object Execute(string firstName, string lastName, string email, DateTime? dateOfBirth, string createdBy, DateTime? createdOn) {
         IDataParameter _firstName = ParameterFactory.CreateParameter("@FirstName", firstName);
         IDataParameter _lastName = ParameterFactory.CreateParameter("@LastName", lastName);
         IDataParameter _email = ParameterFactory.CreateParameter("@Email", email);
         IDataParameter _dateOfBirth = ParameterFactory.CreateParameter("@DateOfBirth", dateOfBirth);
         IDataParameter _createdBy = ParameterFactory.CreateParameter("@CreatedBy", createdBy);
         IDataParameter _createdOn = ParameterFactory.CreateParameter("@CreatedOn", createdOn);

         Object objectToReturn = (Object)Executer.Execute(Text, _firstName, _lastName, _email, _dateOfBirth, _createdBy, _createdOn);

         return objectToReturn;
      }
   }
}
