# Giải pháp & Kiến trúc

## 1. Tổng quan công nghệ

| Tầng | Công nghệ | Vai trò |
|---|---|---|
| **Presentation** | ExtJS Classic MVC | Grid, Form, Window — giao tiếp qua AJAX |
| **Service** | WCF REST, .NET 4.5.1 | Expose endpoint HTTP, serialize JSON |
| **Business** | C# BLL classes | Validate, enforce business rules |
| **Data Access** | Repository + ADO.NET | SQL queries tới SQL Server |
| **Database** | SQL Server | Lưu trữ quan hệ, khóa ngoại |

---

## 2. Kiến trúc 4 tầng Backend

```mermaid
flowchart TD
    subgraph "Tầng 1 — Contract (Interface)"
        I1["IKhoaService"]
        I2["ILopService"]
        I3["ISinhvienService"]
        I4["IGiangvienService"]
        I5["IMonhocService"]
        I6["IBangdiemService"]
        I7["IInforSinhvienService"]
        I8["IInforGiangvienService"]
    end

    subgraph "Tầng 2 — Service (Implementation)"
        S1["KhoaService"]
        S2["LopService"]
        S3["SinhvienService"]
        S4["GiangVienService"]
        S5["MonHocService"]
        S6["BangdiemService"]
    end

    subgraph "Tầng 3 — Business Logic"
        B1["KhoaBLL"]
        B2["LopBLL"]
        B3["SinhvienBLL"]
        B4["GiangVienBLL"]
        B5["MonHocBLL"]
        B6["BangdiemBLL"]
    end

    subgraph "Tầng 4 — Data Access (Repository)"
        R["Repository / DAL\nADO.NET SqlConnection"]
    end

    I1 --> S1 --> B1 --> R
    I2 --> S2 --> B2 --> R
    I3 --> S3 --> B3 --> R
    I4 --> S4 --> B4 --> R
    I5 --> S5 --> B5 --> R
    I6 --> S6 --> B6 --> R
```

---

## 3. Cấu trúc thư mục Backend

```
quanlysinhvien/
├── IServices/              # Tầng Contract (WCF interface)
│   ├── IKhoaService.cs
│   ├── ILopService.cs
│   ├── ISinhvienService.cs
│   ├── IGiangvienService.cs
│   ├── IMonhocService.cs
│   ├── IBangdiemService.cs
│   ├── IInforSinhvienService.cs
│   └── IInforGiangvienService.cs
│
├── Services/               # Tầng Service (triển khai IService)
│   ├── KhoaService.cs
│   ├── LopService.cs
│   ├── SinhvienService.cs
│   ├── GiangVienService.cs
│   ├── MonHocService.cs
│   ├── BangdiemService.cs
│   └── LogService.cs
│
├── Business/               # Tầng Business Logic
│   ├── KhoaBLL.cs
│   ├── LopBLL.cs
│   ├── SinhvienBLL.cs
│   ├── GiangVienBLL.cs
│   ├── MonHocBLL.cs
│   └── BangdiemBLL.cs
│
├── Models/                 # Entity + DTO
│   ├── KHOA.cs, KHOADTO.cs
│   ├── LOP.cs,  LOPDTO.cs
│   ├── SINHVIEN.cs, SINHVIENDTO.cs
│   ├── INFORSINHVIEN.cs, INFORSINHVIENDTO.cs
│   ├── GIANGVIEN.cs, GIANGVIENDTO.cs
│   ├── INFORGIANGVIEN.cs, INFORGIANGVIENDTO.cs
│   ├── MONHOC.cs, MONHOCDTO.cs
│   └── BANGDIEM.cs, BANGDIEMDTO.cs
│
└── web.config              # WCF endpoint config, connection string
```

---

## 4. Cấu hình WCF (web.config)

```xml
<system.serviceModel>
  <behaviors>
    <serviceBehaviors>
      <behavior>
        <serviceMetadata httpGetEnabled="true"/>
        <serviceDebug includeExceptionDetailInFaults="true"/>
      </behavior>
    </serviceBehaviors>
    <endpointBehaviors>
      <behavior name="webBehavior">
        <webHttp helpEnabled="true" defaultOutgoingResponseFormat="Json"/>
      </behavior>
    </endpointBehaviors>
  </behaviors>
  <services>
    <service name="quanlysinhvien.Services.SinhvienService">
      <endpoint address="" binding="webHttpBinding"
                contract="quanlysinhvien.IServices.ISinhvienService"
                behaviorConfiguration="webBehavior"/>
    </service>
    <!-- ... các service khác tương tự ... -->
  </services>
</system.serviceModel>
```

!!! info "webHttpBinding"
    `webHttpBinding` + `webHttp` behavior = REST endpoint với JSON serialize tự động.  
    Không dùng SOAP, không dùng `basicHttpBinding`.

---

## 5. Pattern Repository

