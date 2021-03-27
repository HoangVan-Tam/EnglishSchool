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
    [RoutePrefix("api/student")]
    public class StudentController : ApiController
    {
        private readonly IStudentService _service;
        public StudentController(IStudentService service)
        {
            _service = service;
        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddStudent(JObject studentDTO)
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
    }
}