using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace DataAccess.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> SearchProduct(string searchValue);
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);

        void ReduceQuantityInStock(int quantity, int productId);

        IEnumerable<Product> SearchProductByProductID(int productId);
        IEnumerable<Product> SearchProductByUnitPrice(decimal price);
        IEnumerable<Product> SearchProductByUnitsInStock(int quantity);

        void RecoverQuantity(int quantity, int productId);
        int GetMaxID();
    }
}
