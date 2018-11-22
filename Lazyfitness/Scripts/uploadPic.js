// 保存图片
function saveStudent() {
    $("#student_sfm").form("submit", {
        url: "/Teacher/Teacher/SaveImage", // 上传图片链接
        success: function (result) {
            if (result == "success") {
                $.messager.alert("系统提示", "保存成功！");
                resetForm();
            } else if (result == "error") {
                $.messager.alert("系统提示", "保存失败！");
                return;
            }
        }
    })
}

// 清空表单数据
function resetForm() {
    $("#student_sfm").form('clear');
}