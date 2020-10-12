using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebLanchesMVC.Models;
using WebLanchesMVC.Repositories;
using WebLanchesMVC.ViewModel;

namespace WebLanchesMVC.Controllers
{
    public class CartPurchaseController : Controller
    {
		private readonly ILunchRepository _lunchRepository;
		private readonly CartPurchase _cartPurchase;

		public CartPurchaseController(ILunchRepository lunchRepository, CartPurchase cartPurchase)
		{
			_lunchRepository = lunchRepository;
			_cartPurchase = cartPurchase;
		}

		public IActionResult Index()
		{
			var items = _cartPurchase.GetCartPurchaseItems();
			_cartPurchase.CartPurchaseItens = items;

			var cartPurchaseViewModel = new CartPurchaseViewModel
			{
				CartPurchase = _cartPurchase,
				CartPurchaseTotal = _cartPurchase.GetCartPurchaseTotal()
			};

			return View(cartPurchaseViewModel);
		}

		[Authorize]
		public RedirectToActionResult AddItemInCartPurchase(int lunchId)
		{
			var lunchSelected = _lunchRepository.Lunches.FirstOrDefault(p => p.Id == lunchId);

			if(lunchSelected != null)
			{
				_cartPurchase.AddInCart(lunchSelected, 1);
			}
			return RedirectToAction("Index");
		}

		[Authorize]
		public IActionResult RemoveItemFromCartPurchase(int lunchId)
		{
			var lunchSelected = _lunchRepository.Lunches.FirstOrDefault(p => p.Id == lunchId);

			if(lunchSelected != null)
			{
				_cartPurchase.RemoveFromCart(lunchSelected);
			}
			return RedirectToAction("Index");
		}

    }
}
