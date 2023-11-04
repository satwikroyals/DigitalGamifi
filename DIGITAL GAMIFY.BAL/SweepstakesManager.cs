using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIGITAL_GAMIFY.Entities;
using DIGITAL_GAMIFY.DAL;

namespace DIGITAL_GAMIFY.BAL
{
    public class SweepstakesManager
    {
        private SweepstakesData objsd = new SweepstakesData();
        public List<SweepstakesEntity> GetAdminSweepstakesList(paggingEntity pe, Int32 adminid, Int32 bid)
        {
            return objsd.GetAdminSweepstakesList(pe, adminid, bid);
        }
        public StatusResponse AddSweepstakes(SweepstakesEntity sqEntity)
        {
            return objsd.AddSweepstakes(sqEntity);
        }
        public SweepstakesEntity DeleteSweepstakes(Int32 gid)
        {
            return objsd.DeleteSweepstakes(gid);
        }
        public SweepstakesEntity GetSweepstakesById(Int32 gid)
        {
            return objsd.GetSweepstakesById(gid);
        }
        public StatusEntity InsertSweepstakesResult(Int32 gid,Int64 cid)
        {
            return objsd.InsertSweepstakesResult(gid, cid);
        }
        public List<SweepstakesresultEntity> AdminGetSweepstakesresult(paggingEntity p, Int32 bid, Int64 gid)
        {
            return objsd.AdminGetSweepstakesresult(p, bid, gid);
        }
        public List<Quizddl> GetddlSweepstakes(Int32 bid)
        {
            return objsd.GetddlSweepstakes(bid);
        }
    }
}
