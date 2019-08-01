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

        private db_citizen4Entities1 db = new db_citizen4Entities1();

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
        [Route("profile/{loguin}")]
        public IHttpActionResult Getperfil(string loguin)
        {

            using (var context = new db_citizen4Entities1())

            {


                var User = context.users.Include(x => x.users_profiles).Where(y => y.LoginUsers == loguin).ToList();
                //var perfil = context.users_profiles.Include(x => x.users).Where(z=> z.idProfiles==User).ToList();

                //var msgCanCit = new msg_citizen4_candidates();

                var result = User.FirstOrDefault();
                var c = result.users_profiles.Where(x => x.idProfiles == 2);

                

                    return null;
                }


        }


            

            //citizen4 citizen = new citizen4();
            //using (var contex = new db_citizen4Entities1())
            //{
            //    var users = contex.users.FirstOrDefault(c => c.LoginUsers == loguin);
            //    var perfil = users.users_profiles;

            //   var datosperfilc = new profile();
            //    var datosperfilcan = new candidate();


            //        if (datosperfilc.idProfiles== 2)
            //        {
            //            citizen = contex.citizen4.Where(x => x.idUsers == datosperfilcan.idUsers).FirstOrDefault();
            //            return citizen;


            //        }
            //        else
            //        {
            //            return null;
            //        }


            //    }
        

        [HttpGet]
        [Route("validate/{login}")]
        public HttpResponseMessage findByName(string login)
        {
            try
            {
                db.users.Single(t => t.LoginUsers == login);
                return new HttpResponseMessage(HttpStatusCode.OK);
               
            }
            catch
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }


        [HttpGet]
        [Route("validateup/{login},{pass}")]
        public HttpResponseMessage findup(string login, string pass)
        {
            try
            {
                var us= db.users.Single(t => t.LoginUsers == login);
                var ps = db.users.Single(p => p.PassUsers == pass);

                return new HttpResponseMessage(HttpStatusCode.OK);

            }
            catch
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
                var newUser = db.users.Single(t => t.idUsers == user.idUsers);

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
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                db.users.Add(user);
                db.SaveChanges();
                return result;
            }
            catch
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
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