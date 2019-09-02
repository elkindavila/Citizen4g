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
using System.Net.Http.Handlers;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Citizen4g.Controllers
{

    //comment to push
    [RoutePrefix("api/towns")]                  
    public class townsController : ApiController
    {
        private db_citizen4Entities2 db = new db_citizen4Entities2();

        // GET: api/towns
        [Route("")]                            
        public IEnumerable<town> Gettowns()
        {
            return db.towns.ToList();        //Cambiar ToArray por ToList
        }

        //************************

        //GET: api/towns/5

        [ResponseType(typeof(town))]
        [Route("{id:int}")]                                  
        public IHttpActionResult Gettown(int id)
        {
            town town = db.towns.Find(id);
            if (town == null)
            {
                return NotFound();
            }

            return Ok(town);
        }



        [HttpGet]
        [Route("validate/{town}")]
        public HttpResponseMessage findByName(string town)
        {
            try
            {
                db.towns.Single(t => t.Name == town);
                return new HttpResponseMessage(HttpStatusCode.OK);

            }
            catch
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }



        // BUSQUEDA-1 DE REGISTRO POR NOMBRE

        //[HttpGet]
        //[Route("validate/{name}")]
        //public HttpResponseMessage findByName(string name)
        //{
        //    try
        //    {
        //        var result = new HttpResponseMessage(HttpStatusCode.OK);
        //        result.Content = new StringContent(JsonConvert.SerializeObject
        //            (db.towns.Single(t => t.Name == name)));
        //        result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //        return result;
        //    }
        //    catch
        //    {

        //        return new HttpResponseMessage(HttpStatusCode.BadRequest);
        //    }
        //}


        // BUSQUEDA-2 DE REGISTRO POR NOMBRE


        //[ResponseType(typeof(town))]
        //[Route("{name}")]
        //public IHttpActionResult Gettown(string name)
        //{
        //    List<town> town = db.towns.Where(t => t.Name == name).ToList();
        //    if (town == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(town);
        //}

        //**********************************

        //PUT 

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update([FromBody]town town)
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                var newTown = db.towns.Single(t => t.idTown == town.idTown);

                newTown.Name = town.Name;
                db.SaveChanges();
                return result;
            }
            catch
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        //**************************************

        // POST: api/towns

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create([FromBody]town town)
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                db.towns.Add(town);
                db.SaveChanges();
                return result;
            }
            catch
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        //CREATE

        //[ResponseType(typeof(town))]
        //[HttpPost]
        //[Route("create")]
        //public IHttpActionResult Posttown(town town)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.towns.Add(town);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = town.idTown }, town);
        //}


        //*********************************************

        // DELETE: api/towns/5

        //NOTA:  No elimina registros cuya PK este siendo utilizada como FK en otras tablas

        [ResponseType(typeof(town))]
        [HttpDelete]
        [Route("delete/{id}")]
        public IHttpActionResult Deletetown(int id)
        {
            town town = db.towns.Find(id);
            if (town == null)
            {
                return NotFound();
            }

            db.towns.Remove(town);
            db.SaveChanges();

            return Ok(town);
        }

        //ELIMINA   
        //NOTA:  No elimina registros cuya PK este siendo utilizada como FK en otras tablas

        //[HttpDelete]
        //[Route("delete/{id}")]
        //public HttpResponseMessage Delete(int id)
        //{
        //    try
        //    {
        //        var result = new HttpResponseMessage(HttpStatusCode.OK);
        //        db.towns.Remove(db.towns.Single(p => p.idTown==id));
        //        db.SaveChanges();
        //        return result;
        //    }
        //    catch
        //    {

        //        return new HttpResponseMessage(HttpStatusCode.BadRequest);
        //    }
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool townExists(int id)
        {
            return db.towns.Count(e => e.idTown == id) > 0;
        }
    }
}