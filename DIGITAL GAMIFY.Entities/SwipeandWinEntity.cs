using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DIGITAL_GAMIFY.Entities
{
    public class SwipeandWinEntity:BusinessEntity
    {
        public Int32 GameId { get; set; }
        [Required(ErrorMessage = "Please enter SwipeandWin title.")]       
        public string Title { get; set; }
        public HttpPostedFile ImageFile { get; set; }
        public string Image { get; set; }
        public string ImagePath { get { return Settings.GetSwipeandWinPrizesImagePath(GameId, Image); } }          
        [Required(ErrorMessage = "Please enter Description.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please enter Conditions.")]
        public string Conditions { get; set; }
        public HttpPostedFile FirstPrizeImageFile { get; set; }
        public string FirstPrizeImage { get; set; }
        public string FirstPrizeImagePath { get { return Settings.GetSwipeandWinPrizesImagePath(GameId, FirstPrizeImage); } }       
        public string FirstPrizeText { get; set; }
        [Required(ErrorMessage = "Please enter first prize count.")]
        public Int32 FirstPrizeCount { get; set; }
        public int FirstPrizeAgeLimit { get; set; }
        public string FirstPrizeCondition { get; set; }
        public HttpPostedFile SecondPrizeImageFile { get; set; }
        public string SecondPrizeImage { get; set; }
        public string SecondPrizeImagePath { get { return Settings.GetSwipeandWinPrizesImagePath(GameId, SecondPrizeImage); } } 
        public string SecondPrizeText { get; set; }
        [Required(ErrorMessage = "Please enter second prize count.")]
        public Int32 SecondPrizeCount { get; set; }
        public int SecondPrizeAgeLimit { get; set; }
        public string SecondPrizeCondition { get; set; }
        public HttpPostedFile ThirdPrizeImageFile { get; set; }
        public string ThirdPrizeImage { get; set; }
        public string ThirdPrizeImagePath { get { return Settings.GetSwipeandWinPrizesImagePath(GameId, ThirdPrizeImage); } }
        public string ThirdPrizeText { get; set; }
        [Required(ErrorMessage = "Please enter third prize count.")]
        public Int32 ThirdPrizeCount { get; set; }
        public int ThirdPrizeAgeLimit { get; set;}
        public string ThirdPrizeCondition { get; set; }
        public Int16 OnceIn { get; set; }
        public string Interval { get; set; }
        public Int32 IntervalId { get; set; }
        public string QRCode { get; set; }
        public string QrcodePath { get { return Settings.GetSwipeandWinQRImagePath(GameId, QRCode); } }
        public DateTime? StartDate { get; set; }
        public string StartDateDisplay { get { return Settings.SetDateFormate(this.StartDate); } }
        public DateTime? EndDate { get; set; }
        public string EndDateDisplay { get { return Settings.SetDateFormate(this.EndDate); } }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateDisplay { get { return Settings.SetDateFormate(this.CreatedDate); } }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedDateDisplay { get { return Settings.SetDateFormate(this.ModifiedDate); } }
        public int IsActive { get; set; }
        public string IsActiveText { get { return Settings.SetStatus(this.IsActive); } }
        public int Type { get { return 1; } }
        public Int32 TotalRecords { get; set; }
        public int IsAgeRequire { get; set; }
        public int IsComplimentary { get; set; }
        public int AgeCondition { get; set; }
        public string AgeConditionstring { get { return AgeCondition == 1 ? "You must be 18 years and above to enter into the contest." : AgeCondition == 2 ? "You must be 21 years and above to enter into the contest." : ""; } }
        public int PhysicalPrize1 { get; set; }
        public string Attributes1 { get; set; }
        public int PhysicalPrize2 { get; set; }
        public string Attributes2 { get; set; }
        public int PhysicalPrize3 { get; set; }
        public string Attributes3 { get; set; }
        public string GameLink { get { return Settings.GetSwipeandWinUrl(this.GameId); } }
        public int FirstPrizesLeft { get; set; }
        public int SecondPrizesLeft { get; set; }
        public int ThirdPrizesLeft { get; set; }
        public int TotalPlayed { get; set; }
    }
   
    public class SwipeandWinListParamsEntity : PagingEntities
    {
        public Int32 AdminId { get; set; }
        public Int32 BusinessId { get; set; }      
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string str { get; set; }
    }


    public class SwipeAndWinGameDetails : SwipeandWinEntity
    {
        public Int64 TotalPlayed { get; set; }
        public Int16 FirstPrizeWinCount { get; set; }
        public Int16 SecondPrizeWinCount { get; set; }
        public Int16 ThirdPrizeWinCount { get; set; }
        public int Finish { get; set; }
        public DateTime? FinishedDate { get; set; }
        public string FinishedDisplay { get { return Settings.SetDateTimeFormat(this.FinishedDate); } }
        public string SwipeImagePath { get { return Settings.GetApplicationFilesPath("packages", "", Image); } }
        public List<GamePrizes> GamePrizes
        {
            get
            {
                return new List<GamePrizes>()
               {new GamePrizes{PrizeImage=Settings.GetApplicationFilesPath("swipeandwin",GameId.ToString(),FirstPrizeImage),PrizeNumber=1,PrizeText=FirstPrizeText},
               new GamePrizes{PrizeImage=Settings.GetApplicationFilesPath("swipeandwin",GameId.ToString(),SecondPrizeImage),PrizeNumber=2,PrizeText=SecondPrizeText},
               new GamePrizes{PrizeImage=Settings.GetApplicationFilesPath("swipeandwin", GameId.ToString(),ThirdPrizeImage),PrizeNumber=3,PrizeText=ThirdPrizeText},
               new GamePrizes{PrizeImage=Settings.websiteurl + "/customercontent/images/avatars-000490611378-ivdfi5-t500x500.jpg",PrizeNumber=0,PrizeText="Sorry you didn’t win. Better luck next time and try again soon!"},
           };
            }
        }
        public string AttributeVales1 { get; set; }
        public string AttributeVales2 { get; set; }
        public string AttributeVales3 { get; set; }
        public Int64 AttributeId { get; set; }
        public string AttributeName { get; set; }
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
        public List<Attributes> Attrlist3
        {
            get
            {
                if (string.IsNullOrEmpty(AttributeVales3))
                {
                    return new List<Attributes>();
                }
                string[] at = AttributeVales3.TrimEnd(']').Split(']');
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

    public class GameResultEntity
    {
        public Int64 ResultId { get; set; }
        public Int64 CustomerId { get; set; }
        public Int64 GameId { get; set; }
        public Int16 PrizeNumber { get; set; }
        public Int32 BusinessId { get; set; }
	    public string Logo { get; set; }
        public string LogoPath { get { return Settings.GetBusinessLogoPath(this.BusinessId, this.Logo); } }
        public string RedeemCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateString { get { return Settings.SetDateTimeFormat(CreatedDate); } }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedDateString { get { return Settings.SetDateTimeFormat(UpdatedDate); } }
        public int Status { get; set; }
        public string StatusText { get { return Status == 0 ? "Pending" : Status == 1 ? "Redeemed" : ""; } }
        
        public string PrizeImage { get; set; }
        public string PrizeImagePath
        {
            get { return Settings.GetApplicationFilesPath("swipeandwin", GameId.ToString(), PrizeImage); }
        }
        public string PrizeText { get; set; }
        public string BusinessName { get; set; }
        public string BusinessAddress { get; set; }
        public string ZipCode { get; set; }
        public string GameConditions { get; set; }
        public int PromotionChecked { get; set; }
        public int Type { get; set; }
        public int Shared { get; set; }
        public string SharedTo { get; set; }
        public string SharedFrom { get; set; }
        //public DateTime? PrizeExpiryDate { get; set; }
        //public string PrizeExpiryDateString { get { return Settings.SetDateFormate(this.PrizeExpiryDate); } }
        public DateTime? DeliverDate { get; set; }
        public string DeliverDateString { get { return Settings.SetDateFormate(this.DeliverDate); } }


    }
    public class GamePrizes
    {
        public Int16 PrizeNumber { get; set; }
        public string PrizeImage { get; set; }
        public string PrizeText { get; set; }
    }

    public class SwipeandWinWinPrizesEntity : SwipeandWinEntity
    {
        public Int16 PrizeNumber { get; set; }
        public DateTime WinDate { get; set; }
        public string RedeemCode { get; set; }
    }

    public class SwipeandWinWinPrizesList 
    {
        public Int32 GameId{get;set;}
        public Int32 BusinessId{get;set;}
        public string Title { get; set; }       
        public string Image { get; set; }
        public string ImagePath { get { return Settings.GetSwipeandWinPrizesImagePath(GameId, Image); } }   
        public Int16 PrizeNumber { get; set; }
        public string PrizeImage { get; set; }
        public string PrizeText { get; set; }
        public string RedeemCode { get; set; }
        public DateTime WinDate { get; set; }
        public string WinDateDisplay { get { return Settings.SetDateTimeFormat(this.WinDate); } }
    }
    public class BusinessGameResultEntity
    {
        public Int64 ResultId { get; set; }
        public Int64 CustomerId { get; set; }
        public Int64 GameId { get; set; }
        public string Title { get; set; }
        public Int16 PrizeNumber { get; set; }
        public Int64 BusinessId { get; set; }
        public string RedeemCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateString { get { return Settings.SetDateTimeFormat(CreatedDate); } }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedDateString { get { return Settings.SetDateTimeFormat(UpdatedDate); } }
        public int Status { get; set; }
        public string StatusText { get { return Status == 0 ? "Pending" : Status == 1 ? "Redeemed" : ""; } }

        public string PrizeImage { get; set; }
        public string PrizeImagePath
        {
            get { return Settings.GetApplicationFilesPath("swipeandwin", GameId.ToString(), PrizeImage); }
        }
        public string PrizeText { get; set; }
        public string CustomerName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string GameConditions { get; set; }
        public string StatusMessage { get; set; }
        public int PromotionChecked { get; set; }
        public int AgeNotAllowed { get; set; }
        public string PrizeCondition { get; set; }
        public int Age { get; set; }
        public string Agestring { get { return Age == 1 ? "10-21" : Age == 2 ? "22-40" : Age == 3 ? "41-65" : Age == 4 ? "65+" : ""; } }
        public DateTime? DeliverDate { get; set; }
        public string DeliverDateString { get { return Settings.SetDateFormate(this.DeliverDate); } }
        public string Size { get; set; }
        public string Colour { get; set; }
        public string Address { get; set; }
        //public DateTime? PrizeExpiryDate { get; set; }
        //public string PrizeExpiryDateString { get { return Settings.SetDateFormate(this.PrizeExpiryDate); } }
        public Int32 TotalRecords { get; set; }

    }
    public class RedeemDetailsEntity
    {
        public List<BusinessGameResultEntity> UnRedeemed { get; set; }
        public List<BusinessGameResultEntity> Redeemed { get; set; }
    }
}
