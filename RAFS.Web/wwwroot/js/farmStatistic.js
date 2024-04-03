
$(document).ready(function () {
    GetFiveYearLast();
    SelectFarmStatistic();
    var selectedFarmId = $("#farmStatisticSelect").val();
    var selectedYear = $("#yearSelect").val();

    $("#farmStatisticSelect").change(function () {
        selectedFarmId = $(this).val();
        //console.log("Selected year:", selectedYear);
        //console.log("Selected farm ID:", selectedFarmId);
    });

    $("#yearSelect").change(function () {
        selectedYear = $(this).val();
        //console.log("Selected year:", selectedYear);
        //console.log("Selected farm ID:", selectedFarmId);
    });



    DisplayCashChart();
    DisplayCashIn();
    DisplayCashOut();
    DisplayMaterialChart();
});

function TotalStatistic(farmIdSelected, yearSelected) {

};

function SelectFarmStatistic() {
    var dataFarmLength = 0;
    var selectFarm = document.getElementById("farmStatisticSelect");
    $.ajax({
        url: baseUrl + `Statistics/ListFarmStatistic/${userId}`,
        method: 'GET',
        contentType: 'application/json',
        success: (data) => {
            $('#farm-list').html('');
            dataFarmLength = data.length;
            
            if (dataFarmLength > 0) {
                data.forEach(farm => {
                    var option = document.createElement("option");
                    option.value = farm.id; 
                    option.text = farm.code; 
                    selectFarm.add(option);
                });
               
            }

        },
        complete: (data) => {
        },
        error: (data) => {

        }
    });
};

function GetFiveYearLast() {
    // Lấy ra select box
    var selectBox = document.getElementById("yearSelect");

    // Lấy ra năm hiện tại
    var currentYear = new Date().getFullYear();

    // Đặt giá trị cho option là 5 năm gần nhất
    for (var i = 0; i < 5; i++) {
        var option = document.createElement("option");
        option.text = currentYear - i;
        option.value = currentYear - i;
        if (i === 0) {
            option.selected = true; // Đặt mặc định chọn option là năm hiện tại
        }
        selectBox.add(option);
    }
};

function DisplayMaterialChart() { 
    if ($("#materials-chart").length) {
        var areaData = {
            labels: ["Jan", "Feb", "Mar"],
            datasets: [{
                data: [0, 0, 0],
                backgroundColor: [
                    "#4B49AC", "#FFC100", "#248AFD",
                ],
                borderColor: "rgba(0,0,0,0)"
            }
            ]
        };
        var areaOptions = {
            responsive: true,
            maintainAspectRatio: true,
            segmentShowStroke: false,
            cutoutPercentage: 78,
            elements: {
                arc: {
                    borderWidth: 4
                }
            },
            legend: {
                display: false
            },
            tooltips: {
                enabled: true
            },
            legendCallback: function (chart) {
                var text = [];
                text.push('<div class="report-chart">');
                text.push('<div class="d-flex justify-content-between mx-4 mx-xl-5 mt-3"><div class="d-flex align-items-center"><div class="mr-3" style="width:20px; height:20px; border-radius: 50%; background-color: ' + chart.data.datasets[0].backgroundColor[0] + '"></div><p class="mb-0">Offline sales</p></div>');
                text.push('<p class="mb-0">88333</p>');
                text.push('</div>');
                text.push('<div class="d-flex justify-content-between mx-4 mx-xl-5 mt-3"><div class="d-flex align-items-center"><div class="mr-3" style="width:20px; height:20px; border-radius: 50%; background-color: ' + chart.data.datasets[0].backgroundColor[1] + '"></div><p class="mb-0">Online sales</p></div>');
                text.push('<p class="mb-0">66093</p>');
                text.push('</div>');
                text.push('<div class="d-flex justify-content-between mx-4 mx-xl-5 mt-3"><div class="d-flex align-items-center"><div class="mr-3" style="width:20px; height:20px; border-radius: 50%; background-color: ' + chart.data.datasets[0].backgroundColor[2] + '"></div><p class="mb-0">Returns</p></div>');
                text.push('<p class="mb-0">39836</p>');
                text.push('</div>');
                text.push('</div>');
                return text.join("");
            },
        }
        var materialChartPlugins = {
            beforeDraw: function (chart) {
                var width = chart.chart.width,
                    height = chart.chart.height,
                    ctx = chart.chart.ctx;

                ctx.restore();
                var fontSize = 3.125;
                ctx.font = "500 " + fontSize + "em sans-serif";
                ctx.textBaseline = "middle";
                ctx.fillStyle = "#13381B";

                var text = "95",
                    textX = Math.round((width - ctx.measureText(text).width) / 2),
                    textY = height / 2;

                ctx.fillText(text, textX, textY);
                ctx.save();
            }
        }
        var materialChartCanvas = $("#materials-chart").get(0).getContext("2d");
        var materialChart = new Chart(materialChartCanvas, {
            type: 'doughnut',
            data: areaData,
            options: areaOptions,
            plugins: materialChartPlugins
        });
        //doughnut
        document.getElementById('materials-legend').innerHTML = materialChart.generateLegend();
    }
};

