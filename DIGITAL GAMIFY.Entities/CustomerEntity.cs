using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIGITAL_GAMIFY.Entities
{
    public class ShopfrontalPrizeNotificationEntity
    {
        public Int64 Gameid { get; set; }
        public Int64 BusinessId { get; set; }
        public string Title { get; set; }
        public string RedeemCode { get; set; }
        public string Email { get; set; }
        public Int16 PrizeNumber { get; set; }
        public string PrizePath { get; set; }
        public string GameimgPath { get; set; }
        public string PrizeDetails { get; set; }
        public int Type { get; set; }
        public int LastChance { get; set; }
    }
    public class CustomerEntity
    {
        string _lname;
        public Int64 CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get { return Settings.SetNameFormat(this._lname); } set { _lname = value; } }
        public string Mobile { get; set; }
        public string Pin { get; set; }
        public string Email { get; set; }
        public int Gender { get; set; }
        public string Genderstring { get { return Gender == 1 ? "Male" : Gender == 2 ? "Female" : Gender == 3 ? "Other" : ""; } }
        public int Age { get; set; }
        public string Agestring { get { return Age == 1 ? "10-21" : Age == 2 ? "22-40" : Age == 3 ? "41-65" : Age == 4 ? "65+": ""; } }
        public string ZipCode { get; set; }
        public string DOB { get; set; }
        public string City { get; set; }
        public string StateId { get; set; }
        public string Address { get; set; }
        public Int64 BusinessId { get; set; }
        public int Guest { get; set; }
        public Int64 GameId { get; set; }
        public int GameTypeId { get; set; }
        public string GameTypestring { get { return GameTypeId == 1 ? "Swipe&Win" : GameTypeId == 2 ? "Text Quiz" : GameTypeId == 3 ? "Pic Quiz" : GameTypeId == 4 ? "Survey" : GameTypeId == 5 ? "Sweepstakes" : ""; } }
        public string GameTitle { get; set; }
        public string StreetAddress { get; set; }
        public string AddressLine2 { get; set; }
        public string ReferredBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Int32 TotalRecords { get; set; }
    }
    public class CustomerFirstGamePlayed
    {
        public Int64 CustomerId { get; set; }
        public Int32 GameId { get; set; }
        public string GameName { get; set; }
        public string Game { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
