// App/sinhvien/controller.js
Ext.define('App.sinhvien.controller', {
    extend: 'Ext.app.Controller',
    stores: ['App.sinhvien.store','App.lop.store'],
    models: ['App.sinhvien.model'],

    refs: [
        { ref: 'sinhVienGrid', selector: 'sinhviengrid' }
    ],

    init: function () {
        this.control({
            'sinhviengrid button[action=them]': { click: this.onThem },
            'sinhviengrid button[action=sua]': { click: this.onSua },
            'sinhviengrid button[action=xoa]': { click: this.onXoa },
            'sinhviengrid button[action=infor]': { click: this.onInfor },
            'sinhviengrid button[action=bdShow]': { click: this.onBdShow },
            'sinhviengrid button[action=lammoi]': { click: this.onLamMoi },
            'sinhviengrid textfield[action=search]': {
                change: this.onSearch
            },
            'window[itemId=sinhVienForm] button[action=luu]': { click: this.onLuu },
            'window[itemId=sinhVienForm] button[action=huy]': { click: this.onHuy },
            'window[itemId=inforForm] button[action=capnhat]': { click: this.onCapNhat },
            'window[itemId=inforForm] button[action=saveInfor]': { click: this.onSaveInfor },
            'window[itemId=inforForm] button[action=huyInfor]': {
                click: this.onHuyInfor
            }
        });
    },

    onSearch: function (field) {
        var value = field.getValue().toLowerCase();
        var store = Ext.data.StoreManager.lookup('SinhVienStore');

        if (!value) {
            store.clearFilter();
            return;
        }

        store.filterBy(function (record) {
            var tenSV = (record.get('TenSV') || '').trim().toLowerCase();
            var maSV = (record.get('MaSV') || '').trim().toLowerCase();
            return tenSV.includes(value) || maSV.includes(value);
        });
    },

    getSinhVienStore: function () {
        return Ext.data.StoreManager.lookup('SinhVienStore');
    },

    openForm: function (title, record) {
        var lopStore = Ext.data.StoreManager.lookup('LopStore');
        var old = Ext.ComponentQuery.query('window[itemId=sinhVienForm]')[0];
        if (old) old.destroy();

        var win = Ext.create('Ext.window.Window', {
            itemId: 'sinhVienForm',
            title: title,
            width: 450,
            modal: true,
            autoShow: true,
            layout: 'fit',
            items: [{
                xtype: 'form',
                padding: 20,
                items: [
                    { xtype: 'textfield', fieldLabel: 'Mã SV', name: 'MaSV', allowBlank: false, width: 380 },
                    { xtype: 'textfield', fieldLabel: 'Tên SV', name: 'TenSV', allowBlank: false, width: 380 },
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
                        fieldLabel: 'Mã Lớp',
                        name: 'MaLop',
                        width: 380,
                        editable: false,
                        store: lopStore,  
                        displayField: 'TenLop',     
                        valueField: 'MaLop',        
                        queryMode: 'local'          
                    },
                    //{ xtype: 'textfield', fieldLabel: 'Mã Lớp', name: 'MaLop', allowBlank: false, width: 380 }
                ]
            }],
            buttons: [
                { text: '💾 Lưu', action: 'luu' },
                { text: '❌ Hủy', action: 'huy' }
            ]
        });

        if (record) {
            win.down('form').getForm().setValues(record.getData());
            win.down('form [name=MaSV]').setReadOnly(true);
        }
    },

    openDiemPopup: function (record) {
        var maSV = record.get('MaSV').trim();
        var tenSV = record.get('TenSV').trim();

        Ext.Ajax.request({
            url: '/SinhvienService.svc/sinhvien/' + maSV + '/bangdiem',
            method: 'GET',
            success: function (response) {
                var data = Ext.decode(response.responseText);
                var old = Ext.ComponentQuery.query('window[itemId=sinhVienInfoForm]')[0];
                if (old) old.destroy();

                Ext.create('Ext.window.Window', {
                    itemId: 'sinhVienInfoForm',
                    title: 'Bảng điểm: ' + tenSV + ' (' + maSV + ')',
                    width: 480,
                    height: 350,
                    modal: true,
                    autoShow: true,
                    layout: 'fit',
                    buttons: [{ text: 'Đóng', handler: function () { this.up('window').destroy(); } }],
                    items: [{
                        xtype: 'grid',
                        store: Ext.create('Ext.data.Store', {
                            fields: ['Id', 'MaSV', 'MaMon', 'MaGV', 'DiemSo', 'NamHoc'],
                            data: data
                        }),
                        columns: [
                            { text: 'Mã Môn', dataIndex: 'MaMon', width: 100 },
                            { text: 'Mã GV', dataIndex: 'MaGV', width: 100 },
                            { text: 'Điểm Số', dataIndex: 'DiemSo', width: 80, align: 'center' },
                            { text: 'Năm Học', dataIndex: 'NamHoc', flex: 1 }
                        ]
                    }]
                });
            },
            failure: function () { Ext.Msg.alert('Lỗi', 'Không thể tải bảng điểm!'); }
        });
    },

    openInforPopup: function (record) {
        var maSV = record.get('MaSV').trim();
        var tenSV = record.get('TenSV').trim();
        var me = this;
        Ext.Ajax.request({
            url: '/SinhvienService.svc/sinhvien/' + maSV + '/infor',
            method: 'GET',
            success: function (response) {
                var data = Ext.decode(response.responseText);
                var inforList = data && data.length > 0 ? data[0] : null; // lấy first element

                me.openInforForm(maSV, tenSV, inforList);
            },
            failure: function () {
                me.openInforForm(maSV, tenSV, null); // nếu lỗi cũng coi như null → create
            }
        });
    },

    openInforForm: function (maSV, tenSV, inforData) {
        var me = this;
        var old = Ext.ComponentQuery.query('window[itemId=inforForm]')[0];
        if (old) old.destroy();

        var isCreate = !inforData; // null → create, có data → update

        var win = Ext.create('Ext.window.Window', {
            itemId: 'inforForm',
            title: 'Infor: ' + tenSV + ' (' + maSV + ')',
            width: 480,
            height: 420,
            modal: true,
            autoShow: true,
            layout: 'fit',
            buttons: [
                isCreate
                    ? { text: '💾 Lưu', action: 'saveInfor', itemId: 'saveInforBtn' }
                    : { text: '✏️ Cập nhật', action: 'capnhat', itemId: 'capnhatBtn' },
                { text: '❌ Hủy', action: 'huyInfor' }
            ],
            items: [{
                xtype: 'form',
                padding: 20,
                itemId: 'inforFormPanel',
                items: [
                    { xtype: 'textfield', fieldLabel: 'Mã SV', name: 'MaSV', value: maSV, allowBlank: false, width: 380, readOnly: true },
                    { xtype: 'textfield', fieldLabel: 'Địa chỉ', name: 'DiaChi', width: 380 },
                    { xtype: 'textfield', fieldLabel: 'Tôn giáo', name: 'TonGiao', width: 380 },
                    { xtype: 'textfield', fieldLabel: 'SDT', name: 'SDT', width: 380 },
                    { xtype: 'textfield', fieldLabel: 'Email', name: 'Email', width: 380 },
                    { xtype: 'textfield', fieldLabel: 'Dân tộc', name: 'DanToc', width: 380 }
                ]
            }]
        });

        if (!isCreate) {
            // Có data → điền vào form và set readonly
            var form = win.down('#inforFormPanel');
            form.getForm().setValues(inforData);

            // Set readonly cho tất cả fields
            form.down('[name=MaSV]').setReadOnly(true);
            form.down('[name=DiaChi]').setReadOnly(true);
            form.down('[name=TonGiao]').setReadOnly(true);
            form.down('[name=SDT]').setReadOnly(true);
            form.down('[name=Email]').setReadOnly(true);
            form.down('[name=DanToc]').setReadOnly(true);
        }

        me.inforMaSV = maSV; // lưu maSV để update
        me.isCreateInfor = isCreate;
    },

    onCapNhat: function (btn) {
        var win = btn.up('window');
        var form = win.down('#inforFormPanel');

        // Cho phép sửa tất cả fields
        form.down('[name=DiaChi]').setReadOnly(false);
        form.down('[name=TonGiao]').setReadOnly(false);
        form.down('[name=SDT]').setReadOnly(false);
        form.down('[name=Email]').setReadOnly(false);
        form.down('[name=DanToc]').setReadOnly(false);

        // Đổi nút "Cập nhật" → "Lưu"
        btn.setText('💾 Lưu');
        btn.setAction('saveInfor');
        btn.setItemId('saveInforBtn');

        // Ẩn nút "Cập nhật" (nếu có), chỉ giữ nút Save
    },

    onSaveInfor: function (btn) {
        var me = this;
        var win = btn.up('window');
        var form = win.down('#inforFormPanel').getForm();

        if (!form.isValid()) return;

        var values = form.getValues();
        var maSV = values.MaSV;
        console.log(values);

        Ext.Ajax.request({
            url: me.isCreateInfor
                ? '/SinhvienService.svc/infor/CreateInfor'
                : '/SinhvienService.svc/infor/UpdateInfor/' + maSV,
            method: me.isCreateInfor ? 'POST' : 'PUT',
            jsonData: values,
            headers: { 'Content-Type': 'application/json' },
            success: function () {
                Ext.Msg.alert('Thành công', me.isCreateInfor ? 'Đã thêm infor!' : 'Đã cập nhật infor!');
                win.close();
                me.getSinhVienStore().load();
            },
            failure: function () {
                Ext.Msg.alert('Lỗi', 'Lưu thất bại!');
            }
        });
    },

    onHuyInfor: function (btn) {
        btn.up('window').close();
    },

    onThem: function () {
        this.openForm('➕ Thêm Sinh Viên', null);
    },

    onSua: function () {
        var selected = this.getSinhVienGrid().getSelection()[0];
        if (!selected) { Ext.Msg.alert('Thông báo', 'Vui lòng chọn 1 dòng!'); return; }
        this.openForm('✏️ Sửa Sinh Viên', selected);
    },

    onXoa: function () {
        var me = this;
        var selected = this.getSinhVienGrid().getSelection()[0];
        if (!selected) { Ext.Msg.alert('Thông báo', 'Vui lòng chọn 1 dòng!'); return; }
        Ext.Msg.confirm('Xác nhận', 'Bạn có chắc muốn xóa sinh viên này?', function (btn) {
            if (btn !== 'yes') return;
            Ext.Ajax.request({
                url: '/Delete/' + selected.get('MaSV'),
                method: 'DELETE',
                success: function () { Ext.Msg.alert('Thành công', 'Đã xóa!'); me.getSinhVienStore().load(); },
                failure: function () { Ext.Msg.alert('Lỗi', 'Xóa thất bại!'); }
            });
        });
    },

    onInfor: function () {
        var record = this.getSinhVienGrid().getSelection()[0];
        if (!record) { Ext.Msg.alert('Thông báo', 'Vui lòng chọn một sinh viên!'); return; }
        this.openInforPopup(record);
    },

    onBdShow: function () {
        var record = this.getSinhVienGrid().getSelection()[0];
        if (!record) { Ext.Msg.alert('Thông báo', 'Vui lòng chọn một sinh viên!'); return; }
        this.openDiemPopup(record);
    },

    onLamMoi: function () {
        this.getSinhVienStore().load();
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

        var isEdit = win.down('form [name=MaSV]').readOnly;
        var url = isEdit
            ? '/SinhvienService.svc/Update/' + values.MaSV
            : '/SinhvienService.svc/Create';

        // combobox trả về string "true"/"false", cần ép về boolean
        values.GioiTinh = (values.GioiTinh === 'true' || values.GioiTinh === true);
        
        Ext.Ajax.request({
            url: url,
            method: isEdit ? 'PUT' : 'POST',
            jsonData: values,
            headers: { 'Content-Type': 'application/json' },
            success: function () {
                Ext.Msg.alert('Thành công', isEdit ? 'Đã cập nhật!' : 'Đã thêm!');
                me.getSinhVienStore().load();
                win.close();
            },
            failure: function () { Ext.Msg.alert('Lỗi', 'Lưu thất bại!'); }
        });
    },

    onHuy: function (btn) {
        btn.up('window').close();
    }
});