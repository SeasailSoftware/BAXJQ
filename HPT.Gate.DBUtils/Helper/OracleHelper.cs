using HPT.Gate.Utils.Common;
using System;
using System.Data;
using System.Data.Common;

public class OracleHelper : IDisposable
{
    #region ctor
    public OracleHelper()
    {
        ConnectString = AppSettings.MssqlConnectString;
    }

    public OracleHelper(string connectString)
    {
        ConnectString = connectString;
    }
    #endregion


    public string ProviderName
    {
        get
        {
            //return "System.Data.SqlClient";
            return "System.Data.OleDb";
        }
    }


    #region Var
    public string ConnectString { get; set; }

    #endregion

    public DbConnection connection
    {
        get
        {
            return CreateConnection(ConnectString);
        }
    }


    /// <summary>
    /// 获取连接字符串
    /// </summary>
    /// <returns></returns>
    public string GetConString()
    {
        return ConnectString;
    }

    public DbConnection CreateConnection()
    {
        DbProviderFactory dbfactory = DbProviderFactories.GetFactory(ProviderName);
        DbConnection dbconn = dbfactory.CreateConnection();
        dbconn.ConnectionString = ConnectString;
        return dbconn;
    }
    public DbConnection CreateConnection(string connectionString)
    {
        DbProviderFactory dbfactory = DbProviderFactories.GetFactory(ProviderName);
        DbConnection dbconn = dbfactory.CreateConnection();
        dbconn.ConnectionString = connectionString;
        return dbconn;
    }

    public DbCommand GetStoredProcCommond(string storedProcedure)
    {
        DbCommand dbCommand = connection.CreateCommand();
        dbCommand.CommandText = storedProcedure;
        dbCommand.CommandType = CommandType.StoredProcedure;
        return dbCommand;
    }
    public DbCommand GetSqlStringCommond(string sqlQuery)
    {
        DbCommand dbCommand = connection.CreateCommand();
        dbCommand.CommandText = sqlQuery;
        dbCommand.CommandType = CommandType.Text;
        return dbCommand;
    }

    #region 增加参数
    public void AddParameterCollection(DbCommand cmd, DbParameterCollection dbParameterCollection)
    {
        foreach (DbParameter dbParameter in dbParameterCollection)
        {
            cmd.Parameters.Add(dbParameter);
        }
    }
    public void AddOutParameter(DbCommand cmd, string parameterName, DbType dbType, int size)
    {
        DbParameter dbParameter = cmd.CreateParameter();
        dbParameter.DbType = dbType;
        dbParameter.ParameterName = parameterName;
        dbParameter.Size = size;
        dbParameter.Direction = ParameterDirection.Output;
        cmd.Parameters.Add(dbParameter);
    }
    public void AddInParameter(DbCommand cmd, string parameterName, DbType dbType, object value)
    {
        DbParameter dbParameter = cmd.CreateParameter();
        dbParameter.DbType = dbType;
        dbParameter.ParameterName = parameterName;
        dbParameter.Value = value;
        dbParameter.Direction = ParameterDirection.Input;
        cmd.Parameters.Add(dbParameter);
    }
    public void AddReturnParameter(DbCommand cmd, string parameterName, DbType dbType)
    {
        DbParameter dbParameter = cmd.CreateParameter();
        dbParameter.DbType = dbType;
        dbParameter.ParameterName = parameterName;
        dbParameter.Direction = ParameterDirection.ReturnValue;
        cmd.Parameters.Add(dbParameter);
    }
    public DbParameter GetParameter(DbCommand cmd, string parameterName)
    {
        return cmd.Parameters[parameterName];
    }

    #endregion

    #region 执行
    public DataSet ExecuteDataSet(DbCommand cmd)
    {
        DbProviderFactory dbfactory = DbProviderFactories.GetFactory(ProviderName);
        DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
        dbDataAdapter.SelectCommand = cmd;
        DataSet ds = new DataSet();
        dbDataAdapter.Fill(ds);
        return ds;
    }

    public DataTable ExecuteDataTable(DbCommand cmd)
    {
        try
        {
            DbProviderFactory dbfactory = DbProviderFactories.GetFactory(ProviderName);
            DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
            dbDataAdapter.SelectCommand = cmd;
            DataTable dataTable = new DataTable();
            dbDataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public DbDataReader ExecuteReader(DbCommand cmd)
    {
        cmd.Connection.Open();
        DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        return reader;
    }
    public int ExecuteNonQuery(DbCommand cmd)
    {
        cmd.Connection.Open();
        int ret = cmd.ExecuteNonQuery();
        cmd.Connection.Close();
        return ret;
    }

    public object ExecuteScalar(DbCommand cmd)
    {
        cmd.Connection.Open();
        object ret = cmd.ExecuteScalar();
        cmd.Connection.Close();
        return ret;
    }
    #endregion


    #region IDisposable 成员

    public void Dispose()
    {
        Dispose(true);
    }

    public void Dispose(bool disposing)
    {
        if (disposing)
        {
            connection.Dispose();
        }
    }
    ~OracleHelper()
    {
        Dispose(false);
    }
    #endregion

}



