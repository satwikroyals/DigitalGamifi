using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DIGITAL_GAMIFY.DAL.Repositiories;
using DIGITAL_GAMIFY.Entities;
using System.Data;
using System.Data.SqlClient;

namespace DIGITAL_GAMIFY.DAL
{
    public class ReportData
    {
        public List<GameResultEntity> GetBusinessGameResult(GameResultListParamsEntity p)
        {
            try
            {
                DapperRepositry<GameResultEntity> _repo = new DapperRepositry<GameResultEntity>();
                DynamicParameters param = new DynamicParameters();
                param.Add("AdminId", p.AdminId, DbType.Int32, ParameterDirection.Input);
                param.Add("GameId", p.GameId, DbType.Int32, ParameterDirection.Input);
                param.Add("BusinessId", p.BusinessId, DbType.Int32, ParameterDirection.Input);
                param.Add("FromDate", p.FromDate, DbType.String, ParameterDirection.Input);
                param.Add("ToDate", p.ToDate, DbType.String, ParameterDirection.Input);
                param.Add("PageIndex", p.Pi, DbType.Int32, ParameterDirection.Input);
                param.Add("PageSize", p.Ps, DbType.Int32, ParameterDirection.Input);
                param.Add("Searchstr", p.Str, DbType.Int32, ParameterDirection.Input);
                return _repo.GetList("GetBusinessGameResults", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// get survey customer report by surveyid
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="sid">surveyid</param>
        /// <returns></returns>
        public List<SurveyReportResult> GetSurveyResult(paggingEntity pe, Int32 sid, Int32 bid)
        {
            DapperRepositry<SurveyReportResult> _repo = new DapperRepositry<SurveyReportResult>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@PageSize", pe.pgsize, DbType.String, ParameterDirection.Input);
            param.Add("@PageIndex", pe.pgindex, DbType.String, ParameterDirection.Input);
            param.Add("@Searchstr", pe.str, DbType.String, ParameterDirection.Input);
            param.Add("@SortBy", pe.sortby, DbType.String, ParameterDirection.Input);
            param.Add("@BusinessId", bid, DbType.Int32, ParameterDirection.Input);
            param.Add("@SurveyId", sid, DbType.Int32, ParameterDirection.Input);
            return _repo.GetList("GetSurveyResultsbyBusiness", param);
        }
        /// <summary>
        /// Get Survey Results By Survey Result Id
        /// </summary>
        /// <param name="SrId">SurveyResultId</param>
        /// <returns>Survey Answers which user choosen answers</returns>
        public List<SurveyAnswerResult> GetSurveyResultByResultId(Int64 SrId)
        {
            DapperRepositry<SurveyAnswerResult> _repo = new DapperRepositry<SurveyAnswerResult>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@SurveyResultId", SrId, DbType.Int64, ParameterDirection.Input);
            return _repo.GetList("GetSurveyResultbyResultId", param);
        }
    }
}
