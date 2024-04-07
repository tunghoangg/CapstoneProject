/*userId = "ffce777b-ea98-4c1f-8695-dca40c2fb5b1";*/


$(document).ready(function () {

    //Lấy danh sách trang trại
    getFarms();
    //$('#buttonAddFarm').show();
    

    $('#farm-list').on('click', 'tr', function () {
        // Lấy mã trang trại từ cột đầu tiên tron-ng được click
        let farmAdminId = $(this).find('td:first').text();
        
        // Gọi hàm để lấy chi tiết trang trại và hiển thị
        getFarmDetail(farmAdminId);
        getUserList(farmAdminId);
        loadListImagesFarm(farmAdminId);
    });
    callApiAddress();
    callApiAddress2();
    //debugger
    validateImputUpdateFarm();
    $(document).on('click', '#btn-update-farm-admin', function () {

        if (validateUpdateFarm()) {
            updateFarm();
        } else {
            $("#message-btn-update-farm-admin").text("Vui lòng kiểm tra lại các trường dữ liệu.");
        }
    });
    validateImputCreateFarm();
    $(document).on('click', '#btn-create-new-farm', function () {

        if (validateCreateFarm()) {
            createFarm();
        } else {
            $("#message-btn-create-new-farm").text("Vui lòng kiểm tra lại các trường dữ liệu.");
        }
    });

    //checkCreateFarm();

    $(document).on('click', '#btn-farm-status', function (event) {
        event.stopPropagation();
        let farmIdDelete = $(this).data('farm-id');
        $('.modal-soft-delete-farm').modal('show');
        //$(document).on('click', '', function () {

        //});
        $('.confirm-change-status-farm').off('click').on('click', function (event) {
            event.stopPropagation();
            changeStatusFarm(farmIdDelete);
        });
    });
    $(document).on('click', '.button-change-user-status', function (event) {
        event.stopPropagation();
        let userIdStatus = $(this).data('user-status');
        
        $('.modal-change-status-techinician').modal('show');

        // Tránh việc gán sự kiện 'click' nhiều lần
        $('#change-status-user-confirm').off('click').on('click', function (event) {
            event.stopPropagation();
            
            changeStatusUser(userIdStatus);
        });

    });

    $(document).on('click', '.move-update-function', function (event) {

        event.stopPropagation();
        $('.detail-technician-model').modal('hide');
    });

    // Code để lấy dữ liệu từ API
    var Parameter2 = {
        url: "https://raw.githubusercontent.com/kenzouno1/DiaGioiHanhChinhVN/master/data.json",
        method: "GET",
        responseType: "application/json",
    };

    var promise2 = axios(Parameter2);
    promise2.then(function (result) {
        renderCity(result.data);
    });

    function renderCity(data) {
        for (const x of data) {
            var opt = document.createElement('option');
            opt.value = x.Name;
            opt.text = x.Name;
            opt.setAttribute('data-id', x.Id);
            $("#update-cityFarm").append(opt);
        }
    }

    //$(document).on('click', '.btn-close-rest-farm', function () {
    //    resetUpdateFarm();
    //    resetCreateFarm();
    //});

});

//Lấy danh sách trang trại 
function getFarms() {
    resetUpdateFarm();
    resetCreateFarm();
    //var button = document.getElementById("buttonAddFarm");
    //button.setAttribute("data-toggle", 'modal');
    //button.setAttribute("data-target", '.createFarmModel');

    var dataFarmLength = 0;
    $.ajax({
        url: baseUrl + `Farm/ListFarmAdminitrator/${userId}`,
        method: 'GET',
        contentType: 'application/json',
        success: (data) => {
            $('#farm-list').html('');
            dataFarmLength = data.length;
            
            CheckCreateFarm(dataFarmLength);

            if (dataFarmLength > 0) {
                //Hiển thị farm đầu tiên
                displayFarmDetail(data[0]);
                loadListImagesFarm(data[0].id);

                getUserList(data[0].id);

                $('#farm-list').html(generateHtmlFarmList(data));
            }

            //CheckCreateFarm(data);


        },
        complete: (data) => {
        },
        error: (data) => {
            createToast('error', 'fa-solid fa-circle-exclamation', 'Error', 'Không có dữ liệu phù hợp.');
        }
    });
};

function CheckCreateFarm(dataFarmLength) {
   
    $('#buttonAddFarm').on('click', function () {
        if (dataFarmLength < 5) {
            $("#buttonAddFarm").attr("data-toggle", "modal");
            $("#buttonAddFarm").attr("data-target", ".createFarmModel");
            
        }
        if (dataFarmLength >= 5) {
            $("#buttonAddFarm").removeAttr("data-toggle");
            $("#buttonAddFarm").removeAttr("data-target");
            createToast('info', 'fa-solid fa-circle-info', 'Info', 'Số lượng trang trại bị giới hạn.');
            dataFarmLength = 0
            
        }

    });
}


/*const getFarms = (userId) => */

const generateHtmlFarmList = (data) => {

    let tableContent =
        `
 <table class="table table-hover">
    <thead>
        <tr>
            <th>MÃ</th>
            <th>TÊN</th>
            <th>HÀNH ĐỘNG</th>
        </tr>
    </thead>
    <tbody>
        {0}
    </tbody>
</table>
    `;

    let tableData = '';
    data.forEach(e => {
        let statusButton = `<button id="btn-farm-status" class="btn btn-${e.status ? 'success' : 'danger'} btn-sm" data-farm-id="${e.id}">${e.status ? 'Lưu trữ' : 'Ngừng hoạt động'}</button>`;

        tableData +=
            `
<tr>
    <td style="display:none">${e.id}</td>
    <td>${e.code}</td>
    <td>${e.name}</td>
    <td>${statusButton}</td>
</tr>
        `;
    });
    //<label class="badge badge-success"></label>
    return tableContent.replace('{0}', tableData);
};


