$(function () {
    $(".setAnswer").click(function () {
        //获取问答回复帖子的quesAnswId和回复者的userId
        var quesAnswId = $(".getQuesAnswId").val();
        var userId = $(this).val();
        $.ajax({
            url: '/Home/adoptAnswer',
            type: 'POST',
            data: { 'quesAnswId': quesAnswId, 'userId': userId },
            success: function (data) {
                if (data == "true") {
                    alert('采纳成功！');
                    //刷新当前页面
                    window.location.reload();
                }
                else {
                    alert('采纳失败');
                }
            }
        })
    })
})