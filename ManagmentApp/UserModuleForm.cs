using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagmentApp
{
    public partial class UserModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hamza\source\repos\ManagmentApp\ManagmentApp\DBMS.mdf;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        public UserModuleForm()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            
                this.Dispose();
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtPassword.Text != txtReTypePassword.Text)
                {
                    MessageBox.Show("Password not Matched","Warning!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }
                if(MessageBox.Show("Are You Sure You Want To Save This User?","Saving Records",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("INSERT INTO TbUser(UserName,FullName,Password,Phone)VALUES(@UserName,@FullName,@Password,@Phone) ", con);
                    cmd.Parameters.AddWithValue("@UserName", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("User Successfully Saved");
                    clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }
        public void clear()
        {
            txtUsername.Clear();
            txtFullName.Clear();
            txtPassword.Clear();
            txtReTypePassword.Clear();
            txtPhone.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPassword == txtReTypePassword)
                {
                    MessageBox.Show("Password  Matched", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
               
                if (MessageBox.Show("Are You Sure You Want To Update This User?", "Update Records", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("UPDATE TbUser SET FullName=@FullName,Password=@Password,Phone=@Phone WHERE UserName LIKE '"+txtUsername.Text+"' ", con);
                    
                    cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("User Successfully Updated");
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
