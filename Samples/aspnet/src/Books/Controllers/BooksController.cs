using Books.Model;
using Books.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web;
using Microsoft.ApplicationInsights;

namespace Books.Controllers
{
    [RoutePrefix("api")]
    public class BooksController : ApiController
    {
        private INameGenerator nameGenerator;
        private BooksDemoDataOptions options;

        [ImportingConstructor]
        public BooksController(INameGenerator nameGenerator, BooksDemoDataOptions options)
        {
            this.nameGenerator = nameGenerator;
            this.options = options;
        }

        [HttpGet]
        [Route("books")]
        public async Task<IEnumerable<Book>> Get()
        {
            var numberOfBooks = new Random().Next(this.options.MinimumNumberOfBooks, this.options.MaximumNumberOfBooks + 1);

            var telemetryClient = new TelemetryClient();
            telemetryClient.TrackEvent($"Generating {numberOfBooks} books");

            var result = new Book[numberOfBooks];
            for (var i = 0; i < numberOfBooks; i++)
            {
                result[i] = new Book
                {
                    BookId = i,
                    Title = await this.nameGenerator.GenerateRandomBookTitleAsync(),
                    Description = @"Lorem ipsum dolor sit amet, consetetur sadipscing elitr, 
sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam 
erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea 
rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.",
                    Price = 42.0M
                };
            }

            return result;
        }

        [HttpPost]
        [Route("books")]
        public IHttpActionResult Post(Book newBook)
        {
            var telemetryClient = new TelemetryClient();
            telemetryClient.TrackEvent($"Trying to add a book");

            // For demo purposes, return an HTTP 500 error (used to demonstrate logging)
            return this.InternalServerError();
        }
    }
}
