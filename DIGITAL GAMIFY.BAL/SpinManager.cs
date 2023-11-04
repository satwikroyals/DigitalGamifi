using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIGITAL_GAMIFY.Entities;
using DIGITAL_GAMIFY.DAL;

namespace DIGITAL_GAMIFY.BAL
{
    public class SpinManager
    {
        private SpinData objsd=new SpinData();
        public SpinGameEntity getSpinById(Int64 spid, Int64 cid)
        {
            return objsd.getSpinById(spid, cid);
        }
    }
}
