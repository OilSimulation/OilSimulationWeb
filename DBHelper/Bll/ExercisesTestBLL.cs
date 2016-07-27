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
    public class ExercisesTestBLL
    {
        private string m_strConn;

        public ExercisesTestBLL(string strConn)
        {
            m_strConn = strConn;
        }

        public List<ExercisesTest> GetExercisesTest()
        {
            string strSql = "select * from ExercisesTest";
            return DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql));
        }


        public ExercisesTest? GetExercisesTest(int ExercisesTestId)
        {
            string strSql = "select * from ExercisesTest where ExercisesTestId=@ExercisesTestId";
            List<ExercisesTest> list = DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=ExercisesTestId, ParameterName="@ExercisesTestId"}}));
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }

        }
        private List<ExercisesTest> DataTableToList(DataTable dt)
        {
            List<ExercisesTest> list = new List<ExercisesTest>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ExercisesTest info = new ExercisesTest();
                    info.ExercisesDescribe = dr["ExercisesDescribe"] == DBNull.Value ? "" : dr["ExercisesDescribe"].ToString();
                    info.ExercisesName = dr["ExercisesName"] == DBNull.Value ? "" : dr["ExercisesName"].ToString();
                    info.ExercisesTestId = dr["ExercisesTestId"] == DBNull.Value ? -100 : Convert.ToInt32(dr["ExercisesDescribe"]);
                    info.ExercisesTypeId = dr["ExercisesTypeId"] == DBNull.Value ? -100 : Convert.ToInt32(dr["ExercisesTypeId"]);
                    list.Add(info);
                }
            }
            return list;

        }


    }
}
