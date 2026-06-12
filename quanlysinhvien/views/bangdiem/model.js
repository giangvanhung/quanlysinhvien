// App/bangdiem/model.js
Ext.define('App.bangdiem.model', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'Id', type: 'int' },
        { name: 'MaSV', type: 'string' },
        { name: 'MaMon', type: 'string' },
        { name: 'MaGV', type: 'string' },
        { name: 'DiemSo', type: 'float' },
        { name: 'NamHoc', type: 'string' }
    ]
});