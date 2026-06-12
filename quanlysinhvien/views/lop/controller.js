// App/lop/controller.js
Ext.define('App.lop.controller', {
    extend: 'Ext.app.Controller',

    stores: ['App.lop.store'],
    models: ['App.lop.model'],

    refs: [
        { ref: 'lopGrid', selector: 'lopgrid' }
    ],

    init: function () {
        this.control({
            'lopgrid button[action=them]': { click: this.onThem },
            'lopgrid button[action=sua]': { click: this.onSua },
            'lopgrid button[action=xoa]': { click: this.onXoa },
            'lopgrid button[action=lammoi]': { click: this.onLamMoi },

            'window[itemId=lopForm] button[action=luu]': { click: this.onLuu },
            'window[itemId=lopForm] button[action=huy]': { click: this.onHuy }
        });
    },

    getLopStore: function () {
        return Ext.data.StoreManager.lookup('LopStore');
    },

    openForm: function (title, record) {
        var khoastore = Ext.data.StoreManager.lookup('KhoaStore');
        var old = Ext.ComponentQuery.query('window[itemId=lopForm]')[0];
        if (old) old.destroy();

        var win = Ext.create('Ext.window.Window', {
            itemId: 'lopForm',
            title: title,
            width: 420,
            modal: true,
            autoShow: true,
            layout: 'fit',
            items: [{
                xtype: 'form',
                padding: 20,
                items: [
                    { xtype: 'textfield', fieldLabel: 'Mã Lớp', name: 'MaLop', allowBlank: false, width: 360 },
                    { xtype: 'textfield', fieldLabel: 'Tên Lớp', name: 'TenLop', allowBlank: false, width: 360 },
                    {
                        xtype: 'combobox',
                        fieldLabel: 'Mã Khoa',
                        name: 'MaKhoa',
                        width: 360,
                        editable: false,
                        store: khoastore,
                        displayField: 'TenKhoa',
                        valueField: 'MaKhoa',
                        queryMode: 'local'
                    },
                    //{ xtype: 'textfield', fieldLabel: 'Mã Khoa', name: 'MaKhoa', allowBlank: false, width: 360 }
                ]
            }],
            buttons: [
                { text: '💾 Lưu', action: 'luu' },
                { text: '❌ Hủy', action: 'huy' }
            ]
        });

        if (record) {
            win.down('form').getForm().setValues(record.getData());
            win.down('form [name=MaLop]').setReadOnly(true);
        }
    },

    onThem: function () {
        this.openForm('➕ Thêm Lớp Mới', null);
    },

    onSua: function () {
        var selected = this.getLopGrid().getSelection()[0];
        if (!selected) { Ext.Msg.alert('Thông báo', 'Vui lòng chọn 1 dòng!'); return; }
        this.openForm('✏️ Sửa Lớp', selected);
    },

    onXoa: function () {
        var me = this;
        var selected = this.getLopGrid().getSelection()[0];
        if (!selected) { Ext.Msg.alert('Thông báo', 'Vui lòng chọn 1 dòng!'); return; }
        Ext.Msg.confirm('Xác nhận', 'Bạn có chắc muốn xóa lớp này?', function (btn) {
            if (btn !== 'yes') return;
            Ext.Ajax.request({
                url: '/LopService.svc/Delete/' + selected.get('MaLop'),
                method: 'DELETE',
                success: function () { Ext.Msg.alert('Thành công', 'Đã xóa!'); me.getLopStore().load(); },
                failure: function () { Ext.Msg.alert('Lỗi', 'Xóa thất bại!'); }
            });
        });
    },

    onLamMoi: function () {
        this.getLopStore().load();
    },

    onLuu: function (btn) {
        var me = this;
        var win = btn.up('window');
        var form = win.down('form').getForm();
        if (!form.isValid()) return;

        var values = form.getValues();
        var isEdit = win.down('form [name=MaLop]').readOnly;
        var url = isEdit
            ? '/LopService.svc/Update/' + values.MaLop
            : '/LopService.svc/Create';

        Ext.Ajax.request({
            url: url,
            method: isEdit ? 'PUT' : 'POST',
            jsonData: values,
            success: function () {
                Ext.Msg.alert('Thành công', isEdit ? 'Đã cập nhật!' : 'Đã thêm!');
                me.getLopStore().load();
                win.close();
            },
            failure: function () { Ext.Msg.alert('Lỗi', 'Lưu thất bại!'); }
        });
    },

    onHuy: function (btn) {
        btn.up('window').close();
    }
});