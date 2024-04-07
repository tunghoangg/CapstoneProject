$(document).ready(function () {
    typeid = checkParameterType();
    var plantid = checkParameter();
    const elements = document.querySelectorAll('.backbtn');

    // Loop through each element
    elements.forEach(element => {
        // Set the href attribute
        element.setAttribute('href', '/plant?plantid=' + plantid+"#");
    });
    if (typeid != 0) {
        if (typeid == 1) {
            document.getElementById("profile").checked = true;
            document.getElementById("settings").checked = false;

            document.getElementById("posts").checked = false;
            // Check the settings id
           
        }
        if (typeid == 2) {
            document.getElementById("profile").checked = false;

            document.getElementById("settings").checked = true;

            document.getElementById("posts").checked = false;
          
            
        }
        if (typeid == 3) {
            document.getElementById("profile").checked = false;

            
            // Check the settings id
            document.getElementById("settings").checked = false;

            document.getElementById("posts").checked = true;
        }


    }
    $("#profile").on('click', function () {
        currentpage = 1;
        updatePaginationButtons(currentpage, totalPages1);
        var id3 = '#table1 tbody';

        $(id3).empty().append(getElementsByPage1(currentpage));
        updatePaginationButtons(currentpage, totalPages1);

    });
    $("#settings").on('click', function () {
        currentpage = 1;
        updatePaginationButtons(currentpage, totalPages2);
        var id3 = '#table2 tbody';

        $(id3).empty().append(getElementsByPage2(currentpage));
        updatePaginationButtons(currentpage, totalPages2);
    });
    $("#posts").on('click', function () {
        currentpage = 1;
        updatePaginationButtons(currentpage, totalPages3);
        var id3 = '#table3 tbody';

        $(id3).empty().append(getElementsByPage(currentpage));
        updatePaginationButtons(currentpage, totalPages3);

    });

    $('#pagi-pages').on('click', 'a.page-link', function () {
        if ($(this).attr('id') === 'prevPage') {
            console.log("go pre");
            if (currentpage > 1) {
                currentpage--;
                if (document.getElementById("profile").checked == true) {
                    var id3 = '#table1 tbody';

                    $(id3).empty().append(getElementsByPage1(currentpage));
                    updatePaginationButtons(currentpage, totalPages1);
                }
                if (document.getElementById("settings").checked == true) {
                    var id3 = '#table2 tbody';

                    $(id3).empty().append(getElementsByPage2(currentpage));
                    updatePaginationButtons(currentpage, totalPages2);
                }
                if (document.getElementById("posts").checked == true) {
                    var id3 = '#table3 tbody';

                    $(id3).empty().append(getElementsByPage(currentpage));
                    updatePaginationButtons(currentpage, totalPages3);
                }
                // Check the settings id


            }
        } else if ($(this).attr('id') === 'nextPage') {
            var total = 0;
            if (document.getElementById("profile").checked == true) {
                total = totalPages1;
            }

            if (document.getElementById("posts").checked == true) {
                total = totalPages3;
            }
            // Check the settings id
            if (document.getElementById("settings").checked == true) {
                total = totalPages2;
            }

            if (currentpage < total) {
                currentpage++;
                if (document.getElementById("profile").checked == true) {
                    var id3 = '#table1 tbody';

                    $(id3).empty().append(getElementsByPage1(currentpage));
                    updatePaginationButtons(currentpage, totalPages1);
                }
                if (document.getElementById("settings").checked == true) {
                    var id3 = '#table2 tbody';

                    $(id3).empty().append(getElementsByPage2(currentpage));
                    updatePaginationButtons(currentpage, totalPages2);
                }
                if (document.getElementById("posts").checked == true) {
                    var id3 = '#table3 tbody';

                    $(id3).empty().append(getElementsByPage(currentpage));
                    updatePaginationButtons(currentpage, totalPages3);
                }
            }
        } else {
            currentpage = parseInt($(this).text());
            updatePaginationButtons(currentpage, totalPages3);
        }
        if (document.getElementById("profile").checked == true) {
            var id3 = '#table1 tbody';

            $(id3).empty().append(getElementsByPage1(currentpage));
            updatePaginationButtons(currentpage, totalPages1);
        }
        if (document.getElementById("settings").checked == true) {
            var id3 = '#table2 tbody';

            $(id3).empty().append(getElementsByPage2(currentpage));
            updatePaginationButtons(currentpage, totalPages2);
        }
        if (document.getElementById("posts").checked == true) {
            var id3 = '#table3 tbody';

            $(id3).empty().append(getElementsByPage(currentpage));
            updatePaginationButtons(currentpage, totalPages3);
        }

    });


    $('#profile2').on('click', function () { typeid = 1 });
    $('#settings2').on('click', function () { typeid = 2 });
    $('#posts2').on('click', function () { typeid = 3 });
    Manager.GetAllProduct();
    $("table").on("click", ".deleteffff", function (e) {
        var resId = $(this).attr("id");
        console.log("click");
        $("#myModal").modal();
        var rows = "";
        rows += ` <tr class="milestone" id=milestoneofplant'${resId}' >
                                            <td>
                                            <h3>Việc xóa bản ghi này có thể sẽ không được khôi phục và các dữ liệu liên quan cũng sẽ biến mất (trừ dữ liệu trang QR)</h3>
                                            </td>
                                        </tr>`;
        $('#plantlist').empty().append(rows);
        
    });
    function Delete(resId) {
        $.ajax({
            url: baseUrl + "Diary/DeleteDiary?milestoneId=" + resId,
            type: "delete",
            contentType: "application/json",
            success: function (result, status, xhr) {

                createToast('success', 'fa-solid fa-circle-check', 'Success', 'Xóa thành công');

                var id2 = '#table2 tbody';
                $(id2).empty();
                id2 = '#table1 tbody';
                $(id2).empty();
                id2 = '#table3 tbody';
                $(id2).empty();
                setTimeout(function () {
                    Manager.GetAllProduct();
                }, 1000);

                window.location = "/diary?plantid=" + checkParameter() + "&type=" + checkParameterType() + "#";


            },
            error: function (xhr, status, error) {
                createToast('error', 'fa-solid fa-circle-exclamation', 'Error', error.message);
                pauseExecution(1000);

            }

        });

    }
    $('#deletebtn').on('click', function () {
        var elements = document.getElementsByClassName("milestone");
        var id = elements[0].id;

        var resId = id;
        resId = resId.slice(16);

        resId = resId.replace(/\D/g, '');
        resId = parseInt(resId);
        console.log(resId);
        Delete(resId);
    });
    document.getElementById("update").onclick = function () { Update() };
    document.getElementById("add").onclick = function () { Create() };
    document.getElementById("createbtn").onclick = function () {
        $("#PlantId").val(checkParameter());
        HideOption(1);
    };
    document.getElementById("createbtn2").onclick = function () {
        $("#PlantId").val(checkParameter());
       HideOption(2);
    };
    document.getElementById("createbtn3").onclick = function () {
        $("#PlantId").val(checkParameter());
        HideOption(3);
    };
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
})

