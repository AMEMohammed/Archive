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
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += PrintPage;
            //here to select the printer attached to user PC
            PrintDialog printDialog1 = new PrintDialog();
            printDialog1.Document = pd;
            DialogResult result = printDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    pd.Print();//this will trigger the Print Event handeler PrintPage
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }



            //try
            //{
            //    PrintDocument pd = new PrintDocument();
            //    pd.PrintPage += Pd_PrintPage;

            //    pd.Print();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }


        //      private void Pd_PrintPage(object sender, PrintPageEventArgs e)
        //{
        //    Image i = dbsql.GetImage(id);
        //   // Image i = pictureBox1.Image;
        //    e.Graphics.DrawImage(i, i.Width, i.Height);

        //}
        private void PrintPage(object o, PrintPageEventArgs e)
        {
            try
            {               
                System.Drawing.Image img = pictureBox1.Image;

                Rectangle m = e.MarginBounds;

                if ((double)img.Width / (double)img.Height > (double)m.Width / (double)m.Height) // image is wider
                {
                    m.Height = (int)((double)img.Height / (double)img.Width * (double)m.Width);
                }
                else
                {
                    m.Width = (int)((double)img.Width / (double)img.Height * (double)m.Height);
                }
                e.Graphics.DrawImage(img, m);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
