using System;

namespace EasyInject.Engine.Runtime
{
    [System.AttributeUsage(System.AttributeTargets.Class |  
                       System.AttributeTargets.Struct)  
] 
    public class HasBindings : System.Attribute
    {
  
    }
}