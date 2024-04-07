function pauseExecution(milliseconds) {
    const startTime = Date.now();
    while (Date.now() < startTime + milliseconds) {
        // Loop until the specified time has passed
    }
}
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
const drag = (event) => {

    event.dataTransfer.setData("text/plain", event.target.id);
}

const allowDrop = (ev) => {
    ev.preventDefault();
    if (hasClass(ev.target, "dropzone")) {
        addClass(ev.target, "droppable");
    }
}

const clearDrop = (ev) => {
    removeClass(ev.target, "droppable");
}

const drop = (event) => {
    event.preventDefault();
    const data = event.dataTransfer.getData("text/plain");
    const element = document.querySelector(`#${data}`);
    const originalParent = element.parentElement;

    // Find old column name
    const draggedTitleId = element.id.substring(2);



    // Find new column ID
    const newColumn = event.target.closest('.col-sm-6');
    const newColumnId = newColumn.id.substring(9);



    // Alert the IDs of the title and the target column

    var xhttp = new XMLHttpRequest();

    xhttp.open("PUT", baseUrl + `Plant/UpdatePlanttoMilestone?plantid= ${draggedTitleId}&milestoneid=${newColumnId}`, true);
    xhttp.setRequestHeader("Content-type", "application/json");
    var obj = {

    };
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState === 4) {
            var response = JSON.parse(xhttp.responseText);
            if (xhttp.status === 200) {
                createToast('success', 'fa-solid fa-circle-check', 'Success', 'Sửa thành công');

                updateDropzones();

            } else {
                createToast('error', 'fa-solid fa-circle-exclamation', 'Error', 'Chưa cập nhật thành công');

                originalParent.appendChild(element);
            }
        }
    }
    xhttp.send(JSON.stringify(obj));


    pauseExecution(250);
    try {
        // Remove the spacer content from dropzone
        event.target.removeChild(event.target.firstChild);
        // Add the draggable content
        event.target.appendChild(element);
        // Remove the dropzone parent
        unwrap(event.target);
    } catch (error) {
        createToast('error', 'fa-solid fa-circle-exclamation', 'Error', 'Không thể chuyển đến vị trí ban đầu');
    }
}

const updateDropzones = () => {
    /* after dropping, refresh the drop target areas
      so there is a dropzone after each item
      using jQuery here for simplicity */

    var dz = $('<div class="dropzone rounded" ondrop="drop(event)" ondragover="allowDrop(event)" ondragleave="clearDrop(event)"> &nbsp; </div>');

    // delete old dropzones
    $('.dropzone').remove();

    // insert new dropdzone after each item
    dz.insertAfter('.card.draggable');

    // insert new dropzone in any empty swimlanes
    $(".items:not(:has(.card.draggable))").append(dz);

};

function handleChangeHeal(select) {
    // Get the selected option
    const selectedOption = select.options[select.selectedIndex];
    var inputValue = document.getElementById('id3').value;
    // Get the value and text of the selected option
    const selectedValue = selectedOption.value;
    const selectedText = selectedOption.textContent.trim();

    var xhttp = new XMLHttpRequest();

    xhttp.open("PUT", baseUrl + `Plant/UpdatePlantHeal?plantid= ${inputValue}&typeHeal=${selectedValue}`, true);
    xhttp.setRequestHeader("Content-type", "application/json");
    var obj = {

    };
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState === 4) {
            var response = JSON.parse(xhttp.responseText);
            if (xhttp.status === 200) {
                createToast('success', 'fa-solid fa-circle-check', 'Success', 'Sửa thành công');


            } else {
                createToast('error', 'fa-solid fa-circle-exclamation', 'Error', 'Chưa cập nhật thành công');


            }
        }
    }
    xhttp.send(JSON.stringify(obj));


    pauseExecution(250);

}
function handleChangeMilestone(select) {
    // Get the selected option
    const selectedOption = select.options[select.selectedIndex];
    var inputValue = document.getElementById('id3').value;
    // Get the value and text of the selected option
    const selectedValue = selectedOption.value;
    const selectedText = selectedOption.textContent.trim();

    var xhttp = new XMLHttpRequest();

    xhttp.open("PUT", baseUrl + `Plant/UpdatePlanttoMilestone?plantid= ${inputValue}&milestoneid=${selectedValue}`, true);
    xhttp.setRequestHeader("Content-type", "application/json");
    var obj = {

    };
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState === 4) {
            var response = JSON.parse(xhttp.responseText);
            if (xhttp.status === 200) {
                createToast('success', 'fa-solid fa-circle-check', 'Success', 'Sửa thành công');


            } else {
                createToast('error', 'fa-solid fa-circle-exclamation', 'Error', 'Chưa cập nhật thành công');


            }
        }
    }
    xhttp.send(JSON.stringify(obj));


    pauseExecution(250);

}
function handleChange(select) {
    // Get the selected option
    const selectedOption = select.options[select.selectedIndex];
    var inputValue = document.getElementById('id3').value;
    // Get the value and text of the selected option
    const selectedValue = selectedOption.value;
    const selectedText = selectedOption.textContent.trim();
    if (selectedValue == 1) {
        $('.viessss').remove();
        loadPlantMaterialHistory(inputValue, 4);
        document.getElementById("titlereport").innerHTML = "Số liệu hoạt động chăm sóc cây theo ngày";
    }
    if (selectedValue == 2) {
        $('.viessss').remove();
        loadPlantMaterialHistory(inputValue, 5);
        document.getElementById("titlereport").innerHTML = "Số liệu hoạt động chăm sóc cây theo tuần";
    }
    if (selectedValue == 3) {
        $('.viessss').remove();
        loadPlantMaterialHistory(inputValue, 6);
        document.getElementById("titlereport").innerHTML = "Số liệu hoạt động chăm sóc cây theo tháng";
    }
    // Show an alert with the selected value and text

}
function searchStringInArray(str, strArray) {
    for (var j = 0; j < strArray.length; j++) {
        if (strArray[j].match(str)) return j;
    }
    return -1;
}

