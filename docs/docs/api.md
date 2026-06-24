# API Reference — WCF REST Services

Tất cả services expose qua `webHttpBinding` và trả về **JSON**. Không dùng SOAP.

Base URL (ví dụ IIS Express): `http://localhost:PORT/`

---

## Danh sách Services

| Service | Base path | Mô tả |
|---|---|---|
| `KhoaService` | `/KhoaService.svc/` | CRUD Khoa |
| `LopService` | `/LopService.svc/` | CRUD Lớp |
| `SinhvienService` | `/SinhvienService.svc/` | CRUD SV + liên kết điểm/infor |
| `InforSinhvienService` | `/InforSinhvienService.svc/` | Thông tin mở rộng SV |
| `GiangVienService` | `/GiangVienService.svc/` | CRUD Giảng viên |
| `InforGiangvienService` | `/InforGiangvienService.svc/` | Thông tin mở rộng GV |
| `MonHocService` | `/MonHocService.svc/` | CRUD Môn học |
| `BangdiemService` | `/BangdiemService.svc/` | CRUD Bảng điểm |

---

## KhoaService

=== "Endpoints"
    | Method | URL | Mô tả |
    |---|---|---|
    | GET | `/KhoaService.svc/GetAll` | Lấy tất cả khoa |
    | GET | `/KhoaService.svc/GetById/{id}` | Lấy khoa theo mã |
    | POST | `/KhoaService.svc/Create` | Thêm khoa mới |
    | PUT | `/KhoaService.svc/Update/{id}` | Cập nhật khoa |
    | DELETE | `/KhoaService.svc/Delete/{id}` | Xóa khoa |

=== "Model — KHOADTO"
    ```json
    {
      "MaKhoa": "CNTT",
      "TenKhoa": "Công nghệ thông tin"
    }
    ```

=== "Ví dụ"
    ```http
    GET /KhoaService.svc/GetAll
    → [{ "MaKhoa": "CNTT", "TenKhoa": "Công nghệ thông tin" }, ...]

    POST /KhoaService.svc/Create
    Content-Type: application/json
    { "MaKhoa": "KTKT", "TenKhoa": "Kế toán kiểm toán" }
    → HTTP 200 (void)
    ```

---

## LopService

=== "Endpoints"
    | Method | URL | Mô tả |
    |---|---|---|
    | GET | `/LopService.svc/GetAll` | Lấy tất cả lớp |
    | GET | `/LopService.svc/GetById/{id}` | Lấy lớp theo mã |
    | POST | `/LopService.svc/Create` | Thêm lớp mới |
    | PUT | `/LopService.svc/Update/{id}` | Cập nhật lớp |
    | DELETE | `/LopService.svc/Delete/{id}` | Xóa lớp |

=== "Model — LOPDTO"
    ```json
    {
      "MaLop": "CT22A",
      "TenLop": "Công nghệ 22A",
      "MaKhoa": "CNTT"
    }
    ```

---

## SinhvienService

=== "Endpoints"
    | Method | URL | Mô tả |
    |---|---|---|
    | GET | `/SinhvienService.svc/GetAll` | Lấy tất cả sinh viên |
    | GET | `/SinhvienService.svc/GetById/{id}` | Lấy SV theo mã |
    | POST | `/SinhvienService.svc/Create` | Thêm SV mới |
    | PUT | `/SinhvienService.svc/Update/{id}` | Cập nhật SV |
    | DELETE | `/SinhvienService.svc/Delete/{id}` | Xóa SV |
    | GET | `/sinhvien/{maSV}/bangdiem` | Bảng điểm của SV |
    | GET | `/sinhvien/{maSV}/infor` | Thông tin mở rộng SV |

=== "Model — SINHVIENDTO"
    ```json
    {
      "MaSV": "SV001",
      "TenSV": "Nguyễn Văn An",
      "NgaySinh": "\/Date(1052956800000)\/",
      "GioiTinh": true,
      "MaLop": "CT22A"
    }
    ```

    !!! warning "DateTime serialization"
        WCF serialize `DateTime` thành `/Date(milliseconds)/` theo DataContractJsonSerializer.  
        ExtJS cần parse: `new Date(parseInt(str.match(/\d+/)[0]))`.

=== "Ví dụ"
    ```http
    GET /SinhvienService.svc/GetById/SV001
    → { "MaSV": "SV001", "TenSV": "Nguyễn Văn An", ... }

    GET /sinhvien/SV001/bangdiem
    → [{ "Id": 1, "MaSV": "SV001", "MaMon": "LTCB", "DiemSo": 8.5, "NamHoc": "2024-2025" }, ...]
    ```

---

## InforSinhvienService

=== "Endpoints"
    | Method | URL | Mô tả |
    |---|---|---|
    | GET | `/InforSinhvienService.svc/GetAll` | Tất cả thông tin mở rộng SV |
    | GET | `/InforSinhvienService.svc/GetById/{id}` | Theo Id |
    | POST | `/InforSinhvienService.svc/Create` | Thêm mới |
    | PUT | `/InforSinhvienService.svc/Update/{id}` | Cập nhật |
    | DELETE | `/InforSinhvienService.svc/Delete/{id}` | Xóa |

