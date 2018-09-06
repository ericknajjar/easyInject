using System;

namespace EasyInject.IOC
{
	internal class UnsafeBindingContextAdapter: IUnsafeBindingContext
	{
		BindingContext m_adaptee;

		public UnsafeBindingContextAdapter (BindingContext adaptee)
		{
			m_adaptee = adaptee;
		}
	
		#region IUnsafeBindingContext implementation

		public IUnsafeValueBindingContext Bind (IBindingKey key)
		{
			return m_adaptee.Bind( new BindingName(InnerBindingNames.Empty),key);
		}

		public IUnsafeValueBindingContext Bind (IBindingName name, IBindingKey key)
		{
			return m_adaptee.Bind(name,key);
		}

		public object Get (IBindingName name, IBindingKey key)
		{
			return m_adaptee.Get(name,key);
		}

        object IUnsafeBindingContext.Get(IBindingName name, IBindingKey key, params object[] extras)
        {
            return m_adaptee.Get(name, key, extras);
        }

  
        object IUnsafeBindingContext.TryGet(IBindingName name, IBindingKey key, params object[] extras)
        {
            return m_adaptee.TryGetBinding(name, key, extras);
        }

        #endregion
	}
}

