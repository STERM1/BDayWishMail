using System;
using System.Collections.Generic;
using System.Data.OleDb;
using Microsoft.VisualBasic;
using System.Collections;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using MySql.Data.MySqlClient;

namespace BDayWishMail.Models
{
    class DbConnection
    {
        public static OdbcConnection connect;
        public static MySqlConnection MySQLconnect;
        public static int GetConnection()
        {
            int result = 0;
            try
            {
                OdbcConnection con = new OdbcConnection(Util.GetDbString());
                con.Open();
                connection = con;
                result = 1;
            }
            catch (Exception ex)
            {
                result = 0;
            }
            return result;

        }

        public static int GetConnectionMySQL()
        {
            int result = 0;
            try
            {
                string ConStr1 = Util.GetDbString();
                MySqlConnection con = new MySqlConnection(ConStr1);

                con.Open();
                MySQLconnection = con;

                result = 1;
            }
            catch (Exception ex)
            {
                result = 0;
            }
            return result;

        }

        public static int EUpdate(String Query)
        {
            try
            {
                GetConnectionMySQL();
                OdbcCommand cmd = new OdbcCommand(Query, connect);
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
            catch (InvalidOperationException e)
            {

                if (connect.State == ConnectionState.Open)
                {
                    return EUpdate(Query);
                }
                else
                {

                    // Logger.writeInLogFile(Query + "====" + e.Message, category, active);
                    return -1;
                }


            }
            catch (Exception e)
            {
                //Logger.writeInLogFile(Query + "====" + e.Message, category, active);
                return 0;
            }
        }

        public static int MYSQLEUpdate(String Query)
        {
            try
            {
                string ConStr1 = Util.GetDbString();
                //string ConStr1 = ("server = 10.185.203.163; uid = etuser; pwd = Etuser@123; database = ETDB; port = 3306; respect binary flags = false");
                MySqlConnection con = new MySqlConnection(ConStr1);
                con.Open();
                MySqlCommand cmd = new MySqlCommand(Query, con);
                //MySqlCommand cmd = new MySqlCommand(Query, MySQLconnect);
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
                return result;
            }
            catch (InvalidOperationException e)
            {
                GetConnectionMySQL();

                if (MySQLconnect.State == ConnectionState.Open)
                {
                    return MYSQLEUpdate(Query);
                }
                else
                {

                    // Logger.writeInLogFile(Query + "====" + e.Message, category, active);
                    return -1;
                }


            }
            catch (Exception e)
            {
                //Logger.writeInLogFile(Query + "====" + e.Message, category, active);
                return 0;
            }

        }

        public static DataSet EDataSet(string Query)
        {
            try
            {
                OdbcCommand cmd = new OdbcCommand(Query, connect);
                OdbcDataAdapter adp = new OdbcDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                cmd.Dispose();
                adp.Dispose();
                return ds;
            }
            catch (InvalidOperationException e)
            {
                int res = GetConnection();
                if (res == 1)
                {
                    if (connect.State == ConnectionState.Open)
                    {
                        return EDataSet(Query);
                    }
                    else
                    {


                        return null;
                    }
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {

                return null;
            }
        }

        public static DataSet MYSQLEDataSet(string Query)
        {
            try
            {
                string ConStr1 = Util.GetDbString();
                MySqlConnection con = new MySqlConnection(ConStr1);
                con.Open();
                MySqlCommand cmd = new MySqlCommand(Query, con);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                cmd.Dispose();
                adp.Dispose();
                con.Close();
                return ds;
            }
            catch (InvalidOperationException e)
            {
                int res = GetConnection();
                if (res == 1)
                {
                    if (MySQLconnect.State == ConnectionState.Open)
                    {
                        return MYSQLEDataSet(Query);
                    }
                    else
                    {


                        return null;
                    }
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {

                return null;
            }
        }

        public static OdbcConnection connection
        {
            get
            {
                return connect;
            }
            set
            {
                connect = value;
            }
        }

        public static MySqlConnection MySQLconnection
        {
            get
            {
                return MySQLconnect;
            }
            set
            {
                MySQLconnect = value;
            }
        }

        public static void closeConnection()
        {
            try
            {
                connect.Close();


            }
            catch (Exception e)
            {

            }
            connect = null;
        }

        public static void MySQLcloseConnection()
        {
            try
            {
                MySQLconnect.Close();


            }
            catch (Exception e)
            {

            }
            MySQLconnect = null;
        }

        public DataTable getDataTable(string query)
        {
            MySQLconnect = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEmpDB"].ConnectionString);
            MySQLconnect.Open();
            DataTable dt = new DataTable();
            MySqlDataAdapter dAdop = new MySqlDataAdapter(query, MySQLconnect);
            dAdop.Fill(dt);
            MySQLconnect.Close();
            return dt;
        }



        public DataSet ReturnDataSet(string Query)
        {
            try
            {

                MySQLconnect = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBCon3"].ConnectionString);
                MySQLconnect.Open();
                MySqlCommand cmd = new MySqlCommand(Query, MySQLconnect);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                cmd.Dispose();
                adp.Dispose();
                return ds;

            }
            catch (Exception ex)
            {
                string xyz = ex.Message;
                return null;
                //throw (ex.Message);
            }
            finally
            {
                MySQLconnect.Close();
            }
        }


        public int ExecuteQueryMySql(string Query)
        {
            int i = 0;
            MySQLconnect = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBCon3"].ConnectionString);
            try
            {

                if (MySQLconnect.State != ConnectionState.Open)
                {
                    MySQLconnect.Open();
                }
                MySqlCommand cmd = new MySqlCommand(Query, MySQLconnect);
                cmd.CommandText = Query;
                i = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return i;
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                throw new ApplicationException(ex.Message);
            }
            finally
            {
                if (MySQLconnect.State == ConnectionState.Open)
                {

                    MySQLconnect.Close();
                }
            }
        }
    }
}


