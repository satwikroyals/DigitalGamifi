using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DIGITAL_GAMIFY.Entities;
using DIGITAL_GAMIFY.BAL;
using DIGITAL_GAMIFY.Code;
using System.Web;

namespace DIGITAL_GAMIFY.Services
{
    public class SurveyController : ApiController
    {
        SurveyManager objsm = new SurveyManager();
        [Route("api/GetSurveyList")]
        [HttpGet]
        public List<SurveyEntity> GetSurveyList(Int32 bid,Int32 cid, [FromUri]paggingEntity pe)
        {
            try
            {
                return objsm.GetSurveyList(pe,bid,cid);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "SurveyController", "GetSurveyList - Services");
                return new List<SurveyEntity>();
            }
        }
        [Route("api/GetAdminSurveyList")]
        [HttpGet]
        public List<SurveyEntity> GetAdminSurveyList(Int32 adminid,Int32 bid, [FromUri]paggingEntity pe)
        {
            try
            {
                return objsm.GetAdminSurveyList(pe, adminid,bid);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "SurveyController", "GetAdminSurveyList - Services");
                return new List<SurveyEntity>();
            }
        }
        [Route("api/GetAdminSurveybyId")]
        [HttpGet]
        public SurveyQuestionandAnswer GetAdminSurveybyId(Int64 sid)
        {
            try
            {
                return objsm.GetAdminSurveybyId(sid);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "SurveyController", "GetAdminSurveybyId - Services");
                return new SurveyQuestionandAnswer();
            }
        }
        [Route("api/GetSurveybyId")]
        [HttpGet]
        public SurveyQuestionandAnswer GetSurveybyId(Int32 sid,Int32 cid)
        {
            try
            {
                SurveyQuestionandAnswer qa = new SurveyQuestionandAnswer();
                qa = objsm.GetSurveybyId(sid, cid);
                return qa;
            }
            catch(Exception ex)
            {
                ExceptionUtility.LogException(ex, "SurveyController", "GetSurveybyId - Services");
                return new SurveyQuestionandAnswer();
            }
        }
        [Route("api/DeleteQuestions")]
        [HttpPost]
        public SurveyEntity DeleteQuestions(Int32 qid)
        {
            try
            {
                return objsm.DeleteQuestions(qid);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "SurveyController", "DeleteQuestions - Services");
                return new SurveyEntity();
            }
        }
        [Route("api/DeleteAnswers")]
        [HttpPost]
        public SurveyEntity DeleteAnswers(Int32 aid)
        {
            try
            {
                return objsm.DeleteAnswers(aid);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "SurveyController", "DeleteAnswers - Services");
                return new SurveyEntity();
            }
        }
        [Route("api/DeleteSurvey")]
        [HttpPost]
        public SurveyEntity DeleteSurvey(Int32 sid)
        {
            try
            {
                return objsm.DeleteSurvey(sid);
            }
            catch(Exception ex)
            {
                ExceptionUtility.LogException(ex, "SurveyController", "DeleteSurvey - Services");
                return new SurveyEntity();
            }
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
        [Route("api/InsertCustomerSurveyAnswer")]
        [HttpGet]
        public object InsertCustomerSurveyAnswer(Int32 sid, Int32 cid, Int64 qid, Int64 aid, string answertext, Int32 Duration)
        {
            SurveyCustomerQuestion scq = new SurveyCustomerQuestion();
            SurveyQuestionandAnswer se = new SurveyQuestionandAnswer();
            if (qid > 0)
            {
                scq = objsm.InsertCustomerSurveyAnswer(sid, cid, qid, aid, answertext, Duration);
            }
            int PrizeId = 0;
            string redeemcode = "";
            se = objsm.GetSurveybyId(sid,cid);
            if (scq == null)
            {
                Random rnd = new Random();
                redeemcode = Settings.RandomNumber();
                Int64 EntryNo = se.SurveyDetails.TotalPlayed + 1;
                int IntervalId = se.SurveyDetails.IntervalId;
                string Interval = se.SurveyDetails.Interval;
                Int16 OnceIn = se.SurveyDetails.OnceIn;
                if (EntryNo == se.SurveyDetails.IntervalId)
                {
                    PrizeId = Settings.getGamePrizeNumber(se.SurveyDetails.FirstPrizeCount, se.SurveyDetails.SecondPrizeCount, se.SurveyDetails.FirstPrizeWinCount, se.SurveyDetails.SecondPrizeWinCount);
                    string[] inrl = se.SurveyDetails.Interval.Split('-');
                    int first = Convert.ToInt32(inrl[1]) + 1;
                    //decimal val = Convert.ToDecimal(BrandGameDetails.TotalEntries / BrandGameDetails.OnceIn);
                    int last = Convert.ToInt32(inrl[1]) + OnceIn;
                    int rand = new Random().Next(first, last);
                    IntervalId = rand;
                    Interval = first.ToString() + "-" + last.ToString();
                }
                else
                {
                    PrizeId = 3;
                }
                SurveyEntity st = objsm.RedeemPrize(sid, cid, "", "", PrizeId, redeemcode, Interval, IntervalId,"");
            }
            int isfinished = se.SurveyDetails.IsFinished;
            object res = new object();
            var winprizedetails = se.SurveyDetails.GamePrizes.Where(m => m.PrizeNumber == PrizeId).FirstOrDefault();
            res = new
            {
                IsFinished = isfinished,
                RedeemCode=redeemcode,
                SurveyQuestion = scq,
                PrizeDetails = new
                {
                    PrizeNumber = winprizedetails.PrizeNumber,
                    PrizeMessage = winprizedetails.PrizeText,
                    PrizePath = winprizedetails.PrizeImage
                }
            };
            return res;
        }
        /// <summary>
        /// Check Survey Exist
        /// </summary>
        /// <param name="sid">Surveyid</param>
        /// <param name="sname">Surveyname</param>
        /// <param name="smscode">smscode</param>
        /// <returns></returns>
        [Route("api/CheckSurveyExist")]
        [HttpGet]
        public StatusResponse CheckSurveyExist(Int32 sid,string sname,string smscode)
        {
            try
            {
                return objsm.CheckSurveyExist(sid, sname, smscode);
            }
            catch(Exception ex)
            {
                ExceptionUtility.LogException(ex, "SurveyController", "CheckSurveyExist-Services");
                return new StatusResponse();
            }
        }
        [Route("api/GetCustomerSurveyPrizes")]
        [HttpGet]
        public List<SurveyResult> GetCustomerSurveyPrizes(Int64 cid, Int64 bid)
        {
            try
            {
                return objsm.GetCustomerSurveyPrizes(cid,bid);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "SurveyController", "GetCustomerSurveyPrizes-Services");
                return new List<SurveyResult>();
            }
        }
        [Route("api/AdminGetSurveyPrizes")]
        [HttpGet]
        public List<SurveyResult> AdminGetSurveyPrizes(Int32 bid, int status, Int64 gid, int prize, [FromUri]paggingEntity p)
        {
            try
            {
                return objsm.AdminGetSurveyPrizes(p, bid,status,gid,prize); 
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "SurveyController", "AdminGetSurveyPrizes-Services");
                return new List<SurveyResult>();
            }
        }
        [Route("api/CustomerPrizeRedeemPrize")]
        [HttpGet]
        public SurveyEntity CustomerPrizeRedeemPrize(Int64 sid, Int64 cid, string size, string colour, int PrizeId, string RedeemCode, string interval, int intervalid,string Address)
        {
            try
            {
                return objsm.RedeemPrize(sid, cid, size, colour,PrizeId,RedeemCode,interval,intervalid,Address);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "SurveyController", "CustomerPrizeRedeemPrize-Services");
                return new SurveyEntity();
            }
            
        }
        [Route("api/GetddlSurveys")]
        [HttpGet]
        public List<Quizddl> GetddlSurveys(Int64 bid)
        {
            try
            {
                return objsm.GetddlSurveys(bid);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "SurveyController", "GetddlSurveys-Services");
                return new List<Quizddl>();
            }
        }
    }
}
