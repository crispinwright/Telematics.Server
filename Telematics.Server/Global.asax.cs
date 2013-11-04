﻿using System;
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

        protected void Application_Start()
        {
           // _kernel = CreateKernel();
            //ValueProviderFactories.Factories.Add(new JsonValueProviderFactory());
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            var xml = GlobalConfiguration.Configuration.Formatters.XmlFormatter;
            
            json.UseDataContractJsonSerializer = true;
            
            xml.UseXmlSerializer = true;
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            

        }

    }
}