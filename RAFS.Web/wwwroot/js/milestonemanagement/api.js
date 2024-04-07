function Delete(resId) {
    $.ajax({
        url: baseUrl + "Plant/DeleteMilestone?milestoneId=" + resId,
        type: "delete",
        contentType: "application/json",
        success: function (result, status, xhr) {
            $(this).remove();
            
            createToast('success', 'fa-solid fa-circle-check', 'Success', 'Xóa thành công');
 
            window.location = "/milestone#";

      
            setTimeout(function () {
                window.location.reload();
            }, 500);
            fetchForms(page);
        },
        error: function (xhr, status, error) {
            // Handle error
            createToast('error', 'fa-solid fa-circle-exclamation', 'Error', 'Xóa không thành công');


        }

    });
}
function checkParameter() {
    // Get the URL parameters
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);

    // Get the value of the parameter you're interested in
    const paramValue = urlParams.get('milestoneid');

    // Check if the parameter has a value
    if (paramValue !== null && paramValue !== '') {
        return paramValue;
        // Do something with the parameter value, e.g., validate, process, etc.
    } else {
        return 0;
    }
}
function Update() {

    var name = document.getElementById("name").value.trim();
    var id = document.getElementById("id").value;
    if (name === null || name.length === 0) {

        document.getElementById("name-toast").innerHTML = 'Hãy điền tên của nhóm';
    } else {
        $.ajax({
            url: baseUrl + `Plant/CheckMilestoneUpdateName?farmid=${farmid}&name=${name}&id=${id}`,
            method: 'GET',
            contentType: 'application/json',
            success: (data) => {

                if (data) {
                    document.getElementById("name-toast").innerHTML = 'Tên này đã xuất hiện hãy chọn tên khác';
                } else {
                 document.getElementById("name-toast").innerHTML = '';
                    
                    var xhttp = new XMLHttpRequest();

                    var link = baseUrl + "Plant/UpdateMilestone";

                    xhttp.open("PUT", link, true);
                    xhttp.setRequestHeader("Content-type", "application/json");
                    var obj = {
                        id: id,
                        farmId: farmid,
                        name: name,
                        description: document.getElementById("description").value
                    };
                    xhttp.onreadystatechange = function () {
                        if (xhttp.readyState === 4) {
                            var response = JSON.parse(xhttp.responseText);
                            if (xhttp.status === 200) {

                                createToast('success', 'fa-solid fa-circle-check', 'Success', 'Sửa thành công');
                                
                                setTimeout(function () {
                                    window.location.reload();
                                }, 1000);
                                window.location = "/milestone#";
                                fetchForms(page);

                            } else {
                                createToast('error', 'fa-solid fa-circle-exclamation', 'Error', 'Chưa cập nhật thành công');


                            }
                        }
                    }

                    xhttp.send(JSON.stringify(obj));
                }
            },
            error: (data) => {
                alert(data);
            }
        });
    }
}


function Create() {
    var name = document.getElementById("name2").value.trim();
    if (name === null || name.length === 0) {
        document.getElementById("name2-toast").innerHTML = 'Hãy điền tên của nhóm';
    } else {
        $.ajax({
            url: baseUrl + `Plant/CheckMilestoneName?farmid=${farmid}&name=${name}`,
            method: 'GET',
            contentType: 'application/json',
            success: (data) => {
             
                if (data) {
                    document.getElementById("name2-toast").innerHTML = 'Tên này đã xuất hiện hãy chọn tên khác';

                } else {
                    document.getElementById("name2-toast").innerHTML = '';
                    // name is not null and has non-zero length after trimming
                    var xhttp = new XMLHttpRequest();
                    var name = document.getElementById("name2").value;
                    xhttp.open("POST", baseUrl + "Plant/CreateMilestone", true);
                    xhttp.setRequestHeader("Content-type", "application/json");
                    var obj = {
                        farmId: farmid,
                        name: name,
                        description: document.getElementById("description2").value
                    };
                    xhttp.onreadystatechange = function () {
                        if (xhttp.readyState === 4) {
                            var response = JSON.parse(xhttp.responseText);
                            if (xhttp.status === 200) {
                                createToast('success', 'fa-solid fa-circle-check', 'Success', 'Thêm thành công');
                                setTimeout(function () {
                                    window.location.reload();
                                }, 500);
                                window.location = "/milestone#";
                                fetchForms(page);

                            } else {
                                createToast('success', 'fa-solid fa-circle-check', 'Success', 'Thêm thất bại');

                            }
                        }
                    }

                   
                    xhttp.send(JSON.stringify(obj));
                }
            },
            error: (data) => {
                alert(data);
            }
        });

        
    }
}
function checkInputNameForUpdate(id, input) {
    // Convert input to lower case for case-insensitive comparison
    var inputValue = input.toLowerCase();

    // Get all <tr> elements with id starting with 'milestone'
    var trElements = document.querySelectorAll('tr[id^="milestone"]');

    // Iterate over each <tr> element
    for (var i = 0; i < trElements.length; i++) {
        // Get the first <td> element within the current <tr>
        var firstTd = trElements[i].querySelector('td:first-child');

        // Get the text content of the first <td> and convert it to lower case
        var tdIdContent = firstTd.textContent.toLowerCase();

        // Check if the id matches the content of the first <td>
        if (id === tdIdContent) {
            // Get the second <td> element within the current <tr>
            var secondTd = trElements[i].querySelector('td:nth-child(2)');

            // Get the text content of the second <td> and convert it to lower case
            var tdContent = secondTd.textContent.toLowerCase();

            // Check if the input matches the content of the second <td>
            if (inputValue === tdContent) {
                return true; // Return true if both id and input match
            }
        }
    }
    return false; // Return false if no match is found
}
function normalizeString(str) {
    // Remove diacritics and normalize the string
    return str.normalize("NFD").replace(/[\u0300-\u036f]/g, "").toLowerCase();
}