// Hàm để hiển thị thông tin chi tiết của trang trại
const displayFarmDetail = (e) => {
   
    document.getElementById("update-farm-name").value = e.name;
    document.getElementById("update-farm-establishedDate").value = formatDate(new Date(e.establishedDate));
    document.getElementById("update-farm-area").value = e.area;
    document.getElementById("update-farm-phone").value = e.phone;
    document.getElementById("update-farm-website").value = e.pageLink;
    document.getElementById("update-farm-description").value = e.description;

    setAddressForUpdateFarm(e.address);


    let detailHtml =
        `
<h4 class="card-title">
    CHI TIẾT - ${e.code}
</h4>
<input style="display:none" type="number" id="farm-id-read" value="${e.id}" readonly>
<button type="button" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".updateFarmModel" style="border:none">Cập nhật</button>
<button type="button" class="btn btn-primary btn-sm" data-toggle="modal" data-target=".updateFarmImageModal" style="border:none">Hình ảnh</button>
<div class="row">
    <div class="col-md-5 mb-3" style="padding: 18px 0px 0px 22px">
        <div style="position: relative; display: inline-block;">
            <img id="selected-logo-farm" src="${e.logo}"
                 class="rounded-circle" style="width: 140px; height: 140px; object-fit: cover;" alt=" Chưa có Logo. Hãy thêm mới " />

            <label class="form-label m-1" for="file-logo-farm"
                style="position: absolute; top: 50%; left: 50%; transform: translate(-50%, -50%); z-index: 1;">
                <i class="mdi mdi-file-image" style="display: block; opacity: 0.2;"></i>
            </label>

            <input type="file" class="form-control d-none" id="file-logo-farm" accept=".png, .jpg" onchange="displaySelectedImage(event, 'selected-logo-farm', ${e.id})" />
        </div>
    </div>
    <div class="col-md-7" style="padding-left:25px">
        <div class="row mb-2">
            <div class="col-md-5 detail-farm">
                <p class="card-text"><strong>Trang trại:</strong></p>
            </div>
            <div class="col-md-7 detail-farm">
                <p class="card-text">${e.name}</p>
            </div>
        </div>
        <div class="row mb-2">
            <div class="col-md-5 detail-farm">
                <p class="card-text"><strong>Diện tích:</strong></p>
            </div>
            <div class="col-md-7 detail-farm">
                <p class="card-text">${e.area} ha</p>
            </div>
        </div>
        <div class="row mb-2">
            <div class="col-md-5 detail-farm">
                <p class="card-text"><strong>STĐ:</strong></p>
            </div>
            <div class="col-md-7 detail-farm">
                <p class="card-text">${e.phone}</p>
            </div>
        </div>
        <div class="row mb-2">
            <div class="col-md-5 detail-farm">
                <p class="card-text"><strong>Website:</strong></p>
            </div>
            <div class="col-md-7 detail-farm">
                <p class="card-text"><a href="${e.pageLink}" class="link-info">Link</a></p>
            </div>
        </div>
    </div>
</div>

<div class="row" style="padding-left:20px">
    <div class="col-md-6">
        <div class="row mb-2">
            <div class="col-md-5 detail-farm">
                <p class="card-text"><strong>Thành lập:</strong></p>
            </div>
            <div class="col-md-7 detail-farm">
            
                <p class="card-text">${new Date(e.establishedDate).getDate()}/${new Date(e.establishedDate).getMonth() + 1}/${new Date(e.establishedDate).getFullYear()}</p>
            </div>
        </div>
    </div>
    <div class="col-md-6">
        <div class="row mb-2">
            <div class="col-md-5 detail-farm">
                <p class="card-text"><strong>Khởi tạo:</strong></p>
            </div>
            <div class="col-md-7 detail-farm">
                <p class="card-text">${new Date(e.createdDate).getDate()}/${new Date(e.createdDate).getMonth() + 1}/${new Date(e.createdDate).getFullYear()}</p>
            </div>
        </div>
    </div>
</div>

<div class="row" style="padding-left:20px">
    <div class="col-md-12">
        <div class="row mb-2">
            <div class="col-md-2 detail-farm">
                <p class="card-text"><strong>Địa chỉ:</strong></p>
            </div>
            <div class="col-md-10 detail-farm">
                <p class="card-text">${e.address}</p>
            </div>
        </div>
    </div>
</div>

<div class="row" style="padding-left:20px">
    <div class="col-md-12">
        <div class="row mb-2">
            <div class="col-md-2 detail-farm">
                <p class="card-text"><strong>Mô tả:</strong></p>
            </div>
            <div class="col-md-10 detail-farm">
                <p class="card-text">${e.description}</p>
            </div>
        </div>
    </div>
</div>
    `;



    // Hiển thị thông tin chi tiết trong phần tử đã chọn
    $('#farm-detail').html(detailHtml);

};

// Hàm để lấy chi tiết của trang trại dựa trên mã trang trại
const getFarmDetail = (farmAdminId) => {
    resetUpdateFarm();
    
    $.ajax({
        url: baseUrl + `Farm/GetFarmAdminitratorById/${farmAdminId}`,
        method: 'GET',
        contentType: 'application/json',
        //timeout: 5000,
        success: (data) => {
            // Hiển thị thông tin chi tiết của trang trại
            displayFarmDetail(data);
            /*$('.updateFarmModel').modal('show');*/
        },
        error: (data) => {
            alert("Lỗi khi lấy thông tin chi tiết trang trại");
        }
    });
};

// Hàm để lấy techinician của trang trại dựa trên mã trang trại
const getUserList = (farmIdGetUsserList) => {
    //console.log(farmIdGetUsserList, userId);
    $.ajax({
        url: baseUrl + `Farm/ListUFFAdminitrator/${userId}/${farmIdGetUsserList}`,
        method: 'GET',
        contentType: 'application/json',
        //timeout: 5000,
        success: (data) => {
            $('#user-list').html('');
            if (data.length > 0) {
                generateHtmlUserList(data);
                //console.log(data);
            }
        },
        error: (data) => {
            alert("Lỗi khi lấy thông tin người dùng");
        }
    });
};

