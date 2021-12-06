<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="creatingGame.aspx.cs" Inherits="creatingGame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> משה הדג- עורך</title>
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content="משה הדג- עריכה ויצירת משחקים" />
    <meta name="keywords" content="משה הדג, זיהוי, משחק, לימודי, " />
    <meta name="author" content="אורי אלון, מאי אלעזר וליאת בן יוסף" />
        <link  href="editorImages/MOSHEtheFish_moshe-12.png"  rel="icon" />

            <!-- הפניה לקובץ הCSS -->
    <link href="styles/myStyle.css" rel="stylesheet" />
           <!--גופן!! -->
    <link href="https://fonts.googleapis.com/css2?family=Rubik&display=swap" rel="stylesheet"/>

    
    <%--הפניה לקובץ jquary--%>
    <script src="jscripts/jquery-1.12.0.min.js"></script>
    <%--הפניה לקובץ JS שלנו--%>
    <script src="jscripts/myScript.js"></script>
     <%--הפניה לבוטסראפ--%>
    <script src="jscripts/bootstrap-maxlength.js"></script>

    <style type="text/css">
        .CharacterCount {}
        .Exit {}
    </style>
</head> 

<body dir="rtl" class="editorPages">
    <form id="form1" runat="server">
        <div id="container">
        <div>
             <header>
            <!--קישור לדף עצמו כדי להתחיל את המשחק מחדש בלחיצה על הלוגו-->
            <a class="menu" href="index.html">
                <img id="logo" src="editorImages/logo.png" />
               
                <%--<p class="menu"> משה הדג</p>--%>
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
            &nbsp;
            <%--      ------------------------------------------------      כפתור חזרה למשחקים שלי--%>

            <br />
            <asp:Button ID="backToGames" runat="server" Text="חזרה למשחקים שלי" OnClick="backToGames_Click" CssClass="buttons" />
            <br />
            <%--      ------------------------------------------------      לייבלים של תנאי פרסום--%>
            <asp:Panel ID="pubTermsPanel" runat="server">
            <asp:Label ID="pubTerms" runat="server" Text="תנאי סף לפרסום:"></asp:Label>
&nbsp;<asp:Label ID="pubName" runat="server" Text="שם למשחק" ></asp:Label>
&nbsp;<asp:Label ID="space1" runat="server" Text=" | "></asp:Label>
            <asp:Label ID="pubDir" runat="server" Text="הנחייה למשחק"></asp:Label>
&nbsp;<asp:Label ID="space2" runat="server" Text=" | "></asp:Label>
            <asp:Label ID="pubMinAns" runat="server" Text="לפחות 20 תשובות" ></asp:Label>
</asp:Panel>
                        <%--      ------------------------------------------------      כפתור פיבלוש--%>

            <asp:Button ID="PubBTN" runat="server" Text="פרסם" OnClick="PubBTN_Click"  />
            <hr />
            <br />
            <asp:Label ID="Result" runat="server" Text="" CssClass="hidden" ></asp:Label>
           <%--  <asp:Label ID="PanClick" runat="server" Text=""></asp:Label>--%>
                        <%--      --------------------------הגדרות-------------------------------%>
            <div id="setting">
                <asp:Label ID="settingLbl" runat="server" Text="הגדרות"></asp:Label>
            </div>
            <div id="settingContent">
         
            <%--      ------------------------------------------------     חלק של שם המשחק--%>

            <asp:Panel ID="TTgameName" CssClass="toolTipName" runat="server">
            <asp:Image ID="gameNameInfo"  runat="server" CssClass="info" ImageUrl="~/editorImages/info.png" />
            <asp:Label CssClass="toolTipNameText" ID="gameNameTooltipLbl" runat="server" Text="יש להזין שם משחק"></asp:Label>
            </asp:Panel>
&nbsp;&nbsp;<asp:Label ID="GameNameLBL" runat="server" Text="שם המשחק: "></asp:Label>
                    <asp:TextBox ID="gameNameTXT" CharacterLimit="20" MaxLength="21" CharacterForLabel="gameNameLimitChar" CharacterForLabelMax="tooMuchgameName"  CssClass="CharacterCount" runat="server"></asp:TextBox>
                <asp:Label ID="tooMuchgameName" runat="server" Text="" ></asp:Label>
        <asp:Label ID="gameNameLimitChar" runat="server" Text="0/20" CssClass="countChar"></asp:Label> 
            <br />
            <br />

               
           
            
                
