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
    [RoutePrefix("api/needs")]
    public class needsController : ApiController
    {
        private db_citizen4Entities2 db = new db_citizen4Entities2();

        // GET: api/needs
        [Route("")]
        public IEnumerable<need> Getneeds()
        {
            return db.needs.ToList();
        }

        // GET: api/needs/5
        [Route("{id:int}")]
        [ResponseType(typeof(need))]
        public IHttpActionResult Getneed(int id)
        {
            need need = db.needs.Find(id);
            if (need == null)
            {
                return NotFound();
            }

            return Ok(need);
        }

        // PUT: api/needs/5
        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update([FromBody]need need)
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);

               
                need validarNeed = db.needs.Where(x => x.idNeeds == need.idNeeds).FirstOrDefault();    

                if (validarNeed == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Necesidad no Registrada: ");
                }


                validarNeed.idNeeds = validarNeed.idNeeds;
                validarNeed.DescriptionNeed = need.DescriptionNeed;
                
                db.SaveChanges();
                return result;
            }
            catch
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // POST: api/needs
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create([FromBody]need need)
        {

            
            if (need.DescriptionNeed =="")
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "La descripcion de la necesidad no debe ser vacia");
            }
           
                db.needs.Add(need);
                db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);

        }

        // DELETE: api/needs/5
        [ResponseType(typeof(need))]
        [HttpDelete]
        [Route("delete/{id}")]
        public IHttpActionResult Deleteneed(int id)
        {
            need need = db.needs.Find(id);
            if (need == null)
            {
                return NotFound();
            }

            db.needs.Remove(need);
            db.SaveChanges();

            return Ok(need);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool needExists(int id)
        {
            return db.needs.Count(e => e.idNeeds == id) > 0;
        }
    }
}