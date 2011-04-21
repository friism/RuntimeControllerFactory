using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using Mono.CSharp;

namespace Fusonic.Web.Mvc.RuntimeController
{
	public class RuntimeControllerActivator : IControllerActivator
	{
		private readonly IRuntimeControllerPathProvider pathProvider;
		private readonly List<Assembly> assemblies = new List<Assembly>();

		public RuntimeControllerActivator(IRuntimeControllerPathProvider pathProvider)
		{
			this.pathProvider = pathProvider;
		}

		public void ReferenceAssembly(Assembly assembly)
		{
			assemblies.Add(assembly);
		}

		public IController Create(RequestContext requestContext, Type controllerType)
		{
			CompilerSettings settings = new CompilerSettings();
			Report report = new Report(new ConsoleReportPrinter());
			Evaluator eval = new Evaluator(settings, report);

			object instance = null;
			bool instanceCreated = false;
			eval.ReferenceAssembly(typeof(Controller).Assembly);

			foreach (Assembly assembly in assemblies)
			{
				eval.ReferenceAssembly(assembly);
			}

			string controllerName = GetControllerName(requestContext, controllerType);
			string path = pathProvider.GetPath(requestContext, controllerName);
			CSharpControllerFile controllerFile = CSharpControllerFile.Parse(File.ReadAllText(path));

			eval.Run(controllerFile.ClassSource);
			eval.Evaluate("new " + controllerName + "();", out instance, out instanceCreated);

			return (IController)instance;
		}

		protected virtual string GetControllerName(RequestContext requestContext, Type controllerType)
		{
			string controllerName;
			if (controllerType != null)
			{
				controllerName = controllerType.Name;
			}
			else
			{
				controllerName = requestContext.RouteData.Values["controller"] as string;
				if (controllerName != null)
				{
					controllerName += "Controller";
				}
			}

			return controllerName;
		}
	}
}
