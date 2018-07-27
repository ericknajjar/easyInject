using System;

namespace EasyInject.IOC
{
	public interface IValueBindingContext
	{
		void To<T>(System.Func<T> func);

	}

	public interface IValueBindingContext<T>
	{
		void To(System.Func<T> func);
        void ToSingleton<T>(System.Func<T> func);
		IValueBindingContext<T,K> With<K> ();
		IValueBindingContext<T,K> With<K> (object name);
	}

	public interface IValueBindingContext<T, J>  
	{
		void To(System.Func<J, T> func);
        void ToSingleton(System.Func<J, T> func);

		IValueBindingContext<T,J,K> With<K> ();

		IValueBindingContext<T,J,K> With<K> (IBindingName name);
		IValueBindingContext<T,J,K> With<K> (object name);
	}

	public interface IValueBindingContext<T, J, K>  
	{
		void To(System.Func<J,K,T> func);
        void ToSingleton(System.Func<J, K, T> func);
	}
}

