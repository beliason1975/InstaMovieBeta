using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InstaMovieBeta.Startup))]
namespace InstaMovieBeta
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
