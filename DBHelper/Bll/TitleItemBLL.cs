using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBHelper.Model;
using System.Data.SQLite;
using System.Data.Common;
using System.Data;

namespace DBHelper.Bll
{
    public class TitleItemBLL
    {
        private string m_strConn;

        public TitleItemBLL(string strConn)
        {
            m_strConn = strConn;
        }

        public List<TitleItem> GetTitleItem()
        {
            string strSql = "select * from TitleItem";
            return DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql));
        }

        /// <summary>
        /// 获取题目的所有选项
        /// </summary>
        /// <param name="TitleInfoId"></param>
        /// <returns></returns>
        public List<TitleItem> GetTitleInfoAllItem(int TitleInfoId)
        {
            string strSql = "select * from TitleItemAssoc a left join TitleItem b on a.TitleItemId=b.TitleItemId where a.TitleInfoId=@TitleInfoId";
            return DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=TitleInfoId, ParameterName="@TitleInfoId"}}));
        }


        public TitleItem? GetTitleItem(int TitleItemId)
        {
            string strSql = "select * from TitleItem where TitleItemId=@TitleItemId";
            List<TitleItem> list = DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=TitleItemId, ParameterName="@TitleItemId"}}));
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }

        }
        private List<TitleItem> DataTableToList(DataTable dt)
        {
            List<TitleItem> list = new List<TitleItem>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TitleItem info = new TitleItem();
                    info.TitleItemContent = dr["TitleItemContent"] == DBNull.Value ? "" : dr["TitleItemContent"].ToString();
                    info.TitleItemId = dr["TitleItemId"] == DBNull.Value ? -100 : Convert.ToInt32(dr["TitleItemId"]);
                    list.Add(info);
                }
            }
            return list;

        }


    }
}
