using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DIGITAL_GAMIFY.DAL.Repositiories;
using DIGITAL_GAMIFY.Entities;
using System.Data;
using DbFactory;
namespace DIGITAL_GAMIFY.DAL
{
    public  class BusinessData
    {
        public BusinessEntity GetBusinessById(Int64 Id)
        {
            DapperRepositry<BusinessEntity> repo = new DapperRepositry<BusinessEntity>();
           
            var parm = new DynamicParameters();
            parm.Add("@BusinessId", Id, DbType.Int64, ParameterDirection.Input);
            repo.SpName = "GetBusinessById";
            repo.Parameters = parm;
            return repo.FindById();
        }
       

        public List<BusinessEntity> AdminGetBusiness(BusinessListParamsEntity p)
        {
            try
            {
                DapperRepositry<BusinessEntity> _repo = new DapperRepositry<BusinessEntity>();
                DynamicParameters param = new DynamicParameters();
                param.Add("AdminId", p.AdminId, DbType.Int32, ParameterDirection.Input);
                param.Add("BusinessId", p.BusinessId, DbType.Int32, ParameterDirection.Input);
                param.Add("FromDate", p.FromDate, DbType.String, ParameterDirection.Input);
                param.Add("ToDate", p.ToDate, DbType.String, ParameterDirection.Input);
                param.Add("PageIndex", p.Pi, DbType.Int32, ParameterDirection.Input);
                param.Add("PageSize", p.Ps, DbType.Int32, ParameterDirection.Input);
                param.Add("BusinessTypeId", p.BusinessTypeId, DbType.Int32, ParameterDirection.Input);
                param.Add("Search", p.str, DbType.String , ParameterDirection.Input);
                return _repo.GetList("AdminGetBusiness", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BusinessEntity> GetSwipeandWinClaimedBusiness(Int32 cid)
        {
            try
            {
                DapperRepositry<BusinessEntity> _repo = new DapperRepositry<BusinessEntity>();
                DynamicParameters param = new DynamicParameters();
                param.Add("CustomerId", cid, DbType.Int32, ParameterDirection.Input);
                return _repo.GetList("GetSwipeandWinClaimedBusiness", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BusinessEntity GetBusinessById(Int32 businessId)
        {
            try
            {
                DapperRepositry<BusinessEntity> _repo = new DapperRepositry<BusinessEntity>();
                DynamicParameters param = new DynamicParameters();
                param.Add("BusinessId", businessId, DbType.Int32, ParameterDirection.Input);
                return _repo.GetResult("GetBusinessById", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BusinessEntity AddBusiness(BusinessEntity p)
        {
            try
            {
                DapperRepositry<BusinessEntity> _repo = new DapperRepositry<BusinessEntity>();
                DynamicParameters param = new DynamicParameters();
                param.Add("AdminId", p.AdminId, DbType.Int32, ParameterDirection.Input);
                param.Add("BusinessId", p.BusinessId, DbType.Int32, ParameterDirection.Input);
                param.Add("FirstName", p.FirstName, DbType.String, ParameterDirection.Input);
                param.Add("LastName", p.LastName, DbType.String, ParameterDirection.Input);
                param.Add("Email", p.Email, DbType.String, ParameterDirection.Input);
                param.Add("Mobile", p.Mobile, DbType.String, ParameterDirection.Input);
                param.Add("Logo", p.Logo, DbType.String, ParameterDirection.Input);
                param.Add("UserName", p.UserName, DbType.String, ParameterDirection.Input);
                param.Add("Password", p.Password, DbType.String, ParameterDirection.Input);
                param.Add("PrizeImage", p.PrizeImage, DbType.String, ParameterDirection.Input);
                param.Add("PrizeCount", p.PrizeCount, DbType.Int32, ParameterDirection.Input);
                param.Add("BusinessName", p.BusinessName, DbType.String, ParameterDirection.Input);
                param.Add("BusinessTypeId", p.BusinessTypeId, DbType.Int16, ParameterDirection.Input);
                param.Add("ZipCode", p.ZipCode, DbType.String, ParameterDirection.Input);
                param.Add("Address", p.Address, DbType.String, ParameterDirection.Input);
                param.Add("Latitude", p.Latitude, DbType.Decimal, ParameterDirection.Input);
                param.Add("Longitude", p.Longitude, DbType.Decimal, ParameterDirection.Input);
                param.Add("IsActive", p.IsActive, DbType.Int16, ParameterDirection.Input);
                return _repo.GetResult("AddUpdateBusiness", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BusinessEntity GetBusinessLogin(BusinessLoginEntities p)
        {
            try
            {
                DapperRepositry<BusinessEntity> _repo = new DapperRepositry<BusinessEntity>();
                DynamicParameters param = new DynamicParameters();
                param.Add("UserName", p.UserName, DbType.String, ParameterDirection.Input);
                param.Add("Password", p.Password, DbType.String, ParameterDirection.Input);
                return _repo.GetResult("GetBusinessLogin", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public BusinessEntity GetBusinessAppLogin(string un, string pwd, int utype)
        {
            try
            {
                DapperRepositry<BusinessEntity> _repo = new DapperRepositry<BusinessEntity>();
                DynamicParameters param = new DynamicParameters();
                param.Add("UserName", un, DbType.String, ParameterDirection.Input);
                param.Add("Password", pwd, DbType.String, ParameterDirection.Input);
                param.Add("UserType", utype, DbType.String, ParameterDirection.Input);
                return _repo.GetResult("GetBusinessAppLogin", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public List<BusinessGameResultEntity> GetGameResultsByBusiness(Int64 bid)
        //{
        //    DapperRepositry<BusinessGameResultEntity> repo = new DapperRepositry<BusinessGameResultEntity>();

        //    var parm = new DynamicParameters();

        //    parm.Add("@BusinessId", bid, DbType.Int64, ParameterDirection.Input);
        //    return repo.GetList("GetGameResultsByBusiness", parm);
        //}

        public StatusResponse RedeemGamePrize(Int32 gid, Int32 cid, int action)
        {
            DapperRepositry<StatusResponse> _repo = new DapperRepositry<StatusResponse>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@GameId", gid, DbType.Int32, ParameterDirection.Input);
            param.Add("@CustomerId", cid, DbType.Int32, ParameterDirection.Input);
            param.Add("@Action", action, DbType.Int16, ParameterDirection.Input);

            return _repo.GetResult("GamePrizeActionbtn", param);
        }
        public BusinessGameResultEntity CheckRedeemCode(string Rdcode)
        {
            DapperRepositry<BusinessGameResultEntity> _repo = new DapperRepositry<BusinessGameResultEntity>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@RedeemCode", Rdcode, DbType.String, ParameterDirection.Input);

            return _repo.GetResult("CheckRedeemCode", param);
        }
        public RedeemDetailsEntity GetGameResultsByBusiness(Int64 bid)
        {

            DbFactory.DbSettings _db = new DbFactory.DbSettings(Settings.ProviederName, Settings.DbConnection);
            RedeemDetailsEntity _repo = new RedeemDetailsEntity();
            DynamicParameters param = new DynamicParameters();
            param.Add("@BusinessId", bid, DbType.Int32, ParameterDirection.Input);
            using (IDbConnection db = (IDbConnection)_db.ConnectionString)
            {
                var result = db.QueryMultiple("GetGameResultsByBusiness", commandType: CommandType.StoredProcedure, param: param);
                var resmediadetails = result.Read<BusinessGameResultEntity>().ToList();
                var resmediaimages = resmediadetails.Where(m => m.Status == 0).ToList();
                var resmediavideos = resmediadetails.Where(m => m.Status == 1).ToList();
                _repo.UnRedeemed = resmediaimages;
                _repo.Redeemed = resmediavideos;
            }
            return _repo;
        }
        public DashboardEntity GetBusinessDashboard(Int32 businessId)
        {
            try
            {
                DapperRepositry<DashboardEntity> _repo = new DapperRepositry<DashboardEntity>();
                DynamicParameters param = new DynamicParameters();
                param.Add("BusinessId", businessId, DbType.Int32, ParameterDirection.Input);
                return _repo.GetResult("GetDashBoard", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
