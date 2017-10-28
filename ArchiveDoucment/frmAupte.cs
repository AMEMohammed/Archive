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
    public partial class frmAupte : Form
    {
        

      DBSQL dbsql = new DBSQL();
        DataTable dt;
        int idd = 0;
        public frmAupte()
        {
            InitializeComponent();
        }
        public frmAupte(int id)
        {
            InitializeComponent();
            dt = new DataTable();
            idd = id;
            try
            {
                dt = dbsql.GetDoucmentSingle(idd);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void frmAupte_Load(object sender, EventArgs e)
        {
            MessageBoxManager.Yes = "نعم";
            MessageBoxManager.No = "الغاء";
            MessageBoxManager.Register();
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
            GetData();
            this.BackColor = Properties.Settings.Default.color;
        }
        /// </summary>
        void GetData()
        {
            try
            {
                comboBox1.DisplayMember = "الجهة";
                comboBox1.ValueMember = "الرقم";

                comboBox1.DataSource = dbsql.GetAllOrganization();
                comboBox1.SelectedValue = Convert.ToInt32(dt.Rows[0][2].ToString());
                comboBox2.DisplayMember = "النوع";
                comboBox2.ValueMember = "الرقم";
                comboBox2.DataSource = dbsql.GetAllTypeDoucment();
                comboBox2.SelectedValue= Convert.ToInt32(dt.Rows[0][3].ToString());
                textBox1.Text = dt.Rows[0][1].ToString();
                textBox2.Text = dt.Rows[0][4].ToString();

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((int)comboBox1.SelectedValue > 0 && (int)comboBox2.SelectedValue > 00 && textBox1.Text.Length > 0)
            {
                if ((MessageBox.Show("هل تريد تعديل المستند ؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) == DialogResult.Yes))
                {
                    try
                    {
                        dbsql.UpdateDoucment((int)comboBox2.SelectedValue,(int)comboBox1.SelectedValue, textBox1.Text, textBox2.Text, idd);
                        MessageBox.Show("تم التعديل");
                        this.Close();


                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("هناك خطا في احد الصناديق");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
