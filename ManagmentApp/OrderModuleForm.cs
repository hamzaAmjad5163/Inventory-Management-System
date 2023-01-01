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
    public partial class OrderModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hamza\source\repos\ManagmentApp\ManagmentApp\DBMS.mdf;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        int quantity = 0;
        public OrderModuleForm()
        {
            InitializeComponent();
            LoadCustomer();
            LoadProduct();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void LoadCustomer()
        {
            int i = 0;
            dgvCUSTOMER.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM TbCustomer WHERE CONCAT(CId,Cname) LIKE '%"+textBox1.Text+"%'", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCUSTOMER.Rows.Add(i, dr[0].ToString(), dr[1].ToString());
            }
            dr.Close();
            con.Close();

        }
        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cmd = new SqlCommand("SELECT*FROM TbProduct WHERE CONCAT( Pname , price , pdescription , pcategory) LIKE '%" + textBox2.Text + "%'", con);
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



        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            LoadCustomer();
        }
       

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            getqty();
            if (Convert.ToInt16(numericUpDown1.Value) > quantity)
            {
                MessageBox.Show("Instock qunatity is not enough!","Warning!!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                numericUpDown1.Value = numericUpDown1.Value - 1;
                return;
            }
            if (Convert.ToInt16(numericUpDown1.Value) > 0)
            {
                int total = Convert.ToInt32(textBox6.Text) * Convert.ToInt32(numericUpDown1.Value);
                textBox7.Text = total.ToString();
            }
                
        }

        private void dgvCUSTOMER_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox3.Text = dgvCUSTOMER.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox4.Text = dgvCUSTOMER.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox5.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox8.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox6.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox3.Text == "")
                {
                    MessageBox.Show("PLease Select Customer", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (textBox4.Text == "")
                {
                    MessageBox.Show("PLease Select Product", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
               
                if (MessageBox.Show("Are You Sure You Want To Insert This Order?", "Saving Records", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("INSERT INTO TbOrder(odate,pid,cid,oqty,oprice,total)VALUES(@odate,@pid,@cid,@oqty,@oprice,@total) ", con);
                    cmd.Parameters.AddWithValue("@odate", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@pid",Convert.ToInt16( textBox5.Text));
                    cmd.Parameters.AddWithValue("@cid", Convert.ToInt16(textBox3.Text));
                    cmd.Parameters.AddWithValue("@oqty", Convert.ToInt16(numericUpDown1.Value));
                    cmd.Parameters.AddWithValue("@oprice", Convert.ToInt32(textBox6.Text));
                    cmd.Parameters.AddWithValue("@total", Convert.ToInt32(textBox7.Text));

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Order Successfully Inserted");
                   
                    cmd = new SqlCommand("UPDATE TbProduct SET qty = (qty-@qty) WHERE Pid LIKE '" + textBox5.Text + "'", con);
                    cmd.Parameters.AddWithValue("@qty", Convert.ToInt16(numericUpDown1.Value));
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    clear();
                    LoadProduct();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void clear()
        {
            textBox3.Clear();
            textBox4.Clear();

            textBox5.Clear();
            textBox8.Clear();

            textBox6.Clear();
            numericUpDown1.Value = 0;
            textBox7.Clear();
            dateTimePicker1.Value = DateTime.Now;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();           
        }

        public void getqty()
        {
            
            cmd = new SqlCommand("SELECT qty FROM TbProduct WHERE PId= '" + textBox5.Text + "'", con);

            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                quantity = Convert.ToInt32( dr[0].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
