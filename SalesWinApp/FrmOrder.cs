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
using SalesWinApp.FormUpdateAndInsert;

namespace SalesWinApp
{
    public partial class FrmOrder : Form
    {
        IOrderRepository orderRepository;
        BindingSource source;
        public FrmOrder()
        {
            InitializeComponent();
            orderRepository = new OrderRepository();

        }

        private void FrmOrder_Load(object sender, EventArgs e)
        {
            //LoadData(GetAllOrder());
            txtFreight.Enabled = false;
            txtMemberID.Enabled = false;
            txtOrderDate.Enabled = false;
            txtOrderID.Enabled = false;
            txtReuiredDate.Enabled = false;
            txtShippedDate.Enabled = false;

            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            LoadData(GetAllOrder());
        }

        private void button1_Click(object sender, EventArgs e) // btnLoad
        {
            LoadData(GetAllOrder());
            txtFreight.Enabled = false;
            txtMemberID.Enabled = false;
            txtOrderDate.Enabled = false;
            txtOrderID.Enabled = false;
            txtReuiredDate.Enabled = false;
            txtShippedDate.Enabled = false;

            
        }

        private IEnumerable<Order> GetAllOrder()
        {
            return orderRepository.GetAll().ToList();
        }

        private void ClearText()
        {
            txtFreight.Text = string.Empty;
            txtMemberID.Text = string.Empty;
            txtOrderDate.Text = string.Empty;
            txtOrderID.Text = string.Empty;
            txtReuiredDate.Text = string.Empty;
            txtShippedDate.Text = string.Empty;
        }

        private void LoadData(IEnumerable<Order> list)
        {
            try
            {
                source = new BindingSource();
                source.DataSource = list;

                txtOrderID.DataBindings.Clear();
                txtMemberID.DataBindings.Clear();
                txtOrderDate.DataBindings.Clear();
                txtReuiredDate.DataBindings.Clear();
                txtShippedDate.DataBindings.Clear();
                txtFreight.DataBindings.Clear();

                txtOrderID.DataBindings.Add("Text", source, "OrderID");
                txtMemberID.DataBindings.Add("Text", source, "MemberId");
                txtOrderDate.DataBindings.Add("Text", source, "OrderDate");
                txtReuiredDate.DataBindings.Add("Text", source, "RequiredDate");
                txtShippedDate.DataBindings.Add("Text", source, "ShippedDate");
                txtFreight.DataBindings.Add("Text", source, "Freight");

                dgvOrder.DataSource = source;

                btnNew.Text = "New";
                btnUpdate.Text = "Update";
                if(dgvOrder.Columns["Member"] != null)
                {
                    dgvOrder.Columns["Member"].Visible = false;
                }
                if(dgvOrder.Columns["OrderDetails"] != null)
                {
                    dgvOrder.Columns["OrderDetails"].Visible = false;
                }

                if(list.Count() > 0)
                {
                    btnDelete.Enabled = true;
                    btnUpdate.Enabled = true;
                } else
                {
                    btnDelete.Enabled = false;
                    btnUpdate.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load failed");
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            dgvOrderDetail formOrderInsertOrUpdate = new dgvOrderDetail()
            {
                OrderRepository = orderRepository,
                IsInsert = true,
                Text = "Add new order"
            };

            if(formOrderInsertOrUpdate.ShowDialog() == DialogResult.OK)
            {
                formOrderInsertOrUpdate.Close();
                LoadData(GetAllOrder().ToList());
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            Order order = new Order()
            {
                OrderId = int.Parse(txtOrderID.Text),
                OrderDate = DateTime.Parse(txtOrderDate.Text),
                RequiredDate = DateTime.Parse(txtReuiredDate.Text),
                Freight = decimal.Parse(txtFreight.Text),
                ShippedDate = DateTime.Parse(txtShippedDate.Text),
                MemberId = int.Parse(txtMemberID.Text),
            };
            
            dgvOrderDetail formOrderInsertOrUpdate = new dgvOrderDetail()
            {
                OrderRepository = orderRepository,
                IsInsert = false,
                Text = "Update order",
                Order = order,
            };

            if(formOrderInsertOrUpdate.ShowDialog() == DialogResult.OK)
            {
                formOrderInsertOrUpdate.Close();
                LoadData(GetAllOrder());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure to delete this order?", "Delete order",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if(dialogResult != DialogResult.OK)
            {
                return;
            }
            if (txtOrderID.Text.Length != 0)
            {
                try
                {
                    Order order = new Order()
                    {
                        OrderId = int.Parse(txtOrderID.Text),
                    };
                    orderRepository.Delete(order);
                    LoadData(GetAllOrder());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            } else
            {
                throw new Exception("You must choose something to delete");
            }
        }

        
    }
}
