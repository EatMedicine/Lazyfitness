var count = 1;
var eventNum = 0;
var pageNum = 0;
var interval = 3000;
window.onload = function () {
    eventNum = setInterval(setMargin, interval);
};


function setMargin() {
    var obj = document.getElementById("panel");
    if (count > 2)
        count = 0;
    obj = document.getElementById("dot");
    pobj = document.getElementById("picture");
    for (var i = 0; i <= 2; i++) {
        obj.children[i].children[0].src = "/Resource/picture/square0.png";
        //pobj.children[i].children[0].className = "unvis";
        $("#pic1").fadeOut(500);
        $("#pic2").fadeOut(500);
        $("#pic3").fadeOut(500);
        document.getElementById("scroll-title-" + i).className = "scroll-title-item unvis";
    }
    //pobj.children[count].children[0].className = "vis";
    if (count === 0) {
        $("#pic1").fadeIn(500);
    }
    else if (count === 1) {
        $("#pic2").fadeIn(500);
    }
    else if (count === 2) {
        $("#pic3").fadeIn(500);
    }
    
    document.getElementById("scroll-title-" + count).className = "scroll-title-item vis";
    obj.children[count].children[0].src = "/Resource/picture/square1.png";
    count++;
}

function clickDot(num) {
    count = num - 1;
    setMargin();
}

function cancelScroll() {
    clearInterval(eventNum);
}

function startScroll() {
    eventNum = setInterval(setMargin, interval);
}


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
        var sidebar = document.getElementById("sidebar");
        if (scrollTop > 600) {
            $("#top-btn").removeClass("unvis").addClass("vis");
            sidebar.style.position = "fixed";

        }
        else {
            $("#top-btn").removeClass("vis").addClass("unvis");
            sidebar.style.position = "absolute";
        }

        if (scrollTop / (contentH - viewH) >= 0.99) {
            loadContent();

        }  
    });
});

function loadContent() {
    pageNum++;
    $.ajax({
        url: "/Home/ArticleItemContent?pageNum=" + pageNum,
        type: "get",
        dataType: "json",
        success: function (data) {
            for (var count = 0; count < 5; count++) {
                $(".article-list").append(
                    '<div class="article-list-item" >' +
                    '<div class="item-con">' +
                    '<div class="item-title">' +
                    '<h3><a href="'+data[count].Url+'">' + data[count].Title + '</a></h3>' +
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