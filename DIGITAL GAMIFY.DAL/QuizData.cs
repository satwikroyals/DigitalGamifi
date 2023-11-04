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
using System.Data.SqlClient;

namespace DIGITAL_GAMIFY.DAL
{
    public class QuizData
    {
        public QuizQuestionAndAnswer getQuizById(Int64 qzid, Int64 cid)
        {
            DbFactory.DbSettings _db = new DbFactory.DbSettings(Settings.ProviederName, Settings.DbConnection);
            QuizQuestionAndAnswer _repo = new QuizQuestionAndAnswer();
            List<QuizQuestions> sq = new List<QuizQuestions>();
            List<QuizAnswers> sa = new List<QuizAnswers>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@QuizId", qzid, DbType.Int32, ParameterDirection.Input);
            param.Add("@CustomerId", cid, DbType.Int32, ParameterDirection.Input);
            using (IDbConnection db = (IDbConnection)_db.ConnectionString)
            {
                var result = db.QueryMultiple("GetQuizQandAByQuizId", commandType: CommandType.StoredProcedure, param: param);
                _repo.QuizDetails = result.Read<QuizEntity>().FirstOrDefault();
                sq = result.Read<QuizQuestions>().ToList();
                sa = result.Read<QuizAnswers>().ToList();
                foreach (QuizQuestions surq in sq)
                {
                    surq.answers = sa.Where(s => s.QuestionNumber == surq.QuestionNum).ToList();
                }
                _repo.Question = sq;
            }
            return _repo;
        }
        public List<QuizEntity> GetQuizList(paggingEntity pe, Int32 adminid,Int32 bid,Int32 cid)
        {
            DapperRepositry<QuizEntity> _repo = new DapperRepositry<QuizEntity>(Settings.ProviederName, Settings.DbConnection);
            DynamicParameters param = new DynamicParameters();
            param.Add("@PageSize", pe.pgsize, DbType.String, ParameterDirection.Input);
            param.Add("@PageIndex", pe.pgindex, DbType.String, ParameterDirection.Input);
            param.Add("@Searchstr", pe.str, DbType.String, ParameterDirection.Input);
            param.Add("@SortBy", pe.sortby, DbType.String, ParameterDirection.Input);
            param.Add("@AdminId", adminid, DbType.Int64, ParameterDirection.Input);
            param.Add("@BusinessId", bid, DbType.Int32, ParameterDirection.Input);
            param.Add("@CustomerId", cid, DbType.Int32, ParameterDirection.Input);
            return _repo.GetList("GetQuizList", param);
        }


