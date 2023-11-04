using DIGITAL_GAMIFY.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DIGITAL_GAMIFY.BAL;
using DIGITAL_GAMIFY.Entities;
namespace DIGITAL_GAMIFY.Services
{
    public class SmartQuizController : ApiController
    {
        SmartQuizManager objsm = new SmartQuizManager();

        [Route("api/GetSmartQuizList")]
        [HttpGet]
        public List<SmartQuizEntity> GetSmartQuizList(Int32 bid,Int32 cid, [FromUri]paggingEntity pe)
        {
            try
            {
                return objsm.GetSmartQuizList(pe, bid,cid);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "SurveyController", "GetSmartQuizList - Services");
                return new List<SmartQuizEntity>();
            }
        }
        [Route("api/GetSmartQuizById")]
        [HttpGet]
        public object GetSmartQuizById(Int32 sid, Int32 cid)
        {
            
            try
            {
                SmartQuizQuestionAndAnswer qa = new SmartQuizQuestionAndAnswer();
                qa = objsm.GetSmartQuizById(sid, cid);
                object res = new object();
                //int isfinished = qa.SmartQuizDetails.GameStatus == 1 ? 0:qa.Question == null ? 1 :(qa.SmartQuizDetails.GameStatus == 3 && qa.Question.FirstOrDefault().QuestionNum==1)?1:0;
                int isfinished = qa.SmartQuizDetails.GameStatus == 1 ? 0 : qa.Question == null ? 1 : (qa.SmartQuizDetails.GameStatus == 3) ? 1 : qa.SmartQuizDetails.IsFinished;
                //int isfinished = qa.SmartQuizDetails.IsFinished;

                List<smartquizresultEntity> qre = new List<smartquizresultEntity>();
                string StatusMessage = qa.SmartQuizDetails.GameStatus == 1 ? "Game Started In: " + qa.SmartQuizDetails.StartedInText : qa.SmartQuizDetails.IsFinished == 1 ? "Game Finished" : "Game in Play Mode.";
                if (isfinished == 1)
                {

                    qre = objsm.GetCustomerSmartQuizResult(sid, cid);
                    var selfresult = qre.Where(x => x.IsSelf == 1).FirstOrDefault();
                    if (selfresult != null)
                    {
                        StatusMessage = "Thanks for your time You have completed  the game in " + selfresult.DurationString + ".\n";
                        if (qa.SmartQuizDetails.GameStatus == 3)
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
                    GameStatus = qa.SmartQuizDetails.GameStatus,
                    Duration=qa.SmartQuizDetails.DurationString,
                    IsFinished = isfinished,
                    StatusMessage = StatusMessage,
                    StartedIn = qa.SmartQuizDetails.StartedInText,
                    EndedIn = qa.SmartQuizDetails.EndedInText,
                    Questions = qa,
                    Results = qre
                };
                return res;

            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "SurveyController", "GetSmartQuizbyId - Services");
                return new SmartQuizQuestionAndAnswer();
            }

        }
        [Route("api/InsertCustomerSmartQuizAnswer")]
        [HttpGet]
        public object InsertCustomerSmartQuizAnswer(Int64 sqid, Int64 cid, Int64 qid, Int64 aid, int IsCorrect, int Duration)
        {
            
            SmartQuizCustomerQuestion scq = new SmartQuizCustomerQuestion();
            SmartQuizQuestionAndAnswer qa = new SmartQuizQuestionAndAnswer();
            SmartQuizQuestionAndAnswer qe = new SmartQuizQuestionAndAnswer();
            StatusResponse st = new StatusResponse();
            qa = objsm.GetSmartQuizById(sqid, cid);
            int PrizeId = 0;
            string RedeemCode = "";
          
