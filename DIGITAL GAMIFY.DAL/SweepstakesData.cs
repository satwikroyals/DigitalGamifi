using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DbFactory.Repositories;
using DIGITAL_GAMIFY.Entities;
using DbFactory;
using System.Data;
using System.Data.SqlClient;

namespace DIGITAL_GAMIFY.DAL
{
    public class SweepstakesData
    {
        public List<SweepstakesEntity> GetAdminSweepstakesList(paggingEntity pe, Int32 adminid, Int32 bid)
        {
            DapperRepositry<SweepstakesEntity> _repo = new DapperRepositry<SweepstakesEntity>(Settings.ProviederName, Settings.DbConnection);
            DynamicParameters param = new DynamicParameters();
            param.Add("@PageSize", pe.pgsize, DbType.String, ParameterDirection.Input);
            param.Add("@PageIndex", pe.pgindex, DbType.String, ParameterDirection.Input);
            param.Add("@FromDate", pe.FromDate, DbType.String, ParameterDirection.Input);
            param.Add("@ToDate", pe.ToDate, DbType.String, ParameterDirection.Input);
            param.Add("@Search", pe.str, DbType.String, ParameterDirection.Input);
            param.Add("@AdminId", adminid, DbType.Int64, ParameterDirection.Input);
            param.Add("@BusinessId", bid, DbType.Int32, ParameterDirection.Input);

            return _repo.GetList("GetAdminSweepstakesList", param);
        }
        public StatusResponse AddSweepstakes(SweepstakesEntity sqEntity)
        {
            DapperRepositry<StatusResponse> _repo = new DapperRepositry<StatusResponse>(Settings.ProviederName, Settings.DbConnection);
            DynamicParameters param = new DynamicParameters();
            param.Add("@GameId", sqEntity.GameId, DbType.Int32, ParameterDirection.Input);
            param.Add("@AdminId", sqEntity.AdminId, DbType.Int32, ParameterDirection.Input);
            param.Add("@BusinessId", sqEntity.BusinessId, DbType.Int32, ParameterDirection.Input);
            param.Add("@GameName", sqEntity.GameName, DbType.String, ParameterDirection.Input);
            param.Add("@StartDate", sqEntity.StartDate, DbType.DateTime, ParameterDirection.Input);
            param.Add("@EndDate", sqEntity.EndDate, DbType.DateTime, ParameterDirection.Input);
            param.Add("@GameImage", sqEntity.GameImage, DbType.String, ParameterDirection.Input);
            param.Add("@QRCode", sqEntity.QRCode, DbType.String, ParameterDirection.Input);
            param.Add("@Status", sqEntity.Status, DbType.Int16, ParameterDirection.Input);
            param.Add("@ShortDescription", sqEntity.ShortDescription, DbType.String, ParameterDirection.Input);
            param.Add("@IsAgeRequire", sqEntity.IsAgeRequire, DbType.Int16, ParameterDirection.Input);
            param.Add("@Conditions", sqEntity.Conditions, DbType.String, ParameterDirection.Input);
            param.Add("@AgeCondition", sqEntity.AgeCondition, DbType.Int16, ParameterDirection.Input);

            return _repo.GetResult("AddSweepStakesGame", param);
        }
        public SweepstakesEntity DeleteSweepstakes(Int32 gid)
        {
            DapperRepositry<SweepstakesEntity> _repo = new DapperRepositry<SweepstakesEntity>(Settings.ProviederName, Settings.DbConnection);
            DynamicParameters param = new DynamicParameters();
            param.Add("@GameId", gid, DbType.Int32, ParameterDirection.Input);
            return _repo.GetResult("DeleteSweepstakes", param);
        }
        public SweepstakesEntity GetSweepstakesById(Int32 gid)
        {
            DapperRepositry<SweepstakesEntity> _repo = new DapperRepositry<SweepstakesEntity>(Settings.ProviederName, Settings.DbConnection);
            DynamicParameters param = new DynamicParameters();
            param.Add("@GameId", gid, DbType.Int32, ParameterDirection.Input);
            return _repo.GetResult("GetSweepstakesById", param);
        }
        public StatusEntity InsertSweepstakesResult(Int32 gid,Int64 cid)
        {
            DapperRepositry<StatusEntity> _repo = new DapperRepositry<StatusEntity>(Settings.ProviederName, Settings.DbConnection);
            DynamicParameters param = new DynamicParameters();
            param.Add("@GameId", gid, DbType.Int32, ParameterDirection.Input);
            param.Add("@CustomerId", cid, DbType.Int64, ParameterDirection.Input);
            return _repo.GetResult("InsertSweepstakesRessult", param);
        }
        public List<SweepstakesresultEntity> AdminGetSweepstakesresult(paggingEntity p, Int32 bid, Int64 gid)
        {
            try
            {
                DapperRepositry<SweepstakesresultEntity> _repo = new DapperRepositry<SweepstakesresultEntity>(Settings.ProviederName, Settings.DbConnection);
                DynamicParameters param = new DynamicParameters();
                param.Add("@BusinessId", bid, DbType.Int32, ParameterDirection.Input);
                param.Add("@SearchStr", p.str, DbType.String, ParameterDirection.Input);
                param.Add("@PageIndex", p.pgindex, DbType.Int32, ParameterDirection.Input);
                param.Add("@PageSize", p.pgsize, DbType.Int32, ParameterDirection.Input);
                param.Add("@GameId", gid, DbType.Int64, ParameterDirection.Input);
                return _repo.GetList("GetSweepstakesResults", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Quizddl> GetddlSweepstakes(Int32 bid)
        {
            DapperRepositry<Quizddl> _repo = new DapperRepositry<Quizddl>(Settings.ProviederName, Settings.DbConnection);
            DynamicParameters param = new DynamicParameters();

            param.Add("@BusinessId", bid, DbType.Int64, ParameterDirection.Input);

            return _repo.GetList("GetSweepstakesddl", param);
        }
    }
}
