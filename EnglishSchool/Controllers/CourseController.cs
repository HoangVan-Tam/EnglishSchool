using EnglishSchool.Model.DTOs;
using EnglishSchool.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        [Route("All/{departmentId}")]
        [HttpGet]
        public IHttpActionResult GetAllByUserId(int departmentId)
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
