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
        private db_citizen4Entities2 db = new db_citizen4Entities2();

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
                
                var newUser = db.citizen4.Where(c => c.idCitizen4 == citizen4.idCitizen4).FirstOrDefault();

                if (newUser == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "El id del ciudadano no existe! " + citizen4.idCitizen4);
                }

                newUser.idCitizen4 = newUser.idCitizen4;
                newUser.FullName = citizen4.FullName;
                newUser.Age = citizen4.Age;

                if (citizen4.Age <=0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Ingrese una edad valida");

                }

                newUser.FullName = citizen4.FullName;
                newUser.Age = citizen4.Age;
                newUser.Adress = citizen4.Adress;
                newUser.Gender = citizen4.Gender;
                newUser.CellPhone = citizen4.CellPhone;
                newUser.Phone = citizen4.Phone;
                newUser.Email = citizen4.Email;
                newUser.economicActivity = citizen4.economicActivity;
                newUser.educationLevel = citizen4.educationLevel;
                newUser.HeadFamily = citizen4.HeadFamily;
                newUser.EmployeedNow = citizen4.EmployeedNow;
                newUser.WageLevel = citizen4.WageLevel;
                newUser.TypeTransportUse = citizen4.TypeTransportUse;
                newUser.Profession = citizen4.Profession;
                newUser.WorkEast = citizen4.WorkEast;
                newUser.CivilStatus = citizen4.CivilStatus;

                var sector = db.sectors.Where(x => x.idTown == citizen4.idTown);

                if (sector == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "El sector debe corresponder al municipio seleccionado");
                }
                else
                {
                    newUser.idSector = citizen4.idSector;
                }

                
                newUser.idTown = citizen4.idTown;

                var town = db.towns.Where(x => x.idTown == citizen4.idTown);

                if (town == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Se requiere id valido para un municipio!");
                }
                else
                {
                    newUser.idTown = citizen4.idTown;
                }
                

                newUser.idUsers = newUser.idUsers;
                newUser.idCandidates = newUser.idCandidates;
                db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // POST: api/citizen4
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create([FromBody]citizen4 citizen4)
        {

            
            user registroMasActualizado = db.users.OrderByDescending(x => x.idUsers).First();
            var validar = db.citizen4.Where(x => x.idCandidates == citizen4.idCandidates && x.idUsers == registroMasActualizado.idUsers).FirstOrDefault();

            try
            {
                if (validar != null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Usuario " + registroMasActualizado.idUsers + " ya sigue al canditato " + citizen4.idCandidates);
                }

                if (citizen4.idCandidates == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "El ciudaddado debe elegir a un candidato!");
                }

                var sector = db.sectors.Where(x => x.idTown == citizen4.idTown && x.idSector == citizen4.idSector).FirstOrDefault();

                if (sector == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "El sector debe corresponder al municipio seleccionado");
                }
                else
                {
                   citizen4.idSector= citizen4.idSector;
                }

                var town = db.towns.Where(x => x.idTown == citizen4.idTown).FirstOrDefault();

                if (town == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Se requiere id valido para un municipio!");
                }
                else
                {
                   citizen4.idTown= citizen4.idTown;
                }

                if (citizen4.Age <= 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Ingrese una edad valida");
                }


                citizen4.idUsers = registroMasActualizado.idUsers;

                db.citizen4.Add(citizen4);
                db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK); ;
            }
            catch(Exception ex)
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