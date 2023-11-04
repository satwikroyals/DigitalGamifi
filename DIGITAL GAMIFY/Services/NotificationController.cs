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
    public class NotificationController : ApiController
    {

        private NotificationManager objbm = new NotificationManager();

        [Route("api/GetNotifications")]
        [HttpGet]
        public List<NotificationEntity> GetNotifications()
        {
            return objbm.GetNotifications();
        }

        [Route("api/AdminGetNotifications")]
        [HttpPost]
        public List<NotificationEntity> AdminGetNotifications(NotificationListParamsEntity p)
        {
            return objbm.AdminGetNotifications(p);
        }
        [Route("api/BusinessGetNotifications")]
        [HttpPost]
        public List<NotificationEntity> BusinessGetNotifications(NotificationListParamsEntity p)
        {
            return objbm.BusinessGetNotifications(p);
        }
        [Route("api/GetPushNotifications")]
        [HttpPost]
        public List<PushNotificationEntity> GetPushNotifications(PushNotificationListParamsEntity p)
        {
            return objbm.GetPushNotifications(p);
        }
        [Route("api/GetNotificationbyCustomer")]
        [HttpGet]
        public List<NotificationEntity> GetNotificationbyCustomer(Int32 cid,Int32 bid)
        {
            return objbm.GetNotificationbyCustomer(cid, bid);
        }
        [Route("api/GetCustomerByBusiness")]
        [HttpPost]
        public List<CustomerEntity> GetCustomerByBusiness(NotificationListParamsEntity p)
        {
            return objbm.GetCustomerByBusiness(p);
        }
        [Route("api/GetGuestCheckInByBusiness")]
        [HttpPost]
        public List<CustomerEntity> GetGuestCheckInByBusiness(NotificationListParamsEntity p)
        {
            return objbm.GetGuestCheckInByBusiness(p);
        }
        [Route("api/GetCustomerFirstGame")]
        [HttpGet]
        public CustomerFirstGamePlayed GetCustomerFirstGame(Int32 cid,Int32 bid)
        {
            return objbm.GetCustomerFirstGame(cid,bid);
        }

    }
}
