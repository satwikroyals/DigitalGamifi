using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DIGITAL_GAMIFY.Entities
{
    public class GameResultsEntity
    {
        public Int32 ResultId { get; set; }
        public Int32 GameId { get; set; }
        public Int32 CustomerId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int PrizeNumber { get; set; }
        public string RedeemCode { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedDatestring { get { return Settings.SetDateTimeFormat(this.CreatedDate); } }
        public int TotalRecords { get; set; }
    }
    public class GameResultListParamsEntity : PagingEntities
    {
        public Int32 AdminId { get; set; }
        public Int32 GameId { get; set; }
        public Int32 BusinessId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Str { get; set; }
    }
    public class SurveyReportResult
    {
        public Int64 SurveyResultId { get; set; }
        public Int64 CustomerId { get; set; }
        public Int64 SurveyId { get; set; }
        public int CorrectAnswerCount { get; set; }
        public int AnsweredCount { get; set; }
        public string SurveyName { get; set; }
        public DateTime StartTime { get; set; }
        public string StartTimeString { get { return Settings.SetDateTimeFormat(this.StartTime); } }
        public DateTime EndTime { get; set; }
        public string EndTimeString { get { return Settings.SetDateTimeFormat(this.StartTime); } }
        public DateTime? CreatedDate { get; set; }
        public string CreateDateString { get { return Settings.SetDateTimeFormat(this.CreatedDate); } }
        public Int64 Duration { get; set; }
        public string DurationString { get { return Settings.ConvertSecondsToHoursFormat(this.Duration); } }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string TotalRecords { get; set; }
    }
    public class SurveyAnswerResult
    {
        public Int64 SurveyResultId { get; set; }
        public Int64 CustomerId { get; set; }
        public Int32 SurveyquestionId { get; set; }
        public Int32 SurveyId { get; set; }
        public string Question { get; set; }
        public string QuestionNum { get; set; }
        public string Answer { get; set; }
        public string TextAnswer { get; set; }
    }
    public class Surveyddl
    {
        public Int64 SurveyId { get; set; }
        public string SurveyName { get; set; }
    }
    public class ReultDetails
    {
        public Int64 ResultId { get; set; }
        public string CustomerName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Size { get; set; }
        public string Colour { get; set; }
        public string Address { get; set; }
    }
    public class Statesddl
    {
        public int StateId { get; set; }
        public string State { get; set; }
        public string Abbreviation { get; set; }
    }
}
