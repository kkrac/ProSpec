namespace Sample.Data.Access.Queries {
   using System;
   using System.Data;
   using TwoK.Data.Access;
   using TwoK.Data.Access.Executers;
   
   
   /// <summary>
   /// Wraps the Benchmark_Insert Stored Procedure
   /// </summary>
   public interface IBenchmark_Insert {
      
      Int32 Execute(string target, string method, string args, long? milliseconds, DateTime? creationDate, string createdBy);
   }
   
   public class Benchmark_Insert : QueryBase, IBenchmark_Insert {
      
      public Benchmark_Insert(IDbCommandExecuter executer, IDbParameterFactory parameterFactory) : 
            base(executer, parameterFactory) {
      }
      
      public override String Text {
         get {
            return "Benchmark_Insert";
         }
      }
      
      public virtual Int32 Execute(string target, string method, string args, long? milliseconds, DateTime? creationDate, string createdBy) {
         IDataParameter _target = ParameterFactory.CreateParameter("@Target", target);
         IDataParameter _method = ParameterFactory.CreateParameter("@Method", method);
         IDataParameter _args = ParameterFactory.CreateParameter("@Args", args);
         IDataParameter _milliseconds = ParameterFactory.CreateParameter("@Milliseconds", milliseconds);
         IDataParameter _creationDate = ParameterFactory.CreateParameter("@CreationDate", creationDate);
         IDataParameter _createdBy = ParameterFactory.CreateParameter("@CreatedBy", createdBy);

         Int32 objectToReturn = (Int32)Executer.Execute(Text, _target, _method, _args, _milliseconds, _creationDate, _createdBy);

         return objectToReturn;
      }
   }
}
