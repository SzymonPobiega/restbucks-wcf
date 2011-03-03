using System;
using System.Web;
using Autofac;

namespace Restbucks.Service
{
    public class Global : HttpApplication
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