&nbsp;<%--      ------------------------------------------------      הנחיית המשחק--%>
                 <asp:Panel ID="TTgameDir" CssClass="toolTipDir" runat="server">
             <asp:Image  ID="dirInfo" runat="server" CssClass="info"  ImageUrl="~/editorImages/info.png" />
             <asp:Label CssClass="toolTipDirText" ID="dirTooltipLbl" runat="server" Text="יש להשלים את הנחיית המשחק"></asp:Label>
            </asp:Panel>
            <asp:Label  ID="gameDirLBL"  runat="server" Text="הנחייה לשחקן: אסוף את כל ה -"></asp:Label>
&nbsp;
            <asp:TextBox ID="gameDirTXT" CharacterLimit="25" MaxLength="26" CharacterForLabel="gameDirLimitChar" CharacterForLabelMax="tooMuchgameDir"  CssClass="CharacterCount" runat="server"></asp:TextBox>
&nbsp;<asp:Label ID="gameDirTwo" runat="server" Text="והתחמק מכל השאר"></asp:Label>
            <asp:Label ID="gameDirLimitChar" runat="server" Text="0/25" CssClass="countChar"></asp:Label>
                <asp:Label ID="tooMuchgameDir" runat="server" Text=""></asp:Label>

            <br />
            <br />

&nbsp;<%--      ------------------------------------------------      משוב לשחקן--%>
            <asp:Label ID="FbkLBL" runat="server" Text="משוב לשחקן: "></asp:Label>
            &nbsp;<asp:Label ID="fbForPlayerLbl" runat="server" Text="כל הכבוד! סיימת להאכיל את משה הדג!"></asp:Label>
            &nbsp;<asp:TextBox ID="FbkTxt" runat="server"  MaxLength="51" CharacterLimit="50" CharacterForLabel="FBKLimitChar" CharacterForLabelMax="tooMuchPlayerFB"  CssClass="CharacterCount" Text="" Visible="false" ></asp:TextBox>
            <asp:Label ID="FBKLimitChar" runat="server" Text="0/50" CssClass="countChar" ></asp:Label>
                <asp:Label ID="tooMuchPlayerFB" runat="server" Text=""></asp:Label>
                  &nbsp;<asp:ImageButton ID="editFB" CssClass="icons" runat="server" ImageUrl="~/editorImages/edit.png" onClick="editFB_Click" />
            <br />
                &nbsp;<%--      ------------------------------------------------      כפתור שמירה--%>

            <asp:Button ID="saveBTN" runat="server" OnClick="saveBTN_Click" Text="שמירת הגדרות" CssClass="buttons" />
            <br />
            <br />
