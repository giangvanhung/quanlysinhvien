// App/lop/store.js
Ext.define('App.lop.store', {
    extend: 'Ext.data.Store',
    model: 'App.lop.model',
    storeId: 'LopStore',
    proxy: {
        type: 'ajax',
        api: {
            read: '/LopService.svc/GetAll',
            create: '/LopService.svc/Create',
            update: '/LopService.svc/Update',
            destroy: '/LopService.svc/Delete'
        },
        reader: { type: 'json', rootProperty: 'GetAllResult' },
        writer: { type: 'json', writeAllFields: true }
    },
    autoLoad: true
});