// App/monhoc/view.js
Ext.define('App.monhoc.view', {
    extend: 'Ext.grid.Panel',
    xtype: 'monhocgrid',
    title: '📚 Danh sách Môn Học',
    height: 450,
    width: '100%',
    store: 'MonHocStore',
    selModel: { selType: 'rowmodel', mode: 'SINGLE' },
    columns: [
        { text: 'Mã Môn', dataIndex: 'MaMon', width: 180 },
        { text: 'Tên Môn', dataIndex: 'TenMon', flex: 1 }
    ],
    tbar: [
        { text: '➕ Thêm', action: 'them' },
        { text: '✏️ Sửa', action: 'sua' },
        { text: '❌ Xóa', action: 'xoa' },
        '->',
        { text: '🔄 Làm mới', action: 'lammoi' }
    ]
});