// App/giangvien/view.js
Ext.define('App.giangvien.view', {
    extend: 'Ext.grid.Panel',
    xtype: 'giangviengrid',
    title: '👨‍🏫 Danh sách Giảng Viên',
    height: 450,
    width: '100%',
    store: 'GiangVienStore',
    selModel: { selType: 'rowmodel', mode: 'SINGLE' },
    columns: [
        { text: 'Mã GV', dataIndex: 'MaGV', width: 120 },
        { text: 'Tên GV', dataIndex: 'TenGV', flex: 1 },
        {
            text: 'Ngày Sinh', dataIndex: 'NgaySinh', width: 130,
            renderer: function (val) {
                if (!val) return '';
                var datePart = val.split(' ')[0]; 
                return datePart;
            }
        },
        {
            text: 'Giới Tính', dataIndex: 'GioiTinh', width: 100,
            renderer: function (val) { return val ? 'Nam' : 'Nữ'; }
        },
        { text: 'Mã Khoa', dataIndex: 'MaKhoa', width: 120 }
    ],
    tbar: [
        { text: '➕ Thêm', action: 'them' },
        { text: '✏️ Sửa', action: 'sua' },
        { text: '❌ Xóa', action: 'xoa' },
        '->',
        { text: '🔄 Làm mới', action: 'lammoi' }
    ]
});