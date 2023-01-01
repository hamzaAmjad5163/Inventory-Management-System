using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagmentApp
{
    public partial class MainForm : Form
    {


        public MainForm()
        {
            InitializeComponent();



        }
        private Form activeForm = null;
        private void openchildform(Form childForm)
        {
            if (activeForm != null)

                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelMain.Controls.Add(childForm);
            panelMain.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnUSERS_Click(object sender, EventArgs e)
        {
            openchildform(new UserForm());
        }



        private void BtnCUSTOMER_Click(object sender, EventArgs e)
        {
            openchildform(new CustomerForm());

        }

        private void BtnCATEGORIES_Click(object sender, EventArgs e)
        {
            openchildform(new CategoryForm());
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void BtnProduct_Click(object sender, EventArgs e)
        {
            openchildform(new ProductForm());
        }



        private void BtnORDER_Click_1(object sender, EventArgs e)
        {
            openchildform(new OrderForm());
        }



     
    }
}
