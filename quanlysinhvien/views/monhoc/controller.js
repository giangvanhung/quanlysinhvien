// App/monhoc/controller.js
Ext.define('App.monhoc.controller', {
    extend: 'Ext.app.Controller',

    stores: ['App.monhoc.store'],
    models: ['App.monhoc.model'],

    refs: [
        { ref: 'monHocGrid', selector: 'monhocgrid' }
    ],

    init: function () {
        this.control({
            'monhocgrid button[action=them]': { click: this.onThem },
            'monhocgrid button[action=sua]': { click: this.onSua },
            'monhocgrid button[action=xoa]': { click: this.onXoa },
            'monhocgrid button[action=lammoi]': { click: this.onLamMoi },

            'window[itemId=monHocForm] button[action=luu]': { click: this.onLuu },
            'window[itemId=monHocForm] button[action=huy]': { click: this.onHuy }
        });
    },

    getMonHocStore: function () {
        return Ext.data.StoreManager.lookup('MonHocStore');
    },

    openForm: function (title, record) {
        var old = Ext.ComponentQuery.query('window[itemId=monHocForm]')[0];
        if (old) old.destroy();

        var win = Ext.create('Ext.window.Window', {
            itemId: 'monHocForm',
            title: title,
            width: 420,
            modal: true,
            autoShow: true,
            layout: 'fit',
            items: [{
                xtype: 'form',
                padding: 20,
                items: [
                    { xtype: 'textfield', fieldLabel: 'Mã Môn', name: 'MaMon', allowBlank: false, width: 360 },
                    { xtype: 'textfield', fieldLabel: 'Tên Môn', name: 'TenMon', allowBlank: false, width: 360 }
                ]
            }],
            buttons: [
                { text: '💾 Lưu', action: 'luu' },
                { text: '❌ Hủy', action: 'huy' }
            ]
        });

        if (record) {
            win.down('form').getForm().setValues(record.getData());
            win.down('form [name=MaMon]').setReadOnly(true);
        }
    },

    onThem: function () {
        this.openForm('➕ Thêm Môn Học', null);
    },

    onSua: function () {
        var selected = this.getMonHocGrid().getSelection()[0];
        if (!selected) { Ext.Msg.alert('Thông báo', 'Vui lòng chọn 1 dòng!'); return; }
        this.openForm('✏️ Sửa Môn Học', selected);
    },

    onXoa: function () {
        var me = this;
        var selected = this.getMonHocGrid().getSelection()[0];
        if (!selected) { Ext.Msg.alert('Thông báo', 'Vui lòng chọn 1 dòng!'); return; }
        Ext.Msg.confirm('Xác nhận', 'Bạn có chắc muốn xóa môn học này?', function (btn) {
            if (btn !== 'yes') return;
            Ext.Ajax.request({
                url: '/MonHocService.svc/Delete/' + selected.get('MaMon'),
                method: 'DELETE',
                success: function () { Ext.Msg.alert('Thành công', 'Đã xóa!'); me.getMonHocStore().load(); },
                failure: function () { Ext.Msg.alert('Lỗi', 'Xóa thất bại!'); }
            });
        });
    },

    onLamMoi: function () {
        this.getMonHocStore().load();
    },

    onLuu: function (btn) {
        var me = this;
        var win = btn.up('window');
        var form = win.down('form').getForm();
        if (!form.isValid()) return;

        var values = form.getValues();
        var isEdit = win.down('form [name=MaMon]').readOnly;
        var url = isEdit
            ? '/MonHocService.svc/Update/' + values.MaMon
            : '/MonHocService.svc/Create';

        Ext.Ajax.request({
            url: url,
            method: isEdit ? 'PUT' : 'POST',
            jsonData: values,
            success: function () {
                Ext.Msg.alert('Thành công', isEdit ? 'Đã cập nhật!' : 'Đã thêm!');
                me.getMonHocStore().load();
                win.close();
            },
            failure: function () { Ext.Msg.alert('Lỗi', 'Lưu thất bại!'); }
        });
    },

    onHuy: function (btn) {
        btn.up('window').close();
    }
});