using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Interceptors
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        protected virtual void OnBefore(IInvocation invocation) { }
        protected virtual void OnAfter(IInvocation invocation) { }
        protected virtual void OnSuccess(IInvocation invocation) { }
        protected virtual void OnException(IInvocation invocation,Exception e) { }
        public override void Intercept(IInvocation invocation)
        {
            var isSucess = true;

            OnBefore(invocation);

            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                isSucess = false;
                OnException(invocation,ex);
                throw;
            }
            finally
            {
                if (isSucess)
                {
                    OnSuccess(invocation);
                }
            }
            OnAfter(invocation);
        }













    }
}
