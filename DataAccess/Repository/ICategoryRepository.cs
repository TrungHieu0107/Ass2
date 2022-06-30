using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<string> GetAllCategoryName();
        int GetCategoryByName(string categoryName);

        IEnumerable<int> GetAllCategoryID();
    }
}
