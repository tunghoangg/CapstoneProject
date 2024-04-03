document.addEventListener("DOMContentLoaded", function () {
    // Your code here


    $(".fold-table tr.view").on("click", function () {
        if ($(this).hasClass("open")) {
            $(this).removeClass("open").next(".fold").removeClass("open");
        } else {
            $(".fold-table tr.view").removeClass("open").next(".fold").removeClass("open");
            $(this).addClass("open").next(".fold").addClass("open");
        }
    });
    $('#Open').on("click", function () {
        $('#popup3').addClass("appear");
    });
    $('#close').on("click", function () {
        $('#popup3').removeClass("appear");
    });

    var buttons = document.getElementsByClassName("myButton");
    var holdTimers = {};
    var startColor = [255, 255, 255]; // Màu ban đầu (trắng)
    var endColor = [0, 128, 0]; // Màu kết thúc (xanh lá cây)


    function handleMouseDown(button) {
        var currentTime = Date.now();
        holdTimers[button.id] = setInterval(function () {
            var elapsedTime = Date.now() - currentTime;
            if (elapsedTime >= 3000) {
                clearInterval(holdTimers[button.id]); // Dừng hẹn giờ nếu đạt 3 giây
                alert("hold"); // Hiển thị thông báo "hold"
                button.style.backgroundColor = "green"; // Đặt màu thành xanh
            } else {
                var ratio = elapsedTime / 3000; // Tính tỉ lệ thời gian đã qua
                var currentColor = [
                    Math.round(startColor[0] + (endColor[0] - startColor[0]) * ratio),
                    Math.round(startColor[1] + (endColor[1] - startColor[1]) * ratio),
                    Math.round(startColor[2] + (endColor[2] - startColor[2]) * ratio)
                ];
                button.style.backgroundColor = 'rgb(' + currentColor.join(',') + ')';
            }
        }, 50); // Update màu mỗi 50ms
    }

    function handleMouseUp(button) {
        clearInterval(holdTimers[button.id]); // Dừng hẹn giờ
        if (button.style.backgroundColor === "green") {
            alert("clicked");
            button.style.backgroundColor = ""; // Revert color
        } else {
            alert("clicked");
            button.style.backgroundColor = ""; // Revert color
        }
    }

    for (var i = 0; i < buttons.length; i++) {
        buttons[i].addEventListener("mousedown", function (event) {
            handleMouseDown(event.target);
        });

        buttons[i].addEventListener("mouseup", function (event) {
            handleMouseUp(event.target);
        });
    }
});