<%--      ----------------------סוגר את דיב של תכן ההגדרות--%>

            </div>
                        <%--      ------------------------------------------------    הוספת מסיחים--%>

            <asp:Panel ID="addingAnswers" runat="server">

            <asp:FileUpload ID="FileUpload1" runat="server" Width="0px" type="file" accept=".png,.jpg,.jpeg,.gif"/>
            <asp:FileUpload ID="FileUpload2" runat="server" Width="0px" type="file" accept=".png,.jpg,.jpeg,.gif"/>
            <asp:FileUpload ID="FileUpload3" runat="server" Width="0px" type="file" accept=".png,.jpg,.jpeg,.gif"/>

            <asp:Label ID="addAnslbl" runat="server" Text="הוספת תשובה:"></asp:Label>
            <br />
           
            <asp:ImageButton ID="addPicBTN" runat="server" Height="77px" ImageUrl="~/editorImages/addPic.png" Width="71px"  OnClientClick="openFileUploader1(); return false;"/>
      
            &nbsp;<asp:Label ID="orLbl" runat="server" Text="או"></asp:Label>
            &nbsp;<asp:ImageButton ID="addTxtBTN" runat="server"  ImageUrl="~/editorImages/addTxt.png"  OnClick="addTxtBTN_Click" />
            <br />
            <asp:Label ID="addPicLBL" runat="server" Text="תמונה"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="addTXTlbl" runat="server" Text="טקסט"></asp:Label>
            <br />
            <br />
            <br />
             <%--      ------------------------------------------------     הוספת תמונה--%>
 <asp:Panel ID="wrongFBK" runat="server" CssClass="hidden"> <%--הפאנל הזה צריך להיות בחוץ--%>
                    <asp:Label ID="wrongFBKlbl" runat="server" Text="הקובץ הנבחר אינו מתאים. לחץ על אייקון התמונה לבחירה מחדש" ></asp:Label>
                </asp:Panel>

            <asp:Panel ID="panelPic" runat="server" CssClass="hidden" Height="360px" Width="358px">
                <asp:ImageButton ID="removePicAns" CssClass="icons" runat="server" ImageUrl="~/editorImages/bin.png" OnClick="removePicAns_Click" OnClientClick="addClass(); return false;" />
                <br />
                <br />
                <asp:Image ID="showMeTheImg" runat="server" />
                <br />
                  <span id="spnDocMsg" class="error" style="display: none;"></span>
               
                <br />
                <br />
                <br />
                <br />
                <asp:RadioButtonList ID="PicIfCorrectCBK" runat="server"  AutoPostBack="false">
                    <asp:ListItem Value="0">תשובה נכונה</asp:ListItem>
                    <asp:ListItem Value="1">תשובה לא נכונה</asp:ListItem>
                </asp:RadioButtonList>
                <br />
                <asp:Button ID="addPicAnsBTN"  runat="server" CssClass="buttons" OnClick="addPicAnsBTN_Click" Text="שמור תשובה"  />
                <br />
                <br />
                <br />
            </asp:Panel>
            <br />
            <br />
            <br />

                        <%--      ------------------------------------------------     הוספת מסיח טקסט--%>

            <asp:Panel ID="panelTxt" runat="server" >
                <asp:ImageButton ID="removeTextAns" CssClass="icons" runat="server" ImageUrl="~/editorImages/bin.png" OnClick="removeTextAns_Click" />
                <br />
                <asp:TextBox ID="addAnsTxt"  runat="server" TextMode="SingleLine" MaxLength="51" CharacterForLabel="textAnsLimitChar" CharacterForLabelMax="tooMuchAddAns" CssClass="CharacterCount" Height="57px" Width="305px"></asp:TextBox>
                <asp:Label ID="textAnsLimitChar" runat="server" Text="0/50" CssClass="countChar"></asp:Label>
                <asp:Label ID="tooMuchAddAns" runat="server" Text="" ></asp:Label>

                <br />
                <br />
                <asp:RadioButtonList ID="TextIfCorrectCBK" runat="server" >
                    <asp:ListItem Value="0">תשובה נכונה</asp:ListItem>
                    <asp:ListItem Value="1">תשובה לא נכונה</asp:ListItem>
                </asp:RadioButtonList>
                <br />
                <asp:Button ID="addTxtAnsBtn"  runat="server" Text="שמור תשובה" OnClick="addTxtAnsBtn_Click" CssClass="buttons" />
            </asp:Panel>
            <br />
            <br />
            <br />
            <br />
            <br />
                </asp:Panel>

                       <asp:Label ID="MAXChartyp" runat="server" Text="שים לב, מספר התווים המקסימלי הוא 50" ForeColor="Red"></asp:Label>

            <div class="wrapper">
                <div class="box">
<div id="content">
  
            <%--      ------------------------------------------------      גריד של תשובות נכונות--%>
            <asp:Panel ID="trueAnsHeadlinePanel" runat="server">
            <asp:Label ID="trueAnsHeadline" runat="server" Text="תשובות נכונות"></asp:Label>
            &nbsp;<asp:Label ID="THowMany1" CssClass="howMany" runat="server" Text="(עוד "></asp:Label>
