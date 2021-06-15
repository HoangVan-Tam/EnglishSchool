using EnglishSchool.Model.DTOs;
using EnglishSchool.Service;
using System;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EnglishSchool.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Employee")]
    public class EmployeeController : ApiController
    {
        private IEmployeeService _service;
        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [Route("all")]
        [HttpGet]
        public IHttpActionResult GetAllEmployee()
        {
            var response = _service.GetAllTeacher();
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

        [Route("all/{departmentId}")]
        [HttpGet]
        public IHttpActionResult GetAllEmployeeByDepartmentId(int departmentId)
        {
            var response = _service.GetAllTeacher(departmentId);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

        [Route("teacher/allcourse/{teacherId}")]
        [HttpGet]
        public IHttpActionResult GetAllCourseOfTeacher(string teacherId)
        {
            var response = _service.GetAllCourseOfTeacher(teacherId);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }



        [Route("manage")]
        [HttpGet]
        public IHttpActionResult GetAllEmployee(ManageCourse manage)
        {
            var response = _service.ManageListStudent(manage.firstDayOfWeek, manage.courseId);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }


        [Route("manage/ver2")]
        [HttpPost]
        public IHttpActionResult GetAllEmployeeVer2(ManageCourse manage)
        {
            var response = _service.ManageListStudentVer2(manage.firstDayOfWeek, manage.courseId);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }


        [Route("update")]
        [HttpPatch]
        public IHttpActionResult UpdateEmployee(EmployeeDTO employee)
        {
            var response = _service.Update(employee);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddTeacher(EmployeeDTO employee)
        {
            var response = _service.AddAndSave(employee);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult AddStudent(string id)
        {
            var response = _service.GetById(id);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

        [Route("RegisterCourse")]
        [HttpPost]
        public IHttpActionResult EmployeeRegisterCourse(EmployeeRegisterCourse registerCourse)
        {
            var response = _service.EmployeeRegisterCourse(registerCourse);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }
    }
}
