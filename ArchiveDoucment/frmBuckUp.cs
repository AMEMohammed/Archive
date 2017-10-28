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
    public partial class frmBuckUp : Form
    {
        DBSQL dbsql = new DBSQL();
        public frmBuckUp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = folderBrowserDialog1.SelectedPath;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                try
                {
                    string path = textBox1.Text + "\\Archive" + DateTime.Now.ToShortDateString().Replace('/', '-') + DateTime.Now.ToShortTimeString().Replace(':', '-') + ".bak";
                    dbsql.BuckUpdatabase(path);
                    MessageBox.Show("تمت عملية انشاء نسخة احتياطية بنجاح");
                    textBox1.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
   

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}