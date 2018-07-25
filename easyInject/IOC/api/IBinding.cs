using System;

namespace EasyInject.IOC
{
	public interface IBinding
	{
		object Get(IBindingContext bindingContext, params object[] extras);
		void CheckRequiremets(IBindingKey key,IBindingName name);
	}
}

