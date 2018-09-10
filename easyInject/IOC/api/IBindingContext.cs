using System;

namespace EasyInject.IOC
{
    public delegate object FallbackDelegate(IBindingName name, IBindingKey key, object[] extras);
	public interface IBindingContext
	{
       
		IValueBindingContext<T> Bind<T>();
		IValueBindingContext<T> Bind<T>(IBindingName name);

		T Get<T>();
		T Get<T>(IBindingName name,params object[] extras);

		bool TryGet<T>(out T t,params object[] extras);
		bool TryGet<T>(IBindingName name,out T t,params object[] extras);

		IUnsafeBindingContext Unsafe{get;}

        void FallBack(FallbackDelegate fallbackfunc);
	}
}

