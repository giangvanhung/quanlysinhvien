// App/khoa/view.js
Ext.define('App.khoa.view', {
    extend: 'Ext.grid.Panel',
    xtype: 'khoagrid',
    title: '🏫 Danh sách Khoa',
    height: 450,
    width: '100%',
    store: 'KhoaStore',
    selModel: { selType: 'rowmodel', mode: 'SINGLE' },
    columns: [
        { text: 'Mã Khoa', dataIndex: 'MaKhoa', width: 180 },
        { text: 'Tên Khoa', dataIndex: 'TenKhoa', flex: 1 }
    ],
    tbar: [
        { text: '➕ Thêm', action: 'them' },
        { text: '✏️ Sửa', action: 'sua' },
        { text: '❌ Xóa', action: 'xoa' },
        '->',
        { text: '🔄 Làm mới', action: 'lammoi' }
    ]
});
