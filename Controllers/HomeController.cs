using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebLanchesMVC.Models;
using WebLanchesMVC.Repositories;
using WebLanchesMVC.ViewModel;

namespace WebLanchesMVC.Controllers
{
    public class HomeController : Controller
    {
		private readonly ILunchRepository _lunchRepository;

		public HomeController(ILunchRepository lunchRepository)
		{
			_lunchRepository = lunchRepository;
		}

        public IActionResult Index()
        {
			var homeViewModel = new HomeViewModel
			{
				LunchesPreferred = _lunchRepository.LunchesPreferred
			};

            return View(homeViewModel);
        }

    }
}
