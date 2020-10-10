namespace WebLanchesMVC.Models
{
    public class OrderDetail
    {
		public int Id { get; set; }
		public int OrderId { get; set; }
		public int LunchId { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }

		public virtual Lunch Lunch { get; set; }
		public virtual Order Order { get; set; }
    }
}
