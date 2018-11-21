var count = 1;
var eventNum = 0;

var interval = 3000;
window.onload = function () {
    eventNum = setInterval(setMargin, interval);
}


function setMargin() {
    var obj = document.getElementById("panel");
    if (count > 2)
        count = 0;
    var pos = "-" + (count * 300).toString() + "px";
    switch (count) {
        case 0: $("#panel").animate({ marginTop: "0px" }); break;
        case 1: $("#panel").animate({ marginTop: "-300px" }); break;
        case 2: $("#panel").animate({marginTop: "-600px"}); break;
    }
    //obj.style.marginTop = "-" + (count * 300).toString() + "px";
    obj = document.getElementById("dot");
    for (var i = 0; i <= 2; i++) {
        obj.children[i].children[0].src = "/Resource/picture/dot2.png";
        document.getElementById("scroll-title-" + i).className = "scroll-title-item unvis";
    }
    document.getElementById("scroll-title-" + count).className = "scroll-title-item vis";
    obj.children[count].children[0].src = "/Resource/picture/dot1.png";
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