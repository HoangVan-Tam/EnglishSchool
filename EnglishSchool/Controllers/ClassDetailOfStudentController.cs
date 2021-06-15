using EnglishSchool.Service;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EnglishSchool.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/ClassDetail")]
    public class ClassDetailOfStudentController : ApiController
    {
        private readonly ICourseDetailOfStudentService _service;
        public ClassDetailOfStudentController(ICourseDetailOfStudentService service)
        {
            _service = service;
        }

        [Route("all")]
        [HttpGet]
        public IHttpActionResult GetAllCourseDetail()
        {
            var response = _service.GetAll();
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            if (response.result.Count() < 0)
            {
                return NotFound();
            }
            return Ok(response.result);
        }

        [Route("all/{studentId}")]
        [HttpGet]
        public IHttpActionResult GetAllCourseDetailOfStudent(string studentId)
        {
            var response = _service.GetAllCourseOfStudent(studentId);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            if (response.result.Count() < 0)
            {
                return NotFound();
            }
            return Ok(response.result);
        }

        [Route("all")]
        [HttpPut]
        public IHttpActionResult PutAllCourseDetailOfStudent()
        {
            var response = _service.Update();
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            if (response.result.Count() < 0)
            {
                return NotFound();
            }
            return Ok(response.result);
        }
    }
}