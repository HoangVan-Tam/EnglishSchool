using EnglishSchool.Model.DTOs;
using EnglishSchool.Service;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EnglishSchool.Controllers
{
    [RoutePrefix("api/parent")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ParentController : ApiController
    {
        private readonly IParentService _service;
        public ParentController(IParentService service)
        {
            _service = service;
        }

        [Route("All")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var response = _service.GetAll();
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            if (response.result == null)
            {
                return NotFound();
            }
            return Ok(response.result);
        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddParent(ParentDTO parentDTO)
        {
            var response = _service.AddAndSave(parentDTO);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

        [Route("manage/{studentId}/{courseId}")]
        [HttpGet]
        public IHttpActionResult ManageStudent(string studentId, int courseId)
        {
            var response = _service.ManageStudent(studentId, courseId);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

        [Route("update")]
        [HttpPut]
        public IHttpActionResult UpdateParent(ParentDTO parentDTO)
        {
            var response = _service.Update(parentDTO);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

        [Route("addStudentofparent/{parentId}")]
        [HttpPost]
        public IHttpActionResult AddStudentOfParent(string studentId, string parentId)
        {
            var response = _service.AddStudentOfParent(studentId, parentId);
            if (response.success == true)
            {
                return Ok(response.result);
            }
            else
            {
                return BadRequest(response.message);
            }
        }
    }
}
