using EnglishSchool.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EnglishSchool.Controllers
{
    [RoutePrefix("api/CourseDetail")]
    public class CourseDetailOfStudentController : ApiController
    {
        private readonly ICourseDetailOfStudentService _service;
        public CourseDetailOfStudentController(ICourseDetailOfStudentService service)
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
    }
}