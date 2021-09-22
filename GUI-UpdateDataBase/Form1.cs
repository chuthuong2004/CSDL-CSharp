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

namespace GUI_UpdateDataBase
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
            HienThiDanhMuc();
        }
        private void HienThiDanhMuc()
        {
            if (conn == null)
                conn = new SqlConnection(sqlConn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from DanhMuc";
            command.Connection = conn;

            SqlDataReader reader = command.ExecuteReader();
            cboDanhMuc.Items.Clear();
            while (reader.Read())
            {
                int ma = reader.GetInt32(0);
                string ten = reader.GetString(1);
                cboDanhMuc.Items.Add(ma + "-" + ten);
            }
            reader.Close();
        }
        int maDanhMuc = -1;
        private void cboDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDanhMuc.SelectedIndex == -1) return;
            string line = cboDanhMuc.SelectedItem + "";
            string[] arr = line.Split('-');
            maDanhMuc = int.Parse(arr[0]);
            HienThiSanPhamTheoDanhMuc(maDanhMuc);
        }
        private void HienThiSanPhamTheoDanhMuc(int maDanhMuc)
        {
            if (conn == null)
                conn = new SqlConnection(sqlConn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from SanPham where MaDanhMuc = "+maDanhMuc;
            command.Connection = conn;
            lvSanPham.Items.Clear();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ListViewItem lvi = new ListViewItem(reader.GetInt32(0) + "");
                lvi.SubItems.Add(reader.GetString(1));
                lvi.SubItems.Add(reader.GetInt32(2) + "");
                lvSanPham.Items.Add(lvi);   
            }
            reader.Close();
        }

        private void lvSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSanPham.SelectedItems.Count == 0) return;
            ListViewItem lvi = lvSanPham.SelectedItems[0];
            txtMa.Text = lvi.SubItems[0].Text;
            txtTen.Text = lvi.SubItems[1].Text;
            txtDonGia.Text = lvi.SubItems[2].Text;
        }
        // không sử dụng parameter
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn == null)
                    conn = new SqlConnection(sqlConn);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                string sql = $"update SanPham set Ten = N'{txtTen.Text}', DonGia = {txtDonGia.Text} where Ma = {txtMa.Text}";
                command.CommandText = sql;
                command.Connection = conn;

                int kq = command.ExecuteNonQuery();
                if (kq > 0)
                {
                    HienThiSanPhamTheoDanhMuc(maDanhMuc);
                    MessageBox.Show("Cập nhật thành công !");
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại !");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn == null)
                    conn = new SqlConnection(sqlConn);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                string sql = "update SanPham set Ten=@ten,DonGia=@gia where Ma=@ma";
                command.CommandText = sql;
                command.Connection = conn;
                command.Parameters.Add("@ten", SqlDbType.NVarChar).Value = txtTen.Text;
                command.Parameters.Add("@gia", SqlDbType.Int).Value = txtDonGia.Text;
                command.Parameters.Add("@ma", SqlDbType.Int).Value = txtMa.Text;

                int kq = command.ExecuteNonQuery();
                if (kq > 0)
                {
                    HienThiSanPhamTheoDanhMuc(maDanhMuc);
                    MessageBox.Show("Cập nhật theo parameter thành công !");
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại !");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
