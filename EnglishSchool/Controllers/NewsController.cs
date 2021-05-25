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
    [RoutePrefix("api/news")]
    public class NewsController : ApiController
    {
        private readonly INewsService _service;
        public NewsController(INewsService service)
        {
            _service = service;
        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddNews()
        {
            var response = _service.AddAndSave();
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

        /*[Route("add")]
        [HttpPost]
        public IHttpActionResult UploadFiles()
        {
            var response = _service.AddAndSave();
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }*/

        [Route("update")]
        [HttpPut]
        public IHttpActionResult UpdateNews()
        {
            var response = _service.Update();
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

        [Route("all")]
        [HttpGet]
        public IHttpActionResult GetAllNews()
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
        public IHttpActionResult GetNewsById(int id)
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
