
using System.Collections;
using System.Reflection;
using System.Collections.Generic;

namespace EasyInject.Engine.Runtime
{
	public class ReflectiveBinding  
	{
		public BindingPair Root{get; private set;}
		public MethodInfo Factory{get; private set;}
		public IList<BindingPair> Dependencies{get; private set;}
        public bool Singleton { get; private set; }
        public object[] Subcontexts { get; private set; }

        public ReflectiveBinding (BindingPair root, MethodInfo factory, IList<BindingPair> dependencies, bool singleton, object[] subcontexts)
		{
			this.Root = root;
			this.Factory = factory;
			this.Dependencies = dependencies;
            this.Singleton = singleton;
            this.Subcontexts = subcontexts;
		}
		
	}
}

