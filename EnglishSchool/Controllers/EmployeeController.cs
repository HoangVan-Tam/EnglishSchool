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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Employee")]
    public class EmployeeController : ApiController
    {
        private IEmployeeService _service;
        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [Route("all")]
        [HttpGet]
        public IHttpActionResult GetAllEmployee()
        {
            var response = _service.GetAllTeacher();
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

        [Route("update")]
        [HttpPatch]
        public IHttpActionResult UpdateEmployee(EmployeeDTO employee)
        {
            var response = _service.Update(employee);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddTeacher(EmployeeDTO employee)
        {
            var response = _service.AddAndSave(employee);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult AddStudent(string id)
        {
            var response = _service.GetById(id);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }
    }
}
