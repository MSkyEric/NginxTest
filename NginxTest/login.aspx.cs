using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NginxTest
{
    public partial class login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RedisSession oRedisSession = new RedisSession(this.Context, true, 20);
                if (oRedisSession.IsExistKey("usercode"))
                {
                    Response.Redirect("index.aspx"); 
                }
                
                //System.Web.SessionState.SessionIDManager
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Session["userID"] = "从HTTP访问端口：" + Request.ServerVariables["SERVER_PORT"] + " 创建";
            RedisSession oRedisSession = new RedisSession(this.Context, true, 20);
            oRedisSession.Add("usercode", "ericdeng");
            Response.Redirect("index.aspx");
        }
    }
}