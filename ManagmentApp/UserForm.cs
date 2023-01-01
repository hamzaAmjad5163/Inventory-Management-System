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
    
    public partial class UserForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hamza\source\repos\ManagmentApp\ManagmentApp\DBMS.mdf;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public UserForm()
        {
            InitializeComponent();
            LoadUser();
        }
        public void LoadUser()
        {
            int i = 0;
            dgvUSER.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM TbUser ",con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvUSER.Rows.Add(i,dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
            }
            dr.Close();
            con.Close();

        }




        private void BtnAdd_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserModuleForm userModule = new UserModuleForm();
            userModule.btnSave.Enabled = true;
            userModule.btnUpdate.Enabled = false;
           
            userModule.ShowDialog();
            LoadUser();
        }

        private void dgvUSER_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvUSER.Columns[e.ColumnIndex].Name;
            if (colName ==  "Edit")
            {
                UserModuleForm userModule = new UserModuleForm();
                userModule.txtUsername.Text = dgvUSER.Rows[e.RowIndex].Cells[1].Value.ToString();
                userModule.txtFullName.Text = dgvUSER.Rows[e.RowIndex].Cells[2].Value.ToString();
                userModule.txtPassword.Text = dgvUSER.Rows[e.RowIndex].Cells[3].Value.ToString();
                userModule.txtPhone.Text = dgvUSER.Rows[e.RowIndex].Cells[4].Value.ToString();

                userModule.btnSave.Enabled = false;
                userModule.btnUpdate.Enabled = true;
                userModule.txtUsername.Enabled = false;
                userModule.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are You Sure You Want To Delete This User","Delete Record",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult. Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("DELETE FROM TbUser WHERE UserName LIKE '"+ dgvUSER.Rows[e.RowIndex].Cells[1].Value.ToString() + "'",con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record Successfully Deleted");
         
                }
            }
            LoadUser();
        }
    }
}