&nbsp;<asp:Label ID="THmowManyNum" CssClass="howMany" runat="server" Text="10"></asp:Label>
&nbsp;<asp:Label ID="THowMany2" CssClass="howMany" runat="server" Text="תשובות לפרסום )"></asp:Label>
            </asp:Panel>
      
                      <asp:Panel ID="falseAnsHeadlinePanel" runat="server">
     <asp:Label ID="falseAnsHeadline" runat="server" Text="תשובות לא נכונות"></asp:Label>
            <asp:Label ID="FHowMany1" runat="server" Text="(עוד "></asp:Label>
            <asp:Label ID="FHmowManyNum" runat="server" Text="10 "></asp:Label>
            <asp:Label ID="FHowMany2" runat="server" Text="תשובות לפרסום)"></asp:Label>
            </asp:Panel>

            <br />
                        <asp:Label ID="noTrueAns" runat="server" Text="עוד לא נוספו תשובות"></asp:Label>
                        <asp:Label ID="noFalseAns" runat="server" Text="עוד לא נוספו תשובות"></asp:Label>



            <div class="gridDiv"  id="gridTrueDiv">
                 <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"/>       
                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                 <ContentTemplate>

            <asp:GridView ID="GridViewTrueAns" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="False" Width="580px" DataSourceID="XmlDataSourceTrueAns" Height="79px" OnRowCommand="GridViewTrueAns_RowCommand">
               
                <Columns>
                  <asp:TemplateField HeaderText="תשובה">
                   <ControlStyle />
                        <ItemTemplate> 
                            <div class="input-group">
                            <asp:TextBox ID="changeTrueAns" width="310px" MaxLength="50" TheItemRow='<%#Container.DataItemIndex%>' CharacterLimit="50" CharacterForLabel="AnsLimitChar" CssClass="MaxCharW" runat="server" theAnsId='<%#XPathBinder.Eval(Container.DataItem, "@isCorrect/..").ToString()%>'  ></asp:TextBox>
                                <%--<span id="LBL" class="input-group-addon maxlength"></span>--%>
                                <asp:Label ID="LBL" class="input-group-addon maxlength LBL"  runat="server" Text="" ></asp:Label>
                               
                             </div>
                            <asp:Label ID="tooMuchTrueAns" cssClass="tooMuchGrid" runat="server" Text=""></asp:Label>
                            <asp:Panel ID="resizeImgPanel" runat="server" Width="90">
                            <asp:Image ID="imagetest"  runat="server" TheItemRow='<%#Container.DataItemIndex%>'  Width="60px"  class="imagetestClass" AutoPostBack="false" EnableViewState="false" />
                            <asp:Image ID="trueImage" runat="server" TheItemRow='<%#Container.DataItemIndex%>' ImageUrl='<%#"~/uploadedFiles/"+XPathBinder.Eval(Container.DataItem, "@isCorrect/..").ToString()%>'  Width="60px" />
                            </asp:Panel>
                            <asp:Label ID="showAsnlbl" width="310px"  TheItemRow='<%#Container.DataItemIndex%>' runat="server" Text='<%#XPathBinder.Eval(Container.DataItem, "@isCorrect/..").ToString()%>' Visible="false" ></asp:Label> 
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:ImageButton ID="TbigPicBtn" CssClass="icons" runat="server" ImageUrl="~/editorImages/bigPic.png" CommandName="largeImg" TheItemRow='<%#Container.DataItemIndex%>' theAnsId='<%#XPathBinder.Eval(Container.DataItem, "@ansId").ToString()%>'/>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="עריכה">
                        <ItemTemplate>
                            <asp:ImageButton ID="cancelPicTrueAnsChange" runat="server" CssClass="iconnX" ImageUrl="~/editorImages/Exit.png" theAnsId='<%#XPathBinder.Eval(Container.DataItem, "@ansId").ToString()%>' TheItemRow='<%#Container.DataItemIndex%>' CommandName="cancelEditPic"/>
                            <asp:ImageButton ID="cancelTrueAnsChange" runat="server" CssClass="iconnX" ImageUrl="~/editorImages/Exit.png" theAnsId='<%#XPathBinder.Eval(Container.DataItem, "@ansId").ToString()%>' Visible="false" TheItemRow='<%#Container.DataItemIndex%>' CommandName="cancelEdit"/>
                            <asp:ImageButton ID="approveTrueAnsChange"  runat="server" CssClass="iconTick ApproveT" ImageUrl="~/editorImages/tick.png" theAnsId='<%#XPathBinder.Eval(Container.DataItem, "@ansId").ToString()%>' Visible="false" TheItemRow='<%#Container.DataItemIndex%>' CommandName="approveEdit"/>
                            <asp:ImageButton ID="TeditPancilBtn" runat="server" CssClass="icons" ImageUrl="~/editorImages/edit.png"  theAnsId='<%#XPathBinder.Eval(Container.DataItem, "@ansId").ToString()%>' TheItemRow='<%#Container.DataItemIndex%>' CommandName="editRow" />
                           
                            <asp:ImageButton ID="editTruePic" Width="30" runat="server" CssClass="icons" ImageUrl="~/editorImages/edit.png"  theAnsId='<%#XPathBinder.Eval(Container.DataItem, "@ansId").ToString()%>' TheItemRow='<%#Container.DataItemIndex%>' OnClientClick="openFileUploader2(); return false; " />
                            <asp:ImageButton ID="approveTruePicChange" CssClass="iconTick" runat="server" ImageUrl="~/editorImages/tick.png" theAnsId='<%#XPathBinder.Eval(Container.DataItem, "@ansId").ToString()%>' TheItemRow='<%#Container.DataItemIndex%>' CommandName="approveEdit"/>
                            </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="מחיקה" >
                        <ItemTemplate >
                            <asp:ImageButton ID="TremoveAns" CssClass="icons" runat="server" ImageUrl="~/editorImages/bin.png"  theAnsId='<%#XPathBinder.Eval(Container.DataItem, "@ansId").ToString()%>' CommandName="deleteRow" AutoPostBack="True"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                  
            </asp:GridView>
        </ContentTemplate>             
       </asp:UpdatePanel>

            </div>
           </div>
         <%--   <br />


            <br />
            <br />

       
            <br />--%>

                <div class="box">
    
            <div class="gridDiv" id="gridFalseDiv">
                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                 <ContentTemplate>
 <asp:GridView ID="GridViewFalseAns" runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="FixedHeader" DataSourceID="XmlDataSourceFalseAns" Height="79px" Width="580px" OnRowCommand="GridViewFalseAns_RowCommand">
     <Columns>
                     <asp:TemplateField HeaderText="תשובה">
                         <ControlStyle />
                        <ItemTemplate>
                            <asp:Panel ID="resizeFalseImgPanel" runat="server">
                            <asp:Image ID="falseImage"  runat="server" ImageUrl='<%#"uploadedFiles/"+XPathBinder.Eval(Container.DataItem, "@isCorrect/..").ToString()%>'    Width="60px"/>
                             <asp:Image ID="imageFalseTest"  runat="server"  Width="60px"  class="imagetestClass" AutoPostBack="false" EnableViewState="false" />
                            </asp:Panel>
                            <asp:Label ID="FshowAsnlbl" width="310px" runat="server" text='<%#XPathBinder.Eval(Container.DataItem, "@isCorrect/..").ToString()%>' Visible="false" Font-Overline="False" ></asp:Label>
                             <div class="input-group">
                            <asp:TextBox ID="changeFalseAns" width="310px"  MaxLength="50" TheItemRow='<%#Container.DataItemIndex%>' CharacterLimit="50" CharacterForLabel="textAnsLimitChar" CssClass="MaxCharW" runat="server" theAnsId='<%#XPathBinder.Eval(Container.DataItem, "@isCorrect/..").ToString()%>' Visible="false" TextMode="SingleLine"></asp:TextBox>
                             <asp:Label ID="LBL" class="input-group-addon maxlength LBL" runat="server" Text=""></asp:Label>
                             </div>
                            <asp:Label ID="tooMuchFalseAns" cssClass="tooMuchGrid" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
