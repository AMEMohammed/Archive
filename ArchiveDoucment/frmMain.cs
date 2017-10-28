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
    public partial class frmMain : Form
    {
        public frmMain()
        {
          
          
            InitializeComponent();
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            FormCollection fromco = Application.OpenForms;
            bool foundFrom = false;
            foreach(Form frm in fromco)
            {
                if(frm.Name== "frmNewDoucment")
                {
                    frm.Focus();
                   
                    foundFrom = true;
                   
                }

            }
            if (foundFrom == false)
            {
              frmNewDoucment frm = new frmNewDoucment();
                frm.Show();
            }
            this.Cursor = Cursors.Default;
            this.Cursor = Cursors.Default;
            this.Cursor = Cursors.Default;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormCollection fromco = Application.OpenForms;
            bool foundFrom = false;
            foreach (Form frm in fromco)
            {
                if (frm.Name == "frmSearchDoucment")
                {
                    frm.Focus();

                    foundFrom = true;

                }

            }
            if (foundFrom == false)
            {
                frmSearchDoucment frm = new frmSearchDoucment();
                frm.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormCollection fromco = Application.OpenForms;
            bool foundFrom = false;
            foreach (Form frm in fromco)
            {
                if (frm.Name == "frmType")
                {
                    frm.Focus();

                    foundFrom = true;

                }

            }
            if (foundFrom == false)
            {
                frmType frm = new frmType();
                frm.Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormCollection fromco = Application.OpenForms;
            bool foundFrom = false;
            foreach (Form frm in fromco)
            {
                if (frm.Name == "Orgnation")
                {
                    frm.Focus();

                    foundFrom = true;

                }

            }
            if (foundFrom == false)
            {
               Orgnation frm = new Orgnation();
                frm.Show();
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();



        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("هل تريد الخروج", "خروج", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes  )
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
          
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.BackColor = Properties.Settings.Default.color;
            groupBox1.BackColor = Properties.Settings.Default.color;

            }

        private void button6_Click(object sender, EventArgs e)
        {
            FormCollection fromco = Application.OpenForms;
            bool foundFrom = false;
            foreach (Form frm in fromco)
            {
                if (frm.Name == "frmBuckUp")
                {
                    frm.Focus();

                    foundFrom = true;

                }

            }
            if (foundFrom == false)
            {
                frmBuckUp frm = new frmBuckUp();
                frm.Show();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmLogin fmr = new frmLogin(1);
            fmr.ShowDialog();

        }
    }
}
