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
        TitleInfoBLL TitleInfobll;

        public ExercisesTestBLL(string strConn)
        {
            m_strConn = strConn;
            TitleInfobll = new TitleInfoBLL(m_strConn);
        }

        public List<ExercisesTest> GetExercisesTest()
        {
            string strSql = "select * from ExercisesTest a left join ExperimentType b on a.ExercisesTypeId=b.TypeId";
            return DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql));
        }


        public ExercisesTest GetExercisesTest(int ExercisesTestId)
        {
            string strSql = "select * from ExercisesTest a left join  ExperimentType b on where a.ExercisesTypeId=b.TypeId and ExercisesTestId=@ExercisesTestId";
            List<ExercisesTest> list = DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=ExercisesTestId, ParameterName="@ExercisesTestId"}}));
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return new ExercisesTest();
            }

        }

        /// <summary>
        /// 获取考试或练习 总分
        /// </summary>
        /// <param name="ExercisesTestId"></param>
        /// <returns></returns>
        public double GetExercisesTestTotleScore(int ExercisesTestId)
        {
            string strSql = "select sum(b.Score) from ExercisesTitle a ,TitleInfo  b where a.ExercisesTestId=@ExercisesTestId and a.TitleInfoId=b.TitleInfoId";
            object obj = DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteScalar(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=ExercisesTestId, ParameterName="@ExercisesTestId"}
            });
            double score = 0.0;
            if (obj != null)
            {
                double.TryParse(obj.ToString(), out score);
            }

            return score;
        }

        public int AddExercisesTest(ExercisesTest info)
        {
            string strSql = @"insert into ExercisesTest (ExercisesName,ExercisesDescribe,ExercisesTypeId,UpdateDateTime) 
                            values(@ExercisesName,@ExercisesDescribe,@ExercisesTypeId,@UpdateDateTime)";
            return DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteNonQuery(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=info.ExercisesDescribe, ParameterName="@ExercisesDescribe"},
                new SQLiteParameter(){  Value=info.ExercisesName, ParameterName="@ExercisesName"},
                new SQLiteParameter(){  Value=info.ExercisesTypeId, ParameterName="@ExercisesTypeId"},
                new SQLiteParameter(){  Value=info.UpdateDateTime, ParameterName="@UpdateDateTime"}            
            });

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdateExercisesTest(ExercisesTest info)
        {
            string strSql = @"update ExercisesTest set ExercisesDescribe=@ExercisesDescribe,ExercisesName=@ExercisesName,ExercisesTypeId=@ExercisesTypeId,UpdateDateTime=@UpdateDateTime
                            where ExercisesTestId=@ExercisesTestId";
            return DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteNonQuery(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=info.ExercisesTestId, ParameterName="@ExercisesTestId"},
                new SQLiteParameter(){  Value=info.ExercisesDescribe, ParameterName="@ExercisesDescribe"},
                new SQLiteParameter(){  Value=info.ExercisesName, ParameterName="@ExercisesName"},
                new SQLiteParameter(){  Value=info.ExercisesTypeId, ParameterName="@ExercisesTypeId"},
                new SQLiteParameter(){  Value=info.UpdateDateTime, ParameterName="@UpdateDateTime"}            
            });

        }

        public int DelExercisesTest(int ExercisesTestId)
        {
            string strSql = @"delete from ExercisesTest where ExercisesTestId=@ExercisesTestId";
            return DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteNonQuery(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=ExercisesTestId, ParameterName="@ExercisesTestId"}
            });
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
                    info.ExercisesTestId = dr["ExercisesTestId"] == DBNull.Value ? -100 : Convert.ToInt32(dr["ExercisesTestId"]);
                    info.ExercisesTypeId = dr["ExercisesTypeId"] == DBNull.Value ? -100 : Convert.ToInt32(dr["ExercisesTypeId"]);
                    info.TypeName1 = dr["TypeName1"] == DBNull.Value ? "" : dr["TypeName1"].ToString();
                    info.TypeName2 = dr["TypeName2"] == DBNull.Value ? "" : dr["TypeName2"].ToString();
                    if (dr["UpdateDateTime"] != DBNull.Value)
                    {
                        DateTime datetime;
                        DateTime.TryParse(dr["UpdateDateTime"].ToString(), out datetime);
                        info.UpdateDateTime = datetime;
                    }
                    info.ListTitleInfo = TitleInfobll.GetExercisesAllTitle(info.ExercisesTestId);

                    list.Add(info);
                }
            }
            return list;

        }


    }
}
