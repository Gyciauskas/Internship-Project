using System;
using System.Configuration;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ServiceStack.Logging;


namespace PresentConnection.Internship7.Iot.WebApp
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Removes WebFormViewEngine to increase performance
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        

        protected void Application_Error(object sender, EventArgs e)
        {
            var error = Server.GetLastError();
            var code = (error is HttpException) ? (error as HttpException).GetHttpCode() : 500;
            
            var customErrorsSection = ConfigurationManager.GetSection("system.web/customErrors") as CustomErrorsSection;
            if ((customErrorsSection == null) || (customErrorsSection.Mode == CustomErrorsMode.Off))
            {
                return;
            }

            if ((customErrorsSection.Mode == CustomErrorsMode.RemoteOnly) && Request.IsLocal)
            {
                return;
            }

            if (code != 404)
            {
                // TODO  - Generate email with error details and send to administrator
            }

            Response.Clear();
            Server.ClearError();

            string path = Request.Path;
            Context.RewritePath(string.Format("~/error-{0}", code), false);
            IHttpHandler httpHandler = new MvcHttpHandler();
            httpHandler.ProcessRequest(Context);
            Context.RewritePath(path, false);
        }

        protected void Application_BeginRequest(object src, EventArgs e)
        {
            if (Request.IsLocal)
            {
                ServiceStack.MiniProfiler.Profiler.Start();
            }
        }

        protected void Application_EndRequest(object src, EventArgs e)
        {
            if (Request.IsLocal)
            {
                ServiceStack.MiniProfiler.Profiler.Stop();
            }
        }
    }
}