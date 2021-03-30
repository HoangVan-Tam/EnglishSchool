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
    [RoutePrefix("api/RecruitmentDetail")]
    public class ListRecruitmentDetailController : ApiController
    {
        IRecruitmentDetailService _service;
        public ListRecruitmentDetailController(IRecruitmentDetailService service)
        {
            _service = service;
        }

        [Route("all")]
        [HttpGet]
        public IHttpActionResult GetAllRecruitmentDetail()
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
        public IHttpActionResult GetRecruitmentDetailById(int id)
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

        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddRecruitmentDetail(RecruitmentDetailDTO recruitmentDetail)
        {
            var response = _service.AddAndSave(recruitmentDetail);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

        [Route("{departmentId}/{recruitmentId}")]
        [HttpDelete]
        public IHttpActionResult DeleteDepartment(int departmentId, int recruitmentId)
        {
            var response = _service.Delete(departmentId, recruitmentId);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

        [Route("update")]
        [HttpPut]
        public IHttpActionResult UpdateDepartment(RecruitmentDetailDTO recruitmentDetailDTO)
        {
            var response = _service.Update(recruitmentDetailDTO);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

    }
}
