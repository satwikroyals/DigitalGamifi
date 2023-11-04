using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DIGITAL_GAMIFY.DAL.Repositiories;
using DIGITAL_GAMIFY.Entities;
using System.Data;



namespace DIGITAL_GAMIFY.DAL
{
    public class CustomerData
    {
       public StatusEntity RegisterCustomer(CustomerEntity ce)
        {

            DapperRepositry<StatusEntity> repo = new DapperRepositry<StatusEntity>();

                var parm = new DynamicParameters();
                parm.Add("@FirstName", ce.FirstName, DbType.String, ParameterDirection.Input);
                parm.Add("@LastName", ce.LastName, DbType.String, ParameterDirection.Input);
                parm.Add("@Mobile", ce.Mobile, DbType.String, ParameterDirection.Input);
                parm.Add("@Email", ce.Email, DbType.String, ParameterDirection.Input);
                parm.Add("@Pin", ce.Pin, DbType.String, ParameterDirection.Input);
                parm.Add("@Age", ce.Age, DbType.Int16, ParameterDirection.Input);
                parm.Add("@Gender", ce.Gender, DbType.Int16, ParameterDirection.Input);
                parm.Add("@ZipCode", ce.ZipCode, DbType.String, ParameterDirection.Input);
                parm.Add("@Address", ce.Address, DbType.String, ParameterDirection.Input);
                parm.Add("@BusinessId", ce.BusinessId, DbType.Int64, ParameterDirection.Input);
                parm.Add("@Guest", ce.Guest, DbType.Int16, ParameterDirection.Input);
                repo.SpName = "RegisterCustomer";
                repo.Parameters = parm;
                return repo.FindById();           
        }

       public StatusEntity UpdateCustomer(CustomerEntity ce)
       {

           DapperRepositry<StatusEntity> repo = new DapperRepositry<StatusEntity>();

           var parm = new DynamicParameters();
           parm.Add("@CustomerId", ce.CustomerId, DbType.Int64, ParameterDirection.Input);
           parm.Add("@FirstName", ce.FirstName, DbType.String, ParameterDirection.Input);
           parm.Add("@LastName", ce.LastName, DbType.String, ParameterDirection.Input);
           parm.Add("@Mobile", ce.Mobile, DbType.String, ParameterDirection.Input);
           parm.Add("@Email", ce.Email, DbType.String, ParameterDirection.Input);
           parm.Add("@Age", ce.Age, DbType.Int16, ParameterDirection.Input);
           parm.Add("@ZipCode", ce.ZipCode, DbType.String, ParameterDirection.Input);
           parm.Add("@Address", ce.Address, DbType.String, ParameterDirection.Input);
                     
           repo.SpName = "UpdateCustomer";
           repo.Parameters = parm;
           return repo.FindById();

       }


