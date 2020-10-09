using System.Collections.Generic;
using WebLanchesMVC.Models;

namespace WebLanchesMVC.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<Lunch> LunchesPreferred { get; set; }
    }

}
