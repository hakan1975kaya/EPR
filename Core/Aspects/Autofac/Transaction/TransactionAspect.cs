using Castle.DynamicProxy;
using Core.Utilities.Interceptors;

using System.Transactions;

namespace Core.Aspects.Autofac.Transaction
{
    public class TransactionAspect : MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(30)))
            {
                try
                {
                    invocation.Proceed();
                    transactionScope.Complete();
                }
                catch (System.Exception)
                {
                    transactionScope.Dispose();
                    throw;
                }
            }
        }








    }
}
