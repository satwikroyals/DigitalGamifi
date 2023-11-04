using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DIGITAL_GAMIFY.DAL.Repositiories;
using DIGITAL_GAMIFY.Entities;
using System.Data;
using System.Data.SqlClient;


namespace DIGITAL_GAMIFY.DAL
{
    public class NotificationData
    {

        public List<NotificationEntity> GetNotifications()
        {
            try
            {
                DapperRepositry<NotificationEntity> _repo = new DapperRepositry<NotificationEntity>();
                DynamicParameters param = new DynamicParameters();
                return _repo.GetList("GetNotifications", null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<NotificationEntity> GetNotificationbyCustomer(Int32 cid,Int32 bid)
        {
            try
            {
                DapperRepositry<NotificationEntity> _repo = new DapperRepositry<NotificationEntity>();
                DynamicParameters param = new DynamicParameters();
                param.Add("CustomerId", cid, DbType.Int32, ParameterDirection.Input);
                param.Add("BusinessId", bid, DbType.Int32, ParameterDirection.Input);
                return _repo.GetList("GetNotificationByCustomer", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<NotificationEntity> AdminGetNotifications(NotificationListParamsEntity p)
        {
            try
            {
                DapperRepositry<NotificationEntity> _repo = new DapperRepositry<NotificationEntity>();
                DynamicParameters param = new DynamicParameters();
                param.Add("AdminId", p.AdminId, DbType.Int32, ParameterDirection.Input);
                param.Add("FromDate", p.FromDate, DbType.String, ParameterDirection.Input);
                param.Add("ToDate", p.ToDate, DbType.String, ParameterDirection.Input);
                param.Add("PageIndex", p.Pi, DbType.Int32, ParameterDirection.Input);
                param.Add("PageSize", p.Ps, DbType.Int32, ParameterDirection.Input);
                return _repo.GetList("AdminGetNotifications", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NotificationEntity GetNotificationById(Int64 notificationId)
        {
            try
            {
                DapperRepositry<NotificationEntity> _repo = new DapperRepositry<NotificationEntity>();
                DynamicParameters param = new DynamicParameters();
                param.Add("NotificationId", notificationId, DbType.Int64, ParameterDirection.Input);
                return _repo.GetResult("GetNotificationById", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PushNotificationEntity GetPushNotificationById(Int64 notificationId)
        {
            try
            {
                DapperRepositry<PushNotificationEntity> _repo = new DapperRepositry<PushNotificationEntity>();
                DynamicParameters param = new DynamicParameters();
                param.Add("NotificationId", notificationId, DbType.Int64, ParameterDirection.Input);
                return _repo.GetResult("GetPushNotificationById", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NotificationEntity AddNotification(NotificationEntity p)
        {
            try
            {
                DapperRepositry<NotificationEntity> _repo = new DapperRepositry<NotificationEntity>();
                DynamicParameters param = new DynamicParameters();
                param.Add("AdminId", p.AdminId, DbType.Int32, ParameterDirection.Input);
                param.Add("BusinessId", p.BusinessId, DbType.Int32, ParameterDirection.Input);
                param.Add("CustomerIds", p.CustomerIds, DbType.String, ParameterDirection.Input);
                param.Add("NotificationId", p.NotificationId, DbType.Int64, ParameterDirection.Input);
                param.Add("Title", p.Title, DbType.String, ParameterDirection.Input);
                param.Add("PromoText", p.PromoText, DbType.String, ParameterDirection.Input);
                param.Add("PromoLink", p.PromoLink, DbType.String, ParameterDirection.Input);
                param.Add("Image", p.Image, DbType.String, ParameterDirection.Input);
                param.Add("Conditions", p.Conditions, DbType.String, ParameterDirection.Input);
                param.Add("IsActive", p.IsActive, DbType.Int16, ParameterDirection.Input);
                return _repo.GetResult("AddUpdateNotification", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PushNotificationEntity AddPushNotification(PushNotificationEntity p)
        {
            try
            {
                DapperRepositry<PushNotificationEntity> _repo = new DapperRepositry<PushNotificationEntity>();
                DynamicParameters param = new DynamicParameters();
                param.Add("AdminId", p.AdminId, DbType.Int32, ParameterDirection.Input);
                param.Add("BusinessId", p.BusinessId, DbType.Int32, ParameterDirection.Input);
                param.Add("CustomerIds", p.CustomerIds, DbType.String, ParameterDirection.Input);
                param.Add("NotificationId", p.NotificationId, DbType.Int64, ParameterDirection.Input);
                param.Add("Title", p.Title, DbType.String, ParameterDirection.Input);
                param.Add("Message", p.Message, DbType.String, ParameterDirection.Input);
                param.Add("Url", p.Url, DbType.String, ParameterDirection.Input);             
                return _repo.GetResult("AddUpdatePushNotification", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<NotificationEntity> BusinessGetNotifications(NotificationListParamsEntity p)
        {
            try
            {
                DapperRepositry<NotificationEntity> _repo = new DapperRepositry<NotificationEntity>();
                DynamicParameters param = new DynamicParameters();
                param.Add("AdminId", p.AdminId, DbType.Int32, ParameterDirection.Input);
                param.Add("BusinessId", p.BusinessId, DbType.Int32, ParameterDirection.Input);
                param.Add("FromDate", p.FromDate, DbType.String, ParameterDirection.Input);
                param.Add("ToDate", p.ToDate, DbType.String, ParameterDirection.Input);
                param.Add("PageIndex", p.Pi, DbType.Int32, ParameterDirection.Input);
                param.Add("PageSize", p.Ps, DbType.Int32, ParameterDirection.Input);
                return _repo.GetList("BusinessGetNotifications", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PushNotificationEntity> GetPushNotifications(PushNotificationListParamsEntity p)
        {
            try
            {
                DapperRepositry<PushNotificationEntity> _repo = new DapperRepositry<PushNotificationEntity>();
                DynamicParameters param = new DynamicParameters();
                param.Add("AdminId", p.AdminId, DbType.Int32, ParameterDirection.Input);
                param.Add("BusinessId", p.BusinessId, DbType.Int32, ParameterDirection.Input);
                param.Add("FromDate", p.FromDate, DbType.String, ParameterDirection.Input);
                param.Add("ToDate", p.ToDate, DbType.String, ParameterDirection.Input);
                param.Add("PageIndex", p.Pi, DbType.Int32, ParameterDirection.Input);
                param.Add("PageSize", p.Ps, DbType.Int32, ParameterDirection.Input);
                return _repo.GetList("GetPushNotifications", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CustomerEntity> GetCustomerByBusiness(NotificationListParamsEntity p)
        {
            try
            {
                DapperRepositry<CustomerEntity> _repo = new DapperRepositry<CustomerEntity>();
                DynamicParameters param = new DynamicParameters();
                param.Add("BusinessId", p.BusinessId, DbType.Int32, ParameterDirection.Input);
                param.Add("PageIndex", p.Pi, DbType.Int32, ParameterDirection.Input);
                param.Add("PageSize", p.Ps, DbType.Int32, ParameterDirection.Input);
                return _repo.GetList("GetCustomersbyBusiness", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CustomerEntity> GetGuestCheckInByBusiness(NotificationListParamsEntity p)
        {
            try
            {
                DapperRepositry<CustomerEntity> _repo = new DapperRepositry<CustomerEntity>();
                DynamicParameters param = new DynamicParameters();
                param.Add("BusinessId", p.BusinessId, DbType.Int32, ParameterDirection.Input);
                param.Add("PageIndex", p.Pi, DbType.Int32, ParameterDirection.Input);
                param.Add("PageSize", p.Ps, DbType.Int32, ParameterDirection.Input);
                param.Add("Agegroup", p.age, DbType.Int16, ParameterDirection.Input);
                param.Add("Search", p.str, DbType.String, ParameterDirection.Input);
                param.Add("Gender", p.gender, DbType.Int16, ParameterDirection.Input);
                param.Add("GameTypeId", p.gtid, DbType.Int16, ParameterDirection.Input);
                return _repo.GetList("GetGuestCheckinbyBusiness", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CustomerFirstGamePlayed GetCustomerFirstGame(Int32 cid,Int32 bid)
        {
            try
            {
                DapperRepositry<CustomerFirstGamePlayed> _repo = new DapperRepositry<CustomerFirstGamePlayed>();
                DynamicParameters param = new DynamicParameters();
                param.Add("@CustomerId", cid, DbType.Int32, ParameterDirection.Input);
                param.Add("@BusinessId", bid, DbType.Int32, ParameterDirection.Input);

                return _repo.GetResult("GetCustomerFirstGame", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}
