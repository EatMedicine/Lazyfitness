function changeInput() {
    document.getElementById("button").hidden = true;
    document.getElementById("postData").hidden = false;
    document.getElementById("quit").hidden = false;
    document.getElementById("image").hidden = false;
    document.getElementById("postImg").hidden = false;
    document.getElementById("btnGroup").hidden = false;
    var userName = document.getElementById("userName");
    var userName_new = userName.innerHTML;
    userName.innerHTML = "<input type='text' id='userName' name='userName' class='input' value='" + userName_new + "')/>";

    var userSex = document.getElementById("userSex");
    var userSex_new = userSex.innerHTML;
    if (userSex_new === '男') {
        userSex.innerHTML = "<input type='radio' id='userSex' name='userSex' class='input' value='0' checked='checked'/>男 <input type= 'radio' id= 'userSex' name= 'userSex' class='input' value= '1' />女<input type= 'radio' id= 'userSex' name= 'userSex' class='input' value= '2' />保密";
    }
    if (userSex_new === '女') {
        userSex.innerHTML = "<input type='radio' id='userSex' name='userSex' class='input' value='0'/>男<input type= 'radio' name= 'userSex' id= 'userSex' class='input' value= '1' checked= 'checked' />女<input type= 'radio' id= 'userSex' name= 'userSex' class='input' value= '2' />保密";
    }
    if (userSex_new === '保密') {
        userSex.innerHTML = "<input type='radio' id='userSex' name='userSex' class='input' value='0'/>男<input type= 'radio' name= 'userSex' id= 'userSex' class='input' value= '1' />女<input type= 'radio' id= 'userSex' name= 'userSex' class='input' value= '2' checked= 'checked'/>保密";
    }
    var userEmail = document.getElementById("userEmail");
    var userEmail_new = userEmail.innerHTML;
    userEmail.innerHTML = "<input type='text' id='userEmail' name='userEmail' class='input' value='" + userEmail_new + "'/>";


    var userAge = document.getElementById("userAge");
    var userAge_new = userAge.innerHTML;
    userAge.innerHTML = "<input type='text' id='userAge' name='userAge' class='input' value='" + userAge_new + "'/>";

    var userIntroduce = document.getElementById("userIntroduce");
    var userIntroduce_new = userIntroduce.innerText;
    userIntroduce.innerHTML = "<input type='text' id='userIntroduce' name='userIntroduce' class='input' value='" + userIntroduce_new + "'/>";
}
