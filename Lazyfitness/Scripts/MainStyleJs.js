var IsOpen = false;
$(document).ready(function () {
    $("#userHeadPic").click(function () {
        if (IsOpen == true) {
            $('#user-info').fadeOut(500);
            //$('#user-info').css("visibility", "hidden");
            IsOpen = false;
        }
        else {
            $('#user-info').fadeIn(500);
            //$('#user-info').css("visibility", "visible");
            IsOpen = true;
        }
    });
});