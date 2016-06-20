using Books.Controllers;
using Books.Model;
using Books.Services;
using Microsoft.Owin.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Owin;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System;

namespace Books.Test
{
    [TestClass]
    public class IntegrationTestWithoutFakes
    {
        private class INameGeneratorMock : INameGenerator
        {
            public Task<string> GenerateRandomBookTitleAsync() =>
                Task.FromResult("The Red House");
        }

        private class StartupMock
        {
            public void Configuration(IAppBuilder app)
            {
                var configuration = new HttpConfiguration();
                configuration.MapHttpAttributeRoutes();
                configuration.DependencyResolver =
                    new MefDependencyResolver(this.CreateCompositionContainer());
                app.UseWebApi(configuration);
            }

            private CompositionContainer CreateCompositionContainer()
            {
                // Auto-export all web api controllers
                var rb = new RegistrationBuilder();
                rb.ForTypesMatching<ApiController>(t => typeof(ApiController).IsAssignableFrom(t) && t.Name.EndsWith("Controller"))
                    .Export()
                    .SetCreationPolicy(CreationPolicy.NonShared);
                var catalog = new AssemblyCatalog(typeof(BooksController).Assembly, rb);

                // Create composition container
                var container = new CompositionContainer(catalog);

                // Setup mock objects
                var generatorStub = new INameGeneratorMock();
                container.ComposeExportedValue<INameGenerator>(generatorStub);
                container.ComposeExportedValue(new BooksDemoDataOptions
                {
                    MinimumNumberOfBooks = 2,
                    MaximumNumberOfBooks = 2
                });

                return container;
            }
        }

        [TestMethod]
        public async Task SomeDemoTestWithoutFakes()
        {
            using (WebApp.Start<StartupMock>("http://localhost:12345"))
            {
                using (var client = new HttpClient())
                {
                    using (var response = await client.GetAsync("http://localhost:12345/api/books"))
                    {
                        var books = await response.Content.ReadAsAsync<Book[]>();

                        Assert.AreEqual(2, books.Length);
                        Assert.IsFalse(books.Any(b => b.Title != "The Red House"));
                    }
                }
            }
        }
    }
}
