using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace DIGITAL_GAMIFY.Entities
{
    public class NotificationEntity:BusinessEntity
    {
        public Int32 AdminId { get; set; }
        public Int32 BusinessId { get; set; }
        public string CustomerIds { get; set; }
        public string CustomerId { get; set; }
        public Int64 NotificationId { get; set; }
       [Required(ErrorMessage = "Please enter Promotion Title.")]
        public string Title { get; set; }
       [Required(ErrorMessage = "Please enter Promotion Text.")]
        public string PromoText { get; set; }
        public string PromoLink { get; set; }
        public string Conditions { get; set; }
        public string Image { get; set; }
        public string ImagePath { get { return Settings.GetNotificationImagePath(this.NotificationId,this.Image); } }
        public string Video { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateDisplay { get { return Settings.SetDateFormate(this.CreatedDate); } }
        public int IsActive { get; set; }
        public string IsActiveText { get { return Settings.SetStatus(this.IsActive); } }

        public Int32 TotalRecords { get; set; }
    }

    public class NotificationListParamsEntity : PagingEntities
    {
        public Int32 AdminId { get; set; }
        public Int32 BusinessId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int age { get; set; }
        public int gender { get; set; }
        public string str { get; set; }
        public int gtid { get; set; }
    }

    public class PushNotificationListParamsEntity : PagingEntities
    {
        public Int32 AdminId { get; set; }
        public Int32 BusinessId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }

    public class PushNotificationEntity : BusinessEntity
    {
        public Int32 AdminId { get; set; }
        public Int32 BusinessId { get; set; }
        public string CustomerIds { get; set; }
        public Int64 NotificationId { get; set; }
        [Required(ErrorMessage = "Please enter title.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter message.")]
        public string Message { get; set; }
        public string Url { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateDisplay { get { return Settings.SetDateFormate(this.CreatedDate); } }     
        public Int32 TotalRecords { get; set; }
    }

}