const generateHtmlUserList = (data) => {

    let tableContent =
        `
                        <table class="table table-striped">
                            <thead>
                                <tr>
                                    <th>
                                        ẢNH
                                    </th>
                                    <th>
                                        TÊN
                                    </th>
                                    <th>
                                        SĐT
                                    </th>
                                    <th>
                                        CHỨC NĂNG
                                    </th>
                                    <th>
                                        TRẠNG THÁI
                                    </th>
                                    <th>
                                        CHI TIẾT
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                {0}
                            </tbody>
                        </table>
    `;

    let tableData = '';
    data.forEach(e => {
        let functions = '';
        e.functionDTOs.forEach(f => {
            switch (f) {
                case 2:
                    functions += 'Sổ nhật ký, ';
                    break;
                case 3:
                    functions += 'Dòng tiền, ';
                    break;
                case 4:
                    functions += 'Kho chứa, ';
                    break;
                case 5:
                    functions += 'Quản lý các nhóm, ';
                    break;
                //case 6:
                //    functions += 'Sản xuất, ';
                //    break;
                default:
                    break;
            }
        });
        functions = functions.slice(0, -2); // Loại bỏ dấu ', ' cuối cùng

        let statusButton = `<button id="btn-user-status" class="btn btn-${e.userStatus ? 'success' : 'danger'} status-button btn-sm button-change-user-status" data-user-status="${e.userId}">${e.userStatus ? 'Active' : 'Inactive'}</button>`;

        let detailButton = `<button id="user-custom-button" class="btn btn-info detail-button" data-user-id="${e.userId}" data-toggle="modal" data-target="#detail-user-farm"><i class="mdi mdi-eye user-icon"></i></button>`;

        // Lấy ra button btn-move-update-user var btnMoveUpdateUser = 


        // Kiểm tra xem button có tồn tại không trước khi thêm thuộc tính
        //if (btnMoveUpdateUser) {
        //    //let tmp = e.userId;
        //    // Thêm thuộc tính data-user-id vào button
        //    btnMoveUpdateUser;
        //}

        tableData +=
            `
                                <tr>
                                    <td class="py-1">
                                        <img src="${e.userAvatar}" alt="avatar" />
                                    </td>
                                    <td>
                                        ${e.fullName}
                                    </td>
                                    <td>
                                        ${e.userPhone}
                                    </td>
                                    <td>
                                        ${functions}
                                    </td>
                                    <td>
                                        ${statusButton}
                                    </td>
                                    <td>
                                        ${detailButton}
                                    </td>
                                </tr>
        `;

    });

    $('#user-list').html(tableContent.replace('{0}', tableData));
    //return tableContent.replace('{0}', tableData);

    $('.detail-button').click(function () {
        // Lấy userId từ data attribute của nút được click
        var userIdMove = $(this).data('user-id');
        document.getElementById('btn-move-update-user').setAttribute('data-user-function', userIdMove);
        // Tìm kiếm người dùng có userId tương ứng trong dữ liệu đã có
        var user = data.find(e => e.userId === userIdMove);

        // Hiển thị thông tin chi tiết của người dùng
        $('#uff-user-name').text(user.userName);
        //$('#uff-user-password').text(user.password);
        $('#uff-full-name').text(user.fullName);
        $('#uff-user-phone').text(user.userPhone);
        $('#uff-user-email').text(user.userEmail);

        $('#uff-so-nhat-ky').prop('checked', false);
        $('#uff-dong-tien').prop('checked', false);
        $('#uff-kho-chua').prop('checked', false);
        $('#uff-nhan-cong').prop('checked', false);
        $('#uff-san-xuat').prop('checked', false);

        let functions2 = '';
        user.functionDTOs.forEach(f => {
            switch (f) {
                case 2:
                    functions2 += 'Sổ nhật ký, ';
                    $('#uff-so-nhat-ky').prop('checked', true);
                    break;
                case 3:
                    functions2 += 'Dòng tiền, ';
                    $('#uff-dong-tien').prop('checked', true);
                    break;
                case 4:
                    functions2 += 'Kho chứa, ';
                    $('#uff-kho-chua').prop('checked', true);
                    break;
                case 5:
                    functions2 += 'Quản lý các nhóm, ';
                    $('#uff-nhan-cong').prop('checked', true);
                    break;
                //case 6:
                //    functions2 += 'Sản xuất, ';
                //    $('#uff-san-xuat').prop('checked', true);
                //    break;
                default:
                    break;
            }
        });
        functions2 = functions2.slice(0, -2); // Loại bỏ dấu ', ' cuối cùng

        $('#uff-user-function').text(functions2);
        $('#uff-user-address').text(user.userAddress);
        $('#uff-user-description').text(user.description);


    });

    $('#update-user-function-farm').off('click').click(function () {
        //document.getElementById('btn-move-update-user').modal('hide');

        // Lấy userId từ data attribute của nút được click
        var userIdFunc2 = document.getElementById('btn-move-update-user').getAttribute('data-user-function');
        //            $(this).data('data-user-function');

        // Lấy các trạng thái của các checkbox
        var uffSoNhatKy = $('#uff-so-nhat-ky').prop('checked');
        var uffDongTien = $('#uff-dong-tien').prop('checked');
        var uffKhoChua = $('#uff-kho-chua').prop('checked');
        var uffNhanCong = $('#uff-nhan-cong').prop('checked');
        //var uffSanXuat = $('#uff-san-xuat').prop('checked');


        // Gửi dữ liệu về server
        sendDataToServer(userIdFunc2, uffSoNhatKy ? 2 : 1, uffDongTien ? 3 : 1, uffKhoChua ? 4 : 1, uffNhanCong ? 5 : 1);
        //, uffSanXuat ? 6 : 1
        //sendDataToServer(userId, uffSoNhatKy, uffDongTien, uffKhoChua, uffNhanCong, uffSanXuat);
    });

};

