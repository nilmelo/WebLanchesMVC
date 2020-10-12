

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebLanchesMVC.Controllers
{
    public class ContactController : Controller
    {
		public IActionResult Index()
		{
			return View();
		}
    }
}
