using EnglishSchool.Model.DTOs;
using EnglishSchool.Service;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EnglishSchool.Controllers
{
    [RoutePrefix("api/course")]
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


        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddCourse(JObject courseDTO)
        {
            var response = _service.AddAndSave(courseDTO);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }
    }
}