const updatePaginationButtons = (currentPage, totalPage) => {
    $('#pagi-pages').html('');
    $('#pagi-pages').html('');
    if (totalPage > 1) {
        if (currentPage > 1) {
            $('#pagi-pages').append(`<li class="page-item"><a class="page-link" id="prevPage" href="#" aria-label="Previous"><span aria-hidden="true"></span>Trước</a></li>`);
        }
        let startPage = 1;
        let endPage = totalPage;
        if (totalPage > 3) {
            startPage = Math.max(1, currentPage - 1);
            endPage = Math.min(totalPage, currentPage + 1);
            if (startPage === 1) {
                endPage = 3;
            }
            if (endPage === totalPage) {
                startPage = totalPage - 2;
            }
        }
        for (let i = startPage; i <= endPage; i++) {
            $('#pagi-pages').append(`<li class="page-item"><a class="page-link" id="pagi-pages${i}" href="#">${i}</a></li>`);
        }
        if (currentPage < totalPage) {
            $('#pagi-pages').append(`<li class="page-item"><a class="page-link" id="nextPage" href="#" aria-label="Next"><span aria-hidden="true"></span>Tiếp</a></li>`);
        }
    }
    $(`#pagi-pages${currentPage}`).addClass('active');


};
function checkParameter() {
    // Get the URL parameters
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);

    // Get the value of the parameter you're interested in
    const paramValue = urlParams.get('plantid');

    // Check if the parameter has a value
    if (paramValue !== null && paramValue !== '') {
        return paramValue;
        // Do something with the parameter value, e.g., validate, process, etc.
    } else {
        return 0;
    }
}
function checkParameterType() {
    // Get the URL parameters
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);

    // Get the value of the parameter you're interested in
    const paramValue = urlParams.get('type');

    // Check if the parameter has a value
    if (paramValue !== null && paramValue !== '') {
        // Extract only the numeric part of the parameter value
        const numericValue = parseInt(paramValue.match(/\d+/)[0]);
        return String(numericValue)[0];
        // Do something with the numeric value, e.g., validate, process, etc.
    } else {
        return 0;
    }
}

