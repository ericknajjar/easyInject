using System.Collections;
using EasyInject;
using EasyInject.IOC;
using EasyInject.IOC.extensions;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;

namespace EasyInject.Engine.Runtime
{
	public class ReflectiveBindingContextFactory 
	{
		IEnumerable<ReflectiveBinding> m_bindings;

		public ReflectiveBindingContextFactory(IEnumerable<ReflectiveBinding> bindings)
		{
			m_bindings = bindings;
		}

		public IBindingContext CreateContext()
		{
			var context = BindingContext.Create();

			foreach(var binding in m_bindings)
			{
				List<IBindingRequirement> requirements = new List<IBindingRequirement>();

				foreach(var req in binding.Dependencies)
				{
					var realReq = BindingRequirements.Instance.With(req.name,req.BindingType);
					requirements.Add(realReq);
				}

			
				var args = new List<Type>(binding.Factory.GetParameters().Select(p => p.ParameterType));
				args.Add(binding.Factory.ReturnType);

				var delegateType = Expression.GetFuncType(args.ToArray());

				var factory = System.Delegate.CreateDelegate(delegateType,binding.Factory);

				IBinding realBinding = new Binding(factory,requirements.ToArray());
                if (binding.Singleton)
                    realBinding = new SingletonBinding(realBinding);

                AddToContext(context,binding.Root, realBinding, binding.Subcontexts);
	    	}

			return context;
		}

        private void AddToContext(IBindingContext root, BindingPair pair, IBinding binding, object[] subcontexts) {

            IBindingContext finalBindingContext = root;

            foreach(var contextName in subcontexts)
            {
                finalBindingContext = finalBindingContext.GetSubcontext(contextName);
            }

            finalBindingContext.Unsafe.Bind(pair.name, pair.BindingType).To(binding);
        }
	}
}

