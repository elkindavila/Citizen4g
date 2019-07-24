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
    [RoutePrefix("api/sectors")]
    public class sectorsController : ApiController
    {
        private db_citizen4Entities1 db = new db_citizen4Entities1();

        // GET: api/sectors
        [Route("")]
        public IEnumerable<sector> Getsectors()
        {
            return db.sectors.ToList();
        }

        // GET: api/sectors/5
        [Route("{id:int}")]
        [ResponseType(typeof(sector))]
        public IHttpActionResult Getsector(int id)
        {
            sector sector = db.sectors.Find(id);
            if (sector == null)
            {
                return NotFound();
            }

            return Ok(sector);
        }

        [HttpGet]
        [Route("validate/{name}")]
        public HttpResponseMessage findByName(string name)
        {
            try
            {
                db.sectors.Single(t => t.NameSector == name);
                return new HttpResponseMessage(HttpStatusCode.OK);

            }
            catch
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // PUT: api/sectors/5
        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update([FromBody]sector sector)
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                var newUser = db.sectors.Single(t => t.idSector == sector.idSector);

                newUser.NameSector = sector.NameSector;
                newUser.idTown = sector.idTown;
                db.SaveChanges();
                return result;
            }
            catch
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // POST: api/sectors
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create([FromBody]sector sector)
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                db.sectors.Add(sector);
                db.SaveChanges();
                return result;
            }
            catch
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // DELETE: api/sectors/5
        [ResponseType(typeof(sector))]
        [HttpDelete]
        [Route("delete/{id}")]
        public IHttpActionResult Deletesector(int id)
        {
            sector sector = db.sectors.Find(id);
            if (sector == null)
            {
                return NotFound();
            }

            db.sectors.Remove(sector);
            db.SaveChanges();

            return Ok(sector);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool sectorExists(int id)
        {
            return db.sectors.Count(e => e.idSector == id) > 0;
        }
    }
}