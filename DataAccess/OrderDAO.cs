
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace DataAccess
{
    public class OrderDAO
    {
        private static OrderDAO instance = null;
        private static readonly object instanceLock = new object();
        private OrderDAO() { }
        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Order> GetAll()
        {
            FStoreDBContext fStoreDBContext = new FStoreDBContext();
            var orderList = from member in fStoreDBContext.Orders select member;
            return orderList;

        }

        public void Insert(Order order)
        {
            try
            {
                using FStoreDBContext fStoreDBContext = new FStoreDBContext();
                fStoreDBContext.Orders.Add(order);
                fStoreDBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Insert Error : " + ex.Message);
            }

        }

        public void AddNewOrder(Order newOrder, List<OrderDetail> list)
        {
            try
            {
                using (var fStoreDBContext = new FStoreDBContext())
                {
                    //1. Add new order
                    fStoreDBContext.Orders.Add(newOrder);
                    //2. Add order detail
                    foreach (var item in list)
                    {
                        fStoreDBContext.OrderDetails.Add(item);
                    }
                    //3. Reduce quantity
                    foreach (var item in list)
                    {
                        ProductDAO.Instance.ReduceQuantityInStock(item.Quantity, item.ProductId);
                    }
                    fStoreDBContext.SaveChanges(true);

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void UpdateOrder(Order order, List<OrderDetail>  listNewOrderDetail)
        {
            try
            {
                using (var fStoreDBContext = new FStoreDBContext())
                {
                    //Update order
                    fStoreDBContext.Orders.Update(order);
                    //Update order detial
                    foreach(var item in listNewOrderDetail)
                    {
                        fStoreDBContext.OrderDetails.Add(item);
                    }
                    //3. Reduce quantity
                    foreach (var item in listNewOrderDetail)
                    {
                        ProductDAO.Instance.ReduceQuantityInStock(item.Quantity, item.ProductId);
                    }
                    fStoreDBContext.SaveChanges();
                }

            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Update(Order order)
        {
            try
            {
                using (var fStoreDBContext = new FStoreDBContext())
                {
                    fStoreDBContext.Orders.Update(order);
                    fStoreDBContext.SaveChanges();
                };

            }
            catch (Exception ex)
            {
                throw new Exception("Update Order: " + ex.Message);
            }
        }

        public void Delete(Order order)
        {
            try
            {
                using FStoreDBContext fStoreDBContext = new FStoreDBContext();
                {
                    var o = fStoreDBContext.Orders.SingleOrDefault(m => m.OrderId == order.OrderId);
                    fStoreDBContext.Orders.Remove(o);

                    fStoreDBContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in delete order: " + ex.Message);
            }
        }

        public int GetMaxID()
        {
            try
            {
                using (var dbContext = new FStoreDBContext())
                {
                    var id = (from order in dbContext.Orders
                              orderby order.OrderId descending
                              select order.OrderId).FirstOrDefault();
                    return id;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Order> GetOrderInRange(DateTime start, DateTime end)
        {

            try
            {
                FStoreDBContext fStoreDBContext = new FStoreDBContext();
                var list = fStoreDBContext.Orders.Where(order => order.OrderDate >= start && order.OrderDate <= end);
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Order> GetOrderByMemberId(int memberId)
        {
            try
            {
                FStoreDBContext fStoreDBContext= new FStoreDBContext();
                var list = fStoreDBContext.Orders.Where(order => order.MemberId == memberId);
                return list;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
