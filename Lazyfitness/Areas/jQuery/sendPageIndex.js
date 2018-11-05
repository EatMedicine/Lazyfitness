

$(function () {
    $(".id").click(function () {
        var id = $(this).val();
        alert(id);
        //$.get("/backStage/userManagement/Index", "pageIndex = " + pageIndex, function (success) {
        //    if (success == "success") {
        //        //刷新当前页面
        //        window.location.reload();
        //    }
        //})
        $.ajax({
            url: '/backStage/userManagement/Index',
            type: 'POST',
            data: { id: id },
            success: function () { alert("1"); /*window.location.reload(); */}
        })
    })
})