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
    public  class SmartQuizData
    {
        public SmartQuizQuestionAndAnswer getSmartQuizById(Int64 sid, Int64 cid)
        {
            DbFactory.DbSettings _db = new DbFactory.DbSettings(Settings.ProviederName, Settings.DbConnection);
            SmartQuizQuestionAndAnswer _repo = new SmartQuizQuestionAndAnswer();
            List<SmartQuizQuestions> sq = new List<SmartQuizQuestions>();
            List<SmartQuizAnswers> sa = new List<SmartQuizAnswers>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@SmartQuizId", sid, DbType.Int32, ParameterDirection.Input);
            param.Add("@CustomerId", cid, DbType.Int32, ParameterDirection.Input);
            using (IDbConnection db = (IDbConnection)_db.ConnectionString)
            {
                var result = db.QueryMultiple("GetSmartQandABySmartQuizId", commandType: CommandType.StoredProcedure, param: param);
                _repo.SmartQuizDetails = result.Read<SmartQuizEntity>().FirstOrDefault();
                sq = result.Read<SmartQuizQuestions>().ToList();
                sa = result.Read<SmartQuizAnswers>().ToList();
                foreach (SmartQuizQuestions surq in sq)
                {
                    surq.answers = sa.Where(s => s.QuestionNumber == surq.QuestionNum).ToList();
                }
                _repo.Question = sq;
            }
            return _repo;
        }
        public SmartQuizQuestionAndAnswer GetSmartQuizStatusByCustomer(Int64 sid, Int64 cid)
        {
            DbFactory.DbSettings _db = new DbFactory.DbSettings(Settings.ProviederName, Settings.DbConnection);
            SmartQuizQuestionAndAnswer _repo = new SmartQuizQuestionAndAnswer();
            List<SmartQuizQuestions> sq = new List<SmartQuizQuestions>();
            List<SmartQuizAnswers> sa = new List<SmartQuizAnswers>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@QuizId", sid, DbType.Int32, ParameterDirection.Input);
            param.Add("@CustomerId", cid, DbType.Int32, ParameterDirection.Input);
            using (IDbConnection db = (IDbConnection)_db.ConnectionString)
            {
                var result = db.QueryMultiple("GetSmartQuizStatusByCustomer", commandType: CommandType.StoredProcedure, param: param);
                _repo.SmartQuizDetails = result.Read<SmartQuizEntity>().FirstOrDefault();
                sq = result.Read<SmartQuizQuestions>().ToList();
                sa = result.Read<SmartQuizAnswers>().ToList();
                foreach (SmartQuizQuestions surq in sq)
                {
                    surq.answers = sa.Where(s => s.QuestionNumber == surq.QuestionNum).ToList();
                }
                _repo.Question = sq;
            }
            return _repo;
        }
        public List<SmartQuizEntity> GetSmartQuizList(paggingEntity pe, Int32 businessid,Int32 cid)
        {
            DapperRepositry<SmartQuizEntity> _repo = new DapperRepositry<SmartQuizEntity>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@PageSize", pe.pgsize, DbType.String, ParameterDirection.Input);
            param.Add("@PageIndex", pe.pgindex, DbType.String, ParameterDirection.Input);
            param.Add("@Searchstr", pe.str, DbType.String, ParameterDirection.Input);
            param.Add("@SortBy", pe.sortby, DbType.String, ParameterDirection.Input);
            param.Add("@BusinessId", businessid, DbType.Int64, ParameterDirection.Input);
            param.Add("@CustomerId", cid, DbType.Int32, ParameterDirection.Input);
            return _repo.GetList("GetSmartQuizList", param);
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
        public SmartQuizCustomerQuestion InsertCustomerSmartQuizAnswer(Int64 sqid, Int64 cid, Int64 qid, Int64 aid, int IsCorrect, int Duration)
        {
            DapperRepositry<SmartQuizCustomerQuestion> _repo = new DapperRepositry<SmartQuizCustomerQuestion>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@SmartQuizId", sqid, DbType.Int32, ParameterDirection.Input);
            param.Add("@CustomerId", cid, DbType.Int32, ParameterDirection.Input);
            param.Add("@QuestionId", qid, DbType.Int32, ParameterDirection.Input);
            param.Add("@AnswerId", aid, DbType.Int32, ParameterDirection.Input);
            param.Add("@IsCorrect", IsCorrect, DbType.Int32, ParameterDirection.Input);
            param.Add("@Duration", Duration, DbType.Int32, ParameterDirection.Input);
            return _repo.GetResult("InsertCustomerSmartQuizAnswer", param);
        }
        public List<smartquizresultEntity> GetCustomerSmartQuizResult(Int64 quizid, Int64 cid)
        {
            DapperRepositry<smartquizresultEntity> _repo = new DapperRepositry<smartquizresultEntity>();
            DynamicParameters param = new DynamicParameters();

            param.Add("@QuizId", quizid, DbType.Int64, ParameterDirection.Input);

            param.Add("@CustomerId", cid, DbType.Int64, ParameterDirection.Input);

            return _repo.GetList("GetCustomerSmartQuizResults", param);
        }
        public List<SmartQuizAnswers> GetSmartQuizAnswersByQuestionId(Int32 Qnum,Int64 qzid)
        {
            DapperRepositry<SmartQuizAnswers> _repo = new DapperRepositry<SmartQuizAnswers>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@SmartQuizId", qzid, DbType.Int32, ParameterDirection.Input);
            param.Add("@QuestionNumber", Qnum, DbType.Int32, ParameterDirection.Input);
            return _repo.GetList("GetSmartQuizAnswersByQuestionId", param);
        }
        public StatusResponse AddSmartQuizGame(SmartQuizEntity sqEntity)
        {
            DapperRepositry<StatusResponse> _repo = new DapperRepositry<StatusResponse>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@SmartQuizId", sqEntity.SmartQuizId, DbType.Int32, ParameterDirection.Input);
            param.Add("@AdminId", sqEntity.AdminId, DbType.Int32, ParameterDirection.Input);
            param.Add("@BusinessId", sqEntity.BusinessId, DbType.Int32, ParameterDirection.Input);
            param.Add("@SmartQuizName", sqEntity.SmartQuizName, DbType.String, ParameterDirection.Input);
            param.Add("@SmartQuizCode", sqEntity.SmartQuizCode, DbType.String, ParameterDirection.Input);
            param.Add("@StartDate", sqEntity.StartDate, DbType.DateTime, ParameterDirection.Input);
            param.Add("@EndDate", sqEntity.EndDate, DbType.DateTime, ParameterDirection.Input);
            param.Add("@SmsCode", sqEntity.SmsCode, DbType.String, ParameterDirection.Input);
            param.Add("@SmartQuizImage", sqEntity.SmartQuizImage, DbType.String, ParameterDirection.Input);
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

            return _repo.GetResult("AddSmartQuizGame", param);
        }
        public StatusResponse AddSmartQuizQuestions(SmartQuizCustomerQuestion sqEntity)
        {
            DapperRepositry<StatusResponse> _repo = new DapperRepositry<StatusResponse>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@SmartQuizquestionId", sqEntity.SmartQuizQuestionId, DbType.Int32, ParameterDirection.Input);
            param.Add("@SmartQuizId", sqEntity.SmartQuizId, DbType.Int32, ParameterDirection.Input);
            param.Add("@QuestionNum", sqEntity.QuestionNum, DbType.Int16, ParameterDirection.Input);
            param.Add("@Question", sqEntity.Question, DbType.String, ParameterDirection.Input);
            param.Add("@CorrectAnswerId", sqEntity.CorrectAnswerId, DbType.Int16, ParameterDirection.Input);

            return _repo.GetResult("AddupdateSmartQuizQuestions", param);
        }
        public StatusResponse AddSmartQuizQuestionAnswers(SmartQuizAnswers sqaEntity)
        {
            DapperRepositry<StatusResponse> _repo = new DapperRepositry<StatusResponse>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@SmartQuizAnswerId", sqaEntity.SmartQuizAnswerId, DbType.Int32, ParameterDirection.Input);
            param.Add("@QuestionNumber", sqaEntity.QestionNumber, DbType.Int32, ParameterDirection.Input);
            param.Add("@SmartQuizId", sqaEntity.SmartQuizId, DbType.Int32, ParameterDirection.Input);
            param.Add("@AnswerNumber", sqaEntity.AnswerNumber, DbType.Int16, ParameterDirection.Input);
            param.Add("@AnswerImage", sqaEntity.AnswerImage, DbType.String, ParameterDirection.Input);

            return _repo.GetResult("AddupdateSmartQuizQuestionAnswers", param);
        }
        public List<SmartQuizEntity> GetAdminSmartQuizList(paggingEntity pe,Int32 adminid,Int32 bid)
        {
            DapperRepositry<SmartQuizEntity> _repo = new DapperRepositry<SmartQuizEntity>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@PageSize", pe.pgsize, DbType.String, ParameterDirection.Input);
            param.Add("@PageIndex", pe.pgindex, DbType.String, ParameterDirection.Input);
            param.Add("@FromDate", pe.FromDate, DbType.String, ParameterDirection.Input);
            param.Add("@ToDate", pe.ToDate, DbType.String, ParameterDirection.Input);
            param.Add("@Search", pe.str, DbType.String, ParameterDirection.Input);
            param.Add("@AdminId", adminid, DbType.Int64, ParameterDirection.Input);
            param.Add("@BusinessId", bid, DbType.Int32, ParameterDirection.Input);

            return _repo.GetList("GetAdminSmartQuizList", param);
        }
        public SmartQuizQuestionAndAnswer getAdminSmartQuizById(Int32 sid)
        {
            DbFactory.DbSettings _db = new DbFactory.DbSettings(Settings.ProviederName, Settings.DbConnection);
            SmartQuizQuestionAndAnswer _repo = new SmartQuizQuestionAndAnswer();
            List<SmartQuizQuestions> sq = new List<SmartQuizQuestions>();
            List<SmartQuizAnswers> sa = new List<SmartQuizAnswers>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@SmartQuizId", sid, DbType.Int32, ParameterDirection.Input);
            using (IDbConnection db = (IDbConnection)_db.ConnectionString)
            {
                var result = db.QueryMultiple("GetAdminSmartQandABySmartQuizId", commandType: CommandType.StoredProcedure, param: param);
                _repo.SmartQuizDetails = result.Read<SmartQuizEntity>().FirstOrDefault();
                sq = result.Read<SmartQuizQuestions>().ToList();
                sa = result.Read<SmartQuizAnswers>().ToList();
                foreach (SmartQuizQuestions surq in sq)
                {
                    surq.answers = sa.Where(s => s.QuestionNumber == surq.QuestionNum).ToList();
                }
                _repo.Question = sq;
            }
            return _repo;
        }
        public List<SmartquizResultEntity> GetSmartQuizResult(paggingEntity pe,Int32 smquizid)
        {
            DapperRepositry<SmartquizResultEntity> _repo = new DapperRepositry<SmartquizResultEntity>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@PageSize", pe.pgsize, DbType.String, ParameterDirection.Input);
            param.Add("@PageIndex", pe.pgindex, DbType.String, ParameterDirection.Input);
            param.Add("@Searchstr", pe.str, DbType.String, ParameterDirection.Input);
            param.Add("@SortBy", pe.sortby, DbType.String, ParameterDirection.Input);
            param.Add("@SmquizId", smquizid, DbType.Int32, ParameterDirection.Input);

            return _repo.GetList("GetAdminSmartQuizResult", param);
        }
        public List<Quizddl> GetddlsmartQuizes(Int64 bid)
        {
            DapperRepositry<Quizddl> _repo = new DapperRepositry<Quizddl>();
            DynamicParameters param = new DynamicParameters();

            param.Add("@BusinessId", bid, DbType.Int64, ParameterDirection.Input);

            return _repo.GetList("GetSmartQuizddl", param);
        }
        public SmartQuizEntity DeleteSmartQuiz(Int32 sid)
        {
            DapperRepositry<SmartQuizEntity> _repo = new DapperRepositry<SmartQuizEntity>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@SmartQuizId", sid, DbType.Int32, ParameterDirection.Input);
            return _repo.GetResult("DeleteSmartQuiz", param);
        }
        public smartquizresultEntity InsertSmartQuizCustomerAllAnswers(CustomerQuizAnswers cs)
        {
            string connection = Settings.DbConnection;
            smartquizresultEntity sb = new smartquizresultEntity();
            try
            {
                DataTable custanswers = getdatatblofcustspcanswers(cs.Answers);
                SqlConnection con = new SqlConnection(connection);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("InsertCustomerSmartQuizAllAnswers", con);

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
                        sb.SMartQuizId = Convert.ToInt32(dr["SmartQuizId"]);
                        sb.SmartQuizResultId = Convert.ToInt32(dr["SmartQuizResultId"]);
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
        public List<SmarQuizPrizesEntity> GetCustomerSmartQuizPrizes(Int64 cid, Int64 bid)
        {
            DapperRepositry<SmarQuizPrizesEntity> _repo = new DapperRepositry<SmarQuizPrizesEntity>();
            DynamicParameters param = new DynamicParameters();

            param.Add("@CustomerId", cid, DbType.Int64, ParameterDirection.Input);

            param.Add("@BusinessId", bid, DbType.Int64, ParameterDirection.Input);

            return _repo.GetList("GetCustomerSmartQuizPrizes", param);
        }
        public List<SmarQuizPrizesEntity> AdminGetSmartQuizPrizes(paggingEntity p, Int32 bid, int status,Int64 gid,int prize)
        {
            try
            {
                DapperRepositry<SmarQuizPrizesEntity> _repo = new DapperRepositry<SmarQuizPrizesEntity>();
                DynamicParameters param = new DynamicParameters();
                param.Add("@BusinessId", bid, DbType.Int32, ParameterDirection.Input);
                param.Add("@SearchStr", p.str, DbType.String, ParameterDirection.Input);
                param.Add("@PageIndex", p.pgindex, DbType.Int32, ParameterDirection.Input);
                param.Add("@PageSize", p.pgsize, DbType.Int32, ParameterDirection.Input);
                param.Add("@Status", status, DbType.Int32, ParameterDirection.Input);
                param.Add("@GameId", gid, DbType.Int64, ParameterDirection.Input);
                param.Add("@PrizeId", prize, DbType.Int16, ParameterDirection.Input);
                return _repo.GetList("GetSmartQuizPrizesByBusiness", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public StatusResponse CustomerSmartQuizRedeemPrize(Int64 sid, Int64 cid, int PrizeId, string RedeemCode, string size, string colour,string interval,int intervalid,string Address)
        {
            try
            {
                DapperRepositry<StatusResponse> _repo = new DapperRepositry<StatusResponse>();
                DynamicParameters param = new DynamicParameters();
                param.Add("@SmartQuizId", sid, DbType.Int64, ParameterDirection.Input);
                param.Add("@CustomerId", cid, DbType.Int64, ParameterDirection.Input);
                param.Add("@PrizeId", PrizeId, DbType.Int16, ParameterDirection.Input);
                param.Add("@RedeemCode", RedeemCode, DbType.String, ParameterDirection.Input);
                param.Add("@Size", size, DbType.String, ParameterDirection.Input);
                param.Add("@Colour", colour, DbType.String, ParameterDirection.Input);
                param.Add("@Interval", interval, DbType.String, ParameterDirection.Input);
                param.Add("@IntervalId", intervalid, DbType.Int16, ParameterDirection.Input);
                param.Add("@Address", Address, DbType.String, ParameterDirection.Input);
                return _repo.GetResult("RedeemSmartQuizPrize", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
