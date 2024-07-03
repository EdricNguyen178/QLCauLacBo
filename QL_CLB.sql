create database DA_QL_CLB
go

create table Team
(
	MaTeam nvarchar(10) not null,
	TenTeam nvarchar(20),
	constraint PK_MaTeam primary key (MaTeam),
)

create table ThanhVien
(
	MaSV nvarchar(10) not null,
	HoTenSV nvarchar(30),
	GioiTinh nvarchar(10),
	NgaySinh date,
	ChucVu nvarchar(50),
	MaTeam nvarchar(10) not null,
	SDT nvarchar(20),
	NgayVaoCLB date,
	MatKhau nvarchar(1000),
	Anh nvarchar(1000),

	constraint PK_MaSV primary key (MaSV),
	constraint FK_TV_TEAM foreign key (MaTeam) references Team(MaTeam)
)

create table HoatDong
(
	MaHD nvarchar(10) not null,
	TenHD  nvarchar(20),
	NoiDungHD nvarchar(1000),
	MaTeam nvarchar(10) not null,
	NgayThucHien date,
	DiaDiem nvarchar(50),

	constraint PK_MaHD primary key (MaHD),
	constraint FK_HD_Team foreign key (MaTeam) references Team(MaTeam)
)

create table PhanCongViec
(
	MaHD nvarchar(10) not null,
	MaSV nvarchar(10) not null,
	NoiDungCV nvarchar(1000),

	constraint PK_MAHD_MASV primary key (MaHD,MaSV),
	constraint FK_PCV_TV foreign key (MaSV) references ThanhVien(MaSV),
	constraint FK_PCV_HD foreign key (MaHD) references HoatDong(MaHD)
)
create table TKDangDN(
	    MaSV nvarchar(11) not null,
		constraint pk_TKDangDN primary key (MaSV),
)
create table inHD(
	    MaHD nvarchar(11) not null,
)
INSERT INTO Team
VALUES 
('T1',N'Team Kỹ Năng'),
('T2',N'Team Văn Nghệ'),
('T3',N'Team MC-Hoạt Náo'),
('T4',N'Team Truyển Thông');



set dateformat DMY
INSERT INTO ThanhVien
VALUES 
('SV001',N'Dương Hoàng Hiếu','Nam','07/04/2001',N'Phó Chủ Nhiệm CLB','T3','0961023517','08/10/2020','3244185981728979115075721453575112',N'C:Anh/HieuDepTrai.png'),
('SV002',N'Đỗ Văn Nhân','Nam','12/07/1999',N'Phó Chủ Nhiệm CLB','T4','0756213556','11/01/2019','3244185981728979115075721453575112',N'C:Anh/Nhan.png'),
('SV004',N'Lê Nguyễn Thùy Ngân','Nữ','25/06/2001',N'Chủ Nhiệm CLB','T2','0123415236','05/08/2020','3244185981728979115075721453575112',N'C:Anh/Ngan.png'),
('SV005',N'Nguyễn Thúy Nhi','Nữ','23/09/2000',N'Thành Viên CLB','T1','0965234117','12/02/2020','3244185981728979115075721453575112',N'C:Anh/nhi.png'),
('SV013',N'Nguyễn Văn Định','Nam','25/12/2001',N'Thành Viên CLB','T3','068651230','14/02/2019','3244185981728979115075721453575112',N'C:Anh/dinh.png'),
('SV012',N'Nguyễn Trọng Hiếu','Nam','23/10/2001',N'Cố Vấn CLB','T3','0965666851','05/02/2018','3244185981728979115075721453575112',N'C:Anh/daidien.png'),
('SV015',N'Dương Minh Huy','Nam','23/03/2001',N'Cố Vấn CLB','T4','0961745657','10/11/2019','3244185981728979115075721453575112',N'C:Anh/daidien.png'),
('SV014',N'Huỳnh Kiều Khuyên','Nữ','15/04/2001',N'Trưởng Team Kỹ Năng','T1','0942343517','12/03/2020','3244185981728979115075721453575112',N'C:Anh/daidien.png'),
('SV019',N'Mai Thị Đào','Nữ','16/09/2000',N'Phó Team Truyền Thông','T4','0456812357','19/07/2019','3244185981728979115075721453575112',N'C:Anh/daidien.png'),
('SV020',N'Tuyết Vân','Nữ','18/04/2000',N'Thành Viên CLB','T4','0456844357','20/07/2019','3244185981728979115075721453575112',N'C:Anh/daidien.png'),
('SV010',N'Trịnh Công Văn','Nam','12/11/1999',N'Phó Team Văn Nghệ','T2','0155167581','23/01/2020','3244185981728979115075721453575112',N'C:Anh/daidien.png');


