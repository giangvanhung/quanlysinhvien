// App/lop/view.js
Ext.define('App.lop.view', {
    extend: 'Ext.grid.Panel',
    xtype: 'lopgrid',
    title: '🏢 Danh sách Lớp',
    height: 450,
    width: '100%',
    store: 'LopStore',
    selModel: { selType: 'rowmodel', mode: 'SINGLE' },
    columns: [
        { text: 'Mã Lớp', dataIndex: 'MaLop', width: 180 },
        { text: 'Tên Lớp', dataIndex: 'TenLop', flex: 1 },
        { text: 'Mã Khoa', dataIndex: 'MaKhoa', width: 180 }
    ],
    tbar: [
        { text: '➕ Thêm', action: 'them' },
        { text: '✏️ Sửa', action: 'sua' },
        { text: '❌ Xóa', action: 'xoa' },
        '->',
        { text: '🔄 Làm mới', action: 'lammoi' }
    ]
});