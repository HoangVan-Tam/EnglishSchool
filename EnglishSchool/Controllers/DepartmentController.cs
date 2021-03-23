using AutoMapper;
using EnglishSchool.Data.Repositories;
using EnglishSchool.Model.DTOs;
using EnglishSchool.Model.Models;
using EnglishSchool.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EnglishSchool.Controllers
{
    [RoutePrefix("api/department")]
    public class DepartmentController : ApiController
    {
        private readonly IDepartmentService _service;
        public DepartmentController(IDepartmentService service)
        {
            _service = service;
        }


        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddDepartment(DepartmentDTO department)
        {
            var response = _service.AddAndSave(department);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(_service.GetAll().result);
        }

        [Route("all")]
        [HttpGet]
        public IHttpActionResult GetAllDepartment()
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

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteDepartment(int id)
        {
            var response = _service.Delete(id);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }


        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetDepartmentById(int id)
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
