$(document).ready(function () {
   
    var typeid = checkParameterType();
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
        $.ajax({
            url: baseUrl + "PlantMaterialHistory/DeletePlantMaterialRecord?milestoneId=" + resId,
            type: "delete",
            contentType: "application/json",
            success: function (result, status, xhr) {
                $(this).remove();
                alert("delete successful");
                if (checkParameter() != 0) {


                    window.location = "/materialhistory?plantid=" + checkParameter() + "&type=" + typeid + "#";
                } else {

                    window.location = "/materialhistory#";
                }
                Manager.GetAllProduct();
            },
            error: function (xhr, status, error) {
                // Handle error
                alert("delete fail");

            }

        });
    });

    document.getElementById("add").onclick = function () { Create() };
    document.getElementById("update").onclick = function () { Update() };
    document.getElementById("createbtn").onclick = function () {
        Manager.GetAllItems();
        setTimeout(function () {
            var select = document.getElementById("InventoryId");
            Manager.HideOption(1);
        }, 300);
    };
    document.getElementById("createbtn2").onclick = function () {
        Manager.GetAllItems();
        setTimeout(function () {
            var select = document.getElementById("InventoryId");
            Manager.HideOption(2);
        }, 300);
    };
    document.getElementById("createbtn3").onclick = function () {
        Manager.GetAllItems();
        setTimeout(function () {
            var select = document.getElementById("InventoryId");
            Manager.HideOption(3);
        }, 300);
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
        return paramValue;
        // Do something with the parameter value, e.g., validate, process, etc.
    } else {
        return 0;
    }
}