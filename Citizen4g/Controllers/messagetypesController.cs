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
    [RoutePrefix("api/messagestype")]
    public class messagetypesController : ApiController
    {
        private db_citizen4Entities1 db = new db_citizen4Entities1();

        // GET: api/messagetypes
        [Route("")]
        public IEnumerable<messagetype> Getmessagetypes()
        {
            return db.messagetypes.ToList();
        }

        // GET: api/messagetypes/5
        [ResponseType(typeof(messagetype))]
        [Route("{id:int}")]
        public IHttpActionResult Getmessagetype(int id)
        {
            messagetype messagetype = db.messagetypes.Find(id);
            if (messagetype == null)
            {
                return NotFound();
            }

            return Ok(messagetype);
        }

        //PUT

        [HttpPut]
        [Route("update")]
        // Recibe como argumento el Models user y compara el idUsers proporcionado para realizar el update
        public HttpResponseMessage Update([FromBody]messagetype msg)
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                var newUser = db.messagetypes.Single(m => m.id_MessageType == msg.id_MessageType);

                newUser.DescriptionType = msg.DescriptionType;
                db.SaveChanges();
                return result;
            }
            catch
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // POST: api/messagetypes
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create([FromBody]messagetype msg)
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                db.messagetypes.Add(msg);
                db.SaveChanges();
                return result;
            }
            catch
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // DELETE
        [ResponseType(typeof(user))]
        [HttpDelete]
        [Route("delete/{id}")]

        /// Recibe argumento "int id" for Delete
        public IHttpActionResult DeleteMsg(int id)
        {
            messagetype msg = db.messagetypes.Find(id);
            if (msg == null)
            {
                return NotFound();
            }

            db.messagetypes.Remove(msg);
            db.SaveChanges();

            return Ok(msg);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool messagetypeExists(int id)
        {
            return db.messagetypes.Count(e => e.id_MessageType == id) > 0;
        }
    }
}