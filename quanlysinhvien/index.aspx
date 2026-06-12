<%@ Page Language="C#" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/extjs/6.2.0/classic/theme-classic/resources/theme-classic-all.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/extjs/6.2.0/ext-all.js"></script>
    <title>Quản lý sinh viên</title>
    <style>
        body {
            margin: 0;
            padding: 0;
            font-family: 'Segoe UI', Tahoma, sans-serif;
        }

        .app-container {
            height: 100vh;
            display: flex;
            flex-direction: column;
        }

        .header-custom {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            padding: 15px 30px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            box-shadow: 0 2px 10px rgba(0,0,0,0.2);
        }

        .header-title {
            font-size: 24px;
            font-weight: bold;
        }

        .search-box {
            display: flex;
            gap: 10px;
        }

        .search-input {
            padding: 10px 15px;
            border: 2px solid rgba(255,255,255,0.3);
            border-radius: 20px;
            background: rgba(255,255,255,0.9);
            width: 300px;
            font-size: 14px;
        }

            .search-input:focus {
                outline: none;
                border-color: rgba(255,255,255,0.8);
            }

        .main-content {
            flex: 1;
            padding: 30px;
            background: #f5f7fa;
            overflow: auto;
        }

        .toolbar-custom {
            background: white;
            padding: 15px 20px;
            border-radius: 8px;
            margin-bottom: 20px;
            display: flex;
            gap: 10px;
            box-shadow: 0 2px 5px rgba(0,0,0,0.05);
        }

        .btn-custom {
            padding: 10px 20px;
            border: none;
            border-radius: 6px;
            font-size: 14px;
            cursor: pointer;
            font-weight: 600;
            transition: all 0.3s;
        }

        .btn-add {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
        }

            .btn-add:hover {
                transform: translateY(-2px);
                box-shadow: 0 4px 12px rgba(102, 126, 234, 0.4);
            }

        .btn-edit {
            background: #4CAF50;
            color: white;
        }

            .btn-edit:hover {
                background: #45a049;
            }

        .btn-delete {
            background: #f44336;
            color: white;
        }

            .btn-delete:hover {
                background: #da190b;
            }

        .footer-custom {
            background: #2c3e50;
            color: white;
            padding: 15px 30px;
            text-align: center;
            font-size: 14px;
        }

        .dashboard-container {
            padding: 20px;
            max-width: 1200px;
            margin: 0 auto;
        }

        .welcome-card {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            padding: 30px;
            border-radius: 12px;
            text-align: center;
            margin-bottom: 30px;
            box-shadow: 0 4px 15px rgba(102, 126, 234, 0.3);
        }

        .welcome-icon {
            font-size: 48px;
            margin-bottom: 10px;
        }

        .welcome-title {
            font-size: 28px;
            margin: 10px 0;
            font-weight: 600;
        }

        .welcome-text {
            font-size: 16px;
            opacity: 0.9;
        }

        .stats-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
            gap: 20px;
            margin-bottom: 30px;
        }

        .stat-card {
            background: white;
            padding: 25px;
            border-radius: 12px;
            display: flex;
            align-items: center;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.08);
            transition: transform 0.3s, box-shadow 0.3s;
        }

            .stat-card:hover {
                transform: translateY(-5px);
                box-shadow: 0 4px 20px rgba(0, 0, 0, 0.12);
            }

        .stat-icon {
            font-size: 40px;
            margin-right: 15px;
        }

        .stat-number {
            font-size: 32px;
            font-weight: 700;
            margin: 0;
            color: #333;
        }

        .stat-label {
            font-size: 14px;
            color: #666;
            margin: 5px 0 0 0;
        }

        .stat-primary {
            border-left: 4px solid #667eea;
        }

        .stat-success {
            border-left: 4px solid #28a745;
        }

        .stat-info {
            border-left: 4px solid #17a2b8;
        }

        .stat-warning {
            border-left: 4px solid #ffc107;
        }

        .quick-actions {
            background: white;
            padding: 25px;
            border-radius: 12px;
            margin-bottom: 30px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.08);
        }

        .section-title {
            font-size: 20px;
            margin: 0 0 20px 0;
            color: #333;
            font-weight: 600;
        }

        .action-buttons {
            display: flex;
            gap: 15px;
            flex-wrap: wrap;
        }

        .action-btn {
            padding: 15px 25px;
            border: none;
            border-radius: 8px;
            font-size: 14px;
            cursor: pointer;
            display: flex;
            align-items: center;
            gap: 10px;
            transition: all 0.3s;
            font-weight: 500;
        }

        .btn-icon {
            font-size: 18px;
        }

        .action-btn-add {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
        }

        .action-btn-view {
            background: linear-gradient(135deg, #28a745 0%, #20c997 100%);
            color: white;
        }

        .action-btn-export {
            background: linear-gradient(135deg, #17a2b8 0%, #138496 100%);
            color: white;
        }

        .action-btn-settings {
            background: linear-gradient(135deg, #6c757d 0%, #5a6268 100%);
            color: white;
        }

        .action-btn:hover {
            transform: translateY(-2px);
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
        }

        .recent-activity {
            background: white;
            padding: 25px;
            border-radius: 12px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.08);
        }

        .activity-list {
            display: flex;
            gap: 15px;
            flex-direction: column;
        }

        .activity-item {
            display: flex;
            align-items: center;
            padding: 15px;
            background: #f8f9fa;
            border-radius: 8px;
        }

            .activity-item:hover {
                background: #e9ecef;
            }

        .activity-time {
            font-size: 12px;
            color: #666;
            margin-right: 15px;
            font-weight: 600;
        }

        .activity-content {
            display: flex;
            align-items: center;
            gap: 10px;
            font-size: 14px;
            color: #333;
        }

        .activity-icon {
            font-size: 16px;
        }

            .activity-icon.add {
                color: #28a745;
            }

            .activity-icon.update {
                color: #17a2b8;
            }

            .activity-icon.delete {
                color: #dc3545;
            }
    </style>
</head>
<body>
    <div class="app-container">
        <header class="header-custom">
            <div class="header-title">🎓 Quản lý sinh viên</div>
        </header>

        <section>
            <div style="padding: 5px 10px; background: #ecf0f1; border-bottom: 1px solid #ddd;" id="navigate-dropdown"></div>
        </section>

        <!-- Main Content -->
        <div class="main-content" id="main-content">
            <!-- Replace empty div in main-content with this -->
            <div class="dashboard-container">
                <!-- Welcome Card -->
                <div class="welcome-card">
                    <div class="welcome-icon">🎓</div>
                    <h2 class="welcome-title">Hệ Thống Quản Lý Sinh Viên</h2>
                    <p class="welcome-text">Chăm sóc và quản lý thông tin sinh viên hiệu quả</p>
                </div>

                <!-- Statistics Cards -->
                <div class="stats-grid">
                    <div class="stat-card stat-primary">
                        <div class="stat-icon">👨‍🎓</div>
                        <div class="stat-content">
                            <h3 class="stat-number" id="stat-sinhvien-number">1,245</h3>
                            <p class="stat-label">Tổng Sinh Viên</p>
                        </div>
                    </div>

                    <div class="stat-card stat-success">
                        <div class="stat-icon">🏫</div>
                        <div class="stat-content">
                            <h3 class="stat-number" id="stat-lop-number">28</h3>
                            <p class="stat-label">Số Lớp</p>
                        </div>
                    </div>

                    <div class="stat-card stat-info">
                        <div class="stat-icon">👨‍🏫</div>
                        <div class="stat-content">
                            <h3 class="stat-number" id="stat-giangvien-number">156</h3>
                            <p class="stat-label">Giảng Viên</p>
                        </div>
                    </div>

                    <div class="stat-card stat-warning">
                        <div class="stat-icon">📚</div>
                        <div class="stat-content">
                            <h3 class="stat-number" id="stat-monhoc-number">89</h3>
                            <p class="stat-label">Môn Học</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <footer class="footer-custom">
            © 2026 Hệ thống Quản lý Sinh viên - Trường Đại học - Liên hệ: support@edu.vn
        </footer>
    </div>

    <!-- Khoa -->
    <script src="views/khoa/model.js?v=<%=DateTime.Now.Ticks%>"></script>
    <script src="views/khoa/store.js?v=<%=DateTime.Now.Ticks%>"></script>
    <script src="views/khoa/view.js?v=<%=DateTime.Now.Ticks%>"></script>
    <script src="views/khoa/controller.js?v=<%=DateTime.Now.Ticks%>"></script>

    <!-- SinhVien -->
    <script src="views/sinhvien/model.js?v=<%=DateTime.Now.Ticks%>"></script>
    <script src="views/sinhvien/store.js?v=<%=DateTime.Now.Ticks%>"></script>
    <script src="views/sinhvien/view.js?v=<%=DateTime.Now.Ticks%>"></script>
    <script src="views/sinhvien/controller.js?v=<%=DateTime.Now.Ticks%>"></script>

    <!-- Lop -->
    <script src="views/lop/model.js?v=<%=DateTime.Now.Ticks%>"></script>
    <script src="views/lop/store.js?v=<%=DateTime.Now.Ticks%>"></script>
    <script src="views/lop/view.js?v=<%=DateTime.Now.Ticks%>"></script>
    <script src="views/lop/controller.js?v=<%=DateTime.Now.Ticks%>"></script>

    <!-- GiangVien -->
    <script src="views/giangvien/model.js?v=<%=DateTime.Now.Ticks%>"></script>
    <script src="views/giangvien/store.js?v=<%=DateTime.Now.Ticks%>"></script>
    <script src="views/giangvien/view.js?v=<%=DateTime.Now.Ticks%>"></script>
    <script src="views/giangvien/controller.js?v=<%=DateTime.Now.Ticks%>"></script>

    <!-- MonHoc -->
    <script src="views/monhoc/model.js?v=<%=DateTime.Now.Ticks%>"></script>
    <script src="views/monhoc/store.js?v=<%=DateTime.Now.Ticks%>"></script>
    <script src="views/monhoc/view.js?v=<%=DateTime.Now.Ticks%>"></script>
    <script src="views/monhoc/controller.js?v=<%=DateTime.Now.Ticks%>"></script>

    <!-- BangDiem -->
    <script src="views/bangdiem/model.js?v=<%=DateTime.Now.Ticks%>"></script>
    <script src="views/bangdiem/store.js?v=<%=DateTime.Now.Ticks%>"></script>
    <script src="views/bangdiem/view.js?v=<%=DateTime.Now.Ticks%>"></script>
    <script src="views/bangdiem/controller.js?v=<%=DateTime.Now.Ticks%>"></script>

    <!-- log -->
    <script src="views/logmanagement/model.js?v=<%=DateTime.Now.Ticks%>"></script>
    <script src="views/logmanagement/store.js?v=<%=DateTime.Now.Ticks%>"></script>
    <script src="views/logmanagement/view.js?v=<%=DateTime.Now.Ticks%>"></script>
    <script src="views/logmanagement/controller.js?v=<%=DateTime.Now.Ticks%>"></script>

    <script src="views/app.js?v=<%=DateTime.Now.Ticks%>"></script>

    <script>
        // Load statistics từ API
        function loadDashboardStatistics() {
            Ext.Ajax.request({
                url: '/StatisticsService.svc/GetStatistics',
                success: function (response) {
                    try {
                        var stats = JSON.parse(response.responseText);

                        // Update số lượng trong stat-card
                        Ext.get('stat-sinhvien-number')?.setHtml(stats.TotalSinhVien.toString());
                        Ext.get('stat-lop-number')?.setHtml(stats.TotalLop.toString());
                        Ext.get('stat-giangvien-number')?.setHtml(stats.TotalGiangVien.toString());
                        Ext.get('stat-monhoc-number')?.setHtml(stats.TotalMonHoc.toString());

                        // Update last updated (nếu có element)
                        var updatedEl = Ext.get('stat-last-updated');
                        if (updatedEl) {
                            updatedEl.setHtml('Cập nhật: ' + stats.LastUpdated);
                        }
                    } catch (e) {
                        console.error('Error parsing statistics:', e);
                    }
                },
                failure: function () {
                    console.warn('Failed to load statistics');
                }
            });
        }

        // Initialize khi app ready
        Ext.onReady(function () {
            // Load statistics khi vào dashboard
            loadDashboardStatistics();
        });
    </script>
</body>
</html>
