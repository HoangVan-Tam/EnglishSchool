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
    [RoutePrefix("api/advisory")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PersonalInformationController : ApiController
    {
        private readonly IPersonalInformationService _service;
        public PersonalInformationController(IPersonalInformationService service)
        {
            _service = service;
        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddAdvisory(PersonalInformationDTO personalInformationDTO)
        {
            var response = _service.AddAndSave(personalInformationDTO);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetAdvisoryById(string phoneNumber)
        {
            var response = _service.GetById(phoneNumber);
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

        [Route("all")]
        [HttpGet]
        public IHttpActionResult GetAllAdvisorystring ()
        {
            var response = _service.GetAll();
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
