using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DIGITAL_GAMIFY.DAL.Repositiories;
using DIGITAL_GAMIFY.Entities;
using System.Data;
using System.Data.SqlClient;

namespace DIGITAL_GAMIFY.DAL
{
    public class SwipeandWinData
    {

        public Int32 AddUpdateSwipeandWin(SwipeandWinEntity p)
        {
            string connection = Settings.DbConnection;
            try
            {              
                SqlConnection con = new SqlConnection(connection);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("AddUpdateSwipeandWin", con);
               
                cmd.Parameters.AddWithValue("@GameId", p.GameId);
                cmd.Parameters.AddWithValue("@BusinessId", p.BusinessId);
                cmd.Parameters.AddWithValue("@Title", p.Title);
                cmd.Parameters.AddWithValue("@Image", p.Image);
                cmd.Parameters.AddWithValue("@QRCode", p.QRCode);
                cmd.Parameters.AddWithValue("@Description", p.Description);
                cmd.Parameters.AddWithValue("@Conditions", p.Conditions);
                cmd.Parameters.AddWithValue("@FirstPrizeImage", p.FirstPrizeImage);
                cmd.Parameters.AddWithValue("@FirstPrizeText", p.FirstPrizeText);
                cmd.Parameters.AddWithValue("@FirstPrizeCount", p.FirstPrizeCount);
                cmd.Parameters.AddWithValue("@SecondPrizeImage", p.SecondPrizeImage);
                cmd.Parameters.AddWithValue("@SecondPrizeText", p.SecondPrizeText);
                cmd.Parameters.AddWithValue("@SecondPrizeCount", p.SecondPrizeCount);
                cmd.Parameters.AddWithValue("@ThirdPrizeImage", p.ThirdPrizeImage);
                cmd.Parameters.AddWithValue("@ThirdPrizeText", p.ThirdPrizeText);
                cmd.Parameters.AddWithValue("@ThirdPrizeCount", p.ThirdPrizeCount);
                cmd.Parameters.AddWithValue("@OnceIn", p.OnceIn);
                cmd.Parameters.AddWithValue("@Interval", p.Interval);
                cmd.Parameters.AddWithValue("@IntervalId", p.IntervalId);
                cmd.Parameters.AddWithValue("@StartDate", p.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", p.EndDate);
                cmd.Parameters.AddWithValue("@IsActive", p.IsActive);
                cmd.Parameters.AddWithValue("@FirstPrizeAgeLimit", p.FirstPrizeAgeLimit);
                cmd.Parameters.AddWithValue("@FirstPrizeCondition", p.FirstPrizeCondition);
                cmd.Parameters.AddWithValue("@SecondPrizeAgeLimit", p.SecondPrizeAgeLimit);
                cmd.Parameters.AddWithValue("@SecondPrizeCondition", p.SecondPrizeCondition);
                cmd.Parameters.AddWithValue("@ThirdPrizeAgeLimit", p.ThirdPrizeAgeLimit);
                cmd.Parameters.AddWithValue("@ThirdPrizeCondition", p.ThirdPrizeCondition);
                cmd.Parameters.AddWithValue("@IsAgeRequire", p.IsAgeRequire);
                cmd.Parameters.AddWithValue("@IsComplimentary", p.IsComplimentary);
                cmd.Parameters.AddWithValue("@AgeCondition", p.AgeCondition);
                cmd.Parameters.AddWithValue("@PhysicalPrize1", p.PhysicalPrize1);
                cmd.Parameters.AddWithValue("@Attributes1", p.Attributes1);
                cmd.Parameters.AddWithValue("@PhysicalPrize2", p.PhysicalPrize2);
                cmd.Parameters.AddWithValue("@Attributes2", p.Attributes2);
                cmd.Parameters.AddWithValue("@PhysicalPrize3", p.PhysicalPrize3);
                cmd.Parameters.AddWithValue("@Attributes3", p.Attributes3);
                cmd.CommandType = CommandType.StoredProcedure;
                var GameId = cmd.ExecuteScalar();
                cmd.Dispose();                
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return Convert.ToInt32(GameId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public SwipeandWinEntity GetSwipeandWinById(Int64 Id)
        {
            try
            {
                DapperRepositry<SwipeandWinEntity> _repo = new DapperRepositry<SwipeandWinEntity>();
                DynamicParameters param = new DynamicParameters();
                param.Add("GameId", Id, DbType.Int32, ParameterDirection.Input);
                return _repo.GetResult("GetSwipeandWinById", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SwipeandWinEntity> AdminGetSwipeandWin(SwipeandWinListParamsEntity p)
        {
            try
            {
                DapperRepositry<SwipeandWinEntity> _repo = new DapperRepositry<SwipeandWinEntity>();
                DynamicParameters param = new DynamicParameters();
                param.Add("AdminId", p.AdminId, DbType.Int32, ParameterDirection.Input);
                param.Add("BusinessId", p.BusinessId, DbType.Int32, ParameterDirection.Input);             
                param.Add("FromDate", p.FromDate, DbType.String, ParameterDirection.Input);
                param.Add("ToDate", p.ToDate, DbType.String, ParameterDirection.Input);
                param.Add("Search", p.str, DbType.String, ParameterDirection.Input);
                param.Add("PageIndex", p.Pi, DbType.Int32, ParameterDirection.Input);
                param.Add("PageSize", p.Ps, DbType.Int32, ParameterDirection.Input);
                return _repo.GetList("AdminGetSwipeandWin", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SwipeAndWinGameDetails> GetAvailableGames(Int32 bid,Int32 cid)
        {
            try
            {
                DapperRepositry<SwipeAndWinGameDetails> _repo = new DapperRepositry<SwipeAndWinGameDetails>();
                DynamicParameters param = new DynamicParameters();
               
                param.Add("BusinessId", bid, DbType.Int32, ParameterDirection.Input);
                param.Add("CustomerId", cid, DbType.Int32, ParameterDirection.Input);
                return _repo.GetList("customer.GetAvailableGames", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SwipeAndWinGameDetails GetCustGameDetailsById(Int32 bid,Int32 cid)
        {
            try
            {
                DapperRepositry<SwipeAndWinGameDetails> _repo = new DapperRepositry<SwipeAndWinGameDetails>();
                DynamicParameters param = new DynamicParameters();

                param.Add("GameId", bid, DbType.Int32, ParameterDirection.Input);
                param.Add("CustomerId", cid, DbType.Int32, ParameterDirection.Input);
                return _repo.GetResult("customer.GetSwipeGameById", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public StatusEntity RedeemPrize(Int64 cid, Int64 gid, Int16 pznum, string redeemcode, Int16 Promocheck,string Size,string Colour,string Address)
        {
            DapperRepositry<StatusEntity> repo = new DapperRepositry<StatusEntity>();

            var parm = new DynamicParameters();

            parm.Add("@GameId", gid, DbType.Int64, ParameterDirection.Input);
            parm.Add("@CustomerId", cid, DbType.Int64, ParameterDirection.Input);
            parm.Add("@PrizeNumber", pznum, DbType.Int64, ParameterDirection.Input);
            parm.Add("@RedeemCode", redeemcode, DbType.String, ParameterDirection.Input);
            parm.Add("@Promocheck", Promocheck, DbType.Int16, ParameterDirection.Input);
            parm.Add("@Size", Size, DbType.String, ParameterDirection.Input);
            parm.Add("@Colour", Colour, DbType.String, ParameterDirection.Input);
            parm.Add("@Address", Address, DbType.String, ParameterDirection.Input);
            repo.SpName = "customer.RedeemPrize";
            repo.Parameters = parm;
            return repo.FindById();
        }
        public StatusEntity InsertGameFrequency(Int64 Cid, Int64 GameId, Int64 IntervalId, string Interval, int PrizeNumber)
        {
            DapperRepositry<StatusEntity> repo = new DapperRepositry<StatusEntity>();

            var parm = new DynamicParameters();
            parm.Add("@CustomerId", Cid, DbType.Int64, ParameterDirection.Input);
            parm.Add("@GameId", GameId, DbType.Int64, ParameterDirection.Input);
            parm.Add("@IntervalId", IntervalId, DbType.Int32, ParameterDirection.Input);
            parm.Add("@Interval", Interval, DbType.String, ParameterDirection.Input);
            parm.Add("@PrizeNumber", PrizeNumber, DbType.Int16, ParameterDirection.Input);
            repo.SpName = "customer.InsertGameFrequency";
            repo.Parameters = parm;
            return repo.FindById();
        }

        public List<BusinessEntity> GetSwipeandWinClaimedBusiness(Int32 cid)
        {
            try
            {
                DapperRepositry<BusinessEntity> _repo = new DapperRepositry<BusinessEntity>();
                DynamicParameters param = new DynamicParameters();
                param.Add("CustomerId", cid, DbType.Int32, ParameterDirection.Input);
                return _repo.GetList("GetSwipeandWinClaimedBusiness", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<BusinessGameResultEntity> AdminGetSwipeandWinPrizes(paggingEntity p, Int32 bid, int status,Int64 gid,int prize)
        {
            try
            {
                DapperRepositry<BusinessGameResultEntity> _repo = new DapperRepositry<BusinessGameResultEntity>();
                DynamicParameters param = new DynamicParameters();
                param.Add("@BusinessId", bid, DbType.Int32, ParameterDirection.Input);
                param.Add("@SearchStr", p.str, DbType.String, ParameterDirection.Input);
                param.Add("@PageIndex", p.pgindex, DbType.Int32, ParameterDirection.Input);
                param.Add("@PageSize", p.pgsize, DbType.Int32, ParameterDirection.Input);
                param.Add("@Status", status, DbType.Int32, ParameterDirection.Input);
                param.Add("@GameId", gid, DbType.Int64, ParameterDirection.Input);
                param.Add("@PrizeId", prize, DbType.Int16, ParameterDirection.Input);
                return _repo.GetList("GetSwipeandWinPrizesByBusiness", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Quizddl> GetddlGames(Int32 bid)
        {
            DapperRepositry<Quizddl> _repo = new DapperRepositry<Quizddl>();
            DynamicParameters param = new DynamicParameters();

            param.Add("@BusinessId", bid, DbType.Int64, ParameterDirection.Input);

            return _repo.GetList("GetGamesddl", param);
        }
    }
}
