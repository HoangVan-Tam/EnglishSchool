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

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/auth")]

    public class AuthController : ApiController
    {
        private IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [Route("login")]
        [HttpPost]
        public IHttpActionResult login(LoginDTO account)
        {
            var response = _service.Login(account);
            if (response.result == "Student")
            {
                StudentLoginDTO student = new StudentLoginDTO()
                {
                    password = account.password,
                    studentId = account.userID,
                };
                return Ok(_service.StudentLogin(student));
            }
            else if (response.result == "Parent")
            {
                ParentLoginDTO parent = new ParentLoginDTO()
                {
                    password = account.password,
                    parentId = account.userID,
                };
                return Ok(_service.ParentLogin(parent));
            }
            else
            {
                return BadRequest(response.message);
            }
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

        [Route("Admin/Login")]
        [HttpPost]
        public IHttpActionResult AdminLogin(LoginDTO employee)
        {
            var response = _service.AdminLogin(employee);
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


        [Route("student/ChangePassword")]
        [HttpPost]
        public IHttpActionResult StudentChangePassword(ChangePasswordDTO account)
        {
            var response = _service.StudentChangePassword(account);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

        [Route("employee/ChangePassword")]
        [HttpPost]
        public IHttpActionResult EmployeeChangePassword(ChangePasswordDTO account)
        {
            var response = _service.EmployeeChangePassword(account);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }
    }
}
