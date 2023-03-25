using CafeManagementSystem.Commons;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagementSystem.Areas.Admin.Controllers
{
	[Area(AreaAttributs.AreaAdmin)]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
