using System.Collections.Generic;
using WebLanchesMVC.Models;

namespace WebLanchesMVC.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get;}
    }
}
