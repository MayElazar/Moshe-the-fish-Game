<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="login.aspx.cs" Inherits="loginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content="משה הדג- כניסה לעורך" />
    <meta name="keywords" content="" />
    <meta name="author" content="אורי אלון, מאי אלעזר וליאת בן יוסף" />
    <title>משה הדג- כניסה לעורך</title>
        <link  href="editorImages/MOSHEtheFish_moshe-12.png"  rel="icon" />

            <!-- הפניה לקובץ הCSS -->
    <link href="styles/myStyle.css" rel="stylesheet" />
           <!--גופן!! -->
    <link href="https://fonts.googleapis.com/css2?family=Rubik&display=swap" rel="stylesheet"/>

    
    <%--הפניה לקובץ jquary--%>
    <script src="jscripts/jquery-1.12.0.min.js"></script>
    <%--הפניה לקובץ JS שלנו--%>
  <%--  <script src="jscripts/myScript.js"></script>--%>
    <script src="jscripts/myLoginScript.js"></script>



</head>

<body id="loginBody" dir="rtl">
    <form id="form1" runat="server">
        <div id="container">
        <div>
             <header>
            <!--קישור לדף עצמו כדי להתחיל את המשחק מחדש בלחיצה על הלוגו-->
            <a href="index.html">
                <img id="logo" src="editorImages/logo.png" />
<%--                <p class="menu">משה הדג</p>--%>
            </a>
            <!--תפריט הניווט בראש העמוד-->
            <nav>
                <ul>
                     <li><a class="menu about">אודות</a></li>
                    <li><a class="menu howToPlay">איך משחקים?</a></li>
                    <li><a class="menu Editor" href="index.html">למשחק</a></li>

                </ul>
            </nav>
        </header>
        <div id="aboutDiv" class="popUpMenu bounceInDown hide">
            <a class="closeAbout"><img class="closeThePopUp" src="editorImages/close.png" /></a>
            <p>אפיון ופיתוח: מאי אלעזר, אורי אלון וליאת בן יוסף</p>
            <p>
                אופיין ופותח במסגרת פרויקט בקורסים: <br />
                סביבות לימוד אינטראקטיביות 1 + סביבות אינטראקטיביות 1, תש"פ
                <br />
                <br />
                קרדיטים:
                <br />
                <br />
                התמונות נלקחו מהאתרים:
                <br />

                <a href="https://www.freepik.com/home" target="_blank">freepik</a>
                <br />
                <a href="https://www.pixabay.com/" target="_blank">pixabay</a>
                <br />
                <a href="https://www.flaticon.com/home" target="_blank">flaticon</a>
                <br />
                <a href="https://ccsearch.creativecommons.org/photos/56c50472-fb3a-416d-befe-a9d9a81cfea3" target="_blank">CC search</a>
                <br />
                <br />
                הסאונד נקלח מהאתרים:
                <br />

                <a href="http://dig.ccmixter.org" target="_blank">ccmixter</a><br />
                <a href="https://www.soundeffectsplus.com" target="_blank">soundeffectsplus</a><br />
                <a href="https://www.soundeffectsplus.com" target="_blank">soundeffectsplus</a><br />
                <a href="https://freesound.org" target="_blank">freesound</a><br />
            </p>
            <a href="https://www.hit.ac.il/telem/overview" target="_blank"><img class="hitLogo" src="editorImages/hit.png" /></a>
            <p>המכון טכנולוגי חולון</p>

        </div>
        <div id="howToPlayDiv" class="popUpMenu bounceInDown hide">
             <a class="closeHowToPlay"><img class="closeThePopUp" src="editorImages/close.png" /></a>
            <img class="howTo" src="editorImages/howTo.png" />
        </div>
            <asp:Image ID="loginPic" CssClass="loginPic" runat="server" Height="550px" ImageUrl="~/editorImages/loginPicMax.png" Width="305px" />
            <br />
            <asp:Image ID="wrongFBKPic" CssClass="wrongFBKPic" runat="server" Height="550px" ImageUrl="~/editorImages/loginPicWrongMax.png" Width="305px" />
</div>
        <div id="loginPageLayOut">
            <asp:Label ID="connectLbl" runat="server" Text="התחברות"></asp:Label>
            <br />
            <br />
            <div id="loginField">
            <asp:Label ID="userNameLbl" runat="server" Text="שם משתמש:"></asp:Label>
            
&nbsp;<asp:TextBox ID="userNameTxt"  runat="server" CssClass="loginTB" ></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="passwordLbl" runat="server" Text="ססמה: "></asp:Label>
&nbsp;<asp:TextBox ID="passwordTxt"   runat="server" TextMode="Password" CssClass="loginTB"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="loginBtn" runat="server" OnClick="loginBtn_Click" Text="כניסה" CssClass="buttons loginBtn:active"  />
            <br />
            <br />
        </div>
        </div>
        </div>
        <div class="fotter"></div>
    </form>
</body>
</html>
