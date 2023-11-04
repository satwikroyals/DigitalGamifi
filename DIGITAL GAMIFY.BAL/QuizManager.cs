using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIGITAL_GAMIFY.Entities;
using DIGITAL_GAMIFY.DAL;

namespace DIGITAL_GAMIFY.BAL
{
    public class QuizManager
    {
        private QuizData objsd = new QuizData();
        public List<QuizEntity> GetQuizList(paggingEntity pe, Int32 adminid,Int32 bid,Int32 cid)
        {
            return objsd.GetQuizList(pe, adminid,bid,cid);
        }
        public QuizQuestionAndAnswer GetQuizById(Int64 qzid, Int64 cid)
        {
            return objsd.getQuizById(qzid, cid);
        }
        public QuizCustomerQuestion InsertCustomerQuizAnswer(Int64 quizid, Int64 cid, Int64 qid, Int64 aid, int IsCorrect, int Duration)
        {
            QuizCustomerQuestion scq = new QuizCustomerQuestion();
            scq = objsd.InsertCustomerQuizAnswer(quizid, cid, qid, aid, IsCorrect, Duration);
            if (scq != null)
            {
                scq.answers = objsd.GetQuizAnswersByQuestionId(scq.QuestionNum, quizid);
            }
            return scq;
        }
        public StatusResponse AddQuizGame(QuizEntity sqEntity)
        {
            return objsd.AddQuizGame(sqEntity);
        }
        public StatusResponse AddSmartQuizQuestions(QuizCustomerQuestion sqEntity)
        {
            return objsd.AddQuizQuestions(sqEntity);
        }
        public StatusResponse AddQuizQuestionAnswers(QuizAnswers sqaEntity)
        {
            return objsd.AddQuizQuestionAnswers(sqaEntity);
        }
        public List<QuizEntity> GetAdminQuizList(paggingEntity pe, Int32 adminid, Int32 bid)
        {
            return objsd.GetAdminQuizList(pe, adminid, bid);
        }
        public QuizQuestionAndAnswer getAdminQuizById(Int32 sid)
        {
            return objsd.getAdminQuizById(sid);
        }
        public List<quizResultEntity> GetQuizResult(paggingEntity pe, Int32 quizid)
        {
            return objsd.GetQuizResult(pe, quizid);
        }
        public List<quizResultEntity> GetCustomerQuizResult(Int64 quizid, Int64 cid)
        {
            return objsd.GetCustomerQuizResult(quizid,cid);
        }
        public List<Quizddl> GetddlQuizes(Int32 bid)
        {
            return objsd.GetddlQuizes(bid);
        }
        public QuizEntity DeleteQuiz(Int32 qid)
        {
            return objsd.DeleteQuiz(qid);
        }
        public QuizQuestionAndAnswer GetQuizStatusByCustomer(Int64 qzid, Int64 cid)
        {
            return objsd.GetQuizStatusByCustomer(qzid, cid);
        }

        public quizResultEntity InsertQuizCustomerAllAnswers(CustomerQuizAnswers cs)
        {
            return objsd.InsertQuizCustomerAllAnswers(cs);
        }
        public List<QuizPrizesEntity> GetCustomerQuizPrizes(Int64 cid, Int64 bid)
        {
            return objsd.GetCustomerQuizPrizes(cid, bid);
        }
        public List<QuizPrizesEntity> AdminGetQuizPrizes(paggingEntity p, Int32 bid, int status, Int64 gid, int prize)
        {
            return objsd.AdminGetQuizPrizes(p, bid, status,gid,prize);
        }
        public StatusResponse CustomerQuizRedeemPrize(Int64 sid, Int64 cid, int PrizeId, string RedeemCode, string size, string colour, string interval, int intervalid,string Address)
        {
            return objsd.CustomerQuizRedeemPrize(sid, cid, PrizeId, RedeemCode, size, colour,interval,intervalid,Address);
        }
    }
}
