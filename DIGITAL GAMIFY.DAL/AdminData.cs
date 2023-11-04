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
    public class AdminData
    {
        public AdminEntities GetAdminLogin(AdminLoginEntities p)
        {
            try
            {
                DapperRepositry<AdminEntities> _repo = new DapperRepositry<AdminEntities>();
                DynamicParameters param = new DynamicParameters();
                param.Add("UserName", p.UserName, DbType.String, ParameterDirection.Input);
                param.Add("Password", p.Password, DbType.String, ParameterDirection.Input);
                return _repo.GetResult("GetAdminLogin", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
