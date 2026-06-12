// App/sinhvien/model.js
Ext.define('App.sinhvien.model', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'MaSV', type: 'string' },
        { name: 'TenSV', type: 'string' },
        { name: 'NgaySinh', type: 'string' },
        { name: 'GioiTinh', type: 'string' },
        { name: 'MaLop', type: 'string' }
    ]
});