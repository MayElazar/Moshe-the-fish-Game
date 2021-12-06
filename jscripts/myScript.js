
function openFileUploader1() {
    $('#FileUpload1').click();
}


var iRowIndex;
    function openFileUploader2() {

        if ($('#Result').text() == "false") {
            $('#FileUpload2').click();
        }
        if ($('#Result').text() == "true") {

            console.log("onEditMood")

            ONeditPanel.classList.remove("hidden");
            ONeditPanelMsgPanel.classList.remove("hidden");

            ONeditPanel.classList.add("grayWindows");
            ONeditPanelMsgPanel.classList.add("PopUp");

        }
      
    }

function openFileUploader3() {
    if ($('#Result').text() == "false") {
        $('#FileUpload3').click();
    }
    if ($('#Result').text() == "true") {

        console.log("onEditMood")

        ONeditPanel.classList.remove("hidden");
        ONeditPanelMsgPanel.classList.remove("hidden");

        ONeditPanel.classList.add("grayWindows");
        ONeditPanelMsgPanel.classList.add("PopUp");

    }
    
}




window.addEventListener('DOMContentLoaded', (event) => {

    //----------------בדיקת תנאי פרסום--------------------//
    $(document).keyup(function () {

       
        $(".MaxCharW").maxlength();



        if ($("#gameNameTXT").val().length <= 1 && $("#gameDirTXT").val().length <= 1) {
            $("#PubBTN").addClass("hidden");

        } else {

            $("#PubBTN").removeClass("hidden");
        }

        if ($("#gameNameTXT").val().length > 1) {

            document.getElementById("pubName").className = "EanoghChar";
        }
        else {

            document.getElementById("pubName").className = "notEanoghChar";
        }

        if ($("#gameDirTXT").val().length > 1) {

            document.getElementById("pubDir").className = "EanoghChar";
        }
        else {

            document.getElementById("pubDir").className = "notEanoghChar";

        }

    });


    const MinAns = document.querySelector("#pubMinAns");
    const dir = document.querySelector("#pubDir");
    const name = document.querySelector("#pubName");




    if (MinAns.classList.contains("EanoghChar") && dir.classList.contains("EanoghChar") && name.classList.contains("EanoghChar")) {
        document.getElementById("PubBTN").classList.remove("hidden");

    }
    else {
        document.getElementById("PubBTN").className = "hidden";
    }


    //בדיקה האם ניתן לשמור את המשחק
    if (document.getElementById("gameNameTXT").value.length > 2 && document.getElementById("gameDirTXT").value.length > 2 && document.getElementById("fbForPlayerLbl").value != "") {
        document.getElementById("saveBTN").disableed = false;
    }
    else {
        document.getElementById("saveBTN").disableed = true;
    }

    //בדיקה לשינוי לייבל שסופר את התווים של הנחיה למשחק
    if (document.getElementById("gameDirTXT").value.length >= 2) {
        document.getElementById("gameDirLimitChar").className = "countCharGreen";
        document.getElementById("pubDir").className = "EanoghChar";

    }
    else {
        document.getElementById("gameDirLimitChar").className = "countCharRed";
        document.getElementById("pubDir").className = "notEanoghChar";
    }

    //    //בדיקה לשינוי לייבל שסופר את התווים של שם המשחק
    if (document.getElementById("gameNameTXT").value.length >= 2) {
        document.getElementById("gameNameLimitChar").className = "countCharGreen";
        document.getElementById("pubName").className = "EanoghChar";
    }


    //תנאי משחק
    if (document.getElementById("gameNameTXT").value.length <= 1) {
        document.getElementById("gameNameLimitChar").className = "countCharRed";
        document.getElementById("pubName").className = "notEanoghChar";

    } else {
        document.getElementById("gameNameLimitChar").className = "countCharGreen";
        document.getElementById("pubName").className = "EanoghChar";

    }


    //בדיקה לשינוי לייבל שסופר את התווים של משוב לשחקן
    if (document.getElementById("FbkTxt").value.length >= 2) {
        document.getElementById("FBKLimitChar").className = "countCharGreen";
    }
    else {
        document.getElementById("FBKLimitChar").className = "countCharRed";
    }


    //בדיקה לשינוי לייבל שסופר את התווים של עריכת תשובה
    if (document.getElementById("changeTrueAns").value.length >= 2) {
        document.getElementById("AnsLimitChar").className = "countCharGreen";
    }
    else {
        document.getElementById("AnsLimitChar").className = "countCharRed";
    }

    if (document.getElementById("addAnsTxt").value.length == 0) {
        document.getElementById("textAnsLimitCharס").className = "countCharRed";

    }
});


