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
    [RoutePrefix("api/economicactivity")]
    public class EconomicActivitiesController : ApiController
    {
        private db_citizen4Entities2 db = new db_citizen4Entities2();

        // GET: api/EconomicActivities
        [Route("")]
        public IEnumerable<EconomicActivity> GetEconomicActivities()
        {
            return db.EconomicActivities.ToList();
        }

        // GET: api/EconomicActivities/5
        [ResponseType(typeof(EconomicActivity))]
        [Route("{id:int}")]
        public IHttpActionResult GetEconomicActivity(int id)
        {
            EconomicActivity economicActivity = db.EconomicActivities.Find(id);
            if (economicActivity == null)
            {
                return NotFound();
            }

            return Ok(economicActivity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EconomicActivityExists(int id)
        {
            return db.EconomicActivities.Count(e => e.idEconomicActivity == id) > 0;
        }
    }
}