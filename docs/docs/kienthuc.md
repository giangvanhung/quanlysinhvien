# Kiến Thức

Trang này tổng hợp các kiến thức nền tảng và ghi chú kỹ thuật đã sử dụng trong quá trình xây dựng hệ thống Quản lý sinh viên, bao gồm cơ sở dữ liệu, backend (C#, WCF), frontend (ExtJS) và quy trình phát triển.

---

## 1. Kiến thức về cơ sở dữ liệu

- Thiết kế CSDL với MS SQL Server: tạo bảng, khóa chính, khóa ngoại, định nghĩa quan hệ giữa Khoa – Lớp – Sinh viên – Môn học – Điểm.  
- Viết câu lệnh SQL cơ bản: `SELECT`, `INSERT`, `UPDATE`, `DELETE` để thao tác dữ liệu.  
- Normalization cơ bản: tách các bảng SINHVIEN, LOP, KHOA, MONHOC, DIEM, INFORSINHVIEN, INFORGIANGVIEN nhằm giảm trùng lặp dữ liệu và đảm bảo toàn vẹn tham chiếu.

---

## 2. Kiến thức về backend (C#, WCF)

- Xây dựng DTO (Data Transfer Object) để truyền dữ liệu giữa các lớp và giữa WCF service với client, tránh phụ thuộc trực tiếp vào entity của CSDL.  
- Áp dụng Repository Pattern: tách code truy cập dữ liệu (SQL/ADO.NET) khỏi service, giúp dễ test, dễ bảo trì và thay đổi cách lưu trữ khi cần.  
- Tạo WCF Service: định nghĩa service contract (interface, ví dụ `IDataService`), cài đặt trong lớp `DataService` và cấu hình endpoint, binding, behavior trong `web.config`.  
- Deploy WCF Service lên IIS: tạo Application Pool, cấu hình Virtual Directory / Application, trỏ tới thư mục chứa service và cấu hình binding phù hợp (HTTP).  
- Xử lý connection string và lỗi kết nối SQL: cấu hình connection string trong `web.config`, bắt và log các lỗi khi kết nối CSDL để dễ debug.

---

## 3. Kiến thức về frontend (ExtJS)

- Tạo ExtJS Store kết nối tới WCF Service với proxy (ví dụ `proxy: { type: 'rest', url: '...' }`), cấu hình reader/writer để ánh xạ JSON ↔ model.  
- Xây dựng Grid Panel: định nghĩa các cột, hỗ trợ phân trang (paging), tìm kiếm, sắp xếp và hiển thị danh sách sinh viên, lớp, khoa, v.v.  
- Tạo Form Panel: cấu hình field, validation, xử lý submit và reset form, kết hợp với store/controller để thêm/sửa dữ liệu.  
- Module hóa ứng dụng ExtJS: tổ chức theo mô hình MVC (app/khoa/model.js, view.js, controller.js, …), đăng ký các module và tích hợp vào menu chung của hệ thống.

---

## 4. Kiến thức về quy trình phát triển

- Full-stack workflow: thiết kế CSDL → tạo DTO → xây dựng Repository/DAL → triển khai WCF Service → kết nối từ ExtJS thông qua store/proxy.  
- Kỹ thuật debug: dùng WCF Test Client/Fiddler/Postman để kiểm tra service, DevTools/Console cho ExtJS, SQL Server Profiler để theo dõi truy vấn.  
- Version control: sử dụng Git để quản lý mã nguồn, commit theo từng chức năng, giúp theo dõi lịch sử thay đổi và làm việc nhóm.  
- Viết báo cáo kỹ thuật: trình bày bài toán, phân tích yêu cầu, mô tả giải pháp, trích dẫn code mẫu quan trọng và minh họa kết quả chạy chương trình.

---

## 5. Kiến thức bổ sung

- Logging: tìm hiểu và áp dụng log4net (hoặc thư viện tương tự), cấu hình appender, logger để ghi log ở các lớp Service/Repository.  
- Bảo mật: nghiên cứu các khái niệm Authentication, Authorization cho WCF và ứng dụng web (chưa triển khai trong phạm vi dự án).  
- ORM: tìm hiểu các công cụ ORM như Entity Framework, Dapper để thay thế ADO.NET thuần trong tương lai (chưa áp dụng trong dự án hiện tại).  
- Best practices: áp dụng các nguyên tắc như separation of concerns, dependency injection, SOLID để mã nguồn dễ đọc, dễ test và dễ mở rộng.
