using System.Data;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;

namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        private const string ConnectionString =
            "Server=(localdb)\\MSSQLLocalDB;Database=QuanLySinhVien;Trusted_Connection=True;TrustServerCertificate=True;";

        private FormMode _mode = FormMode.View;
        private string? _selectedMaSinhVien;

        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
        }

        private enum FormMode
        {
            View,
            Add,
            Edit
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            SetFormMode(FormMode.View);
            LoadStudents();
        }

        private void LoadStudents()
        {
            try
            {
                using var connection = new SqlConnection(ConnectionString);
                using var command = new SqlCommand(
                    "SELECT MaSinhVien, HoTen, NgaySinh, GioiTinh, Lop, Email FROM SinhVien ORDER BY MaSinhVien",
                    connection);
                using var adapter = new SqlDataAdapter(command);
                var table = new DataTable();
                adapter.Fill(table);
                dgvSinhVien.DataSource = table;
                ConfigureGridHeaders();
            }
            catch (Exception ex) when (ex is SqlException or InvalidOperationException)
            {
                MessageBox.Show(
                    "Không thể tải dữ liệu. Hãy chạy file Database.sql và kiểm tra chuỗi kết nối.\n\n" + ex.Message,
                    "Lỗi kết nối database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureGridHeaders()
        {
            if (dgvSinhVien.Columns.Count == 0)
            {
                return;
            }

            dgvSinhVien.Columns[nameof(StudentRow.MaSinhVien)].HeaderText = "Mã sinh viên";
            dgvSinhVien.Columns[nameof(StudentRow.HoTen)].HeaderText = "Họ và tên";
            dgvSinhVien.Columns[nameof(StudentRow.NgaySinh)].HeaderText = "Ngày sinh";
            dgvSinhVien.Columns[nameof(StudentRow.GioiTinh)].HeaderText = "Giới tính";
            dgvSinhVien.Columns[nameof(StudentRow.Lop)].HeaderText = "Lớp";
            dgvSinhVien.Columns[nameof(StudentRow.Email)].HeaderText = "Email";
        }

        private void SetFormMode(FormMode mode)
        {
            _mode = mode;
            var isView = mode == FormMode.View;
            var hasSelection = dgvSinhVien.SelectedRows.Count > 0;

            txtMaSinhVien.ReadOnly = mode != FormMode.Add;
            txtHoTen.ReadOnly = isView;
            dtpNgaySinh.Enabled = !isView;
            cboGioiTinh.Enabled = !isView;
            txtLop.ReadOnly = isView;
            txtEmail.ReadOnly = isView;

            btnThem.Enabled = isView;
            btnSua.Enabled = isView && hasSelection;
            btnXoa.Enabled = isView && hasSelection;
            btnLuu.Enabled = !isView;
            btnHuy.Enabled = !isView;
            btnLamMoi.Enabled = isView;
            dgvSinhVien.Enabled = isView;
        }

        private void btnThem_Click(object? sender, EventArgs e)
        {
            ClearInputs();
            SetFormMode(FormMode.Add);
            txtMaSinhVien.Focus();
        }

        private void btnSua_Click(object? sender, EventArgs e)
        {
            if (dgvSinhVien.SelectedRows.Count == 0)
            {
                return;
            }

            _selectedMaSinhVien = txtMaSinhVien.Text;
            SetFormMode(FormMode.Edit);
            txtHoTen.Focus();
        }

        private void btnXoa_Click(object? sender, EventArgs e)
        {
            if (dgvSinhVien.SelectedRows.Count == 0 || string.IsNullOrWhiteSpace(txtMaSinhVien.Text))
            {
                return;
            }

            var answer = MessageBox.Show(
                $"Bạn có chắc muốn xóa sinh viên {txtHoTen.Text} không?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using var connection = new SqlConnection(ConnectionString);
                using var command = new SqlCommand("DELETE FROM SinhVien WHERE MaSinhVien = @MaSinhVien", connection);
                command.Parameters.AddWithValue("@MaSinhVien", txtMaSinhVien.Text.Trim());
                connection.Open();
                command.ExecuteNonQuery();
                LoadStudents();
                ClearInputs();
                SetFormMode(FormMode.View);
            }
            catch (SqlException ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private void btnLuu_Click(object? sender, EventArgs e)
        {
            if (!ValidateInputs())
            {
                return;
            }

            try
            {
                using var connection = new SqlConnection(ConnectionString);
                connection.Open();

                if (_mode == FormMode.Add)
                {
                    using var command = new SqlCommand(
                        "INSERT INTO SinhVien (MaSinhVien, HoTen, NgaySinh, GioiTinh, Lop, Email) " +
                        "VALUES (@MaSinhVien, @HoTen, @NgaySinh, @GioiTinh, @Lop, @Email)", connection);
                    AddParameters(command, txtMaSinhVien.Text.Trim());
                    command.ExecuteNonQuery();
                }
                else if (_mode == FormMode.Edit)
                {
                    using var command = new SqlCommand(
                        "UPDATE SinhVien SET HoTen = @HoTen, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, " +
                        "Lop = @Lop, Email = @Email WHERE MaSinhVien = @MaCu", connection);
                    AddParameters(command, txtMaSinhVien.Text.Trim());
                    command.Parameters.AddWithValue("@MaCu", _selectedMaSinhVien!);
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Lưu dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadStudents();
                SetFormMode(FormMode.View);
            }
            catch (SqlException ex) when (ex.Number is 2627 or 2601)
            {
                MessageBox.Show("Mã sinh viên đã tồn tại.", "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (SqlException ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private void btnHuy_Click(object? sender, EventArgs e)
        {
            if (_mode == FormMode.Edit && dgvSinhVien.Rows.Count > 0)
            {
                LoadSelectedRow();
            }
            else
            {
                ClearInputs();
            }

            SetFormMode(FormMode.View);
        }

        private void btnLamMoi_Click(object? sender, EventArgs e)
        {
            LoadStudents();
            ClearInputs();
            SetFormMode(FormMode.View);
        }

        private void dgvSinhVien_SelectionChanged(object? sender, EventArgs e)
        {
            if (_mode == FormMode.View && dgvSinhVien.SelectedRows.Count > 0)
            {
                LoadSelectedRow();
            }

            SetFormMode(_mode);
        }

        private void LoadSelectedRow()
        {
            if (dgvSinhVien.SelectedRows.Count == 0)
            {
                return;
            }

            var row = dgvSinhVien.SelectedRows[0];
            txtMaSinhVien.Text = row.Cells["MaSinhVien"].Value?.ToString() ?? string.Empty;
            txtHoTen.Text = row.Cells["HoTen"].Value?.ToString() ?? string.Empty;
            if (DateTime.TryParse(row.Cells["NgaySinh"].Value?.ToString(), out var date))
            {
                dtpNgaySinh.Value = date;
            }

            cboGioiTinh.Text = row.Cells["GioiTinh"].Value?.ToString() ?? string.Empty;
            txtLop.Text = row.Cells["Lop"].Value?.ToString() ?? string.Empty;
            txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? string.Empty;
        }

        private void AddParameters(SqlCommand command, string maSinhVien)
        {
            command.Parameters.AddWithValue("@MaSinhVien", maSinhVien);
            command.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
            command.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value.Date);
            command.Parameters.AddWithValue("@GioiTinh", cboGioiTinh.Text);
            command.Parameters.AddWithValue("@Lop", txtLop.Text.Trim());
            command.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(txtEmail.Text) ? DBNull.Value : txtEmail.Text.Trim());
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtMaSinhVien.Text))
            {
                return ShowValidation("Mã sinh viên không được để trống.", txtMaSinhVien);
            }

            if (!Regex.IsMatch(txtMaSinhVien.Text.Trim(), "^[A-Za-z0-9]+$"))
            {
                return ShowValidation("Mã sinh viên chỉ được chứa chữ và số.", txtMaSinhVien);
            }

            if (string.IsNullOrWhiteSpace(txtHoTen.Text) || !Regex.IsMatch(txtHoTen.Text.Trim(), @"^[\p{L}\s]+$"))
            {
                return ShowValidation("Họ tên không được trống và chỉ được chứa chữ cái, khoảng trắng.", txtHoTen);
            }

            if (dtpNgaySinh.Value.Date >= DateTime.Today.AddYears(-15))
            {
                return ShowValidation("Sinh viên phải từ 15 tuổi trở lên.", dtpNgaySinh);
            }

            if (string.IsNullOrWhiteSpace(txtLop.Text))
            {
                return ShowValidation("Lớp không được để trống.", txtLop);
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text) &&
                !Regex.IsMatch(txtEmail.Text.Trim(), @"^[^\s@]+@[^\s@]+\.[^\s@]+$"))
            {
                return ShowValidation("Email không đúng định dạng.", txtEmail);
            }

            return true;
        }

        private static bool ShowValidation(string message, Control control)
        {
            MessageBox.Show(message, "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            control.Focus();
            return false;
        }

        private void ClearInputs()
        {
            txtMaSinhVien.Clear();
            txtHoTen.Clear();
            dtpNgaySinh.Value = DateTime.Today.AddYears(-18);
            cboGioiTinh.SelectedIndex = 0;
            txtLop.Clear();
            txtEmail.Clear();
            _selectedMaSinhVien = null;
        }

        private static void ShowDatabaseError(SqlException ex)
        {
            MessageBox.Show(ex.Message, "Lỗi database", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private sealed class StudentRow
        {
            public string MaSinhVien { get; set; } = string.Empty;
            public string HoTen { get; set; } = string.Empty;
            public DateTime NgaySinh { get; set; }
            public string GioiTinh { get; set; } = string.Empty;
            public string Lop { get; set; } = string.Empty;
            public string? Email { get; set; }
        }
    }
}
