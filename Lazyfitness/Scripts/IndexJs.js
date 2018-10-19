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
    obj.style.marginTop = "-" + (count * 300).toString() + "px";
    obj = document.getElementById("dot");
    for (var i = 0; i <= 2; i++) {
        obj.children[i].children[0].src = "/Resource/picture/dot2.png";
    }

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