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
    public class TitleTypeBLL
    {
        private string m_strConn;

        public TitleTypeBLL(string strConn)
        {
            m_strConn = strConn;
        }

        public List<TitleType> GetTitleType()
        {
            string strSql = "select * from TitleType";
            return DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql));
        }


        public TitleType? GetTitleType(int TiteTypeId)
        {
            string strSql = "select * from TitleType where TiteTypeId=@TiteTypeId";
            List<TitleType> list = DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=TiteTypeId, ParameterName="@TiteTypeId"}}));
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }

        }
        private List<TitleType> DataTableToList(DataTable dt)
        {
            List<TitleType> list = new List<TitleType>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TitleType info = new TitleType();
                    info.TiteTypeId = dr["TiteTypeId"] == DBNull.Value ? -100 : Convert.ToInt32(dr["TiteTypeId"]);
                    info.TitleTypeName = dr["TitleTypeName"] == DBNull.Value ? "" : dr["TitleTypeName"].ToString();
                    list.Add(info);
                }
            }
            return list;

        }


    }
}
