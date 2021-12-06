using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class loginPage : System.Web.UI.Page
{
 
    protected void Page_Load(object sender, EventArgs e)
    {
        wrongFBKPic.Visible = false;
    }

    protected void loginBtn_Click(object sender, EventArgs e)
    {
        string userName = "admin";
        string password = "telem";
        
        if(userName== userNameTxt.Text && password== passwordTxt.Text)
        {
            Response.Redirect("gameTable.aspx");
        }
        else
        {
            userNameTxt.Text = "";
            passwordTxt.Text = "";
            wrongFBKPic.Visible = true;
            loginPic.CssClass="hidden";

        }
    
    }

}