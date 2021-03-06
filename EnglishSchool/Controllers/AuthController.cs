using EnglishSchool.Model.DTOs;
using EnglishSchool.Service;
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
            else
            {
                return BadRequest(response.message);
            }
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

        [Route("employee/login")]
        [HttpPost]
        public IHttpActionResult EmployeeLogin(LoginDTO account)
        {
            var response = _service.EmployeeLogin(account);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response);
        }


        [Authorize]
        [Route("GetInfo/{userId}")]
        [HttpGet]
        public IHttpActionResult GetInFo(string userId)
        {
            if (userId.Substring(0, 3) == "stu")
            {
                var response = _service.StudentInfo(userId);
                if (response.success == false)
                {
                    return BadRequest(response.message);
                }
                return Ok(response.result);
            }
            else
            {
                var response = _service.EmployeeInfo(userId);
                if (response.success == false)
                {
                    return BadRequest(response.message);
                }
                return Ok(response.result);
            }
        }
    }
}
