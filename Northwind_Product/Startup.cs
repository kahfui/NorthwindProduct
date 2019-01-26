using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Northwind_Product.Startup))]
namespace Northwind_Product
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);            
        }
    }
}
