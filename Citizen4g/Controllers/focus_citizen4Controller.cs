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
    [RoutePrefix("api/focuscitizen")]
    public class focus_citizen4Controller : ApiController
    {
        private db_citizen4Entities1 db = new db_citizen4Entities1();

        // GET: api/focus_citizen4
        [Route("")]
        public IEnumerable<focus_citizen4> Getfocus_citizen4()
        {
            return db.focus_citizen4.ToList();
        }

        // GET: api/focus_citizen4/5
        [Route("{id:int}")]
        [ResponseType(typeof(focus_citizen4))]
        public IHttpActionResult Getfocus_citizen4(int id)
        {
            focus_citizen4 focus_citizen4 = db.focus_citizen4.Find(id);
            if (focus_citizen4 == null)
            {
                return NotFound();
            }

            return Ok(focus_citizen4);
        }

        // PUT: api/focus_citizen4/5
        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update([FromBody]focus_citizen4 focus)
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);

                citizen4 validarCitizen = db.citizen4.Where(x => x.idCitizen4 == focus.idCitizen4).FirstOrDefault();
                focus validarFocus = db.foci.Where(x => x.idFocus == focus.idFocus).FirstOrDefault();

                var newFocus = db.focus_citizen4.Single(t => t.idFocus_Citizen4col == focus.idFocus_Citizen4col);

                if (validarCitizen == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Id citizen No esta registrado: " + focus.idCitizen4);
                }

                if (validarFocus == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Id de Prioridad no Registrada: ");
                }

                if (newFocus == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Codigo id focus_citizen No existe : " + focus.idCitizen4);
                }

                newFocus.idFocus_Citizen4col = focus.idFocus_Citizen4col;
                newFocus.idFocus = focus.idFocus;
                newFocus.idCitizen4 = focus.idCitizen4;
               
                db.SaveChanges();
                return result;
            }
            catch
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // POST: api/focus_citizen4
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create([FromBody]focus_citizen4 focus)
        {

           

            citizen4 validarCitizen = db.citizen4.Where(x => x.idCitizen4 == focus.idCitizen4).FirstOrDefault();
            focus validarFocus = db.foci.Where(x=> x.idFocus == focus.idFocus).FirstOrDefault();

            var focusci = db.focus_citizen4.Where(x=> x.idFocus == focus.idFocus && x.idCitizen4 == focus.idCitizen4).FirstOrDefault();

            if (validarCitizen == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Id citizen No esta registrado: " + focus.idCitizen4);
            }

            if (validarFocus == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Prioridad no Registrada: ");
            }

            if (focusci == null )
                {
                db.focus_citizen4.Add(focus);
                db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
                
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Prioridad ya registra para el ID de ciudadano: " + focus.idCitizen4);
            

        }

        // DELETE: api/focus_citizen4/5
        [ResponseType(typeof(focus_citizen4))]
        [HttpDelete]
        [Route("delete/{id}")]
        public IHttpActionResult Deletefocus_citizen4(int id)
        {
            focus_citizen4 focus_citizen4 = db.focus_citizen4.Find(id);
            if (focus_citizen4 == null)
            {
                return NotFound();
            }

            db.focus_citizen4.Remove(focus_citizen4);
            db.SaveChanges();

            return Ok(focus_citizen4);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool focus_citizen4Exists(int id)
        {
            return db.focus_citizen4.Count(e => e.idFocus_Citizen4col == id) > 0;
        }
    }
}