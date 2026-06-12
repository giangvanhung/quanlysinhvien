// App/giangvien/store.js
Ext.define('App.giangvien.store', {
    extend: 'Ext.data.Store',
    model: 'App.giangvien.model',
    storeId: 'GiangVienStore',
    proxy: {
        type: 'ajax',
        api: {
            read: '/GiangVienService.svc/GetAll',
            create: '/GiangVienService.svc/Create',
            update: '/GiangVienService.svc/Update',
            destroy: '/GiangVienService.svc/Delete'
        },
        reader: { type: 'json', rootProperty: 'GetAllResult' },
        writer: { type: 'json', writeAllFields: true }
    },
    autoLoad: true
});