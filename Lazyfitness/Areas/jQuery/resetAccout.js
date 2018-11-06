$(function () {
    $(".resetAccount").click(function () {
        //获取此按钮的val值
        var getUserId = $(this).val();
        //发送post请求到处理类
        $.post("/backStage/payManagement/resetAccount", "userId=" + getUserId, function (data, status) {
            if (data == "success" && status == "success") {
                alert("清零成功");
                //刷新当前页面
                window.location.reload();
            }
        })
    })
})