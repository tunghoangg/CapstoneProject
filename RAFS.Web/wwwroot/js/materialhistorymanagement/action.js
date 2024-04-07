$(document).ready(function () {
    Manager.GetAllItems(0);

    var typeid = checkParameterType();
    var plantid = checkParameter();
    const elements = document.querySelectorAll('.backbtn');

    // Loop through each element
    elements.forEach(element => {
        element.setAttribute('href', '/plant?plantid=' + plantid + "#" );
    });
    if (typeid != 0) {
        if (typeid == 1) {
            document.getElementById("profile").checked = true;

            document.getElementById("posts").checked = false;
            // Check the settings id
            document.getElementById("settings").checked = false;
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
    $("table").on("click", ".deleteffff", function (e) {
        var resId = $(this).attr("id");
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
            url: baseUrl + "PlantMaterialHistory/DeletePlantMaterialRecord?milestoneId=" + resId,
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

                window.location = "/materialhistory?plantid=" + checkParameter() + "&type=" + checkParameterType() + "#";


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
    document.getElementById("add").onclick = function () { Create() };
    document.getElementById("update").onclick = function () { Update() };
    
    document.getElementById("createbtn").onclick = function () {
        $("#PlantId").val(checkParameter());

        Manager.GetAllItems(1);


        
    };
    document.getElementById("createbtn2").onclick = function () {
        $("#PlantId").val(checkParameter());
        Manager.GetAllItems(2);
        
    };
    document.getElementById("createbtn3").onclick = function () {
        $("#PlantId").val(checkParameter());
        Manager.GetAllItems(3);
      
    };

})
function checkParameterType() {
    // Get the URL parameters
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);

    // Get the value of the parameter you're interested in
    const paramValue = urlParams.get('type');

    // Check if the parameter has a value
    if (paramValue !== null && paramValue !== '') {
        // Return the parameter value
        return String(paramValue)[0];

    } else {
        return '0';
    }
}