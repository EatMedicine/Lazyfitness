var pageNum = 0;

$(function () {
    $("body").ready(function () {
        loadContent();
    });
});


$(function () {
    $(window).scroll(function () {
        var $this = $(this),
            viewH = $(this).height(),//可见高度
            contentH = $(document).outerHeight(),//内容高度
            scrollTop = $(this).scrollTop();//滚动高度
        if (scrollTop > 600) {
            $("#top-btn").removeClass("unvis").addClass("vis");

        }
        else {
            $("#top-btn").removeClass("vis").addClass("unvis");
        }
        if (scrollTop / (contentH - viewH) >= 0.99) {
            loadContent();

        }
    });
});

function loadContent() {
    pageNum++;
    $.ajax({
        url: "/Home/QuesItemContent?pageNum=" + pageNum,
        type: "get",
        dataType: "json",
        success: function (data) {
            for (var count = 0; count < 5; count++) {
                $(".ques-list").append(
                    '<div class="ques-list-item" >' +
                    '<div class="item-con">' +
                    '<div class="item-title">' +
                    '<h3><a href="#">' + data[count].Title + '</a></h3>' +
                    '</div>' +
                    '<div class="item-introduction">' +
                    '<p>' + data[count].Introduction + '</p>' +
                    '</div>' +
                    '<div class="item-user">' +
                    '<div class="user-pic fl">' +
                    '<img src="' + data[count].HeadAdr + '" />' +
                    '</div>' +
                    '<div class="user-name fl">' +
                    '<p>' + data[count].Name + '</p>' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '</div >');
            }


        }

    });
}