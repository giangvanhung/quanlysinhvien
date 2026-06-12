// App/sinhvien/view.js
Ext.define('App.sinhvien.view', {
    extend: 'Ext.grid.Panel',
    xtype: 'sinhviengrid',
    title: '🎓 Danh sách Sinh Viên',
    height: 450,
    width: '100%',
    store: 'SinhVienStore',
    selModel: { selType: 'rowmodel', mode: 'SINGLE' },
    columns: [
        { xtype: 'rownumberer'},
        { text: 'Mã SV',     dataIndex: 'MaSV',     width: 120 },
        { text: 'Tên SV', dataIndex: 'TenSV', locked: true, flex: 1, width: 220   },
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
        { text: 'Mã Lớp',   dataIndex: 'MaLop',    width: 120 }
    ],
    tbar: [
        { text: '➕ Thêm',     action: 'them'   },
        { text: '✏️ Sửa',     action: 'sua'    },
        { text: '❌ Xóa', action: 'xoa' },
        { text: '🔍 Xem thông tin', action: 'infor' },
        { text: '📊 Xem Bảng điểm', action: 'bdShow' },
        { text: '🔄 Làm mới', action: 'lammoi' },
        '->',
        {
            width: 400,
            labelWidth: 50,
            xtype: 'textfield',
            emptyText: '🔍 Tìm kiếm sinh viên...',
            enableKeyEvents: true,
            triggers: {
                search: {
                    cls: 'x-form-search-trigger' 
                },
                clear: {
                    cls: 'x-form-clear-trigger',
                    handler: function () {
                        this.setValue('');
                        Ext.data.StoreManager.lookup('SinhVienStore').clearFilter();
                    }
                }
            },
            action: 'search'
        }
    ],
    bbar: {
        xtype: 'pagingtoolbar',
        displayInfo: true,
        displayMsg: 'Topics {0} - {1} of {2}',
        emptyMsg: 'No topics to display'
    },
});