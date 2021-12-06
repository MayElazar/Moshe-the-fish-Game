<%@ Page Language="C#" AutoEventWireup="true"  MaintainScrollPositionOnPostback="true" CodeFile="gameTable.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>  משה הדג- עורך</title>
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content="משה הדג- טבלת משחקים" />
    <meta name="keywords" content="משחק לימודי, זיהוי, משה הדג" />
    <meta name="author" content="אורי אלון, מאי אלעזר וליאת בן יוסף" />

    <link  href="editorImages/MOSHEtheFish_moshe-12.png"  rel="icon" />
        <!-- הפניה לקובץ הCSS -->
    <link href="styles/myStyle.css" rel="stylesheet" />

     <!-- הפניה לקבצי הסקריפט. חשוב שיהיה את שניהם!! -->
    <script src="jscripts/jquery-1.12.0.min.js"></script>
    <script src="jscripts/myScript.js"></script>

         <!--גופן!! -->
    <link href="https://fonts.googleapis.com/css2?family=Rubik&display=swap" rel="stylesheet"/>
  
</head>
<body dir="rtl" id="tablePage" >
    <form id="form1" runat="server">
        <div id="container">
        <header>
            <!--קישור לדף עצמו כדי להתחיל את המשחק מחדש בלחיצה על הלוגו-->
            <a href="indextest.html">
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
        <div id="gameTablePage">

            <asp:Label ID="headLine" runat="server" Text="המשחקים שלי:"></asp:Label>
              <!--הוספת משחק חדש -->
<%--            <asp:Label ID="addGame" runat="server" Text="הוספת משחק"></asp:Label>--%>
            <asp:Button ID="addGameBTN" CssClass="buttons" runat="server" Text="צור משחק חדש" OnClick="addGame_Click" />

            <br />
            <br />

         

              <!--הגריד -->
            <asp:GridView ID="GridView1" runat="server" Width="850px"   AutoGenerateColumns="False" DataSourceID="XmlDataSource1" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="GridView1_RowCommand">
                <Columns>
                       <asp:TemplateField HeaderText="שם משחק">
                        <ItemTemplate>
                            <asp:Label ID="gameName" runat="server" Text='<%#Server.UrlDecode(XPathBinder.Eval(Container.DataItem, "gameName").ToString())%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="קוד משחק">
                        <ItemTemplate>
                              <asp:Label ID="gameCode" runat="server" Text='<%#XPathBinder.Eval(Container.DataItem, "@GameCode").ToString()%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="עריכה">
                        <ItemTemplate>
                            <asp:ImageButton ID="editBTN"  CssClass="icons" runat="server" ImageUrl="~/editorImages/edit.png" CommandName="editRow" theItemId='<%#XPathBinder.Eval(Container.DataItem, "@GameCode").ToString()%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="מחיקה">
                        <ItemTemplate>
                          <asp:ImageButton ID="delBTN" CssClass="icons" runat="server" ImageUrl="~/editorImages/bin.png" CommandName="deleteRow"  theItemId='<%#XPathBinder.Eval(Container.DataItem, "@GameCode").ToString()%>' Visible="True" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="פרסום">
                        <ItemTemplate>
                            <asp:Panel ID="publishTollTip" CssClass="" runat="server">
                            <asp:CheckBox ID="publishedCB"  enableviewstate="true" runat="server" Checked='<%#Convert.ToBoolean(XPathBinder.Eval(Container.DataItem,"@published"))%>' theItemId='<%#XPathBinder.Eval(Container.DataItem, "@GameCode").ToString()%>' OnCheckedChanged="publishedCB_CheckedChanged" AutoPostBack="True" />
                            <asp:Label ID="toolTipLbl" runat="server" CssClass="tooltiptext" Text=""></asp:Label>
                        </asp:Panel>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#66CCFF" Font-Bold="True" ForeColor="#FFFFFF" />
                <PagerStyle BackColor="#D6DCE5" ForeColor="#000000" HorizontalAlign="Left" />
                <RowStyle BackColor="#D6DCE5" ForeColor="#000000"  />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
            <br />
            <br />
            <asp:Image ID="noGames" runat="server" ImageUrl="~/editorImages/noGames.png" />

              <!-- פאנל עם הרקע האפור שבתוכו יהיו החלונות הקופצים -->
            <asp:Panel ID="grayWindows" CssClass="grayWindows" runat="server">

                <!-- פופ-אפ למחיקה - כאן אפשר להוסיף את הפקדים הרלוונטים -->
                <asp:Panel ID="DeleteConfPopUp" CssClass="PopUp" runat="server">
                    <!-- כפתור יציאה - יש לשים לב שהוא מפנה בלחיצה לאותה פונקציה של הכפתור יציאה השני -->
                    <!-- תווית להצגת הודעה למשתמש -->
                    <asp:Label ID="areUSureLbl" runat="server" Text=""></asp:Label>
                     <!-- כפתור לדוגמה -->
                    <asp:Button ID="dontDel" CssClass="importantBTN" runat="server" Text="ביטול" OnClick="dontDel_Click" />
                    <asp:Button ID="OkBtn" CssClass="buttons" runat="server" OnClick="OkBtn_Click" Text="מחיקה" />
                    <br />
                </asp:Panel>

            </asp:Panel>&nbsp;<asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/tree/XMLFile.xml" XPath="//game"></asp:XmlDataSource>
        </div>
    
    <div class="fotter">
</div>
</div>
</form>
</body>
</html>