//כאשר העמוד נטען
$(document).ready(function () {



    /*עבור תבנית המשחק*/
   // $("#aboutDiv").addClass("hidden");

    $(".about").click(function () {
        //$("#aboutDiv").removeClass("hidden");
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

    $(".MaxCharW").maxlength();
    // מציאת האינדקס עליו לחצתי בטבלה

    $('table tbody tr').click(function () {

        iRowIndex = $(this).closest("tr").prevAll("tr").length;
        console.log("GridViewTrueAns_trueImage_" + (iRowIndex - 1));
        

    });

   

    //----------------לייבלים לסירת הטקסט טהצגתו--------------------//

    //בהקלדה בתיבת הטקסט
    $(".CharacterCount").keyup(function () {
        checkCharacter($(this)); //קריאה לפונקציה שבודקת את מספר התווים
    });


    $(".CharacterCount").oncut = $(".CharacterCount").oncopy = $(".CharacterCount").onpaste = function (event) {
        checkCharacter($(this));
    }


    //בהעתקה של תוכן לתיבת הטקסט
    $(".CharacterCount").on("keyup contextmenu input", function () {
        checkCharacter($(this));//קריאה לפונקציה שבודקת את מספר התווים
    });


    function checkCharacter(myTextBox) {

        //משתנה למספר התווים הנוכחי בתיבת הטקסט
        var countCurrentC = myTextBox.val().length;

        //משתנה המכיל את מספר התווים שמוגבל לתיבה זו
        var CharacterLimit = myTextBox.attr("MaxLength");

        //משתנה המקבל את שם הלייבל המקושר לאותה תיבת טקסט 
        var LableToShow = myTextBox.attr("CharacterForLabel");
        var errortoShow = myTextBox.attr("CharacterForLabelMax");

        var charlimitint = parseInt(CharacterLimit) - 1;
        var charlimitString = charlimitint.toString();

        console.log(errortoShow);

        //בדיקה האם ישנה חריגה במספר התווים
        if (countCurrentC > charlimitint) {
            //countCurrentC = 
            console.log("חריג");
            //document.getElementById("Label1").Text = "not";
            document.getElementById(LableToShow).className = "countCharRed";
            //מחיקת התווים המיותרים בתיבה
            myTextBox.val(myTextBox.val().substring(0, charlimitString));
            //עדכון של מספר התווים הנוכחי
            countCurrentC1 = charlimitString;
            document.getElementById(errortoShow).innerHTML = "שים לב, מספר התווים המקסימלי הוא " + charlimitString;


        } else {

            countCurrentC1 = countCurrentC;

        }
        
        setTimeout(function () {
            document.getElementById(errortoShow).innerHTML = "";

        }, 7000);

        console.log(charlimitString);


        if (countCurrentC < 2) {
            document.getElementById(LableToShow).className = "countCharRed";
        }

        if (countCurrentC > 2 && countCurrentC <= charlimitint) {
            document.getElementById(LableToShow).className = "countCharGreen";
        }

        //הדפסה כמה תווים הוקלדו מתוך כמה
        $("#" + LableToShow).text(countCurrentC1 + "/" + charlimitString);

    }
    
    $("#addTxtAnsBtn").attr("disabled", "true");

    $("#addAnsTxt").keyup(function () {
        if ($("#addAnsTxt").val().length > 2) {
            $("#TextIfCorrectCBK").change(function () {

                $("#addTxtAnsBtn").removeAttr("disabled");


            });
        }
        if ($("#addAnsTxt").val().length < 2) {


            $("#addTxtAnsBtn").attr("disabled", "true");

        }
    });


    $("#TextIfCorrectCBK").change(function () {
        $("#addAnsTxt").keyup(function () {
            if ($("#addAnsTxt").val().length > 2) {


                $("#addTxtAnsBtn").removeAttr("disabled");

            } if ($("#addAnsTxt").val().length < 2) {


                $("#addTxtAnsBtn").attr("disabled", "true");

            }

        });
    });

    //----------------הגדרות של הפופ אפ רקע אפור --------------------//

    //תגדיר לתוך משתנים את האורך והרוחב של החלון
    var windowsW = $(document).width();
    var windowsH = $(document).height();

    //תגדיר ערכים אלה לפאנל של החלון האפור
    //יגרום לכך שהחלון האפור תמיד יהיה בגודל של החלון שלנו
    $(".grayWindows").css("width", windowsW);
    $(".grayWindows").css("height", windowsH);

    $("#wrongFilePanel").css("width", windowsW);
    $("#wrongFilePanel").css("height", windowsH);

    //----------------העלאת תמונה--------------------//

    // פונרציה לביקת סוג הקובץ שנבחר אם לא נבחר סוג קובץ מתאים מוצגת הודעה לבחירת תמונה אחרת
    var file = document.getElementById('FileUpload1');

    file.onchange = function (e) {
        var ext = this.value.match(/\.([^\.]+)$/)[1];
        document.getElementById("wrongFBK").classList.add("hidden");


        switch (ext) {
            case 'JPG':
            case 'jpg':
            case 'jpeg':
            case 'JPEG':
            case 'BMP':
            case 'bmp':
            case 'PNG':
            case 'png':
            case 'tif':
            case 'TIF':
            case 'GIF':
            case 'gif':


                if (this.files && this.files[0]) {

                    var reader = new FileReader();

                    reader.onload = function (e) {

                        document.getElementById("panelPic").classList.remove("hidden");
                        $('#showMeTheImg').attr('src', e.target.result);
                    }
                    reader.readAsDataURL(this.files[0]);
                }

                break;
            default:
                document.getElementById("wrongFBK").classList.remove("hidden");
                this.value = '';

        }
    };

    //----------------העלאת תמונה לעריכה מסיחים נכונים--------------------//

    var file = document.getElementById('FileUpload2');

    file.onchange = function (e) {
       

        var ext = this.value.match(/\.([^\.]+)$/)[1];
        switch (ext) {
            case 'JPG':
            case 'jpg':
            case 'jpeg':
            case 'JPEG':
            case 'BMP':
            case 'bmp':
            case 'PNG':
            case 'png':
            case 'tif':
            case 'TIF':
            case 'GIF':
            case 'gif':

                if (this.files && this.files[0]) {

                    var reader = new FileReader();



                    reader.onload = function (e) {
                        $("#GridViewTrueAns_imagetest_" + (iRowIndex - 1)).removeClass("hidden");
                        $("#GridViewTrueAns_trueImage_" + (iRowIndex - 1)).addClass("hidden");

                        $("#GridViewTrueAns_imagetest_" + (iRowIndex - 1)).attr('src', e.target.result);
                        $("#GridViewTrueAns_approveTruePicChange_" + (iRowIndex - 1)).removeClass("hidden");
                        $("#GridViewTrueAns_cancelPicTrueAnsChange_" + (iRowIndex - 1)).removeClass("hidden");

                        $("#GridViewTrueAns_editTruePic_" + (iRowIndex - 1)).addClass("hidden");
                        $("#GridViewTrueAns_approveTruePicChange_" + (iRowIndex - 1)).addClass("icons");
                        $("#GridViewTrueAns_cancelPicTrueAnsChange_" + (iRowIndex - 1)).addClass("icons");

                        
                        return false;
                    }
                    reader.readAsDataURL(this.files[0]);
                }
                // לאחר הבחירה בקובת טוב יש לוודא שהכפתורים לא לחיצים

                break;
            default:
                document.getElementById("wrongFilePanel").classList.remove("hidden");
                document.getElementById("wrongFileMsgPanel").classList.remove("hidden");
                document.getElementById("wrongFilePanel").classList.add("wrongFileGray")

                this.value = '';

        }
    };


    //----------------העלאת תמונה לעריכה מסיחים לא נכונים--------------------//

    var file = document.getElementById('FileUpload3');


    file.onchange = function (e) {
       
        var ext = this.value.match(/\.([^\.]+)$/)[1];
        switch (ext) {
            case 'JPG':
            case 'jpg':
            case 'jpeg':
            case 'JPEG':
            case 'BMP':
            case 'bmp':
            case 'PNG':
            case 'png':
            case 'tif':
            case 'TIF':
            case 'GIF':
            case 'gif':

                if (this.files && this.files[0]) {

                    var reader = new FileReader();



                    reader.onload = function (e) {
                        $("#GridViewFalseAns_imageFalseTest_" + (iRowIndex - 1)).removeClass("hidden");
                        $("#GridViewFalseAns_falseImage_" + (iRowIndex - 1)).addClass("hidden");

                        $("#GridViewFalseAns_imageFalseTest_" + (iRowIndex - 1)).attr('src', e.target.result);
                        $("#GridViewFalseAns_approveFalsePicChange_" + (iRowIndex - 1)).removeClass("hidden");
                        $("#GridViewFalseAns_cancelPicFalseAnsChange_" + (iRowIndex - 1)).removeClass("hidden");

                        $("#GridViewFalseAns_editFalsePic_" + (iRowIndex - 1)).addClass("hidden");
                        $("#GridViewFalseAns_cancelPicFalseAnsChange_" + (iRowIndex - 1)).addClass("icons");
                        $("#GridViewFalseAns_approveFalsePicChange_" + (iRowIndex - 1)).addClass("icons");


                        return false;
                    }
                    reader.readAsDataURL(this.files[0]);
                }
                // לאחר הבחירה בקובת טוב יש לוודא שהכפתורים לא לחיצים

                break;
            default:
                document.getElementById("wrongFilePanel").classList.remove("hidden");
                document.getElementById("wrongFileMsgPanel").classList.remove("hidden");
                document.getElementById("wrongFilePanel").classList.add("wrongFileGray")


                this.value = '';


        }
    };

});







