using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIGITAL_GAMIFY.Entities;
using DIGITAL_GAMIFY.DAL;

namespace DIGITAL_GAMIFY.BAL
{
    public class CustomerManager
    {
        private CustomerData _cudata = new CustomerData();
        public StatusEntity RegisterCustomer(CustomerEntity ce)
        {
            return _cudata.RegisterCustomer(ce);
        }
        public StatusEntity UpdateCustomer(CustomerEntity ce)
        {
            return _cudata.UpdateCustomer(ce);
        }
        public CustomerEntity CheckCustomerLogin(string mob, string pin, string deviceid, int apptype)
        {
            return _cudata.CheckCustomerLogin(mob, pin,deviceid,apptype);
        }
        public CustomerEntity GetCustomerDetailsbyId(Int64 Id)
        {
            return _cudata.GetCustomerDetailsbyId(Id);
        }
        public List<CustomerDeviceEntity> GetCustomerDevicesByCustomerIds(string cids)
        {
            return _cudata.GetCustomerDevicesByCustomerIds(cids);
        }
        public List<GameResultEntity> GetGameResultsByCustomer(Int64 Cid)
        {
            return _cudata.GetGameResultsByCustomer(Cid);
        }

        public List<SwipeandWinWinPrizesEntity> GetCustomerSwipeandWinPrizes(Int64 Cid)
        {
            return _cudata.GetCustomerSwipeandWinPrizes(Cid);
        }
        public StatusResponse ShareGamePrize(int Type,Int32 Resultid,Int64 Cid,Int64 Sharedby)
        {
            return _cudata.ShareGamePrize(Type, Resultid, Cid, Sharedby);
        }
        public List<GameResultEntity> GetSharedPrizes(Int64 Cid)
        {
            return _cudata.GetSharedPrizes(Cid);
        }
        public List<CustomerEntity> GetAllCustomers(Int64 Cid)
        {
            return _cudata.GetAllCustomers(Cid);
        }
        public List<BusinessEntity> GetSwipeandWinBusiness(Int32 cid)
        {
            return _cudata.GetSwipeandWinBusiness(cid);
        }
        public List<BusinessEntity> GetSmartQuizBusiness(Int32 cid)
        {
            return _cudata.GetSmartQuizBusiness(cid);
        }
        public List<BusinessEntity> GetQuizBusiness(Int32 cid)
        {
            return _cudata.GetQuizBusiness(cid);
        }
        public List<BusinessEntity> GetSurveyBusiness(Int32 cid)
        {
            return _cudata.GetSurveyBusiness(cid);
        }
        public List<BusinessEntity> GetBusinessByLocation(Int32 AdminId,decimal Latitude,decimal Longitude,int Radius,Int64 cid)
        {
            return _cudata.GetBusinessByLocation(AdminId, Latitude, Longitude, Radius,cid);
        }
        public List<BusinessEntity> GetFavouriteBusiness(Int64 cid)
        {
            return _cudata.GetFavouriteBusiness(cid);
        }
        public StatusResponse AddRemoveFavourite(Int64 cid,Int64 bid)
        {
            return _cudata.AddRemoveFavourite(cid, bid);
        }
        public StatusEntity ForgotPassword(string email)
        {
            return _cudata.ForgotPassword(email);
        }
        public StatusEntity RegisterGuest(CustomerEntity ce)
        {
            return _cudata.RegisterGuest(ce);
        }
    }
}
