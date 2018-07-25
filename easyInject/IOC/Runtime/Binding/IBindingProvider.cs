using System.Collections.Generic;

namespace EasyInject.Engine.Runtime
{
	public interface IBindingProvider 
	{
		IList<IBindingProvider> Dependencies{get;}
	}


}