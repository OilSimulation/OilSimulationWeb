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
    public class ExperimentTypeBLL
    {
        private string m_strConn;
        public ExperimentTypeBLL(string strConn)
        {
            m_strConn = strConn;
        }

        public List<ExperimentType> GetExperimentType()
        {
            string strSql = "select * from ExperimentType";
            return DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql));
        }
        public ExperimentType? GetExperimentType(int TypeId)
        {
            string strSql = "select * from ExperimentType where TypeId=@TypeId";
            List<ExperimentType> list = DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=TypeId, ParameterName="@TypeId"}}));
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }

        }

        private List<ExperimentType> DataTableToList(DataTable dt)
        {
            List<ExperimentType> listInfo = new List<ExperimentType>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ExperimentType info = new ExperimentType();
                    info.TypeDescribe = dr["TypeDescribe"] == DBNull.Value ? "" : dr["TypeDescribe"].ToString();
                    info.TypeId = dr["TypeId"] == DBNull.Value ? -100 : Convert.ToInt32(dr["TypeId"]);
                    info.TypeName1 = dr["TypeName1"] == DBNull.Value ? "" : dr["TypeName1"].ToString();
                    info.TypeName2 = dr["TypeName2"] == DBNull.Value ? "" : dr["TypeName2"].ToString();
                    listInfo.Add(info);
                }
            }

            return listInfo;
        }

    }
}
