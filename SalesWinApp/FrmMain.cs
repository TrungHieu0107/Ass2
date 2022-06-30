using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesWinApp
{
    public partial class FrmMain : Form
    {

        FrmMembers frmMembers;
        FrmOrder frmOrder;
        FrmProduct frmProduct;
        FormReport formReport;
        public FrmMain()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window ";
            childForm.Show();
        }

        private void member_Click(object sender, EventArgs e)
        {
            if(this.frmMembers is null || this.frmMembers.IsDisposed)
            {
                this.frmMembers = new FrmMembers();
                this.frmMembers.MdiParent = this;
                this.Text = "Member management";
                this.frmProduct?.Close();
                this.frmOrder?.Close();
                this.formReport?.Close();
                this.frmMembers.Show();
            }
        }

        private void product_Click(object sender, EventArgs e)
        {
            if (this.frmProduct is null || this.frmProduct.IsDisposed)
            {
                this.frmProduct = new FrmProduct();
                this.frmProduct.MdiParent = this;
                this.Text = "Product management";
                this.frmMembers?.Close();
                this.frmOrder?.Close();
                this.formReport?.Close();
                this.frmProduct.Show();
                
            }

        }

        private void order_Click(object sender, EventArgs e)
        {
            if (this.frmOrder is null || this.frmOrder.IsDisposed)
            {
                this.frmOrder = new FrmOrder();
                this.frmOrder.MdiParent = this;
                this.Text = "Order management";
                this.frmMembers?.Close();
                this.frmProduct?.Close();
                this.formReport?.Close();
                this.frmOrder.Show();
            }

        }

        private void FrmMain_MdiChildActivate(object sender, EventArgs e)
        {
            if(this.ActiveMdiChild != null )
            {
                //this.ActiveMdiChild.WindowState = FormWindowState.Maximized;
            }
        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.formReport is null || this.formReport.IsDisposed)
            {
                this.formReport = new FormReport();
                this.formReport.MdiParent = this;
                this.Text = "Order management";
                this.frmMembers?.Close();
                this.frmProduct?.Close();
                this.frmOrder?.Close();
                this.formReport.Show();
            }
        }
    }
}
