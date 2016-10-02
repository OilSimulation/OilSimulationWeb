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

        /// <summary>
        /// 修改考试状态
        /// </summary>
        /// <param name="ExercisesTestId"></param>
        /// <param name="state">0:正在考试，1已经结束</param>
        /// <returns></returns>
        public int UpdateExercisesState(int ExercisesTestId, int state)
        {
            string strSql = @"update ExercisesTest set IsOver=@IsOver where  ExercisesTestId=@ExercisesTestId";
            return DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteNonQuery(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=ExercisesTestId, ParameterName="@ExercisesTestId"},
                new SQLiteParameter(){  Value=state, ParameterName="@IsOver"}
            });
        }

        public ExercisesTest? GetExercisesTest(int ExercisesTestId)
        {
            string strSql = "select * from ExercisesTest a left join  ExperimentType b on  a.ExercisesTypeId=b.TypeId where ExercisesTestId=@ExercisesTestId";
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
            string strSql = @"insert into ExercisesTest (ExercisesName,ExercisesDescribe,ExercisesTypeId,UpdateDateTime,IsOver) 
                            values(@ExercisesName,@ExercisesDescribe,@ExercisesTypeId,@UpdateDateTime,@IsOver)";
            return DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteNonQuery(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=info.ExercisesDescribe, ParameterName="@ExercisesDescribe"},
                new SQLiteParameter(){  Value=info.ExercisesName, ParameterName="@ExercisesName"},
                new SQLiteParameter(){  Value=info.ExercisesTypeId, ParameterName="@ExercisesTypeId"},
                new SQLiteParameter(){  Value=info.IsOver, ParameterName="@IsOver"},
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
            string strSql = @"update ExercisesTest set ExercisesDescribe=@ExercisesDescribe,ExercisesName=@ExercisesName
                            ,ExercisesTypeId=@ExercisesTypeId,UpdateDateTime=@UpdateDateTime,IsOver=@IsOver
                            where ExercisesTestId=@ExercisesTestId";
            return DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteNonQuery(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=info.ExercisesTestId, ParameterName="@ExercisesTestId"},
                new SQLiteParameter(){  Value=info.ExercisesDescribe, ParameterName="@ExercisesDescribe"},
                new SQLiteParameter(){  Value=info.ExercisesName, ParameterName="@ExercisesName"},
                new SQLiteParameter(){  Value=info.ExercisesTypeId, ParameterName="@ExercisesTypeId"},
                new SQLiteParameter(){  Value=info.IsOver, ParameterName="@IsOver"},
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
                    info.IsOver = dr["IsOver"] == DBNull.Value ? -100 : Convert.ToInt32(dr["IsOver"]);
                    info.TypeName1 = dr["TypeName1"] == DBNull.Value ? "" : dr["TypeName1"].ToString();
                    info.TypeName2 = dr["TypeName2"] == DBNull.Value ? "" : dr["TypeName2"].ToString();
                    if (dr["UpdateDateTime"] != DBNull.Value)
                    {
                        DateTime datetime;
                        DateTime.TryParse(dr["UpdateDateTime"].ToString(), out datetime);
                        info.UpdateDateTime = datetime;
                    }
                    //info.ListTitleInfo = TitleInfobll.GetExercisesAllTitle(info.ExercisesTestId);
                    
                    if (info.ExercisesTestId!=-100&&info.ExercisesTestId == GetCurrentExercises(info.ExercisesTypeId).ExercisesTestId)
                    {
                        info.IsUse = 1;
                    }
                    list.Add(info);
                }
            }
            return list;

        }

        /// <summary>
        /// 获取前台显示的考试
        /// </summary>
        /// <param name="ExercisesTypeId">考试ID(-1表示 考试 大于-1表示一次练习)</param>
        /// <returns></returns>
        public CurrentExercises GetCurrentExercises(int ExercisesTypeId)
        {
            string strSql = "select * from CurrentExercises where ExercisesTypeId=@ExercisesTypeId";
            return DataTableToCurrentExercises(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=ExercisesTypeId, ParameterName="@ExercisesTypeId"}}));

        }

        public int SaveCurrentExercises(int ExercisesTestId)
        {
            string strSql = "";
            ExercisesTest? info = GetExercisesTest(ExercisesTestId);
            if (info == null)
            {
                return -1;
            }
            else
            {
                CurrentExercises model = GetCurrentExercises(info.Value.ExercisesTypeId);
                if (model.CurrentExercisesId == -100)
                {
                    //增加
                    strSql = "insert into CurrentExercises(ExercisesTestId,ExercisesTypeId) values(@ExercisesTestId,@ExercisesTypeId)";
                    return DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteNonQuery(strSql, new DbParameter[]{
                            new SQLiteParameter(){  Value=ExercisesTestId, ParameterName="@ExercisesTestId"},
                            new SQLiteParameter(){  Value=info.Value.ExercisesTypeId, ParameterName="@ExercisesTypeId"}
                        });

                }
                else
                {
                    strSql = "update CurrentExercises set ExercisesTestId=@ExercisesTestId where ExercisesTypeId=@ExercisesTypeId";
                    //修改
                    return DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteNonQuery(strSql, new DbParameter[]{
                            new SQLiteParameter(){  Value=ExercisesTestId, ParameterName="@ExercisesTestId"},
                            new SQLiteParameter(){  Value=info.Value.ExercisesTypeId, ParameterName="@ExercisesTypeId"}
                        });

                }

            }
        }

        private CurrentExercises DataTableToCurrentExercises(DataTable dt)
        {
            CurrentExercises info = new CurrentExercises();
            if (dt != null && dt.Rows.Count > 0)
            {
                info.CurrentExercisesId = dt.Rows[0]["CurrentExercisesId"] == DBNull.Value ? -100 : Convert.ToInt32(dt.Rows[0]["CurrentExercisesId"]);
                info.ExercisesTestId = dt.Rows[0]["ExercisesTestId"] == DBNull.Value ? -100 : Convert.ToInt32(dt.Rows[0]["ExercisesTestId"]);
                info.ExercisesTypeId = dt.Rows[0]["ExercisesTypeId"] == DBNull.Value ? -100 : Convert.ToInt32(dt.Rows[0]["ExercisesTypeId"]);
            }
            else
            {
                info.CurrentExercisesId = -100;
            }
            return info;
        }


    }
}
