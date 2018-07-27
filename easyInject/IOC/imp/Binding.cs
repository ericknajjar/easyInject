﻿using System;
using System.Collections.Generic;

namespace EasyInject.IOC
{
	public class Binding: IBinding
	{
		Delegate m_factory;
		IBindingRequirement[] m_requirements;
   
        public Binding(Delegate factory, params IBindingRequirement[] requirement)
		{
			m_requirements = requirement;
			m_factory = factory;
		}

		public object Get(IBindingContext bindingContext, params object[] extras)
		{
            List<object> parameters = new List<object>(m_requirements.Length + extras.Length);

            foreach (var requirement in m_requirements)
            {
                var par = requirement.Get(bindingContext);
                parameters.Add(par);
            }

            parameters.AddRange(extras);

            if (parameters.Count == 0)
            {
                return m_factory.DynamicInvoke(null);
            }
            else
            {
                return m_factory.DynamicInvoke(parameters.ToArray());
            }

		}

		public void CheckRequiremets (IBindingKey key, IBindingName name)
		{
			var req = BindingRequirements.Instance.With(name,key);

			foreach(var requiremet in m_requirements)
			{
				if(req.Equals(requiremet))
				{
					throw new BindingSelfRequirement();
				}
			}

		}
	}
}

