using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        void IOrderDetailRepository.Add(OrderDetail orderDetail) => OrderDetailDAO.Instance.Insert(orderDetail);

        void IOrderDetailRepository.Delete(OrderDetail orderDetail) => OrderDetailDAO.Instance.Delete(orderDetail);

        IEnumerable<OrderDetail> IOrderDetailRepository.GetAll() => OrderDetailDAO.Instance.GetAll();

        decimal IOrderDetailRepository.GetInComeOfAOrder(int orderId)
            => OrderDetailDAO.Instance.InComeOfAOrder(orderId);

        IEnumerable<OrderDetail> IOrderDetailRepository.GetOrderDetailsByOrderID(int orderId) 
                => OrderDetailDAO.Instance.GetOrderDetailByOrderID(orderId);

        void IOrderDetailRepository.Update(OrderDetail orderDetail) => OrderDetailDAO.Instance.Update(orderDetail);
    }
}
