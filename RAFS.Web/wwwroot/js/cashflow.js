
$(document).ready(function () {
	
	// Call LoadItemData() with the default filter value (0)
	LoadCashData();

	// Bind change event listener to the select element
	$('#filter-cash-by-type').change(function () {
		// Get the selected value
		var typeId = $(this).val();

		// Call LoadItemData() with the updated filter value
		LoadCashData(typeId);
	});

	CreateCash();

	DetailCash();

	UpdateCash();
	DeleteCashFlow();

});

function LoadCashData(typeId) {
	console.log(userId);
	console.log(farmId);

	resetUpdateCash();
	resetCreateCash();
	// If typeId is undefined, set it to 0
	typeId = typeId || 0;

	// Construct the request data object
	var rqData = {
		FarmId: farmId,
		TypeId: typeId
	};

	// Destroy existing DataTable instance (if any)
	$('#data-table-cash-flow').DataTable().destroy();

	$('#data-table-cash-flow').DataTable({
		"processing": true,
		"serverSide": true,
		"filter": true,
		"bInfo": false,
		//pageLength: 0,
		lengthMenu: [5, 10],
		"ajax": {
			"url": baseUrl + "FundDiary/ListCash",
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

			"sSearch": "Tìm mã: "

		},
		language: {
			'paginate': {
				'previous': '<span class="prev-icon">Trước</span>',
				'next': '<span class="next-icon">Tiếp</span>'
			}
		},
		"columns": [
			{ "data": "id", "name": "Id", "autoWidth": true },
			{ "data": "code", "name": "Mã", "autoWidth": true },
			{ "data": "value", "name": "Giá trị", "autoWidth": true },
			{
				"data": "createdTime",
				"name": "Ngày tạo",
				"autoWidth": true,
				"render": function (data, type, row, meta) {
					// Kiểm tra nếu dữ liệu là null hoặc không xác định
					if (data === null || data === undefined) {
						return "";
					} else {
						// Chuyển đổi dữ liệu thành đối tượng ngày
						let date = new Date(data);
						// Sử dụng hàm formatDate để định dạng ngày tháng
						return formatDate3(date);
					}
				}
			},
			{
				data: "id", render: function (data, type, row, meta) {
					return `
						<button style='background-color: transparent;border: none; padding: 5px; cursor: pointer' class='btn' id='btn-detail-cash' data-cash-detail="${data}" data-toggle="modal" data-target="#modal-detail-cash" ><i class="mdi mdi-eye user-icon"></i></button>
						<button style='background-color: transparent;border: none; padding: 5px; cursor: pointer' class='btn' id='btn-delete-cash' data-cash-delete="${data}" data-toggle="modal" data-target="#modal-soft-delete-cash" ><i class="mdi mdi-delete user-icon"></i></button>
					`;
				}
			}
		],
	});
};

