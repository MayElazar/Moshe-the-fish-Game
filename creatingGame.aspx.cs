using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;


public partial class creatingGame : System.Web.UI.Page
{
    // משתנה גלובלי המחזיק את הספרייה בה נשמור את הקבצים
    string imagesLibPath = "uploadedFiles/";

   protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["theCode"] != null)
        {
            showNoAnsLbl();
            howManyRemin();
           
            //כפתור פבלוש לא קליקבילי
            PubBTN.Enabled = false;

            // PubBTN.Visible = false;

            //PubBTN.CssClass = "hidden";
            //סשן עם קוד המשחק
            string theNewcode = Session["theCode"].ToString();
            //מתן פוסטבק לאלמנטים בטבלה (היינו צריכות לבטל בשביל שיהיה אפשר להחליף את התמונה בעריכה ישירות בגריד)
            foreach (GridViewRow SearchRow in GridViewTrueAns.Rows)
            {
                ScriptManager.GetCurrent(this).RegisterPostBackControl(((ImageButton)SearchRow.FindControl("TremoveAns")));
                ScriptManager.GetCurrent(this).RegisterPostBackControl(((ImageButton)SearchRow.FindControl("approveTruePicChange")));
                ScriptManager.GetCurrent(this).RegisterPostBackControl(((ImageButton)SearchRow.FindControl("approveTrueAnsChange")));
                ScriptManager.GetCurrent(this).RegisterPostBackControl(((ImageButton)SearchRow.FindControl("TeditPancilBtn")));
                ScriptManager.GetCurrent(this).RegisterPostBackControl(((ImageButton)SearchRow.FindControl("TbigPicBtn")));
                ScriptManager.GetCurrent(this).RegisterPostBackControl(((ImageButton)SearchRow.FindControl("cancelPicTrueAnsChange")));
                ScriptManager.GetCurrent(this).RegisterPostBackControl(((ImageButton)SearchRow.FindControl("cancelTrueAnsChange")));
                ScriptManager.GetCurrent(this).RegisterPostBackControl(((TextBox)SearchRow.FindControl("changeTrueAns")));


            }


            foreach (GridViewRow SearchRow in GridViewFalseAns.Rows)
            {

                ScriptManager.GetCurrent(this).RegisterPostBackControl(((ImageButton)SearchRow.FindControl("FremoveAns")));
                ScriptManager.GetCurrent(this).RegisterPostBackControl(((ImageButton)SearchRow.FindControl("approveFalsePicChange")));
                ScriptManager.GetCurrent(this).RegisterPostBackControl(((ImageButton)SearchRow.FindControl("approveFalseAnsChange")));
                ScriptManager.GetCurrent(this).RegisterPostBackControl(((ImageButton)SearchRow.FindControl("FeditPancilBtn")));
                ScriptManager.GetCurrent(this).RegisterPostBackControl(((ImageButton)SearchRow.FindControl("cancelPicFalseAnsChange")));
                ScriptManager.GetCurrent(this).RegisterPostBackControl(((ImageButton)SearchRow.FindControl("cancelFalseAnsChange")));

                
                ScriptManager.GetCurrent(this).RegisterPostBackControl(((ImageButton)SearchRow.FindControl("FbigPicBtn")));
            }



            if (Page.IsPostBack == false)
            {
                Result.Text = "false";
                Session["VisTxt"] = Result.Text;
                XmlDocument myXmlTrueAns = XmlDataSourceTrueAns.GetXmlDocument();
                XmlDocument myXmlFalseAns = XmlDataSourceFalseAns.GetXmlDocument();

                XmlDataSourceTrueAns.XPath = "/games/game[@GameCode=" + theNewcode + "]/Answers/answer[@isCorrect='true']";
                XmlDataSourceFalseAns.XPath = "/games/game[@GameCode=" + theNewcode + "]/Answers/answer[@isCorrect='false']";


                //שומר את שם המשחק
                XmlNode nameNode = myXmlTrueAns.SelectSingleNode("//game[@GameCode=" + theNewcode + "]/gameName");
                gameNameTXT.Text = Server.UrlDecode(nameNode.InnerText);
                //בודק את מספר התווים שיש בכל תיבת טקסט
                gameNameLimitChar.Text = Convert.ToString(gameNameTXT.Text.Length) + "/20";

                //שומר את המשוב לשחקן
                XmlNode playerFbNode = myXmlTrueAns.SelectSingleNode("//game[@GameCode=" + theNewcode + "]/FBtoPlayer");
                fbForPlayerLbl.Text = Server.UrlDecode(playerFbNode.InnerText);
                //בודק את מספר התווים שיש בכל תיבת טקסט
                FBKLimitChar.Text = Convert.ToString(fbForPlayerLbl.Text.Length) + "/50";


                //שומר את הנחית המשחק
                XmlNode dirNode = myXmlTrueAns.SelectSingleNode("//game[@GameCode=" + theNewcode + "]/Topic");
                gameDirTXT.Text = Server.UrlDecode(dirNode.InnerText);
                //בודק את מספר התווים שיש בכל תיבת טקסט
                gameDirLimitChar.Text = Convert.ToString(gameDirTXT.Text.Length) + "/25";



               

                changeMinAns();
            }
            if (Session["VisTxt"].ToString() == "false")
            {
                showInInnerGrid();

            }

            if (fbForPlayerLbl.Text.Length > 2)
            {
                FBKLimitChar.CssClass = "countCharGreen";

            }
            else
            {
                FBKLimitChar.CssClass = "countCharRed";

            }

            if (gameNameTXT.Text.Length > 2)
            {
                gameNameLimitChar.CssClass = "countCharGreen";
                pubName.CssClass = "EanoghChar";
                int lendthTxt = gameNameTXT.Text.Length;
                gameNameLimitChar.Text = lendthTxt.ToString() + "/20";
            }
            else
            {
                pubName.CssClass = "notEanoghChar";

                gameNameLimitChar.CssClass = "countCharRed";
                int lendthTxt = gameNameTXT.Text.Length;
                gameNameLimitChar.Text = lendthTxt.ToString() + "/20";

            }

            if (gameDirTXT.Text.Length > 2)
            {
                gameDirLimitChar.CssClass = "countCharGreen";
                pubDir.CssClass = "EanoghChar";
                int lendthTxt = gameDirTXT.Text.Length;
                gameDirLimitChar.Text = lendthTxt.ToString() + "/25";
            }
            else
            {
                pubDir.CssClass = "notEanoghChar";
                gameDirLimitChar.CssClass = "countCharRed";
                int lendthTxt = gameDirTXT.Text.Length;
                gameDirLimitChar.Text = lendthTxt.ToString() + "/20";

            }


            if (addAnsTxt.Text.Length == 0)
            {
                textAnsLimitChar.CssClass = "countCharRed";
            }
            else
            {
                textAnsLimitChar.CssClass = "countCharGreen";

            }

            allowPub();
            //  הסתרת של חלונות הפופ-אפ
            grayWindows.Visible = false;
            wrongFilePanel.CssClass = "hidden";
            DeleteConfPopUp.Visible = false;
            BigImagePopUp.Visible = false;
            cantSaveGrayPanel.Visible = false;
            cantSave.Visible = false;
            panelTxt.Visible = false;
            noSavePanel.Visible = false;
            noContentPanel.Visible = false;
            ONeditPanel.CssClass = "hidden";
            ONeditPanelMsgPanel.CssClass = "hidden";
            bigImgGrayPanel.CssClass = "hidden";
            FBKLimitChar.Visible = false;
           // tooMuch.CssClass = "hidden";

            //שמירה
            XmlDataSourceTrueAns.Save();
            XmlDataSourceFalseAns.Save();
        }


    }

    //פונקציה שמראה את המסיחים בגריד
    protected void showInInnerGrid()
    {
        string theNewcode = Session["theCode"].ToString();

        MAXChartyp.CssClass = "hidden";

        //שיבוץ בגריד לפי נכונות התשובה
        XmlDataSourceTrueAns.XPath = "/games/game[@GameCode=" + theNewcode + "]/Answers/answer[@isCorrect='true']";
        XmlDataSourceFalseAns.XPath = "/games/game[@GameCode=" + theNewcode + "]/Answers/answer[@isCorrect='false']";

        XmlDocument myXmlTrueAns = XmlDataSourceTrueAns.GetXmlDocument();
        XmlDocument myXmlFalseAns = XmlDataSourceFalseAns.GetXmlDocument();

        ////גריד תשובות נכונות
        foreach (GridViewRow everyRow in GridViewTrueAns.Rows)
        {
            string ansId = ((ImageButton)everyRow.FindControl("TeditPancilBtn")).Attributes["theAnsId"].ToString();
            XmlNode ansName = myXmlTrueAns.SelectSingleNode("/games/game[@GameCode='" + theNewcode + "']/Answers/answer[@ansId='" + ansId + "']");

            if (ansName.Attributes["ansType"].Value == "image")
            {
                
               
                ((Label)(everyRow.FindControl("LBL"))).Visible = false;
                ((Image)(everyRow.FindControl("trueImage"))).Visible = true;
                ((ImageButton)(everyRow.FindControl("approveTrueAnsChange"))).Visible = false;
                ((ImageButton)(everyRow.FindControl("cancelTrueAnsChange"))).Visible = false;
                ((ImageButton)(everyRow.FindControl("TeditPancilBtn"))).Visible = false;
                ((ImageButton)(everyRow.FindControl("editTruePic"))).CssClass = "";
                ((ImageButton)(everyRow.FindControl("approveTruePicChange"))).CssClass = "hidden";
                ((Image)(everyRow.FindControl("imagetest"))).CssClass = "hidden";
                ((Label)(everyRow.FindControl("showAsnlbl"))).Visible = false;
                ((ImageButton)(everyRow.FindControl("cancelPicTrueAnsChange"))).CssClass = "hidden";
                ((TextBox)(everyRow.FindControl("changeTrueAns"))).Visible = false;


            }
            else
            {
               
                ((Label)(everyRow.FindControl("LBL"))).Visible = false;
                ((Label)(everyRow.FindControl("showAsnlbl"))).Visible = true;
                ((ImageButton)(everyRow.FindControl("TbigPicBtn"))).Visible = false;
                ((ImageButton)(everyRow.FindControl("approveTrueAnsChange"))).Visible = false;
                ((ImageButton)(everyRow.FindControl("cancelTrueAnsChange"))).Visible = false;
                ((ImageButton)(everyRow.FindControl("TeditPancilBtn"))).Visible = true;
                ((ImageButton)(everyRow.FindControl("editTruePic"))).Visible = false;
                ((ImageButton)(everyRow.FindControl("approveTruePicChange"))).CssClass = "hidden";
                ((Image)(everyRow.FindControl("imagetest"))).CssClass = "hidden";
                ((Image)(everyRow.FindControl("trueImage"))).Visible = false;
                ((ImageButton)(everyRow.FindControl("cancelPicTrueAnsChange"))).Visible = false;
                ((TextBox)(everyRow.FindControl("changeTrueAns"))).Visible = false;


            }

        }
        // גריד תשובות לא נכונות

        foreach (GridViewRow everyRow in GridViewFalseAns.Rows)
        {
            string ansId = ((ImageButton)everyRow.FindControl("FremoveAns")).Attributes["theAnsId"].ToString();
            XmlNode ansName = myXmlFalseAns.SelectSingleNode("/games/game[@GameCode='" + theNewcode + "']/Answers/answer[@ansId='" + ansId + "']");

            if (ansName.Attributes["ansType"].Value == "image")
            {
               
                ((Label)(everyRow.FindControl("LBL"))).Visible = false;
                ((Image)(everyRow.FindControl("falseImage"))).CssClass = "";
                ((ImageButton)(everyRow.FindControl("approveFalseAnsChange"))).Visible = false;
                ((ImageButton)(everyRow.FindControl("cancelFalseAnsChange"))).Visible = false;
                ((ImageButton)(everyRow.FindControl("FeditPancilBtn"))).Visible = false;
                ((ImageButton)(everyRow.FindControl("editFalsePic"))).Visible = true;
                ((ImageButton)(everyRow.FindControl("approveFalsePicChange"))).CssClass = "hidden";
                ((Label)(everyRow.FindControl("FshowAsnlbl"))).Visible = false;
                ((Image)(everyRow.FindControl("imageFalseTest"))).CssClass = "hidden";
                ((ImageButton)(everyRow.FindControl("cancelPicFalseAnsChange"))).CssClass = "hidden";


            }
            else
            {
               
                ((Label)(everyRow.FindControl("LBL"))).Visible = false;
                ((Label)(everyRow.FindControl("FshowAsnlbl"))).Visible = true;
                ((ImageButton)(everyRow.FindControl("FbigPicBtn"))).Visible = false;
                ((ImageButton)(everyRow.FindControl("editFalsePic"))).Visible = false;
                ((ImageButton)(everyRow.FindControl("FeditPancilBtn"))).Visible = true;
                ((ImageButton)(everyRow.FindControl("approveFalsePicChange"))).Visible = false;
                ((Image)(everyRow.FindControl("falseImage"))).Visible = false;
                ((Image)(everyRow.FindControl("imageFalseTest"))).Visible = false;
                ((ImageButton)(everyRow.FindControl("cancelPicFalseAnsChange"))).Visible = false;



            }

        }
        showNoAnsLbl();

    }

    //כפתור שמירת משחק
    protected void saveBTN_Click(object sender, EventArgs e)
    {
        if (gameNameTXT.Text == "")
        {
            cantSaveGrayPanel.Visible = true;
            cantSave.Visible = true;
            ((Label)FindControl("cantSavelbl")).Text = "עליך להזין שם למשחק על מנת לשמור אותו";
        }
        else
        {
            saveFunc();
        }

        if (gameDirTXT.Text == "")
        {
            cantSaveGrayPanel.Visible = true;
            cantSave.Visible = true;
            ((Label)FindControl("cantSavelbl")).Text = "עליך להזין הנחייה למשחק על מנת לשמור אותו";
        }
        else
        {
            saveFunc();

        }

        if (gameDirTXT.Text == "" && gameNameTXT.Text == "")
        {
            cantSaveGrayPanel.Visible = true;
            cantSave.Visible = true;
            ((Label)FindControl("cantSavelbl")).Text = "עליך להזין הנחייה ושם למשחק על מנת לשמור אותו";
        }
        else
        {
            saveFunc();

        }

    }

    //מסיחים לא נכונים- עריכה, מחיקה והגדלת תשובות
    protected void GridViewFalseAns_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string theNewcode = Session["theCode"].ToString();


        ImageButton i = (ImageButton)e.CommandSource; //הכנסת האימג' בטון לתוך משתנה ובירור על מי לחצנו

        // אנו מושכים את האי די של הפריט באמצעות מאפיין לא שמור במערכת שהוספנו באופן ידני לכפתור-תמונה
        string theId = i.Attributes["theAnsId"];

        //מכניסים את האי די לסשן כדי שנוכל לפות אליו בפונקציה אחרת ובמעבר עמודים
        Session["answerCode"] = i.Attributes["theAnsId"];
        // string answerID = Session["answerCode"].ToString();

        Session["itemIdSession"] = i.Attributes["theItemId"];

        // אנו מושכים את האי די של הפריט באמצעות מאפיין לא שמור במערכת שהוספנו באופן ידני לכפתור-תמונה
        string itemRow = i.Attributes["theItemRow"];
        Session["theItemRow"] = i.Attributes["theItemRow"];

        XmlDocument myXmlFalseAns = XmlDataSourceFalseAns.GetXmlDocument();

        //  string theNewcode = Session["theCode"].ToString();
        XmlDataSourceFalseAns.XPath = "/games/game[@GameCode=" + theNewcode + "]/Answers/answer[@isCorrect='false']";

        XmlNode ansTypeNode = myXmlFalseAns.SelectSingleNode("//game[@GameCode=" + theNewcode + "]/Answers/answer[@ansId='" + theId + "']");
        //  הפניה לפוקנצית מחיקה או שינוי נראות תיבת הטקסט לנראה על אפשרות לשינוי, העלמת הלייבל, יצירת כפתור וי שישמור את התשובה

        // עלינו לברר איזו פקודה צריכה להתבצע - הפקודה רשומה בכל כפתור             
        switch (e.CommandName)
        {
            //אם נלחץ על כפתור מחיקה יקרא לפונקציה של מחיקה                    
            case "deleteRow":
                delFunc();

                break;

            //אם נלחץ על כפתור עריכה (העפרון) נעבור לדף עריכה                    
            case "editRow":
                changeFalseTextAns();
                break;

            case "largeImg":
                bigPicFunc();
                break;

            case "cancelChangeFalse":

                cancelChangeFalseFunc();
                break;

            case "approveChangeFalse":

                approveChangeFalseFunc();
                break;

            case "cancelEditPic":
                cancelEditPicFunc();
                break;
        }
        XmlDataSourceFalseAns.Save();

    }

    // עריכת מסיח טקסט לא נכון
    protected void changeFalseTextAns()
    {
        int itemRowToEdit = Convert.ToInt16(Session["theItemRow"]);

        if (Session["VisTxt"].ToString() == "true")
        {

            ONeditPanel.CssClass = "grayWindows";
            ONeditPanelMsgPanel.CssClass = "PopUp";

            foreach (GridViewRow RowF in GridViewFalseAns.Rows)
            {

                ((Image)(RowF.FindControl("imageFalseTest"))).Visible = false;

            }
            foreach (GridViewRow RowT in GridViewTrueAns.Rows)
            {

                ((Image)(RowT.FindControl("imagetest"))).Visible = false;

            }


        }
        else
        {

            // שמירה בסשן את השורה שאותה נראה לערוך

            string itemContant = ((Label)GridViewFalseAns.Rows[itemRowToEdit].FindControl("FshowAsnlbl")).Text;

            ((TextBox)GridViewFalseAns.Rows[itemRowToEdit].FindControl("changeFalseAns")).Visible = true;
            ((Label)GridViewFalseAns.Rows[itemRowToEdit].FindControl("FshowAsnlbl")).Visible = false;
            ((TextBox)GridViewFalseAns.Rows[itemRowToEdit].FindControl("changeFalseAns")).Text = itemContant;
            ((Label)GridViewFalseAns.Rows[itemRowToEdit].FindControl("LBL")).Visible = true;
            ((ImageButton)GridViewFalseAns.Rows[itemRowToEdit].FindControl("FeditPancilBtn")).Visible = false;
            ((ImageButton)GridViewFalseAns.Rows[itemRowToEdit].FindControl("approveFalseAnsChange")).Visible = true;
            ((ImageButton)GridViewFalseAns.Rows[itemRowToEdit].FindControl("cancelFalseAnsChange")).Visible = true;

            //סשן המצביע לתא מסוים הגריד
            string answerID = Session["answerCode"].ToString();
            string theNewcode = Session["theCode"].ToString();
            XmlDocument myXmlFalseAns = XmlDataSourceFalseAns.GetXmlDocument();

            XmlNode FalseansNode = myXmlFalseAns.SelectSingleNode("//game[@GameCode=" + theNewcode + "]/Answers/answer[@ansId='" + answerID + "']");

            Result.Text = "true";
            Session["VisTxt"] = Result.Text;
        }
    }

    //ביטול עריכת מסיח לא נכון
    protected void cancelChangeFalseFunc()
    {
        Result.Text = "false";
        Session["VisTxt"] = Result.Text;
        int itemRowToEdit = Convert.ToInt16(Session["theItemRow"]);

        ((TextBox)GridViewFalseAns.Rows[itemRowToEdit].FindControl("changeFalseAns")).Visible = false;
        ((Label)GridViewFalseAns.Rows[itemRowToEdit].FindControl("FshowAsnlbl")).Visible = true;
        ((TextBox)GridViewFalseAns.Rows[itemRowToEdit].FindControl("changeFalseAns")).Text = "";

        ((ImageButton)GridViewFalseAns.Rows[itemRowToEdit].FindControl("FeditPancilBtn")).Visible = true;
        ((ImageButton)GridViewFalseAns.Rows[itemRowToEdit].FindControl("approveFalseAnsChange")).Visible = false;
        ((ImageButton)GridViewFalseAns.Rows[itemRowToEdit].FindControl("cancelFalseAnsChange")).Visible = false;

        showInInnerGrid();
    }

    // אישור עריכת מסיח לא נכון
    protected void approveChangeFalseFunc()
    {
        Result.Text = "false";
        Session["VisTxt"] = Result.Text;

        int itemRowToEdit = Convert.ToInt16(Session["theItemRow"]);
        string theNewcode = Session["theCode"].ToString();
        string answerID = Session["answerCode"].ToString();

        XmlDocument myXmlFalseAns = XmlDataSourceFalseAns.GetXmlDocument();
        XmlDataSourceFalseAns.XPath = "/games/game[@GameCode=" + theNewcode + "]/Answers/answer[@isCorrect='false']";

        XmlNode ansTypeNode = myXmlFalseAns.SelectSingleNode("//game[@GameCode=" + theNewcode + "]/Answers/answer[@ansId='" + answerID + "']");
        if (ansTypeNode.Attributes["ansType"].Value == "text")
        {
            string theNewAnsText = Server.UrlDecode(((TextBox)GridViewFalseAns.Rows[itemRowToEdit].FindControl("changeFalseAns")).Text);
            ((Label)GridViewFalseAns.Rows[itemRowToEdit].FindControl("FshowAsnlbl")).Text = Server.UrlDecode(theNewAnsText);

            ansTypeNode.InnerText = theNewAnsText;
        }
        else
        {
            //יצירה של הקוד לתשובה
            string fileType = ((FileUpload)FindControl("FileUpload3")).PostedFile.ContentType;

            if (fileType.Contains("image")) //בדיקה האם הקובץ שהוכנס הוא תמונה
            {
                // הנתיב המלא של הקובץ עם שמו האמיתי של הקובץ
                string fileName = ((FileUpload)FindControl("FileUpload3")).PostedFile.FileName;
                // הסיומת של הקובץ
                string endOfFileName = fileName.Substring(fileName.LastIndexOf("."));
                //לקיחת הזמן האמיתי למניעת כפילות בתמונות
                string myTime = DateTime.Now.ToString("dd_MM_yy-HH_mm_ss");

                // חיבור השם החדש עם הסיומת של הקובץ
                string imageNewName = myTime + endOfFileName;

                // Bitmap המרת הקובץ שיתקבל למשתנה מסוג
                System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(FileUpload3.PostedFile.InputStream);

                //קריאה לפונקציה המקטינה את התמונה
                //אנו שולחים לה את התמונה שלנו בגירסאת הביטמאפ ואת האורך והרוחב שאנו רוצים לתמונה החדשה
                System.Drawing.Image objImage = FixedSize(bmpPostedImage, 300, 300);

                //שמירה של הקובץ לספרייה בשם החדש שלו
                objImage.Save(Server.MapPath(imagesLibPath) + imageNewName);

                ansTypeNode.InnerText = imageNewName;
            }
            
        }
        XmlDataSourceFalseAns.Save();
        GridViewFalseAns.DataBind();
        showInInnerGrid();
        cancelChangeFalseFunc();
    }

    //מסיחים  נכונים- עריכה, מחיקה והגדלת תשובות
    protected void GridViewTrueAns_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string theNewcode = Session["theCode"].ToString();


        ImageButton i = (ImageButton)e.CommandSource; //הכנסת האימג' בטון לתוך משתנה ובירור על מי לחצנו

        // אנו מושכים את האי די של הפריט באמצעות מאפיין לא שמור במערכת שהוספנו באופן ידני לכפתור-תמונה
        string theId = i.Attributes["theAnsId"];

        //מכניסים את האי די לסשן כדי שנוכל לפות אליו בפונקציה אחרת ובמעבר עמודים
        Session["answerCode"] = i.Attributes["theAnsId"];
        string answerID = Session["answerCode"].ToString();

        Session["itemIdSession"] = i.Attributes["theItemId"];

        // אנו מושכים את האי די של הפריט באמצעות מאפיין לא שמור במערכת שהוספנו באופן ידני לכפתור-תמונה
        string itemRow = i.Attributes["theItemRow"];
        Session["theItemRow"] = itemRow;


        XmlDocument myXmlTrueAns = XmlDataSourceTrueAns.GetXmlDocument();
        XmlDataSourceTrueAns.XPath = "/games/game[@GameCode=" + theNewcode + "]/Answers/answer[@isCorrect='true']";


        XmlNode ansTypeNode = myXmlTrueAns.SelectSingleNode("//game[@GameCode=" + theNewcode + "]/Answers/answer[@ansId='" + answerID + "']");


        // עלינו לברר איזו פקודה צריכה להתבצע - הפקודה רשומה בכל כפתור             
        switch (e.CommandName)
        {
            //אם נלחץ על כפתור מחיקה יקרא לפונקציה של מחיקה                    
            case "deleteRow":
                delFunc();


                break;

            //אם נלחץ על כפתור עריכה (העפרון) נעבור לדף עריכה                    
            case "editRow":

                changeTextAns();

                break;

            case "largeImg":
                bigPicFunc();
                break;

            case "cancelEdit":
                cancelEditFunc();
                break;

            case "approveEdit":
                approveEditFunc();
                break;
            case "cancelEditPic":
                cancelEditPicFunc();
                break;

        }
        XmlDataSourceTrueAns.Save();

    }

    //ביטול עריכת תמונה
    protected void cancelEditPicFunc()
    {
        showInInnerGrid();
    }

    //עריכת מסיח טקסט נכון
    protected void changeTextAns()
    {
        

        int itemRowToEdit = Convert.ToInt16(Session["theItemRow"]);

        Session["VisTxt"] = Result.Text;
        if (Session["VisTxt"].ToString()=="true") {

            ONeditPanel.CssClass = "grayWindows";
            ONeditPanelMsgPanel.CssClass = "PopUp";

            foreach (GridViewRow RowF in GridViewFalseAns.Rows)
            {
                ((Image)(RowF.FindControl("imageFalseTest"))).Visible = false;
               

            }
            foreach (GridViewRow RowT in GridViewTrueAns.Rows)
            {
                ((Image)(RowT.FindControl("imagetest"))).Visible = false;
               

            }



        }
        else
        {
       


             // שמירה בסשן את השורה שאותה נראה לערוך
       
             string itemContant = ((Label)GridViewTrueAns.Rows[itemRowToEdit].FindControl("showAsnlbl")).Text;

            ((TextBox) GridViewTrueAns.Rows[itemRowToEdit].FindControl("changeTrueAns")).Visible = true;
            ((Label)GridViewTrueAns.Rows[itemRowToEdit].FindControl("LBL")).Visible = true;

            ((Label) GridViewTrueAns.Rows[itemRowToEdit].FindControl("showAsnlbl")).Visible = false;
           ((TextBox) GridViewTrueAns.Rows[itemRowToEdit].FindControl("changeTrueAns")).Text = itemContant;

           ((ImageButton) GridViewTrueAns.Rows[itemRowToEdit].FindControl("TeditPancilBtn")).Visible = false;
           ((ImageButton) GridViewTrueAns.Rows[itemRowToEdit].FindControl("approveTrueAnsChange")).Visible = true;
           ((ImageButton) GridViewTrueAns.Rows[itemRowToEdit].FindControl("cancelTrueAnsChange")).Visible = true;

            //סשן המצביע לתא מסוים הגריד
           string answerID = Session["answerCode"].ToString();
           string theNewcode = Session["theCode"].ToString();
            XmlDocument myXmlTrueAns = XmlDataSourceTrueAns.GetXmlDocument();

            XmlNode ansNode = myXmlTrueAns.SelectSingleNode("//game[@GameCode=" + theNewcode + "]/Answers/answer[@ansId='" + answerID + "']");

            Result.Text = "true";
            Session["VisTxt"]= Result.Text;
        }
    }

    //ביטול עריכה מסיח נכון
    protected void cancelEditFunc()
    {
        Result.Text = "false";
        Session["VisTxt"] = Result.Text;
        int itemRowToEdit = Convert.ToInt16(Session["theItemRow"]);

        ((TextBox)GridViewTrueAns.Rows[itemRowToEdit].FindControl("changeTrueAns")).Visible = false;
        ((Label)GridViewTrueAns.Rows[itemRowToEdit].FindControl("showAsnlbl")).Visible = true;
        ((TextBox)GridViewTrueAns.Rows[itemRowToEdit].FindControl("changeTrueAns")).Text = "";

        ((ImageButton)GridViewTrueAns.Rows[itemRowToEdit].FindControl("TeditPancilBtn")).Visible = true;
        ((ImageButton)GridViewTrueAns.Rows[itemRowToEdit].FindControl("approveTrueAnsChange")).Visible = false;
        ((ImageButton)GridViewTrueAns.Rows[itemRowToEdit].FindControl("cancelTrueAnsChange")).Visible = false;

        showInInnerGrid();
    }

    //אישור עריכה מסיח נכון
    protected void approveEditFunc()
    {
        Result.Text = "false";
        Session["VisTxt"] = Result.Text;

        int itemRowToEdit = Convert.ToInt16(Session["theItemRow"]);
        string theNewcode = Session["theCode"].ToString();
        string answerID = Session["answerCode"].ToString();

        XmlDocument myXmlTrueAns = XmlDataSourceTrueAns.GetXmlDocument();
        XmlDataSourceTrueAns.XPath = "/games/game[@GameCode=" + theNewcode + "]/Answers/answer[@isCorrect='true']";


        XmlNode ansNode = myXmlTrueAns.SelectSingleNode("//game[@GameCode=" + theNewcode + "]/Answers/answer[@ansId='" + answerID + "']");
        if (ansNode.Attributes["ansType"].Value == "text")
        {
            string theNewAnsText = Server.UrlDecode(((TextBox)GridViewTrueAns.Rows[itemRowToEdit].FindControl("changeTrueAns")).Text);
            ((Label)GridViewTrueAns.Rows[itemRowToEdit].FindControl("showAsnlbl")).Text = Server.UrlDecode(theNewAnsText);

            ansNode.InnerText = theNewAnsText;
        }
        else
        {
            //יצירה של הקוד לתשובה
            string fileType = ((FileUpload)FindControl("FileUpload2")).PostedFile.ContentType;

            if (fileType.Contains("image")) //בדיקה האם הקובץ שהוכנס הוא תמונה
            {
                // הנתיב המלא של הקובץ עם שמו האמיתי של הקובץ
                string fileName = ((FileUpload)FindControl("FileUpload2")).PostedFile.FileName;
                // הסיומת של הקובץ
                string endOfFileName = fileName.Substring(fileName.LastIndexOf("."));
                //לקיחת הזמן האמיתי למניעת כפילות בתמונות
                string myTime = DateTime.Now.ToString("dd_MM_yy-HH_mm_ss");

                // חיבור השם החדש עם הסיומת של הקובץ
                string imageNewName = myTime + endOfFileName;

                // Bitmap המרת הקובץ שיתקבל למשתנה מסוג
                System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(FileUpload2.PostedFile.InputStream);

                //קריאה לפונקציה המקטינה את התמונה
                //אנו שולחים לה את התמונה שלנו בגירסאת הביטמאפ ואת האורך והרוחב שאנו רוצים לתמונה החדשה
                System.Drawing.Image objImage = FixedSize(bmpPostedImage, 300, 300);

                //שמירה של הקובץ לספרייה בשם החדש שלו
                objImage.Save(Server.MapPath(imagesLibPath) + imageNewName);

                ansNode.InnerText = imageNewName;
            }
            
            }
            
            XmlDataSourceTrueAns.Save();
            GridViewTrueAns.DataBind();
            showInInnerGrid();
            cancelEditFunc();
        
    }

    //כפתור יציאה מפופ אפ הגדלת תמונה
    protected void ExitImageButton_Click(object sender, ImageClickEventArgs e)
    {
        cantSaveGrayPanel.Visible = false;
        cantSave.Visible = false;
        bigImgGrayPanel.CssClass = "hidden";
        BigImagePopUp.Visible = false;
        cantSaveGrayPanel.Visible = false;
        cantSave.Visible = false;
        ONeditPanelMsgPanel.CssClass = "hidden";
        ONeditPanel.CssClass = "hidden"; ;
        showInInnerGrid();
    }

    // פונקציה ליציאהה מהפופ אפ של לחיצה בעת עריכה
    protected void ExitImageButtonPopUpEditing_Click(object sender, ImageClickEventArgs e)
    {
        cantSaveGrayPanel.Visible = false;
        cantSave.Visible = false;
        bigImgGrayPanel.CssClass = "hidden";
        BigImagePopUp.Visible = false;
        cantSaveGrayPanel.Visible = false;
        cantSave.Visible = false;
        ONeditPanel.CssClass = "hidden";
        ONeditPanelMsgPanel.CssClass = "hidden"; ;

        foreach (GridViewRow RowF in GridViewFalseAns.Rows)
        {

            ((Image)(RowF.FindControl("imageFalseTest"))).Visible = false;

        }
        foreach (GridViewRow RowT in GridViewTrueAns.Rows)
        {

            ((Image)(RowT.FindControl("imagetest"))).Visible = false;

        }
        //showInInnerGrid();
    }

    //כפתור לפתיחת פאנל טקסט
    protected void addTxtBTN_Click(object sender, ImageClickEventArgs e)
    {
        //כפתור הוספת תמונה לא פעיל
        addPicBTN.Enabled = false;
        addTxtBTN.Enabled = false;

        //פתיחת הפאנל
        panelTxt.Visible = true;

       
    }

    //כפתור להוספת מסיח תמונה
    protected void addPicAnsBTN_Click(object sender, EventArgs e)
    {
        string theNewcode = Session["theCode"].ToString();

        //שיבוץ בגריד לפי נכונות התשובה
        XmlDataSourceTrueAns.XPath = "/games/game[@GameCode=" + theNewcode + "]/Answers/answer[@isCorrect='true']";
        XmlDataSourceFalseAns.XPath = "/games/game[@GameCode=" + theNewcode + "]/Answers/answer[@isCorrect='false']";

        XmlDocument myXmlTrueAns = XmlDataSourceTrueAns.GetXmlDocument();
        XmlDocument myXmlFalseAns = XmlDataSourceFalseAns.GetXmlDocument();



        //יצירה של הקוד לתשובה
        string fileType = ((FileUpload)FindControl("FileUpload1")).PostedFile.ContentType;

        if (fileType.Contains("image")) //בדיקה האם הקובץ שהוכנס הוא תמונה
        {
            // הנתיב המלא של הקובץ עם שמו האמיתי של הקובץ
            string fileName = ((FileUpload)FindControl("FileUpload1")).PostedFile.FileName;
            // הסיומת של הקובץ
            string endOfFileName = fileName.Substring(fileName.LastIndexOf("."));
            //לקיחת הזמן האמיתי למניעת כפילות בתמונות
            string myTime = DateTime.Now.ToString("dd_MM_yy-HH_mm_ss");

            // חיבור השם החדש עם הסיומת של הקובץ
            string imageNewName = myTime + endOfFileName;

            // Bitmap המרת הקובץ שיתקבל למשתנה מסוג
            System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(FileUpload1.PostedFile.InputStream);

            //קריאה לפונקציה המקטינה את התמונה
            //אנו שולחים לה את התמונה שלנו בגירסאת הביטמאפ ואת האורך והרוחב שאנו רוצים לתמונה החדשה
            System.Drawing.Image objImage = FixedSize(bmpPostedImage, 300, 300);

            //שמירה של הקובץ לספרייה בשם החדש שלו
            objImage.Save(Server.MapPath(imagesLibPath) + imageNewName);

            // משתנה למזהה מסיח
            XmlNode myAnsCounter = myXmlTrueAns.SelectSingleNode("games/game[@GameCode = '" + theNewcode + "']/answerCounter");
            string ansCounter = myAnsCounter.InnerText;
            int latestAnsID = Convert.ToInt16(ansCounter);
            Session["ansID"] = latestAnsID;
            latestAnsID++;
            myAnsCounter.InnerText = latestAnsID.ToString();


            //בדיקה האם המסיח נכון או לא 
            string ifAnsCorrect;

            if (((RadioButtonList)FindControl("PicIfCorrectCBK")).SelectedValue == 0.ToString())
            {
                ifAnsCorrect = "true";

                //משתנה להחזקת ענף עוטף למסיחים
                XmlNode allAns = myXmlTrueAns.SelectSingleNode("//Answers");

                //יצירת תשובה
                XmlElement newPicAns = myXmlTrueAns.CreateElement("answer");

            

                newPicAns.SetAttribute("ansId", latestAnsID.ToString());
                newPicAns.SetAttribute("isCorrect", ifAnsCorrect);
                newPicAns.SetAttribute("ansType", "image");
                newPicAns.InnerText = imageNewName;

                //צירוף התשובה לענף התשובות
                allAns.AppendChild(newPicAns);
                XmlNode FirstAns = myXmlTrueAns.SelectNodes("games/game[@GameCode = '" + theNewcode + "']/Answers/answer").Item(0);                myXmlTrueAns.SelectSingleNode("games/game[@GameCode = '" + theNewcode + "']/Answers").InsertBefore(newPicAns, FirstAns);

                //שיבוץ בגריד לפי נכונות התשובה


                XmlDataSourceTrueAns.Save();                GridViewTrueAns.DataBind();
                //הצגה של הקובץ החדש מהספרייה
                showInInnerGrid();
                //בדיקה האם יש לפחות 20 מסיחים- לשינוי צבע הלייבל בתנאי פרסום
                changeMinAns();
                addTxtBTN.Enabled = true;
                PicIfCorrectCBK.ClearSelection();
                showNoAnsLbl();

            }
            else
            {
                ifAnsCorrect = "false";
                //משתנה להחזקת ענף עוטף למסיחים
                XmlNode allAns = myXmlFalseAns.SelectSingleNode("//Answers");

                //יצירת תשובה
                XmlElement newPicAns = myXmlFalseAns.CreateElement("answer");

                newPicAns.SetAttribute("ansId", latestAnsID.ToString());
                newPicAns.SetAttribute("isCorrect", ifAnsCorrect);
                newPicAns.SetAttribute("ansType", "image");
                newPicAns.InnerText = imageNewName;

                //צירוף התשובה לענף התשובות
                allAns.AppendChild(newPicAns);

                XmlNode FirstAns = myXmlFalseAns.SelectNodes("games/game[@GameCode = '" + theNewcode + "']/Answers/answer").Item(0);                myXmlFalseAns.SelectSingleNode("games / game[@GameCode = '" + theNewcode + "'] / Answers").InsertBefore(newPicAns, FirstAns);
                //שיבוץ בגריד לפי נכונות התשובה


                XmlDataSourceFalseAns.Save();
                GridViewFalseAns.DataBind();
                //הצגה של הקובץ החדש מהספרייה
                showInInnerGrid();
                howManyRemin();
                addTxtBTN.Enabled = true;

                //בדיקה האם יש לפחות 20 מסיחים- לשינוי צבע הלייבל בתנאי פרסום
                changeMinAns();
                PicIfCorrectCBK.ClearSelection();
                showNoAnsLbl();
            }
        }
            else
            {
            addPicBTN.Enabled = true;
            addTxtBTN.Enabled = true;
            PicIfCorrectCBK.ClearSelection();
        }

    }

    // פונקציה המקבלת את התמונה שהועלתה , האורך והרוחב שאנו רוצים לתמונה ומחזירה את התמונה המוקטנת
    static System.Drawing.Image FixedSize(System.Drawing.Image imgPhoto, int Width, int Height)
    {
        int sourceWidth = Convert.ToInt32(imgPhoto.Width);
        int sourceHeight = Convert.ToInt32(imgPhoto.Height);

        int sourceX = 0;
        int sourceY = 0;
        int destX = 0;
        int destY = 0;

        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;

        nPercentW =  ((float)Width / (float)sourceWidth);
        nPercentH = ((float)Height / (float)sourceHeight);
        if (nPercentH < nPercentW)
        {
            nPercent = nPercentH;
            destX = System.Convert.ToInt16((Width -
                          (sourceWidth * nPercent))/2 );
        }
        else
        {
            nPercent = nPercentW;
            destY = System.Convert.ToInt16((Height -
                          (sourceHeight * nPercent))/2 );
        }

        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);

        System.Drawing.Bitmap bmPhoto = new System.Drawing.Bitmap(Width, Height,
                          System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                         imgPhoto.VerticalResolution);

        System.Drawing.Graphics grPhoto = System.Drawing.Graphics.FromImage(bmPhoto);
        grPhoto.Clear(System.Drawing.Color.White);
        grPhoto.InterpolationMode =
                System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

        grPhoto.DrawImage(imgPhoto,
            new System.Drawing.Rectangle(destX, destY, destWidth, destHeight),
            new System.Drawing.Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
            System.Drawing.GraphicsUnit.Pixel);

        grPhoto.Dispose();
        return bmPhoto;
    }

    //הגדלת תמונה
    protected void bigPicFunc()
    {
        bigImgGrayPanel.CssClass= "grayWindows";
        BigImagePopUp.Visible = true;

        //סשן עם קוד המשחק
        string theNewcode = Session["theCode"].ToString();

        //סשן עם קוד מסיח
        string answerID = Session["answerCode"].ToString();

        XmlDocument myXmlTrueAns = XmlDataSourceTrueAns.GetXmlDocument();
        XmlDocument myXmlFalseAns = XmlDataSourceFalseAns.GetXmlDocument();

        XmlNode trueImageToShow = myXmlTrueAns.SelectSingleNode("//game[@GameCode=" + theNewcode + "]/Answers/answer[@ansId=" + answerID + "]");
        XmlNode falseImageToShow = myXmlFalseAns.SelectSingleNode("//game[@GameCode=" + theNewcode + "]/Answers/answer[@ansId=" + answerID + "]");

        if (trueImageToShow.Attributes["isCorrect"].Value == "true")
        {
            string imgToShoeName = trueImageToShow.InnerText;
            showBigImage.ImageUrl = "uploadedFiles/" + imgToShoeName;

        }

        if (falseImageToShow.Attributes["isCorrect"].Value == "false")
        {
            string imgToShoeName = falseImageToShow.InnerText;
            showBigImage.ImageUrl = "uploadedFiles/" + imgToShoeName;

        }
    }

    //מחיקת מסיח
    protected void delFunc()
    {
        //הצגה של המסך האפור
        grayWindows.Visible = true;
        //הצגת הפופ-אפ של המחיקה
        DeleteConfPopUp.Visible = true;

        string theNewcode = Session["theCode"].ToString();

        //סשן עם קוד מסיח
        string answerID = Session["answerCode"].ToString();

        //שיבוץ בגריד לפי נכונות התשובה
        //XmlDataSourceTrueAns.XPath = "/games/game[@GameCode=" + theNewcode + "]/Answers/answer[@isCorrect='true']";
        //XmlDataSourceFalseAns.XPath = "/games/game[@GameCode=" + theNewcode + "]/Answers/answer[@isCorrect='false']";

        XmlDocument myXmlTrueAns = XmlDataSourceTrueAns.GetXmlDocument();
        XmlDocument myXmlFalseAns = XmlDataSourceFalseAns.GetXmlDocument();

        XmlNode falseAnsToDel = myXmlFalseAns.SelectSingleNode("/games/game[@GameCode='" + theNewcode + "']/Answers/answer[@ansId='"+ answerID + "']");

        if(falseAnsToDel.Attributes["isCorrect"].Value == "false") { 
        if (falseAnsToDel.Attributes["ansType"].Value== "text")
        {
                ((Panel)FindControl("DeleteConfPopUp")).CssClass = "PopUp";
         ((Image)FindControl("picToDel")).Visible = false;
         ((Label)FindControl("confirmDelLbl")).Text = Server.UrlDecode("ברצונך למחוק את התשובה " + '"' + falseAnsToDel.InnerText + '"' + " לצמיתות?");
        }
        else
            {
                ((Panel)FindControl("DeleteConfPopUp")).CssClass = "PopUpImg";

                ((Label)FindControl("confirmDelLbl")).Text = Server.UrlDecode("ברצונך למחוק את");
                ((Image)FindControl("picToDel")).Visible = true;
                string imgToDelName = falseAnsToDel.InnerText;
            picToDel.ImageUrl = "uploadedFiles/" + imgToDelName;
        }
        }

        XmlNode trueAnsToDel = myXmlTrueAns.SelectSingleNode("/games/game[@GameCode='" + theNewcode + "']/Answers/answer[@ansId='" + answerID + "']");
        if (trueAnsToDel.Attributes["isCorrect"].Value == "true")
        {
            if (trueAnsToDel.Attributes["ansType"].Value == "text")
            {
                ((Panel)FindControl("DeleteConfPopUp")).CssClass = "PopUp";

                ((Image)FindControl("picToDel")).Visible = false;
                ((Label)FindControl("confirmDelLbl")).Text = Server.UrlDecode("ברצונך למחוק את התשובה " + '"' + trueAnsToDel.InnerText + '"' + " לצמיתות?");
            }
            else
            {
                ((Panel)FindControl("DeleteConfPopUp")).CssClass = "PopUpImg";

                ((Label)FindControl("confirmDelLbl")).Text = Server.UrlDecode("ברצונך למחוק את");
                ((Image)FindControl("picToDel")).Visible = true;

                string imgToDelName = trueAnsToDel.InnerText;
                picToDel.ImageUrl = "uploadedFiles/" + imgToDelName;
            }
        }
       
    }

    // כפתור אישור מחיקת מסיח
    protected void OkBtn_Click(object sender, EventArgs e)
    {
        //להבין איך  אני יכולה להפנות לשני דאטה סורס (תשובות נוכונות ותשובות לא נכונות). 
        //להבין איך להפנות ל- איי דיי של מסיח ולא למשחק מסוים
        string theNewcode = Session["theCode"].ToString();
        string answerID = Session["answerCode"].ToString();

        //לקיחת הקובץ 
        XmlDocument trueAnsDocument = XmlDataSourceTrueAns.GetXmlDocument();
        XmlDocument falseAnsDocument = XmlDataSourceFalseAns.GetXmlDocument();

        //זיהוי איזה ענף להוריד

        XmlNode trueansToDel = trueAnsDocument.SelectSingleNode("//game[@GameCode=" + theNewcode + "]/Answers/answer[@ansId=" + answerID + "]");
        XmlNode falseAnsToDel = falseAnsDocument.SelectSingleNode("//game[@GameCode=" + theNewcode + "]/Answers/answer[@ansId=" + answerID + "]");

        if (trueansToDel.Attributes["isCorrect"].Value == "true")
        {

            
            XmlNode trueAnstoremove = trueAnsDocument.SelectSingleNode("/games/game[@GameCode='" + theNewcode + "']/Answers/answer[@ansId='" + answerID + "']");


            //פקודת המחיקה
            //trueAnstoremove.RemoveChild(trueAnstoremove);
            trueAnstoremove.ParentNode.RemoveChild(trueAnstoremove);

            int myAnsCounter = Convert.ToInt16(trueAnsDocument.SelectSingleNode("//answerCounter").InnerXml);
            myAnsCounter--;
            string myNewAnsId = myAnsCounter.ToString();
            trueAnsDocument.SelectSingleNode("//answerCounter").InnerText = myNewAnsId;

            //ולאחר מכן לסגור את החלון בדומה לקוד מעלה
            Button myoKBtn = (Button)sender;

            //סגירת הפאנל בו נמצא הכפתור
            ((Panel)myoKBtn.Parent).Visible = false;
            //סגירת הרקע האפור
            grayWindows.Visible = false;

            ////שמירה
            XmlDataSourceTrueAns.Save();
            GridViewTrueAns.DataBind();
            //הצגה של הקובץ החדש מהספרייה
            showInInnerGrid();
            showNoAnsLbl();
            howManyRemin();
            
            //בדיקה האם יש לפחות 20 מסיחים- לשינוי צבע הלייבל בתנאי פרסום
            allowPub();
            changeMinAns();

        }

        if (falseAnsToDel.Attributes["isCorrect"].Value == "false")
        {

            //סשן עם קוד מסיח
            XmlNode falseAnstoremove = falseAnsDocument.SelectSingleNode("/games/game[@GameCode='" + theNewcode + "']/Answers/answer[@ansId='" + answerID + "']");


            //פקודת המחיקה
            falseAnstoremove.ParentNode.RemoveChild(falseAnstoremove);

            int myAnsCounter = Convert.ToInt16(falseAnsDocument.SelectSingleNode("//answerCounter").InnerXml);
            myAnsCounter--;
            string myNewAnsId = myAnsCounter.ToString();
            falseAnsDocument.SelectSingleNode("//answerCounter").InnerText = myNewAnsId;

            //ולאחר מכן לסגור את החלון בדומה לקוד מעלה
            Button myoKBtn = (Button)sender;

            //סגירת הפאנל בו נמצא הכפתור
            ((Panel)myoKBtn.Parent).Visible = false;
            //סגירת הרקע האפור
            grayWindows.Visible = false;

            ////שמירה
            XmlDataSourceFalseAns.Save();
            GridViewFalseAns.DataBind();
            //הצגה של הקובץ החדש מהספרייה
            showInInnerGrid();
            showNoAnsLbl();
            howManyRemin();

            //בדיקה האם יש לפחות 20 מסיחים- לשינוי צבע הלייבל בתנאי פרסום
            allowPub();
            changeMinAns();

        }
        

    }

    //כפתור להוספת מסיח טקסט
    protected void addTxtAnsBtn_Click(object sender, EventArgs e)
    {
        addTxtBTN.Enabled = true;
        string theNewcode = Session["theCode"].ToString();
        XmlDocument myXmlTrueAns = XmlDataSourceTrueAns.GetXmlDocument();
        XmlDocument myXmlFalseAns = XmlDataSourceFalseAns.GetXmlDocument();
        XmlDataSourceTrueAns.XPath = "/games/game[@GameCode=" + theNewcode + "]/Answers/answer[@isCorrect='true']";
        XmlDataSourceFalseAns.XPath = "/games/game[@GameCode=" + theNewcode + "]/Answers/answer[@isCorrect='false']";

        XmlNode myAnsCounter = myXmlTrueAns.SelectSingleNode("games/game[@GameCode = '" + theNewcode + "']/answerCounter");
        string ansCounter = myAnsCounter.InnerText;
        int latestAnsID = Convert.ToInt16(ansCounter);
        Session["ansID"] = latestAnsID;
        latestAnsID++;
        myAnsCounter.InnerText = latestAnsID.ToString();

        //בדיקה האם המסיח נכון או לא 
        string ifAnsCorrect;


        if (TextIfCorrectCBK.SelectedValue == 0.ToString())
        {
            ifAnsCorrect = "true";

            //משתנה להחזקת ענף עוטף למסיחים
            XmlNode allAns = myXmlTrueAns.SelectSingleNode("//Answers");

            //יצירת תשובה
            XmlElement everyNewAns = myXmlTrueAns.CreateElement("answer");

            everyNewAns.SetAttribute("ansId", latestAnsID.ToString());
            everyNewAns.SetAttribute("isCorrect", ifAnsCorrect);
            everyNewAns.SetAttribute("ansType", "text");
            everyNewAns.InnerText = addAnsTxt.Text;

            //צירוף התשובה לענף התשובות
            allAns.AppendChild(everyNewAns);


            XmlNode FirstAns = myXmlTrueAns.SelectNodes("games/game[@GameCode = '" + theNewcode + "']/Answers/answer").Item(0);
            myXmlTrueAns.SelectSingleNode("games/game[@GameCode = '" + theNewcode + "']/Answers").InsertBefore(everyNewAns, FirstAns);
     

            //שיבוץ בגריד לפי נכונות התשובה
            XmlDataSourceTrueAns.Save();
            GridViewTrueAns.DataBind();

            //שמירת התושבה
            //מחיקת תוכן
            addAnsTxt.Text = "";

            //בדיקה האם יש לפחות 20 מסיחים- לשינוי צבע הלייבל בתנאי פרסום
            changeMinAns();
            allowPub();
            howManyRemin();
            showNoAnsLbl();
            //סגירת הפנאל
            TextIfCorrectCBK.ClearSelection();

            panelTxt.Visible = false;
            addPicBTN.Enabled = true;
            showInInnerGrid();


        }
        else
        {
            ifAnsCorrect = "false";

            //משתנה להחזקת ענף עוטף למסיחים
            XmlNode allAns = myXmlFalseAns.SelectSingleNode("//Answers");

            //יצירת תשובה
            XmlElement everyNewAns = myXmlFalseAns.CreateElement("answer");

            everyNewAns.SetAttribute("ansId", latestAnsID.ToString());
            everyNewAns.SetAttribute("isCorrect", ifAnsCorrect);
            everyNewAns.SetAttribute("ansType", "text");
            everyNewAns.InnerText = addAnsTxt.Text;

            //צירוף התשובה לענף התשובות
            allAns.AppendChild(everyNewAns);


            XmlNode FirstAns = myXmlFalseAns.SelectNodes("games/game[@GameCode = '" + theNewcode + "']/Answers/answer").Item(0);
            myXmlFalseAns.SelectSingleNode("games/game[@GameCode = '" + theNewcode + "']/Answers").InsertBefore(everyNewAns, FirstAns);

            //שיבוץ בגריד לפי נכונות התשובה
            XmlDataSourceFalseAns.Save();
           
             GridViewFalseAns.DataBind();

            //שמירת התושבה
            //מחיקת תוכן
            addAnsTxt.Text = "";

            //בדיקה האם יש לפחות 20 מסיחים- לשינוי צבע הלייבל בתנאי פרסום
            changeMinAns();
            allowPub();
            howManyRemin();
            showNoAnsLbl();
            //סגירת הפנאל
            TextIfCorrectCBK.ClearSelection();
            addPicBTN.Enabled = true;
            panelTxt.Visible = false;

            showInInnerGrid();
        }

     
    }

    //כפתור פבלוש
    protected void PubBTN_Click(object sender, EventArgs e)
    {
        allowPub();
       
        if (PubBTN.Enabled == true) { 
        //לשנות את האטריביוט של המשחק 
    

            XmlDocument myXmlFalseAns = XmlDataSourceFalseAns.GetXmlDocument();
            XmlDocument myXmlTrueAns = XmlDataSourceTrueAns.GetXmlDocument();

            //משתנה לשמירת קוד המשחק
            string theNewcode = Session["theCode"].ToString();

            XmlNode TrueChangePub = myXmlFalseAns.SelectSingleNode("//game[@GameCode=" + theNewcode + "]");
            XmlNode falseChangePub = myXmlTrueAns.SelectSingleNode("//game[@GameCode=" + theNewcode + "]");

            TrueChangePub.Attributes["published"].Value = "true";
            falseChangePub.Attributes["published"].Value = "true";

            //שמירת פרסום
            XmlDataSourceFalseAns.Save();
            XmlDataSourceTrueAns.Save();

            saveFunc();
        }

    }

    //חזרה לטבלת המשחקים
    protected void backToGames_Click(object sender, EventArgs e)
    {
        string theNewcode = Session["theCode"].ToString();

        XmlDocument myXmlFalseAns = XmlDataSourceFalseAns.GetXmlDocument();
        XmlDocument myXmlTrueAns = XmlDataSourceTrueAns.GetXmlDocument();

        XmlNode FalseifSaved = myXmlFalseAns.SelectSingleNode("//game[@GameCode='" + theNewcode + "']");
        XmlNode trueifSaved = myXmlTrueAns.SelectSingleNode("//game[@GameCode='" + theNewcode + "']");

        XmlNode FalseName = myXmlFalseAns.SelectSingleNode("//game[@GameCode='" + theNewcode + "']/gameName");
        XmlNode trueName = myXmlTrueAns.SelectSingleNode("//game[@GameCode='" + theNewcode + "']/gameName");

        XmlNode FalseDir = myXmlFalseAns.SelectSingleNode("//game[@GameCode='" + theNewcode + "']/Topic");
        XmlNode trueDir = myXmlTrueAns.SelectSingleNode("//game[@GameCode='" + theNewcode + "']/Topic");

        if (FalseifSaved.Attributes["saved"].Value == "yes" || trueifSaved.Attributes["saved"].Value == "yes")
        {
            if(FalseName.InnerText!= gameNameTXT.Text)
            {
                FalseName.InnerText = gameNameTXT.Text;
                XmlDataSourceFalseAns.Save();
            }

            if (trueName.InnerText != gameNameTXT.Text)
            {
                trueName.InnerText = gameNameTXT.Text;
                XmlDataSourceTrueAns.Save();
            }

            if (FalseDir.InnerText != gameDirTXT.Text)
            {
                FalseDir.InnerText = gameDirTXT.Text;
                XmlDataSourceFalseAns.Save();

            }

            if (trueDir.InnerText != gameDirTXT.Text)
            {
                trueDir.InnerText = gameDirTXT.Text;
                XmlDataSourceTrueAns.Save();

            }

            Response.Redirect("gameTable.aspx");
           
        }
        if (FalseifSaved.Attributes["saved"].Value == "no" || trueifSaved.Attributes["saved"].Value == "no")
        {
            if(gameDirTXT.Text != "" && gameNameTXT.Text != "") { 
            grayWindows.Visible = true;
            noSavePanel.Visible = true;
            }
        }

        if(FalseifSaved.Attributes["saved"].Value == "no" || trueifSaved.Attributes["saved"].Value == "no" )
        {
            if(gameDirTXT.Text == "" || gameNameTXT.Text == "") { 
            noContentPanel.Visible = true;
            noContent.Visible = true;
            }
        }
    }

    //למחוק את המשחק שרציתי לייצר עכשיו ולחזור לטבלה
    protected void confirmAndDel_Click(object sender, EventArgs e)
    {
        Response.Redirect("gameTable.aspx");

    }

    //להשאר לערוך את המשחק
    protected void dontDelAndStay_Click(object sender, EventArgs e)
    {
        noContentPanel.Visible = false;
        noContent.Visible = false;
    }

    //כפתור לחזרה טלבלת משחקים ללא שמירה
    protected void dontSave_Click(object sender, EventArgs e)
    {
        Response.Redirect("gameTable.aspx");
        
    }

    //כפתור שמירה מהפופ אפ חזרה לטבלת משחקים
    protected void saveBackToGames_Click(object sender, EventArgs e)
    {
        saveFunc();
        Response.Redirect("gameTable.aspx");

    }

    //פונקציית השמירה
    protected void saveFunc()
    {

        string theNewcode = Session["theCode"].ToString();

        XmlDocument myXmlTrueAns = XmlDataSourceTrueAns.GetXmlDocument();
        XmlDocument myXmlFalseAns = XmlDataSourceFalseAns.GetXmlDocument();
       
        //שיבוץ בגריד לפי נכונות התשובה
        XmlDataSourceTrueAns.XPath = "/games/game[@GameCode=" + theNewcode + "]/Answers/answer[@isCorrect='true']";
        XmlDataSourceFalseAns.XPath = "/games/game[@GameCode=" + theNewcode + "]/Answers/answer[@isCorrect='false']";

        XmlNode falseNameNode = myXmlFalseAns.SelectSingleNode("//game[@GameCode=" + theNewcode + "]/gameName");
        XmlNode trueNameNode = myXmlTrueAns.SelectSingleNode("//game[@GameCode=" + theNewcode + "]/gameName");

        XmlNode falseDirNode = myXmlFalseAns.SelectSingleNode("//game[@GameCode=" + theNewcode + "]/Topic");
        XmlNode TrueDirNode = myXmlTrueAns.SelectSingleNode("//game[@GameCode=" + theNewcode + "]/Topic");

        XmlNode TruePlayerFBNode = myXmlTrueAns.SelectSingleNode("//game[@GameCode=" + theNewcode + "]/FBtoPlayer");
        XmlNode FalsePlayerFBNode = myXmlFalseAns.SelectSingleNode("//game[@GameCode=" + theNewcode + "]/FBtoPlayer");


        //בדיקה האם אין שם לשמחק- אם אין שלא יתן לשמור ויהיה טול טיפ על כפתור שמור שיגיד שיש לתת שם לשמחק
        if (gameNameTXT.Text != "" && gameDirTXT.Text != "") {

            //שומר את שם המחשק
            trueNameNode.InnerText = Server.UrlEncode(gameNameTXT.Text);
            falseNameNode.InnerText = Server.UrlEncode(gameNameTXT.Text);

            //שומר את הנחית המשחק
            falseDirNode.InnerText =Server.UrlEncode(gameDirTXT.Text);
            TrueDirNode.InnerText = Server.UrlEncode(gameDirTXT.Text);


            //שומר את משוב למשתמש


            if (FbkTxt.Text != "") { 
            fbForPlayerLbl.Text = FbkTxt.Text;
                TruePlayerFBNode.InnerText = Server.UrlEncode(fbForPlayerLbl.Text);
                FalsePlayerFBNode.InnerText = Server.UrlEncode(fbForPlayerLbl.Text);

            }

            else
            {
                string defualtPlayerFB = "כל הכבוד! סיימת להאכיל את משה הדג!";
                TruePlayerFBNode.InnerText = Server.UrlEncode(defualtPlayerFB);
                FalsePlayerFBNode.InnerText = Server.UrlEncode(defualtPlayerFB);

            }

            FbkTxt.Visible = false;
           FBKLimitChar.Visible = false;
            fbForPlayerLbl.Visible = true;
            editFB.Visible = true;

            XmlNode trueIfSaved = myXmlTrueAns.SelectSingleNode("//game[@GameCode='" + theNewcode + "']");
            XmlNode falseIfSaved = myXmlFalseAns.SelectSingleNode("//game[@GameCode='" + theNewcode + "']");

            trueIfSaved.Attributes["saved"].InnerText = "yes";
            falseIfSaved.Attributes["saved"].InnerText = "yes";


            XmlDataSourceFalseAns.Save();
            XmlDataSourceTrueAns.Save();
            FBKLimitChar.Visible = false;
            allowPub();
        }

    }

    //פונקציה שבודקת האם המשחק עומד בתנאי הפרסום
    protected void allowPub()
    {
        //משתנים לשמירת כמות התשובות הנוכנות והלא נכונות 
        int falseAns = 0;
        int trueAns = 0;

        //סשן לשמירת קוד המשחק
        string theNewcode = Session["theCode"].ToString();

        XmlDataSourceTrueAns.XPath = "/games/game[@GameCode=" + theNewcode + "]/Answers/answer[@isCorrect='true']";
        XmlDataSourceFalseAns.XPath = "/games/game[@GameCode=" + theNewcode + "]/Answers/answer[@isCorrect='false']";

        XmlDocument myXmlTrueAns = XmlDataSourceTrueAns.GetXmlDocument();
        XmlDocument myXmlFalseAns = XmlDataSourceFalseAns.GetXmlDocument();

        //לולאה שעוברת על המסיחים הלא נכונים ובודקת שיש לפחות 10
        XmlNodeList falseAnswerAmount = myXmlFalseAns.SelectNodes("//game[@GameCode='" + theNewcode + "'] / Answers/ answer[@isCorrect='false']");
        for (int falseCount = 0; falseCount <= falseAnswerAmount.Count; falseCount++)
        {
            falseAns = falseCount;
        }

        //לולאה שעוברת על המסיחים הנכונים ובודקת שיש לפחות 10
        XmlNodeList trueAnswerAmount = myXmlTrueAns.SelectNodes("//game[@GameCode='" + theNewcode + "'] / Answers/ answer[@isCorrect='true']");
        for (int trueCount = 0; trueCount <= trueAnswerAmount.Count; trueCount++)
        {
            trueAns = trueCount;
        }
        changeMinAns();

       // XmlNode ifplayerFB = myDoc.SelectSingleNode("//game[@GameCode='" + theNewcode + "']/ FBtoPlayer");
      //  string checkPlayerFB = ifplayerFB.InnerText;



         //בדיקה האם המשחק עומד בתנאי הפרסום
         //&& ifplayerFB.InnerText != ""
        if (gameNameTXT.Text!="" && gameDirTXT.Text!="" && falseAns>=10 && trueAns >= 10)
        {
            //PubBTN.Visible = true;
            PubBTN.CssClass = "";
            PubBTN.Enabled = true;
        }
           
    }

    //פונקציה לשינוי צבע הלייבל בתנאי הפרסום-
    protected void changeMinAns()
    {
        string theNewcode = Session["theCode"].ToString();

        XmlDataSourceTrueAns.XPath = "/games/game[@GameCode=" + theNewcode + "]/Answers/answer[@isCorrect='true']";
        XmlDataSourceFalseAns.XPath = "/games/game[@GameCode=" + theNewcode + "]/Answers/answer[@isCorrect='false']";

        XmlDocument myXmlTrueAns = XmlDataSourceTrueAns.GetXmlDocument();
        XmlDocument myXmlFalseAns = XmlDataSourceFalseAns.GetXmlDocument();

        //משתנים לשמירת כמות התשובות הנוכנות והלא נכונות 
        int falseAns = 0;
        int trueAns = 0;

        //סשן לשמירת קוד המשחק

        //לולאה שעוברת על המסיחים הלא נכונים ובודקת שיש לפחות 10
        XmlNodeList falseAnswerAmount = myXmlFalseAns.SelectNodes("//game[@GameCode='" + theNewcode + "'] / Answers/ answer[@isCorrect='false']");
        for (int falseCount = 0; falseCount <= falseAnswerAmount.Count; falseCount++)
        {
            falseAns = falseCount;
        }

        //לולאה שעוברת על המסיחים הנכונים ובודקת שיש לפחות 10
        XmlNodeList trueAnswerAmount = myXmlTrueAns.SelectNodes("//game[@GameCode='" + theNewcode + "'] / Answers/ answer[@isCorrect='true']");
        for (int trueCount = 0; trueCount <= trueAnswerAmount.Count; trueCount++)
        {
            trueAns = trueCount;
        }

        if (falseAns >= 10 && trueAns >= 10)
        {
            //pubMinAns.ForeColor = System.Drawing.Color.FromArgb(1, 11, 102, 35);
            pubMinAns.CssClass = "EanoghChar";

        }
        else
        {
          //  pubMinAns.ForeColor = System.Drawing.Color.FromArgb(1, 255, 0, 0);
            pubMinAns.CssClass = "notEanoghChar";

        }
    }

    //כפתור פח למחיקת מסיח טקסט שעוד לא נוסף
    protected void removeTextAns_Click(object sender, ImageClickEventArgs e)
    {
        addAnsTxt.Text = "";
        panelTxt.Visible = false;
        addPicBTN.Enabled = true;
        addTxtBTN.Enabled = true;

    }

    //כפתור פח למחיקת מסיח תמונה שעוד לא נוסף
    protected void removePicAns_Click(object sender, ImageClickEventArgs e)
    {
        //מחיקת תמונה מהפלייסהולדר
        ((Panel)FindControl("panelPic")).CssClass = "hidden";
        PicIfCorrectCBK.ClearSelection();
    }

    //כפתור שינוי משוב לשחקן
    protected void editFB_Click(object sender, ImageClickEventArgs e)
    {
        FbkTxt.Text = fbForPlayerLbl.Text;
        FbkTxt.Visible = true;
        fbForPlayerLbl.Visible = false;
        FBKLimitChar.Visible = true;
        editFB.Visible = false;

        if (FbkTxt.Text.Length > 2)
        {
            FBKLimitChar.CssClass = "countCharGreen";
        }
        if(FbkTxt.Text.Length < 2)
        {
            FBKLimitChar.CssClass = "countCharRed";
        }
        if(gameDirTXT.Text.Length > 2)
        {
            gameDirLimitChar.CssClass = "countCharGreen";
        }
        if (gameDirTXT.Text.Length < 2)
        {
            gameDirLimitChar.CssClass = "countCharRed";
        }
    }

    //ביטול מחיקת תשובה
    protected void dontDel_Click(object sender, EventArgs e)
    {
        grayWindows.Visible = false;
        DeleteConfPopUp.Visible = false;
    }

    //בדיקה האם יש תשובות בגריד
    protected void showNoAnsLbl()
    {
        string theNewcode = Session["theCode"].ToString();

        XmlDocument myXmlTrueAns = XmlDataSourceTrueAns.GetXmlDocument();
        XmlDocument myXmlFalseAns = XmlDataSourceFalseAns.GetXmlDocument();

        XmlNodeList trueAnsAmount = myXmlTrueAns.SelectNodes("/games/game[@GameCode='"+ theNewcode + "']/Answers/answer[@isCorrect='true']");

        XmlNodeList falseAnsAmount = myXmlFalseAns.SelectNodes("/games/game[@GameCode='" + theNewcode + "']/Answers/answer[@isCorrect='false']");

        if (trueAnsAmount.Count == 0)
        {
            noTrueAns.Visible = true;
        }
        if(trueAnsAmount.Count > 0)
        {
            noTrueAns.Visible = false;
        }

        if (falseAnsAmount.Count == 0)
        {
            noFalseAns.Visible = true;
        }
        if (falseAnsAmount.Count > 0)
        {
            noFalseAns.Visible = false;
        }
    }

    //שינוי כמה תשובות נותרו
    protected void howManyRemin()
    {
        string theNewcode = Session["theCode"].ToString();

        //שיבוץ בגריד לפי נכונות התשובה
        XmlDataSourceTrueAns.XPath = "/games/game[@GameCode=" + theNewcode + "]/Answers/answer[@isCorrect='true']";
        XmlDataSourceFalseAns.XPath = "/games/game[@GameCode=" + theNewcode + "]/Answers/answer[@isCorrect='false']";

        XmlDocument myXmlTrueAns = XmlDataSourceTrueAns.GetXmlDocument();
        XmlDocument myXmlFalseAns = XmlDataSourceFalseAns.GetXmlDocument();

        XmlNodeList falseAnswerAmount = myXmlFalseAns.SelectNodes("//game[@GameCode='" + theNewcode + "']/Answers/answer[@isCorrect='false']");
        XmlNodeList TrueAnswerAmount = myXmlTrueAns.SelectNodes("//game[@GameCode='" + theNewcode + "']/Answers/answer[@isCorrect='true']");

        int howManyFalseAns = falseAnswerAmount.Count;

        int hoeManyTrueAns = TrueAnswerAmount.Count;

        int howMAnyFalseAnsLeft = 10 - howManyFalseAns;

        int howMAnyTrueAnsLeft = 10 - hoeManyTrueAns;

        if (howMAnyTrueAnsLeft > 0)
        {
            THmowManyNum.Text = howMAnyTrueAnsLeft.ToString();
            THmowManyNum.ForeColor = System.Drawing.Color.FromArgb(1, 255, 255, 255);
            THowMany1.ForeColor = System.Drawing.Color.FromArgb(1, 255, 255, 255);
            THowMany2.ForeColor = System.Drawing.Color.FromArgb(1, 255, 255, 255);
        }

        if(howMAnyTrueAnsLeft <= 0)
        {
            THmowManyNum.Text = "0";
            THmowManyNum.ForeColor = System.Drawing.Color.FromArgb(1, 11, 102, 35);
            THowMany1.ForeColor = System.Drawing.Color.FromArgb(1, 11, 102, 35);
            THowMany2.ForeColor = System.Drawing.Color.FromArgb(1, 11, 102, 35);
        }


        if (howMAnyFalseAnsLeft > 0)
        {
            FHmowManyNum.Text = howMAnyFalseAnsLeft.ToString();
            FHmowManyNum.ForeColor = System.Drawing.Color.FromArgb(1, 255, 255, 255);
            FHowMany1.ForeColor = System.Drawing.Color.FromArgb(1, 255, 255, 255);
            FHowMany2.ForeColor = System.Drawing.Color.FromArgb(1, 255, 255, 255);
        }

        if (howMAnyFalseAnsLeft <= 0)
        {
            FHmowManyNum.Text = "0";
            FHmowManyNum.ForeColor = System.Drawing.Color.FromArgb(1, 11, 102, 35);
            FHowMany1.ForeColor = System.Drawing.Color.FromArgb(1, 11, 102, 35);
            FHowMany2.ForeColor = System.Drawing.Color.FromArgb(1, 11, 102, 35);
        }
    }

}

