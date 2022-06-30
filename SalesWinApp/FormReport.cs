using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccess.Repository;
using BusinessObject.Models;

namespace SalesWinApp
{
    public partial class FormReport : Form
    {
        IOrderDetailRepository orderDetailRepository;
        IOrderRepository orderRepository;
        BindingSource source;
        public FormReport()
        {
            InitializeComponent();
            orderDetailRepository = new OrderDetailRepository();
            orderRepository = new OrderRepository();  
        }

        private void btnGetReport_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime start = Convert.ToDateTime(dateStart.Text);
                DateTime end = Convert.ToDateTime(dateEnd.Text);
                List<listOrder> listOrders = new List<listOrder>();
                
                List<Order> listOrderInRange = orderRepository.GetOrderInRange(start, end).ToList();
                foreach (Order order in listOrderInRange)
                {
                    listOrder o = new listOrder();
                    listOrders.Add(new listOrder()
                    {
                        orderID = order.OrderId,
                        total = orderDetailRepository.GetInComeOfAOrder(order.OrderId),
                    });


                }

                source = new BindingSource();
                source.DataSource = listOrders;
                dgvOrder.DataSource = source;

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get report");
            }
        }
    }
}

public class listOrder
{
    public int orderID { get; set; }
    public decimal total { get; set; }
}

