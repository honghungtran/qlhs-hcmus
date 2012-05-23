﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Data;
using QLHS.DTO;
using QLHS.DAL;

namespace QLHS.BUS
{
    public class MonHocBUS
    {
        private MonHocDAL _MonHocDAL;
        public MonHocBUS()
        {
            _MonHocDAL = new MonHocDAL();
        }
        public string Doichuoi(string input, string oldValue, string newValue, bool matchCase)
        {

            RegexOptions regexOption = RegexOptions.None;
            if (!matchCase)
            {
                regexOption = RegexOptions.IgnoreCase;
            }

            Regex regex = new Regex(oldValue, regexOption);

            input = regex.Replace(input, newValue);

            return input;

        }
        
        public int ThemMonHoc(MonHocDTO MHDTO)
        {
            return _MonHocDAL.ThemMonHoc(MHDTO);
        }
        public int XoaMonHoc(string MaMH)
        {
            return _MonHocDAL.XoaMonHoc(MaMH);
        }
        public int CapNhatMonHOc(MonHocDTO MHDTO)
        {
            return _MonHocDAL.CapNhatMonHoc(MHDTO);
          
        }
        public bool KTTonTaiMonHoc(MonHocDTO MHDTO)
        {
            return _MonHocDAL.KTTTMonhoc(MHDTO);
        }
        /// <summary>
        /// Lấy Datatable danh sách môn học
        /// </summary>
        /// <returns>Datatable</returns>
        public DataTable LayDT_DanhSach_MonHoc()
        {
            return _MonHocDAL.LayDT_DanhSach_MonHoc();
        }
        public DataTable LayDT_DanhSach_MonHoc(bool trangthai)
        {
            return _MonHocDAL.LayDT_DanhSach_MonHoc(trangthai);
        }
        /// <summary>
        /// 1:Tìm theo mã
        /// 2:Tìm theo tên
        /// 3:Tìm theo số tiết
        /// 4:Tìm theo hệ số
        /// 5:Tìm theo trang thái
        /// </summary>
        /// <param name="k"></param>
        /// <param name="Dk"></param>
        /// <returns></returns>
        public DataTable Table_MonHoc(int k,string Dk)
        {
            return _MonHocDAL.TableGiaoVien(k,Dk);
        }
    }
}
