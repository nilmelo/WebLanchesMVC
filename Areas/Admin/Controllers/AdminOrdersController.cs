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
using Microsoft.AspNetCore.Routing;
using ReflectionIT.Mvc.Paging;
using WebLanchesMVC.ViewModel;

namespace WebLanchesMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminOrdersController : Controller
    {
        private readonly AppDbContext _context;

        public AdminOrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminOrders
        // public async Task<IActionResult> Index()
        // {
        //     return View(await _context.Orders.ToListAsync());
        // }

		public IActionResult OrderLunches(int? id)
		{
			var order = _context.Orders
							.Include(or => or.OrderItems)
							.ThenInclude(l => l.Lunch)
							.FirstOrDefault(o => o.Id == id);

			if(order == null)
			{
				Response.StatusCode = 404;
				return View("OrderNotFound", id.Value);
			}

			OrderLunchViewModel orderLunches = new OrderLunchViewModel() {
				Order = order,
				OrderDetails = order.OrderItems
			};
			return View(orderLunches);
		}

		public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort ="Name")
		{
			var result = _context.Orders.AsNoTracking()
							.AsQueryable();

			if(!string.IsNullOrWhiteSpace(filter))
			{
				result = result.Where(p => p.Name.Contains(filter));
			}

			var model = await PagingList.CreateAsync(result, 5, pageindex, sort, "Name");
			model.RouteValue = new RouteValueDictionary { { "filter", filter } };

			return View(model);
		}


        // GET: Admin/AdminOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Admin/AdminOrders/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,LastName,Address1,Address2,Cep,State,City,Telephone,Email,OrderSent,OrderDelivered")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Admin/AdminOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,LastName,Address1,Address2,Cep,State,City,Telephone,Email,OrderSent,OrderDelivered")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            return View(order);
        }

        // GET: Admin/AdminOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Admin/AdminOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(m => m.Id == id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
