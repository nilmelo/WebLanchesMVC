using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebLanchesMVC.Context;
using WebLanchesMVC.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebLanchesMVC.Areas.Admin.Services
{
    public class ReportSalesService
    {
        private readonly AppDbContext _context;
		public ReportSalesService(AppDbContext context)
		{
			_context = context;
		}

		public async Task<List<Order>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
		{
			var result = from obj in _context.Orders select obj;

			if(minDate.HasValue)
			{
				result = result.Where(x => x.OrderSent >= minDate.Value);
			}

			if(maxDate.HasValue)
			{
				result = result.Where(x => x.OrderSent <= maxDate.Value);
			}

			return await result
							.Include(l => l.OrderItems)
							.ThenInclude(l => l.Lunch)
							.OrderByDescending(x => x.OrderSent)
							.ToListAsync();

		}

    }
}
