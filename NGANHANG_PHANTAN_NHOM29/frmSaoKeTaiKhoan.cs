using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using DevExpress.XtraRichEdit.Fields;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NGANHANG_PHANTAN_NHOM29
{
    public partial class frmSaoKeTaiKhoan : DevExpress.XtraEditors.XtraForm
    {
        public frmSaoKeTaiKhoan()
        {
            InitializeComponent();
        }



        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void frmSaoKeTaiKhoan_Load(object sender, EventArgs e)
        {
            dS.EnforceConstraints = false;
            this.thongTinKH_TKSaoKeTableAdapter.Connection.ConnectionString = Program.connstr;
            this.thongTinKH_TKSaoKeTableAdapter.Fill(this.dS.ThongTinKH_TKSaoKe);
            cmbChiNhanh.DataSource = Program.bds_dspm;
            cmbChiNhanh.DisplayMember = "TENCN";
            cmbChiNhanh.ValueMember = "TENSERVER";
            cmbChiNhanh.SelectedIndex = Program.mChiNhanh;
            if (Program.mGroup == "NganHang")
            {
                cmbChiNhanh.Enabled = true;
            }
            else
            {
                cmbChiNhanh.Enabled = false;
            }
        }

        private void cmbChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbChiNhanh.SelectedValue.ToString() == "System.Data.DataRowView")
            {
                return;
            }    
            Program.servername = cmbChiNhanh.SelectedValue.ToString();
            if(cmbChiNhanh.SelectedIndex != Program.mChiNhanh)
            {
                Program.mlogin = Program.remotelogin;
                Program.password = Program.remotepassword;
            }    
            else
            {
                Program.mlogin = Program.mloginDN;
                Program.password = Program.passwordDN;
            }
            if (Program.KetNoi() == 0) MessageBox.Show("Lỗi kết nối về chi nhánh mới", "", MessageBoxButtons.OK);
            else
            {
                this.thongTinKH_TKSaoKeTableAdapter.Connection.ConnectionString = Program.connstr;
                this.thongTinKH_TKSaoKeTableAdapter.Fill(this.dS.ThongTinKH_TKSaoKe);
            }    
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if(txtSoTaiKhoanKhachHang.Text.Trim() == "")
            {
                MessageBox.Show("Số tài khoản không được trống", "", MessageBoxButtons.OK);
                txtSoTaiKhoanKhachHang.Focus();
                txtHoTenKh.Text = "";
                return;
            }
            if (Program.KetNoi() == 0) return;
            string strlenh = "EXEC frmSaoKeTaiKhoan_ThongTinTKSaoKe '" + txtSoTaiKhoanKhachHang.Text.Trim() + "'";
            Program.myReader = Program.ExecSqlDataReader(strlenh);
            if(Program.myReader== null) return;
            Program.myReader.Read();
            if(!Program.myReader.HasRows)
            {
                MessageBox.Show("Số tài khoản không tồn tại \nVui lòng nhập lại", "", MessageBoxButtons.OK);
                return;
            }
            txtHoTenKh.Text = Program.myReader.GetString(0);
            teCMND.Text = Program.myReader.GetString(1);
            Program.myReader.Close();
            Program.conn.Close();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtHoTenKh.Text = teCMND.Text = txtSoTaiKhoanKhachHang.Text = batdau.Text = ketthuc.Text = "";
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if(batdau.DateTime > DateTime.Now || batdau.Text.Trim() == "")
            {
                MessageBox.Show("Ngày bắt đầu trống hoặc mốc thời gian là trước hiện tại", "", MessageBoxButtons.OK);
                batdau.Focus();
                return;
            }
            if (ketthuc.DateTime > DateTime.Now || ketthuc.Text.Trim() == "")
            {
                MessageBox.Show("Ngày kết thúc trống hoặc mốc thời gian là trước hiện tại", "", MessageBoxButtons.OK);
                ketthuc.Focus();
                return;
            }
            if(batdau.DateTime > ketthuc.DateTime)
            {
                MessageBox.Show("Ngày kết thúc không được trước thời gian bắt đầu", "", MessageBoxButtons.OK);
                batdau.Focus();
                return;
            }
            SaoKeReport rpt = new SaoKeReport(txtSoTaiKhoanKhachHang.Text.Trim(), batdau.DateTime, ketthuc.DateTime);
            /* rpt.lbBatDau.Text = batdau.DateTime.ToString("dd/M/yyyy");
            rpt.lbKetThuc.Text = ketthuc.DateTime.ToString("dd/M/yyyy");
            rpt.lbSoTkSaoKe.Text = SoTKSaoKe; */
            ReportPrintTool print = new ReportPrintTool(rpt);
            print.ShowPreviewDialog();
        }
    }
}