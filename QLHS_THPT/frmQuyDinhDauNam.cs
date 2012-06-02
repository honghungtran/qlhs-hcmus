﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QLHS.BUS;
using QLHS.DTO;

namespace QLHS
{
    public partial class frmQuyDinhDauNam : DevExpress.XtraEditors.XtraForm
    {
        private NamHocBUS _namHocBUS;
        private QuyDinhBUS _quyDinhBUS;
        public frmQuyDinhDauNam()
        {
            InitializeComponent();
            _namHocBUS = new NamHocBUS();
            _quyDinhBUS = new QuyDinhBUS();
        }
        private void frmQuyDinhDauNam_Load(object sender, EventArgs e)
        {
            Util.CboUtil.SetDataSource(comboBoxEditNamHoc, _namHocBUS.LayDTNamHoc(),
                                                            "MaNamHoc", "TenNamHoc", 0);
            QuyDinhDTO quyDinh = _quyDinhBUS.LayDS_QuyDinh();
            textEdittenTruong.Text = quyDinh.TenTruong;
            textEditDiaChi.Text = quyDinh.DiaChiTruong;
            spinEditSoLuongLop.Value = quyDinh.SoLuongLop;
            spinEditSiSoToiDa.Value = quyDinh.SiSoCanTren;
            spinEditDoTuoiDen.Value = quyDinh.TuoiCanTren;
            spinEditDoTuoiTu.Value = quyDinh.TuoiCanDuoi;
            spinEditDiemDat.Value = Convert.ToDecimal(quyDinh.DiemChuan);
            dateEditNgayAD.EditValue = quyDinh.NgayApDung;
            Util.CboUtil.SelectedItem(comboBoxEditNamHoc, quyDinh.MaNamHoc);
        }
        private void simpleButtonApDung_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (textEdittenTruong.Text.Length < 3)
            {
                msg = "\nTên trường không hợp lệ! (lớn hơn 3 ký tự)";
            }
            if (textEditDiaChi.Text.Length < 3)
            {
                msg = "\nĐịa chỉ không hợp lệ! (lớn hơn 3 ký tự)";
            }
            if (spinEditSoLuongLop.Value <= 0)
            {
                msg = "\nSố lượng lớp tối đa không hợp lệ! (lớn hơn 0)";
            }
            if (spinEditSiSoToiDa.Value <= 0)
            {
                msg = "\nSỉ số lớp tối đa không hợp lệ! (lớn hơn 0)";
            }
            if (spinEditDoTuoiTu.Value <= 13 || spinEditDoTuoiDen.Value < 13)
            {
                msg = "\nTuổi cận trên hoặc tuổi cận dưới không hợp lệ! (lớn hơn 13)";
            }
            else if(spinEditDoTuoiTu.Value >= spinEditDoTuoiDen.Value)
            {
                msg = "\nTuổi cận dưới phải nhỏ hơn tuổi cận trên!";
            }
            if (spinEditDiemDat.Value < 5 || spinEditDiemDat.Value > 10)
            {
                msg = "\nĐiểm đạt môn không hợp lệ (trong khoảng 5 đến 10)!";
            }
            if (msg != "")
            {
                Util.MsgboxUtil.Error(msg);
                return;
            }
            else
            {
                QuyDinhDTO quyDinh = new QuyDinhDTO()
                {
                    TenTruong= textEdittenTruong.Text.Replace("'","''"),
                    DiaChiTruong = textEditDiaChi.Text.Replace("'","''"),
                    SoLuongLop = Convert.ToInt32(spinEditSoLuongLop.Value),
                    SiSoCanTren = Convert.ToInt32(spinEditSiSoToiDa.Value),
                    TuoiCanTren = Convert.ToInt32( spinEditDoTuoiDen.Value),
                    TuoiCanDuoi = Convert.ToInt32(spinEditDoTuoiTu.Value),
                    DiemChuan = Convert.ToDouble(spinEditDiemDat.Value),
                    NgayApDung = Convert.ToDateTime(dateEditNgayAD.EditValue),
                    MaNamHoc = Util.CboUtil.GetValueItem(comboBoxEditNamHoc)
                };
                if (_quyDinhBUS.CapNhat_QuyDinh(quyDinh))
                    Util.MsgboxUtil.Success("Cập nhật quy định năm học thành công!");
                else
                    Util.MsgboxUtil.Error("Có lỗi trong quá trình cập nhật!");
            }
        }

        private void simpleButtonDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void spinEditDiemDat_Properties_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                double ddat = Convert.ToDouble(spinEditDiemDat.Value);
                if (ddat < 0 || ddat > 10)
                    e.Cancel = true;
            }
            catch
            { 
                
            }
        }

        private void spinEditDiemDat_InvalidValue(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ErrorText = "Điểm nhập không hợp lệ. Điểm có giá trị từ 0 -> 10";
        }

       
    }
}