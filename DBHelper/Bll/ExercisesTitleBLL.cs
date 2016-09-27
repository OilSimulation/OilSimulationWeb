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
        //public List<ExercisesTitle> GetExercisesTitle()
        //{
        //    string strSql = "select * from ExercisesTitle";
        //    return DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql));
        //}

        /// <summary>
        /// 获取考试 中 题目位置的最大位置+1
        /// </summary>
        /// <param name="ExercisesTestId">考试ID</param>
        /// <returns></returns>
        public int GetExercisesTitleMaxIndex(int ExercisesTestId)
        {
            string strSql = "select Max(ExercisesTitleIndex) from ExercisesTitle  where  ExercisesTestId=@ExercisesTestId ";
            object obj = DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteScalar(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=ExercisesTestId, ParameterName="@ExercisesTestId"}});
            int result = 1;
            if (obj != null)
            {
                int.TryParse(obj.ToString(), out result);
                result++;
            }
            return result;

        }

        /// <summary>
        /// 判断 考试中是否存在某题目
        /// </summary>
        /// <param name="ExercisesTestId">考试ID</param>
        /// <param name="TitleInfoId">题目ID</param>
        /// <returns></returns>
        public bool IsExistExercisesTitle(int ExercisesTestId, int TitleInfoId)
        {
            string strSql = "select 1 from ExercisesTitle  where TitleInfoId=@TitleInfoId and ExercisesTestId=@ExercisesTestId";
            DataTable dt = DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=TitleInfoId, ParameterName="@TitleInfoId"},
                new SQLiteParameter(){  Value=ExercisesTestId, ParameterName="@ExercisesTestId"}
            });
            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取某场考试下的所有题目
        /// </summary>
        /// <param name="ExercisesTestId"></param>
        /// <returns></returns>
        public List<ExercisesTitle> GetExercisesTitle(int ExercisesTestId)
        {
            string strSql = "select * from ExercisesTitle a,TitleInfo b where a.ExercisesTestId=@ExercisesTestId and a.TitleInfoId=b.TitleInfoId  ";
            return DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=ExercisesTestId, ParameterName="@ExercisesTestId"}}));
        }

        /// <summary>
        /// 删除考试与题目的对应关系
        /// </summary>
        /// <param name="ExercisesTitleId"></param>
        /// <returns></returns>
        public int DelExercisesTitle(int ExercisesTitleId)
        {
            string strSql = "delete from ExercisesTitle where ExercisesTitleId=@ExercisesTitleId";
            return DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteNonQuery(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=ExercisesTitleId, ParameterName="@ExercisesTitleId"}});
        }

        /// <summary>
        /// 增加考试与题目的对应关系
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int AddExercisesTitle(ExercisesTitle info)
        {
            string strSql = "insert into ExercisesTitle (TitleInfoId,ExercisesTestId,ExercisesTitleIndex,UpdateDateTime) values (@TitleInfoId,@ExercisesTestId,@ExercisesTitleIndex,@UpdateDateTime)";
            return DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteNonQuery(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=info.TitleInfoId, ParameterName="@TitleInfoId"},
                new SQLiteParameter(){  Value=info.ExercisesTestId, ParameterName="@ExercisesTestId"},
                new SQLiteParameter(){  Value=info.ExercisesTitleIndex, ParameterName="@ExercisesTitleIndex"},
                new SQLiteParameter(){  Value=info.UpdateDateTime, ParameterName="@UpdateDateTime"}
            
            });
        }

        public int UpdateExercisesTitle(ExercisesTitle info)
        {
            string strSql = "update  ExercisesTitle set  TitleInfoId=@TitleInfoId,ExercisesTestId=@ExercisesTestId,ExercisesTitleIndex=@ExercisesTitleIndex,UpdateDateTime=@UpdateDateTime where ExercisesTitleId=@ExercisesTitleId";
            return DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteNonQuery(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=info.TitleInfoId, ParameterName="@TitleInfoId"},
                new SQLiteParameter(){  Value=info.ExercisesTestId, ParameterName="@ExercisesTestId"},
                new SQLiteParameter(){  Value=info.ExercisesTitleIndex, ParameterName="@ExercisesTitleIndex"},
                new SQLiteParameter(){  Value=info.ExercisesTitleId, ParameterName="@ExercisesTitleId"},
                new SQLiteParameter(){  Value=info.UpdateDateTime, ParameterName="@UpdateDateTime"}
            });
        }

        ///// <summary>
        ///// 获取某场考试下的所有题目
        ///// </summary>
        ///// <param name="ExercisesTestId"></param>
        ///// <returns></returns>
        //public List<ExercisesTitle> GetExercisesTest(int ExercisesTestId)
        //{
        //    string strSql = "select * from ExercisesTitle where ExercisesTestId=@ExercisesTestId";
        //    return DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
        //        new SQLiteParameter(){  Value=ExercisesTestId, ParameterName="@ExercisesTestId"}}));

        //}


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
                    info.ExercisesTitleIndex = dr["ExercisesTitleIndex"] == DBNull.Value ? -100 : Convert.ToInt32(dr["ExercisesTitleIndex"]);
                    info.CorrectAnswer = dr["CorrectAnswer"] == DBNull.Value ? -100 : Convert.ToInt32(dr["CorrectAnswer"]);
                    info.Score = dr["Score"] == DBNull.Value ? -100 : Convert.ToInt32(dr["Score"]);
                    info.TitleConent = dr["TitleConent"] == DBNull.Value ? "" : dr["TitleConent"].ToString();
                    
                    if (dr["UpdateDateTime"] != DBNull.Value)
                    {
                        DateTime datetime;
                        DateTime.TryParse(dr["UpdateDateTime"].ToString(), out datetime);
                        info.UpdateDateTime = datetime;
                    }
                    listInfo.Add(info);
                }
            }

            return listInfo;
        }

    }
}
