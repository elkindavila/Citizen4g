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
    [RoutePrefix("api/civilstatus")]
    public class CivilStatusController : ApiController
    {

        private db_citizen4Entities2 db = new db_citizen4Entities2();

        // GET: api/CivilStatus
        [Route("")]
        public IQueryable<CivilStatu> GetCivilStatus()
        {
            return db.CivilStatus;
        }

        // GET: api/CivilStatus/5
        [ResponseType(typeof(CivilStatu))]
        [Route("{id:int}")]
        public IHttpActionResult GetCivilStatu(int id)
        {
            CivilStatu civilStatu = db.CivilStatus.Find(id);
            if (civilStatu == null)
            {
                return NotFound();
            }

            return Ok(civilStatu);
        }

       protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CivilStatuExists(int id)
        {
            return db.CivilStatus.Count(e => e.idCivilStatus == id) > 0;
        }
    }
}