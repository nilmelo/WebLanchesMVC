using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebLanchesMVC.Models;
using WebLanchesMVC.Repositories;
using WebLanchesMVC.ViewModel;

namespace WebLanchesMVC.Controllers
{
    public class LunchController : Controller
    {
		private readonly ILunchRepository _lunchRepository;
		private readonly ICategoryRepository _categoryRepository;

		public LunchController(ILunchRepository lunchRepository, ICategoryRepository categoryRepository)
		{
			_lunchRepository = lunchRepository;
			_categoryRepository = categoryRepository;
		}

		public IActionResult List(string category)
		{
			IEnumerable<Lunch> lunches;
			string categoryCurrent = string.Empty;

			if(string.IsNullOrEmpty(category))
			{
				lunches = _lunchRepository.Lunches.OrderBy(l => l.Id);
				category = "Todos os lanches";
			}
			else
			{
				lunches = _lunchRepository.Lunches
							.Where(p => p.Category.Name.Equals(category))
							.OrderBy(p => p.Name);

				categoryCurrent = category;
			}

			var lunchesListViewModel = new LunchListViewModel
			{
				Lunches = lunches,
				CategoryCurrent = categoryCurrent
			};

			return View(lunchesListViewModel);
		}

		public IActionResult Details(int lunchId)
		{
			var lunch = _lunchRepository.Lunches.FirstOrDefault(l => l.Id == lunchId);

			if(lunch == null)
			{
				return View("~/Views/Error/Error.cshtml");
			}
			return View(lunch);
		}

		public IActionResult Search(string searchString)
		{
			string _searchString = searchString;
			IEnumerable<Lunch> lunches;
			string _categoryCurrent = string.Empty;

			if(string.IsNullOrEmpty(_searchString))
			{
				lunches = _lunchRepository.Lunches.OrderBy(l => l.Id);
			}
			else
			{
				lunches = _lunchRepository.Lunches.Where(l => l.Name.ToLower().Contains(_searchString.ToLower()));
			}
			return View("~/Views/Lunch/List.cshtml", new LunchListViewModel {
				Lunches = lunches,
				CategoryCurrent="Todos os lanches"
			});
		}

    }
}
