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
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "El id del ciudadano no existe! " + citizen4.idCitizen4);
                else  
                newUser.idCitizen4 = newUser.idCitizen4;

                if (citizen4.FullName == "" || citizen4.FullName == null)
                    newUser.FullName = newUser.FullName;
                else
                newUser.FullName = citizen4.FullName;

                if (citizen4.Birthday == null)
                    newUser.Birthday = newUser.Birthday;
                else
                newUser.Birthday = citizen4.Birthday;

                // calcular edad
                if(citizen4.Birthday != null) { 
                DateTime fechanac = citizen4.Birthday.Value;
                int anos = System.DateTime.Now.Year - fechanac.Year;
                

                if (anos <=0)                
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Verifique la fecha de nacimiento");
                else
                    newUser.Age = anos;
                }

                if (citizen4.Adress == "" || citizen4.Adress == null)
                    newUser.Adress = newUser.Adress;
                else
                    newUser.Adress = citizen4.Adress;

                if (citizen4.Gender == "" || citizen4.Gender == null)
                    newUser.Gender = newUser.Gender;
                else
                    newUser.Gender = citizen4.Gender;

                if (citizen4.CellPhone == "" || citizen4.CellPhone == null)
                    newUser.CellPhone = newUser.CellPhone;
                else
                    newUser.CellPhone = citizen4.CellPhone;

                if (citizen4.Phone == "" || citizen4.Phone == null)
                    newUser.Phone = newUser.Phone;
                else
                    newUser.Phone = citizen4.Phone;

                if (citizen4.Email == "" || citizen4.Email == null)
                    newUser.Email = newUser.Email;
                else
                    newUser.Email = citizen4.Email;

                if (citizen4.economicActivity == null)
                    newUser.economicActivity = newUser.economicActivity;
                else
                    newUser.economicActivity = citizen4.economicActivity;

                if (citizen4.educationLevel == null)
                    newUser.educationLevel = newUser.educationLevel;
                else
                    newUser.educationLevel = citizen4.educationLevel;

                if (citizen4.HeadFamily == null)
                    newUser.HeadFamily = newUser.HeadFamily;
                else
                    newUser.HeadFamily = citizen4.HeadFamily;

                if (citizen4.EmployeedNow == null)
                    newUser.EmployeedNow = newUser.EmployeedNow;
                else
                    newUser.EmployeedNow = citizen4.EmployeedNow;

                if (citizen4.WageLevel == "" || citizen4.WageLevel == null)
                    newUser.WageLevel = newUser.WageLevel;
                else
                    newUser.WageLevel = citizen4.WageLevel;

                if (citizen4.TypeTransportUse == null)
                    newUser.TypeTransportUse = newUser.TypeTransportUse;
                else
                    newUser.TypeTransportUse = citizen4.TypeTransportUse;

                if (citizen4.Profession == "" || citizen4.Profession == null)
                    newUser.Profession = newUser.Profession;
                else
                    newUser.Profession = citizen4.Profession;

                if (citizen4.WorkEast == null)
                    newUser.WorkEast = newUser.WorkEast;
                else
                    newUser.WorkEast = citizen4.WorkEast;

                if (citizen4.CivilStatus == null)
                    newUser.CivilStatus = newUser.CivilStatus;
                else
                    newUser.CivilStatus = citizen4.CivilStatus;

                if (citizen4.idTown == 0)
                    newUser.idTown = newUser.idTown;
                else
                { 
                var town = db.towns.Where(x => x.idTown == citizen4.idTown);

                if (town == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Se requiere id valido para un municipio!");
                else
                    newUser.idTown = citizen4.idTown;
                }

                if (citizen4.idSector == null)
                    newUser.idSector = newUser.idSector;
                else
                { 
                var sector = db.sectors.Where(x => x.idTown == citizen4.idTown);

                if (sector == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "El sector debe corresponder al municipio seleccionado");
                else
                    newUser.idSector = citizen4.idSector;
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

            // calcular edad
            DateTime fechanac = citizen4.Birthday.Value;
            int anos = System.DateTime.Now.Year - fechanac.Year;

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

                if (anos <= 0)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "edad debe ser mayor a 0 años");
                else
                    citizen4.Age = anos;

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