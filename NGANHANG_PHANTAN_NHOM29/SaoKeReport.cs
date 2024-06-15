using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace NGANHANG_PHANTAN_NHOM29
{
    public partial class SaoKeReport : DevExpress.XtraReports.UI.XtraReport
    {
        public SaoKeReport()
        {
            InitializeComponent();
        }
        public SaoKeReport(string SOTK, DateTime batdau, DateTime ketthuc)
        {
            InitializeComponent();
            this.sqlDataSource1.Connection.ConnectionString = Program.connstr;
            this.sqlDataSource1.Queries[0].Parameters[0].Value = SOTK;
            this.sqlDataSource1.Queries[0].Parameters[1].Value = batdau;
            this.sqlDataSource1.Queries[0].Parameters[2].Value = ketthuc;
            this.sqlDataSource1.Fill();
        }
    }
}
