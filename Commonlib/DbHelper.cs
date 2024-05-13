using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commonlib
{
    public static class DbHelper
    {
        public static SqlConnection GetSqlConnection()
        {
            SqlConnection sqlConnection = null;

            string connectionString = "Data Source=DESKTOP-PBSFM7Q;Initial Catalog=store;Integrated Security=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;TrustServerCertificate=True";

            sqlConnection = new SqlConnection(connectionString);

            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            return sqlConnection;
        }
    }
}
