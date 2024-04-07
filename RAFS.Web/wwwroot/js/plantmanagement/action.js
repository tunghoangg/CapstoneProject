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
    } else {
        window.location = "/plant#";
    }
    document.getElementById('resetBtn').addEventListener('click', function () {
        document.getElementById('searchName').value = "";
        document.getElementById('healFilter').value = 0;
        document.getElementById('typeFilter').value = 0;
        var elements = document.querySelectorAll('.detailclass');
        elements.forEach(function (element) {

            element.removeAttribute('hidden');

        });
    });
    document.getElementById('sortBtn').addEventListener('click', function() {
        // Step 1: Add hidden attribute to all elements
        var elements = document.querySelectorAll('.detailclass');
        elements.forEach(function(element) {
            element.setAttribute('hidden', true);
        });

        // Step 2: Get input and select values
        var searchName = document.getElementById('searchName').value.trim();
        var healFilter = document.getElementById('healFilter').value;
        var typeFilter = document.getElementById('typeFilter').value;

        var acceptable1 = true;
        var acceptable2 = true;
        var acceptable3 = true;
        if (searchName === '' || searchName.length ==0) {
            acceptable1 = false;
        }
        if (healFilter == '0') {
            acceptable2 = false;
        }
        if (typeFilter == '0') {
            acceptable3 = false;
        }
        // Step 3: Find and remove hidden attribute based on input and select values
        elements.forEach(function (element) {
            var nameElement = element.querySelector('.card-title .lead');
            var healStatusElement = element.querySelector('.card-body p:nth-of-type(1)');
            var plantingMethodElement = element.querySelector('.card-body p:nth-of-type(2)');
          

            var name = nameElement.textContent.trim();
            var healStatus = healStatusElement.textContent.trim();
            var plantingMethod = plantingMethodElement.textContent.trim();

          

            if (acceptable1 && acceptable2 && acceptable3) { 
            // Perform comparisons and remove hidden attribute if conditions are met
                if (name.toLowerCase().includes(searchName.toLowerCase()) &&
                    ( healStatus.includes(healFilter)) &&
                    (plantingMethod.includes(typeFilter))) {
                    element.removeAttribute('hidden');
                    }
            }
            if (acceptable1 == false && acceptable2 && acceptable3) {
                if ( (healStatus.includes(healFilter)) &&
                    (plantingMethod.includes(typeFilter))) {
                    element.removeAttribute('hidden');
                }

            }
            if (acceptable1 && acceptable2 == false && acceptable3) {
                if (name.toLowerCase().includes(searchName.toLowerCase()) &&
                    (plantingMethod.includes(typeFilter))) {
                    element.removeAttribute('hidden');
                }
            }
            if (acceptable1 && acceptable2 && acceptable3 == false) {
                if (name.toLowerCase().includes(searchName.toLowerCase()) &&
                    (healStatus.includes(healFilter))) {
                    element.removeAttribute('hidden');
                }
            }
            if (acceptable1 == false && acceptable2 == false && acceptable3) {
                if ((plantingMethod.includes(typeFilter))) {
                    element.removeAttribute('hidden');
                }
            }
            if (acceptable1 && acceptable2 == false && acceptable3 == false) {
                if (name.toLowerCase().includes(searchName.toLowerCase())) {
                    element.removeAttribute('hidden');
                }
            }
            if (acceptable1 == false && acceptable2 && acceptable3 == false) {
                if ((healStatus.includes(healFilter))) {
                    element.removeAttribute('hidden');
                }
            }
            if (acceptable1 == false && acceptable2 == false && acceptable3 == false) {
                element.removeAttribute('hidden');
            }

        });
    });
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

        window.location = "/plant#";
        var flag = document.getElementById("checkload").value;
        var x = Number(flag);
        document.getElementById("checkload").value = 0;

    }
    document.getElementById("topClose").onclick = function () {
        window.location = "/plant#";
        var flag = document.getElementById("checkload").value;
        var x = Number(flag);
        document.getElementById("checkload").value = 0;

    }

    document.getElementById("redirectbtn").onclick = function () {

        var textvalue = document.getElementById("titlereport").innerHTML;
        textvalue = textvalue.substring(18);

        if (textvalue === "sử dụng phân bón") { // Check if the button text is "Nhat ki"
            window.location = "/materialhistory?plantid=" + document.getElementById("id3").value + "&type=1#";
        }
        if (textvalue === "sử dụng các loại thuốc") { // Check if the button text is "Nhat ki"
            window.location = "/materialhistory?plantid=" + document.getElementById("id3").value + "&type=2#";
        }
        if (textvalue === "thu hoạch") { // Check if the button text is "Nhat ki"
            window.location = "/materialhistory?plantid=" + document.getElementById("id3").value + "&type=3#";
        }
        if (textvalue === "chăm sóc cây theo ngày") { // Check if the button text is "Nhat ki"
            window.location = "/diary?plantid=" + document.getElementById("id3").value + "&type=1#";
        }
        if (textvalue === "chăm sóc cây theo tuần") { // Check if the button text is "Nhat ki"
            window.location = "/diary?plantid=" + document.getElementById("id3").value + "&type=2#";
        }
        if (textvalue === "chăm sóc cây theo tháng") { // Check if the button text is "Nhat ki"
            window.location = "/diary?plantid=" + document.getElementById("id3").value + "&type=3#";
        }

    }
    document.getElementById("phan").onclick = function () {
        $('.viessss').remove();
        var inputValue = document.getElementById('id3').value;
        loadPlantMaterialHistory(inputValue, 1);
        const select = document.getElementById("selecttype");
        select.style.display = "none";

        document.getElementById("titlereport").innerHTML = "Số liệu hoạt động sử dụng phân bón";

    };
    document.getElementById("thuoc").onclick = function () {
        $('.viessss').remove();
        var inputValue = document.getElementById('id3').value;
        loadPlantMaterialHistory(inputValue, 2);
        const select = document.getElementById("selecttype");
        select.style.display = "none";

        document.getElementById("titlereport").innerHTML = "Số liệu hoạt động sử dụng các loại thuốc";

    };
    document.getElementById("qua").onclick = function () {
        $('.viessss').remove();
        var inputValue = document.getElementById('id3').value;
        loadPlantMaterialHistory(inputValue, 3);
        const select = document.getElementById("selecttype");
        select.style.display = "none";

        document.getElementById("titlereport").innerHTML = "Số liệu hoạt động thu hoạch";

    };
    document.getElementById("nhat").onclick = function () {
        $('.viessss').remove();
        var inputValue = document.getElementById('id3').value;
        loadPlantMaterialHistory(inputValue, 4);
        const select = document.getElementById("selecttype");
        select.style.display = "block";

        document.getElementById("titlereport").innerHTML = "Số liệu hoạt động chăm sóc cây theo ngày";

    };
    $(".update-option").on("click", function (e) {
        window.location = "/plant#popup2";
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


            }
        });

    });
});