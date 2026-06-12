// App/giangvien/controller.js
Ext.define('App.giangvien.controller', {
    extend: 'Ext.app.Controller',

    stores: ['App.giangvien.store', 'App.khoa.store'],
    models: ['App.giangvien.model'],

    refs: [
        { ref: 'giangVienGrid', selector: 'giangviengrid' }
    ],

    init: function () {
        this.control({
            'giangviengrid button[action=them]': { click: this.onThem },
            'giangviengrid button[action=sua]': { click: this.onSua },
            'giangviengrid button[action=xoa]': { click: this.onXoa },
            'giangviengrid button[action=lammoi]': { click: this.onLamMoi },

            'window[itemId=giangVienForm] button[action=luu]': { click: this.onLuu },
            'window[itemId=giangVienForm] button[action=huy]': { click: this.onHuy }
        });
    },

    getGiangVienStore: function () {
        return Ext.data.StoreManager.lookup('GiangVienStore');
    },

    openForm: function (title, record) {
        var khoastore = Ext.data.StoreManager.lookup('KhoaStore');
        var old = Ext.ComponentQuery.query('window[itemId=giangVienForm]')[0];
        if (old) old.destroy();

        var win = Ext.create('Ext.window.Window', {
            itemId: 'giangVienForm',
            title: title,
            width: 450,
            modal: true,
            autoShow: true,
            layout: 'fit',
            items: [{
                xtype: 'form',
                padding: 20,
                items: [
                    { xtype: 'textfield', fieldLabel: 'Mã GV', name: 'MaGV', allowBlank: false, width: 380 },
                    { xtype: 'textfield', fieldLabel: 'Tên GV', name: 'TenGV', allowBlank: false, width: 380 },
                    { xtype: 'datefield', fieldLabel: 'Ngày Sinh', name: 'NgaySinh', format: 'd/m/Y', width: 380 },
                    {
                        xtype: 'combobox',
                        fieldLabel: 'Giới Tính',
                        name: 'GioiTinh',
                        width: 380,
                        editable: false,
                        store: [[true, 'Nam'], [false, 'Nữ']],
                        value: true
                    },
                    {
                        xtype: 'combobox',
                        fieldLabel: 'Mã Khoa',
                        name: 'MaKhoa',
                        width: 380,
                        editable: false,
                        store: khoastore,
                        displayField: 'TenKhoa',
                        valueField: 'MaKhoa',
                        queryMode: 'local'
                    },
                    //{ xtype: 'textfield', fieldLabel: 'Mã Khoa', name: 'MaKhoa', allowBlank: false, width: 380 }
                ]
            }],
            buttons: [
                { text: '💾 Lưu', action: 'luu' },
                { text: '❌ Hủy', action: 'huy' }
            ]
        });

        if (record) {
            win.down('form').getForm().setValues(record.getData());
            win.down('form [name=MaGV]').setReadOnly(true);
        }
    },

    onThem: function () {
        this.openForm('➕ Thêm Giảng Viên', null);
    },

    onSua: function () {
        var selected = this.getGiangVienGrid().getSelection()[0];
        if (!selected) { Ext.Msg.alert('Thông báo', 'Vui lòng chọn 1 dòng!'); return; }
        this.openForm('✏️ Sửa Giảng Viên', selected);
    },

    onXoa: function () {
        var me = this;
        var selected = this.getGiangVienGrid().getSelection()[0];
        if (!selected) { Ext.Msg.alert('Thông báo', 'Vui lòng chọn 1 dòng!'); return; }
        Ext.Msg.confirm('Xác nhận', 'Bạn có chắc muốn xóa giảng viên này?', function (btn) {
            if (btn !== 'yes') return;
            Ext.Ajax.request({
                url: '/GiangVienService.svc/Delete/' + selected.get('MaGV'),
                method: 'DELETE',
                success: function () { Ext.Msg.alert('Thành công', 'Đã xóa!'); me.getGiangVienStore().load(); },
                failure: function () { Ext.Msg.alert('Lỗi', 'Xóa thất bại!'); }
            });
        });
    },

    onLamMoi: function () {
        this.getGiangVienStore().load();
    },

    onLuu: function (btn) {
        var me = this;
        var win = btn.up('window');
        var form = win.down('form').getForm();
        if (!form.isValid()) return;

        var values = form.getValues();
        var dateValue = form.findField('NgaySinh').getValue();

        values.NgaySinh = dateValue
            ? Ext.Date.format(dateValue, 'Y-m-d')
            : "1900-01-01";
        var isEdit = win.down('form [name=MaGV]').readOnly;
        var url = isEdit
            ? '/GiangVienService.svc/Update/' + values.MaGV
            : '/GiangVienService.svc/Create';

        values.GioiTinh = (values.GioiTinh === 'true' || values.GioiTinh === true);

        Ext.Ajax.request({
            url: url,
            method: isEdit ? 'PUT' : 'POST',
            jsonData: values,
            headers: { 'Content-Type': 'application/json' },
            success: function () {
                Ext.Msg.alert('Thành công', isEdit ? 'Đã cập nhật!' : 'Đã thêm!');
                me.getGiangVienStore().load();
                win.close();
            },
            failure: function () { Ext.Msg.alert('Lỗi', 'Lưu thất bại!'); }
        });
    },

    onHuy: function (btn) {
        btn.up('window').close();
    }
});