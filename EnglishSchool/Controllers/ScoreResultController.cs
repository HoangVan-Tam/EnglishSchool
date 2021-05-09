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
    [RoutePrefix("api/scoreresult")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ScoreResultController : ApiController
    {
        private IScoreResultService _service;
        public ScoreResultController(IScoreResultService service)
        {
            _service = service;
        }

        [Route("all/{id}")]
        [HttpGet]
        public IHttpActionResult GetAllScoreResultByCourseDetailId(int id)
        {
            var response = _service.GetListScoreResultByCourseDetailId(id);
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
    }
}
