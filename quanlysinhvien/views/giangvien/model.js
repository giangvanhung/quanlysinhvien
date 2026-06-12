// App/giangvien/model.js
Ext.define('App.giangvien.model', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'MaGV', type: 'string' },
        { name: 'TenGV', type: 'string' },
        { name: 'NgaySinh', type: 'string'},
        { name: 'GioiTinh', type: 'boolean' },
        { name: 'MaKhoa', type: 'string' }
    ]
});