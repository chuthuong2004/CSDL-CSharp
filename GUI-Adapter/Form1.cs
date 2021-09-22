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

namespace GUI_Adapter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection conn = null;
        string sqlConn = @"Server=DESKTOP-IIQ7LRK\SQLEXPRESS;Database=CSDLTest;Integrated Security = True";
        SqlDataAdapter adapter = null;
        DataSet ds = null;
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void KiemTraKetNoi()
        {
            if (conn == null)
                conn = new SqlConnection(sqlConn);
            //if (conn.State == ConnectionState.Closed)
            //conn.Open();
        }
        //Hiển thị dữ liệu
        private void btnNap_Click(object sender, EventArgs e)
        {
            KiemTraKetNoi();
            adapter = new SqlDataAdapter("Select * from SanPham", conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            ds = new DataSet();
            adapter.Fill(ds, "SanPham");
            gvSanPham.DataSource = ds.Tables["SanPham"];

        }
        //Thêm dữ liệu
        private void btnThem_Click(object sender, EventArgs e)
        {
            DataRow row = ds.Tables["SanPham"].NewRow();
            row["Ma"] = txtMa.Text;
            row["Ten"] = txtTen.Text;
            row["DonGia"] = txtGia.Text;
            row["MaDanhMuc"] = txtMaDM.Text;
            ds.Tables["SanPham"].Rows.Add(row);
            int kq = adapter.Update(ds.Tables["SanPham"]);
            if (kq > 0)
            {
                btnNap.PerformClick();
                MessageBox.Show("Thêm thành công !");
            }
            else
            {
                MessageBox.Show("Thêm không thành công !");

            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (vt == -1)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa !");
                return;
            }
            DataRow row = ds.Tables["SanPham"].Rows[vt];
            row.BeginEdit();
            row["Ma"] = txtMa.Text;
            row["Ten"] = txtTen.Text;
            row["DonGia"] = txtGia.Text;
            row["MaDanhMuc"] = txtMaDM.Text;
            row.EndEdit();
            int kq = adapter.Update(ds.Tables["SanPham"]);
            if (kq > 0)
            {
                btnNap.PerformClick();
                MessageBox.Show(("Cập nhật thành công !"));
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại !");
            }

        }

        private void gvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        int vt = -1;
        private void gvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            vt = e.RowIndex;
            if (vt == -1) return;
            DataRow row = ds.Tables["SanPham"].Rows[vt];
            txtMa.Text = row["Ma"] + "";
            txtTen.Text = row["Ten"] + "";
            txtGia.Text = row["DonGia"] + "";
            txtMaDM.Text = row["MaDanhMuc"] + "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (vt == -1)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa !");
                return;
            }
            DataRow row = ds.Tables["SanPham"].Rows[vt];
            row.Delete();
            int kq = adapter.Update(ds.Tables["SanPham"]);
            if (kq > 0)
            {
                btnNap.PerformClick();
                MessageBox.Show("Xóa thành công !");

            }
            else
            {
                MessageBox.Show("Xóa thất bại !");
            }
        }
    }
}
