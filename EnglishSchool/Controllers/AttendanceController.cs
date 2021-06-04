using EnglishSchool.Model.DTOs;
using EnglishSchool.Service;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EnglishSchool.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/attendance")]
    public class AttendanceController : ApiController
    {
        private IAttendanceService _service;
        public AttendanceController(IAttendanceService service)
        {
            _service = service;
        }


        [Route("student/{courseId}")]
        [HttpGet]
        public IHttpActionResult GetAllStudentOfCourse(int courseId)
        {
            var response = _service.GetListStudent(courseId);
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

        [Route("course/{courseId}")]
        [HttpGet]
        public IHttpActionResult GetAllAttendancetOfStudent(int courseId)
        {
            var response = _service.GetListAttendanceStudentOfCourse(courseId);
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

        [Route("course/{courseId}/Student/{studentId}")]
        [HttpGet]
        public IHttpActionResult GetAllAttendanceOfStudent(int courseId, string studentId)
        {
            var response = _service.GetListAttendanceStudentOfCourse(courseId, studentId);
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

        [Route("course/attendance")]
        [HttpPost] 
        public IHttpActionResult AttendanceStudentOfCourse(AttendanceDTO attendances)
        {
            var response = _service.AttendanceStudentOfCourse(attendances);
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
}
