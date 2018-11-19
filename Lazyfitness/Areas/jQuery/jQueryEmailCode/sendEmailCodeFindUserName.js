$(function () {
    $(".getEmailCode").click(function () {
        //获得收件人邮箱
        var mailAddress = $(".emailAddress").val();
        //判断邮箱格式是否正确
        var reg = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;;
        if (!reg.test(mailAddress)) {
            alert("请输入正确的邮箱格式");
            return false;
        }
        //更改按钮内容
        $(".getEmailCode").attr("value", "发送请求中");
        //把按钮设置为disabled状态
        $(".getEmailCode").attr("disabled", "disabled");
        $.post("/account/sendEmailCodeFindUsername/sendVerification", "mailAddress=" + mailAddress, function (data, status) {
            if (data == "true" && status == "success") {
                var countdown = 60;
                function settime(obj) {
                    if (countdown === 0) {
                        //移除按钮的disabled状态
                        $(".getEmailCode").removeAttr("disabled");
                        obj.attr("value", "获取验证码");
                        countdown = 60;
                        return;
                    }
                    else {
                        obj.attr("value", "重发(" + countdown + "s)");
                        countdown--;
                    }
                    setTimeout(function () { settime(obj) }, 1000)
                }
                settime($(".getEmailCode"));
            }
            else {
                alert("发送失败");
                $(".getEmailCode").attr("value", "再次获取");
                $(".getEmailCode").removeAttr("disabled");
            }
        });
    })
})

