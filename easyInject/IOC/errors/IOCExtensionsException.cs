using System;

namespace EasyInject.IOC
{
    public class IOCExtensionsException: System.Exception
	{
        public IOCExtensionsException (object msg): base(msg.ToString())
		{

		}

		public IOCExtensionsException (){}
	}
}

