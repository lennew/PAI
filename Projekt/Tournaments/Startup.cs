using Microsoft.Extensions.Hosting;
using Microsoft.Owin;
using Owin;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Entity;

[assembly: OwinStartupAttribute(typeof(Tournaments.Startup))]
namespace Tournaments
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            AspNetTimer.Start();
        }
    }
}
