using System.Web.Mvc;
using System;
using System.Collections.Generic;

namespace Fusonic.Web.Mvc.RuntimeController
{
	public class RuntimeControllerDependencyResolver : IDependencyResolver
	{
		public object GetService(Type serviceType)
		{
			return null;
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return new List<object>();
		}
	}
}