function pauseExecution(milliseconds) {
    const startTime = Date.now();
    while (Date.now() < startTime + milliseconds) {
        // Loop until the specified time has passed
    }
}
const generateHtmlForm = (data) => {
    let dataGrid = '';

    data.forEach(element => {
        var dateString = element.lastUpdate;
        var date = new Date(dateString);

        // Lấy ngày, tháng và năm từ đối tượng ngày
        var day = date.getDate();
        var month = date.getMonth() + 1; // Tháng bắt đầu từ 0 nên cần cộng thêm 1
        var year = date.getFullYear();

        // Định dạng lại chuỗi ngày theo định dạng mong muốn
        var formattedDate = day + "/" + month + "/" + year;

        // Kiểm tra status và gán label tương ứng
        let statusLabel = '';

        statusLabel = `<label class="badge badge-success">${element.numberOfPlants}</label>`;


        var formid = element.id;
        dataGrid +=
            `
                                            <tr id=milestone'${element.id}'>
                                        <td>${element.id}</td>
                                        <td>${element.name}</td>
                                        <td>${element.description}</td>
                                <td>${formattedDate}</td>
                                <td>${statusLabel}</td>
                                <td>
                                  <i id = 'demo'  class='material-icons deleteffff pure2' style='font-size:28px;'>delete</i>
                                </td>
                            </tr>
                            `;
    });

    return dataGrid;
};

// Xử lý sự kiện click cho nút "Chi tiết"


const generateHtmlQuestionContent = (data) => {
    return `
                         <p>${data.description} </p>
                    `;
};

const generateHtmlFormStatus = (data) => {
    let option1 = '';
    let option2 = '';

    // Kiểm tra trạng thái của data.status và thiết lập option selected tương ứng
    if (data.status === false) {
        option1 = '<option value="false" selected>Chưa đọc</option>';
        option2 = '<option value="true">Đã trả lời</option>';
    } else {
        option1 = '<option value="false">Chưa đọc</option>';
        option2 = '<option value="true" selected>Đã trả lời</option>';
    }

    return `
                        <label for="exampleFormControlSelect1">Trạng thái</label>
                        <select class="form-control" id="exampleFormControlSelect1">
                            ${option1}
                            ${option2}
                        </select>
                    `;
};

const generateHtmlModalsBtn = (data) => {
    const formid = data.id; // Lấy formid từ dữ liệu

    return `
                         <button type="button" class="btn btn-secondary" data-dismiss="modal">Đóng</button>
                         <button id="saveStatus" class="btn btn-primary updateStatusBtn" data-formid="${formid}">Cập nhật</button>
                         
                    `;

};


$(document).on('click', '.detailBtn', function () {
    const formid = $(this).data('formid');

    // Gửi AJAX request để lấy dữ liệu dựa trên formid
    $.ajax({
        url: `https://localhost:7043/api/Guest/GetQuestionContent/${formid}`,
        method: 'GET',
        contentType: 'application/json',
        success: (data) => {
            $('#content-detail').html('');
            if (data) {
                $('#questionContent').html(generateHtmlQuestionContent(data));
                $('#formStatus').html(generateHtmlFormStatus(data));
                $('#modalsBtn').html(generateHtmlModalsBtn(data));
            }
        },
        error: (data) => {
            // Xử lý lỗi ở đây nếu cần
        }
    });
});
$(document).on('click', '.updateStatusBtn', function () {
    const formid = $(this).data('formid');
    let formStatus = $('#exampleFormControlSelect1').val();
    formStatus = formStatus === 'true';

    // Gửi AJAX request để cập nhật dữ liệu dựa trên formid
    $.ajax({
        url: `https://localhost:7043/api/Guest/UpdateStatus/${formid}`,
        method: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(formStatus),
        success: function (data) {
           

            // Đóng modal
            closeModal();

            // Thực thi mã tạo toast
            let type = 'success';
            let icon = 'fa-solid fa-circle-check';
            let title = 'Cập nhật thành công';
            let text = 'Trạng thái đã được thay đổi';
            createToast(type, icon, title, text);

            // Đợi 5 giây trước khi reload trang
            setTimeout(function () {
                window.location.reload();
            }, 500); // 5000 milliseconds = 5 giây
        },
        error: function (xhr, status, error) {
           
        }
    });
});
function closeModal() {
    $('#exampleModalCenter').modal('hide');
}