using Books.Controllers;
using Books.Services;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Owin.Cors;
using Newtonsoft.Json.Serialization;
using Owin;
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Configuration;
using System.Reflection;
using System.Web.Http;

namespace Books
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            TelemetryConfiguration.Active.InstrumentationKey = ConfigurationManager.AppSettings["InstrumentationKey"];

            // Allow CORS
            app.UseCors(CorsOptions.AllowAll);

            // Configure and add Web API
            var configuration = new HttpConfiguration();
            configuration.MapHttpAttributeRoutes();
            configuration.Formatters.Remove(configuration.Formatters.XmlFormatter);
            configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = 
                new CamelCasePropertyNamesContractResolver();
            configuration.DependencyResolver = 
                new MefDependencyResolver(this.CreateCompositionContainer());
            app.UseWebApi(configuration);
        }

        /// <summary>
        /// Build MEF catalog and create composition container
        /// </summary>
        /// <returns>Configured composition container</returns>
        private CompositionContainer CreateCompositionContainer()
        {
            // In addition to explicitly exported classes, auto-export all web api controllers
            var rb = new RegistrationBuilder();
            rb.ForTypesMatching<ApiController>(t => typeof(ApiController).IsAssignableFrom(t) && t.Name.EndsWith("Controller"))
                .Export()
                .SetCreationPolicy(CreationPolicy.NonShared);
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly(), rb);

            // Create composition container
            var container = new CompositionContainer(catalog);

            container.ComposeExportedValue<INameGenerator>(new NameGenerator());
            container.ComposeExportedValue(new BooksDemoDataOptions
            {
                MinimumNumberOfBooks = Int32.Parse(ConfigurationManager.AppSettings["MinimumNumberOfBooks"]),
                MaximumNumberOfBooks = Int32.Parse(ConfigurationManager.AppSettings["MaximumNumberOfBooks"])
            });

            return container;
        }
    }
}
