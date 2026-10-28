IF DB_ID(N'QuanLySinhVien') IS NULL
BEGIN
	CREATE DATABASE QuanLySinhVien;
END
GO

USE QuanLySinhVien;
GO

IF OBJECT_ID(N'dbo.SinhVien', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.SinhVien
	(
		MaSinhVien VARCHAR(20) NOT NULL CONSTRAINT PK_SinhVien PRIMARY KEY,
		HoTen NVARCHAR(100) NOT NULL,
		NgaySinh DATE NOT NULL,
		GioiTinh NVARCHAR(10) NOT NULL,
		Lop VARCHAR(20) NOT NULL,
		Email VARCHAR(100) NULL,
		CONSTRAINT CK_SinhVien_GioiTinh CHECK (GioiTinh IN (N'Nam', N'Nữ', N'Khác'))
	);
END
GO

-- Dữ liệu mẫu, chỉ thêm nếu bảng đang rỗng.
IF NOT EXISTS (SELECT 1 FROM dbo.SinhVien)
BEGIN
	INSERT INTO dbo.SinhVien (MaSinhVien, HoTen, NgaySinh, GioiTinh, Lop, Email)
	VALUES
		('SV001', N'Nguyễn Văn An', '2005-05-10', N'Nam', 'CNTT1', 'an@example.com'),
		('SV002', N'Trần Thị Bình', '2005-09-20', N'Nữ', 'CNTT1', 'binh@example.com');
END
GO
