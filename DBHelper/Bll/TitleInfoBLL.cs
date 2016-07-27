using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBHelper.Model;
using System.Data.Common;
using System.Data.SQLite;
using System.Data;

namespace DBHelper.Bll
{
    public class TitleInfoBLL
    {
        private string m_strConn;

        public TitleInfoBLL(string strConn)
        {
            m_strConn = strConn;
        }

        public List<TitleInfo> GetTitleInfo()
        {
            string strSql = "select * from TitleInfo";
            return DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql));
        }


        public TitleInfo? GetTitleInfo(int TitleInfoId)
        {
            string strSql = "select * from TitleInfo where TitleInfoId=@TitleInfoId";
            List<TitleInfo> list = DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=TitleInfoId, ParameterName="@TitleInfoId"}}));
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }

        }
        private List<TitleInfo> DataTableToList(DataTable dt)
        {
            List<TitleInfo> list = new List<TitleInfo>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TitleInfo info = new TitleInfo();
                    info.CorrectAnswer = dr["CorrectAnswer"] == DBNull.Value ? -100 : Convert.ToInt32(dr["CorrectAnswer"]);
                    info.Score = dr["Score"] == DBNull.Value ? -100 : Convert.ToDouble(dr["Score"]);
                    info.TitleConent = dr["TitleConent"] == DBNull.Value ? "" : dr["TitleConent"].ToString();
                    info.TitleInfoId = dr["TitleInfoId"] == DBNull.Value ? -100 : Convert.ToInt32(dr["TitleInfoId"]);
                    info.TitleTypeId = dr["TitleTypeId"] == DBNull.Value ? -100 : Convert.ToInt32(dr["TitleTypeId"]);
                    info.TypeId = dr["TypeId"] == DBNull.Value ? -100 : Convert.ToInt32(dr["TypeId"]);
                    list.Add(info);
                }
            }
            return list;

        }

    }
}
