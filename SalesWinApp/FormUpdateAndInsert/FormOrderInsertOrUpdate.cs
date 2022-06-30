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

namespace SalesWinApp.FormUpdateAndInsert
{
    public partial class dgvOrderDetail : Form
    {
        public IOrderRepository OrderRepository { get; set; }
        public bool IsInsert { get; set; }
        public Order Order { get; set; }

        BindingSource source;

        private static Product Product { get; set; }
        private List<OrderDetail> listOrderDetail { get; set; }
        private List<OrderDetail> listOrderDetailNew { get; set; }

        public IProductRepository ProductRepository { get; set; }


        public IOrderDetailRepository OrderDetailRepository { get; set; }
        public dgvOrderDetail()
        {
            InitializeComponent();
            ProductRepository = new ProductRepository();
            OrderDetailRepository = new OrderDetailRepository();
            listOrderDetail = new List<OrderDetail>();
            if (!IsInsert)
            {
                listOrderDetailNew = new List<OrderDetail>();
            }
        }

        private void FormOrderDetail_Load(object sender, EventArgs e)
        {
            txtQuantity.Enabled = false;
            txtDiscount.Enabled = false;
            if (IsInsert)
            {
                btnInsertOrUpdate.Text = "Add new order";
                txtOrderID.Enabled = false;
                txtOrderID.Text = (OrderRepository.GetMaxID() + 1).ToString();
            }
            else
            {

                listOrderDetail = OrderDetailRepository.GetOrderDetailsByOrderID(Order.OrderId).ToList();

                LoadListOrderDetail();

                txtFreight.Text = Order.Freight.ToString();
                txtMemberID.Text = Order.MemberId.ToString();
                txtReuiredDate.Text = Order.RequiredDate.ToString();
                txtShippedDate.Text = Order.ShippedDate.ToString();
                txtOrderID.Text = Order.OrderId.ToString();
                txtOrderDate.Text = Order.OrderDate.ToString();

                txtOrderID.Enabled = false;
                btnInsertOrUpdate.Text = "Update Order";
            }
        }

        private void btnInsertOrUpdate_Click(object sender, EventArgs e)
        {
            Order newOrder = new Order()
            {
                OrderId = int.Parse(txtOrderID.Text),
                OrderDate = DateTime.Parse(txtOrderDate.Text),
                RequiredDate = DateTime.Parse(txtReuiredDate.Text),
                Freight = decimal.Parse(txtFreight.Text),
                ShippedDate = DateTime.Parse(txtShippedDate.Text),
                MemberId = int.Parse(txtMemberID.Text),
            };
            if (IsInsert)
            {
                try
                {
                    OrderRepository.AddNewOrder(newOrder, listOrderDetail);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Add new order");
                    DialogResult = DialogResult.None;
                }
            }
            else
            {
                try
                {
                    OrderRepository.UpdateOrder(newOrder, listOrderDetailNew);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Update order");
                }

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearchValue.Text.Length > 0)
            {
                try
                {
                    string searchValue = txtSearchValue.Text;
                    var listResult = ProductRepository.SearchProduct(searchValue).ToList();
                    LoadProductList(listResult);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Search product");
                }
            }

        }

