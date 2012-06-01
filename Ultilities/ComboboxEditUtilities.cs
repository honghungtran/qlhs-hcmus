﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DevExpress.XtraEditors;

namespace Util
{
    public class CboUtil
    {
        private string sValue;
        private string sDisplay;

        public CboUtil(string svalue, string sdisplay)
        {
            sValue = svalue;
            sDisplay = sdisplay;
        }
        
        public override string ToString() { return sDisplay; }
 
        public string Value
        {
            get { return sValue; }
        }

        /// <summary>
        /// Gắn DataSource cho ComboBoxEdit
        /// </summary>
        /// <param name="comb">ComboBoxEdit</param>
        /// <param name="dt">DataTable</param>
        /// <param name="value">String: Value member</param>
        /// <param name="display">String: Display member</param>
        /// <param name="selected_index">Int: Chọn dòng</param>
        public static void SetDataSource(ComboBoxEdit comb, DataTable dt, string value, string display,
            int selected_index = 0)
        {
            if (dt == null)
                return;
            comb.Properties.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                comb.Properties.Items.Add(
                      new CboUtil(dr[value].ToString(), dr[display].ToString())
                );

            }
            comb.SelectedIndex = selected_index;
        }

        /// <summary>
        /// Gắn DataSource cho ComboBoxEdit có thêm dòng đầu tiên
        /// </summary>
        /// <param name="comb">ComboBoxEdit</param>
        /// <param name="dt">DataTable</param>
        /// <param name="value">String: Value member</param>
        /// <param name="display">String: Display member</param>
        /// <param name="value_all">String: Display member dòng đầu tiên</param>
        /// <param name="display_all">String: Value member dòng đầu tiên</param>
        /// <param name="selected_index">Int: Chọn dòng</param>
        public static void SetDataSource(ComboBoxEdit comb, DataTable dt, string value, string display, 
                string value_all = "all", string display_all = "Tất cả", int selected_index = 0)
        {
           if (dt == null)
                return;
           comb.Properties.Items.Clear();

           comb.Properties.Items.Add(new CboUtil(value_all, display_all));
         
            foreach (DataRow dr in dt.Rows)
            {
                comb.Properties.Items.Add(
                      new CboUtil(dr[value].ToString(), dr[display].ToString())
                );
            }
            comb.SelectedIndex = selected_index;

        }

        /// <summary>
        /// Lấy giá trị trả về selected của ComboboxEdit
        /// </summary>
        /// <param name="comb">ComboBoxEdit</param>
        /// <returns>string: giá trị valuemember</returns>
        public static string GetValueItem(ComboBoxEdit comb) 
        {
            if (comb.SelectedItem == null)
                return null;
            return ((CboUtil)comb.SelectedItem).Value;
        }
        /// <summary>
        /// Lấy giá trị hiển thị selected của ComboboxEdit
        /// </summary>
        /// <param name="comb">ComboBoxEdit</param>
        /// <returns>string: giá trị valuemember</returns>
        public static string GetDisplayItem(ComboBoxEdit comb)
        {
            if (comb.SelectedItem == null)
                return null;
            return ((CboUtil)comb.SelectedItem).sDisplay;
        }

        /// <summary>
        /// Chọn Item ComboBoxEdit
        /// </summary>
        /// <param name="comb">ComboBoxEdit</param>
        /// <param name="svalue">String: Giá trị chọn valuemember</param>
        public static void SelectedItem(ComboBoxEdit comb, string svalue) 
        {
            foreach (var item in comb.Properties.Items)
            {
                if (((CboUtil)item).Value == svalue)
                    comb.SelectedItem = item;
            }
            
        }

        public static bool CheckSelectedNull(ComboBoxEdit comb)
        {
            return comb.SelectedItem == null;
        }
    }

}
