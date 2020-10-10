using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebLanchesMVC.Models;
using WebLanchesMVC.ViewModel;

namespace WebLanchesMVC.Component
{
    public class CartPurchaseResume : ViewComponent
    {
		private readonly CartPurchase _cartPurchase;

		public CartPurchaseResume(CartPurchase cartPurchase)
		{
			_cartPurchase = cartPurchase;
		}

		public IViewComponentResult Invoke()
		{
			//var items = _cartPurchase.GetCartPurchaseItems();

			// TESTE Itens no carrinho
			var items = new List<CartPurchaseItem>(){ new CartPurchaseItem(), new CartPurchaseItem() };
			//

			_cartPurchase.CartPurchaseItens = items;

			var cartPurchaseVM = new CartPurchaseViewModel
			{
				CartPurchase = _cartPurchase,
				CartPurchaseTotal = _cartPurchase.GetCartPurchaseTotal()
			};
			return View();
		}

    }
}
