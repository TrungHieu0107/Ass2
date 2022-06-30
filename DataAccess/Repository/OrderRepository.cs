using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {
        void IOrderRepository.Add(Order order) => OrderDAO.Instance.Insert(order);

        void IOrderRepository.AddNewOrder(Order order, List<OrderDetail> list)
            => OrderDAO.Instance.AddNewOrder(order, list);

        void IOrderRepository.Delete(Order order) => OrderDAO.Instance.Delete(order);

        IEnumerable<Order> IOrderRepository.GetAll() => OrderDAO.Instance.GetAll();

        int IOrderRepository.GetMaxID() => OrderDAO.Instance.GetMaxID();

        IEnumerable<Order> IOrderRepository.GetOrderByMemberId(int member) => OrderDAO.Instance.GetOrderByMemberId(member);

        IEnumerable<Order> IOrderRepository.GetOrderInRange(DateTime start, DateTime end)
            => OrderDAO.Instance.GetOrderInRange(start, end);

        void IOrderRepository.Update(Order order) => OrderDAO.Instance.Update(order);

        void IOrderRepository.UpdateOrder(Order order, List<OrderDetail> list) => OrderDAO.Instance.UpdateOrder(order, list);


    }
}