        /// <summary>
        /// take smartquiz from customer
        /// </summary>
        /// <param name="sqid">smartquizid</param>
        /// <param name="cid">customerid</param>
        /// <param name="qid">questionid</param>
        /// <param name="aid">answerid</param>
        /// <param name="IsCorrect">isanswercorrect</param>
        /// <param name="Duration">duration</param>
        /// <returns></returns>
        public QuizCustomerQuestion InsertCustomerQuizAnswer(Int64 quizid, Int64 cid, Int64 qid, Int64 aid, int IsCorrect, int Duration)
        {
            DapperRepositry<QuizCustomerQuestion> _repo = new DapperRepositry<QuizCustomerQuestion>(Settings.ProviederName, Settings.DbConnection);
            DynamicParameters param = new DynamicParameters();
            param.Add("@QuizId", quizid, DbType.Int32, ParameterDirection.Input);
            param.Add("@CustomerId", cid, DbType.Int32, ParameterDirection.Input);
            param.Add("@QuestionId", qid, DbType.Int32, ParameterDirection.Input);
            param.Add("@AnswerId", aid, DbType.Int32, ParameterDirection.Input);
            param.Add("@IsCorrect", IsCorrect, DbType.Int32, ParameterDirection.Input);
            param.Add("@Duration", Duration, DbType.Int32, ParameterDirection.Input);

            return _repo.GetResult("InsertCustomerQuizAnswer", param);
        }
        public List<QuizAnswers> GetQuizAnswersByQuestionId(Int32 Qnum, Int64 qzid)
        {
            DapperRepositry<QuizAnswers> _repo = new DapperRepositry<QuizAnswers>(Settings.ProviederName, Settings.DbConnection);
            DynamicParameters param = new DynamicParameters();
            param.Add("@QuizId", qzid, DbType.Int32, ParameterDirection.Input);
            param.Add("@QuestionNumber", Qnum, DbType.Int32, ParameterDirection.Input);
            return _repo.GetList("GetQuizAnswersByQuestionId", param);
        }
        public StatusResponse AddQuizGame(QuizEntity sqEntity)
        {
            DapperRepositry<StatusResponse> _repo = new DapperRepositry<StatusResponse>(Settings.ProviederName, Settings.DbConnection);
            DynamicParameters param = new DynamicParameters();
            param.Add("@QuizId", sqEntity.QuizId, DbType.Int32, ParameterDirection.Input);
            param.Add("@AdminId", sqEntity.AdminId, DbType.Int32, ParameterDirection.Input);
            param.Add("@BusinessId", sqEntity.BusinessId, DbType.Int32, ParameterDirection.Input);
            param.Add("@QuizName", sqEntity.QuizName, DbType.String, ParameterDirection.Input);
            param.Add("@QuizCode", sqEntity.QuizCode, DbType.String, ParameterDirection.Input);
            param.Add("@StartDate", sqEntity.StartDate, DbType.DateTime, ParameterDirection.Input);
            param.Add("@EndDate", sqEntity.EndDate, DbType.DateTime, ParameterDirection.Input);
            param.Add("@SmsCode", sqEntity.SmsCode, DbType.String, ParameterDirection.Input);
            param.Add("@QuizImage", sqEntity.QuizImage, DbType.String, ParameterDirection.Input);
            param.Add("@QRCode", sqEntity.QRCode, DbType.String, ParameterDirection.Input);
            param.Add("@IsReferFriend", sqEntity.IsReferFriend, DbType.Int16, ParameterDirection.Input);
            param.Add("@Status", sqEntity.Status, DbType.Int16, ParameterDirection.Input);
            param.Add("@FirstPrizeImage", sqEntity.FirstPrizeImage, DbType.String, ParameterDirection.Input);
            param.Add("@FirstPrizeText", sqEntity.FirstPrizeText, DbType.String, ParameterDirection.Input);
            param.Add("@FirstPrizeCount", sqEntity.FirstPrizeCount, DbType.Int16, ParameterDirection.Input);
            param.Add("@SecondPrizeImage", sqEntity.SecondPrizeImage, DbType.String, ParameterDirection.Input);
            param.Add("@SecondPrizeText", sqEntity.SecondPrizeText, DbType.String, ParameterDirection.Input);
            param.Add("@SecondPrizeCount", sqEntity.SecondPrizeCount, DbType.Int16, ParameterDirection.Input);
            param.Add("@ShortDescription", sqEntity.ShortDescription, DbType.String, ParameterDirection.Input);
            param.Add("@IsAgeRequire", sqEntity.IsAgeRequire, DbType.Int16, ParameterDirection.Input);
            param.Add("@Conditions", sqEntity.Conditions, DbType.String, ParameterDirection.Input);
            param.Add("@IsComplimentary", sqEntity.IsComplimentary, DbType.Int16, ParameterDirection.Input);
            param.Add("@AgeCondition", sqEntity.AgeCondition, DbType.Int16, ParameterDirection.Input);
            param.Add("@OnceIn", sqEntity.OnceIn, DbType.Int16, ParameterDirection.Input);
            param.Add("@Interval", sqEntity.Interval, DbType.String, ParameterDirection.Input);
            param.Add("@IntervalId", sqEntity.IntervalId, DbType.Int32, ParameterDirection.Input);
            param.Add("@PhysicalPrize1", sqEntity.PhysicalPrize1, DbType.Int16, ParameterDirection.Input);
            param.Add("@Attributes1", sqEntity.Attributes1, DbType.String, ParameterDirection.Input);
            param.Add("@PhysicalPrize2", sqEntity.PhysicalPrize2, DbType.Int16, ParameterDirection.Input);
            param.Add("@Attributes2", sqEntity.Attributes2, DbType.String, ParameterDirection.Input);
            //param.Add("@GameLink", sqEntity.GameLink, DbType.String, ParameterDirection.Input);

            return _repo.GetResult("AddQuizGame", param);
        }
        public StatusResponse AddQuizQuestions(QuizCustomerQuestion sqEntity)
        {
            DapperRepositry<StatusResponse> _repo = new DapperRepositry<StatusResponse>(Settings.ProviederName, Settings.DbConnection);
            DynamicParameters param = new DynamicParameters();
            param.Add("@QuizquestionId", sqEntity.QuizQuestionId, DbType.Int32, ParameterDirection.Input);
            param.Add("@QuizId", sqEntity.QuizId, DbType.Int32, ParameterDirection.Input);
            param.Add("@QuestionNum", sqEntity.QuestionNum, DbType.Int16, ParameterDirection.Input);
            param.Add("@Question", sqEntity.Question, DbType.String, ParameterDirection.Input);
            param.Add("@CorrectAnswerId", sqEntity.CorrectAnswerId, DbType.Int16, ParameterDirection.Input);

            return _repo.GetResult("AddupdateQuizQuestions", param);
        }
        public StatusResponse AddQuizQuestionAnswers(QuizAnswers sqaEntity)
        {
            DapperRepositry<StatusResponse> _repo = new DapperRepositry<StatusResponse>(Settings.ProviederName, Settings.DbConnection);
            DynamicParameters param = new DynamicParameters();
            param.Add("@QuizAnswerId", sqaEntity.QuizAnswerId, DbType.Int32, ParameterDirection.Input);
            param.Add("@QuestionNumber", sqaEntity.QestionNumber, DbType.Int32, ParameterDirection.Input);
            param.Add("@QuizId", sqaEntity.QuizId, DbType.Int32, ParameterDirection.Input);
            param.Add("@AnswerNumber", sqaEntity.AnswerNumber, DbType.Int16, ParameterDirection.Input);
            param.Add("@Answer", sqaEntity.Answer, DbType.String, ParameterDirection.Input);

            return _repo.GetResult("AddupdateQuizQuestionAnswers", param);
        }
        public List<QuizEntity> GetAdminQuizList(paggingEntity pe, Int32 adminid, Int32 bid)
        {
            DapperRepositry<QuizEntity> _repo = new DapperRepositry<QuizEntity>(Settings.ProviederName, Settings.DbConnection);
            DynamicParameters param = new DynamicParameters();
            param.Add("@PageSize", pe.pgsize, DbType.String, ParameterDirection.Input);
            param.Add("@PageIndex", pe.pgindex, DbType.String, ParameterDirection.Input);
            param.Add("@FromDate", pe.FromDate, DbType.String, ParameterDirection.Input);
            param.Add("@ToDate", pe.ToDate, DbType.String, ParameterDirection.Input);
            param.Add("@Search", pe.str, DbType.String, ParameterDirection.Input);
            param.Add("@AdminId", adminid, DbType.Int64, ParameterDirection.Input);
            param.Add("@BusinessId", bid, DbType.Int32, ParameterDirection.Input);

            return _repo.GetList("GetAdminQuizList", param);
        }
        public QuizQuestionAndAnswer getAdminQuizById(Int32 sid)
        {
            DbFactory.DbSettings _db = new DbFactory.DbSettings(Settings.ProviederName, Settings.DbConnection);
            QuizQuestionAndAnswer _repo = new QuizQuestionAndAnswer();
            List<QuizQuestions> sq = new List<QuizQuestions>();
            List<QuizAnswers> sa = new List<QuizAnswers>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@QuizId", sid, DbType.Int32, ParameterDirection.Input);
            using (IDbConnection db = (IDbConnection)_db.ConnectionString)
            {
                var result = db.QueryMultiple("GetAdminQandAByQuizId", commandType: CommandType.StoredProcedure, param: param);
                _repo.QuizDetails = result.Read<QuizEntity>().FirstOrDefault();
                sq = result.Read<QuizQuestions>().ToList();
                sa = result.Read<QuizAnswers>().ToList();
                foreach (QuizQuestions surq in sq)
                {
                    surq.answers = sa.Where(s => s.QuestionNumber == surq.QuestionNum).ToList();
                }
                _repo.Question = sq;
            }
            return _repo;
        }
        public List<quizResultEntity> GetQuizResult(paggingEntity pe, Int32 quizid)
        {
            DapperRepositry<quizResultEntity> _repo = new DapperRepositry<quizResultEntity>(Settings.ProviederName, Settings.DbConnection);
            DynamicParameters param = new DynamicParameters();
            param.Add("@PageSize", pe.pgsize, DbType.String, ParameterDirection.Input);
            param.Add("@PageIndex", pe.pgindex, DbType.String, ParameterDirection.Input);
            param.Add("@Searchstr", pe.str, DbType.String, ParameterDirection.Input);
            param.Add("@SortBy", pe.sortby, DbType.String, ParameterDirection.Input);
            param.Add("@QuizId", quizid, DbType.Int32, ParameterDirection.Input);

            return _repo.GetList("GetAdminQuizResult", param);
        }
        public List<quizResultEntity> GetCustomerQuizResult(Int64 quizid,Int64 cid)
        {
            DapperRepositry<quizResultEntity> _repo = new DapperRepositry<quizResultEntity>(Settings.ProviederName, Settings.DbConnection);
            DynamicParameters param = new DynamicParameters();
           
            param.Add("@QuizId", quizid, DbType.Int64, ParameterDirection.Input);

            param.Add("@CustomerId", cid, DbType.Int64, ParameterDirection.Input);

            return _repo.GetList("GetCustomerQuizResults", param);
        }
        public List<Quizddl> GetddlQuizes(Int32 bid)
        {
            DapperRepositry<Quizddl> _repo = new DapperRepositry<Quizddl>(Settings.ProviederName, Settings.DbConnection);
            DynamicParameters param = new DynamicParameters();

            param.Add("@BusinessId", bid, DbType.Int64, ParameterDirection.Input);

            return _repo.GetList("GetQuizddl", param);
        }
        public QuizEntity DeleteQuiz(Int32 qid)
        {
            DapperRepositry<QuizEntity> _repo = new DapperRepositry<QuizEntity>(Settings.ProviederName, Settings.DbConnection);
            DynamicParameters param = new DynamicParameters();
            param.Add("@QuizId", qid, DbType.Int32, ParameterDirection.Input);
            return _repo.GetResult("DeleteQuiz", param);
        }

        
        public QuizQuestionAndAnswer GetQuizStatusByCustomer(Int64 qzid, Int64 cid)
        {
            DbFactory.DbSettings _db = new DbFactory.DbSettings(Settings.ProviederName, Settings.DbConnection);
            QuizQuestionAndAnswer _repo = new QuizQuestionAndAnswer();
            List<QuizQuestions> sq = new List<QuizQuestions>();
            List<QuizAnswers> sa = new List<QuizAnswers>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@QuizId", qzid, DbType.Int32, ParameterDirection.Input);
            param.Add("@CustomerId", cid, DbType.Int32, ParameterDirection.Input);
            using (IDbConnection db = (IDbConnection)_db.ConnectionString)
            {
                var result = db.QueryMultiple("GetQuizStatusByCustomer", commandType: CommandType.StoredProcedure, param: param);
                _repo.QuizDetails = result.Read<QuizEntity>().FirstOrDefault();
                sq = result.Read<QuizQuestions>().ToList();
                sa = result.Read<QuizAnswers>().ToList();
                foreach (QuizQuestions surq in sq)
                {
                    surq.answers = sa.Where(s => s.QuestionNumber == surq.QuestionNum).ToList();
                }
                _repo.Question = sq;
            }
            return _repo;
        }

