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
    [RoutePrefix("api/feedback")]
    public class feedbacksController : ApiController
    {
        private db_citizen4Entities2 db = new db_citizen4Entities2();

        // GET: api/feedbacks
        [Route("")]
        public IEnumerable<feedback> Getfeedbacks()
        {
            return db.feedbacks.ToList();
        }

        // GET: api/feedbacks/5
        [ResponseType(typeof(feedback))]
        [Route("{id:int}")]
        public IHttpActionResult Getfeedback(int id)
        {
            feedback feedback = db.feedbacks.Find(id);
            if (feedback == null)
            {
                return NotFound();
            }

            return Ok(feedback);
        }

        // PUT: api/feedbacks/5
        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update([FromBody]feedback feedback)
        {
            try
            {

                var validarfeedback = db.feedbacks.Where(x => x.idfeedback == feedback.idfeedback).FirstOrDefault();

                if (validarfeedback == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "id validarfeedback no existe");
                }
                if (feedback.Descripcion == "" || feedback.Descripcion == null)
                {
                    validarfeedback.Descripcion = validarfeedback.Descripcion;
                }
                else validarfeedback.Descripcion = feedback.Descripcion;
                
                if(feedback.Observaciones == "" || feedback.Observaciones ==  null)
                {
                    validarfeedback.Observaciones = validarfeedback.Observaciones;
                }
                else validarfeedback.Observaciones = feedback.Observaciones;

                if (feedback.Email == "" || feedback.Email == null)
                {
                    validarfeedback.Email = validarfeedback.Email;
                }
                else validarfeedback.Email = feedback.Email;

                validarfeedback.idCitizen4 = validarfeedback.idCitizen4;

                db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK + validarfeedback.idfeedback);
            }
            catch (Exception ex)
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // POST: api/feedbacks
        [ResponseType(typeof(feedback))]
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create([FromBody]feedback feedback)
        {
            if (feedback.Descripcion == "" || feedback.Descripcion == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Por favor ingresa tus comentarios del app");
            }

            db.feedbacks.Add(feedback);
            db.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.OK + feedback.idfeedback);
        }

        // DELETE: api/feedbacks/5
        [ResponseType(typeof(feedback))]
        [HttpDelete]
        [Route("delete/{id}")]
        public IHttpActionResult Deletefeedback(int id)
        {
            feedback feedback = db.feedbacks.Find(id);
            if (feedback == null)
            {
                return NotFound();
            }

            db.feedbacks.Remove(feedback);
            db.SaveChanges();

            return Ok(feedback);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool feedbackExists(int id)
        {
            return db.feedbacks.Count(e => e.idfeedback == id) > 0;
        }
    }
}