// Hàm để gửi dữ liệu về server
const sendDataToServer = (userId, uffSoNhatKy, uffDongTien, uffKhoChua, uffNhanCong) => {
    let farmIdRead = $("#farm-id-read").val();
    //, uffSanXuat
    // Chuyển các giá trị sang kiểu int
    uffSoNhatKy = parseInt(uffSoNhatKy);
    uffDongTien = parseInt(uffDongTien);
    uffKhoChua = parseInt(uffKhoChua);
    uffNhanCong = parseInt(uffNhanCong);
    //uffSanXuat = parseInt(uffSanXuat);

    //var formData = new FormData();
    //formData.append('UserId', userId);
    //formData.append('FunctionIds', [uffSoNhatKy, uffDongTien, uffKhoChua, uffNhanCong, uffSanXuat]);
    
    $.ajax({
        url: baseUrl + "Farm/UpdateUFF",
        method: 'PUT',
        //contentType: false,
        //processData: false,
        //data: formData,
        contentType: 'application/json',
        data: JSON.stringify({
            userId: userId,
            functionIds: [uffSoNhatKy, uffDongTien, uffKhoChua, uffNhanCong]
        }),
        //timeout: 5000,
        beforeSend: function () {
            //alert("Bạn có muốn cập nhật? Điều này có thể làm thay đổi chức năng của kỹ thuật viên!");
            $('.update-user-modal').modal('hide');
        },
        success: function () {

            getUserList(farmIdRead);
        },
        complete: function () {
        },
        error: function () {
            // Xử lý lỗi (nếu có)
            alert("Lỗi! Không thể thay đổi chức năng");
        }
    });
};

function createFarm() {
    const name = $("#addFarmName").val();
    let cityFarm = $("#cityFarm").val();
    let districtFarm = $("#districtFarm").val();
    let wardFarm = $("#wardFarm").val();
    const address = wardFarm + ", " + districtFarm + ", " + cityFarm;
    const phone = $("#addFarmPhone").val();
    const pageLink = $("#addFarmWebsite").val();
    const area = $("#addFarmArea").val();
    const establishedDate = $("#addFarmEstablishDate").val();
    const description = $("#addFarmDescription").val();

    const data = {
        "userId": userId,
        "name": name,
        "address": address,
        "phone": phone,
        "pageLink": pageLink,
        "area": area,
        "establishedDate": (new Date(establishedDate)).toISOString(),
        "description": description
    };


    //Send data to the API
    $.ajax({
        url: baseUrl + "Farm/CreateFarm",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(data),
        //timeout: 5000,
        success: function () {

        },
        complete: function () {

            $('.createFarmModel').modal('hide');
            getFarms();
            resetCreateFarm();
            createToast('success', 'fa-solid fa-circle-check', 'Success', 'Tạo mới thành công.');
        },
        error: function (xhr, status, error) {
            createToast('error', 'fa-solid fa-circle-exclamation', 'Error', 'Tạo mới thất bại.');
        }
    });
};

function updateFarm() {
    let farmIdRead = $("#farm-id-read").val();
    let name = $("#update-farm-name").val();
    let cityFarm = $("#update-cityFarm").val();
    let districtFarm = $("#update-districtFarm").val();
    let wardFarm = $("#update-wardFarm").val();
    const address = wardFarm + ", " + districtFarm + ", " + cityFarm;
    const phone = $("#update-farm-phone").val();
    const pageLink = $("#update-farm-website").val();
    const area = $("#update-farm-area").val();
    const establishedDate = $("#update-farm-establishedDate").val();
    const description = $("#update-farm-description").val();

    const data = {
        "farmId": farmIdRead,
        "name": name,
        "address": address,
        "phone": phone,
        "pageLink": pageLink,
        "area": area,
        "establishedDate": (new Date(establishedDate)).toISOString(),
        "description": description
    };


    // Send data to the API
    $.ajax({
        url: baseUrl + "Farm/UpdateFarm",
        type: "PUT",
        contentType: "application/json",
        data: JSON.stringify(data),
        //timeout: 5000,
        //beforeSend: function () {
        //    $(".loaderSpinner").show();
        //},
        success: function () {
            getFarms();
        },
        complete: function () {
            // Ẩn modal khi cập nhật thành công
            $('.updateFarmModel').modal('hide');
            // Hiển thị lại dữ liệu của trang trại vừa cập nhật
            getFarmDetail(farmIdRead); // farmId cần được định nghĩa ở đây
            //$("#spinner-div").hide();
            resetUpdateFarm();
            createToast('success', 'fa-solid fa-circle-check', 'Success', 'Cập nhật thành công.');

        },
        error: function (xhr, status, error) {
            createToast('error', 'fa-solid fa-circle-exclamation', 'Error', 'Cập nhật thất bại.');
        }
    });

};

//Thay đổi trạng thái farm
function changeStatusFarm(farmIdDelete) {
    $.ajax({
        url: baseUrl + `Farm/UpdateStatusFarm/${farmIdDelete}`,
        method: 'PUT',
        contentType: false,
        processData: false,
        beforeSend: function () {
            createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Lưu trữ');
        },
        success: function (response) {
            $('.modal-soft-delete-farm').modal('hide');
            getFarms();
        },
        complete: function (response) {

            createToast('success', 'fa-solid fa-circle-check', 'Success', 'Lưu trữ thành công.');
        },
        error: function () {
            createToast('error', 'fa-solid fa-circle-exclamation', 'Error', 'Lưu trữ thất bại.');
        }
    });
};

