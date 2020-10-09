using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebLanchesMVC.Context;

namespace WebLanchesMVC.Models
{
    public class CartPurchase
    {
		private readonly AppDbContext _context;

		public string Id { get; set; }
		public List<CartPurchaseItem> CartPurchaseItens { get; set; }

		public CartPurchase(AppDbContext context)
		{
			_context = context;
		}

		public static CartPurchase GetCart(IServiceProvider services)
		{
			// Define uma sessão acessando o contexto atual(tem que registrar em IServicesCollection)
			ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

			// Obtém um serviço do tipo do nosso contexto
			AppDbContext context = (AppDbContext)services.GetServices<AppDbContext>();

			// Obtém ou gera o Id do carrinho
			string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

			// Atribui o id do carrinho na sessão
			session.SetString("CartId", cartId);

			// Retorna o carrinho com o contexto atual e o Id atribuído ou obtido
			return new CartPurchase(context)
			{
				Id = cartId
			};
		}

		public void AddInCart(Lunch lunch, int quantity)
		{
			var cartPurchaseItem =
				_context.CartPurchaseItens.SingleOrDefault(
					s => s.Lunch.Id == lunch.Id && s.Id.ToString() == Id
				);

			// Verifica se o carriho existe e se não existir cria um
			if(cartPurchaseItem == null)
			{
				cartPurchaseItem = new CartPurchaseItem
				{
					Id = int.Parse(Id),
					Lunch = lunch,
					Quantity = 1
				};
				_context.CartPurchaseItens.Add(cartPurchaseItem);
			}
			else  // Se existir o carrinho com o item, então incrementa a quantidade
			{
				cartPurchaseItem.Quantity++;
			}
			_context.SaveChanges();
		}

		public int RemoveFromCart(Lunch lunch)
		{
			var cartPurchaseItem =
				_context.CartPurchaseItens.SingleOrDefault(
					s => s.Lunch.Id == lunch.Id && s.Id.ToString() == Id
				);

			var quantityLocal = 0;

			if(cartPurchaseItem != null)
			{
				if(cartPurchaseItem.Quantity > 1)
				{
					cartPurchaseItem.Quantity--;
					quantityLocal = cartPurchaseItem.Quantity;
				}
				else
				{
					_context.CartPurchaseItens.Remove(cartPurchaseItem);
				}
			}

			_context.SaveChanges();

			return quantityLocal;
		}

		public List<CartPurchaseItem> GetCartPurchaseItems()
		{
			return CartPurchaseItens ??
				(CartPurchaseItens =
					_context.CartPurchaseItens.Where(c => c.Id.ToString() == Id)
						.Include(s => s.Lunch)
						.ToList()
				);
		}

		public void ClearCart()
		{
			var cartItems = _context.CartPurchaseItens
								.Where(cart => cart.Id.ToString() == Id);

			_context.CartPurchaseItens.RemoveRange(cartItems);
			_context.SaveChanges();
		}

		public decimal GetCartPurchaseTotal()
		{
			var total = _context.CartPurchaseItens.Where(c => c.Id.ToString() == Id)
				.Select(c => c.Lunch.Price * c.Quantity).Sum();

			return total;
		}

    }
}
