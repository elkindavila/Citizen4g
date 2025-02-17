﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Citizen4g.Models;

namespace Citizen4g.Controllers
{
    [RoutePrefix("api/messages")]
    public class messageController : ApiController
    {
        private db_citizen4Entities2 db = new db_citizen4Entities2();

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


            msg_citizen4_candidates msgtype = db.msg_citizen4_candidates.Where(x => x.idMessageType == type).FirstOrDefault();
            if (msgtype == null)
            {
                return NotFound();
            }

            return Ok(msgtype);

        }

        // PUT: api/message/5

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update([FromBody]msg_citizen4_candidates message)
        {
            try
            {
               
                var newUser = db.msg_citizen4_candidates.Single(t => t.idMessageCitizen4 == message.idMessageCitizen4);

                newUser.Title = message.Title;
                newUser.Description = message.Description;
                newUser.Link = message.Link;
                newUser.Date = Convert.ToDateTime(message.Date);
                newUser.Image = message.Image;
                newUser.Answer = newUser.Answer;
                newUser.idCandidates = newUser.idCandidates;
                newUser.idCitizen4 = newUser.idCandidates;
                newUser.idMessageType = message.idMessageType;
                db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // POST: api/message
        [HttpPost]
        [Route("enviarmessage")]
        public HttpResponseMessage Enviarmessage([FromBody]msg_citizen4_candidates msgs)
        {
            try
            {

                var candidate = db.candidates.FirstOrDefault(c => c.idCandidates == msgs.idCandidates);
                var citizen = candidate.citizen4;

                using (var contex = new db_citizen4Entities2())
                {
                    var msgCanCit = new msg_citizen4_candidates();


                    foreach (var dato in citizen)
                    {
                        msgCanCit.idCitizen4 = dato.idCitizen4;
                        msgCanCit.Title = msgs.Title;
                        msgCanCit.Description = msgs.Description;
                        msgCanCit.Link = msgs.Link;
                        msgCanCit.Date = Convert.ToDateTime(msgs.Date);
                        msgCanCit.Image = msgs.Image;
                        msgCanCit.Answer = msgs.Answer;
                        msgCanCit.idCandidates = msgs.idCandidates;
                        msgCanCit.idMessageType = msgs.idMessageType;

                        contex.msg_citizen4_candidates.Add(msgCanCit);
                        contex.SaveChanges();
                    }

                }

                return Request.CreateResponse(HttpStatusCode.OK,"Mensages enviados correctamente! para el candidado " + msgs.idCandidates); 
            }
            catch (Exception ex)
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // POST: api/createimage
        [HttpPut]
        [Route("createimage/{idmessage}")]
        public async Task<HttpResponseMessage> CreateImage(int idmessage)
        {
            try
            {
                byte[] fileBytes;
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                fileBytes = await uploadFile(idmessage);
                var message = db.msg_citizen4_candidates.Single(c => c.idMessageCitizen4 == idmessage);
                message.Image = fileBytes;
                db.SaveChanges();
                return result;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        public async Task<byte[]> uploadFile(int idmessage)
        {
            var ctx = HttpContext.Current;
            var root = ctx.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);
            byte[] fileBytes;
            try
            {
                await Request.Content
                    .ReadAsMultipartAsync(provider);

                foreach (var file in provider.FileData)
                {
                    var name = file.Headers.ContentDisposition.FileName;

                    //remove double quotes from string
                    name = name.Trim('"');

                    var localFileName = file.LocalFileName;
                    var filePath = Path.Combine(root, name);

                    fileBytes = SaveFileBinary(localFileName, name, idmessage);
                    return fileBytes;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return null;
        }

        // GUARDA FILE BINARY
        private byte[] SaveFileBinary(string localFile, string fileName, int idmessage)
        {
            // Get file binary
            byte[] fileBytes;
            using (var fs = new FileStream(
                localFile, FileMode.Open, FileAccess.Read))
            {
                fileBytes = new byte[fs.Length];
                fs.Read(
                    fileBytes, 0, Convert.ToInt32(fs.Length));
            }

            return fileBytes;
        }


        [HttpPost]
        [Route("preguntaalcandidato")]
        public HttpResponseMessage Qcandidate([FromBody] msg_citizen4_candidates qu)
        {

            //metodo que permite que el cidadano registrado realice preguntas a su candidato
            citizen4 citizen = db.citizen4.Where(x => x.idCitizen4 == qu.idCitizen4).FirstOrDefault();

            msg_citizen4_candidates mess = new msg_citizen4_candidates();

            if (citizen != null)
            {

                mess.Title = qu.Title;
                mess.Description = qu.Description;
                mess.Link = qu.Link;
                mess.Date = Convert.ToDateTime(qu.Date);
                mess.Image = qu.Image;
                mess.Answer = qu.Answer;
                mess.idCandidates = citizen.idCandidates.Value;
                mess.idCitizen4 = qu.idCitizen4;
                mess.idMessageType = 6;

                db.msg_citizen4_candidates.Add(mess);
                db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "El id del ciudadano " + qu.idCitizen4 + " No existe! " );

        }

        [HttpPost]
        [Route("answer/{idCitizen},{idmessage},{answerc}")]
        public HttpResponseMessage answerc(int idCitizen, int idmessage, string answerc)
        {

            //metodo que permite que el cidadano registrado realice preguntas a su candidato
            var answer = db.msg_citizen4_candidates.Where(x=> x.idCitizen4 == idCitizen && x.idMessageCitizen4== idmessage && x.idMessageType == 6).FirstOrDefault();

            if (answer == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No existen preguntas para el id Citizen : " + idCitizen);

            }

            answer.Title = answer.Title;
            answer.Description = answer.Description;
            answer.Link = answer.Link;
            answer.Date = Convert.ToDateTime(answer.Date);
            answer.Image = answer.Image;
            answer.Answer = answerc;
            answer.idCandidates = answer.idCandidates;
            answer.idCitizen4 = answer.idCitizen4;
            answer.idMessageType = answer.idMessageType;

            db.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.OK);

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