//Thay đổi trạng thái user 
function changeStatusUser(userIdStatus) {

    let farmIdRead = $("#farm-id-read").val();

    $.ajax({
        url: baseUrl + `Farm/UpdateStatusTechnician/${userIdStatus}`,
        method: 'PUT',
        contentType: false,
        processData: false,
        beforeSend: function () {
            createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Mật khẩu thay đổi');
        },
        success: function () {
            $('.modal-change-status-techinician').modal('hide');

        },
        complete: function (response) {
            var newPass = response.responseText;

            if (newPass != "") {
                $('#mat-khau-moi').text('Mật khẩu mới của bạn là: ' + newPass);
                $('.modal-response-status-techinician').modal('show');
            }
            getUserList(farmIdRead);
            createToast('success', 'fa-solid fa-circle-check', 'Success', 'Thay đổi thành công.');

        },
        error: function () {
            createToast('error', 'fa-solid fa-circle-exclamation', 'Error', 'Thay đổi thất bại.');
        }
    });


};


function displaySelectedImage(event, elementId, farmIdImage) {
    const selectedImage = document.getElementById(elementId);
    const fileInput = event.target;

    if (fileInput.files && fileInput.files[0]) {
        const reader = new FileReader();

        reader.onload = function (e) {
            selectedImage.src = e.target.result;
        };

        reader.readAsDataURL(fileInput.files[0]);
    }

    updateLogoFarm(fileInput.files[0], farmIdImage);
};

//Upload ảnh
const updateLogoFarm = (fileLogo, farmIdImage) => {
    var formData = new FormData();
    formData.append('ImageFile', fileLogo);
    formData.append('FarmId', farmIdImage);
    $.ajax({
        url: baseUrl + "Farm/UpdateLogoFarm",
        type: "PUT",
        contentType: false,
        processData: false,
        data: formData,
        success: function (response) {
            getFarmDetail(farmIdImage);
            createToast('success', 'fa-solid fa-circle-check', 'Success', 'Thay đổi thành công.');
            //location.reload();
        },
        error: function (xhr, status, error) {
            createToast('error', 'fa-solid fa-circle-exclamation', 'Error', 'Thay đổi thất bại.');
        }
    });

};

function formatDate(date) {
    let year = date.getFullYear();
    let month = (date.getMonth() + 1).toString().padStart(2, '0');
    let day = date.getDate().toString().padStart(2, '0');
    return `${year}-${month}-${day}`;
};

function validateImputCreateFarm() {
    $("#addFarmName").on("input", function () {
        var name = $(this).val().trim();
        if (name.length < 3 || name.length > 50) {
            $(this).addClass("is-invalid").removeClass("is-valid");
            $("#message-addFarmName").text("Tên trang trại không hợp lệ.");
        } else {
            $(this).addClass("is-valid").removeClass("is-invalid");
            $("#message-addFarmName").empty();
        }
    });

    $("#addFarmEstablishDate").on("input", function () {
        var establishedDate = $(this).val().trim();
        var selectedDate = new Date(establishedDate);
        var currentDate = new Date();
        if (!establishedDate || selectedDate > currentDate) {
            $(this).addClass("is-invalid").removeClass("is-valid");
            $("#message-addFarmEstablishDate").text("Ngày thành lập không hợp lệ.");
        } else {
            $(this).addClass("is-valid").removeClass("is-invalid");
            $("#message-addFarmEstablishDate").empty();
        }
    });

    $("#addFarmArea").on("input", function () {
        var area = $(this).val().trim();
        if (isNaN(area) || parseFloat(area) < 0.036 || parseFloat(area) > 20.0) {
            $(this).addClass("is-invalid").removeClass("is-valid");
            $("#message-addFarmArea").text("Diện tích không hợp lệ.");
        } else {
            $(this).addClass("is-valid").removeClass("is-invalid");
            $("#message-addFarmArea").empty();
        }
    });

    // Sự kiện input cho trường số điện thoại
    $("#addFarmPhone").on("input", function () {
        var phone = $(this).val().trim();
        if (!/^(09|08|03|07|05)[0-9]{8}$/.test(phone)) {
            $(this).addClass("is-invalid").removeClass("is-valid");
            $("#message-addFarmPhone").text("Số điện thoại không hợp lệ.");
        } else {
            $(this).addClass("is-valid").removeClass("is-invalid");
            $("#message-addFarmPhone").empty();
        }
    });

    // Sự kiện input cho trường website
    $("#addFarmWebsite").on("input", function () {
        var pageLink = $(this).val().trim();
        if (pageLink.length > 1000) {
            $(this).addClass("is-invalid").removeClass("is-valid");
            $("#message-addFarmWebsite").text("Độ dài website không được vượt quá 1000 ký tự.");
        } else {
            $(this).addClass("is-valid").removeClass("is-invalid");
            $("#message-addFarmWebsite").empty();
        }
    });


    // Sự kiện input cho trường mô tả
    $("#addFarmDescription").on("input", function () {
        var description = $(this).val().trim();
        if (description.length < 20 || description.length > 500) {
            $(this).addClass("is-invalid").removeClass("is-valid");
            $("#message-addFarmDescription").text("Mô tả không hợp lệ.");
        } else {
            $(this).addClass("is-valid").removeClass("is-invalid");
            $("#message-addFarmDescription").empty();
        }
    });
};

