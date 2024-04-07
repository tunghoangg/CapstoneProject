

$(document).ready(function () {
    console.log(farmId);

    // Call LoadItemData() with the default filter value (0)
    LoadItemData();

    // Bind change event listener to the select element
    $('#filter-item-by-type').change(function () {
        // Get the selected value
        var typeId = $(this).val();

        // Call LoadItemData() with the updated filter value
        LoadItemData(typeId);
    });

    CreateItem();

    DetailItem();

    UpdateItem();

    DeleteItem();

});

function LoadItemData(typeId) {
    resetUpdateItem();
    resetCreateItem();

    // If typeId is undefined, set it to 0
    typeId = typeId || 0;

    // Construct the request data object
    var rqData = {
        FarmId: farmId,
        TypeId: typeId
    };

    // Destroy existing DataTable instance (if any)
    $('#data-table-farm-item').DataTable().destroy();

    $('#data-table-farm-item').DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "bInfo": false,
        //pageLength: 0,
        lengthMenu: [5, 10],
        "ajax": {
            "url": baseUrl + "Materials/ListItem",
            "type": "GET",
            "datatype": "json",
            //"headers": {
            //	"Authorization": "Bearer " + token
            //},
            "contentType": "application/json",
            "data": rqData,
            /*"data": JSON.stringify(data),*/
        },
        "columnDefs": [
            {
                "targets": [-1], // Chỉ áp dụng cho cột cuối cùng
                "orderable": false // Không cho phép sắp xếp
            },
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            }
        ],
        "oLanguage": {

            "sSearch": "Tìm tên: "

        },
        language: {
            'paginate': {
                'previous': '<span class="prev-icon">Trước</span>',
                'next': '<span class="next-icon">Tiếp</span>'
            }
        },
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "itemName", "name": "Tên", "autoWidth": true },
            { "data": "quantity", "name": "Số lượng", "autoWidth": true },
            //{ "data": "value", "name": "", "autoWidth": true },
            {
                "data": "value",
                "name": "Tổng giá trị",
                "autoWidth": true,
                "render": function (data, type, row, meta) {
                    // Check if data is null or undefined
                    if (data === null || data === undefined) {
                        return "";
                    } else {
                        // Append " triệu VNĐ" after the value and return
                        return data + " triệu VNĐ";
                    }
                }
            },
            {
                "data": "createdTime",
                "name": "Ngày nhập kho",
                "autoWidth": true,
                "render": function (data, type, row, meta) {
                    // Kiểm tra nếu dữ liệu là null hoặc không xác định
                    if (data === null || data === undefined) {
                        return "";
                    } else {
                        // Chuyển đổi dữ liệu thành đối tượng ngày
                        let date = new Date(data);
                        // Sử dụng hàm formatDate để định dạng ngày tháng
                        return formatDate2(date);
                    }
                }
            },
            {
                //onclick='DetailItem(${JSON.stringify(data)})' onclick='DeleteItem(${JSON.stringify(data)})'
                data: "id", render: function (data, type, row, meta) {
                    return `
						<button style='background-color: transparent;border: none; padding: 5px; cursor: pointer' class='btn' id='btn-detail-item' data-item-detail="${data}" data-toggle="modal" data-target="#modal-detail-item" ><i class="mdi mdi-eye user-icon"></i></button>
						<button style='background-color: transparent;border: none; padding: 5px; cursor: pointer' class='btn' id='btn-delete-item' data-item-delete="${data}" data-toggle="modal" data-target="#modal-soft-delete-item" ><i class="mdi mdi-delete user-icon"></i></button>
					`;
                }
            }
        ]
    });
};

function CreateItem() {
    $("#form-create-item").validate({
        rules: {
            inputName: {
                required: true,
                minlength: 2,
                maxlength: 40
            },
            inputQuantity: {
                required: true,
                min: 0
            },
            inputItemValue: {
                required: true,
                min: 0,
                max: 1000,
                integer: true
            },
            inputCreatedDate: {
                required: true,
                checkDate: true
            },
            inputDescription: {
                maxlength: 1000
            },
        },
        messages: {
            inputName: {
                required: "Vui lòng nhập tên sản phẩm phẩm",
                minlength: "Tên phải có ít nhất 2 ký tự",
                maxlength: "Tên không được vượt quá 40 ký tự"
            },
            inputQuantity: {
                required: "Vui lòng nhập số lượng",
                min: "Số lượng không được âm"
            },
            inputItemValue: {
                required: "Vui lòng nhập giá trị",
                min: "Giá trị không được âm",
                max: "Giá trị sản phẩm không vượt quá 1000 triệu VNĐ",
                integer: "Vui lòng chỉ nhập số nguyên"
            },
            inputCreatedDate: {
                required: "Vui lòng chọn ngày",
                checkDate: "Ngày nhập kho không được vượt quá ngày hiện tại"
            },
            inputDescription: {
                maxlength: "Mô tả không được vượt quá 1000 ký tự"
            },
        },
        errorElement: "em",
        highlight: function (element, errorClass, validClass) {
            $(element).addClass("is-invalid").removeClass("is-valid");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).addClass("is-valid").removeClass("is-invalid");
        },
        errorPlacement: function (error, element) {
            error.addClass("invalid-feedback");
            error.insertAfter(element);
        },

        submitHandler: function (form) {
            var itemName = $('#add-model-item-name').val();
            var typeId = $('#add-model-item-type').val();
            var quantity = $('#add-model-item-quantity').val();
            var unit = $('#add-model-item-unit').val();
            var value = $('#add-model-item-value').val();
            var createdDate = $('#add-model-item-created-date').val();
            var description = $('#add-model-item-description').val();

            var typeSelect = $('#filter-item-by-type').val();

            var data = {
                "itemName": itemName,
                "quantity": quantity,
                "unitId": unit,
                "value": value,
                "createdTime": createdDate,
                "description": description,
                "typeId": typeId,
                "farmId": farmId
            };

            $.ajax({
                url: baseUrl + "Materials/CreateItem",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify(data),
                success: function () {

                },
                complete: function () {
                    $('#create-model-item').modal('hide');
                    LoadItemData(typeSelect);
                    createToast('success', 'fa-solid fa-circle-check', 'Success', 'Thêm mới thành công.');
                },
                error: function (xhr, status, error) {
                    createToast('error', 'fa-solid fa-circle-exclamation', 'Error', 'Thêm mới thất bại.');
                }
            });
        }
    });
};

