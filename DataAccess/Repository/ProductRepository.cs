using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {

        void IProductRepository.Add(Product product) => ProductDAO.Instance.Insert(product);

        void IProductRepository.Delete(Product product) => ProductDAO.Instance.Delete(product);

        IEnumerable<Product> IProductRepository.GetAll() => ProductDAO.Instance.GetAll();

        int IProductRepository.GetMaxID() => ProductDAO.Instance.GetMaxID();

        void IProductRepository.RecoverQuantity(int quantity, int productId) => ProductDAO.Instance.RecoverQuantity(quantity, productId);

        void IProductRepository.ReduceQuantityInStock(int quantity, int productId)
            => ProductDAO.Instance.ReduceQuantityInStock(quantity, productId);
        IEnumerable<Product> IProductRepository.SearchProduct(string searchValue) 
                => ProductDAO.Instance.SearchProduct(searchValue);

        IEnumerable<Product> IProductRepository.SearchProductByProductID(int productId)
            => ProductDAO.Instance.SearchProductByID(productId);

        IEnumerable<Product> IProductRepository.SearchProductByUnitPrice(decimal price) 
            => ProductDAO.Instance.SearchProductByPrice(price);

        IEnumerable<Product> IProductRepository.SearchProductByUnitsInStock(int quantity)
        => ProductDAO.Instance.SearchProductByUnitsInStock(quantity);

        void IProductRepository.Update(Product product) => ProductDAO.Instance.Update(product);
    }
}
