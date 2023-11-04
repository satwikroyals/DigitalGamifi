using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIGITAL_GAMIFY.Entities;
using DIGITAL_GAMIFY.DAL;


namespace DIGITAL_GAMIFY.BAL
{
    public class NotificationManager
    {
        private NotificationData data = new NotificationData();

        public List<NotificationEntity> GetNotifications()
        {
            return data.GetNotifications();
        }
        public List<NotificationEntity> GetNotificationbyCustomer(Int32 cid,Int32 bid)
        {
            return data.GetNotificationbyCustomer(cid, bid);
        }

        public List<NotificationEntity> AdminGetNotifications(NotificationListParamsEntity p)
        {
            return data.AdminGetNotifications(p);
        }

        public NotificationEntity GetNotificationById(Int64 businessId)
        {
            return data.GetNotificationById(businessId);
        }

        public PushNotificationEntity GetPushNotificationById(Int64 notificationid)
        {
            return data.GetPushNotificationById(notificationid);
        }

        public NotificationEntity AddNotification(NotificationEntity p)
        {
            return data.AddNotification(p);
        }
        public PushNotificationEntity AddPushNotification(PushNotificationEntity p)
        {
            return data.AddPushNotification(p);
        }
        public List<NotificationEntity> BusinessGetNotifications(NotificationListParamsEntity p)
        {
            return data.BusinessGetNotifications(p);
        }
        public List<PushNotificationEntity> GetPushNotifications(PushNotificationListParamsEntity p)
        {
            return data.GetPushNotifications(p);
        }
        public List<CustomerEntity> GetCustomerByBusiness(NotificationListParamsEntity p)
        {
            return data.GetCustomerByBusiness(p);
        }
        public List<CustomerEntity> GetGuestCheckInByBusiness(NotificationListParamsEntity p)
        {
            return data.GetGuestCheckInByBusiness(p);
        }
        public CustomerFirstGamePlayed GetCustomerFirstGame(Int32 cid,Int32 bid)
        {
            return data.GetCustomerFirstGame(cid,bid);
        }

    }
}
