using System.Collections.Generic;

namespace WebLanchesMVC.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Lunch> Lunchs { get; set; }
    }
}