        public CustomerEntity CheckCustomerLogin(string mob,string pin,string deviceid,int apptype)
       {
           DapperRepositry<CustomerEntity> repo = new DapperRepositry<CustomerEntity>();

           var parm = new DynamicParameters();
          
           parm.Add("@Mobile",mob, DbType.String, ParameterDirection.Input);
           parm.Add("@Pin", pin, DbType.String, ParameterDirection.Input);
           parm.Add("@DeviceId", deviceid, DbType.String, ParameterDirection.Input);
           parm.Add("@App", apptype, DbType.Int16, ParameterDirection.Input);
           repo.SpName = "CheckCustomerLogin";
           repo.Parameters = parm;
           return repo.FindById();
       }
        public CustomerEntity GetCustomerDetailsbyId(Int64 Id)
        {
            DapperRepositry<CustomerEntity> repo = new DapperRepositry<CustomerEntity>();

            var parm = new DynamicParameters();

            parm.Add("@CustomerId", Id, DbType.Int64, ParameterDirection.Input);
            repo.SpName = "GetCustomerDetailsById";
            repo.Parameters = parm;
            return repo.FindById();
        }
        public List<CustomerDeviceEntity> GetCustomerDevicesByCustomerIds(string cids)
        {
            DapperRepositry<CustomerDeviceEntity> repo = new DapperRepositry<CustomerDeviceEntity>();

            var parm = new DynamicParameters();

            parm.Add("@CustomerIds", cids, DbType.String, ParameterDirection.Input);
            return repo.GetList("GetCustomerDevicesByCustomerIds", parm);
        }
        public List<GameResultEntity> GetGameResultsByCustomer(Int64 Cid)
        {
            DapperRepositry<GameResultEntity> repo = new DapperRepositry<GameResultEntity>();

            var parm = new DynamicParameters();

            parm.Add("@CustomerId", Cid, DbType.Int64, ParameterDirection.Input);
            return repo.GetList("customer.GetGameResultsByCustomer", parm);
        }
        public List<SwipeandWinWinPrizesEntity> GetCustomerSwipeandWinPrizes(Int64 Cid)
        {
            DapperRepositry<SwipeandWinWinPrizesEntity> repo = new DapperRepositry<SwipeandWinWinPrizesEntity>();

            var parm = new DynamicParameters();

            parm.Add("@CustomerId", Cid, DbType.Int64, ParameterDirection.Input);
            return repo.GetList("GetCustomerSwipeandWinPrizes", parm);
        }
        public StatusResponse ShareGamePrize(int Type,Int32 Resultid,Int64 Cid,Int64 Sharedby)
        {
            DapperRepositry<StatusResponse> repo = new DapperRepositry<StatusResponse>();

            var parm = new DynamicParameters();
            parm.Add("@Type", Type, DbType.Int32, ParameterDirection.Input);
            parm.Add("@ResultId", Resultid, DbType.Int32, ParameterDirection.Input);
            parm.Add("@Cid", Cid, DbType.Int64, ParameterDirection.Input);
            parm.Add("@SharedBy",Sharedby, DbType.Int64, ParameterDirection.Input);

            return repo.GetResult("Customer.InsertPrizeShare", parm);
        }
        public List<GameResultEntity> GetSharedPrizes(Int64 Cid)
        {
            DapperRepositry<GameResultEntity> repo = new DapperRepositry<GameResultEntity>();

            var parm = new DynamicParameters();

            parm.Add("@CustomerId", Cid, DbType.Int64, ParameterDirection.Input);
            return repo.GetList("Customer.SharedPrizes", parm);
        }
        public List<CustomerEntity> GetAllCustomers(Int64 Cid)
        {
            DapperRepositry<CustomerEntity> repo = new DapperRepositry<CustomerEntity>();

            var parm = new DynamicParameters();

            parm.Add("@CustomerId", Cid, DbType.Int64, ParameterDirection.Input);
            return repo.GetList("customer.GetAllCustomer", parm);
        }
        public List<BusinessEntity> GetSwipeandWinBusiness(Int32 cid)
        {
            DapperRepositry<BusinessEntity> repo = new DapperRepositry<BusinessEntity>();

            var parm = new DynamicParameters();

            parm.Add("@CustomerId", cid, DbType.Int32, ParameterDirection.Input);
            return repo.GetList("GetSwipeandWinBusiness", parm);
        }
        public List<BusinessEntity> GetSmartQuizBusiness(Int32 cid)
        {
            DapperRepositry<BusinessEntity> repo = new DapperRepositry<BusinessEntity>();

            var parm = new DynamicParameters();

            parm.Add("@CustomerId", cid, DbType.Int32, ParameterDirection.Input);
            return repo.GetList("GetSmartQuizBusiness", parm);
        }
        public List<BusinessEntity> GetQuizBusiness(Int32 cid)
        {
            DapperRepositry<BusinessEntity> repo = new DapperRepositry<BusinessEntity>();

            var parm = new DynamicParameters();

            parm.Add("@CustomerId", cid, DbType.Int32, ParameterDirection.Input);
            return repo.GetList("GetQuizBusiness", parm);
        }
        public List<BusinessEntity> GetSurveyBusiness(Int32 cid)
        {
            DapperRepositry<BusinessEntity> repo = new DapperRepositry<BusinessEntity>();

            var parm = new DynamicParameters();

            parm.Add("@CustomerId", cid, DbType.Int32, ParameterDirection.Input);
            return repo.GetList("GetSurveyBusiness", parm);
        }

