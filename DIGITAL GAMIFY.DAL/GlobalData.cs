using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DIGITAL_GAMIFY.DAL.Repositiories;
using DIGITAL_GAMIFY.Entities;
using System.Data;

namespace DIGITAL_GAMIFY.DAL
{
    public class GlobalData
    {

        public List<BusinessTypesEntity> GetBusinessTypes()
        {
            try
            {
                DapperRepositry<BusinessTypesEntity> _repo = new DapperRepositry<BusinessTypesEntity>();
                return _repo.GetList("GetDdlBusinessTypes");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SearchDdlEntities> GetDdlBusiness(Int32 adminId)
        {
            try
            {
                DapperRepositry<SearchDdlEntities> _repo = new DapperRepositry<SearchDdlEntities>();
                DynamicParameters param = new DynamicParameters();
                param.Add("AdminId", adminId, DbType.Int32, ParameterDirection.Input);
                return _repo.GetList("GetDdlBusiness", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public StatusResponse BusinessDeliverprizeactionbtn(Int64 resultid, int type, int action)
        {
            try
            {
                DapperRepositry<StatusResponse> _repo = new DapperRepositry<StatusResponse>();
                DynamicParameters param = new DynamicParameters();
                param.Add("@ResultId", resultid, DbType.Int64, ParameterDirection.Input);
                param.Add("@Type", type, DbType.Int16, ParameterDirection.Input);
                param.Add("@Action", action, DbType.Int16, ParameterDirection.Input);
                return _repo.GetResult("DeliverPrizeActionbtn", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ReultDetails GetResultById(Int64 resultid, int type)
        {
            try
            {
                DapperRepositry<ReultDetails> _repo = new DapperRepositry<ReultDetails>();
                DynamicParameters param = new DynamicParameters();
                param.Add("@ResultId", resultid, DbType.Int64, ParameterDirection.Input);
                param.Add("@Type", type, DbType.Int16, ParameterDirection.Input);
                return _repo.GetResult("GetResultById", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Surveyddl> GetDdlSurveys(Int32 bid)
        {
            try
            {
                DapperRepositry<Surveyddl> _repo = new DapperRepositry<Surveyddl>();
                DynamicParameters param = new DynamicParameters();
                param.Add("@BusinessId", bid, DbType.Int32, ParameterDirection.Input);
                return _repo.GetList("GetSurveysddl", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Attributes> GetAttributesByPrizeTypeId(Int32 ptid)
        {
            DapperRepositry<Attributes> _repo = new DapperRepositry<Attributes>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@PrizeTypeId", ptid, DbType.Int32, ParameterDirection.Input);
            return _repo.GetList("GetAttributesByPrizeTypeId", param);
        }
        public List<Statesddl> GetStateddl()
        {
            DapperRepositry<Statesddl> _repo = new DapperRepositry<Statesddl>();
            DynamicParameters param = new DynamicParameters();
            return _repo.GetList("GetStatesddl", param);
        }
    }
}
