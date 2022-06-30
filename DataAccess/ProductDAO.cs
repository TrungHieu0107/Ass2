using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace DataAccess
{
    public class ProductDAO
    {
        private static ProductDAO instance = null; 
        private ProductDAO()
        {

        }
         public static readonly object instanceLock = new object();
        public static ProductDAO Instance
        {
            get
            {
                lock(instanceLock)
                {
                    if(instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Product> GetAll()
        {
            FStoreDBContext context = new FStoreDBContext();
            return from product in context.Products
                        select product;
        }

        public void Insert(Product newProduct)
        {
            try
            {
                using (var context = new FStoreDBContext())
                {
                    context.Products.Add(newProduct);
                    context.SaveChanges();
                }
            } catch (Exception ex)
            {
                throw new Exception("Error in insert product" + ex.Message);
            }
        }

        public void Delete(Product product)
        {
            try
            {
                using (var context = new FStoreDBContext())
                {   
                    var p = context.Products.SingleOrDefault(p => p.ProductId == product.ProductId);
                    context.Products.Remove(p);
                    context.SaveChanges();
                }
            } catch (Exception ex)
            {
                throw new Exception("Error in delete product: " + ex.Message);
            }
        }
        public void Update(Product product)
        {
            try
            {
                using (var context = new FStoreDBContext())
                {
                    context.Products.Update(product);
                    context.SaveChanges();
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in update product: " + ex.Message);
            }
        }

        public int GetMaxID()
        {
            try
            {
                using (var context = new FStoreDBContext())
                {
                    var id = (from p in context.Products
                              orderby p.ProductId descending
                              select p.ProductId).FirstOrDefault();
                    return id;
                }
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Product> SearchProduct(string searchValue)
        {
            try
            {
                FStoreDBContext context = new FStoreDBContext();
                var listProducts = context.Products.Where(x => x.ProductName.Contains(searchValue));
                return listProducts;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Product> SearchProductByID(int productID)
        {
            try
            {
                FStoreDBContext context = new FStoreDBContext();
                var product = context.Products.Where(x => x.ProductId == productID);
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Product> SearchProductByPrice(decimal price)
        {
            try
            {
                FStoreDBContext context = new FStoreDBContext();
                var list = from p in context.Products
                           where p.UnitPrice < price
                           orderby p.UnitPrice descending
                           select p;
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Product> SearchProductByUnitsInStock(int quantity)
        {
            try
            {
                FStoreDBContext context = new FStoreDBContext();
                var list = from p in context.Products
                           where p.UnitsInStock < quantity
                           orderby p.UnitsInStock ascending
                           select p;
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ReduceQuantityInStock(int quantity, int productId)
        {
            try
            {
                using(var context = new FStoreDBContext())
                {
                    Product p = context.Products.SingleOrDefault(p => p.ProductId.Equals(productId));
                    if(p != null)
                    {
                        p.UnitsInStock -= quantity;
                    }
                    context.Products.Update(p);
                    //context.SaveChanges();
                }
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RecoverQuantity(int quantity, int proId)
        {
            try
            {
                using (var context = new FStoreDBContext())
                {
                    var product = context.Products.Single(x => x.ProductId ==  proId);
                    if(product != null)
                    {
                        product.UnitsInStock += quantity;
                    }
                    context.Products.Update(product);
                    context.SaveChanges();
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       
    }
}
