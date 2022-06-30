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
    public partial class FrmProduct : Form
    {
        IProductRepository productRepository;
        ICategoryRepository categoryRepository = new CategoryRepository();

        BindingSource source;
        public FrmProduct()
        {
            InitializeComponent();
            productRepository = new ProductRepository();

            cboCategoryID.DataSource = categoryRepository.GetAllCategoryID().ToList();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return productRepository.GetAll().ToList();
        }

        public void EnableTextBox(bool flag)
        {
            txtWeight.Enabled = flag;
            txtUnitsInStock.Enabled = flag;
            txtUnitPrice.Enabled = flag;
            txtProductName.Enabled = flag;
            txtProductID.Enabled = flag;
            cboCategoryID.Enabled = flag;
        }

        public void ClearText()
        {
            txtProductID.DataBindings.Clear();
            txtProductName.DataBindings.Clear();
            cboCategoryID.DataBindings.Clear();
            txtWeight.DataBindings.Clear();
            txtUnitsInStock.DataBindings.Clear();
            txtUnitPrice.DataBindings.Clear();

            txtProductID.Text = String.Empty;
            txtProductName.Text = String.Empty;
            txtUnitPrice.Text = String.Empty;
            txtUnitsInStock.Text = String.Empty;
            txtWeight.Text = String.Empty;
            cboCategoryID.Text = String.Empty;
        }

        public void LoadData(IEnumerable<Product> list)
        {
            try
            {
                source = new BindingSource();
                source.DataSource = list;

                txtProductID.DataBindings.Clear();
                txtProductName.DataBindings.Clear();
                cboCategoryID.DataBindings.Clear();
                txtWeight.DataBindings.Clear();
                txtUnitsInStock.DataBindings.Clear();
                txtUnitPrice.DataBindings.Clear();

                txtProductID.DataBindings.Add("Text", source, "ProductId");
                txtProductName.DataBindings.Add("Text", source, "ProductName");
                cboCategoryID.DataBindings.Add("Text", source, "CategoryId");
                txtWeight.DataBindings.Add("Text", source, "Weight");
                txtUnitPrice.DataBindings.Add("Text", source, "UnitPrice");
                txtUnitsInStock.DataBindings.Add("Text", source, "UnitsInStock");

                dgvProduct.DataSource = source;
                if(dgvProduct.Columns["Category"] != null)
                {
                    dgvProduct.Columns["Category"].Visible = false;
                }
                if(dgvProduct.Columns["OrderDetails"] != null)
                {
                    dgvProduct.Columns["OrderDetails"].Visible = false;
                }

                if (list.Count() == 0)
                {
                    btnDelete.Enabled = false;
                    btnUpdate.Enabled = false;
                    DataBindings.Clear();
                }
                else
                {
                    btnDelete.Enabled = true;
                    btnUpdate.Enabled = true;
                }

                EnableTextBox(false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData(GetAllProducts().ToList());
            btnUpdate.Text = "Update";
            btnNew.Text = "New";
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FormProductInsertOrUpdate formProductInsertOrUpdate = new FormProductInsertOrUpdate()
            {
                ProductRepository = productRepository,
                CategoryName = categoryRepository.GetAllCategoryName().ToList(),
                InsertOrUpdate = true,
                CategoryRepository = categoryRepository,
            };
            if (formProductInsertOrUpdate.ShowDialog() == DialogResult.OK)
            {
                formProductInsertOrUpdate.Close();
                LoadData(GetAllProducts().ToList());
            }
        }

        private void FrmProduct_Load(object sender, EventArgs e)
        {
            LoadData(GetAllProducts());
            EnableTextBox(false);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to delete this product", "Delete product", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (txtProductID.Text.Length != 0)
                {
                    try
                    {
                        Product p = new Product()
                        {
                            ProductId = int.Parse(txtProductID.Text),
                        };
                        productRepository.Delete(p);
                        LoadData(GetAllProducts());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Delete product");
                    }
                }
            }
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
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
            FormProductInsertOrUpdate formProductInsertOrUpdate = new FormProductInsertOrUpdate()
            {
                ProductRepository = productRepository,
                //CategoryName = categoryRepository.GetAllCategoryName().ToList(),
                InsertOrUpdate = false,
                CategoryRepository = categoryRepository,
                Product = product,
            };
            if (formProductInsertOrUpdate.ShowDialog() == DialogResult.OK)
            {
                formProductInsertOrUpdate.Close();
                LoadData(GetAllProducts().ToList());
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

           try
            {
                int ProductId = int.Parse(numProductID.Text);
                if (ProductId != 0)
                {
                    LoadData(productRepository.SearchProductByProductID(ProductId).ToList());
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Search product");
            }
        }

        private void btnSearchByName_Click(object sender, EventArgs e)
        {
            try
            {
                string productName = txtProductNameSearch.Text.Trim();
                if(productName.Length != 0)
                {
                    LoadData(productRepository.SearchProduct(productName).ToList());
                } else
                {
                    MessageBox.Show("You must input to search ", "Search product");
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Search product");
            }
        }

        private void btnSearchByUnitPrice_Click(object sender, EventArgs e)
        {
             try
            {
                decimal price = decimal.Parse(txtPriceSearch.Text.Trim());
                if(price > 0)
                {
                    LoadData(productRepository.SearchProductByUnitPrice(price).ToList());
                } else
                {
                    MessageBox.Show("You input wrong", "Search product");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Search product");
            }
        }

        private void btnSearchByUnitsInStock_Click(object sender, EventArgs e)
        {
            try
            {
                int quantity = int.Parse(numUnitInStock.Text.Trim());
                if(quantity > 0)
                {
                    LoadData(productRepository.SearchProductByUnitsInStock(quantity).ToList());
                }
                else
                {
                    MessageBox.Show("You input wrong", "Search product");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Search product");
            }
        }

        private void dgvProduct_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnUpdate_Click(sender, e);
        }
    }
}
