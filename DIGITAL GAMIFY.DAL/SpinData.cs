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
    public class SpinData
    {
        public SpinGameEntity getSpinById(Int64 spid, Int64 cid)
        {
            DbFactory.DbSettings _db = new DbFactory.DbSettings(Settings.ProviederName, Settings.DbConnection);
            SpinGameEntity _repo = new SpinGameEntity();
            List<SpinPrizeEntity> sq = new List<SpinPrizeEntity>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@SpinGameId", spid, DbType.Int32, ParameterDirection.Input);
            param.Add("@CustomerId", cid, DbType.Int32, ParameterDirection.Input);
            using (IDbConnection db = (IDbConnection)_db.ConnectionString)
            {
                var result = db.QueryMultiple("GetSpinGameByCustomer", commandType: CommandType.StoredProcedure, param: param);
                _repo.Game = result.Read<SpinEntity>().FirstOrDefault();
                sq = result.Read<SpinPrizeEntity>().ToList();
                _repo.Prizes = sq;
            }
            return _repo;
        }
    }
}
