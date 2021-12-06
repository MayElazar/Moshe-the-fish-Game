using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;


public partial class _Default : System.Web.UI.Page
{
    XmlDocument myGameDoc = new XmlDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        myGameDoc = XmlDataSource1.GetXmlDocument();
        //בדיקה האם יש משחקים בטבלה
        showLblNoGames();
        //קריאה לפונקציה של הצ'ק בוקס- האם ניתן לפרסם
        canPublishGameCB();
        
        XmlNode checkIfSaved = myGameDoc.SelectSingleNode("//game");
        if (checkIfSaved != null) { 
        if (checkIfSaved.Attributes["saved"].InnerText == "no")
        {
            delGame();
        }
        }
        //בריצה הראשונה של העמוד
        if (Page.IsPostBack == false)
        {
            //הסתרה של החלון האפור
            grayWindows.Visible = false;

            //הסתרת של חלונות הפופ-אפ
            DeleteConfPopUp.Visible = false;
        }
    }

    protected void canPublishGameCB()
    {

        foreach (GridViewRow row in GridView1.Rows)
        {
            //חיפוש הלייבל שבו מופיע ה ID של המשחק
            Label gameCodeLbl = (Label)row.FindControl("gameCode");
            //בעזרת האי-די של המשחק נוכל לבדוק האם עומד בתנאי הפרסום
            string GameCode = gameCodeLbl.Text;

            //-------------תנאי הסף לעמידה בפבלוש---------------//

            //חיפוש כמות תשובות נוכנות
            XmlNodeList trueCount = myGameDoc.SelectNodes("/games/game[@GameCode='" + GameCode + "']/Answers/answer[@isCorrect='true']");
           
            //חיפוש כמות תשובות לא נוכנות
            XmlNodeList falseCount = myGameDoc.SelectNodes("/games/game[@GameCode='" + GameCode + "']/Answers/answer[@isCorrect='false']");


            //חיפוש הצ'אק-בוקס על פי האי-די שלו
            CheckBox GameIsPublishCb = (CheckBox)row.FindControl("publishedCB");

            if (trueCount.Count >= 10 && falseCount.Count>=10)
            {
                GameIsPublishCb.Enabled = true;
                GameIsPublishCb.CssClass = "cbChangeGreen"; 
                ((Panel)row.FindControl("publishTollTip")).CssClass = "";
                ((Label)row.FindControl("toolTipLbl")).Visible = false;
            }
            else
            {
                GameIsPublishCb.Enabled = false;
                GameIsPublishCb.CssClass = "cbChangeGrayNoPublish";
                //הצגת הטולטיפ רק לכפתורים לא פעילים
                ((Panel)row.FindControl("publishTollTip")).CssClass = "tooltip";
                ((Label)row.FindControl("toolTipLbl")).Visible = true;

                int remainTrueAns = 10 - trueCount.Count;

                int remainFalseAns = 10 - falseCount.Count;

                if (remainTrueAns < 0)
                {
                    remainTrueAns = 0;
                }

                if (remainFalseAns < 0)
                {
                    remainFalseAns = 0;
                }

                if (remainFalseAns <= 10 && remainFalseAns > 0 && remainTrueAns > 0 && remainTrueAns <= 10) { 

                    ((Label)row.FindControl("toolTipLbl")).Text = "נותרו עוד "+ remainTrueAns.ToString()+ " תשובות נכונות  ו "+ remainFalseAns .ToString()+ " תשובות לא נכונות לפרסום המשחק";
                    //((Label)row.FindControl("toolTipLbl")).Text = "נותרו עוד " + remainFalseAns.ToString() + "תשובות לא נכונות " + "\n" + "ו " + remainTrueAns.ToString() + "תשובות נכונות לפרסום המשחק";

                }
                if(remainTrueAns==0&& remainFalseAns > 0)
                {
                    ((Label)row.FindControl("toolTipLbl")).Text = "נותרו עוד " + remainFalseAns + " תשובות לא נכונות לפרסום המשחק";
                }
                if (remainFalseAns == 0 && remainTrueAns > 0)
                {
                    ((Label)row.FindControl("toolTipLbl")).Text = "נותרו עוד " + remainTrueAns + " תשובות נכונות לפרסום המשחק";
                }

                if (remainTrueAns==1 && remainFalseAns>= 0)
                {
                    ((Label)row.FindControl("toolTipLbl")).Text = "חסרה עוד תשובה אחת נכונה לפרסום המשחק";
                }

                if(remainTrueAns == 1 && remainFalseAns > 1)
                {
                    ((Label)row.FindControl("toolTipLbl")).Text = "חסרה עוד תשובה אחת נכונה ו  "+ remainFalseAns + " תשובות לא נכונות לפרסום המשחק";
                }

                if (remainFalseAns == 1 && remainTrueAns >= 0)
                {
                    ((Label)row.FindControl("toolTipLbl")).Text = "חסרה עוד תשובה אחת לא נכונה לפרסום המשחק";
                }

                if (remainFalseAns == 1 && remainTrueAns > 1)
                {
                    ((Label)row.FindControl("toolTipLbl")).Text = "חסרה עוד תשובה אחת לא נכונה ו "+ remainTrueAns + " תשובות נכונות לפרסום המשחק";
                }

                if (remainTrueAns == 1 && remainFalseAns == 1)
                {
                    ((Label)row.FindControl("toolTipLbl")).Text = "חסרה עוד תשובה אחת נכונה ותשובה אחת לא נכונה לפרסום המשחק";
                }



                //אם מקודם המשחק היה מפורסם, אנחנו רוצים להחזיר אותו ללא מפורסם בעץ
                XmlNode IsPublish = myGameDoc.SelectSingleNode("/games/game[@GameCode='" + GameCode + "']");

                IsPublish.Attributes["published"].InnerText = "False";
                XmlDataSource1.Save();
                
                //וגם לשנות את הפקד עצמו ללא לחוץ
                GameIsPublishCb.Checked = false;
                
            }  

        }
    }

    //הוספת משחק חדש
    protected void addGame_Click(object sender, EventArgs e)
    {
        XmlDocument xmlDoc = XmlDataSource1.GetXmlDocument(); //טעינת הקובץ אקסמל בצורה חדשה כדי לראות את הסורס. 
      
        //--הקפצה של ה- ID--//
        int myId = Convert.ToInt16(xmlDoc.SelectSingleNode("//idCounter").InnerXml);
        myId++;
        string myNewId = myId.ToString();
        xmlDoc.SelectSingleNode("//idCounter").InnerText = myNewId;

        //יצירה של הקוד משחק
        int GmeCode = 100 + Convert.ToInt16(xmlDoc.SelectSingleNode("//idCounter").InnerXml);
        string myGmeCode = GmeCode.ToString(); //אם אני רוצה ליצור קוד של 106, נוסיף את 10 ידנית ואז את שם המשתנה

        Session["theCode"]= myGmeCode;

        // יצירת ענף למשחק בודד     
        XmlElement newGameNode = xmlDoc.CreateElement("game");
        newGameNode.SetAttribute("GameCode", myGmeCode);
        newGameNode.SetAttribute("published", "false");
        newGameNode.SetAttribute("saved", "no");

        //שם המשחק
        XmlElement newGameName = xmlDoc.CreateElement("gameName");
        newGameName.InnerText = "";
        newGameNode.AppendChild(newGameName);

        //יצירת נושא המשחק
        XmlElement newTopicNode = xmlDoc.CreateElement("Topic");
        newGameNode.AppendChild(newTopicNode);

        //יצירת קאונטר למספר התשובות
        XmlElement AnsCounter = xmlDoc.CreateElement("answerCounter");
        AnsCounter.InnerText = "0";
        newGameNode.AppendChild(AnsCounter);
      
        //יצירת ענף פידבק
        XmlElement newPlayerFB = xmlDoc.CreateElement("FBtoPlayer");
        newPlayerFB.InnerText = "כל הכבוד! סיימת להאכיל את משה הדג!";
        newGameNode.AppendChild(newPlayerFB);


        // יצירת ענף של כל התשובות
        XmlElement newAllAnswersNode = xmlDoc.CreateElement("Answers");
        newGameNode.AppendChild(newAllAnswersNode);
        

        // הוספת המשחק לעץ
        XmlNode FirstGame = xmlDoc.SelectNodes("/games/game").Item(0);
        xmlDoc.SelectSingleNode("/games").InsertBefore(newGameNode, FirstGame);
        XmlDataSource1.Save();
        GridView1.DataBind();

        canPublishGameCB();
        //מעבר לדף העריכה
        Response.Redirect("creatingGame.aspx");

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ImageButton i = (ImageButton)e.CommandSource; //הכנסת האימג' בטון לתוך משתנה ובירור על מי לחצנו

        // אנו מושכים את האי די של הפריט באמצעות מאפיין לא שמור במערכת שהוספנו באופן ידני לכפתור-תמונה
        string theId = i.Attributes["theItemId"];

        //מכניסים את האי די לסשן כדי שנוכל לפות אליו בפונקציה אחרת ובמעבר עמודים
        Session["theCode"] = i.Attributes["theItemId"];

        
        // עלינו לברר איזו פקודה צריכה להתבצע - הפקודה רשומה בכל כפתור             
        switch (e.CommandName)
        {
            //אם נלחץ על כפתור מחיקה יקרא לפונקציה של מחיקה                    
            case "deleteRow":
                popupFunc();
                
                break;

            //אם נלחץ על כפתור עריכה (העפרון) נעבור לדף עריכה                    
            case "editRow":

                Response.Redirect("creatingGame.aspx");
                break;
        }

    }

    //פתיחת פאנל אישור מחיקה
    protected void popupFunc()
    {
        //הצגה של המסך האפור
        grayWindows.Visible = true;
        //הצגת הפופ-אפ של המחיקה
        DeleteConfPopUp.Visible = true;

        string theNewcode = Session["theCode"].ToString();
        XmlDocument Document = XmlDataSource1.GetXmlDocument();
        XmlNode gameToDel = Document.SelectSingleNode("/games/game[@GameCode='" + theNewcode + "']/gameName");


        ((Label)FindControl("areUSureLbl")).Text = Server.UrlDecode("ברצונך למחוק את המשחק " +'"'+ gameToDel.InnerText+'"' + " לצמיתות?");

    }
    
    //אישור מחיקת משחק
    protected void OkBtn_Click(object sender, EventArgs e)
    {
        delGame();

    }

    protected void delGame()
    {

        //לקיחת הקובץ 
        XmlDocument Document = XmlDataSource1.GetXmlDocument();

        string theNewcode = Session["theCode"].ToString();
        //זיהוי איזה ענף להוריד
        XmlNode node = Document.SelectSingleNode("/games/game[@GameCode='" + theNewcode + "']");
        //פקודת המחיקה
        node.ParentNode.RemoveChild(node);


        //ולאחר מכן לסגור את החלון בדומה לקוד מעלה
        //Button myoKBtn = (Button)sender;

        //סגירת הפאנל בו נמצא הכפתור
        //((Panel)myoKBtn.Parent).Visible = false;
        DeleteConfPopUp.Visible = false;
        //סגירת הרקע האפור
        grayWindows.Visible = false;

        //שמירה
        XmlDataSource1.Save();
        GridView1.DataBind();
        canPublishGameCB();
        //בדיקה האם יש משחקים בטבלה
        showLblNoGames();
    }

    protected void publishedCB_CheckedChanged(object sender, EventArgs e)
    {
        XmlDocument xmlDoc = XmlDataSource1.GetXmlDocument();

        CheckBox publishedCB = (CheckBox)sender;
        // מושכים את האי די של הפריט באמצעות המאפיין שהוספנו באופן ידני לתיבה
        string theId = publishedCB.Attributes["theItemId"];

        //שאילתא למציאת הסטודנט שברצוננו לעדכן
        XmlNode theGame = xmlDoc.SelectSingleNode("/games/game[@GameCode=" + theId + "]");

        ////קבלת הערך החדש של התיבה לאחר הלחיצה
        bool NewIsPublished = publishedCB.Checked;
        
        //עדכון של המאפיין בעץ
        theGame.Attributes["published"].InnerText = NewIsPublished.ToString();
        XmlDataSource1.Save();
       

    }

    //ביטול מחיקת משחק
    protected void dontDel_Click(object sender, EventArgs e)
    {

        //סגירת הפאנל בו נמצא כפתור היציאה
        DeleteConfPopUp.Visible = false;

        //סגירת הרקע האפור
        grayWindows.Visible = false;
    }

    //בדיקה האם יש משחקים בטבלה
    protected void showLblNoGames()
    {
        XmlDocument Document = XmlDataSource1.GetXmlDocument();

        XmlNode checkGames = Document.SelectSingleNode("//game");
        if(checkGames==null)
        {
            noGames.Visible = true;
        }
        else
        {
            noGames.Visible = false;
       
        XmlNode firstGameToDel = Document.SelectNodes("/games/game").Item(0);

       // XmlNode gameSingle = Document.SelectSingleNode("//game");
        XmlNode checkGameName = Document.SelectNodes("//game/gameName").Item(0);

        if (checkGameName.InnerText == "")
        {
            firstGameToDel.ParentNode.RemoveChild(firstGameToDel);

            XmlDataSource1.Save();
            canPublishGameCB();

            GridView1.DataBind();

        }
        }

    }
}