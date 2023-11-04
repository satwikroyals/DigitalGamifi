using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
//using System.Web.Http.Cors;
using DIGITAL_GAMIFY.BAL;
using DIGITAL_GAMIFY.Entities;
using SendGrid;
using SendGrid.SmtpApi;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using DIGITAL_GAMIFY.Code;


namespace DIGITAL_GAMIFY.Services
{
    //[EnableCors(origins: "http://www.gamesnatcherz.com,http://gamesnatcherz.com", headers: "*", methods: "*")]
    public class CustomerController : ApiController
    {
        private CustomerManager objcm = new CustomerManager();
        [Route("api/RegisterCustomer")]
        [HttpPost]
        public object RegisterCustomer([FromBody] CustomerEntity ce)
        {
            try
            {
               
                StatusEntity se = objcm.RegisterCustomer(ce);
                object res = new
                {
                    Status = true,
                    Success = se == null ? false : true,
                    Data = se
                };
                return res;
            }
            catch (Exception ex)
            {
                object res = new
                {
                    Status = false,
                    Data = ex
                };
                return res;
            }
        }

        [Route("api/UpdateCustomer")]
        [HttpPost]
        public object UpdateCustomer([FromBody] CustomerEntity ce)
        {
            try
            {
                StatusEntity se = objcm.UpdateCustomer(ce);
                object res = new
                {
                    Status = true,
                    Success = se == null ? false : true,
                    Data = se
                };
                return res;
            }
            catch (Exception ex)
            {
                object res = new
                {
                    Status = false,
                    Data = ex
                };
                return res;
            }
        }


        [Route("api/CheckCustomerLogin")]
        [HttpGet]
        public object CheckCustomerLogin(string mob, string pin, string deviceid, int apptype)
        {

            try
            {
                CustomerEntity ce = objcm.CheckCustomerLogin(mob, pin,deviceid,apptype);
                object res = new
                {
                    Status = true,
                    Success = ce == null ? false : true,
                    Data = ce
                };
                return res;
            }
            catch (Exception ex)
            {
                object res = new
                {
                    Status = false,
                    Data = ex
                };
                return res;
            }
        }
        [Route("api/GetCustomerProfile")]
        [HttpGet]
        public object GetCustomerProfile(Int64 id)
        {
            try
            {
                CustomerEntity ce = objcm.GetCustomerDetailsbyId(id);
                object res = new
                {
                    Status = true,
                    Success = ce == null ? false : true,
                    Data = ce
                };
                return res;
            }
            catch (Exception ex)
            {
                object res = new
                {
                    Status = false,
                    Data = ex
                };
                return res;
            }
        }
        [Route("api/GetGameResultsByCustomer")]
        [HttpGet]
        public object GetGameResultsByCustomer(Int64 Cid)
        {
            try
            {
                List<GameResultEntity> ge = objcm.GetGameResultsByCustomer(Cid);
                object res = new
                {
                    Status = true,
                    Success = ge == null ? false : true,
                    Data = ge
                };
                return res;
            }
            catch (Exception ex)
            {
                object res = new
                {
                    Status = false,
                    Data = ex
                };
                return res;
            }
        }


