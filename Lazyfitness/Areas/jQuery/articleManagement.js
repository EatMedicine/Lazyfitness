//删除文章的jQuery函数
$(function () {
    $(".deleteArticle").click(function () {
        //获取所点击删除按钮的value值
        var getArticleId = $(this).val();
        //把这个值发送到后台       
        $.post("/backStage/articleManagement/deleteArticle", "resourceId=" + getArticleId, function (data, status) {
            if (data == "success" && status == "success") {
                alert("删除成功!");
                //刷新当前页面
                window.location.reload();
            }            
        });
    })
})
