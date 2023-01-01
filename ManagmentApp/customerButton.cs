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
    public partial class customerButton : PictureBox
    {
        public customerButton()
        {
            InitializeComponent();
        }

        private Image Normalimage;
        private Image Hoverimage;
        public Image ImageNormal
        {
            get { return Normalimage; }
            set { Normalimage = value; }
        }

        public Image ImageHover
        {
            get { return Hoverimage; }
            set { Hoverimage = value; }
        }
       

        private void customerButton_MouseHover_1(object sender, EventArgs e)
        {
            this.Image = Hoverimage;
        }

        private void customerButton_MouseLeave_1(object sender, EventArgs e)
        {
            this.Image = Normalimage;
        }
    }
}
