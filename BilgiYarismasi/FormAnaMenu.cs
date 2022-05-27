using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BilgiYarismasi
{
    public partial class FormAnaMenu : Form
    {

        String name;
      
        public FormAnaMenu(String name)
        {
            InitializeComponent();
            this.name = name;
            label1.Text = "Kullanıcı: " + this.name;
        }

        private void FormAnaMenu_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormSorular formSorular = new FormSorular("basicLevel",name);
            formSorular.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormSorular formSorular = new FormSorular("mediumLevel",name);
            formSorular.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormSorular formSorular = new FormSorular("hardLevel",name);
            formSorular.Show();
        }
    }
}
