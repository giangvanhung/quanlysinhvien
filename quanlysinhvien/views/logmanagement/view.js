// App/logmanagement/view.js
Ext.define('App.logmanagement.view', {
    extend: 'Ext.panel.Panel',
    xtype: 'logmanagementPanel',
    store: 'LogManagementStore',
    height: '100%',
    layout: {
        type: 'vbox',
        align: 'stretch'  
    },

    initComponent: function () {
        var me = this;

        me.toolbar = Ext.create('Ext.toolbar.Toolbar', {
            items: [
                {
                    xtype: 'textfield',
                    fieldLabel: 'Lọc',
                    name: 'filter',
                    id: 'log-filter',
                    width: 300,
                    emptyText: 'Tìm kiếm log...',
                    listeners: {
                        change: function () {
                            me.refreshGrid();
                        }
                    }
                },
                '-',
                {
                    xtype: 'datefield',
                    fieldLabel: 'Từ ngày',
                    name: 'startDate',
                    id: 'log-startdate',
                    width: 180,
                    format: 'Y-m-d',
                    listeners: {
                        change: function () {
                            me.refreshGrid();
                        }
                    }
                },
                {
                    xtype: 'datefield',
                    fieldLabel: 'Đến ngày',
                    name: 'endDate',
                    id: 'log-enddate',
                    width: 180,
                    format: 'Y-m-d',
                    listeners: {
                        change: function () {
                            me.refreshGrid();
                        }
                    }
                },
                '-',
                {
                    text: '🔄 Refresh',
                    handler: function () {
                        me.refreshGrid();
                    }
                },
                {
                    text: '📥 Export TXT',
                    handler: function () {
                        me.exportLogs('txt');
                    }
                },
                {
                    text: '📥 Export CSV',
                    handler: function () {
                        me.exportLogs('csv');
                    }
                },
                {
                    text: '🗑️ Clear All',
                    handler: function () {
                        me.clearLogs();
                    }
                }
            ]
        });

        me.grid = Ext.create('Ext.grid.Panel', {
            store: 'LogManagementStore',
            flex: 1,
            border: false,
            columns: [
                {
                    text: 'Thời gian',
                    dataIndex: 'DateTime',
                    width: 180,
                    sortable: true
                },
                {
                    text: 'Level',
                    dataIndex: 'Level',
                    width: 80,
                    sortable: true,
                    renderer: function (value) {
                        var cls = value === 'ERROR' ? 'color:red' :
                            value === 'WARNING' ? 'color:orange' : 'color:green';
                        return '<span style="' + cls + '">' + value + '</span>';
                    }
                },
                {
                    text: 'Type',
                    dataIndex: 'Type',
                    width: 100,
                    sortable: true
                },
                {
                    text: 'Message',
                    dataIndex: 'Message',
                    flex: 1,
                    minWidth: 200
                }
            ],
            stripeRows: true,
            autoScroll: true,
            viewConfig: {
                emptyText: 'Không có log nào'
            },
            bbar: Ext.create('Ext.toolbar.Paging', {
                store: 'LogManagementStore',
                displayInfo: true,
                displayMsg: 'Hiển thị {0} - {1} / {2} logs',
                emptyMsg: 'Không có log nào'
            })
        });

        me.statisticsPanel = Ext.create('Ext.panel.Panel', {
            region: 'bottom',
            height: 120,
            border: false,
            itemId: 'statisticsPanel',
            bodyStyle: 'background:#f5f5f5;padding:15px;',
            html: '<div id="log-statistics-content"></div>'  
        });

        me.items = [
            me.toolbar,
            me.grid,
            me.statisticsPanel
        ];

        me.refreshGrid = function () {
            var store = Ext.StoreManager.lookup('LogManagementStore');
            if (!store) return;
            var filterCmp = Ext.getCmp('log-filter');
            var startCmp = Ext.getCmp('log-startdate');
            var endCmp = Ext.getCmp('log-enddate');
            var proxy = store.getProxy();
            proxy.extraParams = {
                filter: filterCmp ? filterCmp.getValue() : '',
                startDate: startCmp && startCmp.getValue() ? Ext.Date.format(startCmp.getValue(), 'Y-m-d') : '',
                endDate: endCmp && endCmp.getValue() ? Ext.Date.format(endCmp.getValue(), 'Y-m-d') : ''
            };

            store.currentPage = 1;
            store.reload();
            me.loadStatistics();
        };

        me.loadStatistics = function () {
            Ext.Ajax.request({
                url: '/LogService.svc/GetStatistics',
                success: function (response) {
                    var stats = JSON.parse(response.responseText);
                    var html = '<div style="display:flex;gap:20px;justify-content:space-around;">';
                    html += '<div style="text-align:center;"><div style="font-size:24px;font-weight:bold;color:#667eea;">' + stats.TotalLines + '</div><div>Total Lines</div></div>';
                    html += '<div style="text-align:center;"><div style="font-size:24px;font-weight:bold;color:green;">' + stats.InfoCount + '</div><div>INFO</div></div>';
                    html += '<div style="text-align:center;"><div style="font-size:24px;font-weight:bold;color:red;">' + stats.ErrorCount + '</div><div>ERROR</div></div>';
                    html += '<div style="text-align:center;"><div style="font-size:24px;font-weight:bold;color:orange;">' + stats.WarningCount + '</div><div>WARNING</div></div>';
                    html += '<div style="text-align:center;"><div style="font-size:24px;font-weight:bold;color:#17a2b8;">' + stats.CreateCount + '</div><div>Create</div></div>';
                    html += '<div style="text-align:center;"><div style="font-size:24px;font-weight:bold;color:#4CAF50;">' + stats.UpdateCount + '</div><div>Update</div></div>';
                    html += '<div style="text-align:center;"><div style="font-size:24px;font-weight:bold;color:#f44336;">' + stats.DeleteCount + '</div><div>Delete</div></div>';
                    html += '</div>';
                    Ext.get('log-statistics-content').setHtml(html);
                },
                failure: function () {
                    Ext.get('log-statistics-content').setHtml('<div style="text-align:center;color:red;">Error loading statistics</div>');
                }
            });
        };

        me.exportLogs = function (format) {
            Ext.Ajax.request({
                url: '/LogService.svc/ExportLogs',
                method: 'POST',
                params: { format: format },
                success: function () {
                    Ext.alert('Success', 'Logs exported successfully!');
                },
                failure: function () {
                    Ext.alert('Error', 'Failed to export logs!');
                }
            });
        };

        me.clearLogs = function () {
            Ext.confirm('Confirm', 'Are you sure you want to clear all logs?', function (btn) {
                if (btn === 'yes') {
                    Ext.Ajax.request({
                        url: '/LogService.svc/ClearLogs',
                        method: 'DELETE',
                        success: function () {
                            Ext.alert('Success', 'Logs cleared successfully!');
                            me.refreshGrid();
                        },
                        failure: function () {
                            Ext.alert('Error', 'Failed to clear logs!');
                        }
                    });
                }
            });
        };

        me.callParent(arguments);
    },

    afterRender: function () {
        this.callParent(arguments);

        var store = Ext.StoreManager.lookup('LogManagementStore');

        // Debug: load thủ công và xem raw response
        //store.on('load', function (s, records, successful, eOpts) {
        //    console.log('Load successful:', successful);
        //    console.log('Records count:', records ? records.length : 'null');
        //    console.log('Store total:', s.getTotalCount());
        //});

        //store.on('exception', function (proxy, response, operation) {
        //    console.log('Store exception:', response);
        //    console.log('Response text:', response.responseText);
        //});

        //Ext.Ajax.request({
        //    url: '/LogService.svc/GetLogs?page=1&pageSize=20',
        //    success: function (response) {
        //        console.log('Raw response:', response.responseText);
        //        try {
        //            var parsed = JSON.parse(response.responseText);
        //            console.log('Parsed:', parsed);
        //            console.log('Has data field:', !!parsed.data);
        //            console.log('Has total field:', parsed.total);
        //        } catch (e) {
        //            console.log('Parse error:', e);
        //        }
        //    }
        //});

        this.refreshGrid();
    }
});