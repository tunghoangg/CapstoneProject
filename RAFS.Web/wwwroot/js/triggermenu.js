$(document).ready(function () {
    $('.menu-trigger').click(function () {
        var optionsList = $(this).siblings('.options-list');
        optionsList.toggleClass('visible');
    });

    $(document).click(function (event) {
        if (!$(event.target).closest('.options-menu').length) {
            $('.options-list').removeClass('visible');
        }
    });
});