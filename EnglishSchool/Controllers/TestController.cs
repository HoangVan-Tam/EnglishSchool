using EnglishSchool.Model.DTOs;
using EnglishSchool.Service;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EnglishSchool.Controllers
{
    [RoutePrefix("api/test")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TestController : ApiController
    {
        private ITestService _service;
        public TestController(ITestService service)
        {
            _service = service;
        }

        [Route("all/{id}")]
        [HttpGet]
        public IHttpActionResult GetAllTestByCourseDetailId(int id)
        {
            var response = _service.GetListTestByCourseDetailId(id);
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

        [Route("exercise")]
        [HttpGet]
        public IHttpActionResult DoExercise()
        {
            var response = _service.GetListQuestion();
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }
        [Route("exercise/submit")]
        [HttpPost]
        public IHttpActionResult SubmitExercise(SubmitTestDTO submitTestDTO)
        {
            var response = _service.Submit(submitTestDTO);
            if (response.success == false)
            {
                return BadRequest(response.message);
            }
            return Ok(response.result);
        }
    }
}
