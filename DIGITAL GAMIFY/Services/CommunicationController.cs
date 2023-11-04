using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using DIGITAL_GAMIFY.BAL;
using DIGITAL_GAMIFY.Entities;
using SendGrid;
using SendGrid.SmtpApi;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using DIGITAL_GAMIFY.Code;

namespace DIGITAL_GAMIFY.Services
{
    public class CommunicationController : ApiController
    {
        CommunicationManager cm=new CommunicationManager();
        [Route("api/GetAllCustomerIds")]
        [HttpGet]
        public List<CustomerEntity> GetAllCustomerIds(Int32 bid)
        {
            return cm.GetAllCustomerIds(bid);
        }
        [Route("api/GetCommunicationByFromId")]
        [HttpGet]
        public List<CommunicationEntity> GetCommunicationByFromId(Int64 bid,[FromUri]paggingEntity ps)
        {
            return cm.GetCommunicationByFromId(ps, bid);
        }
        [Route("api/InsertCustomerResponse")]
        [HttpGet]
        public StatusResponse InsertCustomerResponse(Int32 comid,Int32 cid)
        {
            return cm.InsertCustomerResponse(comid, cid);
        }

    }
}
