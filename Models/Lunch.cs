using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebLanchesMVC.Models
{
    public class Lunch
    {
        public int Id { get; set; }

		[StringLength(100)]
        public string Name { get; set; }
		[StringLength(100)]
        public string DescriptionShort { get; set; }
		[StringLength(255)]
        public string DescriptionDetail { get; set; }

		[Column(TypeName="decimal(18,2)")]
        public decimal Price { get; set; }
		[StringLength(200)]
        public string ImageURL { get; set; }
		[StringLength(200)]
        public string ImageThumbnailURL { get; set; }
        public bool IsPreferred { get; set; }
        public bool InStock { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
