$(function () {
    $("#resetUnAvailable").click(function () {
        $.post("/backStage/payManagement/resetUnAvailable", function (data, status) {
            if (data == "success" && status == "success") {
                alert("操作成功");
                //刷新当前页面
                window.location.reload();
            }
        })
    })
})