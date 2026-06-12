// App/bangdiem/controller.js
Ext.define('App.bangdiem.controller', {
    extend: 'Ext.app.Controller',

    stores: ['App.bangdiem.store', 'App.monhoc.store', ],
    models: ['App.bangdiem.model'],

    refs: [
        { ref: 'bangDiemGrid', selector: 'bangdiemgrid' }
    ],

    init: function () {
        this.control({
            'bangdiemgrid button[action=them]': { click: this.onThem },
            'bangdiemgrid button[action=sua]': { click: this.onSua },
            'bangdiemgrid button[action=xoa]': { click: this.onXoa },
            'bangdiemgrid button[action=lammoi]': { click: this.onLamMoi },

            'window[itemId=bangDiemForm] button[action=luu]': { click: this.onLuu },
            'window[itemId=bangDiemForm] button[action=huy]': { click: this.onHuy }
        });
    },

    getBangDiemStore: function () {
        return Ext.data.StoreManager.lookup('BangDiemStore');
    },

    openForm: function (title, record) {
        var gvstore = Ext.data.StoreManager.lookup('GiangVienStore');
        var sinhvienstore = Ext.data.StoreManager.lookup('SinhVienStore');
        var monstore = Ext.data.StoreManager.lookup('MonHocStore');
        var old = Ext.ComponentQuery.query('window[itemId=bangDiemForm]')[0];
        if (old) old.destroy();

        var win = Ext.create('Ext.window.Window', {
            itemId: 'bangDiemForm',
            title: title,
            width: 420,
            modal: true,
            autoShow: true,
            layout: 'fit',
            items: [{
                xtype: 'form',
                padding: 20,
                items: [
                    //{ xtype: 'textfield', fieldLabel: 'Mã SV', name: 'MaSV', allowBlank: false, width: 360 },
                    //{ xtype: 'textfield', fieldLabel: 'Mã Môn', name: 'MaMon', allowBlank: false, width: 360 },
                    //{ xtype: 'textfield', fieldLabel: 'Mã GV', name: 'MaGV', allowBlank: false, width: 360 },
                    { xtype: 'hiddenfield', name: 'Id' },
                    {
                        xtype: 'combobox',
                        fieldLabel: 'Sinh vien',
                        name: 'MaSV',
                        width: 360,
                        editable: false,
                        store: sinhvienstore,
                        displayField: 'TenSV',
                        valueField: 'MaSV',
                        queryMode: 'local'
                    },
                    {
                        xtype: 'combobox',
                        fieldLabel: 'Mon',
                        name: 'MaMon',
                        width: 360,
                        editable: false,
                        store: monstore,
                        displayField: 'TenMon',
                        valueField: 'MaMon',
                        queryMode: 'local'
                    },
                    {
                        xtype: 'combobox',
                        fieldLabel: 'Giang vien phu trach',
                        name: 'MaGV',
                        width: 360,
                        editable: false,
                        store: gvstore,
                        displayField: 'TenGV',
                        valueField: 'MaGV',
                        queryMode: 'local'
                    },
                    {
                        xtype: 'numberfield',
                        fieldLabel: 'Điểm Số',
                        name: 'DiemSo',
                        allowBlank: false,
                        minValue: 0,
                        maxValue: 10,
                        decimalPrecision: 2,
                        width: 360
                    },
                    { xtype: 'textfield', fieldLabel: 'Năm Học', name: 'NamHoc', allowBlank: false, width: 360 }
                ]
            }],
            buttons: [
                { text: '💾 Lưu', action: 'luu' },
                { text: '❌ Hủy', action: 'huy' }
            ]
        });

        if (record) {
            win.down('form').getForm().setValues(record.getData());
            // Khoá composite key MaSV + MaMon khi sửa
            win.down('form [name=MaSV]').setReadOnly(true);
        }
    },

    onThem: function () {
        this.openForm('➕ Thêm Bảng Điểm', null);
    },

    onSua: function () {
        var selected = this.getBangDiemGrid().getSelection()[0];
        if (!selected) { Ext.Msg.alert('Thông báo', 'Vui lòng chọn 1 dòng!'); return; }
        this.openForm('✏️ Sửa Bảng Điểm', selected);
    },

    onXoa: function () {
        var me = this;
        var selected = this.getBangDiemGrid().getSelection()[0];
        if (!selected) { Ext.Msg.alert('Thông báo', 'Vui lòng chọn 1 dòng!'); return; }

        Ext.Msg.confirm('Xác nhận', 'Bạn có chắc muốn xóa bản ghi này?', function (btn) {
            if (btn !== 'yes') return;
            Ext.Ajax.request({
                url: '/BangDiemService.svc/Delete/' + selected.get('Id'),
                method: 'DELETE',
                success: function () { Ext.Msg.alert('Thành công', 'Đã xóa!'); me.getBangDiemStore().load(); },
                failure: function () { Ext.Msg.alert('Lỗi', 'Xóa thất bại!'); }
            });
        });
    },

    onLamMoi: function () {
        this.getBangDiemStore().load();
    },

    onLuu: function (btn) {
        var me = this;
        var win = btn.up('window');
        var form = win.down('form').getForm();
        if (!form.isValid()) return;

        var values = form.getValues();
        var isEdit = win.down('form [name=MaSV]').readOnly;
        var url = isEdit
            ? '/BangDiemService.svc/Update/' + values.Id  // ← dùng Id
            : '/BangDiemService.svc/Create';

        values.DiemSo = parseFloat(values.DiemSo) || 0;
        values.Id = values.Id ? parseInt(values.Id) : null;
        Ext.Ajax.request({
            url: url,
            method: isEdit ? 'PUT' : 'POST',
            jsonData: values,
            success: function () {
                Ext.Msg.alert('Thành công', isEdit ? 'Đã cập nhật!' : 'Đã thêm!');
                me.getBangDiemStore().load();
                win.close();
            },
            failure: function () { Ext.Msg.alert('Lỗi', 'Lưu thất bại!'); }
        });
    },

    onHuy: function (btn) {
        btn.up('window').close();
    }
});