using EnglishSchool.Model.DTOs;
using EnglishSchool.Model.Models;
using EnglishSchool.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EnglishSchool.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [Route("{Username}")]
        [HttpGet]
        public IHttpActionResult GetByUserName(string Username)
        {
            var response = _service.GetByUserName(Username);
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

        [Route("Add")]
        [HttpPost]
        public IHttpActionResult CreateAccount(Account account)
        {
            var response = _service.AddAndSave(account);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

        [Route("Login")]
        [HttpPost]
        public IHttpActionResult Login(Account account)
        {
            var response = _service.Login(account);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

        [Route("ChangePassword")]
        [HttpPost]
        public IHttpActionResult ChangePassword(AccountChangePasswordDTO account)
        {
            var response = _service.ChangePassword(account);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }
    }
}
