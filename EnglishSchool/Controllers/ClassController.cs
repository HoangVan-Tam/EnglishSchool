using EnglishSchool.Model.DTOs;
using EnglishSchool.Service;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EnglishSchool.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/class")]
    public class ClassController : ApiController
    {
        private IClassService _service;
        public ClassController(IClassService service)
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

        [Route("All/course/{courseId}/department/{departmentId}")]
        [HttpGet]
        public IHttpActionResult GetAllClassByDepartmentIdAndCourseID()
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

        [Route("All/{departmentId}")]
        [HttpGet]
        public IHttpActionResult GetAllById(int departmentId)
        {
            var response = _service.GetAll(departmentId);
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

        [Route("All/info/{userId}")]
        [HttpGet]
        public IHttpActionResult GetAllByUserId(string userId)
        {
            if (userId.Substring(0, 3) == "stu")
            {
                var response = _service.GetAllInfoOfStudent(userId);
                if (response.success == false)
                {
                    return BadRequest(response.message);
                }
                return Ok(response.result);
            }
            else
            {
                var response = _service.GetAllInfoOfTeacher(userId);
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
        public IHttpActionResult AddCourse(ClassDTO classDTO)
        {
            var response = _service.AddAndSave(classDTO);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

        [Route("update")]
        [HttpPut]
        public IHttpActionResult UpdateCourse(ClassInfoForTeacher courseDTO)
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