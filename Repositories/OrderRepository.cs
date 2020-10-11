using System;
using WebLanchesMVC.Context;
using WebLanchesMVC.Models;

namespace WebLanchesMVC.Repositories
{
	public class OrderRepository : IOrderRepository
	{
		private readonly AppDbContext _appDbContext;
		private readonly CartPurchase _cartPurchase;

		public OrderRepository(AppDbContext appDbContext, CartPurchase cartPurchase)
		{
			_appDbContext = appDbContext;
			_cartPurchase = cartPurchase;
		}

		public void CreateOrder(Order order)
		{
			order.OrderSent = DateTime.Now;
			_appDbContext.Orders.Add(order);

			var cartPurchaseItems = _cartPurchase.CartPurchaseItens;

			foreach(var cartItem in cartPurchaseItems)
			{
				var orderDetail = new OrderDetail()
				{
					Quantity = cartItem.Quantity,
					Id = cartItem.Lunch.Id,
					OrderId = order.Id,
					Price = cartItem.Lunch.Price
				};
				_appDbContext.OrderDetails.Add(orderDetail);
			}
			_appDbContext.SaveChanges();
		}
	}
}
