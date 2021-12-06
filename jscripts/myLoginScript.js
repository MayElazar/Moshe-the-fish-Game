
//כאשר העמוד נטען
$(document).ready(function () {

    /*עבור תבנית המשחק*/

    $(".about").click(function () {
        $("#aboutDiv").toggle();
    });

    $(".howToPlay").click(function () {
        $("#howToPlayDiv").toggle();
    });

    $(".closeAbout").click(function () {
        $("#aboutDiv").hide();
        $("#gameIframe")[0].contentWindow.focus();
    });

    $(".closeHowToPlay").click(function () {
        $("#howToPlayDiv").hide();
        $("#gameIframe")[0].contentWindow.focus();
    });


    //----------------שינוי כפתור לקליקבילי- מסך התחברות לעורך--------------------//
    $("#loginBtn").attr("disabled", "true");

    $(document).keyup(function () {
        if ($("#userNameTxt").val().length >= 3 && $("#passwordTxt").val().length >= 3) {
            $("#loginBtn").removeAttr("disabled");
           
        } else {
            $("#loginBtn").attr("disabled", "true");
        }
    }); 

    $("#userNameTxt").keyup(function () {
        $("#loginPic").removeClass("hidden");
        $("#loginPic").addClass("loginPic");
        $('#wrongFBKPic').addClass("hidden");

    });


    $("#passwordTxt").keyup(function () {

        $("#loginPic").removeClass("hidden");
        $("#loginPic").addClass("loginPic");
        $('#wrongFBKPic').addClass("hidden");

    });


});







