using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIGITAL_GAMIFY.DAL.Repositiories
{
    internal class DapperRepositry<T> where T : class
    {
        private IDbConnection Connection
        {
            get
            {
                return new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ToString());
            }
        }

        public string SpName = string.Empty;
        public DynamicParameters Parameters = null;

        public IEnumerable<T> GetList()
        {
            using (IDbConnection db = Connection)
            {
                return db.Query<T>(this.SpName, commandType: CommandType.StoredProcedure, param: this.Parameters).ToList();
            }
        }

        /// <summary>
        /// Get list of results.
        /// </summary>
        /// <param name="spName">Storedprocedure name</param>
        /// <param name="parameters">Storedprocedure parameters collection</param>
        /// <returns>List</returns>
        public List<T> GetList(string spName, DynamicParameters parameters = null)
        {
            using (IDbConnection db = Connection)
            {
                return db.Query<T>(spName, commandType: CommandType.StoredProcedure, param: parameters).ToList();
            }
        }

        /// Get result.
        /// </summary>
        /// <param name="spName">Storedprocedure name</param>
        /// <param name="parameters">Storedprocedure parameters collection</param>
        /// <returns>result</returns>
        public T GetResult(string spName, DynamicParameters parameters = null)
        {
            using (IDbConnection db = Connection)
            {
                return db.Query<T>(spName, commandType: CommandType.StoredProcedure, param: parameters).FirstOrDefault();
            }
        }

        public int Add()
        {
            using (IDbConnection db = Connection)
            {
                return db.Query<int>(this.SpName, commandType: CommandType.StoredProcedure, param: this.Parameters).SingleOrDefault();
            }
        }
        public int Change()
        {
            using (IDbConnection db = Connection)
            {
                return db.Execute(this.SpName, commandType: CommandType.StoredProcedure, param: this.Parameters);
            }
        }
        public void Delete(T entity)
        {
            using (IDbConnection db = Connection)
            {

            }
        }

        public void Update(T entity)
        {
            using (IDbConnection db = Connection)
            {

            }
        }

        public T FindById()
        {
            using (IDbConnection db = Connection)
            {
                return db.Query<T>(this.SpName, commandType: CommandType.StoredProcedure, param: this.Parameters).FirstOrDefault();
            }
        }
        public IEnumerable<DataTable> GetTables()
        {
            using (IDbConnection db = Connection)
            {
                // var reader = db.QueryMultiple(this.SpName, commandType: CommandType.StoredProcedure, param: this.Parameters);
                return db.Query<DataTable>(this.SpName, commandType: CommandType.StoredProcedure, param: this.Parameters).AsEnumerable();
                //return reader;
            }
        }
    }
}
