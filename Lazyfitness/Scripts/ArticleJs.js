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
    obj = document.getElementById("dot");
    pobj = document.getElementById("picture");
    for (var i = 0; i <= 2; i++) {
        obj.children[i].children[0].src = "/Resource/picture/square0.png";
        pobj.children[i].className = "unvis";
        document.getElementById("scroll-title-" + i).className = "scroll-title-item unvis";
    }
    pobj.children[count].className = "vis";
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