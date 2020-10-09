using Microsoft.AspNetCore.Mvc;
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

		public IActionResult List()
		{
			ViewBag.Lunch = "Lunches";
			ViewData["Category"] = "Category";

			var lunchListViewModel = new LunchListViewModel();
			lunchListViewModel.Lunches = _lunchRepository.Lunches;
			lunchListViewModel.CategoryCurrent = "Categoria Atual";
			return View(lunchListViewModel);
		}
    }
}
