using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace DataAccess.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        void Add(Order order);
        void Update(Order order);
        void Delete(Order order);

        int GetMaxID();

        void AddNewOrder(Order order, List<OrderDetail> list);

        void UpdateOrder(Order order, List<OrderDetail> list);

        IEnumerable<Order> GetOrderInRange(DateTime start, DateTime end);

        IEnumerable<Order> GetOrderByMemberId(int member);
    }
}
