﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QLHS.DTO;
using QLHS.BUS;

namespace QLHS
{
    public partial class frmNamHoc : DevExpress.XtraEditors.XtraForm
    {
        private NamHocBUS _namHocBUS;
        private bool _is_add_button;
        private bool _is_delete_button;
        public frmNamHoc()
        {
            InitializeComponent();
            _namHocBUS = new NamHocBUS();
            _is_add_button = true;
            _is_delete_button = true;
        }

        private void frmNamHoc_Load(object sender, EventArgs e)
        {
            Util.CboUtil.SetDataSource(comboBoxEdit1, _namHocBUS.LayNamHoc_ThemMoi(),
                                                                        "MaNamHoc", "TenNamHoc", 0);
            this._Load_Lai_GridView();
            
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
                return;
            string maNamHoc = gridViewNamHoc.GetRowCellValue(e.FocusedRowHandle, "MaNamHoc").ToString();
            Util.CboUtil.SelectedItem(comboBoxEdit1, maNamHoc);
        }
        private void _Load_Lai_GridView()
        {
            gridControlNamHoc.DataSource = _namHocBUS.LayDTNamHoc();
            this._Disable_Control(editing: false);
        }

        private void _Disable_Control(bool editing)
        {
            _is_add_button = !editing;
            _is_delete_button = !editing;
            
            simpleButtonThem.Text = editing ? "Lưu (Enter)" : "Thêm (Enter)";
            simpleButtonXoa.Text = editing ? "Không thêm (Alt+&D)" : "Xóa (Alt+&D)"; 

            comboBoxEdit1.Enabled = editing;
            gridControlNamHoc.Enabled = !editing;
            if (!editing)
            {

                if (gridViewNamHoc.RowCount > 0)
                    gridView1_FocusedRowChanged(this, 
                        new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(0, 0));
            }

        }

        private void simpleButtonThem_Click(object sender, EventArgs e)
        {
            if (_is_add_button)
            {
                _Disable_Control(editing: true);
            }
            else
            {
                NamHocDTO namHocDTO = new NamHocDTO() { MaNamHoc = Util.CboUtil.GetValueItem(comboBoxEdit1),
                                                        TenNamHoc = Util.CboUtil.GetDisplayItem(comboBoxEdit1)};
                // check & save
                if (_namHocBUS.KiemTraTonTai_NamHoc(namHocDTO.MaNamHoc))
                {
                    Util.MsgboxUtil.Error("Năm học " +namHocDTO.TenNamHoc
                                                                + " đã tồn tại. Hãy chọn 1 năm học khác!");
                    return;
                }
                else
                {
                    _namHocBUS.ThemNamHoc(namHocDTO);
                    Util.MsgboxUtil.Success("Đã tạo năm học mới thành công."
                                                               + "\nTiếp theo bạn hãy tạo danh sách lớp cho năm học này!");
                    
                }
                this._Load_Lai_GridView();
            }
        }

        private void simpleButtonXoa_Click(object sender, EventArgs e)
        {
            string maNamHoc = Util.CboUtil.GetValueItem(comboBoxEdit1);
            string tenNamHoc = Util.CboUtil.GetDisplayItem(comboBoxEdit1);

            if(_is_delete_button)
            {            
                if (_namHocBUS.KiemTraTonTai_NamHoc(maNamHoc))
                {
                    // xóa
                    if (Util.MsgboxUtil.YesNo("Bạn có chắc chắn muốn xóa năm học"
                                                + tenNamHoc + " và tất cả hồ sơ: Lớp học, phân lớp, bảng điểm,... liên quan đến năm học này?")
                        == DialogResult.Yes)
                    {
                        _namHocBUS.XoaNamHoc(maNamHoc);
                        Util.MsgboxUtil.Success("Đã xóa năm học " + tenNamHoc + " thành công!");
                        this._Load_Lai_GridView();
                    }
                }
                else
                {
                    Util.MsgboxUtil.Error("Không tồn tại năm học " + tenNamHoc);
                }
                
            }
            else // Không thêm
            {
                _Disable_Control(editing: false);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}