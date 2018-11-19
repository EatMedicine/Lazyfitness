function checkCode() {
    //获得验证码框的验证码
    var code = $("#code").val();
    //获得邮箱地址
    var emailAddress = $(".emailAddress").val();
    if (code == null || code == '' || emailAddress == null || emailAddress == '') {
        alert("验证码或邮箱不能为空");
        return false;
    }
    else {
        var result = true;
        $.ajax({
            url: '/account/sendEmailCodeRegister/isRightCode',
            type: 'POST',
            data: { "emailAddress": emailAddress, "code": code },
            async: false,
            success: function (data) {
                if (data == "CF") {
                    alert("验证码错误！");
                    result = false;
                }
                if (data == "F") {
                    alert("请获得验证码");
                    result = false;
                }
            },
        });
        return result;
    }
}