function displaySelectedImage(event) {
    const fileInput = event.target;
    const selectedImage = document.getElementById("image3");
    if (fileInput.files && fileInput.files[0]) {
        const reader = new FileReader();

        reader.onload = function (e) {
            selectedImage.src = e.target.result;
        };

        reader.readAsDataURL(fileInput.files[0]);
    }

    updateLogoFarm(fileInput.files[0]);
};

//Upload ảnh
const updateLogoFarm = (fileLogo) => {
    var id = document.getElementById("Id2").value;
    var formData = new FormData();
    formData.append('Image', fileLogo);
    formData.append('Id', document.getElementById("Id2").value);
    $.ajax({
        url: baseUrl + "Diary/UpdateDiaryImage?Id=" + id,
        type: "PUT",
        contentType: false,
        processData: false,
        data: formData,
        success: function (response) {
            createToast('success', 'fa-solid fa-circle-check', 'Success', 'Sửa thành công');
            pauseExecution(1000);
        },
        error: function (xhr, status, error) {
            createToast('error', 'fa-solid fa-circle-exclamation', 'Error', 'Sửa thất bại lỗi: ' + error);
            pauseExecution(1000);
        }
    });

};
function Getdetailinfor(event) {
    var redId = 0;
    resId = event.target.closest("tr").id;
    $.ajax({
        url: baseUrl + "Diary/GetDiaryById?id=" + resId,
        type: "get",
        contentType: "application/json",
        success: function (result, status, xhr) {
            $("#Id2").val(result["id"]);
            $("#PlantId2").val(checkParameter());
            $("#Type2").val(result["type"]);
            $("#Title2").val(result["title"]);
            $("#Body2").val(result["body"]);
            $("#image3").attr("src", result["image"]);
        }
    });
}
function detail11(event) {

    Getdetailinfor(event)

}
function detail22(event) {

    Getdetailinfor(event)
}
function detail33(event) {

    Getdetailinfor(event)
}
function Update() {

    var value = document.getElementById("Title2").value;
    var body = document.getElementById("Body2").value;
    var numofPassField = 0;

    if (value == null || value.length === 0) {
        //     createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Tên của cây không được để trống');
        document.getElementById("Title2-toast").innerHTML = 'Tiêu đề không được để trống';

    } else if (value.length < 6) {
        document.getElementById("Title2-toast").innerHTML = 'Tiêu đề không được ít hơn 5 kí tự ';

    } else {
        numofPassField++;
    }

    if (body == null || body.length === 0) {
        //     createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Tên của cây không được để trống');
        document.getElementById("Body2-toast").innerHTML = 'Nội dung không được để trống';
    } else {
        numofPassField++;
    }

    if (numofPassField == 2) {
        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            if (xhttp.readyState === 4) {
                var response = JSON.parse(xhttp.responseText);
                if (xhttp.status === 200) {
                  
                        createToast('success', 'fa-solid fa-circle-check', 'Success', 'Sửa thành công');
                    pauseExecution(1000);
                    var id2 = '#table2 tbody';
                    $(id2).empty();
                    id2 = '#table1 tbody';
                    $(id2).empty();
                    id2 = '#table3 tbody';
                    $(id2).empty();
                        Manager.GetAllProduct();
                    window.location = "/diary?plantid=" + checkParameter() + "&type=" + checkParameterType() + "#";
                    
                   

                } else {
                    var errorMessage;
                    if (response && response.message) {
                        errorMessage = response.message;
                    } else {
                        errorMessage = "Unknown error occurred.";
                    }
                    createToast('error', 'fa-solid fa-circle-exclamation', 'Error', errorMessage);
                    pauseExecution(1000);
                }
            }
        }

        xhttp.open("PUT", baseUrl + "Diary/UpdateDiary", true);
        xhttp.setRequestHeader("Content-type", "application/json");
        var obj = {

            id: document.getElementById("Id2").value,
            plantId: document.getElementById("PlantId2").value,
            title: document.getElementById("Title2").value,
            type: document.getElementById("Type2").value,
            body: document.getElementById("Body2").value
        };


        xhttp.send(JSON.stringify(obj));
    }

}
function validateInput(inputId) {
    var value = document.getElementById(inputId).value;
    var toastId = inputId + "-toast";
    if (inputId == "Title" || inputId == "Title2") {
        if (value == null || value.length === 0) {
            //     createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Tên của cây không được để trống');
            document.getElementById("Title2-toast").innerHTML = 'Tiêu đề không được để trống';
            document.getElementById("Title-toast").innerHTML = 'Tiêu đề không được để trống';

        } else if (value.length < 6) {
            document.getElementById("Title2-toast").innerHTML = 'Tiêu đề không được ít hơn 5 kí tự ';
            document.getElementById("Title-toast").innerHTML = 'Tiêu đề không được ít hơn 5 kí tự ';

        } else {
            document.getElementById("Title2-toast").innerHTML = "";
            document.getElementById("Title-toast").innerHTML = "";
        }
    }
    if (inputId == "Body" || inputId == "Body2") {
        if (value == null || value.length === 0) {
            //     createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Tên của cây không được để trống');
            document.getElementById("Body-toast").innerHTML = 'Nội dung không được để trống';
            document.getElementById("Body2-toast").innerHTML = 'Nội dung không được để trống';

        } else {
            document.getElementById("Body-toast").innerHTML = "";
            document.getElementById("Body2-toast").innerHTML = "";
        }
    }
}
function chuanHoaChuoi(chuoi) {
    return chuoi.normalize("NFD").replace(/[\u0300-\u036f]/g, "");
}
function Create() {
    var name = document.getElementById("Title").value;
    var body = document.getElementById("Body").value;
    var numofPassField = 0;
    if (name == null || name.length === 0) {
        //     createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Tên của cây không được để trống');
        document.getElementById("Title-toast").innerHTML = 'Tiêu đề không được để trống';
    } else if (name.length < 6) {
        document.getElementById("Title-toast").innerHTML = 'Tiêu đề không được ít hơn 5 kí tự ';

    } else {
        numofPassField++;
    }

    if (body == null || body.length === 0) {
        //     createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Tên của cây không được để trống');
        document.getElementById("Body-toast").innerHTML = 'Nội dung không được để trống';
    } else {
        numofPassField++;
    }
    var image = document.getElementById("Image");
    var imageFile = image.files[0]; // Get the first selected file, if any

    if (imageFile === undefined) {
        // No file selected
        // createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Cây cần có ảnh');
        document.getElementById("Image-toast").innerHTML = 'Cây cần có ảnh';

    } else {
        // File selected
        numofPassField++;
    }
    if (numofPassField == 3) {
        const fileInput = document.getElementById("Image");

        if (fileInput.files && fileInput.files[0]) {
            const reader = new FileReader();
            reader.readAsDataURL(fileInput.files[0]);
        }

        const elementIds = ["PlantId", "Title", "Type", "Body", "Image"];

        const formData = new FormData();

        elementIds.forEach(id => {
            formData.append(id.replace(/2$/, ''), document.getElementById(id).value);
        });
        formData.append('Image', fileInput.files[0]);

        let onReadyStateChangeAdded = false;
        function sendRequest() {
            const xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function () {
                if (xhr.readyState === 4) {
                    if (xhr.status === 200) {
                        createToast('success', 'fa-solid fa-circle-check', 'Success', 'Tạo thành công');
                        var id2 = '#table2 tbody';
                        $(id2).empty();
                        id2 = '#table1 tbody';
                        $(id2).empty();
                        id2 = '#table3 tbody';
                        $(id2).empty();
                        setTimeout(function () {
                            
                        }, 1000);
                        
                        Manager.GetAllProduct();
                        window.location = "/diary?plantid=" + checkParameter() + "&type=" + checkParameterType() +"#";
                        

                    } else {
                        var errorMessage;
                        if (xhr.responseJSON && xhr.responseJSON.message) {
                            errorMessage = xhr.responseJSON.message;
                        } else {
                            errorMessage = "Unknown error occurred.";
                        }
                        createToast('error', 'fa-solid fa-circle-exclamation', 'Error', errorMessage);
                      
                    }
                }

            };
            xhr.open("POST", baseUrl + "Diary/CreateDiary", true);
            xhr.send(formData);
        
        }

        // Send the first request
        sendRequest();
    } else {

    }
}

