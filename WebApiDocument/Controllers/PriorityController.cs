using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories;

namespace WebApiDocument.Controllers
{
    [Route("Priority/[action]")]
    [ApiController]
    [EnableCors("MyCorsImplementionPolicy")]
    public class PriorityController : ControllerBase
    {
        public IUnitOfWork unitofwork;
        public IRepository<Priority> repoPriority;

        public PriorityController(IUnitOfWork _unitOfWork)
        {
            unitofwork = _unitOfWork;
            repoPriority = unitofwork.Priority;
            
        }

        [Route("GetAllPriority")]
        [HttpGet]
        public IActionResult GetAllPriority()
        {
            if (repoPriority.GetAll() == null)
            {
                return StatusCode(404);
            }

            return StatusCode(200, repoPriority.GetAll());
        }
    }
}
