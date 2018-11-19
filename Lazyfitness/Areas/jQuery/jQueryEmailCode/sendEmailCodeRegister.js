$(function () {
    $(".getEmailCode").click(function () {

        if (checkEmail() == false) {
            alert("邮箱已经注册！");
            return false;
        }

        //获得收件人邮箱
        var mailAddress = $(".emailAddress").val();
        //判断邮箱格式是否正确
        var reg = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;;
        if (!reg.test(mailAddress)) {
            alert("请输入正确的邮箱格式");
            return false;
　　    }       
        $.post("/account/sendEmailCodeRegister/sendVerification", "mailAddress=" + mailAddress, function (data, status) {
            if (data == "true" && status == "success") {
                alert("发送成功!");
            }
            else {
                alert("发送失败");
            }
        
        });
    })
})

function checkEmail() {
    //得到邮箱
    var emailAddress = $(".emailAddress").val();
    //发送post请求
    var result = true;
    $.ajax({
        url: '/account/sendEmailCodeRegister/checkEmail',
        type: 'POST',
        data: { "emailAddress": emailAddress},
        async: false,
        success: function (data) {
            if (data == "EF") {
                result = false;
            }          
        },
    });    
    return result;
}