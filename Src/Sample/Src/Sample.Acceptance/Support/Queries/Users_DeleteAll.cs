namespace Sample.Acceptance.Support.Queries
{
   using System;
    using TwoK.Data.Access;
   using TwoK.Data.Access.Executers;
   
   
   /// <summary>
   /// Wraps the Users_DeleteAll Query
   /// </summary>
   public interface IUsers_DeleteAll {
      
      Int32 Execute();
   }

   public class Users_DeleteAll : QueryBase, IUsers_DeleteAll  {

       public Users_DeleteAll(IDbCommandExecuter executer, IDbParameterFactory parameterFactory) : 
            base(executer, parameterFactory) {
      }
      
      public override String Text {
         get {
             return "DELETE FROM Users";
         }
      }

      public virtual Int32 Execute() {
         Int32 objectToReturn = (Int32)Executer.Execute(Text);

         return objectToReturn;
      }
   }
}
