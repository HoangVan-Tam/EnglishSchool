using EnglishSchool.Model.DTOs;
using EnglishSchool.Service;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EnglishSchool.Controllers
{
    [RoutePrefix("api/course")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CourseController : ApiController
    {
        private ICourseService _service;
        public CourseController(ICourseService service)
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


        [Route("All/{studentId}/noregister")]
        [HttpGet]
        public IHttpActionResult GetAllCourseNoOneRegister(string studentId)
        {
            var response = _service.GetAllCourseNoOneRegister(studentId);
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
        public IHttpActionResult AddCourse(CourseDTO courseDTO)
        {
            var response = _service.AddAndSave(courseDTO);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

        [Route("update")]
        [HttpPut]
        public IHttpActionResult UpdateCourse(CourseDTO courseDTO)
        {
            var response = _service.Update(courseDTO);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }
    }
}