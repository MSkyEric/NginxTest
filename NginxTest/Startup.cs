using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NginxTest.Startup))]
namespace NginxTest
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