INSERT INTO HoatDong
VALUES 
('HD001',N'Sinh Hoạt Tuần',N'Sinh Hoạt Kỹ Năng','T1','20/08/2021',N'Bồn Nước Nhà C'),
('HD002',N'Sinh Hoạt Tuần',N'Trò Chơi Lớn','T3','19/09/2021',N'Bồn Nước Nhà C'),
('HD003',N'Haloween CLB',N'Hóa trang haloween','T1','10/10/2021',N'Bồn Nước Nhà C'),
('HD004',N'Noel CLB',N'Tặng quà cho nhau','T2','24/12/2021',N'Bồn Nước Nhà C'),
('HD005',N'Sinh Hoạt Tuần',N'Cafe chuyên đề','T4','20/11/2021',N'The Tree Coffee');

INSERT INTO PhanCongViec
VALUES
('HD001','SV014',N'Lập Bản Kế Hoạch chi tiết'),
('HD001','SV005',N'Hướng dẫn Sinh Hoạt Kỹ Năng'),
('HD001','SV020',N'Chụp ảnh Hoạt Động'),
('HD001','SV013',N'MC chương trình'),
('HD002','SV020',N'Chụp ảnh Hoạt Động'),
('HD002','SV013',N'Quản Trò'),
('HD002','SV001',N'Giám Sát Hoạt Động'),
('HD002','SV019',N'Chụp ảnh Hoạt Động'),
('HD003','SV004',N'Lập Bản Kế Hoạch chi tiết'),
('HD003','SV019',N'Chụp ảnh Hoạt Động');

delete  from TKDangDN;
SELECT ThanhVien.* FROM ThanhVien INNER JOIN TKDangDN ON  ThanhVien.MaSV=TKDangDN.MaSV
select tv.MaSV,tv.HoTenSV,tv.GioiTinh,tv.NgaySinh,tv.ChucVu,tv.MaTeam,tv.SDT,tv.NgayVaoCLB,tv.Anh from ThanhVien tv
select tv.MaSV,tv.HoTenSV,tv.GioiTinh,tv.NgaySinh,tv.ChucVu,tv.MaTeam,tv.SDT,tv.NgayVaoCLB,tv.Anh from ThanhVien tv where tv.MaTeam like 'T1'
select COUNT(*) as dem from TKDangDN
select COUNT(*) as dem from TKDangDN
delete  from TKDangDN;
UPDATE ThanhVien SET MatKhau = '123' WHERE MaSV = (SELECT * FROM TKDangDN)
select tv.MaSV,tv.HoTenSV,tv.GioiTinh,tv.NgaySinh,tv.ChucVu,tv.MaTeam,tv.SDT,tv.NgayVaoCLB,tv.Anh from ThanhVien tv where tv.HoTenSV like N'%hiếu%'
select hd.MaHD,hd.TenHD,hd.NgayThucHien,hd.DiaDiem,tv.MaSV,tv.HoTenSV,pc.NoiDungCV from ThanhVien tv, PhanCongViec pc,inHD ,HoatDong hd where tv.MaSV=pc.MaSV and pc.MaHD=hd.MaHD and hd.MaHD=inHD.MaHD 