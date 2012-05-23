﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using QLHS.DTO;
using QLHS.DAL;

namespace QLHS.BUS
{
    public class LopBUS
    {
        LopDAL _LopDAL;
         
        public LopBUS()
        {
            _LopDAL = new LopDAL();
        }
         /// <summary>
        /// Lấy DataTable Lớp từ mã năm và khối
        /// </summary>
        /// <param name="MaNamHoc">String: Mã năm học</param>
        /// <param name="MaKhoi">String: mã khối</param>
        /// <returns>DataTable:</returns>
        public DataTable LayDTLop_MaNam_MaKhoi(string MaNamHoc, string MaKhoi)
        {
            return _LopDAL.LayDTLop_MaNam_MaKhoi(MaNamHoc, MaKhoi);
        }
        /// <summary>
        /// Lấy list Lớp từ mã năm và khối
        /// </summary>
        /// <param name="MaNamHoc">String: Mã năm học</param>
        /// <param name="MaKhoi">String: mã khối</param>
        /// <returns>List:</returns>
        public List<LopDTO> LayListLop_MaNam_MaKhoi(string MaNamHoc, string MaKhoi)
        {
            return _LopDAL.LayListLop_MaNam_MaKhoi(MaNamHoc, MaKhoi);
        }
         /// <summary>
        /// Lấy tên giáo viên chủ nhiệm
        /// </summary>
        /// <param name="MaLop">String: Mã lớp</param>
        /// <returns>String: Tên giáo viên</returns>
        public string Lay_TenGiaoVien_MaLop(string MaLop)
        {
            return _LopDAL.Lay_TenGiaoVien_MaLop(MaLop);
        }

        #region Thêm dòng mới
        /// <summary>
        /// Thêm 1 dòng mới vào datatable
        /// </summary>
        /// <param name="MaNamHoc">String: Mã năm học</param>
        /// <param name="MaKhoi">String: Mã khối</param>
        public void AddNewRow(string MaNamHoc, string MaKhoi)
        {
            _LopDAL.AddNewRow(MaNamHoc, MaKhoi);
        }
        #endregion
    }
}
