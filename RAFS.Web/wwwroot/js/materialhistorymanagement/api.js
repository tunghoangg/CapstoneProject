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


function detail11(event) {

    setTimeout(function () {
        var redId = 0;
        resId = event.target.closest("tr").id;
        $.ajax({
            url: baseUrl + "PlantMaterialHistory/GetPlantMaterialRecord?id=" + resId,
            type: "get",
            contentType: "application/json",
            success: function (result, status, xhr) {
                $("#Id2").val(result["id"]);
                $("#InventoryId2").val(result["inventoryId"]);
                $("#PlantId2").val(checkParameter());
                $("#Quality2").val(result["quality"]);
            }
        });

    }, 200);
    setTimeout(function () {

        Manager.GetAllItems(4);
    }, 200);
}
function detail22(event) {
    setTimeout(function () {
        var redId = 0;
        resId = event.target.closest("tr").id;
        $.ajax({
            url: baseUrl + "PlantMaterialHistory/GetPlantMaterialRecord?id=" + resId,
            type: "get",
            contentType: "application/json",
            success: function (result, status, xhr) {
                $("#Id2").val(result["id"]);
                $("#InventoryId2").val(result["inventoryId"]);
                $("#PlantId2").val(checkParameter());
                $("#Quality2").val(result["quality"]);
            }
        });
    }, 200);
    setTimeout(function () {

        Manager.GetAllItems(5);
    }, 200);
}
function detail33(event) {
   

    setTimeout(function () {
        var redId = 0;
        resId = event.target.closest("tr").id;
        $.ajax({
            url: baseUrl + "PlantMaterialHistory/GetPlantMaterialRecord?id=" + resId,
            type: "get",
            contentType: "application/json",
            success: function (result, status, xhr) {
                $("#Id2").val(result["id"]);
                $("#InventoryId2").val(result["inventoryId"]);
                $("#PlantId2").val(checkParameter());
                $("#Quality2").val(result["quality"]);
            }
        });
    }, 200);
    setTimeout(function () {

        Manager.GetAllItems(6);
    }, 200);
}
function Update() {
    var name = document.getElementById("InventoryId2").value;
    var Quality = document.getElementById("Quality2").value;
    var numofPassField = 0;
    if (name == 0) {
        //     createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Tên của cây không được để trống');
        document.getElementById("InventoryId2-toast").innerHTML = 'Hãy chọn vật phẩm';
    } else {
        numofPassField++;
    }

    if (Quality === null || Quality.length === 0) {
        // createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Diện tích trồng cây không được để trống');
        document.getElementById("Quality2-toast").innerHTML = 'Số lượng không được để trống';
    } else if (parseFloat(Quality) <= 0) {
        //createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Diện tích không được nhỏ hơn hoặc bằng 0');
        document.getElementById("Quality2-toast").innerHTML = 'Số lượng không được nhỏ hơn hoặc bằng 0';
    } else {
        numofPassField++;
    }

    if (numofPassField == 2) {
        var xhttp = new XMLHttpRequest();

        xhttp.open("PUT", baseUrl + "PlantMaterialHistory/UpdatePlantMaterialRecord", true);
        xhttp.setRequestHeader("Content-type", "application/json");
        var obj = {
            id: document.getElementById("Id2").value,
            inventoryId: document.getElementById("InventoryId2").value,
            plantId: document.getElementById("PlantId2").value,
            quality: document.getElementById("Quality2").value

        };


        xhttp.send(JSON.stringify(obj));

        xhttp.onreadystatechange = function () {
            if (xhttp.readyState === 4) {
                var response = JSON.parse(xhttp.responseText);
                if (xhttp.status === 200) {
                   
                    createToast('success', 'fa-solid fa-circle-check', 'Success', 'Sửa thành công');
                    var id2 = '#table2 tbody';
                    $(id2).empty();
                    id2 = '#table1 tbody';
                    $(id2).empty();
                    id2 = '#table3 tbody';
                    $(id2).empty();
                    pauseExecution(1000);
                    Manager.GetAllProduct();
                    window.location = "/materialhistory?plantid=" + checkParameter() + "&type=" + checkParameterType()+"#";

                }

                else {
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


        pauseExecution(1500);
    }
}
function chuanHoaChuoi(chuoi) {
    return chuoi.normalize("NFD").replace(/[\u0300-\u036f]/g, "");
}
function validateInput(inputId) {
    var value = document.getElementById(inputId).value;
    if (inputId == "InventoryId" || inputId == "InventoryId2") {
        if (value == null || value.length === 0) {
            //     createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Tên của cây không được để trống');
            document.getElementById("InventoryId-toast").innerHTML = 'Hãy chọn vật phẩm';
            document.getElementById("InventoryId2-toast").innerHTML = 'Hãy chọn vật phẩm';

        } else {
            document.getElementById("InventoryId2-toast").innerHTML = "";
            document.getElementById("InventoryId-toast").innerHTML = "";
        }
    }
    if (inputId == "Quality" || inputId == "Quality2") {
        if (value === null || value.length === 0) {
            // createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Diện tích trồng cây không được để trống');
            document.getElementById("Quality-toast").innerHTML = 'Số lượng không được để trống';
            document.getElementById("Quality2-toast").innerHTML = 'Số lượng không được để trống';
        } else if (parseFloat(value) <= 0) {
            //createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Diện tích không được nhỏ hơn hoặc bằng 0');
            document.getElementById("Quality-toast").innerHTML = 'Số lượng không được nhỏ hơn hoặc bằng 0';
            document.getElementById("Quality2-toast").innerHTML = 'Số lượng không được nhỏ hơn hoặc bằng 0';

        } else {
            document.getElementById("Quality-toast").innerHTML = '';
            document.getElementById("Quality2-toast").innerHTML = '';
        }
    }
}
function Create() {
    var name = document.getElementById("InventoryId").value;
    var Quality = document.getElementById("Quality").value;
    var numofPassField = 0;
    if (name == 0) {
        //     createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Tên của cây không được để trống');
        document.getElementById("InventoryId-toast").innerHTML = 'Hãy chọn vật phẩm';
    } else {
        numofPassField++;
    }

    if (Quality === null || Quality.length === 0) {
        // createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Diện tích trồng cây không được để trống');
        document.getElementById("Quality-toast").innerHTML = 'Số lượng không được để trống';
    } else if (parseFloat(Quality) <= 0) {
        //createToast('warning', 'fa-solid fa-triangle-exclamation', 'Warning', 'Diện tích không được nhỏ hơn hoặc bằng 0');
        document.getElementById("Quality-toast").innerHTML = 'Số lượng không được nhỏ hơn hoặc bằng 0';
    } else {
        numofPassField++;
    }

    if (numofPassField == 2) {
        var xhttp = new XMLHttpRequest();


        xhttp.open("POST", baseUrl + "PlantMaterialHistory/CreatePlantMaterialRecord", true);
        xhttp.setRequestHeader("Content-type", "application/json");
        var obj = {
            inventoryId: document.getElementById("InventoryId").value,
            plantId: document.getElementById("PlantId").value,
            quality: document.getElementById("Quality").value
        };


        xhttp.send(JSON.stringify(obj));

        xhttp.onreadystatechange = function () {
            if (xhttp.readyState === 4) {
                var response = JSON.parse(xhttp.responseText);
                if (xhttp.status === 200) {
                   
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
                    window.location = "/materialhistory?plantid=" + checkParameter() + "&type=" + checkParameterType() + "#";
                   



                   

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
        }


        pauseExecution(1000);
    }
}
function pauseExecution(milliseconds) {
    const startTime = Date.now();
    while (Date.now() < startTime + milliseconds) {
        // Loop until the specified time has passed
    }
}
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
var Manager = {
    GetAllProduct: function () {
        var obj = "";
        var serviceUrl = baseUrl + "PlantMaterialHistory/GetAllFarmMaterialHistory?farmid=" + farmId;

        if (checkParameter() != 0) {
            serviceUrl = baseUrl + "PlantMaterialHistory/GetAllPlantMaterialHistory?plantid=" + checkParameter();
            console.log(serviceUrl);
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
            $.each(jsonData, function (i, item) {

                var mydate = new Date(item.lastUpdate);
                var options = { year: 'numeric', month: 'long', day: 'numeric' };
                let text = mydate.toLocaleDateString('vi-VN', options);

                var datetimeString = item.lastUpdate;

                // Parse the datetime string into a Date object
                var targetDatetime = new Date(datetimeString);

                // Add 1 day to the target datetime
                var oneDayLater = new Date(targetDatetime);
                oneDayLater.setDate(oneDayLater.getDate() + 1);

                // Check if the current datetime is greater than 1 day after the target datetime
                var isGreaterThanOneDay = Date.now() >= oneDayLater.getTime();
                if (item.typeId == 12) {
                    if (isGreaterThanOneDay == true) {
                        rows2 = "<tr id='" + item.id + "' class='detail-option' onclick='detail11(event)'>" +
                            "<td><a href='#' class='detail1 detail-option' onclick='detail11(event)'>" + item.id + "</a></td>" +
                            "<td><a href='#' class='detail1 detail-option' onclick='detail11(event)'>" + item.inventoryName + "</a></td>" +
                            "<td><a href='#' class='detail1 detail-option' onclick='detail11(event)'>" + item.plantName + "</a></td>" +
                            "<td><a href='#' class='detail1 detail-option' onclick='detail11(event)'><label class='badge badge-info'>" + item.quality + "</label></a></td>" +
                            "<td><a href='#' class='detail1 detail-option' onclick='detail11(event)'>" + text + "</a></td>" +
                            "<td>" +
                            "</td>" +
                            "</tr>";
                        stringArray10.push(rows2);

                    } else {
                        rows2 =

                            "<tr id='" + item.id + "' class='detail-option' onclick='detail11(event)'>" +
                            "<td><a href='#popup2' class='detail1 detail-option' onclick='detail11(event)'>" + item.id + "</a></td>" +
                            "<td><a href='#popup2' class='detail1 detail-option' onclick='detail11(event)'>" + item.inventoryName + "</a></td>" +
                            "<td><a href='#popup2' class='detail1 detail-option' onclick='detail11(event)'>" + item.plantName + "</a></td>" +
                            "<td><a href='#popup2' class='detail1 detail-option' onclick='detail11(event)'><label class='badge badge-info'>" + item.quality + "</label></a></td>" +
                            "<td><a href='#popup2' class='detail1 detail-option' onclick='detail11(event)'>" + text + "</a></td>" +
                            "<td>" +
                        "<button style='border-style: none; background-color:white;'> <i id='" + item.id + "' class='material-icons deleteffff pure2' style='font-size: 28px;color: black;background-color: floralwhite;height: 25px; '>delete</i> </button>" +
                            "</td>" +
                            "</tr>";
                        stringArray10.push(rows2);

                    }

                }
                if (item.typeId == 18) {
                    if (isGreaterThanOneDay == true) {
                        rows3 = "<tr id='" + item.id + "' class='detail-option' onclick='detail33(event)'>" +
                            "<td><a href='#' class='detail3 detail-option' onclick='detail33(event)'>" + item.id + "</a></td>" +
                            "<td><a href='#' class='detail3 detail-option' onclick='detail33(event)'>" + item.inventoryName + "</a></td>" +
                            "<td><a href='#' class='detail3 detail-option' onclick='detail33(event)'>" + item.plantName + "</a></td>" +
                            "<td><a href='#' class='detail3 detail-option' onclick='detail33(event)'><label class='badge badge-info'>" + item.quality + "</label></a></td>" +
                            "<td><a href='#' class='detail3 detail-option' onclick='detail33(event)'>" + text + "</a></td>" +
                            "<td>" +
                            "</td>" +
                            "</tr>";
                        stringArray18.push(rows3);

                    } else {
                        rows3 = "<tr id='" + item.id + "' class='detail-option' onclick='detail33(event)'>" +
                            "<td><a href='#popup2' class='detail3 detail-option' onclick='detail33(event)'>" + item.id + "</a></td>" +
                            "<td><a href='#popup2' class='detail3 detail-option' onclick='detail33(event)'>" + item.inventoryName + "</a></td>" +
                            "<td><a href='#popup2' class='detail3 detail-option' onclick='detail33(event)'>" + item.plantName + "</a></td>" +
                            "<td><a href='#popup2' class='detail3 detail-option' onclick='detail33(event)'><label class='badge badge-info'>" + item.quality + "</label></a></td>" +
                            "<td><a href='#popup2' class='detail3 detail-option' onclick='detail33(event)'>" + text + "</a></td>" +
                            "<td>" +
                            "<button style='border-style: none; background-color:white;'> <i id='" + item.id + "' class='material-icons deleteffff pure2' style='font-size: 28px;color: black;background-color: floralwhite;height: 25px; '>delete</i> </button>" +
                            "</td>" +
                            "</tr>";
                        stringArray18.push(rows3);

                    }

                }
                if (item.typeId == 10) {
                    if (isGreaterThanOneDay == true) {
                        rows1 = "<tr id='" + item.id + "' class='detail-option' onclick='detail22(event)'>" +
                            "<td><a href='#' class='detail2 detail-option' onclick='detail22(event)'>" + item.id + "</a></td>" +
                            "<td><a href='#' class='detail2 detail-option' onclick='detail22(event)'>" + item.inventoryName + "</a></td>" +
                            "<td><a href='#' class='detail2 detail-option' onclick='detail22(event)'>" + item.plantName + "</a></td>" +
                            "<td><a href='#' class='detail2 detail-option' onclick='detail22(event)'><label class='badge badge-info'>" + item.quality + "</label></a></td>" +
                            "<td><a href='#' class='detail2 detail-option' onclick='detail22(event)'>" + text + "</a></td>" +
                            "<td>" +
                            "</td>" +
                            "</tr>";
                        stringArray12.push(rows1);

                    } else {
                        rows1 = "<tr id='" + item.id + "' class='detail-option' onclick='detail22(event)'>" +
                            "<td><a href='#popup2' class='detail2 detail-option' onclick='detail22(event)'>" + item.id + "</a></td>" +
                            "<td><a href='#popup2' class='detail2 detail-option' onclick='detail22(event)'>" + item.inventoryName + "</a></td>" +
                            "<td><a href='#popup2' class='detail2 detail-option' onclick='detail22(event)'>" + item.plantName + "</a></td>" +
                            "<td><a href='#popup2' class='detail2 detail-option' onclick='detail22(event)'><label class='badge badge-info'>" + item.quality + "</label></a></td>" +
                            "<td><a href='#popup2' class='detail2 detail-option' onclick='detail22(event)'>" + text + "</a></td>" +
                            "<td>" +
                            "<button style='border-style: none; background-color:white;'> <i id='" + item.id + "' class='material-icons deleteffff pure2' style='font-size: 28px;color: black;background-color: floralwhite;height: 25px; '>delete</i> </button>" +
                            "</td>" +
                            "</tr>";
                        stringArray12.push(rows1);

                    }

                }

            });

            var id2 = '#table2 tbody'; 
            if (stringArray12.length == 0) {

            }
            else if (stringArray12.length <= 5 ) {
                for (let i = 0; i < stringArray12.length; i++) {
                    $(id2).append(stringArray12[i]);
                }
                // If there are less than two elements, take the first one
                totalPages2 = 0;

            } else if (stringArray12.length > 5) {
               
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
                totalPages3 = 0;

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
                totalPages1 = 0;

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

            if (document.getElementById("settings").checked == true) {
                updatePaginationButtons(1, totalPages2);
            }

            if (document.getElementById("posts").checked == true) {
                updatePaginationButtons(1, totalPages3);
            }
            // Check the settings id
            
            console.log(stringArray10.length);
            console.log(stringArray12.length);
            console.log(stringArray18.length);
            console.log(totalPages1);
            console.log(totalPages2);
            console.log(totalPages3);
           

        }
        function onFailed(error) {
            window.alert(error.statusText);
        }
        return obj;
    }, HideOption: function (type) {
     

    },
    GetAllItems: function (type) {
        var obj = "";
        var flag = document.getElementById("checkload").value;
        if (flag == 0) {
            document.getElementById("checkload").value = 1;
            var serviceUrl = baseUrl + "PlantMaterialHistory/GetInventoryItem?farmid=" + farmId;
            window.Manager.GetAPI(serviceUrl, onSuccess, onFailed);
            function onSuccess(jsonData) {
                obj = jsonData;
                var options = "<option value='0'> select some </option>";
                $.each(jsonData, function (i, item) {
                    if (item.typeId == 10) {
                        options += "<option class='type10' value='" + item.id + "'>" + item.itemName + "</option>";
                    }
                    if (item.typeId == 12) {
                        options += "<option class='type12' value='" + item.id + "'>" + item.itemName + "</option>";
                    }
                    if (item.typeId == 18) {
                        options += "<option class='type18' value='" + item.id + "'>" + item.itemName + "</option>";
                    }
                });
                $('#InventoryId').empty().append(options);
                $('#InventoryId2').empty().append(options);

            }
            function onFailed(error) {
                window.alert(error.statusText);
            }
          
        }
      

        if (type == 1) {
            var select = document.getElementById("InventoryId");
            
            for (var i = 0; i < select.options.length; i++) {
                if (select.options[i].classList.contains('type12')) {
                    select.options[i].hidden = false; // Hide the option

                }
                else {
                    select.options[i].hidden = true; // Hide the option

                }
            }

        }
        if (type == 2) {
            var select = document.getElementById("InventoryId");
            // Loop through options and hide those with class 'type10'
            for (var i = 0; i < select.options.length; i++) {
                if (select.options[i].classList.contains('type10')) {
                    select.options[i].hidden = false; // Hide the option

                }
                else {
                    select.options[i].hidden = true; // Hide the option

                }
            }
        }
        if (type == 3) {
            var select = document.getElementById("InventoryId");
            // Loop through options and hide those with class 'type10'
            for (var i = 0; i < select.options.length; i++) {
                if (select.options[i].classList.contains('type18')) {
                    select.options[i].hidden = false; // Hide the option

                }
                else {
                    select.options[i].hidden = true; // Hide the option

                }
            }
        }
        if (type == 4) {
            var select = document.getElementById("InventoryId2");
            for (var i = 0; i < select.options.length; i++) {
                if (select.options[i].classList.contains('type12')) {
                    select.options[i].hidden = false; // Hide the option

                }
                else {
                    select.options[i].hidden = true; // Hide the option

                }
            }
        }
        if (type == 5) {
            var select = document.getElementById("InventoryId2");
            // Loop through options and hide those with class 'type10'
            for (var i = 0; i < select.options.length; i++) {
                if (select.options[i].classList.contains('type10')) {
                    select.options[i].hidden = false; // Hide the option

                }
                else {
                    select.options[i].hidden = true; // Hide the option

                }
            }
        }
        if (type == 6) {
            var select = document.getElementById("InventoryId2");
            // Loop through options and hide those with class 'type10'
            for (var i = 0; i < select.options.length; i++) {
                if (select.options[i].classList.contains('type18')) {
                    select.options[i].hidden = false; // Hide the option

                }
                else {
                    select.options[i].hidden = true; // Hide the option

                }
            }
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