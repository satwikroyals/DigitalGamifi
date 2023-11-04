using DIGITAL_GAMIFY.Code;
using DIGITAL_GAMIFY.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DIGITAL_GAMIFY.BAL;

namespace DIGITAL_GAMIFY.Services
{
    public class SpinController : ApiController
    {
        private SpinManager objsm=new SpinManager();
        [Route("api/getSpinById")]
        [HttpGet]
        public SpinGameEntity getSpinById(Int64 spid, Int64 cid)
        {
            try
            {
                return objsm.getSpinById(spid, cid);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "Spin", "getSpinById - Services");
                return new SpinGameEntity();
            }
        }
    }
}
