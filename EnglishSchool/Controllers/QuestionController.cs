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
    [RoutePrefix("api/question")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class QuestionController : ApiController
    {
        private IQuestionService _service;
        public QuestionController(IQuestionService service)
        {
            _service = service;
        }

        [Route("Exercise")]
        [HttpGet]
        public IHttpActionResult GetExercise()
        {
            var response = _service.GetListQuestion();
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
        public IHttpActionResult AddQuestion(QuestionDTO questionDTO)
        {
            var response = _service.AddAndSave(questionDTO);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

        [Route("update")]
        [HttpPut]
        public IHttpActionResult UpdateQuestion(QuestionDTO questionDTO)
        {
            var response = _service.Update(questionDTO);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteQuestion(int id)
        {
            var response = _service.Delete(id);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

    }
}