        public void LoadProductList(List<Product> list)
        {
            try
            {
                source = new BindingSource();
                source.DataSource = list;
                dgvProductSearch.DataSource = list;
                if (dgvProductSearch.Columns["ProductId"] != null)
                {
                    dgvProductSearch.Columns["ProductId"].Visible = false;
                }

                if (dgvProductSearch.Columns["CategoryId"] != null)
                {
                    dgvProductSearch.Columns["CategoryId"].Visible = false;
                }

                if (dgvProductSearch.Columns["Category"] != null)
                {
                    dgvProductSearch.Columns["Category"].Visible = false;
                }
                if (dgvProductSearch.Columns["OrderDetails"] != null)
                {
                    dgvProductSearch.Columns["OrderDetails"].Visible = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Search Product");
            }
        }

        public void LoadListOrderDetail()
        {
            if (listOrderDetail.Count() > 0)
            {
                try
                {
                    source = new BindingSource();
                    source.DataSource = listOrderDetail;

                    dgvOrderProductDetail.DataSource = source;
                    if(dgvOrderProductDetail.Columns["Order"] != null)
                    {
                        dgvOrderProductDetail.Columns["Order"].Visible=false;
                    }
                    if (dgvOrderProductDetail.Columns["Product"] != null)
                    {
                        dgvOrderProductDetail.Columns["Product"].Visible = false;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Load list order detail");
                }
            } else
            {
                dgvOrderProductDetail.DataSource = null;
            }

        }

        private void dgvProductSearch_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtProductName.Text = String.Empty;
            txtQuantity.TabIndex = 0;
            txtDiscount.Text = String.Empty;
            txtQuantity.Enabled = true;
            txtDiscount.Enabled = true;
            if (dgvProductSearch.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                
                Product = new Product()
                {
                    ProductId = int.Parse(dgvProductSearch.Rows[e.RowIndex].Cells["ProductId"].FormattedValue.ToString()),
                    ProductName = dgvProductSearch.Rows[e.RowIndex].Cells["ProductName"].FormattedValue.ToString(),
                    CategoryId = int.Parse(dgvProductSearch.Rows[e.RowIndex].Cells["CategoryId"].FormattedValue.ToString()),
                    UnitPrice = decimal.Parse(dgvProductSearch.Rows[e.RowIndex].Cells["UnitPrice"].FormattedValue.ToString()),
                    UnitsInStock = int.Parse(dgvProductSearch.Rows[e.RowIndex].Cells["UnitsInStock"].FormattedValue.ToString()),
                    Weight = dgvProductSearch.Rows[e.RowIndex].Cells["Weight"].FormattedValue.ToString(),
                };

                txtProductName.Text = dgvProductSearch.Rows[e.RowIndex].Cells["ProductName"].FormattedValue.ToString();
            }

        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            if(int.Parse(txtQuantity.Text) > 0 && txtProductName.Text.Length != 0)
            {
                //kiểm tra trong list đã có sản phẩm đó chưa 
                //nếu có thì return không add
                foreach (OrderDetail o in listOrderDetail)
                {
                    if (o.ProductId == Product.ProductId)
                    {
                        MessageBox.Show("This product is already exist");
                        return;
                    }
                }
                OrderDetail orderDetail = new OrderDetail()
                {
                    Quantity = int.Parse(txtQuantity.Text),
                    Discount = Convert.ToDouble(txtDiscount.Value),
                    ProductId = Product.ProductId,
                    OrderId = int.Parse(txtOrderID.Text),
                    UnitPrice = Product.UnitPrice,

                };
                if (IsInsert)
                {
                    listOrderDetail.Add(orderDetail);
                } else
                {
                    listOrderDetail.Add(orderDetail);
                    listOrderDetailNew.Add(orderDetail);
                }

                LoadListOrderDetail();
            }
             else
            {
                MessageBox.Show("You must choose product and input quantity", "Error add", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void dgvOrderProductDetail_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to delete this product?", "Delete product", MessageBoxButtons.OKCancel
                , MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                int id = int.Parse(dgvOrderProductDetail.Rows[e.RowIndex].Cells["ProductId"].FormattedValue.ToString());
                
                OrderDetail orderDetail = listOrderDetail.FirstOrDefault(o => o.ProductId == id);

                //listOrderDetail.Remove(orderDetail);
                if(orderDetail != null)
                {
                   try
                    {
                        listOrderDetail.Remove(orderDetail);
                        if (listOrderDetail.Count == 0)
                        {
                            listOrderDetail.Clear();
                        }
                        ProductRepository.RecoverQuantity(orderDetail.Quantity, orderDetail.ProductId);
                    } catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Delete order detail");
                    }
                }
                

                LoadListOrderDetail();
            }
        }
    }
}