function getElementsByPage(page) {

    // Calculate the start index based on the page number
    var startIndex = (page - 1) * 5;

    // Check if startIndex is valid
    if (startIndex < stringArray18.length) {
        // If startIndex is valid, retrieve the elements
        var endIndex = Math.min(startIndex + 5, stringArray18.length);
        return stringArray18.slice(startIndex, endIndex);
    }


}
function getElementsByPage2(page) {

    // Calculate the start index based on the page number
    var startIndex = (page - 1) * 5;

    // Check if startIndex is valid
    if (startIndex < stringArray12.length) {
        // If startIndex is valid, retrieve the elements
        var endIndex = Math.min(startIndex + 5, stringArray12.length);
        return stringArray12.slice(startIndex, endIndex);
    }


}

function HideOption(type) {
    if (type == 1) {
        $("#Type").val(1);
    }
    if (type == 2) {
        $("#Type").val(2);
    }
    if (type == 3) {
        $("#Type").val(3);
    }
}
function getElementsByPage1(page) {

    // Calculate the start index based on the page number
    var startIndex = (page - 1) * 5;

    // Check if startIndex is valid
    if (startIndex < stringArray10.length) {
        // If startIndex is valid, retrieve the elements
        var endIndex = Math.min(startIndex + 5, stringArray10.length);
        return stringArray10.slice(startIndex, endIndex);
    }


}

