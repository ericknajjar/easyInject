
using System.Collections.Generic;



namespace EasyInject.Engine.Runtime
{
	public class BindingProviderAttribute : System.Attribute
	{
		public object Name = IOC.InnerBindingNames.Empty;
		public int DependencyCount = 0;
		public object[] DependencieNames = new object[0];
	}


}
