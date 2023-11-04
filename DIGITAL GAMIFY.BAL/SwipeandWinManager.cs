using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIGITAL_GAMIFY.Entities;
using DIGITAL_GAMIFY.DAL;

namespace DIGITAL_GAMIFY.BAL
{
    public class SwipeandWinManager
    {
        private SwipeandWinData data = new SwipeandWinData();

        public Int32 AddUpdateSwipeandWin(SwipeandWinEntity p)
        {
           return data.AddUpdateSwipeandWin(p);
        }

        public SwipeandWinEntity GetSwipeandWinById(Int64 Id)
        {
            return data.GetSwipeandWinById(Id);
        }

        public List<SwipeandWinEntity> AdminGetSwipeandWin(SwipeandWinListParamsEntity p)
        {
            return data.AdminGetSwipeandWin(p);
        }

        public List<SwipeAndWinGameDetails> GetAvailableGames(Int32 bid,Int32 cid)
        {
            return data.GetAvailableGames(bid,cid);
        }
        public object GetSwipePlayGameDetails(Int32 GameId, Int32 CId)
        {
            // return _budata.GetGameById(Id,CId);
            SwipeAndWinGameDetails ge = new SwipeAndWinGameDetails();
            StatusEntity se = new StatusEntity();
            //  GameResult gr = new GameResult();
            ge = data.GetCustGameDetailsById(GameId, CId);
            if (ge.Finish==0)
            {
                Int64 EntryNo = ge.TotalPlayed + 1;
                int PrizeId = 0;
                Int64 IntervalId = ge.IntervalId;
                string Interval = ge.Interval;
                Int16 OnceIn = ge.OnceIn;
                if (EntryNo == ge.IntervalId)
                {
                    PrizeId = Settings.getPrizeNumber(ge.FirstPrizeCount, ge.SecondPrizeCount, ge.ThirdPrizeCount, ge.FirstPrizeWinCount, ge.SecondPrizeWinCount, ge.ThirdPrizeWinCount);
                    string[] inrl = ge.Interval.Split('-');
                    int first = Convert.ToInt32(inrl[1]) + 1;
                    //decimal val = Convert.ToDecimal(BrandGameDetails.TotalEntries / BrandGameDetails.OnceIn);
                    int last = Convert.ToInt32(inrl[1]) + OnceIn;
                    int rand = new Random().Next(first, last);
                    IntervalId = rand;
                    Interval = first.ToString() + "-" + last.ToString();
                }
                else
                {
                    PrizeId = 0;
                }
                var winprizedetails = ge.GamePrizes.Where(m => m.PrizeNumber == PrizeId).FirstOrDefault();
                if (ge.Finish != 1)
                {
                    se = data.InsertGameFrequency(CId, GameId, IntervalId, Interval, PrizeId);
                }

                object obj = new object();
                obj = new
                {
                    GameDetails = ge,
                    PrizeDetails = new
                    {
                        PrizeNumber = winprizedetails.PrizeNumber,
                        PrizeMessage = winprizedetails.PrizeText,
                        PrizePath = winprizedetails.PrizeImage
                    }
                };
                return obj;
            }
            else
            {
                object obj = new object();
                obj = new
                {
                    GameDetails = ge,
                    PrizeDetails = new
                    {
                        PrizeNumber = 0,
                        PrizeMessage = "",
                        PrizePath = ""
                    }
                };
                return obj;
            }
        }

        public StatusEntity RedeemPrize(Int64 cid, Int64 gid, Int16 pznum, string redeemcode, Int16 Promocheck, string Size, string Colour,string Address)
        {
            return data.RedeemPrize(cid, gid, pznum, redeemcode,Promocheck,Size,Colour,Address);
        }
        public SwipeAndWinGameDetails GetGamedetailsById(Int32 gid,Int32 cid)
        {
          return  data.GetCustGameDetailsById(gid, cid);
        }
        public List<BusinessGameResultEntity> AdminGetSwipeandWinPrizes(paggingEntity p, Int32 bid, int status,Int64 gid,int prize)
        {
            return data.AdminGetSwipeandWinPrizes(p, bid, status,gid,prize);
        }
        public List<Quizddl> GetddlGames(Int32 bid)
        {
            return data.GetddlGames(bid);
        }
    }
}
