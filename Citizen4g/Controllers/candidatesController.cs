using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Citizen4g.DTO;
using Citizen4g.Models;

namespace Citizen4g.Controllers
{
    [RoutePrefix("api/candidates")]
    public class candidatesController : ApiController
    {
        private db_citizen4Entities2 db = new db_citizen4Entities2();

        // GET: api/candidates
        [Route("")]
        public IEnumerable<candidate> Getcandidates()
        {
            return db.candidates.ToList();
        }

        // GET: api/candidates/5
        [ResponseType(typeof(candidate))]
        [Route("{id:int}")]
        public IHttpActionResult Getcandidate(int id)
        {
            candidate candidate = db.candidates.Find(id);
            if (candidate == null)
            {
                return NotFound();
            }

            return Ok(candidate);
        }

        [HttpGet]
        [Route("validate/{idcard}")]
        public HttpResponseMessage findByName(int idcard)
        {
            try
            {
                db.candidates.Single(t => t.idCard == idcard);
                return new HttpResponseMessage(HttpStatusCode.OK);

            }
            catch
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [Route("citizensXcandidate/{idcandidate}")]
        public IEnumerable<CandidatoDto> GetcitizensXcandidate(int idcandidate)
        {
            try
            {
                var CiuxCandidates = db
                        .candidates
                        .Where(t => t.idCandidates == idcandidate)
                        .Include("Citizen4")
                        .Select(t => new CandidatoDto
                        {
                            idCandidates = t.idCandidates,
                            idCard = t.idCard,
                            FullName = t.FullName,
                            Email = t.Email,
                            Image = t.Image,
                            Description = t.Description,
                            DescriptionCampaign = t.DescriptionCampaign,
                            LinkCampaign = t.LinkCampaign,
                            idTown = t.idTown,
                            idUsers = t.idUsers,
                            CitizensxCandidate = t.citizen4.Select(p => new CiudadanosDto
                            {
                                idCitizen4 = p.idCitizen4,
                                FullName = p.FullName,
                                Age = p.Age,
                                Adress = p.Adress,
                                Gender = p.Gender,
                                CellPhone = p.CellPhone,
                                Phone = p.Phone,
                                Email = p.Email,
                                economicActivity = p.economicActivity,
                                educationLevel = p.educationLevel,
                                HeadFamily = p.HeadFamily,
                                EmployeedNow = p.EmployeedNow,
                                WageLevel = p.WageLevel,
                                TypeTransportUse = p.TypeTransportUse,
                                Profession = p.Profession,
                                WorkEast = p.WorkEast,
                                CivilStatus = p.CivilStatus,
                                idTown = p.idTown,
                                idSector = p.idSector,
                                idUsers = p.idUsers,
                                idCandidates = p.idCandidates

                            })
                        }).ToList();
                return CiuxCandidates;
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        [Route("Estadisticas/{idcandidate}")]
        public IEnumerable<CiudadanosDto> estadisticas(int idcandidate)
        {


            var edad = (from c in db.citizen4
                        where c.idCandidates == idcandidate
                        group new CiudadanosDto { FullName = c.CellPhone }
                        by c.Age into edades
                        select edades).First();

            return edad;

        }

        [HttpGet]
        [ResponseType(typeof(candidate))]
        [Route("estadistica/{idCandidate},{idEstadistica}")]
        public HttpResponseMessage estadisticas(int idCandidate, int idEstadistica)
        {
            try
            {

                if (idEstadistica >= 1 && idEstadistica <= 12)
                {
                    if (idEstadistica == 1)
                    {
                        // ciudadanos del candidato
                        var cc = (from c in db.citizen4
                                  join cn in db.candidates on c.idTown equals cn.idTown
                                  where c.idCandidates == idCandidate
                                  select cn).FirstOrDefault();
                        return Request.CreateResponse(HttpStatusCode.OK, cc);
                    }
                    else if (idEstadistica == 2)
                    {
                        //Numero de personas por rango de edades
                        var edad = (from c in db.citizen4
                                    group c by c.Age into edades
                                    where edades.FirstOrDefault().idCandidates == idCandidate
                                    select edades).FirstOrDefault();
                        return Request.CreateResponse(HttpStatusCode.OK, edad);
                    }
                    else if (idEstadistica == 3)
                    {
                        //#de personas por zonas (barrios veredas)
                        var sector = (from c in db.citizen4
                                      group c by c.idSector into sectores
                                      where sectores.FirstOrDefault().idCandidates == idCandidate
                                      select sectores).FirstOrDefault();
                        return Request.CreateResponse(HttpStatusCode.OK, sector);
                    }
                    else if (idEstadistica == 4)
                    {

                        //#de personas por nivel educativo
                        var edu = (from c in db.citizen4
                                   group c by c.educationLevel into leveledu
                                   where leveledu.FirstOrDefault().idCandidates == idCandidate
                                   select leveledu).FirstOrDefault();
                        return Request.CreateResponse(HttpStatusCode.OK, edu);
                    }
                    else if (idEstadistica == 5)
                    {
                        // Personas por Actividad economica
                        var act = (from c in db.citizen4
                                   group c by c.economicActivity into EconomicAc
                                   where EconomicAc.FirstOrDefault().idCandidates == idCandidate
                                   select EconomicAc).FirstOrDefault();
                        return Request.CreateResponse(HttpStatusCode.OK, act);
                    }
                    else if (idEstadistica == 6)
                    {
                        //Personas por Estado civil
                        var est = (from c in db.citizen4
                                   group c by c.CivilStatus into civilsta
                                   where civilsta.FirstOrDefault().idCandidates == idCandidate
                                   select civilsta).FirstOrDefault();
                        return Request.CreateResponse(HttpStatusCode.OK, est);
                    }
                    else if (idEstadistica == 7)
                    {
                        //Personas Cabeza de familia
                        var head = (from c in db.citizen4
                                    group c by c.HeadFamily into headF
                                    where headF.FirstOrDefault().idCandidates == idCandidate
                                    select headF).FirstOrDefault();
                        return Request.CreateResponse(HttpStatusCode.OK, head);
                    }
                    else if (idEstadistica == 8)
                    {
                        //Empleados actualmente
                        var empl = (from c in db.citizen4
                                    group c by c.EmployeedNow into employed
                                    where employed.FirstOrDefault().idCandidates == idCandidate
                                    select employed).FirstOrDefault();
                        return Request.CreateResponse(HttpStatusCode.OK, empl);
                    }

                    else if (idEstadistica == 9)
                    {
                        //personas por tipo de transporte
                        var trs = (from c in db.citizen4
                                   group c by c.TypeTransportUse into typet
                                   where typet.FirstOrDefault().idCandidates == idCandidate
                                   select typet).FirstOrDefault();
                        return Request.CreateResponse(HttpStatusCode.OK, trs);

                    }
                    else if (idEstadistica == 10)
                    {
                        //personas que trabajan en el oriente
                        var wr = (from c in db.citizen4
                                  group c by c.WorkEast into workE
                                  where workE.FirstOrDefault().idCandidates == idCandidate
                                  select workE).FirstOrDefault();
                        return Request.CreateResponse(HttpStatusCode.OK, wr);
                    }
                    else if (idEstadistica == 11)
                    {
                        // Necesidades de los ciudadanos por candidato
                        var need = (from nc in db.needs_citizen4
                                    join ct in db.citizen4 on nc.idCitizen4 equals ct.idCitizen4
                                    where ct.idCandidates == idCandidate
                                    select ct).FirstOrDefault();
                        return Request.CreateResponse(HttpStatusCode.OK, need);

                    }
                    else if (idEstadistica == 12)
                    {
                        // prioridades de los ciudadanos por candidato
                        var foc = (from nc in db.focus_citizen4
                                   join ct in db.citizen4 on nc.idCitizen4 equals ct.idCitizen4
                                   where ct.idCandidates == idCandidate
                                   select ct).FirstOrDefault();
                        return Request.CreateResponse(HttpStatusCode.OK, foc);
                    }

                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "El codigo de la estadisticas debe estar entre 1 y 12");
            }
            catch
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }


        // [HttpPost]
        // [Route("uploadFile")]
        //public async Task<string> uploadFile()
        // {

        //     var ctx = HttpContext.Current;
        //     var root = ctx.Server.MapPath("~/App_Data");
        //     var provider = new MultipartFormDataStreamProvider(root);

        //     try
        //     {
        //         await Request.Content
        //             .ReadAsMultipartAsync(provider);

        //         foreach (var file in provider.FileData)
        //         {
        //             var name = file.Headers.ContentDisposition.FileName;

        //             //remove double quotes from string
        //             name = name.Trim('"');

        //             var localFileName = file.LocalFileName;
        //             var filePath = Path.Combine(root,name);

        //             //File.Move(localFileName,filePath);
        //             //SaveFile(localFileName, filePath);

        //             SaveFileBinary(localFileName, name);

        //         }
        //     }
        //     catch (Exception e)
        //     {

        //         return $"Error: {e.Message}";
        //     }


        //     return "File uploaded!";
        // }

        // // GUARDA FILE BINARY
        // private void SaveFileBinary(string localFile, string fileName)
        // {
        //     // Get file binary
        //     byte[] fileBytes;
        //     using(var fs = new FileStream(
        //         localFile, FileMode.Open, FileAccess.Read))
        //     {
        //         fileBytes = new byte[fs.Length];
        //         fs.Read(
        //             fileBytes, 0, Convert.ToInt32(fs.Length));
        //     }

        //     // Create a files object
        //     var file = new candidate()
        //     {
        //          Image = fileBytes
        //     };

        //     // Add and save it in database
        //     db.candidates.Add(file);
        //     db.SaveChanges();

        // }


        // PERMITE GUARDAR LA RUTA DEL ARCHIVO (PATH)
        //private void SaveFile(string localFile,string filePath)
        //{
        //    // Move fle to folder
        //    //File.Move(localFile, filePath);

        //    // Get file binary


        //    // Create a File Location object
        //    var fl = new candidate()
        //    {
        //        Image = filePath
        //    };

        //    //Add and save it in database
        //    db.candidates.Add(fl);
        //    db.SaveChanges();
        //}

        // PUT: api/candidates/5
        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update([FromBody]candidate candidate)
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                var newUser = db.candidates.Single(c => c.idCandidates == candidate.idCandidates);

                newUser.idCard = candidate.idCard;
                newUser.FullName = candidate.FullName;
                newUser.Email = candidate.Email;
                newUser.Image = candidate.Image;
                newUser.Description = candidate.Description;
                newUser.DescriptionCampaign = candidate.DescriptionCampaign;
                newUser.LinkCampaign = candidate.LinkCampaign;
                newUser.LogoCampaign = candidate.LogoCampaign;
                newUser.idTown = candidate.idTown;
                newUser.idUsers = candidate.idUsers;

                db.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        [HttpPut]
        [Route("updateDescriptions")]
        public HttpResponseMessage UpdateDescription([FromBody]candidate candidate)
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                var newUser = db.candidates.Where(c => c.idCandidates == candidate.idCandidates).FirstOrDefault();

                newUser.idCard = newUser.idCard;
                newUser.FullName = newUser.FullName;
                newUser.Email = newUser.Email;
                newUser.Image = newUser.Image;

                if (candidate.Description == "" || candidate.Description == null)
                {
                    newUser.Description = newUser.Description;
                }
                else
                {
                    newUser.Description = candidate.Description;
                }

                if (candidate.DescriptionCampaign == "" || candidate.DescriptionCampaign == null)
                {
                    newUser.DescriptionCampaign = newUser.DescriptionCampaign;
                }
                else
                {
                    newUser.DescriptionCampaign = candidate.DescriptionCampaign;
                }

                newUser.LinkCampaign = newUser.LinkCampaign;
                newUser.LogoCampaign = newUser.LogoCampaign;
                newUser.idTown = newUser.idTown;
                newUser.idUsers = newUser.idUsers;

                db.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {

                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // POST: api/candidates
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create([FromBody]candidate candidate)
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                db.candidates.Add(candidate);
                db.SaveChanges();
                return result;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // POST: api/candidates
        [HttpPut]
        [Route("createimage/{idcandidate}")]
        public async Task<HttpResponseMessage> CreateImage(int idcandidate)
        {
            try
            {
                byte[] fileBytes;
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                fileBytes = await uploadFile(idcandidate);
                var candidate = db.candidates.Single(c => c.idCandidates == idcandidate);
                candidate.Image = fileBytes;
                db.SaveChanges();
                return result;
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        public async Task<byte[]> uploadFile(int idcandidate)
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

                    fileBytes =  SaveFileBinary(localFileName, name, idcandidate);
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
        private byte[] SaveFileBinary(string localFile, string fileName, int idcandidate)
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

            // Create a files object
            var file = new files()
            {
                IdCandidate = idcandidate,
                FileBin = fileBytes,
                Size = Convert.ToString(fileBytes.Length)

            };

            // Add and save it in database
            db.files1.Add(file);
            db.SaveChanges();
            return fileBytes;
        }

        // DELETE: api/candidates/5
        [ResponseType(typeof(candidate))]
        [HttpDelete]
        [Route("delete/{id}")]
        public IHttpActionResult Deletecandidate(int id)
        {
            candidate candidate = db.candidates.Find(id);
            if (candidate == null)
            {
                return NotFound();
            }

            db.candidates.Remove(candidate);
            db.SaveChanges();

            return Ok(candidate);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool candidateExists(int id)
        {
            return db.candidates.Count(e => e.idCandidates == id) > 0;
        }
    }
}