using DevExpress.XtraEditors;
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
    public partial class frmMoTaiKhoanKH : Form
    {

        String macn = "";
        int vitri = 0;
        bool btn_Edit_clicked = false;
        public frmMoTaiKhoanKH()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void taiKhoanBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsKH_TT.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dS);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dS.GD_CHUYENTIEN' table. You can move, or remove it, as needed.
            this.gD_CHUYENTIENTableAdapter.Fill(this.dS.GD_CHUYENTIEN);
            // TODO: This line of code loads data into the 'dS.GD_GOIRUT' table. You can move, or remove it, as needed.
            this.gD_GOIRUTTableAdapter.Fill(this.dS.GD_GOIRUT);

            try
            {
                dS.EnforceConstraints = false;
                this.gD_CHUYENTIENTableAdapter.Connection.ConnectionString = Program.connstr;
                this.gD_CHUYENTIENTableAdapter.Fill(this.dS.GD_CHUYENTIEN);
                this.gD_GOIRUTTableAdapter.Connection.ConnectionString = Program.connstr;
                this.gD_GOIRUTTableAdapter.Fill(this.dS.GD_GOIRUT);
                this.taiKhoanTableAdapter.Connection.ConnectionString = Program.connstr;
                this.taiKhoanTableAdapter.Fill(this.dS.TaiKhoan);
                cmbChiNhanh.DataSource = Program.bds_dspm; // sao chép bds_ds đã load ở form đăng nhập
                cmbChiNhanh.DisplayMember = "TENCN";
                cmbChiNhanh.ValueMember = "TENSERVER";
                cmbChiNhanh.SelectedIndex = Program.mChiNhanh;
                cmbChiNhanh.Enabled = false;
                if (Program.mGroup == "NGANHANG")
                {
                    cmsThem.Enabled = cmsHieuChinh.Enabled = cmsXoa.Enabled = false;
                }
                else
                {
                    cmsThem.Enabled = cmsHieuChinh.Enabled = cmsXoa.Enabled = true;
                }
                grbThongTinKH.Enabled = gcTK.Enabled = grbThongTinTaiKhoan.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi dữ liệu khách hàng", "Xác nhận", MessageBoxButtons.OK);
                return;
            }
        }

        private void taiKhoanGridControl_Click(object sender, EventArgs e)
        {

        }

        private void cmbChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbChiNhanh.SelectedValue.ToString() == "System.Data.DataRowView")
                return;
            Program.servername = cmbChiNhanh.SelectedValue.ToString();
        }

        private void cmsThem_Click(object sender, EventArgs e)
        {
            try
            {
                grbThongTinTaiKhoan.Enabled = true;
                gcTK.Enabled = false;
                bdsTK.AddNew();
                cmsThem.Enabled = cmsHieuChinh.Enabled = cmsXoa.Enabled = cmsTaiLai.Enabled = false;
                cmsLuu.Enabled = cmsPhucHoi.Enabled = cmsThoat.Enabled = true;
                if (cmbChiNhanh.SelectedIndex == 0)
                {
                    txtMACNSet.Text = "BENTHANH";
                }
                else if (cmbChiNhanh.SelectedIndex == 1)
                {
                    txtMACNSet.Text = "TANDINH";
                }
                teCMND.Text = txtCMNDKH.Text;
                numbSODU.Value = 0;
                Program.myReader.Close();
                Program.conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Xác nhận", MessageBoxButtons.OK);
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (txtCMNDKH.Text.Trim() == "" || txtCMNDKH.Text.Length != 10)
            //{
            //    MessageBox.Show("CMND không được trống hoặc không đủ 10 số", "", MessageBoxButtons.OK);
            //    txtCMNDKH.Focus();
            //    grbThongTinKH.Enabled = gcTK.Enabled = grbThongTinTaiKhoan.Enabled = false;
            //    return;
            //}
            try
            {
                grbThongTinTaiKhoan.Enabled = false;
                grbThongTinKH.Enabled = gcTK.Enabled = true;
                this.khachHang_TTTableAdapter.Connection.ConnectionString = Program.connstr;
                this.khachHang_TTTableAdapter.Fill(this.dS.frmMoTaiKhoanKH_InfoCustomer, txtCMNDKH.Text);
                if (bdsTK.Count > 0)
                {
                    cmsLuu.Enabled = cmsPhucHoi.Enabled = false;
                    cmsThem.Enabled = cmsTaiLai.Enabled = cmsHieuChinh.Enabled = cmsXoa.Enabled = cmsThoat.Enabled = true;
                }
                else
                {
                    cmsThem.Enabled = cmsTaiLai.Enabled = true;
                    cmsHieuChinh.Enabled = cmsXoa.Enabled = cmsThoat.Enabled = cmsPhucHoi.Enabled = cmsPhucHoi.Enabled = false;
                }
                if (Program.mGroup == "NGANHANG")
                {
                    cmsThem.Enabled = cmsHieuChinh.Enabled = cmsXoa.Enabled = false;
                }
                else
                {
                    cmsThem.Enabled = cmsHieuChinh.Enabled = cmsXoa.Enabled = true;
                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void cmsLuu_Click(object sender, EventArgs e)
        {
            String SOTK = ((DataRowView)bdsTK[bdsTK.Position])["SOTK"].ToString().TrimEnd();
            if (txtSOTK.Text.Trim() == "")
            {
                MessageBox.Show("Số tài khoản không được trống", "", MessageBoxButtons.OK);
                txtSOTK.Focus();
                return;
            }
            //if (teCMND.Text.Trim() == "" || teCMND.Text.Length != 10)
            //{
            //    MessageBox.Show("CMND không được trống hoặc không đủ 10 số", "", MessageBoxButtons.OK);
            //    teCMND.Focus();
            //    return;
            //}
            if (numbSODU.Value <= 0)
            {
                MessageBox.Show("Số dư không được trống hoặc bằng 0", "", MessageBoxButtons.OK);
                numbSODU.Focus();
                return;
            }
            String dt = String.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", DateTime.Now);
            if (btn_Edit_clicked == true && SOTK.TrimEnd() == txtSOTK.Text.TrimEnd())
            {
                Program.ExecSqlNonQuery("EXEC frmMoTaiKhoanKH_OpenAccount '" + txtSOTK.Text.TrimEnd() + "','" + teCMND.Text + "','" + numbSODU.Value + "','" + txtMACNSet.Text + "','" + dt + "'");
                this.khachHang_TTTableAdapter.Connection.ConnectionString = Program.connstr;
                this.khachHang_TTTableAdapter.Fill(this.dS.frmMoTaiKhoanKH_InfoCustomer, txtCMNDKH.Text);
                btn_Edit_clicked = false;
            }
            else
            {
                Program.myReader.Close();
                string strlenh1 = "EXEC frmMoTaiKhoanKH_duplicateSoTK '" + txtSOTK.Text.TrimEnd() + "'";
                Program.myReader = Program.ExecSqlDataReader(strlenh1);
                Program.myReader.Read();
                if (Program.myReader.HasRows)
                {
                    MessageBox.Show("Số tài khoản đã tồn tại \nVui lòng nhập lại", "", MessageBoxButtons.OK);
                    return;
                }
                Program.myReader.Close();
                Program.ExecSqlNonQuery("EXEC frmMoTaiKhoanKH_OpenAccount '" + txtSOTK.Text.TrimEnd() + "','" + teCMND.Text + "','" + numbSODU.Value + "','" + txtMACNSet.Text + "','" + dt + "'");
                this.khachHang_TTTableAdapter.Connection.ConnectionString = Program.connstr;
                this.khachHang_TTTableAdapter.Fill(this.dS.frmMoTaiKhoanKH_InfoCustomer, txtCMNDKH.Text);
            }
            gcTK.Enabled = true;
            cmsThem.Enabled = cmsHieuChinh.Enabled = cmsXoa.Enabled = cmsTaiLai.Enabled = cmsThem.Enabled = true;
            cmsLuu.Enabled = cmsPhucHoi.Enabled = false;
            grbThongTinKH.Enabled = grbThongTinTaiKhoan.Enabled = false;
            this.taiKhoanTableAdapter.Connection.ConnectionString = Program.connstr;
            this.taiKhoanTableAdapter.Fill(this.dS.TaiKhoan);
            bdsKH_TT.Position = vitri;
        }

        private void cmsHieuChinh_Click(object sender, EventArgs e)
        {
            vitri = bdsKH_TT.Position;
            txtSOTK.ReadOnly = txtMACNSet.ReadOnly = teCMND.ReadOnly = true;
            cmsThem.Enabled = cmsHieuChinh.Enabled = cmsXoa.Enabled = cmsTaiLai.Enabled = cmsThoat.Enabled = false;
            cmsLuu.Enabled = cmsPhucHoi.Enabled = grbThongTinTaiKhoan.Enabled = true;
            btn_Edit_clicked = true;
            gcTK.Enabled = false;
        }

        private void cmsPhucHoi_Click(object sender, EventArgs e)
        {
            bdsTK.CancelEdit();
            if (cmsThem.Enabled == false) bdsTK.Position = vitri;
            gcTK.Enabled = true;

            panel2.Enabled = grbThongTinTaiKhoan.Enabled = false;
            if (bdsTK.Count > 0)
            {
                cmsPhucHoi.Enabled = cmsLuu.Enabled = false;
                cmsThem.Enabled = cmsTaiLai.Enabled = cmsHieuChinh.Enabled = cmsXoa.Enabled = cmsThoat.Enabled = true;
            }
            else
            {
                cmsThem.Enabled = cmsTaiLai.Enabled = true;
                cmsHieuChinh.Enabled = cmsXoa.Enabled = cmsThoat.Enabled = cmsPhucHoi.Enabled = cmsLuu.Enabled = false;
            }
            gcTK.Enabled = true;
        }

        private void cmsTaiLai_Click(object sender, EventArgs e)
        {
            try
            {
                this.taiKhoanTableAdapter.Connection.ConnectionString = Program.connstr;
                this.taiKhoanTableAdapter.Fill(this.dS.TaiKhoan);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi reload: " + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
        }

        private void cmsThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmsXoa_Click(object sender, EventArgs e)
        {
            int SOTK = int.Parse(((DataRowView)bdsTK[bdsTK.Position])["SOTK"].ToString().TrimEnd());
            if (bds_GR.Count > 0)
            {
                MessageBox.Show("Không thể xoá tài khoản ngân hàng, vì đã thực hiện giao dịch gửi rút tiền ", "", MessageBoxButtons.OK);
                return;
            }
            if (bds_CTC.Count > 0)
            {
                MessageBox.Show("Không thể xoá tài khoản ngân hàng, vì đã thực hiện giao dịch chuyển tiền", "", MessageBoxButtons.OK);
                return;
            }
            if (bds_CTN.Count > 0)
            {
                MessageBox.Show("Không thể xoá tài khoản ngân hàng, vì đã thực hiện giao dịch nhận tiền", "", MessageBoxButtons.OK);
                return;
            }
            if (MessageBox.Show("Bạn có thật sự muốn xoá tài khoản " + SOTK + " ??", "Xác nhận",
                MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    Program.myReader.Close();
                    Program.ExecSqlNonQuery("EXEC frmMoTaiKhoanKH_DeleteTaiKhoanKH '" + SOTK + "'");
                    this.khachHang_TTTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.khachHang_TTTableAdapter.Fill(this.dS.frmMoTaiKhoanKH_InfoCustomer, txtCMNDKH.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xoá tài khoản. Bạn hãy xoá lại\n" + ex.Message, "", MessageBoxButtons.OK);
                    this.taiKhoanTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.taiKhoanTableAdapter.Fill(this.dS.TaiKhoan);
                    return;
                }
            }
            if (bdsTK.Count == 0) cmsXoa.Enabled = false;
        }
    }
}
