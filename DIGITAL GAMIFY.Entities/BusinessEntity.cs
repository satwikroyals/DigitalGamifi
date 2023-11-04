using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DIGITAL_GAMIFY.Entities
{

    public class BusinessLoginEntities
    {

        [Required(ErrorMessage = "Please enter UserName.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter Password.")]
        public string Password { get; set; }

    }
    public class StatusResponse
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
    }
    public class BusinessEntity
    {
        private string firstName, lastName = string.Empty;

        public Int32 AdminId { get; set; }
        public Int32 BusinessId { get; set; }
        [Required(ErrorMessage = "Please enter Business Name.")]
        public string BusinessName { get; set; }
        [DdlValidator(ErrorMessage = "Please select Business Type.")]
        public int BusinessTypeId { get; set; }
        public string BusinessType { get; set; }
        [Required(ErrorMessage = "Please enter FirstName.")]
        public string FirstName { get { return this.firstName; } set { this.firstName = Settings.SetFont(value); } }
        [Required(ErrorMessage = "Please enter LastName.")]
        public string LastName { get { return this.lastName; } set { this.lastName = Settings.SetFont(value); } }
        public string FullName { get { return this.FirstName + " " + this.LastName; } }
        [Required(ErrorMessage = "Please enter Email."), EmailAddress(ErrorMessage = "Please enter Valid EmailId.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter Mobile.")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Please enter Username.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter Password.")]
        public string Password { get; set; }
        public HttpPostedFile LogoFile { get; set; }
        // [Required(ErrorMessage = "Please upload Business Logo.",AllowEmptyStrings=false)]
        public string Logo { get; set; }
        public string LogoPath { get { return Settings.GetBusinessLogoPath(this.BusinessId, this.Logo); } }
        public string QR { get; set; }
        public string QRPath { get { return Settings.GetBusinessQRPath(BusinessId, QR); } }
        [Required(ErrorMessage = "Please enter zipcode.")]
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "Please enter Address.")]
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int IsActive { get; set; }
        public string IsActiveText { get { return Settings.SetStatus(this.IsActive); } }
        public HttpPostedFile PrizeImageFile { get; set; }
        public string PrizeImage { get; set; }
        public string PrizeImagePath { get { return Settings.GetSwipeandWin3rdPrizeImagePath(BusinessId, PrizeImage); } }
        public int PrizeCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateDisplay { get { return Settings.SetDateTimeFormat(this.CreatedDate); } }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedDateDisplay { get { return Settings.SetDateTimeFormat(this.ModifiedDate); } }
        public Int32 TotalRecords { get; set; }
        public string BusinessLogoPath { get { return Settings.GetApplicationFilesPath("business", BusinessId.ToString(), Logo); } }
        public int favourite { get; set; }
        public string imgfile { get; set; }
        public DateTime ClaimedDate { get; set; }
        public string ClaimedDateDisplay { get { return Settings.SetDateTimeFormat(this.ClaimedDate); } }

    }

    public class BusinessListParamsEntity : PagingEntities
    {
        public Int32 AdminId { get; set; }
        public Int32 BusinessId { get; set; }
        public Int32 BusinessTypeId { get; set; }
        public string str { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }

    public class BusinessTypesEntity
    {
        public Int32 BusinessTypeId { get; set; }
        public string BusinessType { get; set; }

    }
    public class DashboardEntity
    {
        public string TotalCustomers { get; set; }
        public string TotalGuest { get; set; }
        public string TotalGames { get; set; }
        public string TotalActiveGames { get; set; }
        public string TotalNotifications { get; set; }
        public string ServiceProviders { get; set; }
        public string CurrentJobs { get; set; }
        public string TotalQuizGames { get; set; }
        public string TotalActiveQuizGames { get; set; }
        public string TotalSmartQuizGames { get; set; }
        public string TotalActiveSmartQuizGames { get; set; }
        public string TotalSurveys { get; set; }
        public string TotalActiveSurvays { get; set; }
        public string ActiveServiceProviders { get; set; }
        public double TotalFund { get; set; }
    }
    public class Attributes
    {
        public Int64 AttributeId { get; set; }
        public int PrizeTypeId { get; set; }
        public string Attribute { get; set; }
        public string AttributeName { get; set; }
        public string[] values { get; set; }
    }

}
