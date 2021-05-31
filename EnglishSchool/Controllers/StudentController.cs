using EnglishSchool.Model.DTOs;
using EnglishSchool.Service;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EnglishSchool.Controllers
{
    [RoutePrefix("api/student")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class StudentController : ApiController
    {
        private readonly IStudentService _service;
        public StudentController(IStudentService service)
        {
            _service = service;
        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddStudent(FullInfoStudentDTO studentDTO)
        {
            var response = _service.AddAndSave(studentDTO);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

        [Route("all")]
        [HttpGet]
        public IHttpActionResult GetAllStudent()
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

        [Route("RegisterCourse")]
        [HttpPost]
        public IHttpActionResult StudentRegisterCourse(StudentRegisterCourse registerCourse)
        {
            var response = _service.StudentRegisterCourse(registerCourse);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

        [Route("update")]
        [HttpPatch]
        public IHttpActionResult UpdateStudent(FullInfoStudentDTO studentDTO)
        {
            var response = _service.Update(studentDTO);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetStudentId(string id)
        {
            var response = _service.GetById(id);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            else if (response.result == null)
            {
                return NotFound();
            }
            return Ok(response.result);
        }
    }
}