using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebLanchesMVC.Models
{
    public class Category
    {
        public int Id { get; set; }
		[StringLength(100)]
        public string Name { get; set; }
		[StringLength(200)]
        public string Description { get; set; }
        public List<Lunch> Lunchs { get; set; }
    }
}
