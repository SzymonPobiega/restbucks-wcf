using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace Restbucks.Service
{
    public class MainModule : Module
    {
        private readonly Assembly _assembly = typeof (MainModule).Assembly;

        protected override void Load(ContainerBuilder builder)
        {
            RegisterResources(builder);
            RegisterMappers(builder);
            RegisterActivities(builder);
            RegisterRepositories(builder);
        }

        private void RegisterActivities(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(_assembly)
                .Where(x => x.Namespace == "Restbucks.Service.Activities").AsImplementedInterfaces();
        }

        private void RegisterMappers(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(_assembly)
                .Where(x => x.Namespace == "Restbucks.Service.Mappers").AsSelf().SingleInstance();
        }

        private void RegisterResources(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(_assembly)
                .Where(x => x.Namespace == "Restbucks.Service.Resources").AsSelf();
        }

        private void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(_assembly)
                .Where(x => x.Namespace == "Restbucks.Service.Infrastructure" && x.Name.EndsWith("Repository")).AsImplementedInterfaces().SingleInstance();
        }
    }
}