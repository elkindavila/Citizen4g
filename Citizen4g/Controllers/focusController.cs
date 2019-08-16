using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Citizen4g.Models;

namespace Citizen4g.Controllers
{
    [RoutePrefix("api/focus")]
    public class focusController : ApiController
    {
        private db_citizen4Entities1 db = new db_citizen4Entities1();

        // GET: api/focus
        public IQueryable<focus> Getfoci()
        {
            return db.foci;
        }

        // GET: api/focus/5
        [ResponseType(typeof(focus))]
        public IHttpActionResult Getfocus(int id)
        {
            focus focus = db.foci.Find(id);
            if (focus == null)
            {
                return NotFound();
            }

            return Ok(focus);
        }

        // PUT: api/focus/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putfocus(int id, focus focus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != focus.idFocus)
            {
                return BadRequest();
            }

            db.Entry(focus).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!focusExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/focus
        [ResponseType(typeof(focus))]
        public IHttpActionResult Postfocus(focus focus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.foci.Add(focus);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = focus.idFocus }, focus);
        }

        // DELETE: api/focus/5
        [ResponseType(typeof(focus))]
        public IHttpActionResult Deletefocus(int id)
        {
            focus focus = db.foci.Find(id);
            if (focus == null)
            {
                return NotFound();
            }

            db.foci.Remove(focus);
            db.SaveChanges();

            return Ok(focus);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool focusExists(int id)
        {
            return db.foci.Count(e => e.idFocus == id) > 0;
        }
    }
}