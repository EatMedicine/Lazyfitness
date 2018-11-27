//删除用户的jQuery函数
$(function () {
    $(".delete-data").click(function () {
        //获取所点击删除按钮的value值
        var getuserId = $(this).val();
        //把这个值发送到后台       
        $.post("/backStage/userManagement/delete", "userId=" + getuserId, function (data, status) {
            if (data == "success" && status == "success") {
                alert("删除成功!");
                //刷新当前页面
                window.location.reload();
            }
        });
    })
})