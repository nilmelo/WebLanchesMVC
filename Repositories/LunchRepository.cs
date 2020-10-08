using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebLanchesMVC.Context;
using WebLanchesMVC.Models;

namespace WebLanchesMVC.Repositories
{
	public class LunchRepository : ILunchRepository
	{
		private readonly AppDbContext _context;

		public LunchRepository(AppDbContext context)
		{
			_context = context;
		}
		public IEnumerable<Lunch> Lunches => _context.Lunches.Include(c => c.Category);

		public IEnumerable<Lunch> LunchesPreferred => _context.Lunches.Where(p => p.IsPreferred).Include(c => c.Category);

		public Lunch GetLunchById(int lunchId)
		{
			return _context.Lunches.FirstOrDefault(l => l.Id == lunchId);
		}
	}
}
