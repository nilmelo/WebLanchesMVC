using WebLanchesMVC.Models;

namespace WebLanchesMVC.Repositories
{
    public interface IOrderRepository
    {
		void CreateOrder(Order order);
    }
}
