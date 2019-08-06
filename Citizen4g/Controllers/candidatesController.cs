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