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
    public partial class frmReport : Form
    {DBSQL dbsql=new DBSQL();
        int idimage = 0;
        public frmReport()
        {
            InitializeComponent();
        }
        public frmReport(int idImage)
        {
           
            InitializeComponent();
            idimage = idImage;
        }
        private void frmReport_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            RPT.crp rt = new RPT.crp();



         
            rt.SetDataSource(dbsql.GetImage(idimage));

            crystalReportViewer1.ReportSource = rt;
            crystalReportViewer1.Refresh();
            this.Cursor = Cursors.Default;

        }
    }
}
