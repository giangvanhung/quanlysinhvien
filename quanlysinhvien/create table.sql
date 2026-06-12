create table KHOA
(
    MaKhoa char(15) primary key,
    TenKhoa nvarchar(100) not null
)

create table LOP(
    MaLop char(15) primary key,
    TenLop varchar(100) not null,
    MaKhoa char(15) not null,
    foreign key (MaKhoa) references KHOA(MaKhoa)
)

create table MONHOC(
    MaMon char(15) primary key,
    TenMon nvarchar(100) not null
)

create table SINHVIEN(
    MaSV char(15) primary key,
    TenSV nvarchar(100) not null,
    NgaySinh date not null,
    GioiTinh bit not null,
    MaLop char(15) not null,
    foreign key (MaLop) references LOP(MaLop)
)

create table INFORSINHVIEN(
    Id int identity(1,1) primary key,
    DiaChi nvarchar(200) not null,
    SDT char(15) not null,
    Email varchar(100) not null,
    DanToc nvarchar(50) not null,
    TonGiao nvarchar(50) null,
    MaSV char(15) not null,
    foreign key (MaSV) references SINHVIEN(MaSV)
)

create table GIANGVIEN(
    MaGV char(15) primary key,
    TenGV nvarchar(100) not null,
    NgaySinh date not null,
    GioiTinh bit not null,
    MaKhoa char(15) not null,
    foreign key (MaKhoa) references KHOA(MaKhoa)
)

create table INFORGIANGVIEN(
    Id int identity(1,1) primary key,
    DiaChi nvarchar(200) not null,
    SDT char(15) not null,
    Email varchar(100) not null,
    DanToc nvarchar(50) not null,
    TonGiao nvarchar(50) null,
    MaGV char(15) not null,
    foreign key (MaGV) references GIANGVIEN(MaGV)
)

create table DIEM(
    Id int identity(1,1) primary key,
    MaSV char(15) not null,
    MaMon char(15) not null,
    MaGV char(15) not null,
    DiemSo float not null,
    NamHoc char(9) not null,
    foreign key (MaSV) references SINHVIEN(MaSV),
    foreign key (MaMon) references MONHOC(MaMon),
    foreign key (MaGV) references GIANGVIEN(MaGV)
)
