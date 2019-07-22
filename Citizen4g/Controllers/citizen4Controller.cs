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
    [RoutePrefix("api/citizen4")]
    public class citizen4Controller : ApiController
    {
        private db_citizen4Entities1 db = new db_citizen4Entities1();

        // GET: api/citizen4
        [Route("")]
        public IEnumerable<citizen4> Getcitizen4()
        {
            return db.citizen4.ToList();
        }

        // GET: api/citizen4/5
        
        [ResponseType(typeof(citizen4))]
        [Route("{id:int}")]
        public IHttpActionResult Getcitizen4(int id)
        {
            citizen4 citizen4 = db.citizen4.Find(id);
            if (citizen4 == null)
            {
                return NotFound();
            }

            return Ok(citizen4);
        }

        // PUT: api/citizen4/5

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update([FromBody]citizen4 citizen4)
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                var newUser = db.citizen4.Single(c => c.idCitizen4 == citizen4.idCitizen4);

                newUser.FullName = citizen4.FullName;
                newUser.Age = citizen4.Age;
                newUser.Adress = citizen4.Adress;
                newUser.Gender = citizen4.Gender;
                newUser.CellPhone = citizen4.CellPhone;
                newUser.Phone = citizen4.Phone;
                newUser.Email = citizen4.Email;
                newUser.EconomicActivity = citizen4.EconomicActivity;
                newUser.EducationLevel = citizen4.EducationLevel;
                newUser.HeadFamily = citizen4.HeadFamily;
                newUser.EmployeedNow = citizen4.EmployeedNow;
                newUser.WageLevel = citizen4.WageLevel;
                newUser.Profession = citizen4.Profession;
                newUser.TypeTransportUse = citizen4.TypeTransportUse;
                newUser.WorkEast = citizen4.WorkEast;
                newUser.idTown = citizen4.idTown;
                newUser.idUsers = citizen4.idUsers;
                newUser.idCandidates = citizen4.idCandidates;
                db.SaveChanges();
                return result;
            }
            catch
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // POST: api/citizen4
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create([FromBody]citizen4 citizen4)
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                db.citizen4.Add(citizen4);
                db.SaveChanges();
                return result;
            }
            catch
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // DELETE: api/citizen4/5
        [ResponseType(typeof(citizen4))]
        [HttpDelete]
        [Route("delete/{id}")]
        public IHttpActionResult Deletecitizen4(int id)
        {
            citizen4 citizen4 = db.citizen4.Find(id);
            if (citizen4 == null)
            {
                return NotFound();
            }

            db.citizen4.Remove(citizen4);
            db.SaveChanges();

            return Ok(citizen4);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool citizen4Exists(int id)
        {
            return db.citizen4.Count(e => e.idCitizen4 == id) > 0;
        }
    }
}