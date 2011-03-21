using System;
using System.Web;
using System.Web.Routing;
using Autofac;

namespace Restbucks.Service
{
    public class Global : HttpApplication
    {
        private static RestbucksConfiguration _configuration;

        public static RestbucksConfiguration Configuration
        {
            get { return _configuration; }
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new MainModule());
            var container = containerBuilder.Build();
            _configuration = new RestbucksConfiguration(container);
            RestbucksResources.RegisterRoutes(Configuration);
        }
    }
}