using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TaskMaster.Startup))]
namespace TaskMaster
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
