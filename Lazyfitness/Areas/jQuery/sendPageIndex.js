

$(function () {
    $(".id").click(function () {
        var id = $(this).val();
        alert(id);
        $.ajax({
            url: '/backStage/userManagement/Index',
            type: 'POST',
            data: { id: id },
            success: function () { alert("1"); /*window.location.reload(); */}
        })
    })
})