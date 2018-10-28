//删除文章的jQuery函数
$(function () {
    $(".deleteArticle").click(function () {
        //获取所点击删除按钮的value值
        var getArticleId = $(this).val();
        alert(getArticleId);
        //把这个值发送到后台       
        $.post("/backStage/articleManagement/deleteArticle", "resourceId=" + getArticleId, function (data, status) {
            if (data == "success" && status == "success") {
                alert("删除成功!");
            }
            //刷新当前页面
            window.location.reload();
        });
    })
})

//修改文章信息的jQuery函数
//$(function () {
//    $(".changeArticle").click(function () {
//        //获取所点击修改按钮的value值
//        var getArticleId = $(this).val();
//        //把这个值发送到后台
//        $.get("/backStage/articleManagement/changeArticle", "resourceId=" + getArticleId, function (data, status) {
//            if (data == "success" && status == "success") {
//            }
//            })
//    })
//})