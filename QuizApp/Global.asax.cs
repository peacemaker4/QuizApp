using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace QuizApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_Error()
        {
            HttpContext httpContext = HttpContext.Current;
            if (httpContext != null)
            {
                Exception ex = Server.GetLastError();
                if (ex != null)
                {
                    httpContext.ClearError();
                    httpContext.Response.Clear();
                    var httpEx = (ex as HttpException);
                    if(httpEx!=null)
                        httpContext.Response.Redirect("/Error/TotalError?errorMsg=" + ex.Message+ "&code="+httpEx.GetHttpCode());
                    else
                        httpContext.Response.Redirect("/Error/TotalErrorMsg?errorMsg=" + ex.Message);
                }
            }
                
        }
    }
}
