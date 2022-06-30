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
using DataAccess;
namespace SalesWinApp
{
    public partial class FrmLogin : Form
    {

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text; 
            if(email.Length > 0 && password.Length > 0)
            {
                string role ="";
                Login login = new Login();
                try
                {
                    role = login.CheckLogin(email, password);
                } catch(Exception ex)
                {
                    MessageBox.Show(ex.Message,"Login",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (role == "admin")
                {
                    FrmMain frm = new FrmMain();
                    this.Hide();
                    frm.ShowDialog();
                } else if(role == "user")
                {
                   FormUser  frm = new FormUser()
                    {
                        Email = email,
                    };
                    this.Hide();
                    frm.ShowDialog();
                }
            } else
            {
                MessageBox.Show("You must input all Email and Password");
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
