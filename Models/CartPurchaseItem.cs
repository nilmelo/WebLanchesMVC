using System.ComponentModel.DataAnnotations;

namespace WebLanchesMVC.Models
{
    public class CartPurchaseItem
    {
        public int Id { get; set; }
		public Lunch Lunch { get; set; }
		public int Quantity { get; set; }
		[StringLength(200)]
		public string CartPurchaseId { get; set; }
    }
}
