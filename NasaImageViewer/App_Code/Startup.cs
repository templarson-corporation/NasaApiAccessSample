using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NasaImageViewer.Startup))]
namespace NasaImageViewer
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