<asp:ImageButton ID="FbigPicBtn" CssClass="icons" runat="server" ImageUrl="~/editorImages/bigPic.png" CommandName="largeImg" theAnsId='<%#XPathBinder.Eval(Container.DataItem, "@ansId").ToString()%>'/>

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="עריכה">
                        <ItemTemplate>
                            <asp:ImageButton ID="cancelPicFalseAnsChange"  runat="server" CssClass="iconnX" ImageUrl="~/editorImages/Exit.png" TheItemRow='<%#Container.DataItemIndex%>' theAnsId='<%#XPathBinder.Eval(Container.DataItem, "@ansId").ToString()%>'  CommandName="cancelEditPic"/>
                            <asp:ImageButton ID="cancelFalseAnsChange" runat="server" CssClass="iconnX" ImageUrl="~/editorImages/Exit.png" TheItemRow='<%#Container.DataItemIndex%>' CommandName="cancelChangeFalse" theAnsId='<%#XPathBinder.Eval(Container.DataItem, "@ansId").ToString()%>' Visible="false"/>
                            <asp:ImageButton ID="approveFalseAnsChange" runat="server" CssClass="iconTick ApproveF" ImageUrl="~/editorImages/tick.png" CommandName="approveChangeFalse" TheItemRow='<%#Container.DataItemIndex%>' theAnsId='<%#XPathBinder.Eval(Container.DataItem, "@ansId").ToString()%>' Visible="false"/>
                            <asp:ImageButton ID="FeditPancilBtn" runat="server" CssClass="icons" ImageUrl="~/editorImages/edit.png" CommandName="editRow" TheItemRow='<%#Container.DataItemIndex%>' theAnsId='<%#XPathBinder.Eval(Container.DataItem, "@ansId").ToString()%>'/>
                            <asp:ImageButton ID="editFalsePic" runat="server" CssClass="icons" ImageUrl="~/editorImages/edit.png"  theAnsId='<%#XPathBinder.Eval(Container.DataItem, "@ansId").ToString()%>' TheItemRow='<%#Container.DataItemIndex%>' OnClientClick="openFileUploader3(); return false;" />
                            <asp:ImageButton ID="approveFalsePicChange" CssClass="iconTick" runat="server" ImageUrl="~/editorImages/tick.png" theAnsId='<%#XPathBinder.Eval(Container.DataItem, "@ansId").ToString()%>' TheItemRow='<%#Container.DataItemIndex%>'  CommandName="approveChangeFalse"/>

                            </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="מחיקה">
                        <ItemTemplate>
                            <asp:ImageButton ID="FremoveAns" CssClass="icons" runat="server" ImageUrl="~/editorImages/bin.png" CommandName="deleteRow"   theAnsId='<%#XPathBinder.Eval(Container.DataItem, "@ansId").ToString()%>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
       </ContentTemplate>             
       </asp:UpdatePanel>
            </div>
           </div>
                </div>
