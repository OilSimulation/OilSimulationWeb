using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBHelper.Model;
using System.Data.SQLite;
using System.Data.Common;

namespace DBHelper.Bll
{
    public class PeriodBLL
    {
        private string m_strConn;
        public PeriodBLL(string strConn)
        {
            m_strConn = strConn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<PeriodInfo> GetPeriod()
        {
            string strSql = "select * from Period";
            return ModelConvertHelper<PeriodInfo>.ConvertTo<PeriodInfo>(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PeriodId"></param>
        /// <returns></returns>
        public PeriodInfo GetPeriod(int PeriodId)
        {
            string strSql = "select * from Period";
            IList<PeriodInfo> list = ModelConvertHelper<PeriodInfo>.ConvertTo<PeriodInfo>(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=PeriodId, ParameterName="@PeriodId"}}));
            if (list.Count<=0)
            {
                return null;
            }
            else
            {
                return list[0];
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PeriodNumber">硬盘号</param>
        /// <returns></returns>
        public PeriodInfo GetPeriod(string PeriodNumber)
        {
            string strSql = "select * from Period where PeriodNumber=@PeriodNumber";
            IList<PeriodInfo> list = ModelConvertHelper<PeriodInfo>.ConvertTo<PeriodInfo>(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=PeriodNumber, ParameterName="@PeriodNumber"}}));
            if (list.Count <= 0)
            {
                return null;
            }
            else
            {
                return list[0];
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info">根据ID修改信息</param>
        /// <returns></returns>
        public int UpdatePeriod(PeriodInfo info)
        {
            string strSql = "update Period set PeriodNumber=@PeriodNumber,StartDateTime=@StartDateTime,PeriodDay=@PeriodDay,PeriodAlias=@PeriodAlias where PeriodId=@PeriodId";
            return DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteNonQuery(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=info.PeriodNumber, ParameterName="@PeriodNumber"},
                new SQLiteParameter(){  Value=info.StartDateTime, ParameterName="@StartDateTime"},
                new SQLiteParameter(){  Value=info.PeriodDay, ParameterName="@PeriodDay"},
                new SQLiteParameter(){  Value=info.PeriodAlias, ParameterName="@PeriodAlias"},
                new SQLiteParameter(){  Value=info.PeriodId, ParameterName="@PeriodId"}            
            });

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PeriodId"></param>
        /// <returns></returns>
        public int DeletePeriod(int PeriodId)
        {
            string strSql = "delete from Period where PeriodId=@PeriodId";
            return DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteNonQuery(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=PeriodId, ParameterName="@PeriodId"}            
            });

        }

        public int AddPeriod(PeriodInfo info)
        {
            string strSql = "insert into Period(PeriodNumber,StartDateTime,PeriodDay,PeriodAlias) values (@PeriodNumber,@StartDateTime,@PeriodDay,@PeriodAlias)";
            return DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteNonQuery(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=info.PeriodNumber, ParameterName="@PeriodNumber"},
                new SQLiteParameter(){  Value=info.StartDateTime, ParameterName="@StartDateTime"},
                new SQLiteParameter(){  Value=info.PeriodDay, ParameterName="@PeriodDay"},
                new SQLiteParameter(){  Value=info.PeriodAlias, ParameterName="@PeriodAlias"}   
            });

        }
    }
}
