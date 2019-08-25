using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Description;
using Citizen4g.Models;
using Newtonsoft.Json;

namespace Citizen4g.Controllers
{
    [RoutePrefix("api/users")]
    public class usersController : ApiController
    {

        private db_citizen4Entities2 db = new db_citizen4Entities2();

        // GET: api/users
        [Route("")]
        /// Lista all users
        public IEnumerable<user> Getusers()
        {

            return db.users.ToList();
        }

        // GET: api/users/5
        [ResponseType(typeof(user))]
        [Route("{id:int}")]
        /// Lista users por "int id" 
        public IHttpActionResult Getuser(int id)
        {
            user user = db.users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        // GET: api/users/5

        [HttpGet]
        [ResponseType(typeof(user))]
        [Route("profile/{loguin}")]
        public IHttpActionResult Getperfil(string loguin)
        {
            //candidato
            user user = db.users.Where(x => x.LoginUsers == loguin && x.users_profiles.FirstOrDefault().idProfiles == 4).FirstOrDefault();
            if (user == null)
            { 
                //citizen
                 user = db.users.Where(x => x.LoginUsers == loguin && x.users_profiles.FirstOrDefault().idProfiles == 2).FirstOrDefault();
            }
            return Ok(user);

        }

        [HttpGet]
        [Route("validateup/{login},{pass}")]
        public HttpResponseMessage findup(string login, string pass)
        {
            try
            {
                var us = db.users.Where(x => x.LoginUsers == login && x.PassUsers == pass).FirstOrDefault();
            
                if (us == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Usuario o contraseña incorrecta ");

                }

                return new HttpResponseMessage(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        //PUT

        [HttpPut]
        [Route("update")]
        /// Recibe como argumento el Models user y compara el idUsers proporcionado para realizar el update
        public HttpResponseMessage Update([FromBody]user user)
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);

                var newUser = db.users.Where(x => x.idUsers == user.idUsers && x.LoginUsers == user.LoginUsers).FirstOrDefault();

                 if (newUser == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "El id user y Nombre de user no se corresponden: " + user.idUsers);
                }

                newUser.idUsers = newUser.idUsers;
                newUser.LoginUsers = user.LoginUsers;
                newUser.PassUsers = user.PassUsers;
                db.SaveChanges();
                return result;
            }
            catch
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // POST: api/users


        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create([FromBody]user user)
        {
            
           var exuser = db.users.Where(x=> x.LoginUsers == user.LoginUsers).FirstOrDefault();

                if (exuser != null)
                {

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Nombre de usuario ya existe " + user.LoginUsers);

                }

                db.users.Add(user);

                users_profiles userprofile = new users_profiles
                {
                    idUsers = user.idUsers,
                    idProfiles = 2
                };

          
                db.users_profiles.Add(userprofile);

                db.SaveChanges();
                return new HttpResponseMessage(HttpStatusCode.OK);
        }
           


        // DELETE: api/users/5
        [ResponseType(typeof(user))]
        [HttpDelete]
        [Route("delete/{id}")]

        /// Recibe argumento "int id" for Delete
        public IHttpActionResult Deleteuser(int id)
        {
            user user = db.users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool userExists(int id)
        {
            return db.users.Count(e => e.idUsers == id) > 0;
        }
    }
}