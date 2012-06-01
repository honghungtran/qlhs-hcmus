﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using QLHS.DTO;

namespace QLHS.DAL
{
    public class PhanLopDAL : ConnectData
    {
        /// <summary>
        /// Kiểm tra mã học sinh trong năm cũ có tồn tại trong năm mới chưa
        /// </summary>
        /// <param name="MaHocSinh"></param>
        /// <param name="MaKhoi"></param>
        /// <param name="MaNamHoc"></param>
        /// <returns> bool</returns>
        public DataTable KT_HocSinh_TonTai_NamHoc(string MaHocSinh,string MaKhoi,string MaNamHoc)
        {
            string khoi="";
            if (MaKhoi == "10")
            {
                khoi = khoi + "('10',11)";
            }
            else
            {
                if (MaKhoi == "11")
                {
                    khoi = khoi + "('11','12')";
                }
                else
                    khoi = khoi + "(12)";
            }
            string sql = "select a.MaHocSinh,b.TenLop from PHANLOP as a, LOP as b where a.MaLop=b.MaLop and a.MaHocSinh='"+MaHocSinh+"' and b.MaNamHoc= '" + MaNamHoc + "' and b.MaKhoiLop in "+khoi+"  ";
            return GetTable(sql);
            
        }
        public DataTable KT_HocSinh_ChuyenLop(string MaHocSinh, string MaLop)
        {
            string sql = "select cl.MaHocSinh,l.TenLop from CHUYENLOP AS cl,LOP AS l where cl.DenLop=l.MaLop and cl.TuLop='"+MaLop+"'"
                + "and cl.MaHocSinh='"+MaHocSinh+"'";
            return GetTable(sql);

        }
        public bool KiemTra_STT_TonTai(int STT, string MaLop)
        {
            string sql = "SELECT STT FROM PHANLOP WHERE MaLop = '"+MaLop+"' AND STT = "+STT;
            return GetTable(sql).Rows.Count > 0;
        }
        public int Lay_STT_TiepTheo(string MaLop)
        {
            string sql = "SELECT STT FROM PHANLOP WHERE MaLop = '"+MaLop+"' ORDER BY STT DESC";
            return Convert.ToInt32(ExecuteScalar(sql)) + 1;
        }
        public int Lay_STT_HienTai(string MaHocSinh, string MaLop)
        {
            string sql = "SELECT STT FROM PHANLOP WHERE MaLop = '" + MaLop + "' AND MaHocSinh = '"+MaHocSinh+"'";
            return Convert.ToInt32(ExecuteScalar(sql));
        }
        /// <summary>
        /// Kiểm tra xem mã học sinh đã  phân lớp hay chưa
        /// </summary>
        /// <param name="MaHocSinh">String: mã học sinh</param>
        /// <returns>Bool</returns>
        public bool KiemTra_TonTai_HocSinh_PhanLop(string MaHocSinh)
        {
            string sql = "SELECT * FROM PHANLOP WHERE MaHocSinh = '"+MaHocSinh+"'";
            return GetTable(sql).Rows.Count > 0;
        }
        /// <summary>
        /// Đếm sỉ số của 1 lớp
        /// </summary>
        /// <param name="MaLop">String: Mã lớp</param>
        /// <returns>Int</returns>
        public int Dem_SiSo_Lop(string MaLop)
        {
            string sql = "SELECT count(*) FROM PHANLOP WHERE MaLop = '" + MaLop +"'";
            return Convert.ToInt32(ExecuteScalar(sql));
        }
        /// <summary>
        /// Đếm sỉ số của 1 lớp mà các học sinh đang theo học
        /// </summary>
        /// <param name="MaLop">String: Mã lớp</param>
        /// <returns>Int</returns>
        public int DemSiSoLop_HocSinhDangHoc(string MaLop)
        {
            string sql = "SELECT count(*) FROM PHANLOP WHERE MaLop = '" + MaLop + "' and MaHocSinh not in (select MaHocSinh from CHUYENLOP where TuLop ='"+MaLop+"')";
            return Convert.ToInt32(ExecuteScalar(sql));
        }
        /// <summary>
        /// Cập nhật STT cho cả lớp
        /// </summary>
        /// <param name="MaLop">String: mã lớp</param>
        /// <param name="arrayList">ArrayList: ArrayList HocSinhChuanHoaTenDTO</param>
        /// <returns>Bool</returns>
        public bool CapNhat_STT_Lop(string MaLop, System.Collections.ArrayList arrayList)
        {
            string sql = "";
            foreach (HocSinhChuanHoaTenDTO hs in arrayList)
            {
                sql +=  "\nUPDATE PHANLOP SET STT = "+hs.STT+" WHERE MaHocSinh = '"+hs.MaHocSinh+"' AND MaLop = '"+MaLop+"'";
            }
            return ExecuteQuery(sql) > 0;
        }
        public bool ChuyenLop_HocSinh(string MaHocSinh,string MaLop)
        {
            int Stt = Lay_STT_TiepTheo(MaLop);
            string sql = "INSERT INTO PHANLOP(Stt,MaHocSinh,MaLop) VALUES("+Stt+",'"+MaHocSinh+"','"+MaLop+"') ";
            return ExecuteQuery(sql) > 0;
        }
        public bool XoaHocSinh_Lop(string MaHocSinh, string MaLop)
        {
            string sql = "DELETE FROM PHANLOP WHERE MaHocSinh='"+MaHocSinh+"' AND MaLop='"+MaLop+"' ";
            return ExecuteQuery(sql) > 0;
        }

