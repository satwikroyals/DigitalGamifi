using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DIGITAL_GAMIFY.Entities
{
    public class SurveyEntity : BusinessEntity
    {
        string _surveyname;
        string _surveycode;
        string _smscode;
        string _question;
        string _answer;
        public Int32 SurveyId { get; set; }
        //public Int32 BusinessId { get; set; }
        public Int32 SurveyquestionId { get; set; }
        public Int32 SurveyanswerId { get; set; }
        public string SurveyName { get { return Settings.SetNameFormat(this._surveyname); } set { _surveyname = value; } }
        public string SurveyCode { get { return Settings.SetNameFormat(this._surveycode); } set { _surveycode = value; } }
        public DateTime? StartDate { get; set; }
        public string StartDatestring { get { return Settings.SetDateFormate(this.StartDate); } }
        public DateTime? EndDate { get; set; }
        public string EndDatestring { get { return Settings.SetDateFormate(this.EndDate); } }
        public string SmsCode { get { return Settings.SetNameFormat(this._smscode); } set { _smscode = value; } }
        public string Surveyimage { get; set; }
        public string Surveyimagepath { get { return Settings.getsurveyimage(this.SurveyId, this.Surveyimage); } }
        public string QRCode { get; set; }
        public string QrcodePath { get { return Settings.getsurveyimage(SurveyId, QRCode); } }
        public int IsReferFriend { get; set; }
        public string Question { get { return Settings.SetNameFormat(this._question); } set { _question = value; } }
        public int Status { get; set; }
        public int IsFinished { get; set; }
        public DateTime EndTime { get; set; }
        public string EndTimestring { get { return Settings.SetDateTimeFormat(this.EndTime); } }
        public Int64 Duration { get; set; }
        public string DurationString { get { return Settings.ConvertsMilliSecondsToHoursFormat(this.Duration); } }
        public string Answer { get { return Settings.SetNameFormat(this._answer); } set { _answer = value; } }
        public string QandAValues { get; set; }
        public string SurveyquestionIds { get; set; }
        public int Generalpublic { get; set; }
        public int Selectmember { get; set; }
        public string SurveyanswerIds { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDatestring { get { return Settings.SetDateFormate(this.CreatedDate); } }
        public string TotalRecords { get; set; }
        public string ShortDescription { get; set; }
        public int PrizeId { get; set; }
        public string FirstPrizeImage { get; set; }
        public string FirstPrizeText { get; set; }
        public int FirstPrizeCount { get; set; }
        public int SecondPrizeCount { get; set; }
        public string SecondPrizeImage { get; set; }
        public string SecondPrizeText { get; set; }
        public int IsAgeRequire { get; set; }
        public string Conditions { get; set; }
        public int IsComplimentary { get; set; }
        public int AgeCondition { get; set; }
        public int FirstPrizeWinCount { get; set; }
        public int SecondPrizeWinCount { get; set; }
        public Int16 OnceIn { get; set; }
        public string Interval { get; set; }
        public Int32 IntervalId { get; set; }
        public int TotalPlayed { get; set; }
        public string AgeConditionstring { get { return AgeCondition == 1 ? "You must be 18 years and above to enter into the contest." : AgeCondition == 2 ? "You must be 21 years and above to enter into the contest." : ""; } }
        public int Type { get { return 4; } }
        public List<GamePrizes> GamePrizes
        {
            get
            {
                return new List<GamePrizes>()
               {new GamePrizes{PrizeImage=Settings.GetApplicationFilesPath("surveyprizes",SurveyId.ToString(),FirstPrizeImage),PrizeNumber=1,PrizeText=FirstPrizeText},
               new GamePrizes{PrizeImage=Settings.GetApplicationFilesPath("surveyprizes",SurveyId.ToString(),SecondPrizeImage),PrizeNumber=2,PrizeText=SecondPrizeText},
               new GamePrizes{PrizeImage=Settings.websiteurl + "/customercontent/images/avatars-000490611378-ivdfi5-t500x500.jpg",PrizeNumber=3,PrizeText="Sorry you didn’t win. Better luck next time and try again soon!"},
               new GamePrizes{PrizeImage=Settings.websiteurl + "/customercontent/images/avatars-000490611378-ivdfi5-t500x500.jpg",PrizeNumber=0,PrizeText="Sorry you didn’t win. Better luck next time and try again soon!"},
           };
            }
        }
        public int AnsweredCount { get; set; }
        public int QuestionCount { get; set; }
        public int PhysicalPrize1 { get; set; }
        public string Attributes1 { get; set; }
        public int PhysicalPrize2 { get; set; }
        public string Attributes2 { get; set; }
        public string GameLink { get { return Settings.GetSurveyUrl(this.SurveyId); } }
        public string AttributeVales1 { get; set; }
        public string AttributeVales2 { get; set; }
        public Int64 AttributeId { get; set; }
        public string AttributeName { get; set; }
        public int CorrectAnswerCount { get; set; }
        public int FirstPrizesLeft { get; set; }
        public int SecondPrizesLeft { get; set; }
        public List<Attributes> Attrlist
        {
            get
            {
                if (string.IsNullOrEmpty(AttributeVales1))
                {
                    return new List<Attributes>();
                }
                string[] at = AttributeVales1.TrimEnd(']').Split(']');
                List<Attributes> al = new List<Attributes>();
                foreach (string x in at)
                {
                    Attributes a = new Attributes();
                    a.AttributeId = Convert.ToInt64(x.Split('_')[0]);
                    a.Attribute = x.Split('_')[1].Split('[')[0];
                    a.values = x.Split('_')[1].Split('[')[1].TrimEnd(';').TrimStart(';').Split(';');
                    al.Add(a);
                }

                return al;
            }
        }
        public List<Attributes> Attrlist2
        {
            get
            {
                if (string.IsNullOrEmpty(AttributeVales2))
                {
                    return new List<Attributes>();
                }
                string[] at = AttributeVales2.TrimEnd(']').Split(']');
                List<Attributes> al = new List<Attributes>();
                foreach (string x in at)
                {
                    Attributes a = new Attributes();
                    a.AttributeId = Convert.ToInt64(x.Split('_')[0]);
                    a.Attribute = x.Split('_')[1].Split('[')[0];
                    a.values = x.Split('_')[1].Split('[')[1].TrimEnd(';').TrimStart(';').Split(';');
                    al.Add(a);
                }

                return al;
            }
        }
    }
    public class SurveyQuestions
    {
        public Int32 SurveyquestionId { get; set; }
        public Int32 questionId { get; set; }
        public Int32 SurveyId { get; set; }
        public string Question { get; set; }
        public string QuestionNum { get; set; }
        public Int32 CorrectAnswerId { get; set; }
        public int IsActive { get; set; }
        public int IsTextField { get; set; }
        public Int32 Correctanswer { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<SurveyAnswers> answers { get; set; }
    }
    public class SurveyAnswers
    {
        public Int32 SurveyanswerId { get; set; }
        public Int32 SurveyquestionId { get; set; }
        public string Answer { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class SurveyQuestionandAnswer
    {
        public SurveyEntity SurveyDetails { get; set; }
        public List<SurveyQuestions> question { get; set; }
        //public List<SurveyAnswers> answers { get; set; }
    }
    public class SurveyCustomerQuestion
    {
        public Int32 SurveyquestionId { get; set; }
        public Int32 SurveyId { get; set; }
        public string Question { get; set; }
        public int IsFinished { get; set; }
        public string QuestionNum { get; set; }
        public Int32 CorrectAnswerId { get; set; }
        public int IsActive { get; set; }
		public int IsTextField { get; set; }
        public Int32 Correctanswer { get; set; }
        public DateTime CreatedDate { get; set; }
        public int IsquestionAvailable { get; set; }
        public List<SurveyAnswers> answers { get; set; }
    }
    public class SurveyResult:BusinessEntity
    {
        public Int64 SurveyResultId { get; set; }
        public Int32 ResultId { get; set; }
        public Int64 CustomerId { get; set; }
        public Int32 SurveyquestionId { get; set; }
        public Int32 SurveyId { get; set; }
        public string SurveyName { get; set; }
        public string Question { get; set; }
        public string QuestionNum { get; set; }
        public string Answer { get; set; }
        public string PrizeImage { get; set; }
        public string PrizeImagePath
        {
            get { return Settings.GetApplicationFilesPath("surveyprizes", SurveyId.ToString(), PrizeImage); }
        }
        public string PrizeText { get; set; }
        public string CustomerName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string RedeemCode { get; set; }
        public int Type { get; set; }
        public int Shared { get; set; }
        public string SharedTo { get; set; }
        public string Size { get; set; }
        public string Colour { get; set; }
        public int Status { get; set; }
        public string StatusText { get { return Status == 0 ? "Pending" : Status == 1 ? "Redeemed" : ""; } }
        public string SharedFrom { get; set; }
        //public DateTime? PrizeExpiryDate { get; set; }
        //public string PrizeExpiryDateString { get { return Settings.SetDateFormate(this.PrizeExpiryDate); } }
        public DateTime? DeliverDate { get; set; }
        public string DeliverDateString { get { return Settings.SetDateFormate(this.DeliverDate); } }
        public Int32 TotalRecords { get; set; }
    }
}