        [Route("api/GetCustomerSwipeandWinPrizes")]
        [HttpGet]
        public List<SwipeandWinWinPrizesList> GetCustomerSwipeandWinPrizes(Int64 Cid)
        {
            try
            {
                List<SwipeandWinWinPrizesList> res = new List<SwipeandWinWinPrizesList>();
                List<SwipeandWinWinPrizesEntity> ge = objcm.GetCustomerSwipeandWinPrizes(Cid);              
                if(ge!=null && ge.Count>0)
                {
                    foreach(SwipeandWinWinPrizesEntity p in ge)
                    {
                        string prizeimage = "";
                        string prizetext = "";

                        switch (p.PrizeNumber)
                        {
                            case 1: 
                                prizeimage = p.FirstPrizeImagePath;
                                prizetext = p.FirstPrizeText;
                                break;
                            case 2:
                                prizeimage = p.SecondPrizeImagePath;
                                prizetext = p.SecondPrizeText;
                                break;
                            case 3:
                                prizeimage = p.ThirdPrizeImagePath;
                                prizetext = p.ThirdPrizeText;
                                break;
                        }

                        res.Add(new SwipeandWinWinPrizesList()
                        {
                            GameId = p.GameId,
                            BusinessId=p.BusinessId,
                            Title=p.Title,
                            Image=p.Image,
                            PrizeNumber=p.PrizeNumber,
                            PrizeText=prizetext,
                            PrizeImage = prizeimage,
                            WinDate=p.WinDate,
                            RedeemCode=p.RedeemCode
                        });
                    }

                    return res;
                }
                else { return null; }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("api/ShareGamePrize")]
        [HttpGet]
        public StatusResponse ShareGamePrize(int Type,Int32 Resultid,Int64 Cid,Int64 Sharedby)
        {
            try
            {
                return objcm.ShareGamePrize(Type,Resultid, Cid, Sharedby);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [Route("api/GetSharedPrizes")]
        [HttpGet]
        public object GetSharedPrizes(Int64 Cid)
        {
            try
            {
                List<GameResultEntity> ge = objcm.GetSharedPrizes(Cid);
                object res = new
                {
                    Status = true,
                    Success = ge == null ? false : true,
                    Data = ge
                };
                return res;
            }
            catch (Exception ex)
            {
                object res = new
                {
                    Status = false,
                    Data = ex
                };
                return res;
            }
        }
        [Route("api/GetAllCustomers")]
        [HttpGet]
        public object GetAllCustomers(Int64 Cid)
        {
            try
            {
                List<CustomerEntity> ge = objcm.GetAllCustomers(Cid);
                object res = new
                {
                    Status = true,
                    Success = ge == null ? false : true,
                    Data = ge
                };
                return res;
            }
            catch (Exception ex)
            {
                object res = new
                {
                    Status = false,
                    Data = ex
                };
                return res;
            }
        }
        [Route("api/GetCustomerGames")]
        [HttpGet]
        public object GetCustomerGames(Int32 adminid, Int32 bid,Int32 cid, [FromUri]paggingEntity pe)
        {
            try
            {
                QuizManager qm=new QuizManager();
                SwipeandWinManager swm = new SwipeandWinManager();
                SmartQuizManager sqm = new SmartQuizManager();
                List<QuizEntity> ge = qm.GetQuizList(pe,adminid,bid,cid);
                List<SmartQuizEntity> se = sqm.GetSmartQuizList(pe, bid,cid);
                List<SwipeAndWinGameDetails> swgel = swm.GetAvailableGames(bid, cid);
                object res = new
                {
                    Status = true,
                    Success = swgel == null ? false : true,
                    SwipeandWin = swgel,
                    Quiz = ge,
                    SmartQuiz = se
                };
                return res;
            }
            catch (Exception ex)
            {
                object res = new
                {
                    Status = false,
                    Data = ex
                };
                return res;
            }
        }
        [Route("api/GetSwipeandWinBusiness")]
        [HttpGet]
        public List<BusinessEntity> GetSwipeandWinBusiness(Int32 cid)
        {
            return objcm.GetSwipeandWinBusiness(cid);
        }
        [Route("api/GetSmartQuizBusiness")]
        [HttpGet]
        public List<BusinessEntity> GetSmartQuizBusiness(Int32 cid)
        {
            return objcm.GetSmartQuizBusiness(cid);
        }
        [Route("api/GetQuizBusiness")]
        [HttpGet]
        public List<BusinessEntity> GetQuizBusiness(Int32 cid)
        {
            return objcm.GetQuizBusiness(cid);
        }
        [Route("api/GetSurveyBusiness")]
        [HttpGet]
        public List<BusinessEntity> GetSurveyBusiness(Int32 cid)
        {
            return objcm.GetSurveyBusiness(cid);
        }
        [Route("api/GetBusinessByLocation")]
        [HttpGet]
        public List<BusinessEntity> GetBusinessByLocation(Int32 AdminId,decimal Latitude,decimal Longitude,int Radius,Int64 cid)
        {
            return objcm.GetBusinessByLocation(AdminId, Latitude, Longitude, Radius,cid);
        }
        [Route("api/GetCustomerPrizes")]
        [HttpGet]
        public object GetCustomerPrizes(Int64 cid, Int64 bid)
        {
            try
            {
                QuizManager qm = new QuizManager();
                SmartQuizManager sqm = new SmartQuizManager();
                SurveyManager sm = new SurveyManager();
                List<GameResultEntity> gre = objcm.GetGameResultsByCustomer(cid);
                List<QuizPrizesEntity> ge = qm.GetCustomerQuizPrizes(cid,bid);
                List<SmarQuizPrizesEntity> se = sqm.GetCustomerSmartQuizPrizes(cid, bid);
                List<SurveyResult> sr = sm.GetCustomerSurveyPrizes(cid, bid);
                object res = new
                {
                    Status = true,
                    Swipeandwin=gre,
                    Quiz = ge,
                    SmartQuiz = se,
                    Survey=sr
                };
                return res;
            }
            catch (Exception ex)
            {
                object res = new
                {
                    Status = false,
                    Data = ex
                };
                return res;
            }
        }
        [Route("api/GetFavouriteBusiness")]
        [HttpGet]
        public List<BusinessEntity> GetFavouriteBusiness(Int64 cid)
        {
            return objcm.GetFavouriteBusiness(cid);
        }
        [Route("api/AddRemoveFavourite")]
        [HttpGet]
        public StatusResponse AddRemoveFavourite(Int64 cid,Int64 bid)
        {
            return objcm.AddRemoveFavourite(cid, bid);
        }
        [Route("api/ForgotPassword")]
        [HttpGet]
        public StatusEntity ForgotPassword(string email)
        {
            StatusEntity st = new StatusEntity();
            st = objcm.ForgotPassword(email);
            if (st.Pin != null)
            {
                string body = "";
                body += "<html><body>";
                body += "<p>Please find the  4 digit pin for your Gamesnatcherz login Pin:" + st.Pin + "</p>";
                sendmail send = new sendmail();
                send.SendEmail(email, "Pin Recover", "", body, "no-replay@gmilink.com", "Gamesnatcherz");
            }
            return st;
        }
        [Route("api/EmailVerification")]
        [HttpGet]
        public StatusResponse EmailVerification(string Email)
        {
            StatusResponse st = new StatusResponse();
            string VerificationNumber = Globalsettings.RandomNumber();
            st.StatusMessage = VerificationNumber;
            //st = cm.InsertVerificationCode(Mobile, VerificationNumber);
            string body = "";
            body += "<html><body>";
            body += "<p>Please find the  4 digit pin for your Gamesnatcherz Verification Number:" + VerificationNumber + "</p>";
            sendmail send = new sendmail();
            send.SendEmail(Email, "Gamesnatcherz Verification", "", body, "no-replay@gmilink.com", "Gamesnatcherz");
            return st;
        }
        [Route("api/RegisterGuest")]
        [HttpPost]
        public StatusEntity RegisterGuest([FromBody]CustomerEntity ce)
        {
            return objcm.RegisterGuest(ce);
        }
    }
}