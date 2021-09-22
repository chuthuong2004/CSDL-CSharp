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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection conn = null;
        string sqlConn = @"Server=DESKTOP-IIQ7LRK\SQLEXPRESS;Database=CSDLTest;Integrated Security = True";

        private void Form2_Load(object sender, EventArgs e)
        {
            if (conn == null)
            {
                conn = new SqlConnection(sqlConn);
            }
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from DanhMuc";
            command.Connection = conn;
            lsbDanhMuc.Items.Clear();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string line = reader.GetInt32(0) + "-"+reader.GetString(1);
                lsbDanhMuc.Items.Add(line);
            }
            reader.Close();
        }

        private void lsbDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsbDanhMuc.SelectedIndex == -1) return;
            string line = lsbDanhMuc.SelectedItem.ToString();
            string[] arr = line.Split('-');
            int maDanhMuc = int.Parse(arr[0]);
            if (conn == null)
                conn = new SqlConnection(sqlConn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from SanPham where MaDanhMuc=@maDanhMuc";
            command.Connection = conn;
            SqlParameter parameterDanhMuc = new SqlParameter("@maDanhMuc", SqlDbType.Int);
            parameterDanhMuc.Value = maDanhMuc;
            command.Parameters.Add(parameterDanhMuc);

            lvSanPham.Items.Clear();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int ma = reader.GetInt32(0);
                string ten = reader.GetString(1);
                int donGia = reader.GetInt32(2);
                ListViewItem lvi = new ListViewItem(ma + "");
                lvi.SubItems.Add(ten);
                lvi.SubItems.Add(donGia + "");
                lvSanPham.Items.Add(lvi);
            }
            reader.Close();
        }
    }
}
