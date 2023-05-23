using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BDayWishMail.Models
{
    class SQLManager
    {
        public SqlConnection objConnection = new SqlConnection();
        public SqlCommand Cmd = new SqlCommand();
        public SqlDataAdapter DataAdapter = new SqlDataAdapter();

        private string conString = ConfigurationManager.AppSettings["constr"];


        public SQLManager()
        {
            objConnection.ConnectionString = conString;
            Cmd.Connection = objConnection;

            DataAdapter.SelectCommand = Cmd;
        }
        public bool NonQuery(string Query)
        {
            Cmd.CommandText = Query;

            try
            {
                if (objConnection.State == ConnectionState.Closed)
                    objConnection.Open();
                int result = Cmd.ExecuteNonQuery();
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                    objConnection.Close();
            }
        }

        public DataSet GetDataSet(string Query, string AliasName)
        {
            DataSet dataSet = new DataSet();
            Cmd.CommandText = Query;
            try
            {
                if (objConnection.State == ConnectionState.Closed)
                    objConnection.Open();
                DataAdapter.Fill(dataSet, AliasName);
                return dataSet;
            }
            catch (Exception ex)
            {
                return dataSet;
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                    objConnection.Close();
                DataAdapter.Dispose();
            }
        }
        public DataTable GetDataTable(string Query)
        {
            DataTable dataTable = new DataTable();

            Cmd.CommandText = Query;
            try
            {
                if (objConnection.State == ConnectionState.Closed)
                    objConnection.Open();
                DataAdapter.SelectCommand = Cmd;
                DataAdapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                return dataTable;
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                    objConnection.Close();
            }
        }
        public object GetScalar(string Query)
        {
            Cmd.CommandText = Query;
            try
            {
                if (objConnection.State == ConnectionState.Closed)
                    objConnection.Open();
                object result = Cmd.ExecuteScalar();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                    objConnection.Close();
                DataAdapter.Dispose();
            }
        }
        public object GetReader(string Query)
        {
            Cmd.CommandText = Query;
            try
            {
                if (objConnection.State == ConnectionState.Closed)
                    objConnection.Open();
                object result = Cmd.ExecuteReader();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                    objConnection.Close();
            }
        }
    }
}
