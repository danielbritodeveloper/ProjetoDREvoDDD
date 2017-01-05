using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjetoDREvoDDD.MVC.Startup))]
namespace ProjetoDREvoDDD.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