function validateCreateFarm() {
    // Kiểm tra từng trường dữ liệu
    var isValid = true;

    // Kiểm tra tên trang trại
    var name = $("#addFarmName").val();
    if (name.trim().length < 3 || name.trim().length > 50) {
        isValid = false;
        $("#message-addFarmName").text("Tên trang trại không hợp lệ.");
        $("#addFarmName").addClass("is-invalid").removeClass("is-valid");
    } else {
        $("#message-addFarmName").empty();
    }

    // Kiểm tra ngày thành lập
    var establishedDate = $("#addFarmEstablishDate").val();
    var selectedDate = new Date(establishedDate);
    var currentDate = new Date();
    if (!establishedDate || selectedDate > currentDate) {
        isValid = false;
        $("#message-addFarmEstablishDate").text("Ngày thành lập không hợp lệ.");
        $("#addFarmEstablishDate").addClass("is-invalid").removeClass("is-valid");
    } else {
        $("#message-addFarmEstablishDate").empty();
    }

    // Kiểm tra diện tích
    var area = $("#addFarmArea").val();
    if (isNaN(area) || parseFloat(area) < 0.036 || parseFloat(area) > 20) {
        isValid = false;
        $("#message-addFarmArea").text("Diện tích không hợp lệ.");
        $("#addFarmArea").addClass("is-invalid").removeClass("is-valid");
    } else {
        $("#message-addFarmArea").empty();
    }

    // Kiểm tra số điện thoại
    var phone = $("#addFarmPhone").val();
    if (!/^(09|08|03|07|05)[0-9]{8}$/.test(phone)) {
        isValid = false;
        $("#message-addFarmPhone").text("Số điện thoại không hợp lệ.");
        $("#addFarmPhone").addClass("is-invalid").removeClass("is-valid");
    } else {
        $("#message-addFarmPhone").empty();
    }

    // Kiểm tra website
    var pageLink = $("#addFarmWebsite").val();
    if (pageLink.length > 1000) {
        isValid = false;
        $("#message-addFarmWebsite").text("Độ dài website không được vượt quá 1000 ký tự.");
        $("#addFarmWebsite").addClass("is-invalid").removeClass("is-valid");
    } else {
        $("#message-addFarmWebsite").empty();
    }

    // Kiểm tra địa chỉ
    //var address = $("#addFarmAddress").val();
    //if (!/^(?:\d+\s+)?(?:[A-Za-zÀ-ỹ\s]+,\s+){2}[A-Za-zÀ-ỹ\s]+,\s+[A-Za-zÀ-ỹ\s]+$/u.test(address)) {
    //    isValid = false;
    //    $("#message-addFarmAddress").text("Địa chỉ không hợp lệ.");
    //} else {
    //    $("#message-addFarmAddress").empty();
    //}
    var cityFarm = $("#cityFarm").val();
    var districtFarm = $("#districtFarm").val();
    var wardFarm = $("#wardFarm").val();

    if (!cityFarm || !districtFarm || !wardFarm) {
        isValid = false; // Trả về false nếu có bất kỳ trường nào chưa được chọn
        $("#message-addFarmAddress").text("Địa chỉ chọn thiếu");

    }

    // Kiểm tra mô tả
    var description = $("#addFarmDescription").val();
    if (description.trim().length < 20 || description.trim().length > 500) {
        isValid = false;
        $("#message-addFarmDescription").text("Mô tả không hợp lệ.");
        $("#addFarmDescription").addClass("is-invalid").removeClass("is-valid");
    } else {
        $("#message-addFarmDescription").empty();
    }

    return isValid;
}

function validateImputUpdateFarm() {
    $("#update-farm-name").on("input", function () {
        var name = $(this).val().trim();
        if (name.length < 3 || name.length > 50) {
            $(this).addClass("is-invalid").removeClass("is-valid");
            $("#message-update-farm-name").text("Tên trang trại không hợp lệ.");
        } else {
            $(this).addClass("is-valid").removeClass("is-invalid");
            $("#message-update-farm-name").empty();
        }
    });

    $("#update-farm-establishedDate").on("input", function () {
        var establishedDate = $(this).val().trim();
        var selectedDate = new Date(establishedDate);
        var currentDate = new Date();
        if (!establishedDate || selectedDate > currentDate) {
            $(this).addClass("is-invalid").removeClass("is-valid");
            $("#message-update-farm-establishedDate").text("Ngày thành lập không hợp lệ.");
        } else {
            $(this).addClass("is-valid").removeClass("is-invalid");
            $("#message-update-farm-establishedDate").empty();
        }
    });

    $("#update-farm-area").on("input", function () {
        var area = $(this).val().trim();
        if (isNaN(area) || parseFloat(area) < 0.036 || parseFloat(area) > 20.0) {
            $(this).addClass("is-invalid").removeClass("is-valid");
            $("#message-update-farm-area").text("Diện tích không hợp lệ.");
        } else {
            $(this).addClass("is-valid").removeClass("is-invalid");
            $("#message-update-farm-area").empty();
        }
    });

    // Sự kiện input cho trường số điện thoại
    $("#update-farm-phone").on("input", function () {
        var phone = $(this).val().trim();
        if (!/^(09|08|03|07|05)[0-9]{8}$/.test(phone)) {
            $(this).addClass("is-invalid").removeClass("is-valid");
            $("#message-update-farm-phone").text("Số điện thoại không hợp lệ.");
        } else {
            $(this).addClass("is-valid").removeClass("is-invalid");
            $("#message-update-farm-phone").empty();
        }
    });

    // Sự kiện input cho trường website
    $("#update-farm-website").on("input", function () {
        var pageLink = $(this).val().trim();
        if (pageLink.length > 1000) {
            $(this).addClass("is-invalid").removeClass("is-valid");
            $("#message-update-farm-website").text("Độ dài website không được vượt quá 1000 ký tự.");
        } else {
            $(this).addClass("is-valid").removeClass("is-invalid");
            $("#message-update-farm-website").empty();
        }
    });

    // Sự kiện input cho trường mô tả
    $("#update-farm-description").on("input", function () {
        var description = $(this).val().trim();
        if (description.length < 20 || description.length > 500) {
            $(this).addClass("is-invalid").removeClass("is-valid");
            $("#message-update-farm-description").text("Mô tả không hợp lệ.");
        } else {
            $(this).addClass("is-valid").removeClass("is-invalid");
            $("#message-update-farm-description").empty();
        }
    });
};
function validateUpdateFarm() {
    // Kiểm tra từng trường dữ liệu
    var isValid = true;

    // Kiểm tra tên trang trại
    var name = $("#update-farm-name").val();
    if (name.trim().length < 3 || name.trim().length > 50) {
        isValid = false;
        $("#message-update-farm-name").text("Tên trang trại không hợp lệ.");
        $("#update-farm-name").addClass("is-invalid").removeClass("is-valid");
    } else {
        $("#message-update-farm-name").empty();
    }

    // Kiểm tra ngày thành lập
    var establishedDate = $("#update-farm-establishedDate").val();
    var selectedDate = new Date(establishedDate);
    var currentDate = new Date();
    if (!establishedDate || selectedDate > currentDate) {
        isValid = false;
        $("#message-update-farm-establishedDate").text("Ngày thành lập không hợp lệ.");
        $("#update-farm-establishedDate").addClass("is-invalid").removeClass("is-valid");
    } else {
        $("#message-update-farm-establishedDate").empty();
    }

    // Kiểm tra diện tích
    var area = $("#update-farm-area").val();
    if (isNaN(area) || parseFloat(area) < 0.036 || parseFloat(area) > 20) {
        isValid = false;
        $("#message-update-farm-area").text("Diện tích không hợp lệ.");
        $("#update-farm-area").addClass("is-invalid").removeClass("is-valid");
    } else {
        $("#message-update-farm-area").empty();
    }

    // Kiểm tra số điện thoại
    var phone = $("#update-farm-phone").val();
    if (!/^(09|08|03|07|05)[0-9]{8}$/.test(phone)) {
        isValid = false;
        $("#message-update-farm-phone").text("Số điện thoại không hợp lệ.");
        $("#update-farm-phone").addClass("is-invalid").removeClass("is-valid");
    } else {
        $("#message-update-farm-phone").empty();
    }

    // Kiểm tra website
    var pageLink = $("#update-farm-website").val();
    if (pageLink.length > 1000) {
        isValid = false;
        $("#message-update-farm-website").text("Độ dài website không được vượt quá 1000 ký tự.");
        $("#update-farm-website").addClass("is-invalid").removeClass("is-valid");
    } else {
        $("#message-update-farm-website").empty();
    }


    var cityFarm = $("#update-cityFarm").val();
    var districtFarm = $("#update-districtFarm").val();
    var wardFarm = $("#update-wardFarm").val();

    if (!cityFarm || !districtFarm || !wardFarm) {
        isValid = false; // Trả về false nếu có bất kỳ trường nào chưa được chọn
        $("#message-update-farm-address").text("Địa chỉ chọn thiếu");

    }

    // Kiểm tra mô tả
    var description = $("#update-farm-description").val();
    if (description.trim().length < 20 || description.trim().length > 500) {
        isValid = false;
        $("#message-update-farm-description").text("Mô tả không hợp lệ.");
        $("#update-farm-description").addClass("is-invalid").removeClass("is-valid");
    } else {
        $("#message-update-farm-description").empty();
    }

    return isValid;
}

