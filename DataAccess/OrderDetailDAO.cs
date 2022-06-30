using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace DataAccess
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO instance;
        private static readonly object instanceLock = new object();
        private OrderDetailDAO() { }
        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if(instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            FStoreDBContext context = new FStoreDBContext();
            return from orderDetail in context.OrderDetails
                   select orderDetail;
        }

        public void Insert(OrderDetail newOrder)
        { 
            try
            {
                FStoreDBContext fStoreDBContext = new FStoreDBContext();
                fStoreDBContext.OrderDetails.Add(newOrder);
                //fStoreDBContext.SaveChanges();
            } catch (Exception ex)
            {
                throw new Exception("Error in insert order detail: " + ex.Message);
            }
        }public void Update(OrderDetail newOrder)
        { 
            try
            {
                using FStoreDBContext fStoreDBContext = new FStoreDBContext();
                fStoreDBContext.OrderDetails.Update(newOrder);
                fStoreDBContext.SaveChanges();
            } catch (Exception ex)
            {
                throw new Exception("Error in upadate order detail: " + ex.Message);
            }
        }public void Delete(OrderDetail order)
        { 
            try
            {
                using FStoreDBContext fStoreDBContext = new FStoreDBContext();
                var o = fStoreDBContext.OrderDetails.SingleOrDefault(o => o.OrderId == order.OrderId);
                fStoreDBContext.OrderDetails.Remove(o);
                fStoreDBContext.SaveChanges();
            } catch (Exception ex)
            {
                throw new Exception("Error in delete order detail: " + ex.Message);
            }
        }

        public IEnumerable<OrderDetail> GetOrderDetailByOrderID(int orderID)
        {
            try
            {
                FStoreDBContext fStoreDBContext = new FStoreDBContext();
                var list = fStoreDBContext.OrderDetails.Where(x => x.OrderId == orderID);
                
                return list;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        //Hàm trả về số lượng product cos trong list order 
        public int SumQuantity(int productId, List<int> orderId)
        {
            try
            {
                using (var context = new FStoreDBContext())
                {
                    int sum = 0;
                    foreach (var item in orderId)
                    {
                        var list = context.OrderDetails.Where(p => p.ProductId.Equals(productId) && p.OrderId.Equals(orderId));
                        
                        sum += list.Sum(x => x.Quantity);
                    }
                    return sum;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public decimal InComeOfAOrder(int orderId)
        {
            decimal total = 0;
            try
            {
                using (FStoreDBContext context = new FStoreDBContext())
                { 
                    var list = context.OrderDetails.Where(x => x.OrderId == orderId).ToList();
                    foreach (var item in list)
                    {
                        total += item.UnitPrice * item.Quantity * (decimal)(100 - item.Discount) / 100;
                    }
                    return total;
                }
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
    }
}
