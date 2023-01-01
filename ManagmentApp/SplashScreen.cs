using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagmentApp
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            
            InitializeComponent();
        }

        private void gradientPanel1_Paint(object sender, PaintEventArgs e)
        {
            panel2.Width = 0;
            Timer1.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 80; i++)
            {
                panel2.Width = panel2.Width + 6;
                Thread.Sleep(20);
            }
            Timer1.Stop();
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
