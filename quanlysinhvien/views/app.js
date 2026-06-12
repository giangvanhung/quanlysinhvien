// app.js
Ext.application({
    name: 'App',

    controllers: [
        'App.khoa.controller',
        'App.sinhvien.controller',
        'App.lop.controller',
        'App.giangvien.controller',
        'App.monhoc.controller',
        'App.bangdiem.controller',
        'App.logmanagement.controller'
    ],

    stores: [
        'App.khoa.store',
        'App.sinhvien.store',
        'App.lop.store',
        'App.giangvien.store',
        'App.monhoc.store',
        'App.bangdiem.store',
        'App.logmanagement.store'
    ],

    launch: function () {
        Ext.Ajax.setDefaultHeaders({
            'Content-Type': 'application/json',
            'Accept': 'application/json'
        });

        // ============================================================
        // Khởi tạo tất cả store — đăng ký vào StoreManager
        // ============================================================
        //Ext.create('App.khoa.store');
        //Ext.create('App.sinhvien.store');
        //Ext.create('App.lop.store');
        //Ext.create('App.giangvien.store');
        //Ext.create('App.monhoc.store');
        //Ext.create('App.bangdiem.store');
        //Ext.create('App.logmanagement.store');

        // ============================================================
        // TIỆN ÍCH CHUNG
        // ============================================================
        function clearMain() {
            Ext.ComponentQuery.query('[renderTo=main-content]').forEach(function (c) {
                c.destroy();
            });
            Ext.get('main-content').setHtml('');
        }

        function loadModule(viewClass, storeId) {
            clearMain();
            if (storeId) {
                var store = Ext.data.StoreManager.lookup(storeId);
                if (store) {
                    store.load();
                }
            }
            Ext.create(viewClass, { renderTo: 'main-content' });
        }

        // ============================================================
        // NAVIGATION HEADER
        // ============================================================
        Ext.create('Ext.container.Container', {
            renderTo: 'navigate-dropdown',
            layout: { type: 'hbox', align: 'middle' },
            items: [
                {
                    xtype: 'button',
                    text: 'Home',
                    style: 'height:40px; width:120px; border:1px solid rgba(0,0,0,0.5); margin-right:8px',
                    handler: function () {
                        window.location.href = '';
                    }
                },
                {
                    xtype: 'button',
                    text: '🏫 Khoa',
                    style: 'height:40px; width:120px; border:1px solid rgba(0,0,0,0.5); margin-right:8px',
                    handler: function () {
                        loadModule('App.khoa.view', 'KhoaStore');
                    }
                },
                {
                    xtype: 'button',
                    text: '🏢 Lớp',
                    style: 'height:40px; width:120px; border:1px solid rgba(0,0,0,0.5); margin-right:8px',
                    handler: function () {
                        loadModule('App.lop.view', 'LopStore');
                    }
                },
                {
                    xtype: 'button',
                    text: '📚 Môn Học',
                    style: 'height:40px; width:120px; border:1px solid rgba(0,0,0,0.5); margin-right:8px',
                    handler: function () {
                        loadModule('App.monhoc.view', 'MonHocStore');
                    }
                },
                {
                    xtype: 'button',
                    text: '👨‍🏫 Giảng Viên',
                    style: 'height:40px; width:120px; border:1px solid rgba(0,0,0,0.5); margin-right:8px',
                    handler: function () {
                        loadModule('App.giangvien.view', 'GiangVienStore');
                    }
                },
                {
                    xtype: 'button',
                    text: '🎓 Sinh Viên',
                    style: 'height:40px; width:120px; border:1px solid rgba(0,0,0,0.5); margin-right:8px',
                    handler: function () {
                        loadModule('App.sinhvien.view', 'SinhVienStore');
                    }
                },
                {
                    xtype: 'button',
                    text: '📊 Bảng Điểm',
                    style: 'height:40px; width:120px; border:1px solid rgba(0,0,0,0.5); margin-right:8px',
                    handler: function () {
                        loadModule('App.bangdiem.view', 'BangDiemStore');
                    }
                },
                {
                    xtype: 'button',
                    text: '📋 Quản Lý Log',
                    style: 'height:40px; width:120px; border:1px solid rgba(0,0,0,0.5); margin-right:8px',
                    handler: function () {
                        loadModule('App.logmanagement.view', 'LogManagementStore');
                    }
                },
                {
                    xtype: 'button',
                    text: '⚙️ Cài đặt',
                    style: 'height:40px; width:120px; border:1px solid rgba(0,0,0,0.5)',
                    handler: function () {
                        Ext.Msg.alert('Thông báo', 'Đang phát triển...');
                    }
                }
            ]
        });
    }
});