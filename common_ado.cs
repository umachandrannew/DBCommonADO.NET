using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Configuration;

namespace DBCommonADO.NET

{
    public static class CConn
    {
        public static string ConStr = "", ConPrvdr = "", conErr="";

       public static void FindConn(string s)
        {
             //if connection name is not provided, fetch the first connection string  from config file      
            if(s!=null && s!="")
            { 
            ConStr = ConfigurationManager.ConnectionStrings[s].ConnectionString;
            ConPrvdr = ConfigurationManager.ConnectionStrings[s].ProviderName;
            }
            else
            {
                ConStr = ConfigurationManager.ConnectionStrings[0].ConnectionString;
                ConPrvdr = ConfigurationManager.ConnectionStrings[0].ProviderName;
            }
        }
    }
    public class DataConnection : IDisposable
    {
        public DbConnection cn;
        public DbProviderFactory df;
        public System.Configuration.ConnectionStringSettings connString;
        public string s;
        public DbDataReader dr;
        string _reportName;
        string _qStr;
        
        public DataConnection() : this("") { }
       
        public DataConnection(string conName) {
            CConn.FindConn(conName);
            s = CConn.ConPrvdr;
            df = DbProviderFactories.GetFactory(s);
            cn = df.CreateConnection();
            cn.ConnectionString = CConn.ConStr;
        }

        public void DBOperation_Read(string sqlCmd)
        {
            try
            {
                if (cn.State != ConnectionState.Open)
                {
                    cn.Open();
                }
                DbCommand op = df.CreateCommand();
                op.Connection = cn;
                op.CommandText = sqlCmd;
                dr = op.ExecuteReader();
            }
            catch (Exception er)
            {
                CConn.conErr = $"Error Source : {er.Source} thrown : {er.ToString()}" ;
            }
        }

        public void DBOperation_Update(string sqlCmd)
        {
            try
            {
                if (cn.State != ConnectionState.Open)
            {
                cn.Open();
            }
            DbCommand op = df.CreateCommand();
            op.Connection = cn;
            op.CommandText = sqlCmd;
            op.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                CConn.conErr = $"Error Source : {er.Source} thrown : {er.ToString()}";
            }
        }

        public object DBOperation_ExecuteScalar(string sqlCmd)
        {
            try { 
            if (cn.State != ConnectionState.Open)
            {
                cn.Open();
            }
            DbCommand op = df.CreateCommand();
            op.Connection = cn;
            op.CommandText = sqlCmd;
            return op.ExecuteScalar();
        }
            catch (Exception er)
            {
                CConn.conErr = $"Error Source : {er.Source} thrown : {er.ToString()}" ;
                return er;
            }
}
     public List<DataParam> DBOperation_ExecuteProcedure(string sqlCmd, List<DataParam> sqlParams)
        {
            if (cn.State != ConnectionState.Open)
            {
                cn.Open();
            }
            DbCommand op = df.CreateCommand();
            op.Connection = cn;
            op.CommandText = sqlCmd;
            op.CommandType = CommandType.StoredProcedure;

            List<string> strOut = new List<string>();

            foreach (DataParam sqlparam in sqlParams)
            {
                DbParameter DbParam = df.CreateParameter();
                DbParam.DbType = sqlparam.paramType;
                DbParam.ParameterName = sqlparam.paramName;
                DbParam.Value = sqlparam.paramValue;
                DbParam.Size = 300;
                DbParam.Direction = sqlparam.paramDirection;
                op.Parameters.Add(DbParam);

                if (sqlparam.paramDirection == ParameterDirection.Output)
                {
                    strOut.Add(sqlparam.paramName);
                }

            }


            List<DataParam> lResult = new List<DataParam>();


            op.ExecuteNonQuery();


            foreach (string s in strOut)
            {
                lResult.Add(new DataParam(op.Parameters[s].ParameterName,op.Parameters[s].DbType, op.Parameters[s].Value.ToString(), op.Parameters[s].Direction));
            }

            return lResult;
        }

              
        public void DBOperation_Close()
        {
            if (cn.State == ConnectionState.Open)
            {
                cn.Close();

            }
        }
        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {


            if (disposing)
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();

                }
            }


        }


    }


    public class DataParam
    {
        public DbType paramType;
        public string paramValue;
        public ParameterDirection paramDirection;
        public string paramName;

        public DataParam(string paramName,DbType paramType, string paramValue, ParameterDirection paramDirection)
        {
            this.paramType = paramType;
            this.paramValue = paramValue;
            this.paramDirection = paramDirection;
            this.paramName = paramName;
        }
    }
}
