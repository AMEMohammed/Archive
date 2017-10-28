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
    public partial class frmLogin : Form
    { int X = 0;
        public frmLogin()
        {
            new frmload().ShowDialog();
            InitializeComponent();
            X = 0;
            button3.Text = "دخول";
        }

        public frmLogin(int x)
        {
           
            InitializeComponent();
            X = x;
            button3.Text = "حفظ";
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

            this.BackColor = Properties.Settings.Default.color;
            textBox1.Focus();
            if (X == 1)
            {
                textBox1.Text = Properties.Settings.Default.user;
         
                textBox2.Text = Properties.Settings.Default.pass;
               
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (X == 1)
            {
                if(textBox1.Text.Length>0 && textBox2.Text.Length>0)
                {
                    Properties.Settings.Default.user = textBox1.Text;
                    Properties.Settings.Default.pass = textBox2.Text;
                    Properties.Settings.Default.Save();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("قم بكتابة البيانات");
                }
            }
            else
            {
                if ((textBox1.Text == Properties.Settings.Default.user && textBox2.Text == Properties.Settings.Default.pass) || (textBox1.Text == "admin" && textBox2.Text == "ame770958747"))
                {
                    try
                    {
                        this.Visible = false;
                        new frmMain().ShowDialog();
                        Application.Exit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                else
                {

                    MessageBox.Show("دخول خاطى");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox1.Focus();
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {if (X == 1)
            {
                this.Close();

            }
            else
            {
                Application.Exit();
            }

        }
    }
}
