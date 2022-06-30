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

namespace SalesWinApp.Form_Update_and_Insert
{
    public partial class FormMemberInsertOrUpdate : Form
    {
        public IMemberRepository MemberRepository { get; set; }
        public Member MemberObject { get; set; }
        public bool InsertOrUpdate { get; set; } //true: Insert 
        public FormMemberInsertOrUpdate()
        {
            InitializeComponent();

        }

        private void FormMemberInsertOrUpdate_Load(object sender, EventArgs e)
        {
            if (InsertOrUpdate)
            {
                txtMemberId.Enabled = false;
                txtMemberId.Text = (MemberRepository.GetMaxMemberId() + 1).ToString();
                btnInsertOrUpdate.Text = "Add new";
            }
            else
            {
                txtMemberId.Enabled = false;
                btnInsertOrUpdate.Text = "Update";

                txtMemberId.Text = MemberObject.MemberId.ToString();
                txtCompanyName.Text = MemberObject.CompanyName;
                txtEmail.Text = MemberObject.Email;
                txtPassword.Text = "";
                txtComfirm.Text = "";
                txtCity.Text = MemberObject.City;
                txtCountry.Text = MemberObject.Country;
            }
        }

        private void btnInsertOrUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateAll().flag)
            {
                if (InsertOrUpdate) //Insert
                {
                    try
                    {
                        Member newMember = new Member()
                        {
                            MemberId = int.Parse(txtMemberId.Text),
                            Email = txtEmail.Text.Trim(),
                            CompanyName = txtCompanyName.Text.Trim(),
                            City = txtCity.Text.Trim(),
                            Country = txtCountry.Text.Trim(),
                            Password = txtPassword.Text.Trim(),
                        };
                        MemberRepository.Add(newMember);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Add new member", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        DialogResult = DialogResult.None;
                    }
                }
                else
                {
                    try
                    {
                        Member member = new Member()
                        {
                            MemberId = int.Parse(txtMemberId.Text),
                            Email = txtEmail.Text.Trim(),
                            CompanyName = txtCompanyName.Text.Trim(),
                            City = txtCity.Text.Trim(),
                            Country = txtCountry.Text.Trim(),
                            Password = txtPassword.Text.Trim(),
                        };
                        MemberRepository.Update(member);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Update member", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        DialogResult = DialogResult.None;
                    }
                }
            }
            else
            {
                txtPassword.Text = "";
                txtComfirm.Text = "";
                MessageBox.Show(ValidateAll().msg, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        public (bool flag, string msg) ValidateAll()
        {
            bool Flag = true;
            string MsgErr = "";

            //Check Email
            var trimmedEmail = txtEmail.Text.Trim();
            if (trimmedEmail.Length == 0)
            {
                Flag |= false;
                MsgErr += "Please input email \n";
            }
            else
            {
                if (trimmedEmail.EndsWith("."))
                {
                    Flag = false;
                    MsgErr += "Invalid email \n";
                }
                else if (trimmedEmail.Contains(' '))
                {
                    Flag = false;
                    MsgErr += "Invalid email \n";
                }
                else
                {
                    if (!trimmedEmail.Contains("."))
                    {
                        Flag = false;
                        MsgErr += "Invalid email \n";
                    }
                    else
                    {
                        try
                        {
                            var addr = new System.Net.Mail.MailAddress(txtEmail.Text);
                            Flag = (addr.Address == trimmedEmail);
                        }
                        catch
                        {
                            Flag = false;
                            MsgErr += "Invalid email \n";
                        }
                    }

                }
            }
            //check Company Name
            string CompanyName = txtCompanyName.Text.Trim();
            if (CompanyName.Length == 0)
            {
                Flag = false;
                MsgErr += "Please input company Name \n";
            }

            //Check city 
            string City = txtCity.Text.Trim();
            if (City.Length == 0)
            {
                Flag = false;
                MsgErr += "Please input City \n";
            }

            //check Country 
            if (txtCountry.Text.Trim().Length == 0)
            {
                Flag = false;
                MsgErr += "Please input Country \n";
            }

            //check password
            if (txtPassword.Text.Trim().Length == 0)
            {
                Flag = false;
                MsgErr += "Please input password \n";
            }
            else if (txtPassword.Text.Trim().Contains(' '))
            {
                Flag = false;
                MsgErr += "Password not contain whitespace \n";
            } else
            if (!txtPassword.Text.Trim().Equals(txtComfirm.Text.Trim())){
                Flag = false;
                MsgErr += "Confirm not match password \n";
            }

            return (Flag, MsgErr);

        }
    }
}
