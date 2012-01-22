namespace Sample.Data.Access.Queries {
   using System;
   using System.Data;
   using TwoK.Core;
   using TwoK.Data.Access;
   using TwoK.Data.Access.Executers;
   using Sample.Domain.Model.User;
   
   
   /// <summary>
   /// Wraps the User_SelectById Stored Procedure
   /// </summary>
   public interface IUser_SelectById {
      
      AuthenticatedUser Execute(string userId);
   }
   
   public class User_SelectById : QueryBase, IUser_SelectById {
      
      public User_SelectById(IDbCommandExecuter executer, IDbParameterFactory parameterFactory, IConverter<IDataReader, AuthenticatedUser> converter) : 
            base(executer, parameterFactory) {
         this.converter = converter;
      }
      
      public override String Text {
         get {
            return "User_SelectById";
         }
      }
      
      private IConverter<IDataReader, AuthenticatedUser> converter;
      
      public virtual AuthenticatedUser Execute(string userId) {
         IDataParameter _userId = ParameterFactory.CreateParameter("@UserId", userId);

         AuthenticatedUser objectToReturn;

         using(IDataReader response = (IDataReader)Executer.Execute(Text, _userId))
         {
            objectToReturn = converter.Convert(response);
         }

         return objectToReturn;
      }
   }
}
