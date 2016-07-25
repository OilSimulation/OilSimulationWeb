using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data.Common;
using System.Data;

namespace DBHelper
{
    public class DBSqLite:IDBHelper
    {

        private string stringConn;
        public DBSqLite(string strConn)
        {
            stringConn = strConn;
        }
        public int ExecuteNonQuery(string strSql)
        {
            try
            {
                using (SQLiteConnection sqliteConn = new SQLiteConnection(stringConn))
                {
                    sqliteConn.Open();
                    SQLiteCommand sqliteComm = new SQLiteCommand(sqliteConn);
                    sqliteComm.CommandText = strSql;
                    sqliteComm.CommandType = System.Data.CommandType.Text;
                    
                    return sqliteComm.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                return 0;
               
            }

        }

        public DbDataReader ExecuteReader(string strSql)
        {
            try
            {
                using (SQLiteConnection sqliteConn = new SQLiteConnection(stringConn))
                {
                    sqliteConn.Open();
                    SQLiteCommand sqliteComm = new SQLiteCommand(sqliteConn);
                    sqliteComm.CommandText = strSql;
                    sqliteComm.CommandType = System.Data.CommandType.Text;
                    
                    return sqliteComm.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public DataTable ExecuteStrSql(string strSql)
        {
            try
            {
                using (SQLiteConnection sqliteConn = new SQLiteConnection(stringConn))
                {
                    sqliteConn.Open();
                    SQLiteCommand sqliteComm = new SQLiteCommand(sqliteConn);
                    sqliteComm.CommandText = strSql;
                    sqliteComm.CommandType = System.Data.CommandType.Text;
                    //SQLiteDataReader p=sqliteComm.ExecuteReader();
                    //string sss = DateTime.Now.ToString("s");
                    //DateTime fff=  DateTime.Parse(sss);
                    //while (p.Read())
                    //{
                       
                    //    string xx = p["CreateDateTime"].ToString();
                    //}

                    SQLiteDataAdapter sqliteAdapter = new SQLiteDataAdapter(sqliteComm);
                    DataTable dt = new DataTable();
                    sqliteAdapter.Fill(dt);
                    sqliteConn.Close();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public object ExecuteScalar(string strSql)
        {
            try
            {
                using (SQLiteConnection sqliteConn = new SQLiteConnection(stringConn))
                {
                    sqliteConn.Open();
                    SQLiteCommand sqliteComm = new SQLiteCommand(sqliteConn);
                    sqliteComm.CommandText = strSql;
                    sqliteComm.CommandType = System.Data.CommandType.Text;
                    
                    return  sqliteComm.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                return null;

            }
        }


        public int ExecuteNonQuery(string strSql,  DbParameter[] DBParams)
        {
            try
            {
                using (SQLiteConnection sqliteConn = new SQLiteConnection(stringConn))
                {
                    sqliteConn.Open();
                    SQLiteCommand sqliteComm = new SQLiteCommand(sqliteConn);
                    sqliteComm.CommandText = strSql;
                    sqliteComm.CommandType = System.Data.CommandType.Text;
                    sqliteComm.Parameters.AddRange(DBParams);
                    return sqliteComm.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                return 0;

            }
        }

        public DataTable ExecuteStrSql(string strSql,  DbParameter[] DBParams)
        {
            try
            {
                using (SQLiteConnection sqliteConn = new SQLiteConnection(stringConn))
                {
                    sqliteConn.Open();
                    SQLiteCommand sqliteComm = new SQLiteCommand(sqliteConn);
                    sqliteComm.CommandText = strSql;
                    sqliteComm.CommandType = System.Data.CommandType.Text;
                    sqliteComm.Parameters.AddRange(DBParams);

                    SQLiteDataAdapter sqliteAdapter = new SQLiteDataAdapter(sqliteComm);
                    DataTable dt = new DataTable();
                    sqliteAdapter.Fill(dt);
                    sqliteConn.Close();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                return null;

            }
        }


        public object ExecuteScalar(string strSql, DbParameter[] DBParams)
        {
            try
            {


                using (SQLiteConnection sqliteConn = new SQLiteConnection(stringConn))
                {
                    sqliteConn.Open();
                    SQLiteCommand sqliteComm = new SQLiteCommand(sqliteConn);
                    sqliteComm.CommandText = strSql;
                    sqliteComm.CommandType = System.Data.CommandType.Text;
                    sqliteComm.Parameters.AddRange(DBParams);
                    return sqliteComm.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                return null;

            }
        }
    }
}
