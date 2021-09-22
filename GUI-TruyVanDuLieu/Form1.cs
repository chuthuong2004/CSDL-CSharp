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

namespace GUI_TruyVanDuLieu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conn = null;
        string sqlConn = @"Server=DESKTOP-IIQ7LRK\SQLEXPRESS;Database=CSDLTest;Integrated Security = True";

        // Truy vấn cơ sở dữ liệu để count
        private void btnCount_Click(object sender, EventArgs e)
        {
            if (conn == null)
                conn = new SqlConnection(sqlConn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select count(*) from SanPham";
            command.Connection = conn;

            object data = command.ExecuteScalar();
            int n = (int)data;
            lblSoSanPham.Text = "Có " + n + " sản phẩm ";
        }

        // truy vấn 1 dòng dữ liệu
        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            if (conn == null)
                conn = new SqlConnection(sqlConn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from SanPham where Ma = " + txtNhap.Text;
            command.Connection = conn;
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read()) // có dữ liệu trong bảng
            {
                txtMa.Text = reader.GetInt32(0) + "";
                txtTen.Text = reader.GetString(1);
                txtGia.Text = reader.GetInt32(2) + "";
            }
            reader.Close();
        }

        // truy vấn 1 dòng dữ liệu bằng paramater
        private void btnChiTiet2_Click(object sender, EventArgs e)
        {
            if (conn == null)
                conn = new SqlConnection(sqlConn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from SanPham where Ma=@ma";
            command.Connection = conn;

            SqlParameter sqlParameter = new SqlParameter("@ma", SqlDbType.Int);
            sqlParameter.Value = txtNhap.Text;
            command.Parameters.Add(sqlParameter);

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                txtMa.Text = reader.GetInt32(0) + "";
                txtTen.Text = reader.GetString(1);
                txtGia.Text = reader.GetInt32(2) + "";
            }
            reader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (conn == null)
                conn = new SqlConnection(sqlConn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from SanPham";
            command.Connection = conn;

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem lvi = new ListViewItem(reader.GetInt32(0)+"");
                lvi.SubItems.Add(reader.GetString(1));
                lvi.SubItems.Add(reader.GetInt32(2) + "");
                lvSanPham.Items.Add(lvi);
            }
            reader.Close();
        }

        private void lvSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblSoSanPham_Click(object sender, EventArgs e)
        {

        }
    }
}
