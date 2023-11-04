using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIGITAL_GAMIFY.Entities;
using DIGITAL_GAMIFY.DAL;

namespace DIGITAL_GAMIFY.BAL
{
    public class ReportManager
    {
        private ReportData objrd = new ReportData();
        public List<GameResultEntity> GetBusinessGameResult(GameResultListParamsEntity p)
        {
            return objrd.GetBusinessGameResult(p);
        }
        public List<SurveyReportResult> GetSurveyResult(paggingEntity pe, Int32 sid, Int32 bid)
        {
            return objrd.GetSurveyResult(pe, sid, bid);
        }
        public List<SurveyAnswerResult> GetSurveyResultByResultId(Int64 SrId)
        {
            return objrd.GetSurveyResultByResultId(SrId);
        }
    }
}
