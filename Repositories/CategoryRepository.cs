using System.Collections.Generic;
using WebLanchesMVC.Context;
using WebLanchesMVC.Models;

namespace WebLanchesMVC.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly AppDbContext _context;

		public CategoryRepository(AppDbContext context)
		{
			_context = context;
		}
		public IEnumerable<Category> Categories => _context.Categories;
	}
}
