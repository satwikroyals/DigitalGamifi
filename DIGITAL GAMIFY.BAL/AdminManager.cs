using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIGITAL_GAMIFY.DAL;
using DIGITAL_GAMIFY.Entities;

namespace DIGITAL_GAMIFY.BAL
{
    public class AdminManager
    {

        AdminData dal = new AdminData();

        public AdminEntities GetAdminLogin(AdminLoginEntities p)
        {
            return dal.GetAdminLogin(p);
        }


    }
}
