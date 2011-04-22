using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;

namespace Fusonic.Web.Mvc.RuntimeController
{
	public class RuntimeControllerDependencyResolver : IDependencyResolver
	{
		Assembly _applicationAssembly;

		public RuntimeControllerDependencyResolver(Assembly applicationAssembly)
		{
			_applicationAssembly = applicationAssembly;
		}

		public object GetService(Type serviceType)
		{
			if (serviceType == typeof(IControllerActivator))
			{
				var runtimeControllerActivator = new RuntimeControllerActivator(new DefaultPathProvider());
				runtimeControllerActivator.ReferenceAssembly(_applicationAssembly);
				return runtimeControllerActivator;
			}
			return null;
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return new List<object>();
		}
	}
}