// Hàm để thiết lập địa chỉ trong phần cập nhật trang trại dựa trên chuỗi địa chỉ
function setAddressForUpdateFarm(address) {
    // Phân tách chuỗi địa chỉ thành các thành phần riêng biệt
    var addressComponents = address.split(',').map(component => component.trim());

    // Lấy các thành phần của địa chỉ
    var ward = addressComponents[0]; // Xã/Phường
    var district = addressComponents[1]; // Quận/Huyện
    var city = addressComponents[2]; // Tỉnh/Thành phố

    // Gán giá trị cho các select box tương ứng
    $("#update-cityFarm").val(city);
    // Trigger sự kiện onchange để cập nhật select box quận/huyện và phường/xã
    $("#update-cityFarm").change();
    $("#update-districtFarm").val(district);
    $("#update-districtFarm").change();
    $("#update-wardFarm").val(ward);


    //setDropdownValue("update-cityFarm", city);
    //setDropdownValue("update-districtFarm", district);
    //setDropdownValue("update-wardFarm", ward);   

}

// Hàm để thiết lập giá trị cho dropdown
function setDropdownValue(dropdownId, value) {
    var dropdown = document.getElementById(dropdownId);
    for (var i = 0; i < dropdown.options.length; i++) {
        if (dropdown.options[i].text === value) {
            dropdown.selectedIndex = i;
            break;
        }
    }
}


function resetUpdateFarm() {
    $("#update-farm-name").val(null);
    $("#update-farm-establishedDate").val(null);
    $("#update-farm-area").val(null);
    $("#update-farm-phone").val(null);
    $("#update-farm-website").val(null);
    $("#update-farm-description").val(null);
    $("#update-farm-name").removeClass("is-valid");
    $("#update-farm-establishedDate").removeClass("is-valid");
    $("#update-farm-area").removeClass("is-valid");
    $("#update-farm-phone").removeClass("is-valid");
    $("#update-farm-website").removeClass("is-valid");
    $("#update-farm-description").removeClass("is-valid");
}

function resetCreateFarm() {
    $("#addFarmName").val(null);
    $("#addFarmEstablishDate").val(null);
    $("#addFarmArea").val(null);
    $("#addFarmPhone").val(null);
    $("#addFarmWebsite").val(null);
    $("#addFarmDescription").val(null);
    $("#addFarmName").removeClass("is-valid");
    $("#addFarmEstablishDate").removeClass("is-valid");
    $("#addFarmArea").removeClass("is-valid");
    $("#addFarmPhone").removeClass("is-valid");
    $("#addFarmWebsite").removeClass("is-valid");
    $("#addFarmDescription").removeClass("is-valid");
};

