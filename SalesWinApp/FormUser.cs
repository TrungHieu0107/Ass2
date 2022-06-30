using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessObject.Models;
using DataAccess.Repository;
using SalesWinApp.Form_Update_and_Insert;


namespace SalesWinApp
{
    public partial class FormUser : Form
    {
        IMemberRepository memberRepository;
        IOrderRepository orderRepository;
        IOrderDetailRepository orderDetailRepository;
        public string Email { get; set; }

        BindingSource source;

        Member member;
        public FormUser()
        {
            InitializeComponent();
            memberRepository = new MemberRepository();  
            orderRepository = new OrderRepository();
            orderDetailRepository = new OrderDetailRepository();
        }

        private void FormUser_Load(object sender, EventArgs e)
        {
            LoadMember();
        }

        public void LoadOrder(int memberId)
        {
            try
            {
                List<Order> orders = new List<Order>();
                orders = orderRepository.GetOrderByMemberId(memberId).ToList();
                source = new BindingSource();
                source.DataSource = orders;
                dgvOrder.DataSource = source;
                if (dgvOrder.Columns["Member"] != null)
                {
                    dgvOrder.Columns["Member"].Visible = false;
                }
                if (dgvOrder.Columns["OrderDetails"] != null)
                {
                    dgvOrder.Columns["OrderDetails"].Visible = false;
                }
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Order OF User", MessageBoxButtons.OK ,MessageBoxIcon.Error);
            }
            
        }

        public void LoadMember()
        {
            member = memberRepository.GetMemberByEmail(Email);

            txtMemberId.Enabled = false;
            txtMemberId.Text = member.MemberId.ToString();
            txtPassword.Text = member.Password.ToString();
            txtEmail.Text = member.Email.ToString();
            txtCompanyName.Text = member.CompanyName.ToString();
            txtCountry.Text = member.Country.ToString();
            txtCity.Text = member.City.ToString();
        }


        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            if (txtMemberId.Text.Length == 0)
            {
                return;
            }
            Member mem = new Member()
            {
                MemberId = int.Parse(txtMemberId.Text),
                Email = txtEmail.Text,
                CompanyName = txtCompanyName.Text,
                City = txtCity.Text,
                Country = txtCountry.Text,
                Password = txtPassword.Text,
            };

            FormMemberInsertOrUpdate formMemberInsertOrUpate = new FormMemberInsertOrUpdate()
            {
                Text = "Update",
                MemberRepository = memberRepository,
                InsertOrUpdate = false,
                MemberObject = member,
            };

            if (formMemberInsertOrUpate.ShowDialog() == DialogResult.OK)
            {
                formMemberInsertOrUpate.Close();
                LoadMember();
            }
        }

        private void btnLoadOrder_Click(object sender, EventArgs e)
        {
            LoadOrder(int.Parse(txtMemberId.Text));
            
        }

        private void dgvOrder_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int orderId = int.Parse(dgvOrder.Rows[e.RowIndex].Cells["OrderId"].FormattedValue.ToString());
                List<OrderDetail> orderDetails = new List<OrderDetail>();
                orderDetails = orderDetailRepository.GetOrderDetailsByOrderID(orderId).ToList();
                source = new BindingSource();
                source.DataSource = orderDetails;
                dgvOrderDetail.DataSource = null;
                dgvOrderDetail.DataSource = source;

                if (dgvOrderDetail.Columns["Order"] != null)
                {
                    dgvOrderDetail.Columns["Order"].Visible = false;
                }
                if (dgvOrderDetail.Columns["Product"] != null)
                {
                    dgvOrderDetail.Columns["Product"].Visible = false;
                }


            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Order Detail Of User", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
