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
        private db_citizen4Entities2 db = new db_citizen4Entities2();

        // GET: api/focus
        [Route("")]
        public IEnumerable<focus> Getfoci()
        {
            return db.foci.ToList();
        }

        // GET: api/focus/5
        [ResponseType(typeof(focus))]
        [Route("{id:int}")]
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
        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update([FromBody]focus focus)
        {
            try
            {

                var validarfocus = db.foci.Where(x => x.idFocus == focus.idFocus).FirstOrDefault();

                if (validarfocus == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Prioridad no Registrada ");
                }


                validarfocus.idFocus = validarfocus.idFocus;
                validarfocus.DescriptionFocus = focus.DescriptionFocus;

                db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK + validarfocus.idFocus);
            }
            catch
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // POST: api/focus
        [ResponseType(typeof(focus))]
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create([FromBody]focus focus)
        {
            if (focus.DescriptionFocus == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "La descripcion de la prioridad no debe ser vacia");
            }

            db.foci.Add(focus);
            db.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.OK + focus.idFocus);
        }

        // DELETE: api/focus/5
        [ResponseType(typeof(focus))]
        [HttpDelete]
        [Route("delete/{id}")]
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