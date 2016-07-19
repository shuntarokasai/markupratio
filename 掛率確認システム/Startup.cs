using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(掛率確認システム.Startup))]
namespace 掛率確認システム
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
