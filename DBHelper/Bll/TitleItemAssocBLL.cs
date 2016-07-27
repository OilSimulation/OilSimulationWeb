using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBHelper.Model;
using System.Data;
using System.Data.SQLite;
using System.Data.Common;

namespace DBHelper.Bll
{
    public class TitleItemAssocBLL
    {
        private string m_strConn;

        public TitleItemAssocBLL(string strConn)
        {
            m_strConn = strConn;
        }

        public List<TitleItemAssoc> GetTitleItemAssoc()
        {
            string strSql = "select * from TitleItemAssoc";
            return DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql));
        }


        public TitleItemAssoc? GetTitleItemAssoc(int TitleItemAssocId)
        {
            string strSql = "select * from TitleItemAssoc where TitleItemAssocId=@TitleItemAssocId";
            List<TitleItemAssoc> list = DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=TitleItemAssocId, ParameterName="@TitleItemAssocId"}}));
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }

        }
        private List<TitleItemAssoc> DataTableToList(DataTable dt)
        {
            List<TitleItemAssoc> list = new List<TitleItemAssoc>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TitleItemAssoc info = new TitleItemAssoc();
                    info.TitleItemAssocId = dr["TitleItemAssocId"] == DBNull.Value ? -100 : Convert.ToInt32(dr["TitleItemAssocId"]);
                    info.TitleItemId = dr["TitleItemId"] == DBNull.Value ? -100 : Convert.ToInt32(dr["TitleItemId"]);
                    info.TitleInfoId = dr["TitleInfoId"] == DBNull.Value ? -100 : Convert.ToInt32(dr["TitleInfoId"]);
                    list.Add(info);
                }
            }
            return list;

        }

    }
}
