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
    [RoutePrefix("api/educationlevels")]
    public class EducationLevelsController : ApiController
    {
        private db_citizen4Entities2 db = new db_citizen4Entities2();

        // GET: api/EducationLevels
        [Route("")]
        public IQueryable<EducationLevel> GetEducationLevels()
        {
            return db.EducationLevels;
        }

        // GET: api/EducationLevels/5
        [ResponseType(typeof(EducationLevel))]
        [Route("{id:int}")]
        public IHttpActionResult GetEducationLevel(int id)
        {
            EducationLevel educationLevel = db.EducationLevels.Find(id);
            if (educationLevel == null)
            {
                return NotFound();
            }

            return Ok(educationLevel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EducationLevelExists(int id)
        {
            return db.EducationLevels.Count(e => e.idEducationLevel == id) > 0;
        }
    }
}