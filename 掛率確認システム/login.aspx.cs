using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 掛率確認システム
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            if(Login1.UserName == "admin" && Login1.Password == "password")
            {
                //クッキーを新たに取得する
                HttpCookie cookie = new HttpCookie("UserName");
                cookie.Value = "admin";
                cookie.Expires = DateTime.Now.AddHours(1); //クッキー所有時間を設定
                Response.Cookies.Add(cookie);
                Login1.DestinationPageUrl = "~/Markup.aspx";
                e.Authenticated = true;
         
            }
            else
            if(Login1.UserName == "user" && Login1.Password == "password")
            {
                //クッキーを新たに取得する
                HttpCookie cookie = new HttpCookie("UserName");
                cookie.Value = "user";
                cookie.Expires = DateTime.Now.AddHours(1); //クッキー所有時間を設定
                Response.Cookies.Add(cookie);
                Login1.DestinationPageUrl = "~/Markup_default.aspx";
                e.Authenticated = true;
            }
            else
            {
                e.Authenticated = false;
            }
        }
    }
}