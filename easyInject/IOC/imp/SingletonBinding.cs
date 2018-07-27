using System;

namespace EasyInject.IOC
{
    public class SingletonBinding: IBinding
    {
        IBinding m_adaptee;
        object m_singleton = null;

        public SingletonBinding(IBinding adaptee)
        {
            m_adaptee = adaptee;
        }

        public void CheckRequiremets(IBindingKey key, IBindingName name)
        {
            m_adaptee.CheckRequiremets(key, name);
        }

        public object Get(IBindingContext bindingContext, params object[] extras)
        {
            if (m_singleton == null)
                m_singleton = m_adaptee.Get(bindingContext, extras);

            return m_singleton;
        }
    }
}
