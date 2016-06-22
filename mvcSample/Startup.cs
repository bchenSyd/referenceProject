using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mvcSample.Startup))]
namespace mvcSample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
        
    }
}
