using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LazyLoadingMvc5.Startup))]
namespace LazyLoadingMvc5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
