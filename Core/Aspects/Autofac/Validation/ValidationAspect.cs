using Castle.DynamicProxy;
using Core.Constants.Messages;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        Type _valiadtorType;
        public ValidationAspect(Type valiadtorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(valiadtorType))
            {
                throw new System.Exception(AspectMessages.WrongValidatorType);
            }
            _valiadtorType = valiadtorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_valiadtorType);
            var entityType = _valiadtorType.BaseType.GetGenericArguments()[0];
            var entities = invocation.Arguments.Where(x => x.GetType() == entityType);
            if (entities != null)
            {
                if (entities.Count() > 0)
                {
                    foreach (var entity in entities)
                    {
                        ValidationTool.Validate(validator, entity);
                    }
                }
            }
        }





    }
}
