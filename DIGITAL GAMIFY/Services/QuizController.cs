using DIGITAL_GAMIFY.Code;
using DIGITAL_GAMIFY.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DIGITAL_GAMIFY.BAL;

namespace DIGITAL_GAMIFY.Services
{
    public class QuizController : ApiController
    {
        QuizManager objsm = new QuizManager();

        [Route("api/GetQuizList")]
        [HttpGet]
        public List<QuizEntity> GetQuizList(Int32 adminid, Int32 bid, Int32 cid, [FromUri]paggingEntity pe)
        {
            try
            {
                return objsm.GetQuizList(pe, adminid,bid,cid);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "QuizController", "GetQuizList - Services");
                return new List<QuizEntity>();
            }
        }
        [Route("api/GetQuizById")]
        [HttpGet]
        public object GetQuizById(Int32 sid, Int32 cid)
        {
            try
            {
                QuizQuestionAndAnswer qa = new QuizQuestionAndAnswer();
                qa=objsm.GetQuizById(sid, cid);
                object res = new object();
               /// int isfinished = qa.QuizDetails.IsFinished;
                int isfinished = qa.QuizDetails.GameStatus == 1 ? 0 : qa.Question == null ? 1 : (qa.QuizDetails.GameStatus == 3) ? 1 : qa.QuizDetails.IsFinished;
                //if (qa.Question != null)
                //{
                //    if (qa.Question.FirstOrDefault().QuestionNum == 0)
                //    {
                //        isfinished = 1;
                //    }
                //}
                List<quizResultEntity> qre = new List<quizResultEntity>();
                string StatusMessage = qa.QuizDetails.GameStatus == 1 ? "Game Started In: " + qa.QuizDetails.StartedInText : qa.QuizDetails.IsFinished == 1 ? "Game Finished" : "Game in Play Mode.";
                if (isfinished == 1)
                {
                    qre = objsm.GetCustomerQuizResult(sid, cid);
                    var selfresult = qre.Where(x => x.IsSelf == 1).FirstOrDefault();
                    if (selfresult != null)
                    {
                        StatusMessage = "Thanks for your time You have completed  the game in " + selfresult.DurationString + ".\n";
                        if (qa.QuizDetails.GameStatus == 3)
                        {
                            StatusMessage += " Final Results Are: \n";
                        }
                        else
                        {
                            StatusMessage = "Thanks for your time You have completed  the game in  " + selfresult.CorrectAnswerCount + "corret answers out of " + selfresult.AnsweredCount + " in " + selfresult.DurationString;
                        }
                    }
                    else
                    {
                        StatusMessage = "You missed Game already finished, better luck next time...";
                    }

                }
                res = new
                {
                    GameStatus = qa.QuizDetails.GameStatus,
                    Duration = qa.QuizDetails.StartedIn,
                    IsFinished = isfinished,
                    StatusMessage=StatusMessage,
                    StartedIn = qa.QuizDetails.StartedInText,
                    EndedIn = qa.QuizDetails.EndedIn,
                    Questions = qa,
                    Results=qre
                };
                return res;
                
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "QuizController", "GetQuizbyId - Services");
                return new QuizQuestionAndAnswer();
            }
        }
        [Route("api/InsertCustomerQuizAnswer")]
        [HttpGet]
        public object InsertCustomerQuizAnswer(Int64 quid, Int64 cid, Int64 qid, Int64 aid, int IsCorrect, int Duration)
        {
            QuizCustomerQuestion scq = new QuizCustomerQuestion();
            QuizQuestionAndAnswer qa = new QuizQuestionAndAnswer();
            QuizQuestionAndAnswer qe = new QuizQuestionAndAnswer();
            StatusResponse st = new StatusResponse();
            qa = objsm.GetQuizById(quid, cid);
            int PrizeId = 0;
            string RedeemCode = "";
            if (qid > 0)
            {
                scq = objsm.InsertCustomerQuizAnswer(quid, cid, qid, aid, IsCorrect, Duration);
            }
            qe = objsm.GetQuizById(quid, cid);
            if (qe.QuizDetails.IsFinished==1)
            {
                if (qe.QuizDetails.CorrectAnswerCount >= 5)
                {
                    string redeemcode = "";
                    Random rnd = new Random();
                    redeemcode = Settings.RandomNumber();
                    Int64 EntryNo = qe.QuizDetails.TotalPlayed + 1;
                    int IntervalId = qe.QuizDetails.IntervalId;
                    string Interval = qe.QuizDetails.Interval;
                    Int16 OnceIn = qe.QuizDetails.OnceIn;
                    if (EntryNo == qe.QuizDetails.IntervalId)
                    {
                        PrizeId = Settings.getGamePrizeNumber(qe.QuizDetails.FirstPrizeCount, qe.QuizDetails.SecondPrizeCount, qe.QuizDetails.FirstPrizeWinCount, qe.QuizDetails.SecondPrizeWinCount);
                        string[] inrl = qe.QuizDetails.Interval.Split('-');
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
                    RedeemCode = redeemcode;
                    st=objsm.CustomerQuizRedeemPrize(quid,cid,PrizeId,RedeemCode,"","",Interval,IntervalId,"");
                    //qa.QuizDetails. = prize;
                    //cs.RedeemCode = redeemcode;
                }
            }
            int isfinished = qe.QuizDetails.IsFinished;
                //qa.QuizDetails.GameStatus == 1 ? 0 : scq == null ? 1 : 0;
            if (scq != null)
            {
                if (scq.QuestionNum == 0)
                {
                    isfinished = 1;
                }
            }
            List<quizResultEntity> qre = new List<quizResultEntity>();
            object res = new object();
            string StatusMessage = qa.QuizDetails.GameStatus == 1 ? "Game Started In: " + qa.QuizDetails.StartedInText : scq == null ? "GameFinished" : "Game in Play Mode.";
           
            if (isfinished == 1)
            {
                qre = objsm.GetCustomerQuizResult(quid, cid);
                var selfresult = qre.Where(x => x.IsSelf == 1).FirstOrDefault();
                if (selfresult != null)
                {
                    StatusMessage = "Thanks for your time You have completed  the game in  " + selfresult.CorrectAnswerCount + "corret answers out of " + selfresult.AnsweredCount + " in " + selfresult.DurationString;
                }
                else
                {
                    StatusMessage = "You missed Game already finished, better luck next time..";
                }

            }
            var winprizedetails = qa.QuizDetails.GamePrizes.Where(m => m.PrizeNumber == PrizeId).FirstOrDefault();
            res = new
            {
                GameStatus = qa.QuizDetails.GameStatus,
                Duration = qa.QuizDetails.StartedIn,
                IsFinished = isfinished,
                StatusMessage = StatusMessage,
                StartedIn = qa.QuizDetails.StartedInText,
                EndedIn = qa.QuizDetails.EndedIn,
                Questions = scq,
                Results = qre,
                RedeemCode=RedeemCode,
                PrizeDetails = new
                {
                    PrizeNumber = winprizedetails.PrizeNumber,
                    PrizeMessage = winprizedetails.PrizeText,
                    PrizePath = winprizedetails.PrizeImage
                }
            };
            return res;
        }
        [Route("api/GetAdminQuizList")]
        [HttpGet]
        public List<QuizEntity> GetAdminQuizList(Int32 adminid, Int32 bid, [FromUri]paggingEntity pe)
        {
            try
            {
                return objsm.GetAdminQuizList(pe, adminid, bid);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "QuizController", "GetAdminQuizList-Services");
                return new List<QuizEntity>();
            }
        }
        [Route("api/getAdminQuizById")]
        [HttpGet]
        public QuizQuestionAndAnswer getAdminQuizById(Int32 sid)
        {
            try
            {
                return objsm.getAdminQuizById(sid);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "QuizController", "getAdminQuizById-Services");
                return new QuizQuestionAndAnswer();
            }
        }
        [Route("api/GetQuizResult")]
        [HttpGet]
        public List<quizResultEntity> GetQuizResult(Int32 quizid, [FromUri] paggingEntity pe)
        {
            try
            {
                return objsm.GetQuizResult(pe, quizid);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "QuizController", "GetQuizResult-Services");
                return new List<quizResultEntity>();
            }
        }
        [Route("api/GetddlQuizes")]
        [HttpGet]
        public List<Quizddl> GetddlQuizes(Int32 bid)
        {
            try
            {
                return objsm.GetddlQuizes(bid);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "QuizController", "GetddlQuizes-Services");
                return new List<Quizddl>();
            }
        }
        [Route("api/DeleteQuiz")]
        [HttpPost]
        public QuizEntity DeleteQuiz(Int32 qid)
        {
            try
            {
                return objsm.DeleteQuiz(qid);
            }
            catch(Exception ex)
            {
                ExceptionUtility.LogException(ex, "QuizController", "DeleteQuiz-Services");
                return new QuizEntity();
            }
        }


        [Route("api/GetQuizStatusByCustomer")]
        [HttpGet]
        public object GetQuizStatusByCustomer(Int64 qzid, Int64 cid)
        {
            try
            {
                QuizQuestionAndAnswer qa = new QuizQuestionAndAnswer();
                qa = objsm.GetQuizStatusByCustomer(qzid, cid);
                object res = new object();
                int isfinished = qa.QuizDetails.IsFinished;
                int PrizeId = qa.QuizDetails.PrizeId;

                List<quizResultEntity> qre = new List<quizResultEntity>();
                string StatusMessage = qa.QuizDetails.GameStatus == 1 ? "Game Started In: " + qa.QuizDetails.StartedInText : qa.QuizDetails.IsFinished == 1 ? "Game Finished" : "Game in Play Mode.";
                if (isfinished == 1)
                {
                    qre = objsm.GetCustomerQuizResult(qzid, cid);
                    var selfresult = qre.Where(x => x.IsSelf == 1).FirstOrDefault();
                    if (selfresult != null)
                    {
                        StatusMessage = "Thanks for your time You have completed  the game in " + selfresult.DurationString + ".\n";
                        if (qa.QuizDetails.GameStatus == 3)
                        {
                            StatusMessage += " Final Results Are: \n";
                        }
                        else
                        {
                            StatusMessage += " Game In Play Mode Your Rank Will Change : \n";
                        }
                    }
                    else
                    {
                        StatusMessage = "You missed Game already finished, better luck next time...";
                    }

                }
                var winprizedetails = qa.QuizDetails.GamePrizes.Where(m => m.PrizeNumber == PrizeId).FirstOrDefault();
                res = new
                {
                    GameStatus = qa.QuizDetails.GameStatus,
                    StartDuration = qa.QuizDetails.StartedIn,
                    IsFinished = isfinished,
                    StatusMessage = StatusMessage,
                    StartedIn = qa.QuizDetails.StartedInText,
                    EndedIn = qa.QuizDetails.EndedIn,
                    Questions = qa.Question,
                    Results = qre,
                    PrizeDetails = new
                    {
                        PrizeNumber = winprizedetails.PrizeNumber,
                        PrizeMessage = winprizedetails.PrizeText,
                        PrizePath = winprizedetails.PrizeImage
                    }
                };
                return res;

            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "QuizController", "GetSmartQuizStatusByCustomer - Services");
                return new
                {
                    GameStatus = 0,
                    StartDuration = 0,
                    IsFinished = -1,
                    StatusMessage = "Invalid data",
                    StartedIn = -1,
                    EndedIn = -1,
                    Questions = new List<QuizQuestions>(),
                    Results = new List<quizResultEntity>()
                };
            }
        }
        [Route("api/InsertQuizCustomerAllAnswers")]
        [HttpPost]
        public object InsertQuizCustomerAllAnswers(CustomerQuizAnswers cs)
        {
            quizResultEntity sbr = new quizResultEntity();
            if (cs.CorrectAnsweredCount >= 5)
            {
                string redeemcode = "";
                Random rnd = new Random();
                redeemcode = Settings.RandomNumber();
                int prize = rnd.Next(1, 3);
                cs.PrizeId = prize;
                cs.RedeemCode = redeemcode;
            }
            else
            {
                cs.PrizeId = 0;
                cs.RedeemCode = "";
            }
            sbr = objsm.InsertQuizCustomerAllAnswers(cs);
            QuizQuestionAndAnswer qa = new QuizQuestionAndAnswer();
            qa = objsm.GetQuizStatusByCustomer(cs.QuizId, cs.CustomerId);
            List<quizResultEntity> qre = new List<quizResultEntity>();
            object res = new object();
            string StatusMessage = "";

            qre = objsm.GetCustomerQuizResult(cs.QuizId, cs.CustomerId);

            var selfresult = qre.Where(x => x.IsSelf == 1).FirstOrDefault();
            if (selfresult != null)
            {
                StatusMessage = "Thanks for your time You have completed  the game in " + (selfresult.DurationString).FirstOrDefault() + ". \n";
                if (qa.QuizDetails.GameStatus == 3)
                {
                    StatusMessage += " Final Results Are: \n";
                }
                else
                {
                    StatusMessage += " Game In Play Mode Your Rank Will Change : \n";
                }
            }
            else
            {
                StatusMessage = "You missed Game already finished, better luck next time...";
            }
            var winprizedetails = qa.QuizDetails.GamePrizes.Where(m => m.PrizeNumber == cs.PrizeId).FirstOrDefault();
            res = new
            {

                StatusMessage = StatusMessage,
                Results = qre,
                PrizeDetails = new
                {
                    PrizeNumber = winprizedetails.PrizeNumber,
                    PrizeMessage = winprizedetails.PrizeText,
                    PrizePath = winprizedetails.PrizeImage
                }
            };
            return res;
        }
        [Route("api/GetCustomerQuizPrizes")]
        [HttpGet]
        public List<QuizPrizesEntity> GetCustomerQuizPrizes(Int64 cid, Int64 bid)
        {
            return objsm.GetCustomerQuizPrizes(cid, bid);
        }
        [Route("api/AdminGetQuizPrizes")]
        [HttpGet]
        public List<QuizPrizesEntity> AdminGetQuizPrizes(Int32 bid, int status, int prize, Int64 gid, [FromUri]paggingEntity p)
        {
            try
            {
                return objsm.AdminGetQuizPrizes(p,bid,status,gid,prize);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "QuizController", "AdminGetQuizPrizes-Services");
                return new List<QuizPrizesEntity>();
            }
        }
        [Route("api/CustomerQuizRedeemPrize")]
        [HttpGet]
        public StatusResponse CustomerQuizRedeemPrize(Int64 sid, Int64 cid, int PrizeId, string RedeemCode, string size, string colour, string interval, int intervalid, string Address)
        {
            return objsm.CustomerQuizRedeemPrize(sid, cid, PrizeId, RedeemCode, size, colour,interval,intervalid,Address);
        }
    }
}
