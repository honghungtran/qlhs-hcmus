﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using QLHS.DTO;
using System.Data.OleDb;

namespace QLHS.DAL
{
    public class NamHocDAL : ConnectData
    {
        public List<NamHocDTO> LayListNamHoc()
        {
            string sql = "SELECT MaNamHoc, TenNamHoc FROM NAMHOC";
            List<NamHocDTO> listNamHoc = new List<NamHocDTO>();
            NamHocDTO namHoc;

            OpenConnect();
            var dr = ExecuteReader(sql);
            while (dr.Read())
            {
                namHoc = new NamHocDTO(Convert.ToString(dr["MaNamHoc"]),
                                       Convert.ToString(dr["TenNamHoc"]));
                listNamHoc.Add(namHoc);
            }
            CloseConnect();

            return listNamHoc;
        }
    }
}
