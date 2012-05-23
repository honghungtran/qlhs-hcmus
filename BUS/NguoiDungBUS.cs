﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using QLHS.DTO;
using QLHS.DAL;

namespace QLHS.BUS
{
    public class NguoiDungBUS
    {
        private NguoiDungDAL _nguoiDungDAL;
        public NguoiDungBUS()
        {
            _nguoiDungDAL = new NguoiDungDAL();
        }
        /// <summary>
        /// Lấy danh sách người dùng
        /// </summary>
        /// <returns></returns>
        public DataTable Lay_DT_NguoiDung()
        {
            return _nguoiDungDAL.Lay_DT_NguoiDung();
        }
        /// <summary>
        /// Lấy danh sách người dùng đăng nhập
        /// </summary>
        /// <returns></returns>
        public DataTable Lay_DT_NguoiDung_DangNhap()
        {
            return _nguoiDungDAL.Lay_DT_NguoiDung_DangNhap();
        }
        /// <summary>
        /// Lấy thông tin người dùng qua username
        /// </summary>
        /// <param name="username">String: username</param>
        /// <returns></returns>
        public NguoiDungDTO LayThongTinNguoiDung(string username)
        {
            return _nguoiDungDAL.LayThongTinNguoiDung(username);
        }
        /// <summary>
        /// Đổi mật khẩu người dùng
        /// </summary>
        /// <param name="TenDangNhap">Tên đăng nhập</param>
        /// <param name="NewPassword">Mật khẩu mới</param>
        public bool DoiMatKhauNguoiDung(string TenDangNhap, string NewPassword)
        {
            return _nguoiDungDAL.DoiMatKhauNguoiDung(TenDangNhap, NewPassword);
        }
        /// <summary>
        /// Kiểm tra tồn tại người dùng
        /// </summary>
        /// <param name="MaUser">String: Mã người dùng</param>
        /// <returns></returns>
        public bool KiemTraTonTai_NguoiDung(string MaUser)
        {
            return _nguoiDungDAL.KiemTraTonTai_NguoiDung(MaUser);
        }
        /// <summary>
        /// Thêm thông tin người dùng
        /// </summary>
        /// <param name="user">NguoiDungDTO</param>
        /// <returns></returns>
        public bool InsertUser(NguoiDungDTO user)
        {
            return _nguoiDungDAL.InsertUser(user);
        }
        /// <summary>
        /// Sửa thông tin người dùng
        /// </summary>
        /// <param name="user">NguoiDungDTO</param>
        /// <returns></returns>
        public bool UpdateUser(NguoiDungDTO user)
        {
            return _nguoiDungDAL.UpdateUser(user);
        }
        /// <summary>
        /// Xóa thông tin người dùng
        /// </summary>
        /// <param name="MaUser">String: Mã user</param>
        /// <returns></returns>
        public bool DeleteUser(string MaUser)
        {
            return _nguoiDungDAL.DeleteUser(MaUser);
        }
    }
}
