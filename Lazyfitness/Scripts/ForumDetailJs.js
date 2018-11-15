$(function () {
    $(window).scroll(function () {
        var $this = $(this),
        scrollTop = $(this).scrollTop();//滚动高度
        $("#reply-btn").css({ "top": scrollTop });
    });
});