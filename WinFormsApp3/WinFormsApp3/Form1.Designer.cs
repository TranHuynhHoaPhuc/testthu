namespace WinFormsApp3
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblMaSinhVien = null!;
        private Label lblHoTen = null!;
        private Label lblNgaySinh = null!;
        private Label lblGioiTinh = null!;
        private Label lblLop = null!;
        private Label lblEmail = null!;
        private TextBox txtMaSinhVien = null!;
        private TextBox txtHoTen = null!;
        private DateTimePicker dtpNgaySinh = null!;
        private ComboBox cboGioiTinh = null!;
        private TextBox txtLop = null!;
        private TextBox txtEmail = null!;
        private Button btnThem = null!;
        private Button btnSua = null!;
        private Button btnXoa = null!;
        private Button btnLuu = null!;
        private Button btnHuy = null!;
        private Button btnLamMoi = null!;
        private DataGridView dgvSinhVien = null!;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblMaSinhVien = new Label();
            lblHoTen = new Label();
            lblNgaySinh = new Label();
            lblGioiTinh = new Label();
            lblLop = new Label();
            lblEmail = new Label();
            txtMaSinhVien = new TextBox();
            txtHoTen = new TextBox();
            dtpNgaySinh = new DateTimePicker();
            cboGioiTinh = new ComboBox();
            txtLop = new TextBox();
            txtEmail = new TextBox();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLuu = new Button();
            btnHuy = new Button();
            btnLamMoi = new Button();
            dgvSinhVien = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvSinhVien).BeginInit();
            SuspendLayout();

            Text = "QUẢN LÝ SINH VIÊN";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(1080, 650);
            MinimumSize = new Size(900, 550);

            ConfigureLabel(lblMaSinhVien, "Mã sinh viên", 35, 30);
            ConfigureLabel(lblHoTen, "Họ và tên", 35, 75);
            ConfigureLabel(lblNgaySinh, "Ngày sinh", 35, 120);
            ConfigureLabel(lblGioiTinh, "Giới tính", 35, 165);
            ConfigureLabel(lblLop, "Lớp", 530, 30);
            ConfigureLabel(lblEmail, "Email", 530, 75);

            ConfigureTextBox(txtMaSinhVien, 150, 25, 300);
            ConfigureTextBox(txtHoTen, 150, 70, 300);
            dtpNgaySinh.Location = new Point(150, 115);
            dtpNgaySinh.Size = new Size(300, 27);
            dtpNgaySinh.Format = DateTimePickerFormat.Short;
            dtpNgaySinh.MaxDate = DateTime.Today;

            cboGioiTinh.Location = new Point(150, 160);
            cboGioiTinh.Size = new Size(300, 28);
            cboGioiTinh.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
            cboGioiTinh.SelectedIndex = 0;

            ConfigureTextBox(txtLop, 620, 25, 300);
            ConfigureTextBox(txtEmail, 620, 70, 300);

            ConfigureButton(btnThem, "Thêm", 35, 220);
            ConfigureButton(btnSua, "Sửa", 145, 220);
            ConfigureButton(btnXoa, "Xóa", 255, 220);
            ConfigureButton(btnLuu, "Lưu", 365, 220);
            ConfigureButton(btnHuy, "Hủy", 475, 220);
            ConfigureButton(btnLamMoi, "Làm mới", 585, 220);

            dgvSinhVien.Location = new Point(35, 275);
            dgvSinhVien.Size = new Size(1000, 330);
            dgvSinhVien.AllowUserToAddRows = false;
            dgvSinhVien.AllowUserToDeleteRows = false;
            dgvSinhVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSinhVien.BackgroundColor = SystemColors.Window;
            dgvSinhVien.ReadOnly = true;
            dgvSinhVien.RowHeadersVisible = false;
            dgvSinhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSinhVien.MultiSelect = false;
            dgvSinhVien.SelectionChanged += dgvSinhVien_SelectionChanged;

            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLuu.Click += btnLuu_Click;
            btnHuy.Click += btnHuy_Click;
            btnLamMoi.Click += btnLamMoi_Click;

            Controls.AddRange(new Control[]
            {
                lblMaSinhVien, lblHoTen, lblNgaySinh, lblGioiTinh, lblLop, lblEmail,
                txtMaSinhVien, txtHoTen, dtpNgaySinh, cboGioiTinh, txtLop, txtEmail,
                btnThem, btnSua, btnXoa, btnLuu, btnHuy, btnLamMoi, dgvSinhVien
            });

            ((System.ComponentModel.ISupportInitialize)dgvSinhVien).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private static void ConfigureLabel(Label label, string text, int x, int y)
        {
            label.AutoSize = true;
            label.Location = new Point(x, y + 5);
            label.Text = text;
        }

        private static void ConfigureTextBox(TextBox textBox, int x, int y, int width)
        {
            textBox.Location = new Point(x, y);
            textBox.Size = new Size(width, 27);
        }

        private static void ConfigureButton(Button button, string text, int x, int y)
        {
            button.Location = new Point(x, y);
            button.Size = new Size(95, 35);
            button.Text = text;
            button.UseVisualStyleBackColor = true;
        }

        #endregion
    }
}
