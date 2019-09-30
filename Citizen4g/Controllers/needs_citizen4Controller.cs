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
    [RoutePrefix("api/needcitizen")]
    public class needs_citizen4Controller : ApiController
    {
        private db_citizen4Entities2 db = new db_citizen4Entities2();

        // GET: api/needs_citizen4
        [Route("")]
        public IEnumerable<needs_citizen4> Getneeds_citizen4()
        {
            return db.needs_citizen4.ToList();
        }

        // GET: api/needs_citizen4/5
        [Route("{id:int}")]
        [ResponseType(typeof(needs_citizen4))]
        public IHttpActionResult Getneeds_citizen4(int id)
        {
            needs_citizen4 needs_citizen4 = db.needs_citizen4.Find(id);
            if (needs_citizen4 == null)
            {
                return NotFound();
            }

            return Ok(needs_citizen4);
        }

        // PUT: api/needs_citizen4/5
        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update([FromBody]needs_citizen4 need)
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);

                citizen4 validarCitizen = db.citizen4.Where(x => x.idCitizen4 == need.idCitizen4).FirstOrDefault();
                need validarNeed = db.needs.Where(x => x.idNeeds == need.idNeeds).FirstOrDefault();

                var newNeed = db.needs_citizen4.Single(t => t.idNeeds_Citizen4 == need.idNeeds_Citizen4);

                if (validarCitizen == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Id citizen No esta registrado: " + need.idCitizen4);
                }

                else if (validarNeed == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Necesidad no existe en la tabla maestra needs");
                }


                else if (newNeed == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Codigo id Needs_citizen No existe : " + need.idNeeds_Citizen4);
                }

                newNeed.idNeeds_Citizen4 = need.idNeeds_Citizen4;
                newNeed.idCitizen4 = need.idCitizen4;
                newNeed.idNeeds = need.idNeeds;

                db.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // POST: api/needs_citizen4
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create([FromBody]needs_citizen4 need)
        {


            citizen4 validarCitizen = db.citizen4.Where(x => x.idCitizen4 == need.idCitizen4).FirstOrDefault();
            need validarNeed = db.needs.Where(x => x.idNeeds == need.idNeeds).FirstOrDefault();
            

            if (validarCitizen == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Id citizen No esta registrado: " + need.idCitizen4);
            }

            else if (validarNeed == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Necesidad no existe en la tabla maestra needs");
            }
       
                db.needs_citizen4.Add(need);
                db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);

        }

        // DELETE: api/needs_citizen4/5
        [ResponseType(typeof(needs_citizen4))]
        [HttpDelete]
        [Route("delete/{id}")]
        public IHttpActionResult Deleteneeds_citizen4(int id)
        {
            needs_citizen4 needs_citizen4 = db.needs_citizen4.Find(id);
            if (needs_citizen4 == null)
            {
                return NotFound();
            }

            db.needs_citizen4.Remove(needs_citizen4);
            db.SaveChanges();

            return Ok(needs_citizen4);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool needs_citizen4Exists(int id)
        {
            return db.needs_citizen4.Count(e => e.idNeeds_Citizen4 == id) > 0;
        }
    }
}