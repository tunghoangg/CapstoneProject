$(document).ready(function () {

    var searchName = "";
    var selectValue = 0;
    var odByNum = 0;
    var page = 1;
    var totalPages = 0;
    var milestoneId = checkParameter()
    $("table").on("click", "tr", function (e) {

        var resId = e.target.closest("tr").id;
        resId = resId.slice(9);

        resId = resId.replace(/\D/g, '');
        resId = parseInt(resId);
        var checkload = false;
        $.ajax({
            url: baseUrl + "Plant/GetMilestoneById?id=" + resId,
            type: "get",
            contentType: "application/json",
            success: function (result, status, xhr) {

                $("#name").val(result["name"]);
                $("#id").val(result["id"]);
                $("#description").val(result["description"]);
                checkload = true;
                if (checkload) {
                    window.location = "/milestone#popup2";
                }
                

            }
        });


    });
    $('#sortBtn').click(function () {
        searchName = document.getElementById("searchName").value;
        selectValue = document.getElementById("filterForm").value;
        
        fetchForms(page);
    });
    var fetchForms = (page) => {

        var sort = $('#filterForm').val();
        var apiurl = "";
        apiurl = baseUrl + `Plant/GetListAllFarmMilestones?farmid=${farmid}&page=${page}`;

        if (searchName.trim().length > 0 && selectValue == 0) {
            apiurl = baseUrl + `Plant/GetSearchMilestone?farmid=${farmid}&search=${searchName}`;
        } else if (searchName.trim().length > 0 && selectValue == 1) {
            apiurl = baseUrl + `Plant/GetSearchDateMilestone?farmid=${farmid}&search=${searchName}`;
        } else if (searchName.trim().length > 0 && selectValue == 2) {
            apiurl = baseUrl + `Plant/GetSearchNumMilestone?farmid=${farmid}&search=${searchName}`;
        } else if (searchName.trim().length > 0) {
            apiurl = baseUrl + `Plant/GetSearchMilestone?farmid=${farmid}&search=${searchName}`;
        } else if (selectValue == 0) {
            apiurl = baseUrl + `Plant/GetListAllFarmMilestones?farmid=${farmid}&page=${page}`;
        } else if (selectValue == 1) {
            apiurl = baseUrl + `Plant/GetMilestoneOdByLastUpdate?farmid=${farmid}`;
        } else if (selectValue == 2) {
            apiurl = baseUrl + `Plant/GetMilestoneOdByNumberOfPlant?farmid=${farmid}`;
        }
        $.ajax({
            url: apiurl,
            method: 'GET',
            data: {
                sort: sort
            },
            contentType: 'application/json',
            success: (data) => {
                $('#content-form').html('');
                if (data && data.length > 0) {
                    $('#content-form').html(generateHtmlForm(data));
                    data.forEach(element => {
                        totalPages = element.totalpage;
                    });
                    updatePaginationButtons(page);
                } else {
                    $('#content-form').html('<h2>Chưa có nhóm nào được tạo trong trang trại này</h2>');
                    $('#pagi-pages').html('');
                }
            },
            error: function () {
                console.error('Failed to fetch data.');
            }
        });
    };
    // Initial fetch
    fetchForms(page);
    const updatePaginationButtons = (currentPage) => {
        $('#pagi-pages').html('');
        $('#pagi-pages').html('');
        if (totalPages > 1) {
            if (currentPage > 1) {
                $('#pagi-pages').append(`<li class="page-item"><a class="page-link" id="prevPage" href="#" aria-label="Previous"><span aria-hidden="true"></span>Trước</a></li>`);
            }
            let startPage = 1;
            let endPage = totalPages;
            if (totalPages > 3) {
                startPage = Math.max(1, currentPage - 1);
                endPage = Math.min(totalPages, currentPage + 1);
                if (startPage === 1) {
                    endPage = 3;
                }
                if (endPage === totalPages) {
                    startPage = totalPages - 2;
                }
            }
            for (let i = startPage; i <= endPage; i++) {
                $('#pagi-pages').append(`<li class="page-item"><a class="page-link" id="pagi-pages${i}" href="#">${i}</a></li>`);
            }
            if (currentPage < totalPages) {
                $('#pagi-pages').append(`<li class="page-item"><a class="page-link" id="nextPage" href="#" aria-label="Next"><span aria-hidden="true"></span>Tiếp</a></li>`);
            }
        }
        $(`#pagi-pages${currentPage}`).addClass('active');


    };
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
    $('#pagi-pages').on('click', 'a.page-link', function () {
        if ($(this).attr('id') === 'prevPage') {
            if (page > 1) {
                page--;
            }
        } else if ($(this).attr('id') === 'nextPage') {
            if (page < totalPages) {
                page++;
            }
        } else {
            page = parseInt($(this).text());
        }
        fetchForms(page);
    });
    // Function to fetch farms based on page number


    // Event delegation for pagination click
    document.getElementById("add").onclick = function () { Create() };
    document.getElementById("update").onclick = function () { Update() };

    $("table").on("click", ".deleteffff", function (event) {

        event.stopPropagation();
        var resId = event.target.closest("tr").id;
        resId = resId.slice(9);

        resId = resId.replace(/\D/g, '');
        resId = parseInt(resId);
        var rows = "";
        var count = 0;
        $("#myModal").modal();
        $.ajax({
            url: baseUrl + `Plant/GetMilestonesPlantList?milestone=${resId}`,
            method: 'GET',
            contentType: 'application/json',
            success: (data) => {
                $.each(data, function (i, item) {
                    rows += ` <tr  >
                                    <td>
                                                ${item.name}
                                    </td>
                                    <td>
                                                         ${item.plantingMethod}
                                    </td>
                                    <td>
                                                         <img src=" ${item.image}" alt="loading faild"/>
                                            </td>
                                </tr>`;

                    count++;

                });
                if (count == 0) {
                    $('.modal-body thead').empty();

                    rows += ` <tr class="milestone" id=milestoneofplant'${resId}' >
                                            <td>
                                            Hiện đang không có cây nào thuộc nhóm này
                                            </td>
                                        </tr>`;
                } else {
                    rows += ` <tr class="milestone" id=milestoneofplant'${resId}' >
                                            <td>

                                            </td>
                                            <td>

                                            </td>
                                            <td>

                                                    </td>
                                        </tr>`;
                }



                $('#plantlist').empty().append(rows);
            },
            error: function () {
                console.error('Failed to fetch data.');
            }
        });

    });
   
    if (milestoneId != 0) {

        console.log(baseUrl + "Plant/GetMilestoneById?id=" + milestoneId);
        $.ajax({
            url: baseUrl + "Plant/GetMilestoneById?id=" + milestoneId,
            type: "get",
            contentType: "application/json",
            success: function (result, status, xhr) {
                console.log(baseUrl + "Plant/GetMilestoneById?id=" + milestoneId);
                $("#name").val(result["name"]);
                $("#id").val(result["id"]);
                $("#description").val(result["description"]);
                pauseExecution(500);
                window.location = "/milestone?milestoneid=" + milestoneId + "#popup2";
            }
        });

    } else {
        window.location = "/milestone#";
    }
});