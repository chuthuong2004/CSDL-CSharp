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

namespace GUI_Binding
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Server=DESKTOP-IIQ7LRK\SQLEXPRESS;Database=CSDLTest;Integrated Security = True");
        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("select * from SanPham", conn);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            lsbSanPham.DataSource = ds.Tables[0];
            lsbSanPham.DisplayMember = "Ten";
            lsbSanPham.ValueMember = "Ma";
        }

        private void cboDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDanhMuc.SelectedIndex == -1 )
            {
                return;
            }
            if (finished == false)
            {
                return;
            }
            int maDm = (int)cboDanhMuc.SelectedValue;

            SqlDataAdapter adapterSanPham = new SqlDataAdapter
                ("select * from SanPham where MaDanhMuc = "+maDm, conn);
            DataSet dsSP = new DataSet();
            adapterSanPham.Fill(dsSP);
            dataGridView1.DataSource = dsSP.Tables[0];
        }
        bool finished = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            SqlDataAdapter adapterDanhMuc = new SqlDataAdapter("select * from DanhMuc", conn);
            DataSet dsDanhMuc = new DataSet();
            adapterDanhMuc.Fill(dsDanhMuc);
            finished = false;
            cboDanhMuc.DataSource = dsDanhMuc.Tables[0];
            cboDanhMuc.DisplayMember = "TenDanhMuc";
            cboDanhMuc.ValueMember = "MaDanhMuc";
            finished = true;
        }
    }
}
