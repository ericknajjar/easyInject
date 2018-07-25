using System;
using EasyInject.IOC;

namespace EasyInject.Tests.BindingContextTests
{
	public static class TestsFactory
	{
		//static BindingContext m_context 
		public static IBindingContext BindingContext ()
		{
			return EasyInject.IOC.BindingContext.Create();
		}
	}
}

