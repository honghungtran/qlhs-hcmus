﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using QLHS.DTO;
using QLHS.BUS;
using DevExpress.Utils;

namespace QLHS
{
    public partial class frmTimHocSinh : DevExpress.XtraEditors.XtraForm
    {
        private KhoiBUS _khoiBUS;
        private LopBUS _lopBUS;
        private NamHocBUS _namHocBUS;
        private HocSinhBUS _hocSinhBUS;

        public frmTimHocSinh()
        {
            InitializeComponent();
            _khoiBUS = new KhoiBUS();
            _lopBUS = new LopBUS();
            _namHocBUS = new NamHocBUS();
            _hocSinhBUS = new HocSinhBUS();
        }

        /// <summary>
        /// Cập nhật lại list lớp theo khối
        /// </summary>
        private void CapNhatListLop()
        {
            List<LopDTO> list_LopNode;
            // Duyệt từng khối
            foreach (TreeListNode item in treeListSearch.Nodes)
            {

                item.Nodes.Clear();
                list_LopNode = _lopBUS.LayListLop_MaNam_MaKhoi(
                                    Utilities.ComboboxEditUtilities.GetValueItem(comboBoxEditNamHoc),
                                    item.GetValue("MaKhoi").ToString()
                               );
                // add các lớp vào khối item
                foreach (LopDTO lopNode in list_LopNode)
                {
                    this.treeListSearch.AppendNode(new object[] { lopNode.MaLop, lopNode.TenLop}, item);
                }
            }
            treeListSearch.ExpandAll(); // Expand all nodes
        }
        private AutoCompleteStringCollection Tao_Data_AutoComplete_Cbo_TenHocSinh()
        {
            DataTable tb = _hocSinhBUS.LayDTTenHocSinh();
            AutoCompleteStringCollection cl = new AutoCompleteStringCollection();
            foreach (DataRow rd in tb.Rows)
            {
                cl.Add(rd["TenHocSinh"].ToString());
                string[] hoten = Utilities.ObjectUtilities.LayHoTen(rd["TenHocSinh"].ToString());
                if (hoten[0] != "") 
                    cl.Add(hoten[0]);
                if (hoten[1] != "") 
                    cl.Add(hoten[1]);
                if (hoten[2] != "") 
                    cl.Add(hoten[2]);
            }
            return cl;
        }
        private void frmSearchHocSinh_Load(object sender, EventArgs e)
        {
            //disable button khi form load
            simpleButtonXoaDK.Enabled = false;
            
            Utilities.ComboboxEditUtilities.SetDataSource(comboBoxEditNamHoc,
                                                          _namHocBUS.LayDTNamHoc(),
                                                          "MaNamHoc", "TenNamHoc",0);
            treeListSearch.ParentFieldName = "MaKhoi";
            treeListSearch.PreviewFieldName = "TenKhoi";
            treeListSearch.DataSource = _khoiBUS.LayDTKhoi();

            textBoxTenHocSinh.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBoxTenHocSinh.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBoxTenHocSinh.AutoCompleteCustomSource = Tao_Data_AutoComplete_Cbo_TenHocSinh();

            CapNhatListLop();
            // Disable controls search
            DisableControls(false);
        }

        private void comboBoxEditNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapNhatListLop();
        }

