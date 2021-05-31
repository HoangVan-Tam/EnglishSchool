using EnglishSchool.Model.DTOs;
using EnglishSchool.Service;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EnglishSchool.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/department")]
    public class DepartmentController : ApiController
    {
        private readonly IDepartmentService _service;
        public DepartmentController(IDepartmentService service)
        {
            _service = service;
        }


        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddDepartment(DepartmentDTO department)
        {
            var response = _service.AddAndSave(department);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

        [Route("all")]
        [HttpGet]
        public IHttpActionResult GetAllDepartment()
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


        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetDepartmentById(int id)
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
        [HttpPut]
        public IHttpActionResult UpdateDepartment(DepartmentDTO departmentDTO)
        {
            var response = _service.Update(departmentDTO);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }
    }
}
