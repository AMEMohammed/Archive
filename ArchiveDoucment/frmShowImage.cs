using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArchiveDoucment
{
    public partial class frmShowImage : Form
    {
        int id = 0;
        DBSQL dbsql = new DBSQL();
        public frmShowImage()
        {
            InitializeComponent();
        }
        public frmShowImage(int ima)
        {
            InitializeComponent();
            try
            {
                id = ima;
             pictureBox1.Image=   dbsql.GetImage(id);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void frmShowImage_Load(object sender, EventArgs e)
        {
            this.BackColor = Properties.Settings.Default.color;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += Pd_PrintPage;

                pd.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


              private void Pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            Image i = dbsql.GetImage(id);
        
            e.Graphics.DrawImage(i, i.Width, i.Height);
        }

        private void button2_Click(object sender, EventArgs e)
        {
               }
    }
}
