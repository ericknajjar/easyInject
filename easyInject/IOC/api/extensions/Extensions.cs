using System;

namespace EasyInject.IOC.extensions
{
	public static class IUnsafeBindingContextExtensions
	{
		public static object Get(this IUnsafeBindingContext me,object name,object key)
		{
			return me.Get(new BindingName(name), new BindingKey(key));
		}

		public static  IUnsafeValueBindingContext Bind(this IUnsafeBindingContext me,object key)
		{
			return me.Bind(new BindingKey(key));
		}

		public static IUnsafeValueBindingContext Bind (this IUnsafeBindingContext me,object name,object key)
		{
			return me.Bind(new BindingName(name),new BindingKey(key));
		}

	}

	public static class IBindingContextExtensions
	{
		static public IValueBindingContext<T> Bind<T> (this IBindingContext me,object name)
		{
			return me.Bind<T>(new BindingName(name));
		}

		static public  T Get<T>(this IBindingContext me,object name, params object[] extras)
		{
			return me.Get<T>(new BindingName(name),extras);
		}

		static public  bool TryGet<T>(this IBindingContext me,object name,out T t, params object[] extras)
		{
			return me.TryGet<T>(new BindingName(name),out t);
		}

        public static IBindingContext GetSubcontext(this IBindingContext me ,params object[] names)
        {
            IBindingContext context = me;
            var length = names.Length;

            for (int i = 0; i < length;++i)
            {
                var name = names[i];
                IBindingContext currentContext;

                if (!context.TryGet<IBindingContext>(name, out currentContext))
                {
                    currentContext = BindingContext.Create();
                    context.Bind<IBindingContext>(name).To(currentContext);
                }

                context = currentContext;
            }

          

            return context;
        }
	}

	public static class BindingRequirementsExtensions
	{
		static public IBindingRequirement With<T>(this BindingRequirements me, object name)
		{
			return me.With<T>(new BindingName(name));
		}

		static public IBindingRequirement With(this BindingRequirements me, object name,object key)
		{
			return me.With(new BindingName(name), new BindingKey(key));
		}
	}

	public static class IBindingExtensions
	{
		static public void CheckRequiremets (this IBinding me,object key, object name)
		{
			 me.CheckRequiremets(new BindingKey(key), new BindingName(name));
		}
	}

    public static class IValueBindingContextExtensions
    {
        static public void To<T>(this IValueBindingContext<T> me, T obj) {
            me.ToSingleton(() => obj);
        }

    }
}
	

