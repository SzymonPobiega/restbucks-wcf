using System;
using System.Web;
using System.Web.Routing;
using Autofac;
using Microsoft.ApplicationServer.Http.Description;

namespace Restbucks.Service
{
    public class Global : HttpApplication
    {
        private static IHttpHostConfigurationBuilder _configuration;

        public static IHttpHostConfigurationBuilder Configuration
        {
            get { return _configuration; }
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new MainModule());
            var container = containerBuilder.Build();

            _configuration = HttpHostConfiguration.Create()
                .AddFormatters(new RestbucksMediaTypeFormatter())         
                .SetResourceFactory(new RestbucksResourceFactory(container));

            RestbucksResources.RegisterRoutes(Configuration);
        }
    }
}