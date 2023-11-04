using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DbFactory.Repositories;
using DIGITAL_GAMIFY.Entities;
using DbFactory;
using System.Data;

namespace DIGITAL_GAMIFY.DAL
{
    public class Surveydata
    {
        /// <summary>
        /// Get Survey List
        /// </summary>
        /// <param name="BusinessId">BusinessId</param>
        /// <param name="cid">Customerid</param>
        /// <returns></returns>
        public List<SurveyEntity> GetSurveyList(paggingEntity pe,Int32 BusinessId,Int32 cid)
        {
            DapperRepositry<SurveyEntity> _repo = new DapperRepositry<SurveyEntity>(Settings.ProviederName, Settings.DbConnection);
            DynamicParameters param = new DynamicParameters();
            param.Add("@PageSize", pe.pgsize, DbType.String, ParameterDirection.Input);
            param.Add("@PageIndex", pe.pgindex, DbType.String, ParameterDirection.Input);
            param.Add("@Searchstr", pe.str, DbType.String, ParameterDirection.Input);
            param.Add("@SortBy", pe.sortby, DbType.String, ParameterDirection.Input);
            param.Add("@BusinessId", BusinessId, DbType.Int64, ParameterDirection.Input);
            param.Add("@CustomerId", cid, DbType.Int32, ParameterDirection.Input);
            return _repo.GetList("GetSurveyList", param);
        }
        /// <summary>
        /// Get Survey List
        /// </summary>
        /// <param name="orgid">BusinessId</param>
        /// <returns></returns>
        public List<SurveyEntity> GetAdminSurveyList(paggingEntity pe, Int32 adminid, Int32 bid)
        {
            DapperRepositry<SurveyEntity> _repo = new DapperRepositry<SurveyEntity>(Settings.ProviederName, Settings.DbConnection);
            DynamicParameters param = new DynamicParameters();
            param.Add("@PageSize", pe.pgsize, DbType.String, ParameterDirection.Input);
            param.Add("@PageIndex", pe.pgindex, DbType.String, ParameterDirection.Input);
            param.Add("@FromDate", pe.FromDate, DbType.String, ParameterDirection.Input);
            param.Add("@ToDate", pe.ToDate, DbType.String, ParameterDirection.Input);
            param.Add("@Search", pe.str, DbType.String, ParameterDirection.Input);
            param.Add("@AdminId", adminid, DbType.Int32, ParameterDirection.Input);
            param.Add("@BusinessId", bid, DbType.Int32, ParameterDirection.Input);
            return _repo.GetList("GetAdminSurveyList", param);
        }
        /// <summary>
        /// adding Survey
        /// </summary>
        /// <returns></returns>
        public StatusResponse AddSurvey(SurveyEntity se)
        {
            DapperRepositry<StatusResponse> _repo = new DapperRepositry<StatusResponse>(Settings.ProviederName, Settings.DbConnection);
            DynamicParameters param = new DynamicParameters();
            param.Add("@SurveyId", se.SurveyId, DbType.Int32, ParameterDirection.Input);
            param.Add("@BusinessId", se.BusinessId, DbType.Int64, ParameterDirection.Input);
            param.Add("@AdminId", se.AdminId, DbType.Int32, ParameterDirection.Input);
            param.Add("@SurveyName", se.SurveyName, DbType.String, ParameterDirection.Input);
            param.Add("@SurveyCode", se.SurveyCode, DbType.String, ParameterDirection.Input);
            param.Add("@StartDate", se.StartDate, DbType.DateTime, ParameterDirection.Input);
            param.Add("@EndDate", se.EndDate, DbType.DateTime, ParameterDirection.Input);
            param.Add("@SmsCode", se.SmsCode, DbType.String, ParameterDirection.Input);
            param.Add("@Surveyimage", se.Surveyimage, DbType.String, ParameterDirection.Input);
            param.Add("@QRCode", se.QRCode, DbType.String, ParameterDirection.Input);
            param.Add("@IsReferFriend", se.IsReferFriend, DbType.Int16, ParameterDirection.Input);
            param.Add("@Status", se.Status, DbType.Int16, ParameterDirection.Input);
            param.Add("@Generalpublic", se.Generalpublic, DbType.Int16, ParameterDirection.Input);
            param.Add("@ShortDescription", se.ShortDescription, DbType.String, ParameterDirection.Input);
            param.Add("@FirstPrizeImage", se.FirstPrizeImage, DbType.String, ParameterDirection.Input);
            param.Add("@FirstPrizeText", se.FirstPrizeText, DbType.String, ParameterDirection.Input);
            param.Add("@FirstPrizeCount", se.FirstPrizeCount, DbType.Int16, ParameterDirection.Input);
            param.Add("@SecondPrizeImage", se.SecondPrizeImage, DbType.String, ParameterDirection.Input);
            param.Add("@SecondPrizeText", se.SecondPrizeText, DbType.String, ParameterDirection.Input);
            param.Add("@SecondPrizeCount", se.SecondPrizeCount, DbType.Int16, ParameterDirection.Input);
            param.Add("@IsAgeRequire", se.IsAgeRequire, DbType.Int16, ParameterDirection.Input);
            param.Add("@Conditions", se.Conditions, DbType.String, ParameterDirection.Input);
            param.Add("@IsComplimentary", se.IsComplimentary, DbType.Int16, ParameterDirection.Input);
            param.Add("@AgeCondition", se.AgeCondition, DbType.Int16, ParameterDirection.Input);
            param.Add("@OnceIn", se.OnceIn, DbType.Int16, ParameterDirection.Input);
            param.Add("@Interval", se.Interval, DbType.String, ParameterDirection.Input);
            param.Add("@IntervalId", se.IntervalId, DbType.Int32, ParameterDirection.Input);
            param.Add("@PhysicalPrize1", se.PhysicalPrize1, DbType.Int16, ParameterDirection.Input);
            param.Add("@Attributes1", se.Attributes1, DbType.String, ParameterDirection.Input);
            param.Add("@PhysicalPrize2", se.PhysicalPrize2, DbType.Int16, ParameterDirection.Input);
            param.Add("@Attributes2", se.Attributes2, DbType.String, ParameterDirection.Input);

            return _repo.GetResult("AddSurvey", param);
        }
        /// <summary>
        /// adding questions and answers
        /// </summary>
        /// <returns></returns>
        public StatusResponse AddUpdateSurveyQandA(SurveyEntity sEntity)
        {
            DapperRepositry<StatusResponse> _repo = new DapperRepositry<StatusResponse>(Settings.ProviederName, Settings.DbConnection);
            DynamicParameters param = new DynamicParameters();
            param.Add("@SurveyId", sEntity.SurveyId, DbType.Int32, ParameterDirection.Input);
            param.Add("@QandA", sEntity.QandAValues, DbType.String, ParameterDirection.Input);
            return _repo.GetResult("AdminAddUpdateSurveyQandA", param);
        }
        /// <summary>
        /// get survey details by survey id
        /// </summary>
        /// <param name="sid">SurveyId</param>
        /// <returns></returns>
        public SurveyQuestionandAnswer GetAdminSurveybyId(Int64 sid)
        {
            DbFactory.DbSettings _db = new DbFactory.DbSettings(Settings.ProviederName, Settings.DbConnection);
            SurveyQuestionandAnswer _repo = new SurveyQuestionandAnswer();
            List<SurveyQuestions> sq = new List<SurveyQuestions>();
            List<SurveyAnswers> sa = new List<SurveyAnswers>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@SurveyId", sid, DbType.Int64, ParameterDirection.Input);
            using(IDbConnection db=(IDbConnection)_db.ConnectionString)
            {
                var result = db.QueryMultiple("GetAdminSurveyQandABySurveyId", commandType: CommandType.StoredProcedure, param: param);
                _repo.SurveyDetails = result.Read<SurveyEntity>().FirstOrDefault();
                sq = result.Read<SurveyQuestions>().ToList();
                sa = result.Read<SurveyAnswers>().ToList();
                foreach(SurveyQuestions surq in sq)
                {
                    surq.answers = sa.Where(s => s.SurveyquestionId == surq.SurveyquestionId).ToList();
                }
                _repo.question = sq;
            }
            return _repo;
        }
        /// <summary>
        /// get survey details by survey id
        /// </summary>
        /// <param name="sid">SurveyId</param>
        /// <returns></returns>
        public SurveyQuestionandAnswer GetSurveybyId(Int32 sid,Int32 cid)
        {
            DbFactory.DbSettings _db = new DbFactory.DbSettings(Settings.ProviederName, Settings.DbConnection);
            SurveyQuestionandAnswer _repo = new SurveyQuestionandAnswer();
            List<SurveyQuestions> sq = new List<SurveyQuestions>();
            List<SurveyAnswers> sa = new List<SurveyAnswers>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@SurveyId", sid, DbType.Int32, ParameterDirection.Input);
            param.Add("@CustomerId", cid, DbType.Int32, ParameterDirection.Input);
            using (IDbConnection db = (IDbConnection)_db.ConnectionString)
            {
                var result = db.QueryMultiple("GetSurveyQandABySurveyId", commandType: CommandType.StoredProcedure, param: param);
                _repo.SurveyDetails = result.Read<SurveyEntity>().FirstOrDefault();
                sq = result.Read<SurveyQuestions>().ToList();
                sa = result.Read<SurveyAnswers>().ToList();
                foreach (SurveyQuestions surq in sq)
                {
                    surq.answers = sa.Where(s => s.SurveyquestionId == surq.SurveyquestionId).ToList();
                }
                _repo.question = sq;
            }
            return _repo;
        }
        public SurveyEntity DeleteQuestions(Int32 qid)
        {
            DapperRepositry<SurveyEntity> _repo = new DapperRepositry<SurveyEntity>(Settings.ProviederName,Settings.DbConnection);
            DynamicParameters param = new DynamicParameters();
            param.Add("@QuestionIds", qid, DbType.String, ParameterDirection.Input);
            return _repo.GetResult("AdminDeleteSurveyQuestions", param);
        }
        public SurveyEntity DeleteAnswers(Int32 aid)
        {
            DapperRepositry<SurveyEntity> _repo = new DapperRepositry<SurveyEntity>(Settings.ProviederName, Settings.DbConnection);
            DynamicParameters param = new DynamicParameters();
            param.Add("@AnswerIds", aid, DbType.String, ParameterDirection.Input);
            return _repo.GetResult("AdminDeleteSurveyQuestionAnswers", param);
        }
        public SurveyEntity DeleteSurvey(Int32 sid)
        {
            DapperRepositry<SurveyEntity> _repo = new DapperRepositry<SurveyEntity>(Settings.ProviederName, Settings.DbConnection);
            DynamicParameters param = new DynamicParameters();
            param.Add("@SurveyId", sid, DbType.Int32, ParameterDirection.Input);
            return _repo.GetResult("DeleteSurvey", param);
        }
        /// <summary>
        /// take survey from customer
        /// </summary>
        /// <param name="sid">srveyid</param>
        /// <param name="cid">customerid</param>
        /// <param name="qid">questionid</param>
        /// <param name="aid">answerid</param>
        /// <param name="IsCorrect">isanswercorrect</param>
        /// <param name="Duration">duration</param>
        /// <returns></returns>
        public SurveyCustomerQuestion InsertCustomerSurveyAnswer(Int32 sid, Int32 cid, Int64 qid, Int64 aid, string answertext, Int32 Duration)
        {
            DapperRepositry<SurveyCustomerQuestion> _repo = new DapperRepositry<SurveyCustomerQuestion>(Settings.ProviederName, Settings.DbConnection);
            DynamicParameters param = new DynamicParameters();
            param.Add("@SurveyId", sid, DbType.Int32, ParameterDirection.Input);
            param.Add("@CustomerId", cid, DbType.Int32, ParameterDirection.Input);
            param.Add("@QuestionId", qid, DbType.Int64, ParameterDirection.Input);
            param.Add("@AnswerId", aid, DbType.Int64, ParameterDirection.Input);
            param.Add("@Duration", Duration, DbType.Int32, ParameterDirection.Input);
            param.Add("@AnswerText", answertext, DbType.String, ParameterDirection.Input);

            return _repo.GetResult("InsertCustomerSurveyAnswer", param);
        }
        public List<SurveyAnswers> GetSurveyAnswersByQuetionId(Int64 Qid)
        {
            DapperRepositry<SurveyAnswers> _repo = new DapperRepositry<SurveyAnswers>(Settings.ProviederName, Settings.DbConnection);
            DynamicParameters param = new DynamicParameters();
            param.Add("@QuestionId", Qid, DbType.Int32, ParameterDirection.Input);
            return _repo.GetList("GetAnswersByQuestionId", param);
        }
        /// <summary>
        /// Check Survey Exist
        /// </summary>
        /// <param name="sid">Surveyid</param>
        /// <param name="sname">Surveyname</param>
        /// <param name="smscode">smscode</param>
        /// <returns></returns>
        public StatusResponse CheckSurveyExist(Int32 sid,string sname,string smscode)
        {
            DapperRepositry<StatusResponse> _repo = new DapperRepositry<StatusResponse>(Settings.ProviederName, Settings.DbConnection);
            DynamicParameters param = new DynamicParameters();
            param.Add("@SurveyId", sid, DbType.Int32, ParameterDirection.Input);
            param.Add("@SurveyName", sname, DbType.String, ParameterDirection.Input);
            param.Add("@SmsCode", smscode, DbType.String, ParameterDirection.Input);

            return _repo.GetResult("CheckSurveyExist", param);
        }
        public List<Quizddl> GetddlSurveys(Int64 bid)
        {
            DapperRepositry<Quizddl> _repo = new DapperRepositry<Quizddl>(Settings.ProviederName, Settings.DbConnection);
            DynamicParameters param = new DynamicParameters();

            param.Add("@BusinessId", bid, DbType.Int64, ParameterDirection.Input);

            return _repo.GetList("GetSurveyddl", param);
        }
        public List<SurveyResult> GetCustomerSurveyPrizes(Int64 cid, Int64 bid)
        {
            DapperRepositry<SurveyResult> _repo = new DapperRepositry<SurveyResult>(Settings.ProviederName, Settings.DbConnection);
            DynamicParameters param = new DynamicParameters();

            param.Add("@CustomerId", cid, DbType.Int64, ParameterDirection.Input);

            param.Add("@BusinessId", bid, DbType.Int64, ParameterDirection.Input);

            return _repo.GetList("GetCustomerSurveyPrizes", param);
        }
        public List<SurveyResult> AdminGetSurveyPrizes(paggingEntity p, Int32 bid, int status,Int64 gid,int prize)
        {
            try
            {
                DapperRepositry<SurveyResult> _repo = new DapperRepositry<SurveyResult>(Settings.ProviederName, Settings.DbConnection);
                DynamicParameters param = new DynamicParameters();
                param.Add("@BusinessId", bid, DbType.Int32, ParameterDirection.Input);
                param.Add("@SearchStr", p.str, DbType.String, ParameterDirection.Input);
                param.Add("@PageIndex", p.pgindex, DbType.Int32, ParameterDirection.Input);
                param.Add("@PageSize", p.pgsize, DbType.Int32, ParameterDirection.Input);
                param.Add("@Status", status, DbType.Int32, ParameterDirection.Input);
                param.Add("@GameId", gid, DbType.Int64, ParameterDirection.Input);
                param.Add("@PrizeId", prize, DbType.Int16, ParameterDirection.Input);
                return _repo.GetList("GetSurveyPrizesByBusiness", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SurveyEntity RedeemPrize(Int64 sid, Int64 cid, string size, string colour,int PrizeId, string RedeemCode,string interval,int intervalid,string Address)
        {
            try
            {
                DapperRepositry<SurveyEntity> _repo = new DapperRepositry<SurveyEntity>(Settings.ProviederName, Settings.DbConnection);
                DynamicParameters param = new DynamicParameters();
                param.Add("@SurveyId", sid, DbType.Int64, ParameterDirection.Input);
                param.Add("@CustomerId", cid, DbType.Int64, ParameterDirection.Input);
                param.Add("@Size", size, DbType.String, ParameterDirection.Input);
                param.Add("@Colour", colour, DbType.String, ParameterDirection.Input);
                param.Add("@PrizeId", PrizeId, DbType.Int16, ParameterDirection.Input);
                param.Add("@RedeemCode", RedeemCode, DbType.String, ParameterDirection.Input);
                param.Add("@Interval", interval, DbType.String, ParameterDirection.Input);
                param.Add("@IntervalId", intervalid, DbType.Int16, ParameterDirection.Input);
                param.Add("@Address", Address, DbType.String, ParameterDirection.Input);
                return _repo.GetResult("RedeemSurveyPrize", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
