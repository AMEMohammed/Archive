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
    public partial class frmType : Form
    {
        DBSQL dbsql = new DBSQL();
        int id = 0;
        string name = "";

        public frmType()
        {
            InitializeComponent();
        }

        private void frmType_Load(object sender, EventArgs e)
        {
            this.BackColor = Properties.Settings.Default.color;

           // try
            {
                dataGridView1.DataSource = dbsql.GetAllTypeDoucment();
                MessageBoxManager.Yes = "نعم";
                MessageBoxManager.No = "الغاء";
                MessageBoxManager.Register();
                changeLanguage();
            }
           // catch (Exception ex)
            {
              //  MessageBox.Show(ex.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                try
                {

                    {
                        dbsql.AddTypeDoucment(textBox1.Text);
                        Refersh1();
                        textBox1.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void changeLanguage()
        {
            foreach (InputLanguage lng in InputLanguage.InstalledInputLanguages)
            {
                if (lng.LayoutName == "العربية (101)")
                    InputLanguage.CurrentInputLanguage = lng;
            }
        }
        /// 
        public void Refersh1()
        {
            dataGridView1.DataSource = dbsql.GetAllTypeDoucment();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox1.Focus();
            id = 0;name = "";

        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    name = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    textBox2.Text = name;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0)
            {
                try
                {
                    name = textBox2.Text;
                    dbsql.UpdateTypeDoucment(id,name);
                    Refersh1();
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("عند حذف (" + name + ")  سيتم حذف جميع المحفوظات المتربطه به هل تريد الاستمرار", "حذف صنف", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes)
            {
                try
                {
                    dbsql.DeleteTypeDoucment(id);
                    Refersh1();
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Refersh1();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
