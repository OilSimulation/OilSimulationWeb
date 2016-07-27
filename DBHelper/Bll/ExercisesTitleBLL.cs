using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBHelper.Model;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;

namespace DBHelper.Bll
{
    public class ExercisesTitleBLL
    {
        private string m_strConn;
        public ExercisesTitleBLL(string strConn)
        {
            m_strConn = strConn;
        }
        public List<ExercisesTitle> GetExercisesTitle()
        {
            string strSql = "select * from ExercisesTitle";
            return DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql));
        }


        /// <summary>
        /// 获取某场考试下的所有题目
        /// </summary>
        /// <param name="ExercisesTestId"></param>
        /// <returns></returns>
        public List<ExercisesTitle> GetExercisesTest(int ExercisesTestId)
        {
            string strSql = "select * from ExercisesTitle where ExercisesTestId=@ExercisesTestId";
            return DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=ExercisesTestId, ParameterName="@ExercisesTestId"}}));

        }


        private List<ExercisesTitle> DataTableToList(DataTable dt)
        {
            List<ExercisesTitle> listInfo = new List<ExercisesTitle>();
            if (dt!=null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ExercisesTitle info = new ExercisesTitle();
                    info.ExercisesTestId = dr["ExercisesTestId"] == DBNull.Value ? -100 : Convert.ToInt32(dr["ExercisesTestId"]);
                    info.ExercisesTitleId = dr["ExercisesTitleId"] == DBNull.Value ? -100 : Convert.ToInt32(dr["ExercisesTitleId"]);
                    info.TitleInfoId = dr["TitleInfoId"] == DBNull.Value ? -100 : Convert.ToInt32(dr["TitleInfoId"]);
                    listInfo.Add(info);
                }
            }

            return listInfo;
        }

    }
}