=== "Model — INFORSINHVIENDTO"
    ```json
    {
      "Id": 1,
      "DiaChi": "12 Trần Hưng Đạo, Hà Nội",
      "SDT": "0987654321",
      "Email": "nguyenvanan@email.com",
      "DanToc": "Kinh",
      "TonGiao": "Không",
      "MaSV": "SV001"
    }
    ```

---

## GiangVienService

=== "Endpoints"
    | Method | URL | Mô tả |
    |---|---|---|
    | GET | `/GiangVienService.svc/GetAll` | Tất cả giảng viên |
    | GET | `/GiangVienService.svc/GetById/{id}` | Theo mã GV |
    | POST | `/GiangVienService.svc/Create` | Thêm GV |
    | PUT | `/GiangVienService.svc/Update/{id}` | Cập nhật GV |
    | DELETE | `/GiangVienService.svc/Delete/{id}` | Xóa GV |

=== "Model — GIANGVIENDTO"
    ```json
    {
      "MaGV": "GV001",
      "TenGV": "Nguyễn Thị Lan",
      "NgaySinh": "\/Date(...)\/",
      "GioiTinh": false,
      "MaKhoa": "CNTT"
    }
    ```

---

## MonHocService

=== "Endpoints"
    | Method | URL | Mô tả |
    |---|---|---|
    | GET | `/MonHocService.svc/GetAll` | Tất cả môn học |
    | GET | `/MonHocService.svc/GetById/{id}` | Theo mã môn |
    | POST | `/MonHocService.svc/Create` | Thêm môn học |
    | PUT | `/MonHocService.svc/Update/{id}` | Cập nhật |
    | DELETE | `/MonHocService.svc/Delete/{id}` | Xóa |

=== "Model — MONHOCDTO"
    ```json
    {
      "MaMon": "CSDL",
      "TenMon": "Cơ sở dữ liệu"
    }
    ```

---

## BangdiemService

=== "Endpoints"
    | Method | URL | Mô tả |
    |---|---|---|
    | GET | `/BangdiemService.svc/GetAll` | Tất cả bảng điểm |
    | GET | `/BangdiemService.svc/GetById/{id}` | Theo Id |
    | POST | `/BangdiemService.svc/Create` | Nhập điểm mới |
    | PUT | `/BangdiemService.svc/Update/{id}` | Sửa điểm |
    | DELETE | `/BangdiemService.svc/Delete/{id}` | Xóa record điểm |

=== "Model — BANGDIEMDTO"
    ```json
    {
      "Id": null,
      "MaSV": "SV001",
      "MaMon": "LTCB",
      "MaGV": "GV001",
      "DiemSo": 8.5,
      "NamHoc": "2024-2025"
    }
    ```

=== "Ví dụ"
    ```http
    POST /BangdiemService.svc/Create
    Content-Type: application/json
    {
      "Id": null,
      "MaSV": "SV001",
      "MaMon": "LTCB",
      "MaGV": "GV001",
      "DiemSo": 8.5,
      "NamHoc": "2024-2025"
    }
    → HTTP 200

    PUT /BangdiemService.svc/Update/1
    Content-Type: application/json
    { "Id": 1, "DiemSo": 9.0, ... }
    → HTTP 200
    ```

---

## Xử lý lỗi

WCF trả về **HTTP 200** với exception detail trong body khi bật `includeExceptionDetailInFaults="true"`.

```json
// Lỗi — WCF exception detail
{
  "ExceptionDetail": {
    "Message": "Cannot insert duplicate key in object 'dbo.SINHVIEN'...",
    "Type": "System.Data.SqlException"
  }
}
```

!!! danger "Production"
    Tắt `includeExceptionDetailInFaults` trên production để không lộ thông tin nội bộ.  
    Thay bằng `FaultException` có message tùy chỉnh.

---

## Gọi API từ ExtJS

```javascript
// GET — load store
var store = Ext.create('Ext.data.Store', {
    model: 'App.model.SinhVien',
    proxy: {
        type: 'rest',
        url:  'http://localhost:PORT/SinhvienService.svc/',
        reader: { type: 'json', rootProperty: '' },
        api: {
            read:   'GetAll',
            create: 'Create',
            update: 'Update',
            destroy: 'Delete'
        }
    }
});
store.load();

// POST thủ công — dùng Ext.Ajax.request
Ext.Ajax.request({
    url:      'http://localhost:PORT/SinhvienService.svc/Create',
    method:   'POST',
    jsonData: { MaSV: 'SV003', TenSV: 'Lê Văn C', ... },
    success: function(resp) { store.reload(); },
    failure: function(resp) { Ext.Msg.alert('Lỗi', resp.statusText); }
});
```
