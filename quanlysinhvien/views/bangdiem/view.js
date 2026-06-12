// App/bangdiem/view.js
Ext.define('App.bangdiem.view', {
    extend: 'Ext.grid.Panel',
    xtype: 'bangdiemgrid',
    title: '📊 Bảng Điểm',
    height: 450,
    width: '100%',
    store: 'BangDiemStore',
    selModel: { selType: 'rowmodel', mode: 'SINGLE' },
    columns: [
        { text: 'Sinh Viên', dataIndex: 'MaSV', width: 130 },
        { text: 'Môn học', dataIndex: 'MaMon', width: 130 },
        { text: 'Giảng viên', dataIndex: 'MaGV', width: 130 },
        {
            text: 'Điểm Số', dataIndex: 'DiemSo', width: 100,
            renderer: function (val) { return Ext.util.Format.number(val, '0.00'); }
        },
        { text: 'Năm Học', dataIndex: 'NamHoc', flex: 1 }
    ],
    tbar: [
        { text: '➕ Thêm', action: 'them' },
        { text: '✏️ Sửa', action: 'sua' },
        { text: '❌ Xóa', action: 'xoa' },
        '->',
        { text: '🔄 Làm mới', action: 'lammoi' }
    ]
});