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

namespace GUI_DeleteDataBase
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
            HienThiSanPhamLenListBox();
        }
        private void KiemTraKetNoi()
        {
            if (conn == null)
                conn = new SqlConnection(sqlConn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();

        }
        private void HienThiSanPhamLenListBox()
        {
            KiemTraKetNoi();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from SanPham";
            command.Connection = conn;
            SqlDataReader reader = command.ExecuteReader();
            lsbSanPham.Items.Clear();
            while (reader.Read())
            {
                int ma = reader.GetInt32(0);
                string ten = reader.GetString(1);
                int donGia = reader.GetInt32(2);
                lsbSanPham.Items.Add(ma + "-" + ten + "-" + donGia);
            }
            reader.Close();
        }

        private void lsbSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        //Không dùng parameter
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lsbSanPham.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa !");
                return;
            }
            string line = lsbSanPham.SelectedItem + "";
            string[] arr = line.Split('-');
            int ma = int.Parse(arr[0]);
            KiemTraKetNoi();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "delete from SanPham where Ma =" + ma;
            command.Connection = conn;
            int kq = command.ExecuteNonQuery();
            if (kq > 0)
            {
                HienThiSanPhamLenListBox();
                MessageBox.Show("Đã xóa thành công !");
            }
            else
            {
                MessageBox.Show("Đã xóa thất bại !");
            }
        }

        // Có dùng parameter
        private void button1_Click(object sender, EventArgs e)
        {
            if (lsbSanPham.SelectedIndex == -1) return;
            string line = lsbSanPham.SelectedItem + "";
            string[] arr = line.Split('-');
            int ma = int.Parse(arr[0]);
            KiemTraKetNoi();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "delete from SanPham where Ma=@ma";
            command.Connection = conn;
            command.Parameters.Add("@ma", SqlDbType.Int).Value = ma;

            int kq = command.ExecuteNonQuery();
            if ( kq > 0)
            {
                HienThiSanPhamLenListBox();
                MessageBox.Show("Xóa thành công !");
            }
            else
            {
                MessageBox.Show("Xóa thành công !");
            }
        }
    }
}
