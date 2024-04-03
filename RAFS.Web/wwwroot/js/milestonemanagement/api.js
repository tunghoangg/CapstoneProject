function Delete(resId) {
    $.ajax({
        url: baseUrl + "Plant/DeleteMilestone?milestoneId=" + resId,
        type: "delete",
        contentType: "application/json",
        success: function (result, status, xhr) {
            $(this).remove();
            alert("delete success");

            pauseExecution(250);
            window.location = "/milestone?#";

            pauseExecution(250);
            setTimeout(function () {
                window.location.reload();
            }, 500);
            fetchForms(page);
        },
        error: function (xhr, status, error) {
            // Handle error
            alert("delete fail");

        }

    });
}
// Function to update pagination buttons
function Update() {
    var xhttp = new XMLHttpRequest();
    var name = document.getElementById("name").value;
    var id = document.getElementById("updateId").value;
    var link = baseUrl + "Plant/UpdateMilestone";
    console.log(link);
    xhttp.open("PUT", link, true);
    xhttp.setRequestHeader("Content-type", "application/json");
    var obj = {
        id: document.getElementById("id").value,
        farmId: 1,
        name: document.getElementById("name").value,
        description: document.getElementById("description").value
    };
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState === 4) {
            var response = JSON.parse(xhttp.responseText);
            if (xhttp.status === 200) {
                alert("update success");

                pauseExecution(250);
                window.location = "/milestone?#";

                pauseExecution(250);
                setTimeout(function () {
                    window.location.reload();
                }, 500);
                fetchForms(page);
            } else {
                alert("update faild");

            }
        }
    }

    xhttp.send(JSON.stringify(obj));

    pauseExecution(250);

}


function Create() {
    var xhttp = new XMLHttpRequest();
    var name = document.getElementById("name2").value;
    xhttp.open("POST", baseUrl + "Plant/CreateMilestone", true);
    xhttp.setRequestHeader("Content-type", "application/json");
    var obj = {
        farmId: 1,
        name: document.getElementById("name2").value,
        description: document.getElementById("description2").value
    };
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState === 4) {
            var response = JSON.parse(xhttp.responseText);
            if (xhttp.status === 200) {
                alert("add success");

                pauseExecution(250);
                window.location = "/milestone?#";

                pauseExecution(250);
                setTimeout(function () {
                    window.location.reload();
                }, 500);

                fetchForms(page);

            } else {
                alert("add faild");

            }
        }
    }

    xhttp.send(JSON.stringify(obj));
    pauseExecution(250);
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
                                  <i id = 'demo'  class='material-icons deleteffff pure2' style='font-size:48px;'>delete</i>
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

function closeModal() {
    $('#exampleModalCenter').modal('hide');
}
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
            console.log(data);

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
            console.log("error");
            console.log(formid);
            console.log(formStatus);
        }
    });
});