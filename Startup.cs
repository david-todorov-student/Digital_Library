using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DigitalLibrary.Startup))]
namespace DigitalLibrary
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
