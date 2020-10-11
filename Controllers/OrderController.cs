using Microsoft.AspNetCore.Mvc;
using WebLanchesMVC.Models;
using WebLanchesMVC.Repositories;

namespace WebLanchesMVC.Controllers
{
    public class OrderController : Controller
    {
		private readonly IOrderRepository _orderRepository;
		private readonly CartPurchase _cartPurchase;

		public OrderController(IOrderRepository orderRepository, CartPurchase cartPurchase)
		{
			_orderRepository = orderRepository;
			_cartPurchase = cartPurchase;
		}

		public IActionResult Checkout()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Checkout(Order order)
		{
			var items = _cartPurchase.GetCartPurchaseItems();
			_cartPurchase.CartPurchaseItens = items;

			if(_cartPurchase.CartPurchaseItens.Count == 0)
			{
				ModelState.AddModelError("", "Seu carrinho está vazio, inclua um lanche...");
			}

			if(ModelState.IsValid)
			{
				_orderRepository.CreateOrder(order);
				_cartPurchase.ClearCart();
				return RedirectToAction("CheckoutComplete");
			}

			return View(order);
		}

		public IActionResult CheckoutComplete()
		{
			ViewBag.CheckoutCompleteMessage = "Obrigado pelo seu pedido!";

			return View();
		}

    }
}
