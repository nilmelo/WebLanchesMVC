using System.Collections.Generic;
using WebLanchesMVC.Models;

namespace WebLanchesMVC.Repositories
{
    public interface ILunchRepository
    {
         IEnumerable<Lunch> Lunches { get;}
		 IEnumerable<Lunch> LunchesPreferred { get;}
		 Lunch GetLunchById(int lunchId);
    }
}
