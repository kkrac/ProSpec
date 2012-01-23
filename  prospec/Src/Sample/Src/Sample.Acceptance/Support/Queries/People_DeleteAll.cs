namespace Sample.Acceptance.Support.Queries
{
   using System;
    using TwoK.Data.Access;
   using TwoK.Data.Access.Executers;
   
   
   /// <summary>
    /// Wraps the Persons_DeleteAll Query
   /// </summary>
   public interface IPersons_DeleteAll {
      
      Int32 Execute();
   }

   public class Persons_DeleteAll : QueryBase, IPersons_DeleteAll  {

       public Persons_DeleteAll(IDbCommandExecuter executer, IDbParameterFactory parameterFactory) : 
            base(executer, parameterFactory) {
      }
      
      public override String Text {
         get {
             return "DELETE FROM Persons";
         }
      }

      public virtual Int32 Execute() {
         Int32 objectToReturn = (Int32)Executer.Execute(Text);

         return objectToReturn;
      }
   }
}
