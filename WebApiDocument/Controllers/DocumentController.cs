using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Models;
using Newtonsoft.Json;
using Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace WebApiDocument.Controllers
{
    [Route("document/[action]")]
    [ApiController]
    [EnableCors("MyCorsImplementionPolicy")]
    public class DocumentController : ControllerBase
    {

        #region Initilaztion with Injection
        public IUnitOfWork unitofwork;
        public IRepository<document> repoDocument;
        public IRepository<document_files> repoDocumentfiles;
        public IWebHostEnvironment _webHostEnv;
        public IHttpContextAccessor _httpContextAccessor;
        public readonly IMapper _mapper;
        public DocumentController(IUnitOfWork _unitOfWork, IWebHostEnvironment _webHEnv,
            IHttpContextAccessor _hCAccessor ,IMapper mapper)
        {
            unitofwork=_unitOfWork;
            repoDocument = unitofwork.Document;
            repoDocumentfiles = unitofwork.documentFiles;
            _webHostEnv = _webHEnv;
            _httpContextAccessor = _hCAccessor;
            _mapper = mapper;
        }

        #endregion

        [HttpGet]
        public IActionResult GetAllDocument()
        {
            if(repoDocument.GetAll() == null)
            {
                return StatusCode(404);
            }
            

            var doc = repoDocument.GetAll().Include(i=>i.document_Files).Include(i=>i.priority);

            List<DocumentViewModel> docList = new List<DocumentViewModel>(); 
         
              foreach(var document in doc)
            {
                docList.Add(new DocumentViewModel()
                {
                    id = document.Id,
                    name = document.Name,
                    date = document.date,
                    createdDate = document.CreatedDate,
                    due_date = document.Due_date,
                    priorityId = document.priorityID,
                    documents = document.document_Files.AsQueryable(),
                    priority=document.priority.Name

                }) ;
            }
           
            return StatusCode(200, docList);
        }

       // [Produces("application/json")]
        [HttpPost]
        public IActionResult AddDocument(string name, DateTime date, DateTime due_Date, int priorityId)
        {
            var files = Request.Form.Files;
           
            try
            {
                #region add info File
                document doc = new document()
                {   
                    Name = name,
                    priorityID = priorityId,
                    Due_date = due_Date,
                    date = date,
                    CreatedDate =DateTime.Now

                };
                

                repoDocument.Add(doc);
                unitofwork.Save();

                #endregion
                #region Uplodefile

                foreach (var f in files)
                {
                    var path = Path.Combine(_webHostEnv.WebRootPath, "Document", f.FileName);

                    FileStream stf = new FileStream(path, FileMode.Create);
                    
                        f.CopyTo(stf);

                       stf.Close();
                   
                    
                    document_files docfile = new document_files()
                    {
                        DocuemntID = doc.Id,
                        path = f.FileName,
                    };
                    repoDocumentfiles.Add(docfile); 
                        unitofwork.Save();

                }
                #endregion

                return Ok();

            }
            catch
            {
                return BadRequest();
            }



        }

        //update doucument

        [HttpPut]
        public IActionResult UpdateDocument(int id, string name, DateTime date, DateTime due_Date, int priorityId)
        {
            var files = Request.Form.Files;

            try
            {
                var doc = repoDocument.GetById(id);

                doc.Name = name;
                doc.date = date;
                doc.Due_date = due_Date;
                doc.priorityID = priorityId;



                repoDocument.Update(doc);
                unitofwork.Save();
                foreach (var f in files)
                {
                    var path = Path.Combine(_webHostEnv.WebRootPath, "Document", f.FileName);

                    FileStream stf = new FileStream(path, FileMode.Create);

                    f.CopyTo(stf);

                    stf.Close();
                    document_files documentfile = new document_files()
                    {
                        DocuemntID = id,
                        path = f.FileName,
                    };
                    repoDocumentfiles.Add(documentfile);
                    unitofwork.Save();

                   
                    

                }


                return Ok();

            }
            catch
            {
                return BadRequest();
            }



        }

        // GetDocument by Id doucument
        [HttpGet]
        public IActionResult GetDocumentbyId([FromQuery]int id)
        {
            if (repoDocument.GetById(id) == null)
            {
                return StatusCode(404);
            }
           

            var doc= repoDocument.GetAll().Include(i => i.document_Files).Include(i => i.priority).FirstOrDefault(i=>i.Id==id); ;

            DocumentViewModel docvm = new DocumentViewModel()
            {
                id=doc.Id,
                name = doc.Name,
                priorityId = doc.priorityID,
                createdDate = doc.CreatedDate,
                date = doc.date,
                due_date = doc.Due_date,
                priority =doc.priority.Name,
               documents=doc.document_Files.AsQueryable()
              

            };
            
           
            return StatusCode(200, docvm);
        }


        //Delete doucument
        [HttpDelete]
        public IActionResult DeleteDocument([FromQuery] int id)
        {
            if (repoDocument.GetById(id) == null)
            {
                return StatusCode(404);
            }
            var docfiles = repoDocumentfiles.GetAll().Where(i => i.DocuemntID == id);
            foreach (var docfile in docfiles)
            {
                if (docfile.path != null)
                    deleteFilefromRoot(docfile.path);


            }
            repoDocument.Delete(repoDocument.GetById(id));
            unitofwork.Save();
            return StatusCode(200);
        }

        //Delete remove current Ducment

        [HttpDelete]
        public IActionResult DeleteFileDocument([FromQuery] int id)
        {
            if (repoDocumentfiles.GetById(id) == null)
            {
                return StatusCode(404);
            }
          
                if (repoDocumentfiles.GetById(id).path != null)
                    deleteFilefromRoot(repoDocumentfiles.GetById(id).path);


            
            repoDocumentfiles.Delete(repoDocumentfiles.GetById(id));
            unitofwork.Save();
            return StatusCode(200);
        }

        private void deleteFilefromRoot(string img)
        {
            img = Path.Combine(_webHostEnv.WebRootPath, "Document", img);
            FileInfo fileinfo = new FileInfo(img);
            if (fileinfo != null)
            {
                System.IO.File.SetAttributes(img, FileAttributes.Normal);
                System.IO.File.Delete(img);
            }
        }
    }
}
