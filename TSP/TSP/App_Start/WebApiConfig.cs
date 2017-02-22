using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Newtonsoft.Json.Serialization;
using TSP.Services;

namespace TSP
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
            UnityContainer container = new UnityContainer();

            container.RegisterType<ITargetsService, TargetsFileStorage>(new ContainerControlledLifetimeManager());

            IConfigurationService configurationService = new FileConfigurationService();
            container.RegisterInstance(configurationService, new ContainerControlledLifetimeManager());

            container.RegisterInstance<IDistanceServiceFactory>(new DistanceServiceFactory(configurationService), 
                new ContainerControlledLifetimeManager());

            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));

            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}
