function changeInput() {
    document.getElementById("button").hidden = true;
    document.getElementById("postData").hidden = false;
    document.getElementById("quit").hidden = false;
    var userName = document.getElementById("userName");
    var userName_new = userName.innerHTML;
    userName.innerHTML = "<input type='text' id='userName' name='userName' class='input' value='" + userName_new + "'/>";

    var userSex = document.getElementById("userSex");
    var userSex_new = userSex.innerHTML;
    if (userSex_new == '男') {
        userSex.innerHTML = "<input type='radio' id='userSex' name='userSex' class='input' value='0' checked='checked'/>男<input type='radio' id='userSex' name='userSex' class='input' value='1' />女";
    }
    if (userSex_new == '女') {
        userSex.innerHTML = "<input type='radio' id='userSex' name='userSex' class='input' value='0'/>男<input type='radio' name='userSex' id='userSex' class='input' value='1' checked='checked'/>女";
    }

    var userEmail = document.getElementById("userEmail");
    var userEmail_new = userEmail.innerHTML;
    userEmail.innerHTML = "<input type='text' id='userEmail' name='userEmail' class='input' value='" + userEmail_new + "'/>";


    var userAge = document.getElementById("userAge");
    var userAge_new = userAge.innerHTML;
    userAge.innerHTML = "<input type='text' id='userAge' name='userAge' class='input' value='" + userAge_new + "'/>";

    var userintroduce = document.getElementById("userIntroduce");
    var userintroduce_new = userintroduce.innerHTML;
    //userintroduce.innerHTML = "<textarea rows='3' cols='20' type='text' id='userintroduce' name='userintroduce' class='input' value='" + userintroduce_new + "'/>";
    userintroduce.innerHTML = "<textarea rows='3' cols='20' type='text' id='userintroduce' name='userintroduce' class='input'/>" + userintroduce_new + "</textarea>";
}


