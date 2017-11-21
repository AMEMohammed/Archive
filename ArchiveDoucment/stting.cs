using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArchiveDoucment
{
    public partial class stting : Form
    {
        public stting()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Server = textBox1.Text;
            Properties.Settings.Default.UserSer = textBox2.Text;
            Properties.Settings.Default.PassSer = textBox3.Text;
            Properties.Settings.Default.NameDate = textBox4.Text;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void stting_Load(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = Properties.Settings.Default.Server;
                textBox2.Text = Properties.Settings.Default.UserSer;
                textBox3.Text = Properties.Settings.Default.PassSer;
                textBox4.Text = Properties.Settings.Default.NameDate;
               
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
