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
    [RoutePrefix("api/focusnewadmincandidates")]
    public class focusNewAdmin_candidateController : ApiController
    {
        private db_citizen4Entities2 db = new db_citizen4Entities2();

        // GET: api/focusNewAdmin_candidate
        [Route("")]
        public IEnumerable<focusNewAdmin_candidate> GetfocusNewAdmin_candidate()
        {
            return db.focusNewAdmin_candidate.ToList();
        }

        // GET: api/focusNewAdmin_candidate/5
        [ResponseType(typeof(focusNewAdmin_candidate))]
        [Route("{id:int}")]
        public IHttpActionResult GetfocusNewAdmin_candidate(int id)
        {
            focusNewAdmin_candidate focusNewAdmin_candidate = db.focusNewAdmin_candidate.Find(id);
            if (focusNewAdmin_candidate == null)
            {
                return NotFound();
            }

            return Ok(focusNewAdmin_candidate);
        }

        // PUT: api/focusNewAdmin_candidate/5
        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update([FromBody]focusNewAdmin_candidate focuscandidate)
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);

                var idfocus = db.focusNewAdmin_candidate.Where(x => x.idfocusNewAdmin_candidate == focuscandidate.idfocusNewAdmin_candidate).FirstOrDefault();
                candidate validarCandidate = db.candidates.Where(x => x.idCandidates == focuscandidate.idCandidate).FirstOrDefault();
                FocusNewAdmin validarFocus = db.FocusNewAdmins.Where(x => x.idFocusNewAdmin == focuscandidate.idFocusNewAdmin).FirstOrDefault();
                var newFocus = db.focusNewAdmin_candidate.Where(t => t.idFocusNewAdmin == focuscandidate.idFocusNewAdmin && t.idCandidate == focuscandidate.idCandidate).FirstOrDefault();

                if (idfocus == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "el id de la prioridad no existe!");
                }

                if (validarCandidate == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Id candidato: " + focuscandidate.idCandidate + " No registrado");
                }
                
                else if (validarFocus == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Id Prioridad no Registrada ");
                }

                else if (newFocus != null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Prioridad " + focuscandidate.idFocusNewAdmin + " ya se encuentra registrada para el candidato " + focuscandidate.idCandidate);
                }

                idfocus.idfocusNewAdmin_candidate = idfocus.idfocusNewAdmin_candidate;
                idfocus.idFocusNewAdmin = focuscandidate.idFocusNewAdmin;
                idfocus.idCandidate = focuscandidate.idCandidate;

                db.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // POST: api/focusNewAdmin_candidate
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create([FromBody]focusNewAdmin_candidate focuscandidate)
        {
            try
            {

                candidate validarCandidate = db.candidates.Where(x => x.idCandidates == focuscandidate.idCandidate).FirstOrDefault();
                FocusNewAdmin validarFocus = db.FocusNewAdmins.Where(x => x.idFocusNewAdmin == focuscandidate.idFocusNewAdmin).FirstOrDefault();

                var newFocus = db.focusNewAdmin_candidate.Where(t => t.idFocusNewAdmin == focuscandidate.idFocusNewAdmin && t.idCandidate == focuscandidate.idCandidate).FirstOrDefault();

                if (validarCandidate == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Id candidato: " + focuscandidate.idCandidate + " No registrado");
                }

                else if (validarFocus == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Id Prioridad no Registrada ");
                }

                else if (newFocus != null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Prioridad " + focuscandidate.idFocusNewAdmin + " ya se encuentra registrada para el candidato " + focuscandidate.idCandidate);
                }

                db.focusNewAdmin_candidate.FirstOrDefault().idFocusNewAdmin = focuscandidate.idFocusNewAdmin;
                db.focusNewAdmin_candidate.FirstOrDefault().idCandidate = focuscandidate.idCandidate;

                db.focusNewAdmin_candidate.Add(focuscandidate);
                db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            
        }

        // DELETE: api/focusNewAdmin_candidate/5
        [ResponseType(typeof(focusNewAdmin_candidate))]
        [HttpDelete]
        [Route("delete/{id}")]
        public IHttpActionResult DeletefocusNewAdmin_candidate(int id)
        {
            focusNewAdmin_candidate focusNewAdmin_candidate = db.focusNewAdmin_candidate.Find(id);
            if (focusNewAdmin_candidate == null)
            {
                return NotFound();
            }

            db.focusNewAdmin_candidate.Remove(focusNewAdmin_candidate);
            db.SaveChanges();

            return Ok(focusNewAdmin_candidate);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool focusNewAdmin_candidateExists(int id)
        {
            return db.focusNewAdmin_candidate.Count(e => e.idfocusNewAdmin_candidate == id) > 0;
        }
    }
}