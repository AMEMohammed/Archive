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
    public partial class frmSearchDoucment : Form
    { DBSQL dbsql = new DBSQL();
        public frmSearchDoucment()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmSearchDoucment_Load(object sender, EventArgs e)
        {
            this.BackColor = Properties.Settings.Default.color;
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
            GetData();
            MessageBoxManager.Yes = "نعم";
            MessageBoxManager.No = "الغاء";
            MessageBoxManager.Register();
        }
/// <summary>
/// ////////////
/// </summary>
        void GetData()
        {
            try
            {
                comboBox1.DisplayMember = "الجهة";
                comboBox1.ValueMember = "الرقم";
                comboBox1.DataSource = dbsql.GetAllOrganization();
                comboBox2.DisplayMember = "النوع";
                comboBox2.ValueMember = "الرقم";
                comboBox2.DataSource = dbsql.GetAllTypeDoucment();
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        /// <summary>
        /// ////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked== true)
            {
                comboBox1.Enabled = false;

            }
            else
            {
                comboBox1.Enabled = true;
            }
        }
        /// <summary>
        /// ////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                comboBox2.Enabled = false;

            }
            else
            {
                comboBox2.Enabled = true;
            }
        }
        /// <summary>
        /// ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox3.Checked==true)
            {
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;

            }
            else
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;

            }
        }

       

        /// <summary>
        /// //// btn search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string idty = "";
            string idor = "";
            string name = textBox1.Text;
           if(checkBox1.Checked==false)
            {
                idor = comboBox1.SelectedValue.ToString();
            }
           if(checkBox2.Checked==false)
            {
                idty = comboBox2.SelectedValue.ToString();
            }
            try
            {
                if (checkBox3.Checked == true)
                {
                    dataGridView1.DataSource = dbsql.SearchDoucmentByDate(idty, idor, name, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
                    
                }


                else
                {
                    dataGridView1.DataSource = dbsql.SearchDoucment(idty, idor, name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          colordatagrid();

        }
        /// <summary>
        /// //////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
    
        void colordatagrid()
        {
            int couu = dataGridView1.Rows.Count;
            for (int i = 0; i < couu; i+=2)
            {
               try
                {
                   dataGridView1.Rows[i].DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#909998");
                    if (i < couu-1)
                    {
                        dataGridView1.Rows[i + 1].DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#EEF2ED");  }
                }
             catch
                { }
            }
        }
        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count>0)
            {
                if(MessageBox.Show("هل تريد حذف المستند نهائيا ؟","تاكيد",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    int idDo =Convert.ToInt32( dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    try
                    {
                        dbsql.deleteDoucment(idDo);
                        MessageBox.Show("تم حذف المستند");
                        refrshh();


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                 
                }


            }
        }
        int idDo = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    idDo = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    this.Cursor = Cursors.WaitCursor;
                    frmShowImage frm = new frmShowImage(idDo);
                    frm.ShowDialog();
                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                //frmReport frm = new frmReport(idDo);
                //frm.ShowDialog();
                //this.Cursor = Cursors.Default;
                //try
                //{
                //    PrintDocument pd = new PrintDocument();
                //    pd.PrintPage += Pd_PrintPage;

                //    pd.Print();
                //}
                //catch(Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}
            }
        }

        private void Pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            Image i = dbsql.GetImage(idDo);
            Point p = new Point(100, 100);
           e.Graphics.DrawImage(i, 10, 10, i.Width, i.Height);
        }

        private void تعديلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            idDo = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            this.Cursor = Cursors.WaitCursor;
            new frmAupte(idDo).ShowDialog();
            this.Cursor = Cursors.Default;
            refrshh();
        }
        void refrshh()
        {
            GetData();
            textBox1.Text = "";
            dataGridView1 = null;
        }
    }
}