function UpdateItem() {
    $("#form-update-item").validate({
        rules: {
            inputName: {
                required: true,
                minlength: 2,
                maxlength: 40
            },
            inputQuantity: {
                required: true,
                min: 0
            },
            inputItemValue: {
                required: true,
                min: 0,
                max: 1000,
                integer: true
            },
            inputCreatedDate: {
                required: true,
                checkDate: true
            },
            inputDescription: {
                maxlength: 1000
            },
        },
        messages: {
            inputName: {
                required: "Vui lòng nhập tên của bạn",
                minlength: "Tên phải có ít nhất 2 ký tự",
                maxlength: "Tên không được vượt quá 40 ký tự"
            },
            inputQuantity: {
                required: "Vui lòng nhập số lượng",
                min: "Số lượng không được âm"
            },
            inputItemValue: {
                required: "Vui lòng nhập giá trị",
                min: "Giá trị không được âm",
                max: "Giá trị sản phẩm không vượt quá 1000 triệu VNĐ",
                integer: "Vui lòng chỉ nhập số nguyên"
            },
            inputCreatedDate: {
                required: "Vui lòng chọn ngày",
                checkDate: "Ngày nhập kho không được vượt quá ngày hiện tại"
            },
            inputDescription: {
                maxlength: "Mô tả không được vượt quá 1000 ký tự"
            },
        },
        errorElement: "em",
        highlight: function (element, errorClass, validClass) {
            $(element).addClass("is-invalid").removeClass("is-valid");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).addClass("is-valid").removeClass("is-invalid");
        },
        errorPlacement: function (error, element) {
            error.addClass("invalid-feedback");
            error.insertAfter(element);
        },
        submitHandler: function (form) {
            var itemId = $('#update-model-item-id').val();
            var itemName = $('#update-model-item-name').val();
            var type = $('#update-model-item-type').val();
            var quantity = $('#update-model-item-quantity').val();
            var unit = $('#update-model-item-unit').val();
            var value = $('#update-model-item-value').val();
            var createdDate = $('#update-model-item-created-date').val();
            var description = $('#update-model-item-description').val();

            var typeSelect = $('#filter-item-by-type').val();

            var data = {
                "id": itemId,
                "itemName": itemName,
                "quantity": quantity,
                "unitId": unit,
                "value": value,
                "createdTime": createdDate,
                "description": description,
                "typeId": type
            };

            $.ajax({
                url: baseUrl + "Materials/UpdateItem",
                type: "PUT",
                contentType: "application/json",
                data: JSON.stringify(data),
                success: function () {

                },
                complete: function () {
                    $('#update-model-item').modal('hide');
                    LoadItemData(typeSelect);
                    createToast('success', 'fa-solid fa-circle-check', 'Success', 'Cập nhật thành công.');
                },
                error: function (xhr, status, error) {
                    createToast('error', 'fa-solid fa-circle-exclamation', 'Error', 'Cập nhật thất bại.');
                }
            });
        }
    });
};

function DeleteItem() {
    $(document).on('click', '#btn-delete-item', function (event) {
        event.stopPropagation();
        var itemId = $(this).data('item-delete');
        var typeSelect = $('#filter-item-by-type').val();

        // Hiển thị modal xác nhận xóa
        $('#modal-soft-delete-item').modal('show');

        $('#request-delete-item').off('click').on('click', function (event) {
            event.stopPropagation();
            $.ajax({
                url: baseUrl + `Materials/DeateSofftItem/${itemId}`,
                type: "DELETE",
                contentType: "application/json",
                success: function () {
                    $('#modal-soft-delete-item').modal('hide');
                    // Nếu xóa thành công, cập nhật lại bảng dữ liệu
                    LoadItemData(typeSelect);
                    createToast('success', 'fa-solid fa-circle-check', 'Success', 'Xóa thành công.');
                },
                complete: function () {
                    //LoadItemData(typeSelect);
                    //createToast('success', 'fa-solid fa-circle-check', 'Success', 'Xóa thành công.');
                    //resetUpdateItem();
                    //resetCreateItem();
                },
                error: function (xhr, status, error) {
                    createToast('error', 'fa-solid fa-circle-exclamation', 'Error', 'Xóa thất bại.');
                }
            });
        });

    });


}

