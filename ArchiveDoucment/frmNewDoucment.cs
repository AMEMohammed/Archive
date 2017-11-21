using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WIATest;
using System.IO;
namespace ArchiveDoucment
{
    public partial class frmNewDoucment : Form
    { DBSQL dbsql = new DBSQL();
        public frmNewDoucment()
        {
            InitializeComponent();
        }

       
        /// <summary>
        /// ///////////// load 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmNewDoucment_Load(object sender, EventArgs e)
        {
            MessageBoxManager.Yes = "نعم";
            MessageBoxManager.No = "الغاء";
            MessageBoxManager.Register();
            this.BackColor = Properties.Settings.Default.color;
            try
            {
                comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
                LoadDate();
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// //////// load date
        /// </summary>
        void LoadDate()
        {
            try
            {
                comboBox1.DataSource = dbsql.GetAllOrganization();
                comboBox1.DisplayMember = "الجهة";
                comboBox1.ValueMember = "الرقم";
                comboBox2.DataSource = dbsql.GetAllTypeDoucment();
                comboBox2.DisplayMember = "النوع";
                comboBox2.ValueMember = "الرقم";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// // Scanner Phote
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        Image image11 = null;
        private void button4_Click(object sender, EventArgs e)
        {
            ListBox lbDevices = new ListBox();
            try
            {
                //get list of devices available
                List<string> devices = WIAScanner.GetDevices();

                foreach (string device in devices)
                {
                    lbDevices.Items.Add(device);
                }
                //check if device is not available
                if (lbDevices.Items.Count == 0)
                {
                    MessageBox.Show("لا يوجد اي جهاز اسكنر");
                    
                }
                else
                {
                    lbDevices.SelectedIndex = 0;
                }
                //get images from scanner
                List<Image> images = WIAScanner.Scan((string)lbDevices.SelectedItem);
             image11 = images[0];
                pictureBox1.Image = images[0];
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe;  *.png";
            try
            {
                if (openfile.ShowDialog() == DialogResult.OK)
                {
                    image11 = Image.FromFile(openfile.FileName);
                    pictureBox1.Image = Image.FromFile(openfile.FileName);
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message);
                image11 = null;
                pictureBox1.Image = null;
            }
        }
/// <summary>
/// / save image
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        { if((int)comboBox1.SelectedValue>0 &&(int) comboBox2.SelectedValue>0 &&image11!=null && textBox1.Text.Length>0)
            {
                try
                {
                    if (MessageBox.Show( "هل تريد الحفظ؟", "تاكيد", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int idor = (int)comboBox1.SelectedValue;
                        int idtype = (int)comboBox2.SelectedValue;
                        MemoryStream ms = new MemoryStream();
                        image11.Save(ms, pictureBox1.Image.RawFormat);
                        dbsql.AddNewDoucment(idtype, idor, textBox1.Text,Convert.ToDateTime(textBox3.Text), textBox2.Text, ms.ToArray());
                      
                        if (checkBox1.Checked)
                        {

                        }
                        else
                        {
                            textBox1.Text = "";
                            textBox2.Text = "";
                            LoadDate();
                        }
                        image11 = null;
                        pictureBox1.Image = null;
                    }
                }
                catch(Exception  ex)
                { MessageBox.Show(ex.Message); }
            }

        }
        /// <summary>
        /// / refish image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            image11 = null;
            pictureBox1.Image = null;
        }
        /// <summary>
        /// /////////// refish all
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            LoadDate();
            image11 = null;
            pictureBox1.Image = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void textBox3_Leave(object sender, EventArgs e)
        { 
            if (textBox3.Text.Length == 8)
            {
                string stt = textBox3.Text;

                string day = stt[0].ToString() + stt[1].ToString();
                string month = stt[2].ToString() + stt[3].ToString();

                string year = stt[4].ToString() + stt[5].ToString() + stt[6].ToString() + stt[7].ToString();

                textBox3.Text = day + "/" + month + "/" + year;
                if ((Convert.ToInt32(day) > 0 && Convert.ToInt32(day) <= 31) && (Convert.ToInt32(month) > 0 && Convert.ToInt32(month) <= 12) && (Convert.ToInt32(year) > 1990 && Convert.ToInt32(year) <= Convert.ToInt32(DateTime.Now.Year.ToString())))
                    textBox3.ForeColor = Color.Black;
                else
                    errordate();

            }
            else
            {
                errordate();
            }
        }
        void errordate()
        {
            textBox3.Focus();
            textBox3.ForeColor = Color.Red;

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }

    }

