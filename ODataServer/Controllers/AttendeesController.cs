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
    builder.EntitySet<Attendee>("Attendees");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AttendeesController : ODataController
    {
        private AttendeeContext db = new AttendeeContext();

        // GET: odata/Attendees
        [EnableQuery]
        public IQueryable<Attendee> GetAttendees()
        {
            return db.Attendees;
        }

        // GET: odata/Attendees(5)
        [EnableQuery]
        public SingleResult<Attendee> GetAttendee([FromODataUri] int key)
        {
            return SingleResult.Create(db.Attendees.Where(attendee => attendee.AttendeeId == key));
        }

        // PUT: odata/Attendees(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Attendee> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Attendee attendee = await db.Attendees.FindAsync(key);
            if (attendee == null)
            {
                return NotFound();
            }

            patch.Put(attendee);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendeeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(attendee);
        }

        // POST: odata/Attendees
        public async Task<IHttpActionResult> Post(Attendee attendee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Attendees.Add(attendee);
            await db.SaveChangesAsync();

            return Created(attendee);
        }

        // PATCH: odata/Attendees(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Attendee> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Attendee attendee = await db.Attendees.FindAsync(key);
            if (attendee == null)
            {
                return NotFound();
            }

            patch.Patch(attendee);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendeeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(attendee);
        }

        // DELETE: odata/Attendees(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Attendee attendee = await db.Attendees.FindAsync(key);
            if (attendee == null)
            {
                return NotFound();
            }

            db.Attendees.Remove(attendee);
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

        private bool AttendeeExists(int key)
        {
            return db.Attendees.Count(e => e.AttendeeId == key) > 0;
        }
    }
}
