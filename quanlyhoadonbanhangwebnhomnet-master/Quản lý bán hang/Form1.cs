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


namespace Quản_lý_bán_hang
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
            command.CommandText = "select * from HoaDonBanHang ";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;


        }

        public Form1()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            loaddata();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            int i;
            i = dataGridView1.CurrentRow.Index;
            txtmahoadon.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            txtmasanpham.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            txtmanhanvien.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            txtmakhachhang.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            txtsoluong.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            txtdongia.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
            txtdiemtichluy.Text= dataGridView1.Rows[i].Cells[6].Value.ToString();
            dateTimePicker1.Text= dataGridView1.Rows[i].Cells[8].Value.ToString();
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "insert into HoaDonBanHang values(N'" + txtmahoadon.Text + "',N'" + txtmasanpham.Text + "',N'" + txtmanhanvien.Text + "',N'" + txtmakhachhang.Text + "',N'" + txtsoluong.Text + "',N'" + txtdongia.Text + "',N'"+txtdiemtichluy.Text+"',N'"+dateTimePicker1.Text+"')";
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
                    command.CommandText = "delete from HoaDonBanHang where maHoaDon='" + txtmahoadon.Text + "'";
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

        private void btnsua_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn sửa thông tin khách hàng này không?", "Xác nhận sửa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    command = connection.CreateCommand();
                    command.CommandText = "update HoaDonBanHang set maSanPham = N'" + txtmasanpham.Text +
                                          "', maNhanVien = N'" + txtmanhanvien.Text +
                                          "', maKhachHang = N'" + txtmakhachhang.Text +
                                          "', soLuong = N'" + txtsoluong.Text +
                                          "', donGia = N'" + txtdongia.Text +
                                           "', diemTichLuySuDung = N'" + txtdiemtichluy.Text +
                                          "' where maHoaDon = N'" + txtmahoadon.Text + "'";
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

            txtmahoadon.Text = "";
            txtmasanpham.Text = "";
            txtmanhanvien.Text = "";
            txtmakhachhang.Text = "";
            txtsoluong.Text = "";
            txtdongia.Text = "";
            txtdiemtichluy.Text = "";
            dateTimePicker1.Text = "";
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            
            // Hiển thị hộp thoại xác nhận trước khi thoát
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?",
                                                  "Xác nhận thoát",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

            // Kiểm tra kết quả từ hộp thoại
            if (result == DialogResult.Yes)
            {
                // Đóng ứng dụng
                Application.Exit();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {

            try
            {
                // Tạo câu lệnh SQL để tìm kiếm theo mã hóa đơn
                command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM HoaDonBanHang WHERE maHoaDon LIKE N'%" + txttimkiem.Text + "%'";

                // Thực hiện truy vấn và hiển thị kết quả lên DataGridView
                adapter.SelectCommand = command;
                table.Clear();
                adapter.Fill(table);
                dataGridView1.DataSource = table;

                // Thông báo nếu không tìm thấy
                if (table.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy mã hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

