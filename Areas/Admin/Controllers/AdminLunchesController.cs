using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebLanchesMVC.Context;
using WebLanchesMVC.Models;
using Microsoft.AspNetCore.Authorization;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Routing;

namespace WebLanchesMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminLunchesController : Controller
    {
        private readonly AppDbContext _context;

        public AdminLunchesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminLunches
        // public async Task<IActionResult> Index()
        // {
        //     var appDbContext = _context.Lunches.Include(l => l.Category);
        //     return View(await appDbContext.ToListAsync());
        // }

		public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Name")
        {
            var result = _context.Lunches.Include(l => l.Category)
                           .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                result = result.Where(p => p.Name.Contains(filter));
            }

            var model = await PagingList.CreateAsync(result, 5, pageindex, sort, "Name");

            model.RouteValue = new RouteValueDictionary { { "filter", filter } };

            return View(model);
        }

        // GET: Admin/AdminLunches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lunch = await _context.Lunches
                .Include(l => l.Category)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (lunch == null)
            {
                return NotFound();
            }

            return View(lunch);
        }

        // GET: Admin/AdminLunches/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DescriptionShort,DescriptionDetail,Price,ImageURL,ImageThumbnailURL,IsPreferred,InStock,CategoryId")] Lunch lunch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lunch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Categories, "Id", "Name", lunch.Id);
            return View(lunch);
        }

        // GET: Admin/AdminLunches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lunch = await _context.Lunches.SingleOrDefaultAsync(m => m.Id == id);
            if (lunch == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Categories, "Id", "Name", lunch.Id);
            return View(lunch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DescriptionShort,DescriptionDetail,Price,ImageURL,ImageThumbnailURL,IsPreferred,InStock,CategoryId")] Lunch lunch)
        {
            if (id != lunch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lunch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LunchExists(lunch.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Categories, "CategoriaId", "Name", lunch.Id);
            return View(lunch);
        }

        // GET: Admin/AdminLunches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lanche = await _context.Lunches
                .Include(l => l.Category)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (lanche == null)
            {
                return NotFound();
            }

            return View(lanche);
        }

        // POST: Admin/AdminLunches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lunch = await _context.Lunches.SingleOrDefaultAsync(m => m.Id == id);
            _context.Lunches.Remove(lunch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LunchExists(int id)
        {
            return _context.Lunches.Any(e => e.Id == id);
        }
    }
}
