// App/khoa/store.js
Ext.define('App.khoa.store', {
    extend: 'Ext.data.Store',
    model: 'App.khoa.model',
    storeId: 'KhoaStore',
    proxy: {
        type: 'ajax',
        api: {
            read: '/KhoaService.svc/GetAll',
            create: '/KhoaService.svc/Create',
            update: '/KhoaService.svc/Update',
            destroy: '/KhoaService.svc/Delete'
        },
        reader: { type: 'json', rootProperty: 'GetAllResult' },
        writer: { type: 'json', writeAllFields: true }
    },
    autoLoad: true
});
