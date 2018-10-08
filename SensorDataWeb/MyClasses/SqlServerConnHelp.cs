using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


public class SqlServerConnHelp
{
    public SqlServerConnHelp()
    {
    }

    public SqlConnection GetConnection(string Server, string Database, string UserId, string Password)
    {
        SqlConnection sqlConnection = new SqlConnection();

        sqlConnection.ConnectionString = 
            "Server=" + Server + 
            ";Database=" + Database +
            ";User Id=" + UserId + 
            ";Password=" + Password + ";";

        sqlConnection.Open();

        return sqlConnection;
    }
}