        private void treeListSearch_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            Utilities.TreeListUtilities.SetCheckedChildNodes(e.Node, e.Node.CheckState);
            Utilities.TreeListUtilities.SetCheckedParentNodes(e.Node, e.Node.CheckState);
        }

        private void checkEditTatCaHoSo_CheckedChanged(object sender, EventArgs e)
        {
            checkEditTatCaNam.Enabled = !checkEditTatCaHoSo.Checked;
            comboBoxEditNamHoc.Enabled = !checkEditTatCaHoSo.Checked;
            treeListSearch.Enabled = !checkEditTatCaHoSo.Checked;
        }

        private void checkEditTatCaNam_CheckedChanged(object sender, EventArgs e)
        {
            checkEditTatCaHoSo.Enabled = !checkEditTatCaNam.Checked;      
            comboBoxEditNamHoc.Enabled = !checkEditTatCaNam.Checked;
            treeListSearch.Enabled = !checkEditTatCaNam.Checked;
        }
                
        /// <summary>
        /// Tìm kiếm học sinh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonSearch_Click(object sender, EventArgs e)
        {
            //enable button khi nhấn nút tìm
            simpleButtonXoaDK.Enabled = true;            

            DataTable kq_TimKiemDS = null;
            HocSinhTimKiemDTO hsTimKiemDTO = new HocSinhTimKiemDTO();
            hsTimKiemDTO.MaHocSinh = textEditMaHocSinh.Text;
            hsTimKiemDTO.TenHocSinh = textBoxTenHocSinh.Text;
            //lấy giá trị của radioGioiTinh
            if (checkEditGioiTinh.Checked)
            {
                hsTimKiemDTO.GioiTinh = radioGroupGioiTinh.SelectedIndex;
            }
            else
            {
                hsTimKiemDTO.GioiTinh = -1;
            }
            hsTimKiemDTO.NamSinhTu = textEditNamSinhTu.Text;
            hsTimKiemDTO.NamSinhDen = textEditNamSinhDen.Text;
            hsTimKiemDTO.Email = textEditEmail.Text;
            hsTimKiemDTO.DiaChi = textEditDiaChi.Text;

            try
            {
                if (checkEditTatCaHoSo.Checked)  //Tìm tất cả hồ sơ học sinh
                {                                     
                    kq_TimKiemDS = _hocSinhBUS.TimKiem_HocSinh_KoPhanLop(hsTimKiemDTO); 
                }
                if (checkEditTatCaNam.Checked)  // Tìm trong tất cả các năm, các lớp
                {                                
                    kq_TimKiemDS = _hocSinhBUS.TimKiem_HocSinh(hsTimKiemDTO);                    
                }
                else // Nếu ko chọn tìm kiếm tất cả các năm => Tìm kiếm trong tất cả các lớp được checked
                {
                    checkEditTatCaHoSo.Enabled = false;
                    List<string> lopCheck = new List<string>();
                    foreach (TreeListNode khoi in treeListSearch.Nodes)
                    {
                        foreach (TreeListNode lop in khoi.Nodes)
                        {
                            if (lop.Checked)
                            {
                                lopCheck.Add(lop.GetValue("MaKhoi").ToString());
                            }
                        }
                    }
                    kq_TimKiemDS = _hocSinhBUS.TimKiem_HocSinh(hsTimKiemDTO, lopCheck);
                }
            }
            catch (Exception ex)
            {
                Utilities.MessageboxUtilities.MessageError(ex);
                return;
            }

            gridControlSearchHocSinh.DataSource = kq_TimKiemDS;               
        }

        #region Ẩn hiện các control khi load form
        bool Check;
        private void DisableControls(bool show = true)
        {
            checkEditMaHocSinh.Checked = show;
            checkEditHoTen.Checked = show;
            checkEditGioiTinh.Checked = show;
            checkEditNamSinh.Checked = show;
            checkEditEmail.Checked = show;
            checkEditDiaChi.Checked = show;

            textEditMaHocSinh.Enabled = show;
            textBoxTenHocSinh.Enabled = show;
            radioGroupGioiTinh.Enabled = show;
            textEditNamSinhTu.Enabled = show;
            textEditNamSinhDen.Enabled = show;
            textEditEmail.Enabled = show;
            textEditDiaChi.Enabled = show;
        }

        private void EnableControl(CheckEdit checkEdit, TextBox textEdit)
        {
            if (checkEdit.Checked)
                Check = true;
            else
            {
                Check = false;
                textEdit.ResetText();
            }
            textEdit.ReadOnly = !Check;
            textEdit.Enabled = Check;
        }

        private void EnableControl(CheckEdit checkEdit, TextEdit textEdit)
        {
            if (checkEdit.Checked)
                Check = true;
            else
            {
                Check = false;
                textEdit.ResetText();
            }
            textEdit.Properties.ReadOnly = !Check;
            textEdit.Enabled = Check;
        }
        private void EnableControl(CheckEdit checkEdit, RadioGroup radioGroup)
        {
            if (checkEdit.Checked)
                Check = true;
            else
            {
                Check = false;
            }
            radioGroup.Properties.ReadOnly = !Check;
            radioGroup.Enabled = Check;            
        }
        #endregion

        #region Ẩn/hiện textEdit khi checked (không checked) checkEdit
        private void checkEditMaHocSinh_CheckedChanged(object sender, EventArgs e)
        {
            EnableControl(checkEditMaHocSinh, textEditMaHocSinh);
        }

        private void checkEditHoTen_CheckedChanged(object sender, EventArgs e)
        {
            EnableControl(checkEditHoTen, textBoxTenHocSinh);
        }

        private void checkEditGioiTinh_CheckedChanged(object sender, EventArgs e)
        {
            EnableControl(checkEditGioiTinh, radioGroupGioiTinh);
        }

        private void checkEditNamSinh_CheckedChanged(object sender, EventArgs e)
        {
            EnableControl(checkEditNamSinh, textEditNamSinhTu);
            EnableControl(checkEditNamSinh, textEditNamSinhDen);
        }

        private void checkEditEmail_CheckedChanged(object sender, EventArgs e)
        {
            EnableControl(checkEditEmail, textEditEmail);
        }

        private void checkEditDiaChi_CheckedChanged(object sender, EventArgs e)
        {
            EnableControl(checkEditDiaChi, textEditDiaChi);
        }
        #endregion

        private void simpleButtonXoaDK_Click(object sender, EventArgs e)
        {
            frmSearchHocSinh_Load(sender, e);
        }

        private void menucontextXemHoSo_Click(object sender, EventArgs e)
        {
            if (gridViewSearch.FocusedRowHandle < 0)
                return;
            // Lấy form Main
            var frmMainInstance = this.ParentForm as frmMain;
            // Hiển thị frmHocSinh
            frmMainInstance.ShowMDIChildForm<frmHocSinh>();
            // Lấy instance formHocSinh
            var frmHocSinhInstance = frmMainInstance.openForms[typeof(frmHocSinh)] as frmHocSinh;
            // Gắn các properties chuẩn bị hiển thị chi tiết hồ sơ học sinh
            frmHocSinhInstance.MaHocSinh = gridViewSearch.GetFocusedRowCellValue("MaHocSinh").ToString();
            frmHocSinhInstance.MaLop = gridViewSearch.GetFocusedRowCellValue("MaLop").ToString();
            // Hiển thị lại thông tin học sinh
            frmHocSinhInstance.HienThiLai_FrmHocSinh_TuFormTimKiem();
        }

        private void gridViewSearch_MouseMove(object sender, MouseEventArgs e)
        {
            if (gridViewSearch.FocusedRowHandle < 0)
                return;
            Utilities.MessageboxUtilities.ShowTooltip(toolTipController1,
                "Click chuột phải lên dòng để xem chi tiết hồ sơ học sinh", "<b>Hướng dẫn</b>");
        }

        private void simpleButtonDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}