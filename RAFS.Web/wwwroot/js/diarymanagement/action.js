$(document).ready(function () {
    typeid = checkParameterType();
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
            // Check the settings id
            
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
    
    document.getElementById("update").onclick = function () { Update() };
    document.getElementById("add").onclick = function () { Create() };
    document.getElementById("createbtn").onclick = function () {
        Manager.GetAllItems();
        Manager.HideOption(1);
    };
    document.getElementById("createbtn2").onclick = function () {
        Manager.GetAllItems();
        Manager.HideOption(2);
    };
    document.getElementById("createbtn3").onclick = function () {
        Manager.GetAllItems();
        Manager.HideOption(3);
    };

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
        return paramValue;
        // Do something with the parameter value, e.g., validate, process, etc.
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

        },
        error: function (xhr, status, error) {
            createToast('error', 'fa-solid fa-circle-exclamation', 'Error', 'Sửa thất bại lỗi: ' + error);

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
            $("#PlantId2").val(result["plantId"]);
            $("#Type2").val(result["type"]);
            $("#Title2").val(result["title"]);
            $("#Body2").val(result["body"]);
            $("#image3").attr("src", result["image"]);
        }
    });
}
function detail11(event) {
    Manager.GetAllItems();
    Getdetailinfor(event)

}
function detail22(event) {
    Manager.GetAllItems();
    Getdetailinfor(event)
}
function detail33(event) {
    Manager.GetAllItems();
    Getdetailinfor(event)
}
function Update() {

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState === 4) {
            var response = JSON.parse(xhttp.responseText);
            if (xhttp.status === 200) {
                alert("update success");
                if (checkParameter() != 0) {


                    window.location = "/diary?plantid=" + checkParameter() + "&type=" + typeid;
                } else {

                    window.location = "/diary#";
                    Manager.GetAllProduct();
                }
            } else {
                alert("update faild");

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




    pauseExecution(500);

}
function chuanHoaChuoi(chuoi) {
    return chuoi.normalize("NFD").replace(/[\u0300-\u036f]/g, "");
}
function Create() {
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
    $.ajax({
        url: baseUrl + "Diary/CreateDiary",
        type: "POST",
        contentType: false,
        processData: false,
        data: formData,
        success: function (response) {
            pauseExecution(500);
            createToast('success', 'fa-solid fa-circle-check', 'Success', 'Tạo thành công');
            if (checkParameter() != 0) {


                window.location = "/diary?plantid=" + checkParameter() + "&type=" + typeid + "?#";
                Manager.GetAllProduct();
            } else {

                window.location = "/diary#";
                Manager.GetAllProduct();
            }
        },
        error: function (xhr, status, error) {
            createToast('error', 'fa-solid fa-circle-exclamation', 'Error', 'Chưa tạo thành công lỗi: ' + error);
        }
    });
};


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

function pauseExecution(milliseconds) {
    const startTime = Date.now();
    while (Date.now() < startTime + milliseconds) {
        // Loop until the specified time has passed
    }
}

var Manager = {
    GetAllProduct: function () {
        var obj = "";
        var serviceUrl = baseUrl + "Diary/GetDiaryList?farmid=" + farmid;
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
                        "<button style='border-style: none; background-color:white;'> <i id='" + item.id + "' class='material-icons deleteffff pure2' style='font-size:48px;color:red'>delete</i> </button>" +
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
                        "<button style='border-style: none; background-color:white;'> <i id='" + item.id + "' class='material-icons deleteffff pure2' style='font-size:48px;color:red'>delete</i> </button>" +
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
                        "<button style='border-style: none; background-color:white;'> <i id='" + item.id + "' class='material-icons deleteffff pure2' style='font-size:48px;color:red'>delete</i> </button>" +
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
    },
    HideOption: function (type) {
        if (type == 1) {
            $("#Type").val(1);
        }
        if (type == 2) {
            $("#Type").val(2);
        }
        if (type == 3) {
            $("#Type").val(3);
        }
    },
    GetAllItems: function () {
        var obj = "";
        var flag = document.getElementById("checkload").value;
        if (flag == 0) {
            document.getElementById("checkload").value = 1;
            var serviceUrl = "";

            serviceUrl = baseUrl + "Plant/GetPlantList?farmid=" + farmid;
            window.Manager.GetAPI(serviceUrl, onSuccess2, onFailed2);
            function onSuccess2(jsonData) {

                obj = jsonData;
                var options = "";
                $.each(jsonData, function (i, item) {
                    options += "<option value='" + item.id + "'>" + item.name + "</option>";
                });

                $('#PlantId').empty().append(options);
                $('#PlantId2').empty().append(options);
            }
            function onFailed2(error) {
                window.alert(error.statusText);
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
