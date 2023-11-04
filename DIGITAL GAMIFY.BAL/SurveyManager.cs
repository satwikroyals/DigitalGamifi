using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIGITAL_GAMIFY.Entities;
using DIGITAL_GAMIFY.DAL;

namespace DIGITAL_GAMIFY.BAL
{
    public class SurveyManager
    {
        public Surveydata objsd = new Surveydata();
        public List<SurveyEntity> GetSurveyList(paggingEntity pe,Int32 bid,Int32 cid)
        {
            return objsd.GetSurveyList(pe, bid,cid);
        }
        public List<SurveyEntity> GetAdminSurveyList(paggingEntity pe, Int32 adminid, Int32 bid)
        {
            return objsd.GetAdminSurveyList(pe, adminid, bid);
        }
        public StatusResponse AddSurvey(SurveyEntity se)
        {
            return objsd.AddSurvey(se);
        }
        public StatusResponse AddUpdateSurveyQandA(SurveyEntity sEntity)
        {
            return objsd.AddUpdateSurveyQandA(sEntity);
        }
        public SurveyQuestionandAnswer GetAdminSurveybyId(Int64 sid)
        {
            return objsd.GetAdminSurveybyId(sid);
        }
        public SurveyQuestionandAnswer GetSurveybyId(Int32 sid,Int32 cid)
        {
            return objsd.GetSurveybyId(sid,cid);
        }
        public SurveyEntity DeleteQuestions(Int32 qid)
        {
            return objsd.DeleteQuestions(qid);
        }
        public SurveyEntity DeleteAnswers(Int32 aid)
        {
            return objsd.DeleteAnswers(aid);
        }
        public SurveyEntity DeleteSurvey(Int32 sid)
        {
            return objsd.DeleteSurvey(sid);
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
            SurveyCustomerQuestion scq = new SurveyCustomerQuestion();
            SurveyQuestionandAnswer se = new SurveyQuestionandAnswer();
            scq = objsd.InsertCustomerSurveyAnswer(sid, cid, qid, aid, answertext, Duration);
            if (scq != null)
            {
                scq.answers = objsd.GetSurveyAnswersByQuetionId(scq.SurveyquestionId);
            }
            return scq;
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
            return objsd.CheckSurveyExist(sid, sname, smscode);
        }
        public List<Quizddl> GetddlSurveys(Int64 bid)
        {
            return objsd.GetddlSurveys(bid);
        }
        public List<SurveyResult> GetCustomerSurveyPrizes(Int64 cid, Int64 bid)
        {
            return objsd.GetCustomerSurveyPrizes(cid, bid);
        }
        public List<SurveyResult> AdminGetSurveyPrizes(paggingEntity p, Int32 bid, int status, Int64 gid, int prize)
        {
            return objsd.AdminGetSurveyPrizes(p, bid, status,gid,prize);
        }
        public SurveyEntity RedeemPrize(Int64 sid, Int64 cid, string size, string colour, int PrizeId, string RedeemCode, string interval, int intervalid,string Address)
        {
            return objsd.RedeemPrize(sid, cid, size, colour,PrizeId,RedeemCode,interval,intervalid,Address);
        }
    }
}
