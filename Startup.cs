using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentActivity.Startup))]
namespace StudentActivity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
