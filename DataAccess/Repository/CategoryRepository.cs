using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace DataAccess.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        IEnumerable<int> ICategoryRepository.GetAllCategoryID() => CategoryDAO.Instance.GetAllCategoryID();

        IEnumerable<string> ICategoryRepository.GetAllCategoryName() => CategoryDAO.Instance.GetAllCategoryName();

        int ICategoryRepository.GetCategoryByName(string categoryName)
            => CategoryDAO.Instance.GetCategoryByName(categoryName); 
    }
}
