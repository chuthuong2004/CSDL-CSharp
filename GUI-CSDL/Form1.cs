﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace GUI_CSDL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection conn = null;
        string strConn = @"Server=DESKTOP-IIQ7LRK\SQLEXPRESS;Database=CSDL-QLSV;Integrated Security = True";
        private void btnKetNoi_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(strConn);
                conn.Open();
                MessageBox.Show("Kết nối thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDongKetNoi_Click(object sender, EventArgs e)
        {
            if (conn!=null && conn.State == ConnectionState.Open)
            {
                conn.Close();
                MessageBox.Show("Đã đóng kết nối thành công !");
            }
            else
            {
                MessageBox.Show("Bạn chưa kết nối CSDL");
            }
        }
    }
}
