using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using ODataServer.Models;


namespace ODataServer.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using ODataServer.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<MovieTicket>("MovieTickets");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    
    public class MovieTicketsController : ODataController
    {
        private MovieTicketContext db = new MovieTicketContext();


       // [ResponseCache(NoStore = true)]
        // GET: odata/MovieTickets
        [EnableQuery]
        public IQueryable<MovieTicket> GetMovieTickets()
        {
            return db.MovieTickets;
        }

        // GET: odata/MovieTickets(5)
        [EnableQuery]
        public SingleResult<MovieTicket> GetMovieTicket([FromODataUri] int key)
        {
            return SingleResult.Create(db.MovieTickets.Where(movieTicket => movieTicket.movieId == key));
        }

        // PUT: odata/MovieTickets(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<MovieTicket> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MovieTicket movieTicket = await db.MovieTickets.FindAsync(key);
            if (movieTicket == null)
            {
                return NotFound();
            }
            if (movieTicket.numOfTicketsToBuy > 0)
            {
                movieTicket.availableTickets = movieTicket.availableTickets - movieTicket.numOfTicketsToBuy;
            }
            patch.Put(movieTicket);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieTicketExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(movieTicket);
        }

        // POST: odata/MovieTickets
        public async Task<IHttpActionResult> Post(MovieTicket movieTicket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MovieTickets.Add(movieTicket);
            await db.SaveChangesAsync();

            return Created(movieTicket);
        }



        //[HttpPost]
        //[("GetSalesTaxRate(PostalCode={postalCode})")]
        //public IHttpActionResult GetSalesTaxRate([FromODataUri] int postalCode)
        //{
        //    double rate = 5.6;  // Use a fake number for the sample.
        //    return Ok(rate);
        //}




        //// POST: odata/MovieTickets
        //public async Task<IHttpActionResult> Post(List<MovieTicket> movieTicket)
        //{
        //    MovieTicket movieData = new MovieTicket();
        //    int i = 0;
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //   while(movieTicket.Count!=0)
        //        {
        //        movieData = movieTicket[i];
        //        db.MovieTickets.Add(movieData);
        //        i++;
        //        }
        //    await db.SaveChangesAsync();

        //    return Created(movieTicket);
        //}




        // PATCH: odata/MovieTickets(5)  
       
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<MovieTicket> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MovieTicket movieTicket = await db.MovieTickets.FindAsync(key);
            if (movieTicket == null)
            {
                return NotFound();
            }
            //if(movieTicket.numOfTicketsToBuy>0)
            //{
            //    movieTicket.availableTickets = movieTicket.availableTickets - movieTicket.numOfTicketsToBuy;
            //}
            patch.Patch(movieTicket);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieTicketExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(movieTicket);
        }

        // DELETE: odata/MovieTickets(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            MovieTicket movieTicket = await db.MovieTickets.FindAsync(key);
            if (movieTicket == null)
            {
                return NotFound();
            }

            db.MovieTickets.Remove(movieTicket);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MovieTicketExists(int key)
        {
            return db.MovieTickets.Count(e => e.movieId == key) > 0;
        }
    }
}
