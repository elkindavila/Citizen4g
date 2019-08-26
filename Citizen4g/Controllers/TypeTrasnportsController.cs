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
    public class TypeTrasnportsController : ApiController
    {
        private db_citizen4Entities2 db = new db_citizen4Entities2();

        // GET: api/TypeTrasnports
        public IQueryable<TypeTrasnport> GetTypeTrasnports()
        {
            return db.TypeTrasnports;
        }

        // GET: api/TypeTrasnports/5
        [ResponseType(typeof(TypeTrasnport))]
        public IHttpActionResult GetTypeTrasnport(int id)
        {
            TypeTrasnport typeTrasnport = db.TypeTrasnports.Find(id);
            if (typeTrasnport == null)
            {
                return NotFound();
            }

            return Ok(typeTrasnport);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TypeTrasnportExists(int id)
        {
            return db.TypeTrasnports.Count(e => e.idTypeTrasnport == id) > 0;
        }
    }
}