using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DIGITAL_GAMIFY.Entities
{
    public class QuizEntity : BusinessEntity
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
        public Int32 QuizId { get; set; }
        public string QuizName { get { return Settings.SetNameFormat(this._smqname); } set { _smqname = value; } }
        public Int32 QuizQuestionId { get; set; }
        public Int32 QuizAnswerId { get; set; }
        public string QuizCode { get { return Settings.SetNameFormat(_smquizcode); } set { _smquizcode = value; } }
        public DateTime? StartDate { get; set; }
        public string StartDatestring { get { return Settings.SetDateFormate(this.StartDate); } }
        public DateTime? EndDate { get; set; }
        public string EndDatestring { get { return Settings.SetDateFormate(this.EndDate); } }
        public string SmsCode { get { return Settings.SetNameFormat(this._smscode); } set { _smscode = value; } }
        public string QuizImage { get; set; }
        public string QuizImagepath { get { return Settings.getQuizImage(this.QuizId, this.QuizImage); } }
        public string QRCode { get { return Settings.SetNameFormat(this._qrcode); } set { _qrcode = value; } }
        public string QrcodePath { get { return Settings.getQuizImage(QuizId, QRCode); } }
        public int IsReferFriend { get; set; }
        public string Question { get { return Settings.SetNameFormat(_question); } set { _question = value; } }
        public int Status { get; set; }
        public string ShortDescription { get { return Settings.SetNameFormat(_shortdesc); } set { _shortdesc = value; } }
        public string Answer { get { return Settings.SetNameFormat(this._answer); } set { _answer = value; } }
        public string QandAValues { get { return Settings.SetNameFormat(this._qandqvalues); } set { _qandqvalues = value; } }
        public DateTime CreatedDate { get; set; }
        public string CreatedDatestring { get { return Settings.SetDateFormate(this.CreatedDate); } }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedDatestring { get { return Settings.SetDateFormate(this.ModifiedDate); } }
        public int IsAgeRequire { get; set; }
        public string Conditions { get; set; }
        public int IsComplimentary { get; set; }
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
        public Int64 Duration { get; set; }
        public string DurationString { get { return Settings.ConvertsMilliSecondsToHoursFormat(this.Duration); } }
        public int PrizeId { get; set; }
        public string FirstPrizeImage { get; set; }
        public string FirstPrizeText { get; set; }
        public int FirstPrizeCount { get; set; }
        public int SecondPrizeCount { get; set; }
        public string SecondPrizeImage { get; set; }
        public string SecondPrizeText { get; set; }
        public int FirstPrizeWinCount { get; set; }
        public int SecondPrizeWinCount { get; set; }
        public Int16 OnceIn { get; set; }
        public string Interval { get; set; }
        public Int32 IntervalId { get; set; }
        public int Type { get { return 2; } }
        public int TotalPlayed { get; set; }
        public int CorrectAnsweredCount { get; set; }
        public int LastChance { get; set; }
        public List<GamePrizes> GamePrizes
        {
            get
            {
                return new List<GamePrizes>()
               {new GamePrizes{PrizeImage=Settings.GetApplicationFilesPath("quizgameprizes",QuizId.ToString(),FirstPrizeImage),PrizeNumber=1,PrizeText=FirstPrizeText},
               new GamePrizes{PrizeImage=Settings.GetApplicationFilesPath("quizgameprizes",QuizId.ToString(),SecondPrizeImage),PrizeNumber=2,PrizeText=SecondPrizeText},
               new GamePrizes{PrizeImage=Settings.websiteurl + "/customercontent/images/avatars-000490611378-ivdfi5-t500x500.jpg",PrizeNumber=3,PrizeText="Great try...but sorry you weren’t selected as a winner in our instant drawing. Try After 24 hours!"},
               new GamePrizes{PrizeImage="",PrizeNumber=0,PrizeText="Sorry you didn’t win. Better luck next time and try again soon!"},
               
           };
            }
        }
        public int PhysicalPrize1 { get; set; }
        public string Attributes1 { get; set; }
        public int PhysicalPrize2 { get; set; }
        public string Attributes2 { get; set; }
        public string GameLink { get { return Settings.GetQuizUrl(this.QuizId); } }
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
    public class QuizCustomerQuestion
    {
        public Int32 QuizQuestionId { get; set; }
        public Int32 QuizId { get; set; }
        public string Question { get; set; }
        public Int32 QuestionNum { get; set; }
        public Int32 CorrectAnswerId { get; set; }
        public int IsActive { get; set; }
        public Int32 Correctanswer { get; set; }
        //   public DateTime CreatedDate { get; set; }
        public int IsquestionAvailable { get; set; }
        public List<QuizAnswers> answers { get; set; }
    }
    public class QuizAnswers
    {
        public Int32 QuizAnswerId { get; set; }
        public Int32 QuizQuestionId { get; set; }
        public Int32 QuestionNumber { get; set; }
        public Int32 QuizId { get; set; }
        public Int32 AnswerNumber { get; set; }
        public Int32 QestionNumber { get; set; }
        public string Answer { get; set; }
        //  public DateTime CreatedDate { get; set; }
    }

    public class QuizQuestionAndAnswer
    {
        public QuizEntity QuizDetails { get; set; }
        public List<QuizQuestions> Question { get; set; }
    }
    public class QuizQuestions
    {
        public Int32 QuizQuestionId { get; set; }
        public Int32 QuizId { get; set; }
        public Int32 QuestionId { get; set; }
        public string Question { get; set; }
        public int QuestionNum { get; set; }
        public Int32 CorrectAnswerId { get; set; }
        public int IsActive { get; set; }
        //public Int32 Correctanswer { get; set; }
        //  public DateTime CreatedDate { get; set; }
        public List<QuizAnswers> answers { get; set; }
    }

    public class quizResultEntity
    {
        public Int32 QuizResultId { get; set; }
        public Int32 QuizId { get; set; }
        public int AnsweredCount { get; set; }
        public int CorrectAnswerCount { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string EndTimestring { get { return Settings.SetDateTimeFormat(this.EndTime); } }
        public string CustomerName { get; set; }
        public string Mobile { get; set; }
        public string Country { get; set; }
        public string QuizName { get; set; }
        public Int64 Duration { get; set; }
        public string DurationString { get { return Settings.ConvertsMilliSecondsToHoursFormat(this.Duration); } }
        public Int32 TotalRecords { get; set; }
        public Int64 Rank { get; set; }
        public int IsSelf { get; set; }
    }
    public class QuizPrizesEntity : BusinessEntity
    {
        public Int32 QuizResultId { get; set; }
        public Int32 ResultId { get; set; }
        public int PrizeId { get; set; }
        public Int32 QuizId { get; set; }
        public string QuizName { get; set; }
        public string PrizeImage { get; set; }
        public string PrizeImagePath
        {
            get { return Settings.GetApplicationFilesPath("quizgameprizes", QuizId.ToString(), PrizeImage); }
        }
        public string PrizeText { get; set; }
        public string CustomerName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string RedeemCode { get; set; }
        public string Address { get; set; }
        public int Type { get; set; }
        public int Shared { get; set; }
        public string SharedTo { get; set; }
        public string Size { get; set; }
        public string Colour { get; set; }
        public string SharedFrom { get; set; }
        public int Status { get; set; }
        public string StatusText { get { return Status == 0 ? "Pending" : Status == 1 ? "Redeemed" : ""; } }
        //public DateTime? PrizeExpiryDate { get; set; }
        //public string PrizeExpiryDateString { get { return Settings.SetDateFormate(this.PrizeExpiryDate); } }
        public DateTime? DeliverDate { get; set; }
        public string DeliverDateString { get { return Settings.SetDateFormate(this.DeliverDate); } }
        public Int32 TotalRecords { get; set; }
    }
}
