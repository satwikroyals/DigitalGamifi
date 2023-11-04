using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIGITAL_GAMIFY.Entities
{
    public class CommunicationEntity
    {
        public Int64 CommunicationId { get; set; }
        public Int32 BusinessId { get; set; }
        public Int32 FromId { get; set; }
        public Int16 FromType { get; set; }
        public string ToIds { get; set; }
        public Int16 ToType { get; set; }
        public string SessionId { get; set; }
        public string EmailImage { get; set; }
        public string Emailimagepath { get { return Settings.GetCommunicationImage(this.EmailImage); } }
        public string Qrcode { get; set; }
        public string EmailHeading { get; set; }
        public string PageTitle { get; set; }
        public string OpeningTextHeader { get; set; }
        public string Message { get; set; }
        public int CommunicationTypeId { get; set; }
        public string CommunicationType { get; set; }
        public Int16 MarketingTypeId { get; set; }
        public Int64 MarketingItemId { get; set; }
        public Int64 ReceipentCount { get; set; }
        public Int64 TemplateId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDatestring { get { return Settings.SetDateFormate(this.CreatedDate); } }
        public string LiveStream { get; set; }
        public int IsLiveStream { get; set; }
        public int IsCommericial { get; set; }
        public string CreatedDateString { get { return Settings.SetDateFormate(CreatedDate); } }
        public Int32 TotalRecords { get; set; }
    }
    public class UsersLists
    {
        public string ci;
        public string rcpt;
        public string name;
        public string navurl;
        public string ressession;
    }
}
