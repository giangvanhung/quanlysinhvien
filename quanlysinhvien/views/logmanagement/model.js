// App/logmanagement/model.js
Ext.define('App.logmanagement.model', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'Time', type: 'string' },
        { name: 'DateTime', type: 'string' },
        { name: 'Level', type: 'string' },
        { name: 'Type', type: 'string' },
        { name: 'Message', type: 'string' }
    ]
});