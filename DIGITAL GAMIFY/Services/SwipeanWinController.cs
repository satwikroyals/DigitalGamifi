using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using System.Web.Http.Cors;
using DIGITAL_GAMIFY.BAL;
using DIGITAL_GAMIFY.Entities;

namespace DIGITAL_GAMIFY.Services
{
    //[EnableCors(origins: "http://www.gamesnatcherz.com,http://gamesnatcherz.com", headers: "*", methods: "*")]
    public class SwipeandWinController : ApiController
    {

        private SwipeandWinManager objbm = new SwipeandWinManager();

        [Route("api/AdminGetSwipeandWin")]
        [HttpPost]
        public List<SwipeandWinEntity> AdminGetSwipeandWin(SwipeandWinListParamsEntity p)
        {
            return objbm.AdminGetSwipeandWin(p);
        }


        [Route("api/RedeemPrize")]
        [HttpGet]
        public object RedeemPrize(Int64 cid, Int64 gid, Int16 pznum,Int16 Promocheck,string Size,string Colour,string Address)
        {
            try
            {
                string redeemcode = "";
                redeemcode = Settings.RandomNumber();
                StatusEntity ce = objbm.RedeemPrize(cid, gid, pznum, redeemcode, Promocheck, Size, Colour,Address);
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
        [Route("api/GetSwipeGamePlaydetails")]
        [HttpGet]
        public object GetSwipeGamePlayDetails(Int32 Id, Int32 CId)
        {       
            try
            {
                //string StatusMsg = "";
                object ge = objbm.GetSwipePlayGameDetails(Id, CId);
                object res = new
                {
                    StatusMsg = ge == null ? "You have finished your game." : "",
                    Status = ge == null ? false : true,
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
        [Route("api/GetSwipeGameDetails")]
        [HttpGet]
        public object GetSwipeGameDetails(Int32 Gid,Int32 Cid)
        {

            try
            {
                SwipeAndWinGameDetails ge = objbm.GetGamedetailsById(Gid,Cid);
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
        [Route("api/GetAvailableGames")]
        [HttpGet]
        public object GetAvailableGames(Int32 bid,Int32 cid)
        {

            try
            {
                List<SwipeAndWinGameDetails> swgel = new List<SwipeAndWinGameDetails>();
                swgel = objbm.GetAvailableGames(bid,cid);
                object res = new
                {
                    Status = true,
                    Success = swgel == null ? false : true,
                    Data = swgel
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
        [Route("api/AdminGetSwipeandWinPrizes")]
        [HttpGet]
        public List<BusinessGameResultEntity> AdminGetSwipeandWinPrizes(Int32 bid, int status, Int64 gid, int prize, [FromUri]paggingEntity p)
        {
            return objbm.AdminGetSwipeandWinPrizes(p, bid, status,gid,prize);
        }
        [Route("api/GetSwipeandWinById")]
        [HttpGet]
        public SwipeandWinEntity GetSwipeandWinById(Int64 Id)
        {
            return objbm.GetSwipeandWinById(Id);
        }
        [Route("api/GetddlGames")]
        [HttpGet]
        public List<Quizddl> GetddlGames(Int32 bid)
        {
            return objbm.GetddlGames(bid);
        }
    }
   
}
