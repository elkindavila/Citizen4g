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
using Citizen4g.Models;

namespace Citizen4g.Controllers
{
    [RoutePrefix("api/files")]
    public class filesController : ApiController
    {
        private db_citizen4Entities2 db = new db_citizen4Entities2();

        // GET: api/files
        [Route("")]
        public IQueryable<files> Getfiles1()
        {
            return db.files1;
        }

        // GET: api/files/5
        [Route("{id:int}")]
        [ResponseType(typeof(files))]
        public IHttpActionResult Getfiles(int id)
        {
            files files = db.files1.Find(id);
            if (files == null)
            {
                return NotFound();
            }

            return Ok(files);
        }


        [HttpPost]
        [Route("uploadFile/{idcandidate}")]
        public async Task<string> uploadFile(int idcandidate)
        {

            var ctx = HttpContext.Current;
            var root = ctx.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

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

                    //File.Move(localFileName,filePath);
                    //SaveFile(localFileName, filePath);

                    SaveFileBinary(localFileName, name, idcandidate);

                }
                    
            }
            catch (Exception e)
            {

                return $"Error: {e.Message}";
            }


            return "File uploaded!";
        }

        // GUARDA FILE BINARY
        private void SaveFileBinary(string localFile, string fileName, int idcandidate)
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

        }

        // DELETE: api/files/5
        [ResponseType(typeof(files))]
        [HttpDelete]
        [Route("delete/{id}")]
        public IHttpActionResult Deletefiles(int id)
        {
            files files = db.files1.Find(id);
            if (files == null)
            {
                return NotFound();
            }

            db.files1.Remove(files);
            db.SaveChanges();

            return Ok(files);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool filesExists(int id)
        {
            return db.files1.Count(e => e.idFiles == id) > 0;
        }
    }
}