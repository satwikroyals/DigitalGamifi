using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DIGITAL_GAMIFY.Entities
{
    public class SweepstakesEntity : BusinessEntity
    {
        #region private variables
        string _smqname;
        string _smscode;
        string _qrcode;
        string _question;
        string _answer;
        string _qandqvalues;
        string _smquizcode;
        string _shortdesc;
        #endregion
        public Int32 GameId { get; set; }
        public string GameName { get { return Settings.SetNameFormat(this._smqname); } set { _smqname = value; } }
        public DateTime? StartDate { get; set; }
        public string StartDatestring { get { return Settings.SetDateFormate(this.StartDate); } }
        public DateTime? EndDate { get; set; }
        public string EndDatestring { get { return Settings.SetDateFormate(this.EndDate); } }
        public string SmsCode { get { return Settings.SetNameFormat(this._smscode); } set { _smscode = value; } }
        public string GameImage { get; set; }
        public string GameImagepath { get { return Settings.getSweepstakeImage(this.GameId, this.GameImage); } }
        public string QRCode { get { return Settings.SetNameFormat(this._qrcode); } set { _qrcode = value; } }
        public string QrcodePath { get { return Settings.getSweepstakeImage(GameId, QRCode); } }
        public int Status { get; set; }
        public string ShortDescription { get { return Settings.SetNameFormat(_shortdesc); } set { _shortdesc = value; } }
        public DateTime CreatedDate { get; set; }
        public string CreatedDatestring { get { return Settings.SetDateFormate(this.CreatedDate); } }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedDatestring { get { return Settings.SetDateFormate(this.ModifiedDate); } }
        public int IsAgeRequire { get; set; }
        public string Conditions { get; set; }
        public int AgeCondition { get; set; }
        public string AgeConditionstring { get { return AgeCondition == 1 ? "You must be 18 years and above to enter into the contest." : AgeCondition == 2 ? "You must be 21 years and above to enter into the contest." : ""; } }
        public Int64 TotalRecords { get; set; }
        public Int64 StartedIn { get; set; }
        public Int64 EndedIn { get; set; }
        public int GameStatus
        {
            get
            {
                return StartedIn > 0 ? 1 : EndedIn > 0 ? 2 : 3;
            }
        }
        public string StartedInText
        {
            get { return StartedIn > 0 ? "" : Settings.ConvertSecondsToHoursFormat(StartedIn); }
        }
        public string EndedInText { get { return EndedIn > 0 ? "" : Settings.ConvertSecondsToHoursFormat(EndedIn); } }
        public int IsFinished { get; set; }
        public DateTime EndTime { get; set; }
        public string EndTimestring { get { return Settings.SetDateTimeFormat(this.EndTime); } }
        public string GameLink { get { return Settings.GetSweepstakesUrl(this.GameId); } }
        public int Type { get { return 5; } }
        public int TotalPlayed { get; set; }
    }
    public class SweepstakesresultEntity : BusinessEntity
    {
        public Int32 SweepstakesResultId { get; set; }
        public Int32 GameId { get; set; }
        public string GameName { get; set; }
        public string CustomerName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string StateId { get; set; }
        public string Address { get; set; }
        public string AddressLine2 { get; set; }
        public string Zipcode { get; set; }
        public string ReferredBy { get; set; }
        public int Gender { get; set; }
        public string Genderstring { get { return Gender == 1 ? "Male" : Gender == 2 ? "Female" : Gender == 3 ? "Other" : ""; } }
        public int Age { get; set; }
        public string Agestring { get { return Age == 1 ? "10-21" : Age == 2 ? "22-40" : Age == 3 ? "41-65" : Age == 4 ? "65+" : ""; } }
        public int Type { get; set; }
        public Int32 TotalRecords { get; set; }
    }
}
