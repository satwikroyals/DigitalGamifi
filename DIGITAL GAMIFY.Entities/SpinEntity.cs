using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DIGITAL_GAMIFY.Entities
{
    public class SpinEntity
    {
        public Int64 SpinGameId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int PrizesCount { get; set; }
    }
    public class SpinPrizeEntity
    {
        public Int64 PrizeId { get; set; }
        public Int64 SpinGameId { get; set; }
        public string Prize { get; set; }
        public string Colour { get; set; }
        public string PrizeNumber { get; set; }
        public string ShortDescription { get; set; }
    }
    public class SpinGameEntity
    {
        public SpinEntity Game { get; set; }
        public List<SpinPrizeEntity> Prizes { get; set; }
    }
}