        public quizResultEntity InsertQuizCustomerAllAnswers(CustomerQuizAnswers cs)
        {
            string connection = Settings.DbConnection;
            quizResultEntity sb = new quizResultEntity();
            try
            {
                DataTable custanswers = getdatatblofcustspcanswers(cs.Answers);
                SqlConnection con = new SqlConnection(connection);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("InsertCustomerQuizAllAnswers", con);

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@Answers",
                    SqlDbType = SqlDbType.Structured,
                    Value = custanswers
                });
                cmd.Parameters.AddWithValue("@QuizId", cs.QuizId);
                cmd.Parameters.AddWithValue("@CustomerId", cs.CustomerId);
                cmd.Parameters.AddWithValue("@Duration", cs.Duration);
                cmd.Parameters.AddWithValue("@CorrectAnsweredCount", cs.CorrectAnsweredCount);
                cmd.Parameters.AddWithValue("@AnsweredCount", cs.AnsweredCount);
				cmd.Parameters.AddWithValue("@PrizeId", cs.PrizeId);
                cmd.Parameters.AddWithValue("@RedeemCode", cs.RedeemCode);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {

                        sb.AnsweredCount = Convert.ToInt16(dr["AnsweredCount"]);
                        sb.CorrectAnswerCount = Convert.ToInt16(dr["CorrectAnswerCount"]);
                        sb.Duration = Convert.ToInt64(dr["Duration"]);
                        sb.QuizId = Convert.ToInt32(dr["QuizId"]);
                        sb.QuizResultId = Convert.ToInt32(dr["QuizResultId"]);
                        sb.StartTime = Convert.ToDateTime(dr["StartTime"]);
                        sb.EndTime = Convert.ToDateTime(dr["EndTime"]);

                    }
                }
                cmd.Dispose();
                dr.Close();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return sb;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataTable getdatatblofcustspcanswers(List<CustomerAnswers> sbc)
        {
            DataTable custspcanswers = new DataTable();
            custspcanswers.Columns.Add("QuestionId");
            custspcanswers.Columns.Add("QuestionNumber");
            custspcanswers.Columns.Add("AnswerId");
            custspcanswers.Columns.Add("AnswerNumber");
            custspcanswers.Columns.Add("IsCorrect");
            foreach (CustomerAnswers t in sbc)
            {
                var values = new object[5];
                //inserting property values to datatable rows
                values[0] = t.QuestionId;
                values[1] = t.QuestionNumber;
                values[2] = t.AnswerId;
                values[3] = t.AnswerNumber;
                values[4] = t.IsCorrect;
                custspcanswers.Rows.Add(values);
            }
            return custspcanswers;
        }
        public List<QuizPrizesEntity> GetCustomerQuizPrizes(Int64 cid, Int64 bid)
        {
            DapperRepositry<QuizPrizesEntity> _repo = new DapperRepositry<QuizPrizesEntity>(Settings.ProviederName, Settings.DbConnection);
            DynamicParameters param = new DynamicParameters();

            param.Add("@CustomerId", cid, DbType.Int64, ParameterDirection.Input);

            param.Add("@BusinessId", bid, DbType.Int64, ParameterDirection.Input);

            return _repo.GetList("GetCustomerQuizPrizes", param);
        }
        public List<QuizPrizesEntity> AdminGetQuizPrizes(paggingEntity p, Int32 bid, int status,Int64 gid,int prize)
        {
            try
            {
                DapperRepositry<QuizPrizesEntity> _repo = new DapperRepositry<QuizPrizesEntity>(Settings.ProviederName, Settings.DbConnection);
                DynamicParameters param = new DynamicParameters();
                param.Add("@BusinessId", bid, DbType.Int32, ParameterDirection.Input);
                param.Add("@SearchStr", p.str, DbType.String, ParameterDirection.Input);
                param.Add("@PageIndex", p.pgindex, DbType.Int32, ParameterDirection.Input);
                param.Add("@PageSize", p.pgsize, DbType.Int32, ParameterDirection.Input);
                param.Add("@Status", status, DbType.Int32, ParameterDirection.Input);
                param.Add("@GameId", gid, DbType.Int64, ParameterDirection.Input);
                param.Add("@PrizeId", prize, DbType.Int16, ParameterDirection.Input);
                return _repo.GetList("GetQuizPrizesByBusiness", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public StatusResponse CustomerQuizRedeemPrize(Int64 sid, Int64 cid, int PrizeId, string RedeemCode, string size, string colour, string interval, int intervalid,string Address)
        {
            try
            {
                DapperRepositry<StatusResponse> _repo = new DapperRepositry<StatusResponse>(Settings.ProviederName, Settings.DbConnection);
                DynamicParameters param = new DynamicParameters();
                param.Add("@QuizId", sid, DbType.Int64, ParameterDirection.Input);
                param.Add("@CustomerId", cid, DbType.Int64, ParameterDirection.Input);
                param.Add("@PrizeId", PrizeId, DbType.Int16, ParameterDirection.Input);
                param.Add("@RedeemCode", RedeemCode, DbType.String, ParameterDirection.Input);
                param.Add("@Size", size, DbType.String, ParameterDirection.Input);
                param.Add("@Colour", colour, DbType.String, ParameterDirection.Input);
                param.Add("@Interval", interval, DbType.String, ParameterDirection.Input);
                param.Add("@IntervalId", intervalid, DbType.Int16, ParameterDirection.Input);
                param.Add("@Address", Address, DbType.String, ParameterDirection.Input);
                return _repo.GetResult("RedeemQuizPrize", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
