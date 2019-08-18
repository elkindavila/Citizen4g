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
    [RoutePrefix("api/candidates")]
    public class candidatesController : ApiController
    {
        private db_citizen4Entities1 db = new db_citizen4Entities1();

        // GET: api/candidates
        [Route("")]
        public IEnumerable<candidate> Getcandidates()
        {
            return db.candidates.ToList();
        }

        // GET: api/candidates/5
        [ResponseType(typeof(candidate))]
        [Route("{id:int}")]
        public IHttpActionResult Getcandidate(int id)
        {
            candidate candidate = db.candidates.Find(id);
            if (candidate == null)
            {
                return NotFound();
            }

            return Ok(candidate);
        }

        [HttpGet]
        [Route("validate/{idcard}")]
        public HttpResponseMessage findByName(int idcard)
        {
            try
            {
                db.candidates.Single(t => t.idCard == idcard);
                return new HttpResponseMessage(HttpStatusCode.OK);

            }
            catch
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }


        [HttpGet]
        [ResponseType(typeof(candidate))]
        [Route("estadistica/{idCandidate},{idEstadistica}")]
        public HttpResponseMessage estadisticas(int idCandidate, int idEstadistica)
        {
            try
            {
               

                if (idEstadistica >=1 && idEstadistica <= 10)
                {
                    if (idEstadistica == 1)
                    {
                        // ciudadanos del candidato
                        var estadistica1 = (from c in db.citizen4
                                           join cn in db.candidates on c.idTown equals cn.idTown
                                           where c.idCandidates == idCandidate
                                           select cn).FirstOrDefault();                  

                        if (estadistica1 == null)
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "El candidato no tiene seguidores");
                        }

                        return Request.CreateResponse(HttpStatusCode.OK,estadistica1); ;
                    }

                    if (idEstadistica == 2)
                    {

                        // Necesidades de los ciudadanos por candidato
                        var estadistica2 = (from nc in db.needs_citizen4
                                            join ct in db.citizen4 on nc.idCitizen4 equals ct.idCitizen4
                                            where ct.idCandidates == idCandidate
                                            select ct).FirstOrDefault();

                        if (estadistica2 == null)
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No registran necesidades de ciudadanos para el candidato " + idCandidate);
                        }

                        return Request.CreateResponse(HttpStatusCode.OK, estadistica2); ;
                    }

                    if (idEstadistica == 3)
                    {
                        // prioridades de los ciudadanos por candidato
                        var estadistica3 = (from nc in db.focus_citizen4
                                            join ct in db.citizen4 on nc.idCitizen4 equals ct.idCitizen4
                                            where ct.idCandidates == idCandidate
                                            select ct).FirstOrDefault();

                        if (estadistica3 == null)
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No registran prioridades de ciudadanos para el candidato " + idCandidate);
                        }

                        return Request.CreateResponse(HttpStatusCode.OK, estadistica3); ;
                    }

                }

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "El codigo de la estadisticas debe estar entre 1 y 10");

            }
            catch
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }


        // PUT: api/candidates/5
        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update([FromBody]candidate candidate)
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                var newUser = db.candidates.Single(c => c.idCandidates == candidate.idCandidates);

                newUser.idCard = candidate.idCard;
                newUser.FullName = candidate.FullName;
                newUser.Email = candidate.Email;
                newUser.Image = candidate.Image;
                newUser.Description = candidate.Description;
                newUser.DescriptionCampaign = candidate.DescriptionCampaign;
                newUser.LinkCampaign = candidate.LinkCampaign;
                newUser.LogoCampaign = candidate.LogoCampaign;
                newUser.idTown = candidate.idTown;
                newUser.idUsers = candidate.idUsers;
                
                db.SaveChanges();
                return result;
            }
            catch
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // POST: api/candidates
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create([FromBody]candidate candidate)
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                db.candidates.Add(candidate);
                db.SaveChanges();
                return result;
            }
            catch
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // DELETE: api/candidates/5
        [ResponseType(typeof(candidate))]
        [HttpDelete]
        [Route("delete/{id}")]
        public IHttpActionResult Deletecandidate(int id)
        {
            candidate candidate = db.candidates.Find(id);
            if (candidate == null)
            {
                return NotFound();
            }

            db.candidates.Remove(candidate);
            db.SaveChanges();

            return Ok(candidate);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool candidateExists(int id)
        {
            return db.candidates.Count(e => e.idCandidates == id) > 0;
        }
    }
}