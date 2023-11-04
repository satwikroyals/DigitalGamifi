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
    public class CommunicationData
    {
        public List<CustomerEntity> GetCommunicationSelectedMembers(string ids)
        {
            DapperRepositry<CustomerEntity> _repo = new DapperRepositry<CustomerEntity>();
            DynamicParameters param = new DynamicParameters();

            param.Add("@Ids", ids, DbType.String, ParameterDirection.Input);
            return _repo.GetList("GetCommunicationSelectedMembers", param);
        }
        public List<CustomerEntity> GetAllCustomerIds(Int32 bid)
        {
            DapperRepositry<CustomerEntity> _repo = new DapperRepositry<CustomerEntity>();
            DynamicParameters param = new DynamicParameters();

            param.Add("@BusinessId", bid, DbType.Int32, ParameterDirection.Input);
            return _repo.GetList("GetAllCustomerIds", param);
        }
        public List<CommunicationEntity> GetCommunicationByFromId(paggingEntity ps, Int64 bid)
        {
            DapperRepositry<CommunicationEntity> _repo = new DapperRepositry<CommunicationEntity>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@PageSize", ps.pgsize, DbType.Int16, ParameterDirection.Input);
            param.Add("@PageIndex", ps.pgindex, DbType.Int16, ParameterDirection.Input);
            param.Add("@Searchstr", ps.str, DbType.String, ParameterDirection.Input);
            param.Add("@SortBy", ps.sortby, DbType.Int16, ParameterDirection.Input);
            param.Add("@FromDate", ps.FromDate, DbType.String, ParameterDirection.Input);
            param.Add("@ToDate", ps.ToDate, DbType.String, ParameterDirection.Input);
            param.Add("@BusinessId", bid, DbType.Int64, ParameterDirection.Input);
            return _repo.GetList("GetCommunicationsByBusiness", param);
        }
        public StatusResponse InsertCommunication(CommunicationEntity ce)
        {
            DapperRepositry<StatusResponse> _repo = new DapperRepositry<StatusResponse>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@CommunicationId", ce.CommunicationId, DbType.Int64, ParameterDirection.Input);
            param.Add("@BusinessId", ce.BusinessId, DbType.Int32, ParameterDirection.Input);
            param.Add("@FromId", ce.FromId, DbType.Int64, ParameterDirection.Input);
            param.Add("@FromType", ce.FromType, DbType.Int32, ParameterDirection.Input);
            param.Add("@ToIds", ce.ToIds, DbType.String, ParameterDirection.Input);
            param.Add("@ToType", ce.ToType, DbType.Int16, ParameterDirection.Input);
            param.Add("@Title", ce.PageTitle, DbType.String, ParameterDirection.Input);
            param.Add("@Message", ce.Message, DbType.String, ParameterDirection.Input);
            param.Add("@CommunicationTypeId", ce.CommunicationTypeId, DbType.Int16, ParameterDirection.Input);
            param.Add("@ReceipentCount", ce.ReceipentCount, DbType.Int32, ParameterDirection.Input);
            param.Add("@TemplateId", ce.TemplateId, DbType.Int16, ParameterDirection.Input);
            param.Add("@QRcode", ce.Qrcode, DbType.String, ParameterDirection.Input);
            param.Add("@Image", ce.EmailImage, DbType.String, ParameterDirection.Input);

            return _repo.GetResult("InsertCommunication", param);
        }
        public StatusResponse InsertCustomerResponse(Int32 comid,Int32 cid)
        {
            DapperRepositry<StatusResponse> _repo = new DapperRepositry<StatusResponse>();
            DynamicParameters param = new DynamicParameters();

            param.Add("@CommunicationId", comid, DbType.Int32, ParameterDirection.Input);
            param.Add("@CustomerId", cid, DbType.Int32, ParameterDirection.Input);
            return _repo.GetResult("InsertCommunicationResponse", param);
        }
    }
}