function loadPlantMaterialHistory(plantid, type) {
    $('.preloader').remove();
    var obj = "";
    var typeid = 0;
    var id = "#fold-table";
    var rows = "";
    $('.viessss').remove();

    if (type == 1) {
        typeid = 12;
    }
    if (type == 2) {
        typeid = 10;
    }
    if (type == 3) {
        typeid = 18;
    }
    if (type == 4) {
        typeid = 1;
    }
    if (type == 5) {
        typeid = 2;
    }
    if (type == 6) {
        typeid = 3;
    }
    var serviceUrl = baseUrl + "PlantMaterialHistory/GetAllPlantMaterialHistoryByType?plantid=" + plantid + "&typeId=" + typeid;
    if (type == 6 | type == 5 | type == 4) {
        serviceUrl = baseUrl + "Diary/GetPlantDiaryListByType?plantid=" + plantid + "&type=" + typeid;
    }
    console.log(serviceUrl);
    window.Manager.GetAPI(serviceUrl, onSuccess, onFailed);
    
    $(id).append(`            <div class='preloader'>
                                                                                    <svg style='margin-left: 35%;' class="ip" viewBox="0 0 256 128" width="256px" height="128px" xmlns="http://www.w3.org/2000/svg">
                                                                        <defs>
                                                                            <linearGradient id="grad1" x1="0" y1="0" x2="1" y2="0">
                                                                                <stop offset="0%" stop-color="#5ebd3e" />
                                                                                <stop offset="33%" stop-color="#ffb900" />
                                                                                <stop offset="67%" stop-color="#f78200" />
                                                                                <stop offset="100%" stop-color="#e23838" />
                                                                            </linearGradient>
                                                                            <linearGradient id="grad2" x1="1" y1="0" x2="0" y2="0">
                                                                                <stop offset="0%" stop-color="#e23838" />
                                                                                <stop offset="33%" stop-color="#973999" />
                                                                                <stop offset="67%" stop-color="#009cdf" />
                                                                                <stop offset="100%" stop-color="#5ebd3e" />
                                                                            </linearGradient>
                                                                        </defs>
                                                                        <g fill="none" stroke-linecap="round" stroke-width="16">
                                                                            <g class="ip__track" stroke="#ddd">
                                                                                <path d="M8,64s0-56,60-56,60,112,120,112,60-56,60-56" />
                                                                                <path d="M248,64s0-56-60-56-60,112-120,112S8,64,8,64" />
                                                                            </g>
                                                                            <g stroke-dasharray="180 656">
                                                                                <path class="ip__worm1" stroke="url(#grad1)" stroke-dashoffset="0" d="M8,64s0-56,60-56,60,112,120,112,60-56,60-56" />
                                                                                <path class="ip__worm2" stroke="url(#grad2)" stroke-dashoffset="358" d="M248,64s0-56-60-56-60,112-120,112S8,64,8,64" />
                                                                            </g>
                                                                        </g>
                                                                    </svg>
                                                                </div>`);
    function onSuccess(jsonData) {
        let listDate = [];
       

        obj = jsonData;
        var rows = "";
        var options = "";
        var indexof = 0
        $.each(jsonData, function (i, item) {
            var mydate = new Date(item.lastUpdate);
            var options = { year: 'numeric', month: 'long', day: 'numeric' };
            let text = mydate.toLocaleDateString('vi-VN', options);
            indexof = searchStringInArray(text, listDate);

            var mydatetime = new Date(item.lastUpdate);
            var optionstime = {
                year: 'numeric',
                month: 'long',
                day: 'numeric',
                hour: 'numeric',
                minute: 'numeric',
                second: 'numeric'
            };
            let texttime = mydatetime.toLocaleDateString('vi-VN', optionstime);

            if (indexof === -1) {
                listDate.push(text);
                var rows = "";
                if (type == 6 | type == 5 | type == 4) {
                    rows = "<tr class='view viessss '><td><h3> " + text + "</h3></td></tr>" +
                        "<tr class='fold viessss'>" +
                        "<td colspan='7'>" +
                        "<div class='fold-content'>" +
                        "<h3>Chi tiết bản ghi</h3>" +
                        "<table class='small-friendly'>" +
                        "<thead>" +
                        "<tr>" +
                        "<th><span class='visible-small' title='Company name'>Comp. name</span><span class='visible-big'>Id</span></th>" +
                        "<th><span class='visible-small' title='Customer number'>Cust.#</span><span class='visible-big'>Tiêu đề</span></th>" +
                        "<th><span class='visible-small' title='Customer name'>Cust. name</span><span class='visible-big'>Nội dung</span></th>" +
                        "<th><span class='visible-small' title='Customer name'>Cust. name</span><span class='visible-big'>Ảnh</span></th>" +
                        "<th><span class='visible-small' title='Customer name'>Cust. name</span><span class='visible-big'>Thời điểm cập nhật</span></th>" +

                        "</tr>" +
                        "</thead>" +
                        "<tbody id ='indexof" + listDate.length + "'>" +
                        "<tr>" +
                        "<td data-th='Company name'>" + item.id + "</td>" +
                        "<td data-th='Customer no'>" + item.title + "</td>" +
                        "<td data-th='Customer no'>" + item.body + "</td>" +
                        "<td data-th='Customer name'> <img style='width:100%;' src='" + item.image + "' alt='Girl in a jacket'></td>" +
                        "<td data-th='Insurance no'>" + texttime + "</td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "</div>" +

                        "</td>" +
                        "</tr>";
                } else {



                    rows = "<tr class='view viessss ' ><td><h3> " + text + "</h3></td></tr>" +
                        "<tr class='fold viessss'>" +
                        "<td colspan='7'>" +
                        "<div class='fold-content'>" +
                        "<h3>Chi tiết bản ghi</h3>" +
                        "<table class='small-friendly'>" +
                        "<thead>" +
                        "<tr>" +
                        "<th><span class='visible-small' title='Company name'>Comp. name</span><span class='visible-big'> Id</span></th>" +
                        "<th><span class='visible-small' title='Customer number'>Cust.#</span><span class='visible-big'>Tên vật phẩm</span></th>" +
                        "<th><span class='visible-small' title='Customer name'>Cust. name</span><span class='visible-big'>Số lượng </span></th>" +
                        "<th><span class='visible-small' title='Customer name'>Cust. name</span><span class='visible-big'>Đơn vị </span></th>" +
                        "<th><span class='visible-small' title='Insurance number'>Ins.#</span><span class='visible-big'>Thời điểm cập nhật </span></th>" +

                        "</tr>" +
                        "</thead>" +
                        "<tbody id ='indexof" + listDate.length + "'>" +
                        "<tr>" +
                        "<td data-th='Company name'>" + item.id + "</td>" +
                        "<td data-th='Customer no'>" + item.inventoryName + "</td>" +
                        "<td data-th='Customer name'>" + item.quality + "</td>" +
                        "<td data-th='Customer name'>" + item.unit + "</td>" +
                        "<td data-th='Insurance no'>" + texttime + "</td>" +
                        "</tr>" +
                        "</tbody>" +
                        "</table>" +
                        "</div>" +

                        "</td>" +
                        "</tr>";
                }
                $('.viessss').remove();
                $(id).append(rows);

            } else {
                if (type == 6 | type == 5 | type == 4) {
                    rows = "<tr>" +
                        "<td data-th='Company name'>" + item.id + "</td>" +
                        "<td data-th='Customer no'>" + item.title + "</td>" +
                        "<td data-th='Customer no'>" + item.body + "</td>" +
                        "<td data-th='Customer name'> <img style='width:100%;'  src='" + item.image + "' alt='Girl in a jacket'></td>" +
                        "<td data-th='Insurance no'>" + texttime + "</td>" +
                        "</tr>";

                } else {
                    rows =
                        "<tr>" +
                        "<td data-th='Company name'>" + item.id + "</td>" +
                    "<td data-th='Customer no'>" + item.inventoryName + "</td>" +
                    
                    "<td data-th='Customer name'>" + item.quality + "</td>" +
                    "<td data-th='Customer name'>" + item.unit + "</td>" +
                        "<td data-th='Insurance no'>" + texttime + "</td>" +
                        "</tr>";
                }

                id = "#indexof" + (indexof + 1);

                $(id).append(rows); $('.preloader').remove();
                id = "#fold-table";
            }

        });

        $(".fold-table tr.view").on("click", function () {
            if ($(this).hasClass("open")) {
                $(this).removeClass("open").next(".fold").removeClass("open");
            } else {
                $(".fold-table tr.view").removeClass("open").next(".fold").removeClass("open");
                $(this).addClass("open").next(".fold").addClass("open");
            }
        });
        $('.preloader').remove();
        

    }
    function onFailed(error) {
        createToast('error', 'fa-solid fa-circle-exclamation', 'Error', 'Hiển thị dữ liệu thất bại mã lỗi: ' + error);
        $('.preloader').remove();

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
    var id = document.getElementById("id3").value;
    var formData = new FormData();
    formData.append('Image', fileLogo);
    formData.append('Id', document.getElementById("id3").value);
    $.ajax({
        url: baseUrl + "Plant/UpdatePlantImage?Id=" + id,
        type: "PUT",
        contentType: false,
        processData: false,
        data: formData,
        success: function (response) {
            createToast('success', 'fa-solid fa-circle-check', 'Success', 'Sửa thành công');

        },
        error: function (xhr, status, error) {
            createToast('error', 'fa-solid fa-circle-exclamation', 'Error', 'Sửa thất bại lỗi: ' + error);

        }
    });

};

function Update() {
    var name = document.getElementById("name2").value;
    var type = document.getElementById("type2").value;
    var area = document.getElementById("area2").value;
    var plantingMethod = document.getElementById("plantingMethod2").value;
    var numofPassField = 0;
    if (name == null || name.length === 0) {
        //     createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Tên của cây không được để trống');
        document.getElementById("name2-toast").innerHTML = 'Tên của cây không được để trống';
    } else if (name.length < 6) {
        document.getElementById("name2-toast").innerHTML = 'Tên của cây không được ít hơn 5 kí tự ';

    } else {
        numofPassField++;
    }
    if (type === null || type.length === 0) {
        // createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Loại cây không được để trống');
        document.getElementById("type2-toast").innerHTML = 'Loại cây không được để trống';
    } else {
        numofPassField++;
    }
    if (area === null || area.length === 0) {
        // createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Diện tích trồng cây không được để trống');
        document.getElementById("area2-toast").innerHTML = 'Diện tích trồng cây không được để trống';
    } else if (parseFloat(area) <= 0) {
        //createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Diện tích không được nhỏ hơn hoặc bằng 0');
        document.getElementById("area2-toast").innerHTML = 'Diện tích không được nhỏ hơn hoặc bằng 0';
    } else {
        numofPassField++;
    }
    if (plantingMethod === null || plantingMethod.length === 0) {
        // createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Phương thức trồng của cây không được để trống');
        document.getElementById("plantingMethod2-toast").innerHTML = 'Phương thức trồng của cây không được để trống';
    } else {
        numofPassField++;
    }

    if (numofPassField == 4) {
        var xhttp = new XMLHttpRequest();

        xhttp.open("PUT", baseUrl + "Plant/UpdatePlant", true);
        xhttp.setRequestHeader("Content-type", "application/json");
        var obj = {
            id: document.getElementById("id2").value,
            name: document.getElementById("name2").value,
            milestoneId: document.getElementById("milestoneId2").value,
            type: document.getElementById("type2").value,
            description: document.getElementById("description2").value,
            area: document.getElementById("area2").value,
            areaUnit: document.getElementById("areaUnit2").value,
            healthCondition: document.getElementById("healthCondition2").value,
            plantingMethod: document.getElementById("plantingMethod2").value

        };
        xhttp.onreadystatechange = function () {
            if (xhttp.readyState === 4) {
                var response = JSON.parse(xhttp.responseText);
                if (xhttp.status === 200) {
                    createToast('success', 'fa-solid fa-circle-check', 'Success', 'Sửa thành công');

                    window.location = "/plant?#";
                    Manager.GetAllProduct();


                } else {
                   
                    var errorMessage;
                    if (response && response.message) {
                        errorMessage = response.message;
                    } else {
                        errorMessage = "Unknown error occurred.";
                    }
                    createToast('error', 'fa-solid fa-circle-exclamation', 'Error', errorMessage);

                }
            }
        }
        xhttp.send(JSON.stringify(obj));


        pauseExecution(100);
    }
}
function validateInput(inputId) {
    var value = document.getElementById(inputId).value;
    var toastId = inputId + "-toast";
    if (inputId == "name2" || inputId == "name") {
        if (value == null || value.length === 0) {
            //     createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Tên của cây không được để trống');
            document.getElementById("name2-toast").innerHTML = 'Tên của cây không được để trống';
            document.getElementById("name-toast").innerHTML = 'Tên của cây không được để trống';

        } else if (value.length < 6) {
            document.getElementById("name2-toast").innerHTML = 'Tên của cây không được ít hơn 5 kí tự ';
            document.getElementById("name-toast").innerHTML = 'Tên của cây không được ít hơn 5 kí tự ';

        } else {
            document.getElementById("name2-toast").innerHTML = "";
            document.getElementById("name-toast").innerHTML ="";
        }
    }
    if (inputId == "type2" || inputId == "type") {
        if (value === null || value.length === 0) {
            // createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Loại cây không được để trống');
            document.getElementById("type2-toast").innerHTML = 'Loại cây không được để trống';
            document.getElementById("type-toast").innerHTML = 'Loại cây không được để trống';

        } else {
            document.getElementById("type2-toast").innerHTML = "";
            document.getElementById("type-toast").innerHTML = "";
        }
    }
    if (inputId == "area2" || inputId == "area") {
        if (value === null || value.length === 0) {
            // createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Diện tích trồng cây không được để trống');
            document.getElementById("area2-toast").innerHTML = 'Diện tích trồng cây không được để trống';
            document.getElementById("area-toast").innerHTML = 'Diện tích trồng cây không được để trống';
        } else if (parseFloat(value) <= 0) {
            //createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Diện tích không được nhỏ hơn hoặc bằng 0');
            document.getElementById("area2-toast").innerHTML = 'Diện tích không được nhỏ hơn hoặc bằng 0';
            document.getElementById("area-toast").innerHTML = 'Diện tích không được nhỏ hơn hoặc bằng 0';

        } else {
            document.getElementById("area2-toast").innerHTML = "";
            document.getElementById("area-toast").innerHTML = "";
        }
    }
    if (inputId == "plantingMethod2" || inputId == "plantingMethod") {
        if (value === null || value.length === 0) {
            // createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Phương thức trồng của cây không được để trống');
            document.getElementById("plantingMethod2-toast").innerHTML = 'Phương thức trồng của cây không được để trống';
            document.getElementById("plantingMethod-toast").innerHTML = 'Phương thức trồng của cây không được để trống';

        } else {
            document.getElementById("plantingMethod2-toast").innerHTML = "";
            document.getElementById("plantingMethod-toast").innerHTML = "";
        }
    }
}
function Delete(resId) {
    $.ajax({
        url: baseUrl + "Plant/DeletePlant?milestoneId=" + resId,
        type: "delete",
        contentType: "application/json",
        success: function (result, status, xhr) {

            $(this).remove();
            createToast('success', 'fa-solid fa-circle-check', 'Success', 'Xóa thành công');
            Manager.GetAllProduct();
        },
        error: function (error) {
            createToast('error', 'fa-solid fa-circle-exclamation', 'Error', 'Xóa không thành công');
        }

    });

}
function Create() {
    var name = document.getElementById("name").value;
   
    var type = document.getElementById("type").value;
    var area = document.getElementById("area").value;
    var plantingMethod = document.getElementById("plantingMethod").value;
   
    var numofPassField = 0;
    
    if (name == null || name.length === 0 ) {
   //     createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Tên của cây không được để trống');
        document.getElementById("name-toast").innerHTML = 'Tên của cây không được để trống';
    } else if ( name.length < 6) {
        document.getElementById("name-toast").innerHTML = 'Tên của cây không được ít hơn 5 kí tự ';

    } else {
        numofPassField++;
    }
    if (type === null || type.length === 0) {
       // createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Loại cây không được để trống');
        document.getElementById("type-toast").innerHTML = 'Loại cây không được để trống';
    } else {
        numofPassField++;
    }
    if (area === null || area.length === 0) {
       // createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Diện tích trồng cây không được để trống');
        document.getElementById("area-toast").innerHTML = 'Diện tích trồng cây không được để trống';
    } else if (parseFloat(area) <= 0) {
        //createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Diện tích không được nhỏ hơn hoặc bằng 0');
        document.getElementById("area-toast").innerHTML = 'Diện tích không được nhỏ hơn hoặc bằng 0';
    } else {
        numofPassField++;
    }
    if (plantingMethod === null || plantingMethod.length === 0) {
       // createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Phương thức trồng của cây không được để trống');
        document.getElementById("plantingMethod-toast").innerHTML = 'Phương thức trồng của cây không được để trống';
    } else {
        numofPassField++;
    }

    var image = document.getElementById("image");
    var imageFile = image.files[0]; // Get the first selected file, if any

    if (imageFile === undefined) {
        // No file selected
       // createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Cây cần có ảnh');
        document.getElementById("image-toast").innerHTML = 'Cây cần có ảnh';

    } else {
        // File selected
        numofPassField++;
    }
    if (numofPassField == 5) { 
        var description = document.getElementById("description").value;
        if (description == null || description.length === 0) {
            //     createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Tên của cây không được để trống');
            $('#description').val("không có mô tả");
        }
        const fileInput = document.getElementById("image");
        if (fileInput.files && fileInput.files[0]) {
            const reader = new FileReader();
            reader.readAsDataURL(fileInput.files[0]);
        }

        const elementIds = ["name", "milestoneId", "type", "description", "area", "areaUnit", "healthCondition", "plantingMethod"];

        const formData = new FormData();

        elementIds.forEach(id => {
            formData.append(id.replace(/2$/, ''), document.getElementById(id).value);
        });
        formData.append('Image', fileInput.files[0]);
        $.ajax({
            url: baseUrl + "Plant/CreatePlant",
            type: "POST",
            contentType: false,
            processData: false,
            data: formData,
            success: function (response) {
                createToast('success', 'fa-solid fa-circle-check', 'Success', 'Tạo thành công');
                window.location = "/plant?#";
                Manager.GetAllProduct();
            },
            error: function (xhr, status, error) {
                var errorMessage;
                if (xhr.responseJSON && xhr.responseJSON.message) {
                    errorMessage = xhr.responseJSON.message;
                } else {
                    errorMessage = "Unknown error occurred.";
                }
                createToast('error', 'fa-solid fa-circle-exclamation', 'Error', errorMessage);
            }
        });
    }
};

// helpers
function addColumn(name, id) {
    // Create HTML string for the new column
    var newColumnHTML = `
            <div class="col-sm-6 col-md-4 col-xl-3" id='milestone${id}'>
            <div class="card bg-light">
                <div class="card-body">
                       <a href="/milestone?milestoneid=${id}"> <h6 style="color: blue;" class="card-title text-uppercase text-truncate py-2">${name}</h6></a>
                        <div id='listplant${id}' class="items border border-light">
                    </div>
                  <div class="dropzone rounded" ondrop="drop(event)" ondragover="allowDrop(event)" ondragleave="clearDrop(event)"> &nbsp; </div>

                </div>
            </div>
        </div>
        `;


    // Append the new column HTML to the parent element
    var parentElement = document.querySelector('.container-fluid .row');
    parentElement.insertAdjacentHTML('beforeend', newColumnHTML);
}

function hasClass(target, className) {
    return new RegExp('(\\s|^)' + className + '(\\s|$)').test(target.className);
}

function addClass(ele, cls) {
    if (!hasClass(ele, cls)) ele.className += " " + cls;
}

function removeClass(ele, cls) {
    if (hasClass(ele, cls)) {
        var reg = new RegExp('(\\s|^)' + cls + '(\\s|$)');
        ele.className = ele.className.replace(reg, ' ');
    }
}
function makeCode(text) {
    qrcode.makeCode(text);
}
function unwrap(node) {
    node.replaceWith(...node.childNodes);
}
var Manager = {
    GetAllProduct: function () {
        let listType = [];
        var obj = "";
        var serviceUrl = baseUrl + "Plant/GetMilestoneList?farmid=" + farmId;
        window.Manager.GetAPI(serviceUrl, onSuccess, onFailed);

        function onSuccess(jsonData) {
            obj = jsonData;
            var rows = "";
            var options = "";
            var parentElement = document.querySelector('.container-fluid .row');
            parentElement.innerHTML = '';
            $.each(jsonData, function (i, item) {
                addColumn(item.name, item.id);
                options += "<option value='" + item.id + "'>" + item.name + "</option>";
            });
            $('#milestoneId').empty().append(options);
            $('#milestoneId2').empty().append(options);
            $('#milestoneId3').empty().append(options);
            // addColumn(3, rows); // Example: adding a column with 3 draggable cards and passing rows
        }

        function onFailed(error) {
            createToast('error', 'fa-solid fa-circle-exclamation', 'Error', 'Hiển thị dữ liệu thất bại mã lỗi: ' + error.statusText);

        }

        setTimeout(function () {
            serviceUrl = baseUrl + "Plant/GetPlantList?farmid=" + farmId;
            window.Manager.GetAPI(serviceUrl, onSuccess2, onFailed2);
           
            function onSuccess2(jsonData) {
                obj = jsonData;
                var rows = "";
                var health = "";
                $.each(jsonData, function (i, item) {
                    if (item.healthCondition == 1) {
                        health = "Tốt";
                    }
                    if (item.healthCondition == 2) {
                        health = "Bình thường";
                    }
                    if (item.healthCondition == 3) {
                        health = "Xấu";
                    }
                    rows +=
                        ` <div class="card draggable shadow-sm detailclass" id="cd${item.id}" draggable="true" ondragstart="drag(event)">
                    <div class="card-body p-2">
                        <div class="card-title">
                            <img src="//via.placeholder.com/30" class="rounded-circle float-right">
                            <a href="#" style="color:black;" class="lead font-weight-light">${item.name}</a>
                        </div>
                            <p>
                              <strong>Tình trạng sức khỏe: </strong> ${health}
                            </p>
                            <p>
                               <strong>Phương pháp trồng: </strong> ${item.plantingMethod}
                            </p>
                            <div class="row" style="width :100%;">
                            <div class="col-lg-5 col-md-5 col-sm-5 ">
                            <button id="detailButton" style=" padding: inherit; margin-bottom: 5px;    margin-top: 5px;  height: 100%; width: 100%;max-width: 120px;min-width: 67px;" type="button" class="btn btn-primary detail-option" data-toggle="modal"data-target="#staticBackdrop">Xem chi tiết</button>
                             </div>
                            <div class="col-lg-4 col-md-4 col-sm-4">
                            <button id="plantid${item.id}" style="padding: inherit;   margin-bottom: 5px;    margin-top: 5px;  height: 100%; background-color: gray; border-color: gray;  width: 100%;max-width: 120px;min-width: 67px;" class=" pure2 btn btn-primary btn-sm update-option">Chỉnh sửa </button>
                             </div>
                            <div class="col-lg-3 col-md-3 col-sm-3">
                            <button style="padding: inherit; background-color: red;  margin-bottom: 5px;   margin-top: 5px;   height: 100%; border-color: red; width: 100%;max-width: 120px;min-width: 55px;" id="plandeltid${item.id}" class=" pure2 btn btn-primary btn-sm delete-option">Xóa </button>    
                             </div>
                            </div>
                    </div>
                </div>
                <div class="dropzone rounded" ondrop="drop(event)" ondragover="allowDrop(event)" ondragleave="clearDrop(event)">&nbsp;</div>`;

                    var indexof = searchStringInArray(item.plantingMethod, listType);
                   
                    if (indexof === -1) {
                        listType.push(item.plantingMethod);
                    }
                   
                    var id = "#listplant" + item.milestoneId;
                    $(id).append(rows);

                    rows = "";

                });
                
                $.each(listType, function (index, option) {
                    $('#typeFilter').append($('<option>', {
                        value: option,
                        text: option
                    }));
                });

                const select = document.getElementById("selecttype");
                select.style.display = "none";
                $(".delete-option").on("click", function (e) {
                    var resId = $(this).attr("id");
                    resId = resId.slice(10);

                    resId = resId.replace(/\D/g, '');
                    resId = parseInt(resId);
                    $("#myModal").modal();
                    var rows = "";
                    rows += ` <tr class="milestone" id=milestoneofplant'${resId}' >
                                            <td>
                                            <h3>Việc xóa cây này có thể sẽ không được khôi phục và các dữ liệu liên quan cũng sẽ biến mất (trừ dữ liệu trang QR)</h3>
                                            </td>
                                        </tr>`;
                    $('#plantlist').empty().append(rows);
                });
                $(".update-option").on("click", function (e) {
                    
                    var resId = $(this).attr("id");
                    resId = resId.slice(7);

                    resId = resId.replace(/\D/g, '');
                    resId = parseInt(resId);
                   
                    $.ajax({
                        url: baseUrl + "Plant/GetPlantById?id=" + resId,
                        type: "get",
                        contentType: "application/json",
                        success: function (result, status, xhr) {
                            $("#id2").val(result["id"]);
                            $("#milestoneId2").val(result["milestoneId"]);
                            $("#name2").val(result["name"]);
                            $("#type2").val(result["type"]);
                            $("#description2").val(result["description"]);
                            $("#area2").val(result["area"]);
                            $("#areaUnit2").val(result["areaUnit"]);
                            $("#healthCondition2").val(result["healthCondition"]);
                            $("#plantingMethod2").val(result["plantingMethod"]);
                            pauseExecution(200);
                            window.location = "/plant#popup2";
                            
                        }
                    });
                    
                });
                $(".detail-option").on("click", function (e) {

                    var resId = e.target.closest(".detailclass").id.substring(2);

                    $.ajax({
                        url: baseUrl + "Plant/GetPlantById?id=" + resId,
                        type: "get",
                        contentType: "application/json",
                        success: function (result, status, xhr) {
                            makeCode(result["qrCode"] + result["id"]);
                            $("#id3").val(result["id"]);
                            $("#milestoneId3").val(result["milestoneId"]);
                            $("#name3").val(result["name"]);
                            $("#type3").val(result["type"]);
                            $("#description3").val(result["description"]);
                            $("#area3").val(result["area"]);
                            $("#areaUnit3").val(result["areaUnit"]);
                            $("#healthCondition3").val(result["healthCondition"]);
                            $("#plantingMethod3").val(result["plantingMethod"]);
                            $("#image3").attr("src", result["image"]);

                            loadPlantMaterialHistory(result["id"], 1);
                            $('.viessss').remove();
                            Manager.GetAllRecord();   
                        }
                    });





                });
            }

            function onFailed2(error) {
                createToast('error', 'fa-solid fa-circle-exclamation', 'Error', 'Hiển thị dữ liệu thất bại mã lỗi: ' + error);
            }
        }, 500);

        return obj;
    }, GetAllRecord: function () {
        
        var obj = "";
        var inputValue = document.getElementById('id3').value;
        var serviceUrl = baseUrl + "QR/GetPlantRecord?plantid=" + inputValue;
        console.log(serviceUrl);
        window.Manager.GetAPI(serviceUrl, onSuccess, onFailed);
        const elementsToDelete = document.querySelectorAll('.headerdelete');

        // Loop through each element and remove it
        elementsToDelete.forEach(element => {
            element.remove();
        });
        $('#listrecord').empty().append(`      
                                                                            <li>
                                                                            <div class='preloader'>
                                                                                        <svg style='margin-left: 35%;' class="ip" viewBox="0 0 256 128" width="256px" height="128px" xmlns="http://www.w3.org/2000/svg">
                                                                            <defs>
                                                                                <linearGradient id="grad1" x1="0" y1="0" x2="1" y2="0">
                                                                                    <stop offset="0%" stop-color="#5ebd3e" />
                                                                                    <stop offset="33%" stop-color="#ffb900" />
                                                                                    <stop offset="67%" stop-color="#f78200" />
                                                                                    <stop offset="100%" stop-color="#e23838" />
                                                                                </linearGradient>
                                                                                <linearGradient id="grad2" x1="1" y1="0" x2="0" y2="0">
                                                                                    <stop offset="0%" stop-color="#e23838" />
                                                                                    <stop offset="33%" stop-color="#973999" />
                                                                                    <stop offset="67%" stop-color="#009cdf" />
                                                                                    <stop offset="100%" stop-color="#5ebd3e" />
                                                                                </linearGradient>
                                                                            </defs>
                                                                            <g fill="none" stroke-linecap="round" stroke-width="16">
                                                                                <g class="ip__track" stroke="#ddd">
                                                                                    <path d="M8,64s0-56,60-56,60,112,120,112,60-56,60-56" />
                                                                                    <path d="M248,64s0-56-60-56-60,112-120,112S8,64,8,64" />
                                                                                </g>
                                                                                <g stroke-dasharray="180 656">
                                                                                    <path class="ip__worm1" stroke="url(#grad1)" stroke-dashoffset="0" d="M8,64s0-56,60-56,60,112,120,112,60-56,60-56" />
                                                                                    <path class="ip__worm2" stroke="url(#grad2)" stroke-dashoffset="358" d="M248,64s0-56-60-56-60,112-120,112S8,64,8,64" />
                                                                                </g>
                                                                            </g>
                                                                        </svg>
                                                                    </div>
                                                                        </li>`);
        function onSuccess(jsonData) {
            obj = jsonData;
            var rows = "";
            var footer = "";
            var options = "";
            var header = "";
            var count1 = 0;
            var count2 = 0;
            var count3 = 0;

            $.each(jsonData.plantRecords, function (i, item) {
                var mydatetime = new Date(item.createdate);
                var optionstime = {
                    year: 'numeric',
                    month: 'long',
                    day: 'numeric',
                    hour: 'numeric',
                    minute: 'numeric',
                    second: 'numeric'
                };
                let texttime = mydatetime.toLocaleDateString('vi-VN', optionstime);
                if (item.type == 1) {
                    footer += `<li style="padding-top:5%; padding-bottom:10%;  color:white; background-image: url('https://t4.ftcdn.net/jpg/03/69/11/25/360_F_369112549_7TaHV9HV6QEWIauBBJGaW7DnLd7nibkf.jpg');background-repeat: no-repeat;background-size: cover;width:100%;"
                                                                                            class="location-title rating-positive memo-yep">
                                                                                                            <h2>${item.description}</h2>
                                                                                                            <timer>${texttime}</timer>
                                                                                            <i></i>
                                                                                        </li>`;
                }
                if (item.type == 2) {
                    header += ` <div class="location-title rating-negative memo-yep headerdelete">
                                                                            <h3>${item.description}</h3>
                                                                            <time>${texttime}</time>
                                                            <i>Has a memo attached</i>
                                                        </div>`;

                }
                if (item.type == 3) {
                    rows += "<li class='rating-negative memo-yep'>" +
                        "<h3>" + item.description + "</h3>" +
                        "<time>" + texttime + "</time>" +
                        "<i></i>" +
                        "</li>";
                    count1++;
                }
                if (item.type == 4) {
                    rows += "<li class='rating-positive memo-yep1'>" +
                        "<h3>" + item.description + "</h3>" +
                        "<time>" + texttime + "</time>" +
                        "<i></i>" +
                        "</li>";
                    count2++;
                }
                if (item.type == 5) {
                    rows += "<li class='rating-positive memo-yep2'>" +
                        "<h3>" + item.description + "</h3>" +
                        "<time>" + texttime + "</time>" +
                        "<i></i>" +
                        "</li>";
                    count3++;
                }


            });
            $('#num1').html(` <i>Negative: </i> <span>${count1}</span>`);
            $('#num2').html(` <i>Negative: </i> <span>${count2}</span>`);
            $('#num3').html(` <i>Negative: </i> <span>${count3}</span>`);
            $('#totalnum').html(` <i>Negative: </i> <span>${(count3 + count2 + count1)}</span>`);

            var id = '#listrecord';
            $(id).empty().append(rows);
            $(id).append(footer);
            id = '#headerrecord';
            $(id).append(header);
            $('.preloader').remove();
        }
        function onFailed(error) {
            createToast('error', 'fa-solid fa-circle-exclamation', 'Error', 'Hiển thị dữ liệu thất bại mã lỗi: ' + error);
        }
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