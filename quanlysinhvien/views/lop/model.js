// App/lop/model.js
Ext.define('App.lop.model', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'MaLop', type: 'string' },
        { name: 'TenLop', type: 'string' },
        { name: 'MaKhoa', type: 'string' }
    ]
});