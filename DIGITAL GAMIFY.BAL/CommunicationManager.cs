using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIGITAL_GAMIFY.Entities;
using DIGITAL_GAMIFY.DAL;

namespace DIGITAL_GAMIFY.BAL
{
    public class CommunicationManager
    {
        CommunicationData cd=new CommunicationData();
        public List<CustomerEntity> GetCommunicationSelectedMembers(string ids)
        {
            return cd.GetCommunicationSelectedMembers(ids);
        }
        public List<CustomerEntity> GetAllCustomerIds(Int32 bid)
        {
            return cd.GetAllCustomerIds(bid);
        }
        public List<CommunicationEntity> GetCommunicationByFromId(paggingEntity ps, Int64 bid)
        {
            return cd.GetCommunicationByFromId(ps, bid);
        }
        public StatusResponse InsertCommunication(CommunicationEntity ce)
        {
            return cd.InsertCommunication(ce);
        }
        public StatusResponse InsertCustomerResponse(Int32 comid,Int32 cid)
        {
            return cd.InsertCustomerResponse(comid, cid);
        }
    }
}