function DisplayCashIn() {

    var doughnutPieData = {
        datasets: [{
            data: [30, 40, 30, 500, 400],
            backgroundColor: [
                'rgba(255, 99, 132, 0.5)',
                'rgba(54, 162, 235, 0.5)',
                'rgba(255, 206, 86, 0.5)',
                'rgba(75, 192, 192, 0.5)',
                'rgba(153, 102, 255, 0.5)',
                'rgba(255, 159, 64, 0.5)'
            ],
            borderColor: [
                'rgba(255,99,132,1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)',
                'rgba(153, 102, 255, 1)',
                'rgba(255, 159, 64, 1)'
            ],
        }],

        // These labels appear in the legend and in the tooltips when hovering different arcs
        labels: [
            'Sản phẩm',
            'Thu hồi tài sản',
            'Đầu tư',
            'Sản phẩm sinh học',
            'Thu khác',
        ]
    };

    var doughnutPieOptions = {
        responsive: true,
        animation: {
            animateScale: true,
            animateRotate: true
        }
    };

    if ($("#doughnutChart-cash-in").length) {
        var doughnutChartCanvasIn = $("#doughnutChart-cash-in").get(0).getContext("2d");
        var doughnutChartIn = new Chart(doughnutChartCanvasIn, {
            type: 'doughnut',
            data: doughnutPieData,
            options: doughnutPieOptions
        });
    }

    
};

function DisplayCashOut() {

    var doughnutPieData = {
        datasets: [{
            data: [0, 0, 0, 0, 0, 0, 0, 0],
            backgroundColor: [
                'rgba(255, 99, 132, 0.5)',
                'rgba(54, 162, 235, 0.5)',
                'rgba(255, 206, 86, 0.5)',
                'rgba(75, 192, 192, 0.5)',
                'rgba(153, 102, 255, 0.5)',
                'rgba(255, 159, 64, 0.5)'
            ],
            borderColor: [
                'rgba(255,99,132,1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)',
                'rgba(153, 102, 255, 1)',
                'rgba(255, 159, 64, 1)'
            ],
        }],

        // These labels appear in the legend and in the tooltips when hovering different arcs
        labels: [
            'Thuốc bảo vệ thực vật',
            'Phân hóa học',
            'Máy móc',
            'Công cụ',
            'Bán hàng',
            'Nhân công',
            'Đầu tư',
            'Phát sinh khác',
        ]
    };

    var doughnutPieOptions = {
        responsive: true,
        animation: {
            animateScale: true,
            animateRotate: true
        }
    };

    if ($("#doughnutChart-cash-out").length) {
        var doughnutChartCanvasOut = $("#doughnutChart-cash-out").get(0).getContext("2d");
        var doughnutChartOut = new Chart(doughnutChartCanvasOut, {
            type: 'doughnut',
            data: doughnutPieData,
            options: doughnutPieOptions
        });
    }
};



function DisplayCashChart() {
    if ($("#cash-flow-chart").length) {
        var CashChartCanvas = $("#cash-flow-chart").get(0).getContext("2d");
        var CashChart = new Chart(CashChartCanvas, {
            type: 'bar',
            data: {
                labels: ["Quý 1", "Quý 2", "Quý 3", "Quý 4"],
                datasets: [{
                    label: 'Tổng thu',
                    data: [0, 0, 0, 0],
                    backgroundColor: '#98BDFF'
                },
                {
                    label: 'Tổng chi',
                    data: [0, 0, 0, 0],
                    backgroundColor: '#4B49AC'
                }
                ]
            },
            options: {
                cornerRadius: 5,
                responsive: true,
                maintainAspectRatio: true,
                layout: {
                    padding: {
                        left: 0,
                        right: 0,
                        top: 20,
                        bottom: 0
                    }
                },
                scales: {
                    yAxes: [{
                        display: true,
                        gridLines: {
                            display: true,
                            drawBorder: false,
                            color: "#F2F2F2"
                        },
                        ticks: {
                            display: true,
                            min: 0,
                            max: 1000,
                            callback: function (value, index, values) {
                                return value + ' triệu đồng';
                            },
                            autoSkip: true,
                            maxTicksLimit: 10,
                            fontColor: "#6C7383"
                        }
                    }],
                    xAxes: [{
                        stacked: false,
                        ticks: {
                            beginAtZero: true,
                            fontColor: "#6C7383"
                        },
                        gridLines: {
                            color: "rgba(0, 0, 0, 0)",
                            display: false
                        },
                        barPercentage: 1
                    }]
                },
                legend: {
                    display: false
                },
                elements: {
                    point: {
                        radius: 0
                    }
                }
            },
        });
        document.getElementById('cash-flow-legend').innerHTML = CashChart.generateLegend();
    }
};

