using System.Collections.Generic;
using WebLanchesMVC.Models;

namespace WebLanchesMVC.ViewModel
{
    public class LunchListViewModel
    {
        public IEnumerable<Lunch> Lunches { get; set; }
		public string CategoryCurrent { get; set; }
    }
}
