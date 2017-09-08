using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NginxTest
{
    public class PageBase : System.Web.UI.Page
    {
        NginxTest.RedisSession _oRedisSession;
        public PageBase()
        { }

        public NginxTest.RedisSession oRedisSession
        {
            get {
                if (_oRedisSession == null)
                {
                    _oRedisSession = new NginxTest.RedisSession(this.Context, true, 20);
                }
                return _oRedisSession;
            }
        }

        protected override void OnPreInit(EventArgs e)
        {            
            base.OnPreInit(e);
            if (!oRedisSession.IsExistKey("usercode"))
            {
                Response.Redirect("login.aspx");
            }
        }
    }
}