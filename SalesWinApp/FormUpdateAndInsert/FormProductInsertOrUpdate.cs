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
    public partial class FormProductInsertOrUpdate : Form
    {
        public IProductRepository ProductRepository { get; set; }
        public Product Product { get; set; }
        public List<string> CategoryName { get; set; }
        public bool InsertOrUpdate { get; set; } // true: Insert

        public ICategoryRepository CategoryRepository { get; set; }

        public FormProductInsertOrUpdate()
        {
            InitializeComponent();
        }

        private void FormProductInsertOrUpdate_Load(object sender, EventArgs e)
        {
            cboCategoryID.DataSource = CategoryRepository.GetAllCategoryID().ToList();

            if (!InsertOrUpdate)
            {
                txtProductID.Enabled = false;
                txtProductID.Text = Product.ProductId.ToString();
                txtProductName.Text = Product.ProductName;
                txtUnitPrice.Text = Product.UnitPrice.ToString();
                txtUnitsInStock.Text = Product.UnitsInStock.ToString();
                txtWeight.Text = Product.Weight.ToString();
                cboCategoryID.Text = Product.CategoryId.ToString();
                btnInsertOrUpdate.Text = "Update";
            }
            else
            {
                btnInsertOrUpdate.Text = "Add new";
                txtProductID.Enabled = false;
                txtProductID.Text = (ProductRepository.GetMaxID() + 1).ToString();
            }
        }

        private void btnInsertOrUpdate_Click(object sender, EventArgs e)
        {
            Product product = new Product()
            {
                ProductName = txtProductName.Text,
                ProductId = int.Parse(txtProductID.Text),
                CategoryId = int.Parse(cboCategoryID.Text),
                Weight = txtWeight.Text,
                UnitPrice = decimal.Parse(txtUnitPrice.Text),
                UnitsInStock = int.Parse(txtUnitsInStock.Text),
            };
            if (InsertOrUpdate)
            {
                try
                {
                    ProductRepository.Add(product);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Add new product");
                    DialogResult = DialogResult.None;
                }
            }
            else
            {
                try
                {
                    ProductRepository.Update(product);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Update product");
                    DialogResult = DialogResult.None;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
