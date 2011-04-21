using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Fusonic.Web.Mvc.RuntimeController
{
	public class RuntimeControllerActivator : IControllerActivator
	{
		public IController Create(RequestContext requestContext, Type controllerType)
		{
			throw new NotImplementedException();
		}
	}
}
