using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Autofac;

namespace Restbucks.Service
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new MainModule());
            var container = containerBuilder.Build();
            var configuration = new RestbucksConfiguration(container);
            RestbucksResources.RegisterRoutes(configuration);
        }
    }
}