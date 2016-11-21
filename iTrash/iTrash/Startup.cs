using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(iTrash.Startup))]
namespace iTrash
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