</div>

            <br />
            <br />
            <br />

            <asp:XmlDataSource ID="XmlDataSourceTrueAns" runat="server" DataFile="~/tree/XMLFile.xml"></asp:XmlDataSource>
            <br />
            <asp:XmlDataSource ID="XmlDataSourceFalseAns" runat="server" DataFile="~/tree/XMLFile.xml"></asp:XmlDataSource>
            <br />

  
         
 <%----------------------------------------------------POP UP---------------------------------------%>

  <!-- פאנל עם הרקע האפור שבתוכו יהיו החלונות הקופצים -->
            <asp:Panel ID="grayWindows" CssClass="grayWindows" runat="server" >
              <!-- פופ-אפ למחיקה - כאן אפשר להוסיף את הפקדים הרלוונטים -->
                <asp:Panel ID="DeleteConfPopUp" CssClass="PopUp PopUpImg" runat="server" >
                    <!-- כפתור יציאה - יש לשים לב שהוא מפנה בלחיצה לאותה פונקציה של הכפתור יציאה השני -->
                    <!-- תווית להצגת הודעה למשתמש -->
                    <asp:Label ID="confirmDelLbl" runat="server" Text=""></asp:Label>
                    <asp:Image ID="picToDel" runat="server" />
                     <!-- כפתור לדוגמה -->
                    <asp:Button ID="dontDel" CssClass="importantBTN" runat="server" Text="ביטול" OnClick="dontDel_Click" />
                    <asp:Button ID="OkBtn" CssClass="buttons" runat="server" Text="מחיקה" OnClick="OkBtn_Click" />
                </asp:Panel>
                </asp:Panel>
                

             <asp:Panel ID="wrongFilePanel" CssClass="wrongFileGray" runat="server">
                <asp:Panel ID="wrongFileMsgPanel" CssClass="PopUp hidden" runat="server">
                   <asp:ImageButton ID="Exit" CssClass="Exit" runat="server" ImageUrl="~/editorImages/close.png" OnClick="ExitImageButton_Click"  />

                    <asp:Label ID="worngFileMsg" runat="server" Text="הקובץ הנבחר אינו מתאים. לחץ על עריכה לבחירה מחדש"></asp:Label>
                </asp:Panel>
             </asp:Panel>


            <asp:Panel ID="cantSaveGrayPanel" CssClass="grayWindows" runat="server">
                <asp:Panel ID="cantSave" CssClass="PopUp" runat="server">
                    <asp:Label ID="cantSavelbl" runat="server" Text=""></asp:Label>
                   <asp:ImageButton ID="cantSaveExit" CssClass="Exit" runat="server" ImageUrl="~/editorImages/close.png" OnClick="ExitImageButton_Click" />
                </asp:Panel>
                </asp:Panel>
            

            <asp:Panel ID="bigImgGrayPanel" CssClass="grayWindows" runat="server">
                <!-- פופ-אפ לתמונה המוגדלת - כאן אפשר להוסיף את הפקדים הרלוונטים -->
                <asp:Panel ID="BigImagePopUp" CssClass="PopUp" runat="server" >
                    <!-- כפתור יציאה - יש לשים לב שהוא מפנה בלחיצה לאותה פונקציה של הכפתור יציאה השני -->
                    <!-- התמונה שתוצג-->
                    <asp:Image ID="showBigImage" runat="server" Height="300px" />
                     <asp:ImageButton ID="ExitBigImage" CssClass="Exit" runat="server" ImageUrl="~/editorImages/close.png" OnClick="ExitImageButton_Click"  />

                </asp:Panel>
            </asp:Panel>


            <asp:Panel ID="noSavePanel" CssClass="grayWindows" runat="server">
                <asp:Panel ID="noSaveMsg" CssClass="PopUp" runat="server">
                    <asp:Label ID="doYouWanna" runat="server" Text="שים לב חזרה לטבלת המשחקים ללא שמירה תוביל למחיקת המשחק."></asp:Label>
                    <asp:Label ID="areYouSure" runat="server" Text="ברצונך לשמור את המשחק?"></asp:Label>
                    <asp:Button ID="dontSave" CssClass="importantBTN" runat="server" Text="חזרה ללא שמירה" OnClick="dontSave_Click" />
                    <asp:Button ID="saveBackToGames" CssClass="buttons" runat="server" Text="שמירה וחזרה" OnClick="saveBackToGames_Click" />
                </asp:Panel>
            </asp:Panel>

            <asp:Panel ID="noContentPanel" CssClass="grayWindows" runat="server">
                <asp:Panel ID="noContent" CssClass="PopUp" runat="server">
                    <asp:Label ID="noContentlbl" runat="server" Text="חסר תוכן לשמירת המשחק. חזרה לטבלת המשחקים תוביל למחיקתו. "></asp:Label>
                    <asp:Button ID="confirmBTN" CssClass="buttons" runat="server" Text="אישור" OnClick="confirmAndDel_Click" />
                    <asp:Button ID="cancelBTN" CssClass=" importantBTN" runat="server" Text="ביטול" OnClick="dontDelAndStay_Click" />
                </asp:Panel>

            </asp:Panel>
            <asp:Panel ID="ONeditPanel" CssClass="grayWindows" runat="server">
                <asp:Panel ID="ONeditPanelMsgPanel" CssClass="PopUp" runat="server">
                <asp:Label ID="ONeditPanelMsg" runat="server" Text="שים לב, עליך לסיים את העריכה על מנת להמשיך" ></asp:Label>
                 <asp:ImageButton ID="exitMsg" CssClass="Exit" runat="server" ImageUrl="~/editorImages/close.png" OnClick="ExitImageButtonPopUpEditing_Click"  EnableViewState="false" AutoPostBack="false" />
                </asp:Panel>
            </asp:Panel>

            <br />
            <br />
        </div>
    </form>
</body>
</html>
