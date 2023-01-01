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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ManagmentApp
{
    public partial class OrderForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hamza\source\repos\ManagmentApp\ManagmentApp\DBMS.mdf;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        
        public OrderForm()
        {
            InitializeComponent();
            LoadOrder();
        }

        public void LoadOrder()
        {
            double total = 0;
            int i = 0;
            dgvOrder.Rows.Clear();
            cmd = new SqlCommand("SELECT OrderId,odate,O.PId,P.Pname,O.CId,C.Cname,oqty,oprice,total FROM TbOrder AS O JOIN TbCustomer AS C ON O.CId=C.CId JOIN TbProduct AS P ON O.PId=P.PId WHERE CONCAT (OrderId,odate,O.PId,P.Pname,O.CId,C.Cname,oqty,oprice) LIKE '%"+textBox1.Text+"%'", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvOrder.Rows.Add(i, dr[0].ToString(), Convert.ToDateTime(dr[1].ToString()).ToString("dd/MM/yyyy"), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString());
                total += Convert.ToInt32(dr[8].ToString());
            }
            dr.Close();
            con.Close();

            lblQty.Text = i.ToString();
            lbltotal.Text = total.ToString();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            OrderModuleForm orderform = new OrderModuleForm();
            orderform.ShowDialog();
            LoadOrder();
        }

        private void dgvOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvOrder.Columns[e.ColumnIndex].Name;
            
            if (colName == "Delete")
            {
                if (MessageBox.Show("Are You Sure You Want To Delete This Order", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("DELETE FROM TbOrder WHERE OrderId LIKE '" + dgvOrder.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record Successfully Deleted");

                    cmd = new SqlCommand("UPDATE TbProduct SET qty = (qty+@qty) WHERE Pid LIKE '" + dgvOrder.Rows[e.RowIndex].Cells[3].Value.ToString() + "'", con);
                    cmd.Parameters.AddWithValue("@qty", Convert.ToInt16(dgvOrder.Rows[e.RowIndex].Cells[5].Value.ToString()));
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            LoadOrder();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            LoadOrder();
        }
    }
}
