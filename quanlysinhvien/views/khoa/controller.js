// App/khoa/controller.js
Ext.define('App.khoa.controller', {
    extend: 'Ext.app.Controller',

    // Khai báo store và view để controller tự khởi tạo
    stores: ['App.khoa.store'],
    models: ['App.khoa.model'],

    refs: [
        // ref: tên biến getter    selector: xtype của view
        { ref: 'khoaGrid', selector: 'khoagrid' }
    ],

    init: function () {
        this.control({
            'khoagrid button[action=them]': { click: this.onThem },
            'khoagrid button[action=sua]': { click: this.onSua },
            'khoagrid button[action=xoa]': { click: this.onXoa },
            'khoagrid button[action=lammoi]': { click: this.onLamMoi },

            'window[itemId=khoaForm] button[action=luu]': { click: this.onLuu },
            'window[itemId=khoaForm] button[action=huy]': { click: this.onHuy }
        });
    },

    // -------------------------------------------------------
    // Helpers
    // -------------------------------------------------------
    getKhoaStore: function () {
        return Ext.data.StoreManager.lookup('KhoaStore');
    },

    openForm: function (title, record) {
        // Đóng form cũ nếu còn mở
        var old = Ext.ComponentQuery.query('window[itemId=khoaForm]')[0];
        if (old) old.destroy();

        var win = Ext.create('Ext.window.Window', {
            itemId: 'khoaForm',
            title: title,
            width: 420,
            modal: true,
            autoShow: true,
            layout: 'fit',
            items: [{
                xtype: 'form',
                padding: 20,
                items: [
                    {
                        xtype: 'textfield',
                        fieldLabel: 'Mã Khoa',
                        name: 'MaKhoa',
                        allowBlank: false,
                        width: 360
                    },
                    {
                        xtype: 'textfield',
                        fieldLabel: 'Tên Khoa',
                        name: 'TenKhoa',
                        allowBlank: false,
                        width: 360
                    }
                ]
            }],
            buttons: [
                { text: '💾 Lưu', action: 'luu' },
                { text: '❌ Hủy', action: 'huy' }
            ]
        });

        if (record) {
            // Chế độ Sửa: điền data + khoá MaKhoa
            win.down('form').getForm().setValues(record.getData());
            win.down('form [name=MaKhoa]').setReadOnly(true);
        }
    },

    // -------------------------------------------------------
    // Handlers
    // -------------------------------------------------------
    onThem: function () {
        this.openForm('➕ Thêm Khoa Mới', null);
    },

    onSua: function () {
        var selected = this.getKhoaGrid().getSelection()[0];
        if (!selected) {
            Ext.Msg.alert('Thông báo', 'Vui lòng chọn 1 dòng!');
            return;
        }
        this.openForm('✏️ Sửa Khoa', selected);
    },

    onXoa: function () {
        var me = this;
        var selected = this.getKhoaGrid().getSelection()[0];
        if (!selected) {
            Ext.Msg.alert('Thông báo', 'Vui lòng chọn 1 dòng!');
            return;
        }
        Ext.Msg.confirm('Xác nhận', 'Bạn có chắc muốn xóa khoa này?', function (btn) {
            if (btn !== 'yes') return;
            Ext.Ajax.request({
                url: '/KhoaService.svc/Delete/' + selected.get('MaKhoa'),
                method: 'DELETE',
                success: function () {
                    Ext.Msg.alert('Thành công', 'Đã xóa khoa!');
                    me.getKhoaStore().load();
                },
                failure: function () {
                    Ext.Msg.alert('Lỗi', 'Xóa thất bại!');
                }
            });
        });
    },

    onLamMoi: function () {
        this.getKhoaStore().load();
    },

    onLuu: function (btn) {
        var me = this;
        var win = btn.up('window');
        var form = win.down('form').getForm();

        if (!form.isValid()) return;

        var values = form.getValues();
        var isEdit = win.down('form [name=MaKhoa]').readOnly;
        var url = isEdit
            ? '/KhoaService.svc/Update/' + values.MaKhoa
            : '/KhoaService.svc/Create';

        Ext.Ajax.request({
            url: url,
            method: isEdit ? 'PUT' : 'POST',
            jsonData: values,
            success: function () {
                Ext.Msg.alert('Thành công', isEdit ? 'Đã cập nhật khoa!' : 'Đã thêm khoa!');
                me.getKhoaStore().load();
                win.close();
            },
            failure: function () {
                Ext.Msg.alert('Lỗi', 'Lưu thất bại!');
            }
        });
    },

    onHuy: function (btn) {
        btn.up('window').close();
    }
});