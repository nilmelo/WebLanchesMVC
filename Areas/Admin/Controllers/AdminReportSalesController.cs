using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebLanchesMVC.Areas.Admin.Services;

namespace WebLanchesMVC.Areas.Admin.Controllers
{
    public class AdminReportSalesController : Controller
    {
		private readonly ReportSalesService _reportSalesService;

		public AdminReportSalesController(ReportSalesService reportSalesService)
		{
			_reportSalesService = reportSalesService;
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> ReportSalesSingles(DateTime? minDate, DateTime? maxDate)
		{
			if(!minDate.HasValue)
			{
				minDate = new DateTime(DateTime.Now.Year, 1, 1);
			}

			if(!maxDate.HasValue)
			{
				maxDate = DateTime.Now;
			}

			ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
			ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

			var result = await _reportSalesService.FindByDateAsync(minDate, maxDate);

			return View(result);

		}

    }
}