```csharp
// BLL chỉ biết về business rules, không biết SQL
public class SinhvienBLL
{
    public List<SINHVIENDTO> GetAll()
    {
        // Gọi repository — không viết SQL ở đây
        return new SinhvienRepository().GetAll();
    }

    public void Create(SINHVIENDTO dto)
    {
        // Validate nghiệp vụ
        if (string.IsNullOrEmpty(dto.MaSV))
            throw new ArgumentException("Mã SV không được rỗng");

        new SinhvienRepository().Insert(dto);
    }
}

// Repository chứa SQL thuần
public class SinhvienRepository
{
    private string _connStr = ConfigurationManager.ConnectionStrings["QlSVDB"].ConnectionString;

    public List<SINHVIENDTO> GetAll()
    {
        var result = new List<SINHVIENDTO>();
        using (var conn = new SqlConnection(_connStr))
        {
            conn.Open();
            var cmd = new SqlCommand("SELECT MaSV, TenSV, NgaySinh, GioiTinh, MaLop FROM SINHVIEN", conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new SINHVIENDTO {
                    MaSV     = reader["MaSV"].ToString(),
                    TenSV    = reader["TenSV"].ToString(),
                    NgaySinh = Convert.ToDateTime(reader["NgaySinh"]),
                    GioiTinh = Convert.ToBoolean(reader["GioiTinh"]),
                    MaLop    = reader["MaLop"].ToString()
                });
            }
        }
        return result;
    }
}
```

---

## 6. DTO Pattern

Mỗi entity có 2 class: **Entity** (ánh xạ DB) và **DTO** (truyền giữa tầng / sang client).

```csharp
// Entity — ánh xạ 1:1 với bảng DB
public class SINHVIEN
{
    public string MaSV    { get; set; }
    public string TenSV   { get; set; }
    public DateTime NgaySinh { get; set; }
    public bool GioiTinh  { get; set; }
    public string MaLop   { get; set; }
}

// DTO — [DataContract] để WCF serialize sang JSON
[DataContract]
public class SINHVIENDTO
{
    [DataMember] public string MaSV    { get; set; }
    [DataMember] public string TenSV   { get; set; }
    [DataMember] public DateTime NgaySinh { get; set; }
    [DataMember] public bool GioiTinh  { get; set; }
    [DataMember] public string MaLop   { get; set; }
}
```

!!! tip "Tại sao cần DTO?"
    DTO tách biệt cấu trúc truyền dữ liệu khỏi model DB.  
    Có thể thêm/bỏ field mà không ảnh hưởng đến bảng hoặc ngược lại.

---

## 7. Kiến trúc Frontend (ExtJS Classic MVC)

```mermaid
flowchart LR
    subgraph "ExtJS Application"
        direction TB
        M["Model\nExt.data.Model\nField definitions"]
        S["Store\nExt.data.Store\nREST proxy → WCF"]
        V["View\nExt.grid.Panel\nExt.form.Panel\nExt.window.Window"]
        C["Controller\nExt.app.Controller\nEvent handlers"]
    end

    WCF["WCF REST\nJSON"]

    M --- S
    S --- V
    V --- C
    C -->|"AJAX / store.sync()"| WCF
    WCF -->|"JSON"| S
```

### Cấu trúc module

```
app/
├── khoa/
│   ├── model.js       # Ext.data.Model: MaKhoa, TenKhoa
│   ├── view.js        # Grid + Form + Toolbar
│   └── controller.js  # Handlers: add, edit, delete, reload
├── lop/
│   ├── model.js
│   ├── view.js
│   └── controller.js
├── sinhvien/
│   ├── model.js
│   ├── view.js        # Grid SV + Form SV + Tab thông tin mở rộng
│   └── controller.js
├── giangvien/
│   └── ...
├── monhoc/
│   └── ...
└── diem/
    └── ...
```

---

## 8. Luồng dữ liệu điển hình — Thêm sinh viên

```mermaid
sequenceDiagram
    actor U as Người dùng
    participant V as ExtJS View (Form)
    participant C as ExtJS Controller
    participant WCF as WCF SinhvienService
    participant BLL as SinhvienBLL
    participant DB as SQL Server

    U->>V: Điền form + nhấn Lưu
    V->>C: onSaveClick event
    C->>C: Lấy data từ form
    C->>WCF: POST /sinhvien/Create (JSON body)
    WCF->>BLL: Create(SINHVIENDTO)
    BLL->>BLL: Validate (MaSV không rỗng, MaLop tồn tại)
    BLL->>DB: INSERT INTO SINHVIEN ...
    DB-->>BLL: OK (1 row affected)
    BLL-->>WCF: void (success)
    WCF-->>C: HTTP 200
    C->>V: store.reload() → grid refresh
    V->>U: Hiển thị sinh viên mới trong grid
```

---

## 9. Luồng dữ liệu — Xem bảng điểm sinh viên

```mermaid
sequenceDiagram
    actor U as Người dùng
    participant C as ExtJS Controller
    participant WCF as WCF SinhvienService
    participant DB as SQL Server

    U->>C: Chọn sinh viên → click "Bảng điểm"
    C->>WCF: GET /sinhvien/{maSV}/bangdiem
    WCF->>DB: SELECT d.*, m.TenMon, g.TenGV FROM DIEM d ...
    DB-->>WCF: List<BANGDIEMDTO>
    WCF-->>C: JSON array
    C->>C: Đổ data vào diemStore
    C->>U: Hiển thị grid bảng điểm
```
