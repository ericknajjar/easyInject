using System;

namespace EasyInject.IOC
{
	public interface IUnsafeBindingContext
	{
		object Get(IBindingName name,IBindingKey key);
        object Get(IBindingName name, IBindingKey key, params object[] extras );
        object TryGet(IBindingName name, IBindingKey key, params object[] extras);

		IUnsafeValueBindingContext Bind (IBindingKey key);
		IUnsafeValueBindingContext Bind (IBindingName name,IBindingKey key);
	}
}

