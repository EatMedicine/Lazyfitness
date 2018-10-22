
$(function () {
    $("img#imgEnter").mousedown(function () {
        $("input#userPwd")[0].type = "text";
    })
    $("img#imgEnter").mouseup(function () {
        $("input#userPwd")[0].type = "password";
        $("input#userPwd")[0].focus();
    })
});
