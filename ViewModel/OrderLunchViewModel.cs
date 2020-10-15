using System.Collections.Generic;
using WebLanchesMVC.Models;

namespace WebLanchesMVC.ViewModel
{
    public class OrderLunchViewModel
    {
        public Order Order { get; set; }
		public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
