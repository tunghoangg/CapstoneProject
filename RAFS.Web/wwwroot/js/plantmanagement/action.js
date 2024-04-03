$(document).ready(function () {

    document.getElementById("add").onclick = function () { Create() };
    document.getElementById("update").onclick = function () { Update() };
    Manager.GetAllProduct();
    var plantid = checkParameter()
    if (plantid != 0) {
        $('#staticBackdrop').modal();

        var resId = plantid;

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
                Manager.GetAllRecord();
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
    document.getElementById("closemodalbtn").onclick = function () {
        Manager.GetAllProduct();
        var flag = document.getElementById("checkload").value;
        var x = Number(flag);
        document.getElementById("checkload").value = 0;

    }

    document.getElementById("redirectbtn").onclick = function () {

        var textvalue = document.getElementById("titlereport").innerHTML;
        textvalue = textvalue.substring(18);

        if (textvalue === "sử dụng phân bón") { // Check if the button text is "Nhat ki"
            window.location = "/materialhistory?plantid=" + document.getElementById("id3").value + "&type=1";
        }
        if (textvalue === "sử dụng các loại thuốc") { // Check if the button text is "Nhat ki"
            window.location = "/materialhistory?plantid=" + document.getElementById("id3").value + "&type=2";
        }
        if (textvalue === "thu hoạch") { // Check if the button text is "Nhat ki"
            window.location = "/materialhistory?plantid=" + document.getElementById("id3").value + "&type=3";
        }
        if (textvalue === "chăm sóc cây theo ngày") { // Check if the button text is "Nhat ki"
            window.location = "/diary?plantid=" + document.getElementById("id3").value + "&type=1";
        }
        if (textvalue === "chăm sóc cây theo tuần") { // Check if the button text is "Nhat ki"
            window.location = "/diary?plantid=" + document.getElementById("id3").value + "&type=2";
        }
        if (textvalue === "chăm sóc cây theo tháng") { // Check if the button text is "Nhat ki"
            window.location = "/diary?plantid=" + document.getElementById("id3").value + "&type=3";
        }

    }
    document.getElementById("phan").onclick = function () {
        var inputValue = document.getElementById('id3').value;
        loadPlantMaterialHistory(inputValue, 1);
        const select = document.getElementById("selecttype");
        select.style.display = "none";

        document.getElementById("titlereport").innerHTML = "Số liệu hoạt động sử dụng phân bón";

    };
    document.getElementById("thuoc").onclick = function () {
        var inputValue = document.getElementById('id3').value;
        loadPlantMaterialHistory(inputValue, 2);
        const select = document.getElementById("selecttype");
        select.style.display = "none";

        document.getElementById("titlereport").innerHTML = "Số liệu hoạt động sử dụng các loại thuốc";

    };
    document.getElementById("qua").onclick = function () {
        var inputValue = document.getElementById('id3').value;
        loadPlantMaterialHistory(inputValue, 3);
        const select = document.getElementById("selecttype");
        select.style.display = "none";

        document.getElementById("titlereport").innerHTML = "Số liệu hoạt động thu hoạch";

    };
    document.getElementById("nhat").onclick = function () {
        var inputValue = document.getElementById('id3').value;
        loadPlantMaterialHistory(inputValue, 4);
        const select = document.getElementById("selecttype");
        select.style.display = "block";

        document.getElementById("titlereport").innerHTML = "Số liệu hoạt động chăm sóc cây theo ngày";

    };

});