using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace GUI_Stored_Procedured
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection conn = null;
        string strConn = @"Server=DESKTOP-IIQ7LRK\SQLEXPRESS;Database=CSDLTest;Integrated Security = True";

        private void Form1_Load(object sender, EventArgs e)
        {
            HienThiToanBoSanPham();
        }
        private void HienThiToanBoSanPham()
        {
            KiemTraKetNoi();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "LayToanBoSanPham";
            command.Connection = conn;

            SqlDataReader reader = command.ExecuteReader();
            lvSanPham.Items.Clear();
            while (reader.Read())
            {
                int ma = reader.GetInt32(0);
                string ten = reader.GetString(1);
                int gia = reader.GetInt32(2);
                ListViewItem lvi = new ListViewItem(ma + "");
                lvi.SubItems.Add(ten);
                lvi.SubItems.Add(gia + "");
                lvSanPham.Items.Add(lvi);
            }
            reader.Close();
        }
        private void KiemTraKetNoi()
        {
            if (conn == null)
                conn = new SqlConnection(strConn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
        }

        private void lvSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSanPham.SelectedItems.Count == 0) return;
            ListViewItem lvi = lvSanPham.SelectedItems[0];
            int ma = int.Parse(lvi.SubItems[0].Text);
            HienThiChiTietSanPham(ma);
        }
        private void HienThiChiTietSanPham(int ma)
        {
            KiemTraKetNoi();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ChiTietSanPham";
            command.Connection = conn;
            SqlParameter parameter = new SqlParameter("@ma", SqlDbType.Int);
            parameter.Value = ma;
            command.Parameters.Add(parameter);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                txtMa.Text = reader.GetInt32(0) + "";
                txtTen.Text = reader.GetString(1);
                txtGia.Text = reader.GetInt32(2) + "";
                txtMaDM.Text = reader.GetInt32(3) + "";
            }
            reader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            KiemTraKetNoi();

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ThemSanPham";
            command.Connection = conn;
            command.Parameters.Add("@ma", SqlDbType.Int).Value = txtMa.Text;
            command.Parameters.Add("@ten", SqlDbType.NVarChar).Value = txtTen.Text;
            command.Parameters.Add("@gia", SqlDbType.NVarChar).Value = txtGia.Text;
            command.Parameters.Add("@maDanhMuc", SqlDbType.NVarChar).Value = txtMaDM.Text;
            int kq = command.ExecuteNonQuery();
            if (kq > 0)
            {
                HienThiToanBoSanPham();
                MessageBox.Show("Thêm thành công !");

            }
            else
            {
                MessageBox.Show("Thêm thất bại !");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            KiemTraKetNoi();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "CapNhatGia";
            command.Connection = conn;
            command.Parameters.Add("@ma", SqlDbType.Int).Value = txtMa.Text;
            command.Parameters.Add("@gia", SqlDbType.Int).Value = txtGia.Text;
            int kq = command.ExecuteNonQuery();
            if ( kq > 0)
            {
                HienThiToanBoSanPham();
                MessageBox.Show("Sửa giá thành công !");

            }
            else
            {
                MessageBox.Show("Sửa giá không thành công !");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            KiemTraKetNoi();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "XoaSanPham";
            command.Connection = conn;
            command.Parameters.Add("@ma", SqlDbType.Int).Value = txtMa.Text;
            int kq = command.ExecuteNonQuery();
            if (kq > 0)
            {
                HienThiToanBoSanPham();
                MessageBox.Show("Xóa sản phẩm thành công !");
            }
            else
            {
                MessageBox.Show("Xóa sản phẩm thất bại !");
            }
        }
    }
}
