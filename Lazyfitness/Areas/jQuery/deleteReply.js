$(function () {
    $(".deleteReply").click(function () {
        var getId = $(this).val();
        $.ajax({
            url: "/backStage/commentManagement/delete",
            type: 'POST',
            data: { id: getId },
            success: function (data) {
                if (data == 'T') {
                    alert("删除成功");
                    //刷新当前页面
                    window.location.reload();
                }
                if (data == "F") {
                    alert("删除失败");
                }
                if (data == "NF") {
                    alert("删除出错");               
                }
            }
        })
    })
})