function callApiAddress() {
    var citis = document.getElementById("cityFarm");
    var districts = document.getElementById("districtFarm");
    var wards = document.getElementById("wardFarm");
    var Parameter = {
        url: "https://raw.githubusercontent.com/kenzouno1/DiaGioiHanhChinhVN/master/data.json",
        method: "GET",
        responseType: "application/json",
    };
    var promise = axios(Parameter);
    promise.then(function (result) {
        renderCity(result.data);
    });

    function renderCity(data) {
        for (const x of data) {
            var opt = document.createElement('option');
            opt.value = x.Name;
            opt.text = x.Name;
            opt.setAttribute('data-id', x.Id);
            citis.options.add(opt);
        }

        citis.onchange = function () {
            districts.length = 1;
            wards.length = 1;
            if (this.options[this.selectedIndex].dataset.id != "") {
                const result = data.filter(n => n.Id === this.options[this.selectedIndex].dataset.id);

                for (const k of result[0].Districts) {
                    var opt = document.createElement('option');
                    opt.value = k.Name;
                    opt.text = k.Name;
                    opt.setAttribute('data-id', k.Id);
                    districts.options.add(opt);
                }
            }
        };

        districts.onchange = function () {
            wards.length = 1;
            const dataCity = data.filter((n) => n.Id === citis.options[citis.selectedIndex].dataset.id);
            if (this.options[this.selectedIndex].dataset.id != "") {
                const dataWards = dataCity[0].Districts.filter(n => n.Id === this.options[this.selectedIndex].dataset.id)[0].Wards;

                for (const w of dataWards) {
                    var opt = document.createElement('option');
                    opt.value = w.Name;
                    opt.text = w.Name;
                    opt.setAttribute('data-id', w.Id);
                    wards.options.add(opt);
                }
            }
        };
    }
};

function callApiAddress2() {
    var citis = document.getElementById("update-cityFarm");
    var districts = document.getElementById("update-districtFarm");
    var wards = document.getElementById("update-wardFarm");
    var Parameter = {
        url: "https://raw.githubusercontent.com/kenzouno1/DiaGioiHanhChinhVN/master/data.json",
        method: "GET",
        responseType: "application/json",
    };
    var promise = axios(Parameter);
    promise.then(function (result) {
        renderCity(result.data);
    });

    function renderCity(data) {
        for (const x of data) {
            var opt = document.createElement('option');
            opt.value = x.Name;
            opt.text = x.Name;
            opt.setAttribute('data-id', x.Id);
            citis.options.add(opt);
        }

        citis.onchange = function () {
            districts.length = 1;
            wards.length = 1;
            if (this.options[this.selectedIndex].dataset.id != "") {
                const result = data.filter(n => n.Id === this.options[this.selectedIndex].dataset.id);

                for (const k of result[0].Districts) {
                    var opt = document.createElement('option');
                    opt.value = k.Name;
                    opt.text = k.Name;
                    opt.setAttribute('data-id', k.Id);
                    districts.options.add(opt);
                }
            }
        };

        districts.onchange = function () {
            wards.length = 1;
            const dataCity = data.filter((n) => n.Id === citis.options[citis.selectedIndex].dataset.id);
            if (this.options[this.selectedIndex].dataset.id != "") {
                const dataWards = dataCity[0].Districts.filter(n => n.Id === this.options[this.selectedIndex].dataset.id)[0].Wards;

                for (const w of dataWards) {
                    var opt = document.createElement('option');
                    opt.value = w.Name;
                    opt.text = w.Name;
                    opt.setAttribute('data-id', w.Id);
                    wards.options.add(opt);
                }
            }
        };
    }
};

const loadListImagesFarm = (farmAdminId) => {

    $.ajax({
        url: baseUrl + `Farm/GetImagesByFarmId/${farmAdminId}`,
        method: 'GET',
        contentType: 'application/json',
        success: (data) => {
            if (data.length >= 5) {
                $('#selected-imgaes-farm-1').attr('src', data[0].imageURL);
                $('#selected-imgaes-farm-1').attr('src', data[0].imageURL);
                $('#selected-imgaes-farm-2').attr('src', data[1].imageURL);
                $('#selected-imgaes-farm-3').attr('src', data[2].imageURL);
                $('#selected-imgaes-farm-4').attr('src', data[3].imageURL);
                $('#selected-imgaes-farm-5').attr('src', data[4].imageURL);

                $('#file-images-farm-1').attr('onchange', `displaySelectedImagesFarm(event, 'selected-imgaes-farm-1', ${data[0].imageId})`);
                $('#file-images-farm-2').attr('onchange', `displaySelectedImagesFarm(event, 'selected-imgaes-farm-2', ${data[1].imageId})`);
                $('#file-images-farm-3').attr('onchange', `displaySelectedImagesFarm(event, 'selected-imgaes-farm-3', ${data[2].imageId})`);
                $('#file-images-farm-4').attr('onchange', `displaySelectedImagesFarm(event, 'selected-imgaes-farm-4', ${data[3].imageId})`);
                $('#file-images-farm-5').attr('onchange', `displaySelectedImagesFarm(event, 'selected-imgaes-farm-5', ${data[4].imageId})`);
                //onchange="displaySelectedImage(event, 'selected-logo-farm', ${e.id})"
            }
        },
        complete: (data) => {
        },
        error: (data) => {
            createToast('error', 'fa-solid fa-circle-exclamation', 'Error', 'Không có dữ liệu phù hợp.');
        }
    });
};

function displaySelectedImagesFarm(event, elementId, imageId) {
    let selectedImage = document.getElementById(elementId);
    let fileInput = event.target;

    if (fileInput.files && fileInput.files[0]) {
        let reader = new FileReader();

        reader.onload = function (e) {
            selectedImage.src = e.target.result;
        };

        reader.readAsDataURL(fileInput.files[0]);
    }

    updateImageFarm(fileInput.files[0], imageId);
};

const updateImageFarm = (fileImage, imageId) => {
    var formData = new FormData();
    formData.append('ImageFile', fileImage);
    formData.append('ImageId', imageId);
    $.ajax({
        url: baseUrl + "Farm/UpdateImagesFarm",
        type: "PUT",
        contentType: false,
        processData: false,
        data: formData,
        success: function (response) {
            //getFarmDetail(farmIdImage);
            createToast('success', 'fa-solid fa-circle-check', 'Success', 'Thay đổi thành công.');
            //location.reload();
        },
        error: function (xhr, status, error) {
            createToast('error', 'fa-solid fa-circle-exclamation', 'Error', 'Thay đổi thất bại.');
        }
    });

};


