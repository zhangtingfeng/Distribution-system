using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NgnixTest.Startup))]
namespace NgnixTest
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
