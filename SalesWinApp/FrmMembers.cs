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
using SalesWinApp.Form_Update_and_Insert;

namespace SalesWinApp
{
    public partial class FrmMembers : Form
    {
        IMemberRepository memberRepository;
        BindingSource source;
        public FrmMembers()
        {
            InitializeComponent();
            this.memberRepository = new MemberRepository();
            //LoadData(getAllMemberList());


        }


        private void FrmMembers_Load(object sender, EventArgs e)
        {
            txtCountry.Enabled = false;
            txtCompanyName.Enabled = false;
            txtCity.Enabled = false;
            txtMemberId.Enabled = false;
            txtEmail.Enabled = false;
            txtPassword.Enabled = false;

            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            LoadData(getAllMemberList().ToList());
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData(getAllMemberList());
        }

        private void ClearText()
        {
            txtMemberId.Text = "";
            txtCompanyName.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            txtCity.Text = "";
            txtCountry.Text = "";
        }

        public IEnumerable<Member> getAllMemberList()
        {
            return memberRepository.GetAll();
        }

        public void LoadData(IEnumerable<Member> listMember)
        {
            try
            {
                btnNew.Text = "New";
                btnUpdate.Text = "Update";
                source = new BindingSource();
                source.DataSource = listMember;

                txtMemberId.DataBindings.Clear();
                txtCompanyName.DataBindings.Clear();
                txtEmail.DataBindings.Clear();
                txtPassword.DataBindings.Clear();
                txtCity.DataBindings.Clear();
                txtCountry.DataBindings.Clear();

                txtMemberId.DataBindings.Add("Text", source, "MemberId");
                txtCompanyName.DataBindings.Add("Text", source, "CompanyName");
                txtEmail.DataBindings.Add("Text", source, "Email");
                txtPassword.DataBindings.Add("Text", source, "Password");
                txtCity.DataBindings.Add("Text", source, "City");
                txtCountry.DataBindings.Add("Text", source, "Country");
                if(dgvMember.Columns["Orders"] != null)
                {
                    dgvMember.Columns["Orders"].Visible = false;
                }

                dgvMember.DataSource = source;
                if (listMember.Count() == 0)
                {
                    btnDelete.Enabled = false;
                    btnUpdate.Enabled = false;
                }
                else
                {
                    btnDelete.Enabled = true;
                    btnUpdate.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Get member list error " + ex.Message);
            }

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FormMemberInsertOrUpdate formMemberInsertOrUpate = new FormMemberInsertOrUpdate()
            {
                Text = "Add new member",
                MemberRepository = memberRepository,
                InsertOrUpdate = true,

            };

            if (formMemberInsertOrUpate.ShowDialog() == DialogResult.OK)
            {
                formMemberInsertOrUpate.Close();
                LoadData(getAllMemberList().ToList());
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
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
                Text = "Update member",
                MemberRepository = memberRepository,
                InsertOrUpdate = false,
                MemberObject = mem,
            };

            if (formMemberInsertOrUpate.ShowDialog() == DialogResult.OK)
            {
                formMemberInsertOrUpate.Close();
                LoadData(getAllMemberList().ToList());
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtMemberId.Text.Length != 0)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure to delete this member?", "Delete member",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.OK)
                {
                    Member member = new Member()
                    {
                        MemberId = int.Parse(txtMemberId.Text),
                    };

                    try
                    {
                        memberRepository.Delete(member);
                        LoadData(getAllMemberList());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Delete member");
                    }
                }

            }
            else
            {
                MessageBox.Show("You must choose something to delete");
            }

        }

        private void dgvMember_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnUpdate_Click(sender, e);
        }
    }
}