        public List<BusinessEntity> GetBusinessByLocation(Int32 AdminId,decimal Latitude,decimal Longitude,int Radius,Int64 cid)
        {
            DapperRepositry<BusinessEntity> repo = new DapperRepositry<BusinessEntity>();

            var parm = new DynamicParameters();

            parm.Add("@AdminId", AdminId, DbType.Int32, ParameterDirection.Input);
            parm.Add("@RADIUS", Radius, DbType.Int16, ParameterDirection.Input);
            parm.Add("@Latitude", Latitude, DbType.Decimal, ParameterDirection.Input);
            parm.Add("@Longitude", Longitude, DbType.Decimal, ParameterDirection.Input);
            parm.Add("@CustomerId", cid, DbType.Int64, ParameterDirection.Input);

            return repo.GetList("customer.GetBusinessListByLatLong", parm);
        }
        public List<BusinessEntity> GetFavouriteBusiness(Int64 cid)
        {
            DapperRepositry<BusinessEntity> _repo = new DapperRepositry<BusinessEntity>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@CustomerId", cid, DbType.Int64, ParameterDirection.Input);
            return _repo.GetList("GetCustomerFavouriteBusiness", param);
        }

        public StatusResponse AddRemoveFavourite(Int64 cid,Int64 bid)
        {
            DapperRepositry<StatusResponse> _repo = new DapperRepositry<StatusResponse>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@CustomerId", cid, DbType.Int64, ParameterDirection.Input);
            param.Add("@MerchantId", bid, DbType.Int64, ParameterDirection.Input);
            return _repo.GetResult("AddCustomerFavourites", param);
        }
        public StatusEntity ForgotPassword(string email)
        {
            DapperRepositry<StatusEntity> _repo = new DapperRepositry<StatusEntity>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@Email", email, DbType.String, ParameterDirection.Input);

            return _repo.GetResult("ForgotPin", param);
        }
        public StatusEntity RegisterGuest(CustomerEntity ce)
        {

            DapperRepositry<StatusEntity> _repo = new DapperRepositry<StatusEntity>();

            DynamicParameters param = new DynamicParameters();
            param.Add("@FirstName", ce.FirstName, DbType.String, ParameterDirection.Input);
            param.Add("@LastName", ce.LastName, DbType.String, ParameterDirection.Input);
            param.Add("@Mobile", ce.Mobile, DbType.String, ParameterDirection.Input);
            param.Add("@Email", ce.Email, DbType.String, ParameterDirection.Input);
            param.Add("@DOB", ce.DOB, DbType.String, ParameterDirection.Input);
            param.Add("@Age", ce.Age, DbType.Int16, ParameterDirection.Input);
            param.Add("@Gender", ce.Gender, DbType.Int16, ParameterDirection.Input);
            param.Add("@City", ce.City, DbType.String, ParameterDirection.Input);
            param.Add("@StateId", ce.StateId, DbType.String, ParameterDirection.Input);
            param.Add("@ZipCode", ce.ZipCode, DbType.String, ParameterDirection.Input);
            param.Add("@BusinessId", ce.BusinessId, DbType.Int64, ParameterDirection.Input);
            param.Add("@Guest", ce.Guest, DbType.Int16, ParameterDirection.Input);
            param.Add("@Address", ce.Address, DbType.String, ParameterDirection.Input);
            param.Add("@AddressLine2", ce.AddressLine2, DbType.String, ParameterDirection.Input);
            param.Add("@ReferredBy", ce.ReferredBy, DbType.String, ParameterDirection.Input);
            return _repo.GetResult("RegisterGuest", param);
        }
    }
}
