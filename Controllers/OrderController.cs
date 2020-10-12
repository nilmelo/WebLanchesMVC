using Microsoft.AspNetCore.Authorization;
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

		[Authorize]
		public IActionResult Checkout()
		{
			return View();
		}

		[HttpPost]
		[Authorize]
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

				ViewBag.CheckoutCompleteMessage = "Obrigado pelo seu pedido!";
				ViewBag.TotalOrder = _cartPurchase.GetCartPurchaseTotal();

				_cartPurchase.ClearCart();
				return View("~/Views/Order/CheckoutComplete.cshtml", order);
			}

			return View(order);
		}

		// TODO: Verificar se ainda preciso deste método
		public IActionResult CheckoutComplete()
		{
			ViewBag.Client = TempData["Client"];
			ViewBag.DateOrder = TempData["DateOrder"];
			ViewBag.NumberOrder = TempData["NumberOrder"];
			ViewBag.TotalOrder = TempData["TotalOrder"];

			ViewBag.CheckoutCompleteMessage = "Obrigado pelo seu pedido!";

			return View();
		}

    }
}
