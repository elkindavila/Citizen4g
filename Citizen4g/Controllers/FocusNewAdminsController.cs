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
    [RoutePrefix("api/economicactivities")]
    public class FocusNewAdminsController : ApiController
    {
        private db_citizen4Entities2 db = new db_citizen4Entities2();

        // GET: api/FocusNewAdmins
        [Route("")]
        public IQueryable<FocusNewAdmin> GetFocusNewAdmins()
        {
            return db.FocusNewAdmins;
        }

        // GET: api/FocusNewAdmins/5
        [ResponseType(typeof(FocusNewAdmin))]
        [Route("{id:int}")]
        public IHttpActionResult GetFocusNewAdmin(int id)
        {
            FocusNewAdmin focusNewAdmin = db.FocusNewAdmins.Find(id);
            if (focusNewAdmin == null)
            {
                return NotFound();
            }

            return Ok(focusNewAdmin);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FocusNewAdminExists(int id)
        {
            return db.FocusNewAdmins.Count(e => e.idFocusNewAdmin == id) > 0;
        }
    }
}