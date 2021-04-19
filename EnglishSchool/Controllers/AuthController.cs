using EnglishSchool.Model.DTOs;
using EnglishSchool.Model.Models;
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

    [RoutePrefix("api/auth")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthController : ApiController
    {
        private IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;
        }



        [Route("Parent/Login")]
        [HttpPost]
        public IHttpActionResult ParentLogin(ParentLoginDTO parent)
        {
            var response = _service.ParentLogin(parent);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

        [Route("Student/Login")]
        [HttpPost]
        public IHttpActionResult StudentLogin(StudentLoginDTO student)
        {
            var response = _service.StudentLogin(student);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

        [Route("parent/ChangePassword")]
        [HttpPost]
        public IHttpActionResult ParentChangePassword(ChangePasswordDTO account)
        {
            var response = _service.ParentChangePassword(account);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }


        [Route("ChangePassword")]
        [HttpPost]
        public IHttpActionResult ChangePassword(ChangePasswordDTO account)
        {
            var response = _service.StudentChangePassword(account);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }
    }
}
