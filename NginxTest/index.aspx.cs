
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NginxTest
{
    public partial class index :  PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
              //["usercode"] = "ericdeng";
            Label0.Text = "请求开始时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +"<br/>";
            Label0.Text += "服务器名称：" + Server.MachineName + "<br/>";//服务器名称  
            Label0.Text += "服务器IP地址：" + Request.ServerVariables["LOCAL_ADDR"] + "<br/>";//服务器IP地址  
            Label0.Text += "HTTP访问端口：" + Request.ServerVariables["SERVER_PORT"];//HTTP访问端口"
            Label0.Text += ".NET解释引擎版本：" + ".NET CLR" + Environment.Version.Major + "." + Environment.Version.Minor + "." + Environment.Version.Build + "." + Environment.Version.Revision + "<br/>";//.NET解释引擎版本  
            Label0.Text += "服务器操作系统版本：" + Environment.OSVersion.ToString() + "<br/>";//服务器操作系统版本  
            Label0.Text += "服务器IIS版本：" + Request.ServerVariables["SERVER_SOFTWARE"] + "<br/>";//服务器IIS版本  
            Label0.Text += "服务器域名：" + Request.ServerVariables["SERVER_NAME"] + "<br/>";//服务器域名  
            Label0.Text += "虚拟目录的绝对路径：" + Request.ServerVariables["APPL_RHYSICAL_PATH"] + "<br/>";//虚拟目录的绝对路径  
            Label0.Text += "执行文件的绝对路径：" + Request.ServerVariables["PATH_TRANSLATED"] + "<br/>";//执行文件的绝对路径  
            Label0.Text += "虚拟目录Session总数：" + Session.Contents.Count.ToString() + "<br/>";//虚拟目录Session总数  
            Label0.Text += "虚拟目录Application总数：" + Application.Contents.Count.ToString() + "<br/>";//虚拟目录Application总数  
            Label0.Text += "域名主机：" + Request.ServerVariables["HTTP_HOST"] + "<br/>";//域名主机  
            Label0.Text += "服务器区域语言：" + Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"] + "<br/>";//服务器区域语言  
            Label0.Text += "用户信息：" + Request.ServerVariables["HTTP_USER_AGENT"] + "<br/>";
            Label0.Text += "CPU个数：" + Environment.GetEnvironmentVariable("NUMBER_OF_PROCESSORS") + "<br/>";//CPU个数  
            Label0.Text += "CPU类型：" + Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER") + "<br/>";//CPU类型  
            Label0.Text += "请求来源地址：" + Request.Headers["X-Real-IP"] + "<br/>";

            Label0.Text += "usercode: " + oRedisSession["usercode"].ToString() + "<br/>";
            Label0.Text += "usercode: " + Session["userID"].ToString() + "<br/>";
            Label0.Text += "usercode: " + Session.SessionID.ToString() + "<br/>";
            
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            oRedisSession.Remove("usercode");
        }
    }
}