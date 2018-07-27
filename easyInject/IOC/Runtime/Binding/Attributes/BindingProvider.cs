
using System.Collections.Generic;



namespace EasyInject.Engine.Runtime
{
    [System.AttributeUsage(System.AttributeTargets.Method)] 
	public class BindingProviderAttribute : System.Attribute
	{
		public object Name = IOC.InnerBindingNames.Empty;
		public int DependencyCount = 0;
        public bool Singleton = false;
		public object[] DependencieNames = new object[0];
	}


}
