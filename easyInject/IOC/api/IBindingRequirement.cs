using System;

namespace EasyInject.IOC
{
	public interface IBindingRequirement
	{
		object Get(IBindingContext bindingContext);
	}

}

