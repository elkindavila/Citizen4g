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
    [RoutePrefix("api/users")]
    public class usersController : ApiController
    {

        private db_citizen4Entities1 db = new db_citizen4Entities1();

        // GET: api/users
        [Route("")]
        public IEnumerable<user> Getusers()
        {

            return db.users.ToList();
        }

        // GET: api/users/5
        [ResponseType(typeof(user))]
        [Route("{id:int}")]
        public IHttpActionResult Getuser(int id)
        {
            user user = db.users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        //PUT

        [HttpPut]
        [Route("update")]
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