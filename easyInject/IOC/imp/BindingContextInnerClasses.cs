﻿using System;
using System.Collections.Generic;

namespace EasyInject.IOC
{
	public partial class BindingContext
	{
		class ValueBingindContextAdapter<T>: IValueBindingContext<T>
		{
			ValueBindingContext m_adaptee;

			public ValueBingindContextAdapter(ValueBindingContext adaptee)
			{
				m_adaptee = adaptee;
			}

			#region IValueBindingContext implementation

			void IValueBindingContext<T>.To (Func<T> func)
			{
				m_adaptee.To<T>(func);
			}

            public void ToSingleton<T1>(Func<T1> func)
            {
                var binding = new Binding(func);
                m_adaptee.To<T1>(new SingletonBinding(binding));
            }

			IValueBindingContext<T,K> IValueBindingContext<T>.With<K> ()
			{
				return new ValueBindingContextAdapterWith<T,K>(new BindingName(InnerBindingNames.Empty),m_adaptee);
			}

			public IValueBindingContext<T, K> With<K> (IBindingName name)
			{
				return new ValueBindingContextAdapterWith<T,K>(name,m_adaptee);
			}

			IValueBindingContext<T, K> IValueBindingContext<T>.With<K> (object name)
			{
				return With<K>(new BindingName(name));
			}

		

            #endregion
		}
	
		class UnsafeValueBindindContextAdapter: IUnsafeValueBindingContext
		{
			ValueBindingContext m_adaptee;
			IBindingKey m_key;

			public UnsafeValueBindindContextAdapter(IBindingKey key,ValueBindingContext adaptee)
			{
				m_adaptee = adaptee;
				m_key = key;
		
			}

			#region IUnsafeValueBindingContext implementation

			public void To (IBinding binding)
			{
				m_adaptee.To(m_key,binding);
			}

			#endregion
		}

		class ValueBindingContextAdapterWith<T,J>: IValueBindingContext<T,J>
		{
			ValueBindingContext m_adaptee;
			IBindingName m_name;

			public ValueBindingContextAdapterWith(IBindingName name,ValueBindingContext adaptee)
			{
				m_adaptee = adaptee;
				m_name = name;
			}

			#region IValueBindingContext implementation
			void IValueBindingContext<T, J>.To (Func<J, T> func)
			{
				var binding = new Binding(func, Requirements().ToArray());

				m_adaptee.To<T>(binding);
			}

            public void ToSingleton(Func<J, T> func)
            {
                var binding = new Binding(func, Requirements().ToArray());
                m_adaptee.To<T>(new SingletonBinding(binding));
            }


			public List<IBindingRequirement> Requirements()
			{
				var requirement = BindingRequirements.Instance.With<J>(m_name);

				return new List<IBindingRequirement>(){requirement};
			}

			IValueBindingContext<T, J, K> IValueBindingContext<T, J>.With<K> ()
			{
				return new ValueBingindContextAdapterWith<T,J,K>(new BindingName(InnerBindingNames.Empty),m_adaptee,this);
			}

			public IValueBindingContext<T, J, K> With<K> (IBindingName name)
			{
				return new ValueBingindContextAdapterWith<T,J,K>(name,m_adaptee,this);
			}

			public IValueBindingContext<T,J, K> With<K> (object name)
			{
				return With<K>(new BindingName(name));
			}

           

            #endregion
		}

		class ValueBingindContextAdapterWith<T,J,K>: IValueBindingContext<T,J,K>
		{
			ValueBindingContext m_adaptee;
			ValueBindingContextAdapterWith<T,J> m_parent;
			ValueBindingContextAdapterWith<T,K> m_me;

			IBindingName m_name;

			public ValueBingindContextAdapterWith(IBindingName name,ValueBindingContext adaptee, ValueBindingContextAdapterWith<T,J> parent)
			{
				m_adaptee = adaptee;
				m_name = name;
				m_parent = parent;
				m_me = new ValueBindingContextAdapterWith<T, K>(name,m_adaptee);
			}

			#region IValueBindingContext implementation

			void IValueBindingContext<T, J, K>.To (Func<J,K,T> func) 
			{
				var binding = new Binding(func, Requirements().ToArray());

				m_adaptee.To<T>(binding);
			}

            public void ToSingleton(Func<J, K, T> func)
            {
                var binding = new Binding(func, Requirements().ToArray());
                m_adaptee.To<T>(new SingletonBinding(binding));
            }
			#endregion

			public List<IBindingRequirement> Requirements()
			{
				List<IBindingRequirement> ret = new List<IBindingRequirement>();

				ret.AddRange(m_parent.Requirements());
				ret.AddRange(m_me.Requirements());

				return ret;
			}

     
		}

	
	}
}

