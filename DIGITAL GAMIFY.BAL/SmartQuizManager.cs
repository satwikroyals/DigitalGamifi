using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIGITAL_GAMIFY.Entities;
using DIGITAL_GAMIFY.DAL;

namespace DIGITAL_GAMIFY.BAL
{
    public  class SmartQuizManager
    {
        private SmartQuizData objsd = new SmartQuizData();
        public List<SmartQuizEntity> GetSmartQuizList(paggingEntity pe, Int32 bid,Int32 cid)
        {
            return objsd.GetSmartQuizList(pe, bid,cid);
        }
        public SmartQuizQuestionAndAnswer GetSmartQuizById(Int64 sid, Int64 cid)
        {
            return objsd.getSmartQuizById(sid, cid);
        }
        public SmartQuizQuestionAndAnswer GetSmartQuizStatusByCustomer(Int64 sid, Int64 cid)
        {
            return objsd.GetSmartQuizStatusByCustomer(sid, cid);
        }
        public SmartQuizCustomerQuestion InsertCustomerSmartQuizAnswer(Int64 sqid, Int64 cid, Int64 qid, Int64 aid, int IsCorrect, int Duration)
        {
            SmartQuizCustomerQuestion scq = new SmartQuizCustomerQuestion();
            scq = objsd.InsertCustomerSmartQuizAnswer(sqid, cid, qid, aid, IsCorrect, Duration);
            if (scq != null)
            {
                scq.answers = objsd.GetSmartQuizAnswersByQuestionId(scq.QuestionNum,sqid);
            }
            return scq;
        }
        public smartquizresultEntity InsertSmartQuizCustomerAllAnswers(CustomerQuizAnswers cs)
        {
            return objsd.InsertSmartQuizCustomerAllAnswers(cs);
        }
        public List<smartquizresultEntity> GetCustomerSmartQuizResult(Int64 quizid, Int64 cid)
        {
            return objsd.GetCustomerSmartQuizResult(quizid, cid);
        }
        public StatusResponse AddSmartQuizGame(SmartQuizEntity sqEntity)
        {
            return objsd.AddSmartQuizGame(sqEntity);
        }
        public StatusResponse AddSmartQuizQuestions(SmartQuizCustomerQuestion sqEntity)
        {
            return objsd.AddSmartQuizQuestions(sqEntity);
        }
        public StatusResponse AddSmartQuizQuestionAnswers(SmartQuizAnswers sqaEntity)
        {
            return objsd.AddSmartQuizQuestionAnswers(sqaEntity);
        }
        public List<SmartQuizEntity> GetAdminSmartQuizList(paggingEntity pe, Int32 adminid, Int32 bid)
        {
            return objsd.GetAdminSmartQuizList(pe, adminid,bid);
        }
        public SmartQuizQuestionAndAnswer getAdminSmartQuizById(Int32 sid)
        {
            return objsd.getAdminSmartQuizById(sid);
        }
        public List<SmartquizResultEntity> GetSmartQuizResult(paggingEntity pe,Int32 smquizid)
        {
            return objsd.GetSmartQuizResult(pe,smquizid);
        }
        public List<Quizddl> GetddlsmartQuizes(Int64 bid)
        {
            return objsd.GetddlsmartQuizes(bid);
        }
        public SmartQuizEntity DeleteSmartQuiz(Int32 sid)
        {
            return objsd.DeleteSmartQuiz(sid);
        }
        public List<SmarQuizPrizesEntity> GetCustomerSmartQuizPrizes(Int64 cid, Int64 bid)
        {
            return objsd.GetCustomerSmartQuizPrizes(cid, bid);
        }
        public List<SmarQuizPrizesEntity> AdminGetSmartQuizPrizes(paggingEntity p, Int32 bid, int status, Int64 gid, int prize)
        {
            return objsd.AdminGetSmartQuizPrizes(p, bid, status,gid,prize);
        }
        public StatusResponse CustomerSmartQuizRedeemPrize(Int64 sid, Int64 cid, int PrizeId, string RedeemCode, string size, string colour, string interval, int intervalid,string Address)
        {
            return objsd.CustomerSmartQuizRedeemPrize(sid, cid, PrizeId, RedeemCode, size, colour,interval,intervalid,Address);
        }
    }
}
