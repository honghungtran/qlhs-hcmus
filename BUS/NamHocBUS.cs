﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using QLHS.DTO;
using QLHS.DAL;

namespace QLHS.BUS
{
    public class NamHocBUS
    {
        private NamHocDAL _NamHocDAL;

        public NamHocBUS()
        {
            _NamHocDAL = new NamHocDAL();
        }
        /// <summary>
        /// Lấy list năm học
        /// </summary>
        /// <returns>List</returns>
        public List<NamHocDTO> LayList_NamHoc()
        {
            return _NamHocDAL.LayList_NamHoc();
        }
        /// <summary>
        /// Lấy DataTable năm học
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable LayDTNamHoc()
        {
            return _NamHocDAL.LayDT_NamHoc();
        }
        /// <summary>
        /// Lấy DataTable năm học có mã năm học là tham  số truyền vào
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable LayDTNamHoc(string MaNamHoc)
        {
            return _NamHocDAL.LayDT_NamHoc(MaNamHoc);
        }
        /// <summary>
        /// Lấy DataTable năm học làm năm hiện tại=năm học mới
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable LayDT_NamHocHienTai()
        {
            return _NamHocDAL.LayDTNamHocHienTai();
        }
        /// <summary>
        /// Lấy DataTable năm học cần chuyển lên lớp khi kết thúc năm học
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable LayDT_NamHocTruoc()
        {
            return _NamHocDAL.LayDT_NamHocTruoc();
        }
        /// <summary>
        /// Lấy datatable thêm mới trong 10 năm gần đây
        /// </summary>
        /// <returns></returns>
        public DataTable LayDT_NamHoc_ThemMoi()
        {
            DataTable tbNH = new DataTable();
            tbNH.Columns.Add("MaNamHoc");
            tbNH.Columns.Add("TenNamHoc");
            for (int i = DateTime.Now.Year; i >= DateTime.Now.Year - 9; i--)
            { 
                DataRow dr = tbNH.NewRow();
                int mstart = (i%100), msend = ((i+1)%100);
                dr["MaNamHoc"] = "NH" + (mstart < 10 ? "0"+mstart.ToString() : mstart.ToString()) 
                                      + (msend < 10 ? "0"+msend.ToString() : msend.ToString() );
                dr["TenNamHoc"] = i.ToString() + " - " + (i + 1).ToString();
                tbNH.Rows.Add(dr);
            }
            return tbNH;
        }
        /// <summary>
        /// Kiểm tra tồn tại 1 năm học
        /// </summary>
        /// <param name="maNamHoc">string: mã năm học</param>
        /// <returns></returns>
        public bool KiemTraTonTai_NamHoc(string maNamHoc)
        {
            return _NamHocDAL.KiemTraTonTai_MaNamHoc(maNamHoc);
        }
          /// <summary>
        /// Thêm 1 năm học mới (không kiểm tra trùng mã năm học cũ)
        /// </summary>
        /// <param name="namHoc">NamHocDTO</param>
        /// <returns></returns>
        public bool Them_NamHoc(NamHocDTO namHoc)
        {
            return _NamHocDAL.Them_NamHoc(namHoc);
        }
         /// <summary>
        /// Xóa 1 năm học (xóa toàn bộ thông tin liên quan đến năm học đó)
        /// </summary>
        /// <param name="maNamHoc">string: mã năm học</param>
        /// <returns></returns>
        public bool Xoa_NamHoc(string maNamHoc)
        {
            return _NamHocDAL.Xoa_NamHoc(maNamHoc);
        }
         /// <summary>
        /// Lấy tên năm học
        /// </summary>
        /// <param name="MaNamHoc">String: Mã năm học</param>
        /// <returns></returns>
        public string LayTenNamHoc_MaNamHoc(string MaNamHoc)
        {
            return _NamHocDAL.LayTenNamHoc(MaNamHoc);
        }
    }
}
