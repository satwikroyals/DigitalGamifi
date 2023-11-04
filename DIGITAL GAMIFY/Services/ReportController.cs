using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using System.Web.Http.Cors;
using DIGITAL_GAMIFY.BAL;
using DIGITAL_GAMIFY.Entities;

namespace DIGITAL_GAMIFY.Services
{
    //[EnableCors(origins: "http://www.gamesnatcherz.com,http://gamesnatcherz.com", headers: "*", methods: "*")]
    public class ReportController : ApiController
    {
        private ReportManager objrm = new ReportManager();
        [Route("api/GetBusinessGameResult")]
        [HttpPost]
        public List<GameResultEntity> GetBusinessGameResult(GameResultListParamsEntity p)
        {
            return objrm.GetBusinessGameResult(p);
        }
        [Route("api/GetSurveyResult")]
        [HttpGet]
        public List<SurveyReportResult> GetSurveyResult(Int32 sid, Int32 bid,[FromUri]paggingEntity pe)
        {
            return objrm.GetSurveyResult(pe, sid, bid);
        }
        [Route("api/GetSurveyResultByResultId")]
        [HttpGet]
        public List<SurveyAnswerResult> GetSurveyResultByResultId(Int64 SrId)
        {
            return objrm.GetSurveyResultByResultId(SrId);
        }
    }
}
