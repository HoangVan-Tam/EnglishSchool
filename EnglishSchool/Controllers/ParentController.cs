using EnglishSchool.Model.DTOs;
using EnglishSchool.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EnglishSchool.Controllers
{
    [RoutePrefix("api/parent")]
    public class ParentController : ApiController
    {
        private readonly IParentService _service;
        public ParentController(IParentService service)
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
        public IHttpActionResult AddCourse(ParentDTO parentDTO)
        {
            var response = _service.AddAndSave(parentDTO);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }
    }
}
