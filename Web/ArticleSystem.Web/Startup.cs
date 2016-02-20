using Microsoft.Owin;

using Owin;

[assembly: OwinStartupAttribute(typeof(ArticleSystem.Web.Startup))]

namespace ArticleSystem.Web
{
    /// <summary>Startup partial class</summary>
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
