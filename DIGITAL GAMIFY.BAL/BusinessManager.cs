using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIGITAL_GAMIFY.Entities;
using DIGITAL_GAMIFY.DAL;
namespace DIGITAL_GAMIFY.BAL
{
    public  class BusinessManager
    {
        private BusinessData _budata = new BusinessData();
        public BusinessEntity GetBusinessById(Int64 Id)
        {
            return _budata.GetBusinessById(Id);
        }        

        public List<BusinessEntity> AdminGetBusiness(BusinessListParamsEntity p)
        {
            return _budata.AdminGetBusiness(p);
        }

        public List<BusinessEntity> GetSwipeandWinClaimedBusiness(Int32 cid)
        {
            return _budata.GetSwipeandWinClaimedBusiness(cid);
        }

        public BusinessEntity GetBusinessById(Int32 businessId)
        {
            return _budata.GetBusinessById(businessId);
        }

        public BusinessEntity AddBusiness(BusinessEntity p)
        {
            return _budata.AddBusiness(p);
        }

        public BusinessEntity GetBusinessLogin(BusinessLoginEntities p)
        {
            return _budata.GetBusinessLogin(p);
        }
        public BusinessEntity GetBusinessAppLogin(string un, string pwd, int utype)
        {
            return _budata.GetBusinessAppLogin(un, pwd, utype);
        }
        public RedeemDetailsEntity GetGameResultsByBusiness(Int64 bid)
        {
            return _budata.GetGameResultsByBusiness(bid);
        }
        public StatusResponse RedeemGamePrize(Int32 gid, Int32 cid, int action)
        {
            return _budata.RedeemGamePrize(gid, cid, action);
        }
        public BusinessGameResultEntity CheckRedeemCode(string Rdcode)
        {
            return _budata.CheckRedeemCode(Rdcode);
        }
        public DashboardEntity GetBusinessDashboard(Int32 businessId)
        {
            return _budata.GetBusinessDashboard(businessId);
        }
    }
}
