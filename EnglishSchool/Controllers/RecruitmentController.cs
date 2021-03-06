using EnglishSchool.Model.DTOs;
using EnglishSchool.Service;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EnglishSchool.Controllers
{
    [RoutePrefix("api/recruitment")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RecruitmentController : ApiController
    {
        private readonly IRecruitmentService _service;
        public RecruitmentController(IRecruitmentService service)
        {
            _service = service;
        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddRecruitment(RecruitmentDTO recruitment)
        {
            var response = _service.AddAndSave(recruitment);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

        [Route("all")]
        [HttpGet]
        public IHttpActionResult GetAllRecruitment()
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
        public IHttpActionResult GetRecruitmentById(int id)
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
        [HttpDelete]
        public IHttpActionResult DeleteRecruitment(int id)
        {
            var response = _service.Delete(id);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }

        [Route("update")]
        [HttpPatch]
        public IHttpActionResult UpdateDepartment(RecruitmentDTO recruitmentDTO)
        {
            var response = _service.Update(recruitmentDTO);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }
    }
}
