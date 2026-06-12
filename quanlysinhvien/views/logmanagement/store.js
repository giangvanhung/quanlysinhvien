Ext.define('App.logmanagement.store', {
    extend: 'Ext.data.Store',
    model: 'App.logmanagement.model',
    storeId: 'LogManagementStore',
    pageSize: 20,
    proxy: {
        type: 'ajax',
        url: '/LogService.svc/GetLogs',
        startParam: 'page',
        limitParam: 'pageSize',
        reader: {
            type: 'json',
            rootProperty: 'data',   // ✅ khớp với field trong LogPagedResultDTO
            totalProperty: 'total'  // ✅ ExtJS đọc field này để tính số trang
        },
        extraParams: {
            filter: '',
            startDate: '',
            endDate: ''
        }
    }
});