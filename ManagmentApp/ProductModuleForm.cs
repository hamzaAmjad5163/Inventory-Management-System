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
    public partial class ProductModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hamza\source\repos\ManagmentApp\ManagmentApp\DBMS.mdf;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public ProductModuleForm()
        {
            InitializeComponent();
            LoadCategory();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void LoadCategory()
        {
            comboBoxQty.Items.Clear();
            cmd = new SqlCommand("SELECT CatName FROM TbCategory", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBoxQty.Items.Add(dr[0].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (MessageBox.Show("Are You Sure You Want To Save This Product?", "Saving Records", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("INSERT INTO TbProduct(Pname,qty,price,pdescription,pcategory)VALUES(@Pname, @qty, @price, @pdescription, @pcategory) ", con);
                    
                    cmd.Parameters.AddWithValue("@Pname", txtProductName.Text);
                    cmd.Parameters.AddWithValue("@qty", Convert.ToInt64(txtProqty.Text));
                    cmd.Parameters.AddWithValue("@price", Convert.ToInt64(txtProductPrice.Text));
                    cmd.Parameters.AddWithValue("@pdescription", txtProDescription.Text);
                    cmd.Parameters.AddWithValue("@pcategory", comboBoxQty.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Product Successfully Saved");
                    clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void clear()
        {
            txtProductName.Clear();
            txtProqty.Clear();
            txtProductPrice.Clear();
            txtProDescription.Clear();
            comboBoxQty.Text = "";
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are You Sure You Want To Changed This Product?", "Update Records", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("UPDATE TbProduct SET Pname=@Pname,qty=@qty,price=@price,pdescription=@pdescription,pcategory=@pcategory WHERE PId LIKE '" + lblPid.Text + "' ", con);

                    cmd.Parameters.AddWithValue("@Pname", txtProductName.Text);
                    cmd.Parameters.AddWithValue("@qty", Convert.ToInt64(txtProqty.Text));
                    cmd.Parameters.AddWithValue("@price", Convert.ToInt64(txtProductPrice.Text));
                    cmd.Parameters.AddWithValue("@pdescription", txtProDescription.Text);
                    cmd.Parameters.AddWithValue("@pcategory", comboBoxQty.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Product Successfully Changed");
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
