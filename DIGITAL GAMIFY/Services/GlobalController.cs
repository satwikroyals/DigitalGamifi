using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using System.Web.Http.Cors;
using DIGITAL_GAMIFY.BAL;
using DIGITAL_GAMIFY.Entities;
using DIGITAL_GAMIFY.Code;

namespace DIGITAL_GAMIFY.Services
{
    //[EnableCors(origins: "http://www.gamesnatcherz.com,http://gamesnatcherz.com", headers: "*", methods: "*")]
    public class GlobalController : ApiController
    {

        [Route("api/GetBusinessTypes")]       
        [HttpGet]
        public List<BusinessTypesEntity> GetBusinessTypes()
        {
            GlobalManager bal = new GlobalManager();
            return bal.GetBusinessTypes();
        }

        [Route("api/GetDdlBusiness")]       
        [HttpGet]
        public List<SearchDdlEntities> GetDdlBusiness(Int32 AdminId)
        {
            GlobalManager bal = new GlobalManager();
            return bal.GetDdlBusiness(AdminId);
        }
        [Route("api/BusinessDeliverprizeactionbtn")]
        [HttpGet]
        public StatusResponse BusinessDeliverprizeactionbtn(Int64 resultid,int type,int action)
        {
            GlobalManager bal = new GlobalManager();
            return bal.BusinessDeliverprizeactionbtn(resultid, type, action);
        }
        [Route("api/SendDigitalGamifyPrizeNotification")]
        [HttpPost]
        public object SendDigitalGamifyPrizeNotification([FromBody]ShopfrontalPrizeNotificationEntity p)
        {
            try
            {
                BusinessManager bm = new BusinessManager();
                string emailstring = Globalsettings.GetGamesnatcherzEmail(p.PrizeNumber,p.Type,p.LastChance);

                BusinessEntity be=bm.GetBusinessById(p.BusinessId);

                emailstring = emailstring.Replace("[blogo]", be.LogoPath);
                emailstring = emailstring.Replace("[businessname]", be.BusinessName);
                emailstring = emailstring.Replace("[redeemcode]", p.RedeemCode);
                emailstring = emailstring.Replace("[GameName]", p.Title);
                emailstring = emailstring.Replace("[PrizeImage]", p.PrizePath);
                emailstring = emailstring.Replace("[PrizeDetails]", p.PrizeDetails);
                emailstring = emailstring.Replace("[GameimgPath]", p.GameimgPath);



                //SwipeandWinManager bal = new SwipeandWinManager();
                //SwipeandWinEntity g = bal.GetSwipeandWinById(Convert.ToInt32(p.Gameid));

                //if (g != null)
                //{
                //    if (!string.IsNullOrEmpty(g.RedeemMailRedirectUrl))
                //    {
                //        emailstring = emailstring.Replace("[RedeemMailRedirectUrl]", g.RedeemMailRedirectUrl);
                //    }
                //    else
                //    {
                //        emailstring = emailstring.Replace("[RedeemMailRedirectUrl]", "#");
                //    }
                //}

                Globalsettings.SendEmail(p.Email, "Gamesnatcherz", "", be.BusinessName, emailstring);
                object res = new
                {
                    Status = true

                };
                return res;
            }
            catch (Exception ex)
            {
                object res = new
                {
                    Status = false
                };
                return res;
            }
        }
        [Route("api/GetAttributesByPrizeTypeId")]
        [HttpGet]
        public List<Attributes> GetAttributesByPrizeTypeId(Int32 ptid)
        {
            GlobalManager bal = new GlobalManager();
            return bal.GetAttributesByPrizeTypeId(ptid);
        }
        [Route("api/GetResultById")]
        [HttpGet]
        public ReultDetails GetResultById(Int64 resultid, int type)
        {
            GlobalManager bal = new GlobalManager();
            return bal.GetResultById(resultid, type);
        }
        [Route("api/GetStateddl")]
        [HttpGet]
        public List<Statesddl> GetStateddl()
        {
            GlobalManager bal = new GlobalManager();
            return bal.GetStateddl();
        }

    }
}