            if(qid>0)
            {
                scq = objsm.InsertCustomerSmartQuizAnswer(sqid, cid, qid, aid, IsCorrect, Duration);
            }
            qe = objsm.GetSmartQuizById(sqid, cid);
            if (qe.SmartQuizDetails.IsFinished == 1)
            {
                if (qe.SmartQuizDetails.CorrectAnswerCount >= 5)
                {
                    string redeemcode = "";
                    Random rnd = new Random();
                    redeemcode = Settings.RandomNumber();
                    Int64 EntryNo = qe.SmartQuizDetails.TotalPlayed + 1;
                    int IntervalId = qe.SmartQuizDetails.IntervalId;
                    string Interval = qe.SmartQuizDetails.Interval;
                    Int16 OnceIn = qe.SmartQuizDetails.OnceIn;
                    if (EntryNo == qe.SmartQuizDetails.IntervalId)
                    {
                        PrizeId = Settings.getGamePrizeNumber(qe.SmartQuizDetails.FirstPrizeCount, qe.SmartQuizDetails.SecondPrizeCount, qe.SmartQuizDetails.FirstPrizeWinCount, qe.SmartQuizDetails.SecondPrizeWinCount);
                        string[] inrl = qe.SmartQuizDetails.Interval.Split('-');
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
                    st = objsm.CustomerSmartQuizRedeemPrize(sqid, cid, PrizeId, RedeemCode, "", "",Interval,IntervalId,"");
                    //qa.QuizDetails. = prize;
                    //cs.RedeemCode = redeemcode;
                }
                //else 
                //{
                //    Int64 EntryNo = qe.SmartQuizDetails.TotalPlayed + 1;
                //    int IntervalId = qe.SmartQuizDetails.IntervalId;
                //    string Interval = qe.SmartQuizDetails.Interval;
                //    Int16 OnceIn = qe.SmartQuizDetails.OnceIn;
                //    if(EntryNo==qe.SmartQuizDetails.IntervalId)
                //    {
                //        string[] inrl = qe.SmartQuizDetails.Interval.Split('-');
                //        int first = Convert.ToInt32(inrl[1]) + 1;
                //        //decimal val = Convert.ToDecimal(BrandGameDetails.TotalEntries / BrandGameDetails.OnceIn);
                //        int last = Convert.ToInt32(inrl[1]) + OnceIn;
                //        int rand = new Random().Next(first, last);
                //        IntervalId = rand;
                //        Interval = first.ToString() + "-" + last.ToString();
                //    }
                //    st = objsm.CustomerSmartQuizRedeemPrize(sqid, cid, PrizeId, RedeemCode, "", "", Interval, IntervalId);
                //}
            }

            int isfinished = qe.SmartQuizDetails.IsFinished;
                //qa.SmartQuizDetails.GameStatus == 1 ? 0 : scq == null ? 1 : 0;
            if (scq != null)
            {
                if (scq.QuestionNum == 0)
                {
                    isfinished = 1;
                }
            }
            List<smartquizresultEntity> qre = new List<smartquizresultEntity>();
            object res = new object();
            string StatusMessage = qa.SmartQuizDetails.GameStatus == 1 ? "Game Started In: " + qa.SmartQuizDetails.StartedInText : scq == null ? "GameFinished" : "Game in Play Mode.";

            if (isfinished == 1)
            {
                qre = objsm.GetCustomerSmartQuizResult(sqid, cid);
                var selfresult = qre.Where(x => x.IsSelf == 1).FirstOrDefault();
                if (selfresult != null)
                {
                    StatusMessage = "You are fished your game  " + selfresult.CorrectAnswerCount + "corret answers out of " + selfresult.AnsweredCount + " in " + selfresult.DurationString;
                }
                else
                {
                    StatusMessage = "You missed Game already finished, better luck next time..";
                }

            }
            var winprizedetails = qa.SmartQuizDetails.GamePrizes.Where(m => m.PrizeNumber == PrizeId).FirstOrDefault();
            res = new
            {
                GameStatus = qa.SmartQuizDetails.GameStatus,
                Duration = qa.SmartQuizDetails.StartedIn,
                IsFinished =isfinished,
                StatusMessage = StatusMessage,
                StartedIn = qa.SmartQuizDetails.StartedInText,
                EndedIn = qa.SmartQuizDetails.EndedIn,
                Questions = scq,
                Results = qre,
                RedeemCode = RedeemCode,
                PrizeDetails = new
                {
                    PrizeNumber = winprizedetails.PrizeNumber,
                    PrizeMessage = winprizedetails.PrizeText,
                    PrizePath = winprizedetails.PrizeImage
                }
            };
            return res;

        }

