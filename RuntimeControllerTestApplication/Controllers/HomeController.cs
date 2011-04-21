using System.Web.Mvc;

namespace RuntimeControllerTestApplication.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}
