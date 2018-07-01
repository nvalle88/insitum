using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(insitum.Startup))]
namespace insitum
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
