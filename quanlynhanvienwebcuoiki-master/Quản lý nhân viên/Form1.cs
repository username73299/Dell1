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


namespace Quản_lý_nhân_viên
{
    public partial class Form1 : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = "Data Source=NGUYE\\MANHCHI;Initial Catalog=web;Integrated Security=True;Encrypt=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();

        void loaddata()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from KhachHang ";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;


        }

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            loaddata();


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtmakhachhang.ReadOnly = true;
            int i;
            i = dataGridView1.CurrentRow.Index;
            txtmakhachhang.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            txthoten.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            cbogioitinh.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            txtsodienthoai.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            txtdiachi.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            txtdiemtichluy.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
        }

        private void btnthem_Click(object sender, EventArgs e)
        { 
                command = connection.CreateCommand();
                command.CommandText = "insert into KhachHang values(N'" + txtmakhachhang.Text + "',N'" + txthoten.Text + "',N'" + cbogioitinh.Text + "',N'" + txtsodienthoai.Text + "',N'" + txtdiachi.Text + "',N'" + txtdiemtichluy.Text + "')";
                command.ExecuteNonQuery();
                loaddata();
                MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    command = connection.CreateCommand();
                    command.CommandText = "delete from KhachHang where maKhachHang='" + txtmakhachhang.Text + "'";
                    command.ExecuteNonQuery();
                    loaddata();
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn sửa thông tin khách hàng này không?", "Xác nhận sửa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    command = connection.CreateCommand();
                    command.CommandText = "update KhachHang set hoTen = N'" + txthoten.Text +
                                          "', gioiTinh = N'" + cbogioitinh.Text +
                                          "', soDienThoai = N'" + txtsodienthoai.Text +
                                          "', diaChi = N'" + txtdiachi.Text +
                                          "', diemTichLuy = N'" + txtdiemtichluy.Text +
                                          "' where maKhachHang = N'" + txtmakhachhang.Text + "'";
                    command.ExecuteNonQuery();
                    loaddata();
                    MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnkhoitao_Click(object sender, EventArgs e)
        {
            txtmakhachhang.Text = "";
            txthoten.Text = "";
            cbogioitinh.Text = "";
            txtsodienthoai.Text = "";
            txtdiachi.Text = "";
            txtdiemtichluy.Text = "";
        }
    }

    }

