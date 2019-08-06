using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Citizen4g.Models;

namespace Citizen4g.Controllers
{
    [RoutePrefix("api/messages")]
    public class messageController : ApiController
    {
        private db_citizen4Entities1 db = new db_citizen4Entities1();

        // GET: api/message
        [Route("")]
        public IEnumerable<msg_citizen4_candidates> Getmsg_citizen4_candidates()
        {
            return db.msg_citizen4_candidates.ToList();
        }

        // GET: api/message/5
        [Route("{id:int}")]
        [ResponseType(typeof(msg_citizen4_candidates))]
        public IHttpActionResult Getmsg_citizen4_candidates(int id)
        {
            msg_citizen4_candidates msg_citizen4_candidates = db.msg_citizen4_candidates.Find(id);
            if (msg_citizen4_candidates == null)
            {
                return NotFound();
            }

            return Ok(msg_citizen4_candidates);
        }

        [HttpGet]
        [Route("type/{type}")]
        [ResponseType(typeof(msg_citizen4_candidates))]
        public IHttpActionResult messagesxtype(int type)
        {


            msg_citizen4_candidates msgtype = db.msg_citizen4_candidates.Where(x=> x.idMessageType == type).FirstOrDefault();
            if (msgtype == null)
            {
                return NotFound();
            }

            return Ok(msgtype);


            //var mtype = db.msg_citizen4_candidates.Where(x => x.idMessageType == type).ToList();

            //if (mtype != null)
            //{
            //    return Ok(mtype);

            //}

            //return NotFound();


            //var msgType = db.Database.SqlQuery<msg_citizen4_candidates>(
            //    @"SELECT *
            //      FROM msg_citizen4_candidates
            //      WHERE idMessageType = @type", new SqlParameter("@type", type));
            //return Ok(msgType);

        }



        // PUT: api/message/5

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update([FromBody]msg_citizen4_candidates message)
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                var newUser = db.msg_citizen4_candidates.Single(t => t.idMessageCitizen4 == message.idMessageCitizen4);

                newUser.Title = message.Title;
                newUser.Description = message.Description;
                newUser.Link = message.Link;
                newUser.Date = message.Date;
                newUser.Image = message.Image;
                newUser.Answer = message.Answer;
                newUser.idCandidates = message.idCandidates;
                newUser.idCitizen4 = message.idCitizen4;
                newUser.idMessageType = message.idMessageType;
                db.SaveChanges();
                return result;
            }
            catch
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // POST: api/message


        [HttpPost]
        [Route("enviarMsgCitizens")]
        public HttpResponseMessage EnviarMsg([FromBody] msg_citizen4_candidates msg)
        {
            try
            {

                var result = new HttpResponseMessage(HttpStatusCode.OK);
                var candidate = db.candidates.FirstOrDefault(c => c.idCandidates == msg.idCandidates);
                var citizen = candidate.citizen4;

                using (var contex = new db_citizen4Entities1())
                {
                    var msgCanCit = new msg_citizen4_candidates();


                    foreach (var dato in citizen)
                    {
                        msgCanCit.idCitizen4 = dato.idCitizen4;
                        msgCanCit.Title = msg.Title;
                        msgCanCit.Description = msg.Description;
                        msgCanCit.Link = msg.Link;
                        msgCanCit.Date = Convert.ToDateTime(msg.Date);
                        msgCanCit.Image = msg.Image;
                        msgCanCit.Answer = msg.Answer;
                        msgCanCit.idCandidates = msg.idCandidates;
                        msgCanCit.idMessageType = msg.idMessageType;

                        contex.msg_citizen4_candidates.Add(msgCanCit);
                        contex.SaveChanges();
                    }

                }

                return result;

            }
            catch
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }


        // DELETE: api/message/5
        [ResponseType(typeof(msg_citizen4_candidates))]
        [HttpDelete]
        [Route("delete/{id}")]
        public IHttpActionResult Deletemsg_citizen4_candidates(int id)
        {
            msg_citizen4_candidates msg_citizen4_candidates = db.msg_citizen4_candidates.Find(id);
            if (msg_citizen4_candidates == null)
            {
                return NotFound();
            }

            db.msg_citizen4_candidates.Remove(msg_citizen4_candidates);
            db.SaveChanges();

            return Ok(msg_citizen4_candidates);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool msg_citizen4_candidatesExists(int id)
        {
            return db.msg_citizen4_candidates.Count(e => e.idMessageCitizen4 == id) > 0;
        }
    }
}