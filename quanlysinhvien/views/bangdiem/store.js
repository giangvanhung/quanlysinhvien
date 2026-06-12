// App/bangdiem/store.js
Ext.define('App.bangdiem.store', {
    extend: 'Ext.data.Store',
    model: 'App.bangdiem.model',
    storeId: 'BangDiemStore',
    proxy: {
        type: 'ajax',
        api: {
            read: '/BangDiemService.svc/GetAll',
            create: '/BangDiemService.svc/Create',
            update: '/BangDiemService.svc/Update',
            destroy: '/BangDiemService.svc/Delete'
        },
        reader: { type: 'json', rootProperty: 'GetAllResult' },
        writer: { type: 'json', writeAllFields: true }
    },
    autoLoad: true
});