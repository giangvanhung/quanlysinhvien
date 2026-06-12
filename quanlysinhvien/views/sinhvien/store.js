// App/sinhvien/store.js
Ext.define('App.sinhvien.store', {
    extend: 'Ext.data.Store',
    model: 'App.sinhvien.model',
    storeId: 'SinhVienStore',
    proxy: {
        type: 'ajax',
        api: {
            read: '/SinhvienService.svc/GetAll',
            create: '/SinhvienService.svc/Create',
            update: '/SinhvienService.svc/Update',
            destroy: '/SinhvienService.svc/Delete'
        },
        reader: { type: 'json', rootProperty: 'GetAllResult' },
        writer: { type: 'json', writeAllFields: true }
    },
    autoLoad: true
});