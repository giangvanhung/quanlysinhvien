// App/monhoc/store.js
Ext.define('App.monhoc.store', {
    extend: 'Ext.data.Store',
    model: 'App.monhoc.model',
    storeId: 'MonHocStore',
    proxy: {
        type: 'ajax',
        api: {
            read: '/MonHocService.svc/GetAll',
            create: '/MonHocService.svc/Create',
            update: '/MonHocService.svc/Update',
            destroy: '/MonHocService.svc/Delete'
        },
        reader: { type: 'json', rootProperty: 'GetAllResult' },
        writer: { type: 'json', writeAllFields: true }
    },
    autoLoad: true
});