using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIGITAL_GAMIFY.Entities;
using DIGITAL_GAMIFY.DAL;

namespace DIGITAL_GAMIFY.BAL
{
    public class GlobalManager
    {


        GlobalData dal = new GlobalData();

        public List<BusinessTypesEntity> GetBusinessTypes()
        {
            return dal.GetBusinessTypes();
        }

        public List<SearchDdlEntities> GetDdlBusiness(Int32 AdminId)
        {
            return dal.GetDdlBusiness(AdminId);
        }
        public StatusResponse BusinessDeliverprizeactionbtn(Int64 resultid,int type,int action)
        {
            return dal.BusinessDeliverprizeactionbtn(resultid, type, action);
        }
        public List<Surveyddl> GetDdlSurveys(Int32 bid)
        {
            return dal.GetDdlSurveys(bid);
        }
        public List<Attributes> GetAttributesByPrizeTypeId(Int32 ptid)
        {
            return dal.GetAttributesByPrizeTypeId(ptid);
        }
        public ReultDetails GetResultById(Int64 resultid, int type)
        {
            return dal.GetResultById(resultid, type);
        }
        public List<Statesddl> GetStateddl()
        {
            return dal.GetStateddl();
        }
    }
}
