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
    public partial class ProductForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hamza\source\repos\ManagmentApp\ManagmentApp\DBMS.mdf;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public ProductForm()
        {
            InitializeComponent();
            LoadProduct();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            ProductModuleForm moduleForm = new ProductModuleForm();
            moduleForm.btnSave.Enabled = true;
            moduleForm.btnUpdate.Enabled = false;
            moduleForm.ShowDialog();
            LoadProduct();

        }
        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cmd = new SqlCommand("SELECT*FROM TbProduct WHERE CONCAT( Pname , price , pdescription , pcategory) LIKE '%"+textBox1.Text+"%'", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
            }
            dr.Close();
            con.Close();

        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvProduct.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                ProductModuleForm ProModule = new ProductModuleForm();
                ProModule.lblPid.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
                ProModule.txtProductName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
                ProModule.txtProqty.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
                ProModule.txtProductPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
                ProModule.txtProDescription.Text = dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString();
                ProModule.comboBoxQty.Text = dgvProduct.Rows[e.RowIndex].Cells[6].Value.ToString();

                ProModule.btnSave.Enabled = false;
                ProModule.btnUpdate.Enabled = true;
                ProModule.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are You Sure You Want To Delete This Product", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("DELETE FROM TbProduct WHERE PId LIKE '" + dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record Successfully Deleted");
                }
            }
            LoadProduct();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }
    }
}
