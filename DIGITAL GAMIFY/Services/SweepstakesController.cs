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
    public class SweepstakesController : ApiController
    {
        SweepstakesManager objsm = new SweepstakesManager();
        [Route("api/GetAdminSweepstakesList")]
        [HttpGet]
        public List<SweepstakesEntity> GetAdminSweepstakesList(Int32 adminid, Int32 bid, [FromUri]paggingEntity pe)
        {
            try
            {
                return objsm.GetAdminSweepstakesList(pe, adminid, bid);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "SweepstakesController", "GetAdminSweepstakesList-Services");
                return new List<SweepstakesEntity>();
            }
        }
        [Route("api/DeleteSweepstakes")]
        [HttpPost]
        public SweepstakesEntity DeleteSweepstakes(Int32 gid)
        {
            try
            {
                return objsm.DeleteSweepstakes(gid);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "SweepstakesController", "DeleteSweepstakes-Services");
                return new SweepstakesEntity();
            }
        }
        [Route("api/GetSweepstakesById")]
        [HttpGet]
        public SweepstakesEntity GetSweepstakesById(Int32 gid)
        {
            try
            {
                return objsm.GetSweepstakesById(gid);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "SweepstakesController", "GetSweepstakesById-Services");
                return new SweepstakesEntity();
            }
        }
        [Route("api/InsertSweepstakesResult")]
        [HttpGet]
        public StatusEntity InsertSweepstakesResult(Int32 gid,Int64 cid)
        {
            try
            {
                return objsm.InsertSweepstakesResult(gid, cid);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "SweepstakesController", "InsertSweepstakesResult-Services");
                return new StatusEntity();
            }
        }
        [Route("api/AdminGetSweepstakesresult")]
        [HttpGet]
        public List<SweepstakesresultEntity> AdminGetSweepstakesresult(Int32 bid, Int64 gid,[FromUri]paggingEntity p)
        {
            try
            {
                return objsm.AdminGetSweepstakesresult(p, bid, gid);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "SweepstakesController", "AdminGetSweepstakesresult-Services");
                return new List<SweepstakesresultEntity>();
            }
        }
        [Route("api/GetddlSweepstakes")]
        [HttpGet]
        public List<Quizddl> GetddlSweepstakes(Int32 bid)
        {
            return objsm.GetddlSweepstakes(bid);
        }
    }
}