function pauseExecution(milliseconds) {
    const startTime = Date.now();
    while (Date.now() < startTime + milliseconds) {
        // Loop until the specified time has passed
    }
}

var Manager = {
    GetAllProduct: function () {
        var obj = "";
        var serviceUrl = baseUrl + "Diary/GetDiaryList?farmid=" + farmId;
        let params = (new URL(document.location)).searchParams;
        let id = params.get("plantid");


        if (checkParameter() != 0) {
            serviceUrl = baseUrl + "Diary/GetPlantDiaryList?plantid=" + checkParameter();
        }

        window.Manager.GetAPI(serviceUrl, onSuccess, onFailed);

        function onSuccess(jsonData) {
            stringArray10 = [];
            stringArray12 = [];
            stringArray18 = [];
            obj = jsonData;
            var rows1 = "";
            var rows2 = "";
            var rows3 = "";
            var options = "";
            $.each(jsonData, function (i, item) {

                var mydate = new Date(item.lastUpdate);
                var options = { year: 'numeric', month: 'long', day: 'numeric' };
                let text = mydate.toLocaleDateString('vi-VN', options);

                var mydate2 = new Date(item.createdDay);
                var options2 = { year: 'numeric', month: 'long', day: 'numeric' };
                let text2 = mydate2.toLocaleDateString('vi-VN', options2);

                if (item.type == 1) {

                    rows1 =

                        "<tr id='" + item.id + "' class='detail-option' onclick='detail11(event)'>" +
                        "<td><a href='#popup2' class='detail1 detail-option' onclick='detail11(event)'>" + item.id + "</a></td>" +
                        "<td><a href='#popup2' class='detail1 detail-option' onclick='detail11(event)'>" + item.plantName + "</a></td>" +
                        "<td><a href='#popup2' class='detail1 detail-option' onclick='detail11(event)'>" + item.title + "</a></td>" +
                        "<td><a href='#popup2' class='detail1 detail-option' onclick='detail11(event)'><img src='" + item.image + "' alt='Girl in a jacket'></a></td>" +
                        "<td><a href='#popup2' class='detail1 detail-option' onclick='detail11(event)'>" + text2 + "</a></td>" +
                        "<td><a href='#popup2' class='detail1 detail-option' onclick='detail11(event)'>" + text + "</a></td>" +
                        "<td>" +
                    "<button style='border-style: none; background-color:white;'> <i id='" + item.id + "' class='material-icons deleteffff pure2' style='font-size: 28px;color: black;background-color: floralwhite;height: 25px; '>delete</i> </button>" +
                        "</td>" +
                        "</tr>"
                        ;
                    stringArray10.push(rows1);
                }
                if (item.type == 2) {

                    rows2 = "<tr id='" + item.id + "' class='detail-option' onclick='detail33(event)'>" +
                        "<td><a href='#popup2' class='detail3 detail-option' onclick='detail33(event)'>" + item.id + "</a></td>" +
                        "<td><a href='#popup2' class='detail3 detail-option' onclick='detail33(event)'>" + item.plantName + "</a></td>" +
                        "<td><a href='#popup2' class='detail3 detail-option' onclick='detail33(event)'>" + item.title + "</a></td>" +
                        "<td><a href='#popup2' class='detail3 detail-option' onclick='detail33(event)'><img src='" + item.image + "' alt='Girl in a jacket'></a></td>" +
                        "<td><a href='#popup2' class='detail1 detail-option' onclick='detail11(event)'>" + text2 + "</a></td>" +
                        "<td><a href='#popup2' class='detail3 detail-option' onclick='detail33(event)'>" + text + "</a></td>" +
                        "<td>" +
                        "<button style='border-style: none; background-color:white;'> <i id='" + item.id + "' class='material-icons deleteffff pure2' style='font-size: 28px;color: black;background-color: floralwhite;height: 25px; '>delete</i> </button>" +
                        "</td>" +
                        "</tr>";
                    stringArray12.push(rows2);
                }
                if (item.type == 3) {

                    rows3 = "<tr id='" + item.id + "' class='detail-option' onclick='detail22(event)'>" +
                        "<td><a href='#popup2' class='detail2 detail-option' onclick='detail22(event)'>" + item.id + "</a></td>" +
                        "<td><a href='#popup2' class='detail2 detail-option' onclick='detail22(event)'>" + item.plantName + "</a></td>" +
                        "<td><a href='#popup2' class='detail2 detail-option' onclick='detail22(event)'>" + item.title + "</a></td>" +
                        "<td><a href='#popup2' class='detail2 detail-option' onclick='detail22(event)'><img src='" + item.image + "' alt='Girl in a jacket'></a></td>" +
                        "<td><a href='#popup2' class='detail1 detail-option' onclick='detail11(event)'>" + text2 + "</a></td>" +
                        "<td><a href='#popup2' class='detail2 detail-option' onclick='detail22(event)'>" + text + "</a></td>" +
                        "<td>" +
                        "<button style='border-style: none; background-color:white;'> <i id='" + item.id + "' class='material-icons deleteffff pure2' style='font-size: 28px;color: black;background-color: floralwhite;height: 25px; '>delete</i> </button>" +
                        "</td>" +
                        "</tr>";
                    stringArray18.push(rows3);
                }

            });
            var id2 = '#table2 tbody';
            if (stringArray12.length == 0) {

            }
            else if (stringArray12.length <= 5) {
                for (let i = 0; i < stringArray12.length; i++) {
                    $(id2).append(stringArray12[i]);
                }
                // If there are less than two elements, take the first one


            } else if (stringArray12.length > 5) {
                console.log(stringArray12.length);
                if (stringArray12.length % 5 == 0) {
                    totalPages2 = Math.round(stringArray12.length / 5);
                } else {
                    if (Math.round(stringArray12.length / 5) * 5 > stringArray12.length) {
                        totalPages2 = Math.round(stringArray12.length / 5);

                    } else {
                        totalPages2 = Math.round(stringArray12.length / 5);
                        totalPages2++;
                    }

                }
                $(id2).empty().append(getElementsByPage2(1));
            }

            var id3 = '#table3 tbody'; // Corrected the selector to target the tbody of #table3
            if (stringArray18.length == 0) {

            }
            else if (stringArray18.length <= 5) {
                for (let i = 0; i < stringArray18.length; i++) {
                    $(id3).append(stringArray18[i]);
                }
                // If there are less than two elements, take the first one


            } else if (stringArray18.length > 5) {

                if (stringArray18.length % 5 == 0) {
                    totalPages3 = Math.round(stringArray18.length / 5);
                } else {
                    if (Math.round(stringArray18.length / 5) * 5 > stringArray18.length) {
                        totalPages3 = Math.round(stringArray18.length / 5);

                    } else {
                        totalPages3 = Math.round(stringArray18.length / 5);
                        totalPages3++;
                    }
                }
                $(id3).empty().append(getElementsByPage(1));
            }

            var id = '#table1 tbody';
            if (stringArray10.length == 0) {

            }
            else if (stringArray10.length <= 5) {
                for (let i = 0; i < stringArray10.length; i++) {
                    $(id).append(stringArray10[i]);
                }
                // If there are less than two elements, take the first one


            } else if (stringArray10.length > 5) {
                if (stringArray10.length % 5 == 0) {
                    totalPages1 = Math.round(stringArray10.length / 5);
                } else {
                    if (Math.round(stringArray10.length / 5) * 5 > stringArray10.length) {
                        totalPages1 = Math.round(stringArray10.length / 5);

                    } else {
                        totalPages1 = Math.round(stringArray10.length / 5);
                        totalPages1++;
                    }

                }
                $(id).empty().append(getElementsByPage1(1));
            }

            if (document.getElementById("profile").checked == true) {
                updatePaginationButtons(1, totalPages1);
            }

            if (document.getElementById("posts").checked == true) {
                updatePaginationButtons(1, totalPages3);
            }
            // Check the settings id
            if (document.getElementById("settings").checked == true) {
                updatePaginationButtons(1, totalPages2);
            }



        }
        function onFailed(error) {
            window.alert(error.statusText);
        }
        return obj;
    },
    GetAPI: function (serviceUrl, successCallback, errorCallback) {
        $.ajax({
            type: "GET",
            url: serviceUrl,
            dataType: "json",
            success: successCallback,
            error: errorCallback
        });
    }
}