// Hàm kiểm tra số nguyên
$.validator.addMethod("integer", function (value, element) {
    return this.optional(element) || /^-?\d+$/.test(value);
}, "Vui lòng nhập một số nguyên");

$.validator.addMethod("checkDate", function (value, element) {
    var selectedDate = new Date(value);
    var currentDate = new Date();
    //currentDate.setHours(0, 0, 0, 0); // Loại bỏ thời gian để so sánh chỉ về ngày

    return selectedDate <= currentDate;
}, "Ngày nhập kho không được vượt quá ngày hiện tại");


function formatDate2(date) {
    let year = date.getFullYear();
    let month = (date.getMonth() + 1).toString().padStart(2, '0');
    let day = date.getDate().toString().padStart(2, '0');
    return `${year}-${month}-${day}`;
};

function DetailItem() {
    
    //DeleteItem(data.id);
    $(document).on('click', '#btn-detail-item', function (event) {
        //event.stopPropagation();
        //var itemId = $(this).data('item-detail');
        var data = $('#data-table-farm-item').DataTable().row($(this).closest('tr')).data();
        $('#detail-item-farm').text(data.farmCode);
        $('#detail-item-name').text(data.itemName);
        switch (data.typeId) {
            case 18:
                $('#detail-item-type').text("Sản phẩm nông sản");
                break;
            case 10:
                $('#detail-item-type').text("Thuốc hóa học");
                break;
            case 12:
                $('#detail-item-type').text("Phân bón hóa học");
                break;
            case 17:
                $('#detail-item-type').text("Chế phẩm sinh học");
                break;
            case 15:
                $('#detail-item-type').text("Máy móc");
                break;
            case 16:
                $('#detail-item-type').text("Công cụ");
                break;
            default:
                break;
        };
        switch (data.unitId) {
            case 1:
                $('#detail-item-quantity').text(data.quantity + " Kilogram");
                break;
            case 2:
                $('#detail-item-quantity').text(data.quantity + " Mét");
                break;
            case 3:
                $('#detail-item-quantity').text(data.quantity + " Cái");
                break;
            case 4:
                $('#detail-item-quantity').text(data.quantity + " Gói");
                break;
            case 5:
                $('#detail-item-quantity').text(data.quantity + " Lít");
                break;
            case 6:
                $('#detail-item-quantity').text(data.quantity + " Chiếc");
                break;
            default:
                break;
        };
        $('#detail-item-value').text(data.value + " triệu VNĐ");
        $('#detail-item-created-date').text(formatDate2(new Date(data.createdTime)));
        $('#detail-item-lasted-update').text(formatDate2(new Date(data.lastUpdate)));
        $('#detail-item-description').text(data.description);
        
        $('#btn-move-update-item').off('click').on('click', function (event) {
            event.stopPropagation();
            $('#modal-detail-item').modal('hide');
            /*var data = $('#data-table-item').DataTable().row($(this).closest('tr')).data();*/

            // Populate data into the update modal
            $('#update-model-item-id').val(data.id);
            $('#update-model-item-name').val(data.itemName);
            $('#update-model-item-type').val(data.typeId);
            $('#update-model-item-quantity').val(data.quantity);
            $('#update-model-item-unit').val(data.unitId);
            $('#update-model-item-value').val(data.value);
            $('#update-model-item-created-date').val(formatDate2(new Date(data.createdTime)));
            $('#update-model-item-description').val(data.description);

            // Show the update modal
            $('#update-model-item').modal('show');
        });

    });

}


function resetUpdateItem() {
    $('#update-model-item-id').val(null).removeClass("is-valid");
    $('#update-model-item-name').val(null).removeClass("is-valid");
    $('#update-model-item-type').val(18).removeClass("is-valid");
    $('#update-model-item-quantity').val(0).removeClass("is-valid");
    $('#update-model-item-unit').val(1).removeClass("is-valid");
    $('#update-model-item-value').val(0).removeClass("is-valid");
    $('#update-model-item-created-date').val(null).removeClass("is-valid");
    $('#update-model-item-description').val(null).removeClass("is-valid");
}

function resetCreateItem() {
    $('#add-model-item-name').val(null).removeClass("is-valid");
    $('#add-model-item-type').val(18).removeClass("is-valid");
    $('#add-model-item-quantity').val(0).removeClass("is-valid");
    $('#add-model-item-unit').val(1).removeClass("is-valid");
    $('#add-model-item-value').val(0).removeClass("is-valid");
    $('#add-model-item-created-date').val(null).removeClass("is-valid");
    $('#add-model-item-description').val(null).removeClass("is-valid");
};