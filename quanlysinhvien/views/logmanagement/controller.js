// App/logmanagement/controller.js
Ext.define('App.logmanagement.controller', {
    extend: 'Ext.app.Controller',
    init: function () {
        this.control({
            'logmanagement': {
                afterrender: this.onAfterRender
            }
        });
    },

    onAfterRender: function (view) {
        view.refreshGrid();
    }
});