        [Route("api/GetSmartQuizStatusByCustomer")]
        [HttpGet]
        public object GetSmartQuizStatusByCustomer(Int64 sqid, Int64 cid)
        {
            try
            {
                SmartQuizQuestionAndAnswer qa = new SmartQuizQuestionAndAnswer();
                qa = objsm.GetSmartQuizStatusByCustomer(sqid, cid);
                object res = new object();
                int isfinished = qa.SmartQuizDetails.IsFinished;
                int PrizeId = qa.SmartQuizDetails.PrizeId;

                List<smartquizresultEntity> qre = new List<smartquizresultEntity>();
                string StatusMessage = qa.SmartQuizDetails.GameStatus == 1 ? "Game Started In: " + qa.SmartQuizDetails.StartedInText : qa.SmartQuizDetails.IsFinished == 1 ? "Game Finished" : "Game in Play Mode.";
                if (isfinished == 1)
                {
                    
                    qre = objsm.GetCustomerSmartQuizResult(sqid, cid);
                    var selfresult = qre.Where(x => x.IsSelf == 1).FirstOrDefault();
                    if (selfresult != null)
                    {
                        StatusMessage = "Thanks for your time You have completed  the game in " + selfresult.DurationString + ".\n";
                        if (qa.SmartQuizDetails.GameStatus == 3)
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
               
                var winprizedetails = qa.SmartQuizDetails.GamePrizes.Where(m => m.PrizeNumber == PrizeId).FirstOrDefault();
                
                res = new
                {
                    GameStatus = qa.SmartQuizDetails.GameStatus,
                    StartDuration = qa.SmartQuizDetails.StartedIn,
                    IsFinished = isfinished,
                    StatusMessage = StatusMessage,
                    StartedIn = qa.SmartQuizDetails.StartedInText,
                    EndedIn = qa.SmartQuizDetails.EndedIn,
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
                ExceptionUtility.LogException(ex, "SmartQuiz", "GetSmartQuizStatusByCustomer - Services");
                return new
                {
                    GameStatus = 0,
                    StartDuration = 0,
                    IsFinished = -1,
                    StatusMessage = "Invalid data",
                    StartedIn = -1,
                    EndedIn = -1,
                    Questions = new List<SmartQuizQuestions>(),
                    Results = new List<smartquizresultEntity>()
                };
            }
        }
        [Route("api/InsertSmartQuizCustomerAllAnswers")]
        [HttpPost]
        public object InsertSmartQuizCustomerAllAnswers([FromBody]CustomerQuizAnswers sbc)
        {
            smartquizresultEntity sbr = new smartquizresultEntity();
            if(sbc.CorrectAnsweredCount>=5)
            {
                string redeemcode = "";
                Random rnd = new Random();
                redeemcode = Settings.RandomNumber();
                int prize = rnd.Next(1, 3);
                sbc.PrizeId = prize;
                sbc.RedeemCode = redeemcode;
            }
            else
            {
                sbc.PrizeId = 0;
                sbc.RedeemCode = "";
            }
            sbr = objsm.InsertSmartQuizCustomerAllAnswers(sbc);
            SmartQuizQuestionAndAnswer qa = new SmartQuizQuestionAndAnswer();
            qa = objsm.GetSmartQuizStatusByCustomer(sbc.QuizId, sbc.CustomerId);
            List<smartquizresultEntity> qre = new List<smartquizresultEntity>();
            object res = new object();
            string StatusMessage = "";
            int PrizeId = qa.SmartQuizDetails.PrizeId;
            qre = objsm.GetCustomerSmartQuizResult(sbc.QuizId, sbc.CustomerId);

            var selfresult = qre.Where(x => x.IsSelf == 1).FirstOrDefault();
            if (selfresult != null)
            {
                StatusMessage = "Thanks for your time You have completed  the game in " + (selfresult.DurationString).FirstOrDefault() + ". \n";
                if (qa.SmartQuizDetails.GameStatus == 3)
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
            var winprizedetails = qa.SmartQuizDetails.GamePrizes.Where(m => m.PrizeNumber == sbc.PrizeId).FirstOrDefault();
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


        [Route("api/GetAdminSmartQuizList")]
        [HttpGet]
        public List<SmartQuizEntity> GetAdminSmartQuizList(Int32 adminid, Int32 bid, [FromUri]paggingEntity pe)
        {
            try
            {
                return objsm.GetAdminSmartQuizList(pe, adminid,bid);
            }
            catch(Exception ex)
            {
                ExceptionUtility.LogException(ex, "SmartQuizController", "GetAdminSmartQuizList-Services");
                return new List<SmartQuizEntity>();
            }
        }
        [Route("api/getAdminSmartQuizById")]
        [HttpGet]
        public SmartQuizQuestionAndAnswer getAdminSmartQuizById(Int32 sid)
        {
            try
            {
                return objsm.getAdminSmartQuizById(sid);
            }
            catch(Exception ex)
            {
                ExceptionUtility.LogException(ex, "SmartQuizController", "getAdminSmartQuizById-Services");
                return new SmartQuizQuestionAndAnswer();
            }
        }
        [Route("api/GetSmartQuizResult")]
        [HttpGet]
        public List<SmartquizResultEntity> GetSmartQuizResult(Int32 smquizid,[FromUri] paggingEntity pe)
        {
            try
            {
                return objsm.GetSmartQuizResult(pe,smquizid);
            }
            catch(Exception ex)
            {
                ExceptionUtility.LogException(ex, "SmartQuizController", "GetSmartQuizResult-Services");
                return new List<SmartquizResultEntity>();
            }
        }
        [Route("api/GetddlsmartQuizes")]
        [HttpGet]
        public List<Quizddl> GetddlsmartQuizes(Int64 bid)
        {
            try
            {
                return objsm.GetddlsmartQuizes(bid);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "SmartQuizController", "GetddlsmartQuizes-Services");
                return new List<Quizddl>();
            }
        }
        [Route("api/DeleteSmartQuiz")]
        [HttpPost]
        public SmartQuizEntity DeleteSmartQuiz(Int32 sid)
        {
            try
            {
                return objsm.DeleteSmartQuiz(sid);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "SmartQuizController", "DeleteSmartQuiz - Services");
                return new SmartQuizEntity();
            }
        }
        [Route("api/GetCustomerSmartQuizPrizes")]
        [HttpGet]
        public List<SmarQuizPrizesEntity> GetCustomerSmartQuizPrizes(Int64 cid, Int64 bid)
        {
            try
            {
                return objsm.GetCustomerSmartQuizPrizes(cid, bid);
            }
            catch(Exception ex)
            {
                ExceptionUtility.LogException(ex, "SmartQuizController", "GetCustomerSmartQuizPrizes-Services");
                return new List<SmarQuizPrizesEntity>();
            }
        }
        [Route("api/AdminGetSmartQuizPrizes")]
        [HttpGet]
        public List<SmarQuizPrizesEntity> AdminGetSmartQuizPrizes(Int32 bid, int status, int prize, Int64 gid, [FromUri]paggingEntity p)
        {
            try
            {
                return objsm.AdminGetSmartQuizPrizes(p,bid,status,gid,prize);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "SmartQuizController", "AdminGetSmartQuizPrizes-Services");
                return new List<SmarQuizPrizesEntity>();
            }
        }
        [Route("api/CustomerSmartQuizRedeemPrize")]
        [HttpGet]
        public StatusResponse CustomerSmartQuizRedeemPrize(Int64 sid, Int64 cid, int PrizeId, string RedeemCode, string size, string colour, string interval, int intervalid,string Address)
        {
            return objsm.CustomerSmartQuizRedeemPrize(sid, cid, PrizeId, RedeemCode, size, colour,interval,intervalid,Address);
        } 
    }
}