using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SafetyAndSecurityForNSI.Startup))]
namespace SafetyAndSecurityForNSI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