function CreateCash() {


	$("#form-create-cash").validate({
		rules: {
			inputValue: {
				required: true,
				min: 0,
				max: 1000,
				integer: true
			},
			inputDescription: {
				maxlength: 1000
			},
		},
		messages: {
			inputValue: {
				required: "Vui lòng nhập giá trị",
				min: "Giá trị không được âm",
				max: "Giá trị không vượt quá 1000 triệu VNĐ",
				integer: "Vui lòng chỉ nhập số nguyên"
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
			let type = $('#add-model-cash-type').val();
			let value = $('#add-model-cash-value').val();
			let description = $('#add-model-cash-description').val();

			var data = {
				"value": value,
				"description": description,
				"typeId": type,
				"farmId": farmId,
				"userId": userId
			};

			var typeSelect = $('#filter-cash-by-type').val();

			$.ajax({
				url: baseUrl + "FundDiary/CreateCash",
				type: "POST",
				contentType: "application/json",
				data: JSON.stringify(data),
				success: function () {

				},
				complete: function () {
					$('#create-model-cash').modal('hide');
					
					LoadCashData(typeSelect);
					createToast('success', 'fa-solid fa-circle-check', 'Success', 'Thêm mới thành công.');
				},
				error: function (xhr, status, error) {
					createToast('error', 'fa-solid fa-circle-exclamation', 'Error', 'Thêm mới thất bại.');
				}
			});
		}
	});
};

function UpdateCash() {
	$("#form-update-cash").validate({
		rules: {
			inputValue: {
				required: true,
				min: 0,
				max: 1000,
				integer: true
			},
			inputDescription: {
				maxlength: 1000
			},
		},
		messages: {
			inputValue: {
				required: "Vui lòng nhập giá trị",
				min: "Giá trị không được âm",
				max: "Giá trị không vượt quá 1000 triệu VNĐ",
				integer: "Vui lòng chỉ nhập số nguyên"
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
			var cashId = $('#update-model-cash-id').val();
			var type = $('#update-model-cash-type').val();
			var value = $('#update-model-cash-value').val();
			var description = $('#update-model-cash-description').val();

			var typeSelect = $('#filter-cash-by-type').val();

			var data = {
				"id": cashId,
				"value": value,
				"description": description,
				"typeId": type
			};
			console.log(data);


			$.ajax({
				url: baseUrl + "FundDiary/UpdateCashFlow",
				type: "PUT",
				contentType: "application/json",
				data: JSON.stringify(data),
				success: function () {
					$('#update-model-cash').modal('hide');
					
					LoadCashData(typeSelect);
					createToast('success', 'fa-solid fa-circle-check', 'Success', 'Cập nhật thành công.');
				},
				complete: function () {
					
				},
				error: function (xhr, status, error) {
					createToast('error', 'fa-solid fa-circle-exclamation', 'Error', 'Cập nhật thất bại.');
				}
			});
		}
	});
};

function DeleteCashFlow() {
	
	$(document).on('click', '#btn-delete-cash', function () {
		//var data = $('#data-table-cash-flow').DataTable().row($(this).closest('tr')).data();
		let typeSelect = $('#filter-cash-by-type').val();
		let cashId = $(this).data('cash-delete');
		
		console.log(cashId);
		$('#confirm-delete-cash-flow').off('click').on('click', function (event) {
			event.stopPropagation();
			console.log(cashId);
			$.ajax({
				url: baseUrl + `FundDiary/DeleteSofftCash/${cashId}`,
				type: "DELETE",
				contentType: "application/json",
				success: function () {

				},
				complete: function () {
					$('#modal-soft-delete-cash').modal('hide');

					LoadCashData(typeSelect);
					createToast('success', 'fa-solid fa-circle-check', 'Success', 'Xóa thành công.');
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

//$.validator.addMethod("checkDate", function (value, element) {
//	var selectedDate = new Date(value);
//	var currentDate = new Date();
//	//currentDate.setHours(0, 0, 0, 0); // Loại bỏ thời gian để so sánh chỉ về ngày

//	return selectedDate <= currentDate;
//}, "Ngày nhập kho không được vượt quá ngày hiện tại");


function formatDate3(date) {
	let year = date.getFullYear();
	let month = (date.getMonth() + 1).toString().padStart(2, '0');
	let day = date.getDate().toString().padStart(2, '0');
	return `${year}-${month}-${day}`;
};

function DetailCash() {
	$(document).on('click', '#btn-detail-cash', function () {
		var data = $('#data-table-cash-flow').DataTable().row($(this).closest('tr')).data();
		$('#detail-cash-code').text(data.code);
		$('#detail-cash-value').text(data.value);
		switch (data.typeId) {
			case 1:
				$('#detail-cash-type').text(" Chi thuốc bảo vệ thực vật");
				break;
			case 2:
				$('#detail-cash-type').text(" Chi phân hóa học");
				break;
			case 3:
				$('#detail-cash-type').text(" Chi máy móc");
				break;
			case 4:
				$('#detail-cash-type').text(" Chi công cụ");
				break;
			case 5:
				$('#detail-cash-type').text(" Chi bán hàng");
				break;
			case 6:
				$('#detail-cash-type').text(" Chi nhân công");
				break;
			case 7:
				$('#detail-cash-type').text(" Chi đầu tư");
				break;
			case 8:
				$('#detail-cash-type').text(" Chi phát sinh khác");
				break;
			case 9:
				$('#detail-cash-type').text(" Thu sản phẩm");
				break;
			case 10:
				$('#detail-cash-type').text(" Thu hồi tài sản");
				break;
			case 11:
				$('#detail-cash-type').text(" Thu đầu tư");
				break;
			case 12:
				$('#detail-cash-type').text(" Thu sản phẩm sinh học");
				break;
			case 13:
				$('#detail-cash-type').text(" Thu khác");
				break;
			default:
				break;
		}
		$('#detail-cash-farm').text(data.farmCode);
		$('#detail-cash-user').text(data.userName);
		$('#detail-cash-created-date').text(formatDate3(new Date(data.createdTime)));
		$('#detail-cash-description').text(data.description);

		$('#btn-move-update-cash').off('click').on('click', function (event) {
			event.stopPropagation();
			$('#modal-detail-cash').modal('hide');
			/*var data = $('#data-table-item').DataTable().row($(this).closest('tr')).data();*/

			$('#update-model-cash-id').val(data.id);
			// Populate data into the update modal
			$('#update-model-cash-value').val(data.value);
			$('#update-model-cash-type').val(data.typeId);
			$('#update-model-cash-description').val(data.description);
			

			// Show the update modal
			$('#update-model-cash').modal('show');
		});

	});

}

function resetUpdateCash() {
	$('#update-model-cash-value').val(0).removeClass("is-valid");
	$('#update-model-cash-type').val(1).removeClass("is-valid");
	$('#update-model-cash-description').val(null).removeClass("is-valid");
}

function resetCreateCash() {
	$('#add-model-cash-value').val(0).removeClass("is-valid");
	$('#add-model-cash-type').val(1).removeClass("is-valid");
	$('#add-model-cash-description').val(null).removeClass("is-valid");
};

