using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace DBHelper
{
    public interface IDBHelper
    {

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns>受影响的行数</returns>
        int ExecuteNonQuery(string strSql);

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns>数据列表</returns>
        DbDataReader ExecuteReader(string strSql);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        DataTable ExecuteStrSql(string strSql);

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns>数据第一行第一列</returns>
        object ExecuteScalar(string strSql);

        object ExecuteScalar(string strSql, DbParameter[] DBParams);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="DBParams"></param>
        /// <returns></returns>
        int ExecuteNonQuery(string strSql,  DbParameter[] DBParams);


        DataTable ExecuteStrSql(string strSql,   DbParameter[] DBParams);

    }
}
