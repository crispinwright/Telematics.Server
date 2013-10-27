using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
//using Ninject;

namespace Telematics.Server
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {

       // private IKernel _kernel;


        protected void Application_Start()
        {
           // _kernel = CreateKernel();
            var xml = GlobalConfiguration.Configuration.Formatters.XmlFormatter;
            xml.UseXmlSerializer = true;
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        //protected IKernel CreateKernel()
        //{
        //    var f = new FileInfo(this.Server.MapPath("log4net.config"));
        //    log4net.Config.XmlConfigurator.ConfigureAndWatch(f);

        //    //KernelContainer.Kernel = ServiceLocator.Initialize(new StandardKernel(new ServiceModule(), new ContentModule()));
        //    return new StandardKernel();
        //}
    }
}