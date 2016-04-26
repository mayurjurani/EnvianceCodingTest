using System.Data;
using IType;
using System.Data.SqlClient;

namespace DAL
{
    public class PassangerDAL : IPassangerDAL
    {
        private const string SP_SEARCHPASSANGERBYNAME = "sp_searchPassangerByName";

        DatabaseConnection connection;

        public PassangerDAL()
        {
            connection = new DatabaseConnection();
        }

        public DataTable SearchPassangerByName(string name1, string name2, string name3)
        {
            SqlParameter[] paramaters = new SqlParameter[3];
            paramaters[0] = new SqlParameter("@name1", name1);
            paramaters[1] = new SqlParameter("@name2", name2);
            paramaters[2] = new SqlParameter("@name3", name3);
            return connection.ExecuteReader(SP_SEARCHPASSANGERBYNAME, paramaters);
        }
    }
}
