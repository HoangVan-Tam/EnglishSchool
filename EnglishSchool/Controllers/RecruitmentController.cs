using EnglishSchool.Model.DTOs;
using EnglishSchool.Service;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EnglishSchool.Controllers
{
    [RoutePrefix("api/recruitment")]
    public class RecruitmentController : ApiController
    {
        private readonly IRecruitmentService _service;
        public RecruitmentController(IRecruitmentService service)
        {
            _service = service;
        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddRecruitment(RecruitmentDTO recruitment)
        {
            var response = _service.AddAndSave(recruitment);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

        [Route("all")]
        [HttpGet]
        public IHttpActionResult GetAllRecruitment()
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
        [HttpGet]
        public IHttpActionResult GetRecruitmentById(int id)
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

        [Route("update")]
        [HttpPut]
        public IHttpActionResult UpdateDepartment(RecruitmentDTO recruitmentDTO)
        {
            var response = _service.Update(recruitmentDTO);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }
    }
}
