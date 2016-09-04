using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Moveax.Mvc.ErrorHandler;
using Ninject.Web.Common;

namespace MVCUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, System.EventArgs e)
        {
            var errorHandler = new MvcApplicationErrorHandler(application: this, exception: this.Server.GetLastError())
            {
                EnableHttpReturnCodes = false,
                PassThroughHttp401 = false
            };

            errorHandler.Execute();
        }
    }
}
