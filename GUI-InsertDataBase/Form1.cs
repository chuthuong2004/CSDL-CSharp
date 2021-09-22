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

namespace GUI_InsertDataBase
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conn = null;
        string sqlConn = @"Server=DESKTOP-IIQ7LRK\SQLEXPRESS;Database=CSDLTest;Integrated Security = True";

        private void Form1_Load(object sender, EventArgs e)
        {
            HienThiToanBoSanPham();
        }

        private void HienThiToanBoSanPham()
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
            lvSanPham.Items.Clear();
            while (reader.Read())
            {
                ListViewItem lvi = new ListViewItem(reader.GetInt32(0) + "");
                lvi.SubItems.Add(reader.GetString(1));
                lvi.SubItems.Add(reader.GetInt32(2) + "");
                lvi.SubItems.Add(reader.GetInt32(3) + "");
                lvSanPham.Items.Add(lvi);
            }
            reader.Close();
        }


        //Không dùng parameter
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (conn == null)
                conn = new SqlConnection(sqlConn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            string sql = $"insert into SanPham(Ma,Ten,DonGia,MaDanhMuc)" +
                $" values ({txtMa.Text},N'{txtTen.Text}',{txtGia.Text},{txtDanhMuc.Text})";
            command.CommandText = sql;
            command.Connection = conn;
            int ret = command.ExecuteNonQuery();
            if (ret > 0)
            {
                HienThiToanBoSanPham();
            }
            else
            {
                MessageBox.Show("Thêm không được cha !");
            }
        }

        //Có dùng parameter
        private void btnLuuParameter_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn == null)
                    conn = new SqlConnection(sqlConn);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                string sql = "insert into SanPham(Ma,Ten,DonGia,MaDanhMuc) " +
                    "values(@ma,@ten,@gia,@maDanhMuc)";
                command.CommandText = sql;
                command.Connection = conn;

                command.Parameters.Add("@ma", SqlDbType.Int).Value = txtMa.Text;
                command.Parameters.Add("@ten", SqlDbType.NVarChar).Value = txtTen.Text;
                command.Parameters.Add("@gia", SqlDbType.Int).Value = txtGia.Text;
                command.Parameters.Add("@maDanhMuc", SqlDbType.Int).Value = txtDanhMuc.Text;

                int kq = command.ExecuteNonQuery();
                if (kq > 0)
                {
                    HienThiToanBoSanPham();
                }
                else
                {
                    MessageBox.Show("Thêm thất bại !");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