        public DataTable LayDTLop_MaNam_MaKhoi_KhacMaLop(string MaNamHoc, string MaKhoi, string MaLop)
        {
            string sql = string.Format("SELECT MaLop, TenLop from Lop where "
                                      + " MaKhoiLop = '{0}' AND MaNamHoc = '{1}' and MaLop not in('{2}') ", MaKhoi, MaNamHoc, MaLop);
            return GetTable(sql);
        }
        public DataTable LayDTKhoi(string MaNamHoc)
        {
            string sql = "select distinct MaKhoiLop as MaKhoi ,N'Khối '+CONVERT(varchar,MaKhoiLop) as TenKhoi from LOP where MaNamHoc='" + MaNamHoc + "' ";
            return GetTable(sql);
        }
        public DataTable LayDTKhoi10(string MaNamHoc)
        {
            string sql = "select distinct MaKhoiLop as MaKhoi ,N'Khối '+CONVERT(varchar,MaKhoiLop) as TenKhoi from LOP where MaNamHoc='" + MaNamHoc + "' and MaKhoiLop=10 ";
            return GetTable(sql);
        }
        public DataTable LayDTKhoi_PhanLopCu(string MaNamHoc)
        {
            string sql = "select distinct MaKhoiLop as MaKhoi ,N'Khối '+CONVERT(varchar,MaKhoiLop) as TenKhoi from LOP where MaNamHoc='" + MaNamHoc + "' and MaKhoiLop in (10,11)";
            return GetTable(sql);
        }
        public DataTable LayDTKhoi_Chuyen(string MaNamHoc, string MaKhoi)
        {
            string sql = "select distinct MaKhoiLop as MaKhoi ,N'Khối '+CONVERT(varchar,MaKhoiLop) as TenKhoi from LOP where MaNamHoc='" + MaNamHoc + "' and MaKhoiLop ='" + MaKhoi + "'";
            return GetTable(sql);
        }
        public DataTable KiemTraHSTonTaiTrongLop_ChuyenLop(string MaHocSinh, string MaLop)
        {
            string sql = "select pl.MaHocSinh,hs.MaHocSinh from PhanLop pl,HocSinh hs where pl.MaHocSinh='"+MaHocSinh+"' and pl.MaLop='"+MaLop+"' and pl.MaHocSinh=hs.MaHocSinh";
            return GetTable(sql);
        }
    }
}

