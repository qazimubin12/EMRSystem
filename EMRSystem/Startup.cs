using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EMRSystem.Startup))]
namespace EMRSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
           
        }
    }
}
