using EnglishSchool.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EnglishSchool.Controllers
{
    public class ScoreResultController : ApiController
    {
        private IScoreResultService _service;
        public ScoreResultController(IScoreResultService service)
        {
            _service = service;
        }
    }
}
