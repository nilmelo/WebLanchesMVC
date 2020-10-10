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
			string _category = category;
			IEnumerable<Lunch> lunches;
			string categoryCurrent = string.Empty;

			if(string.IsNullOrEmpty(category))
			{
				lunches = _lunchRepository.Lunches.OrderBy(l => l.Id);
				category = "Todos os lanches";
			}
			else
			{
				if(string.Equals("Normal", _category, StringComparison.OrdinalIgnoreCase))
				{
					lunches = _lunchRepository.Lunches.Where(l =>
					l.Category.Name.Equals("Normal")).OrderBy(l => l.Name);
				}
				else
				{
					lunches = _lunchRepository.Lunches.Where(l =>
					l.Category.Name.Equals("Natural")).OrderBy(l => l.Name);
				}
				categoryCurrent = _category;
			}

			var lunchesListViewModel = new LunchListViewModel
			{
				Lunches = lunches,
				CategoryCurrent = categoryCurrent
			};

			return View(lunchesListViewModel);
		}
    }
}
