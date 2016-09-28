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
        TitleItemBLL TitleItembll;
        public TitleInfoBLL(string strConn)
        {
            m_strConn = strConn;
            TitleItembll = new TitleItemBLL(m_strConn);
        }

        public List<TitleInfo> GetTitleInfo()
        {
            string strSql = @"select * from TitleInfo a left join ExperimentType b on a.TypeId=b.TypeId
                            left join TitleType c on a.TitleTypeId=c.TiteTypeId";
            return DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql));
        }

        /// <summary>
        /// 根据实验类型获取题目
        /// </summary>
        /// <param name="TypeId">实验类型</param>
        /// <returns></returns>
        public List<TitleInfo> GetTitleInfoTypeId(int TypeId)
        {
            string strSql = @"select * from TitleInfo a left join ExperimentType b on a.TypeId=b.TypeId
                            left join TitleType c on a.TitleTypeId=c.TiteTypeId where a.TypeId=@TypeId";
            return DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=TypeId, ParameterName="@TypeId"}}));
        }

        public TitleInfo GetTitleInfo(int TitleInfoId)
        {
            string strSql = @"select * from TitleInfo a left join ExperimentType b on a.TypeId=b.TypeId
                            left join TitleType c on a.TitleTypeId=c.TiteTypeId where TitleInfoId=@TitleInfoId";
            List<TitleInfo> list = DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=TitleInfoId, ParameterName="@TitleInfoId"}}));
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return new TitleInfo();
            }
        }

        public int DelTitleInfo(int TitleInfoId)
        {
            string strSql = "delete from TitleInfo where TitleInfoId=@TitleInfoId";
            return DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteNonQuery(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=TitleInfoId, ParameterName="@TitleInfoId"}});
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info">修改条件：TitleInfoId</param>
        /// <returns></returns>
        public int UpdateTitleInfo(TitleInfo info)
        {
             string strSql = @"update TitleInfo set TitleConent=@TitleConent,TitleTypeId=@TitleTypeId,TypeId=@TypeId,
                               CorrectAnswer=@CorrectAnswer,UpdateDateTime=@UpdateDateTime,Score=@Score where TitleInfoId=@TitleInfoId";// 
            return DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteNonQuery(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=info.TitleInfoId, ParameterName="@TitleInfoId"},
                new SQLiteParameter(){  Value=info.TitleConent, ParameterName="@TitleConent"},
                new SQLiteParameter(){  Value=info.TitleTypeId, ParameterName="@TitleTypeId"},
                new SQLiteParameter(){  Value=info.TypeId, ParameterName="@TypeId"},
                new SQLiteParameter(){  Value=info.CorrectAnswer, ParameterName="@CorrectAnswer"},
                new SQLiteParameter(){  Value=info.Score, ParameterName="@Score"},
                new SQLiteParameter(){  Value=info.UpdateDateTime, ParameterName="@UpdateDateTime"}
            });
        }

        public int AddTitleInfo(TitleInfo info)
        {
            string strSql = @"insert into TitleInfo (TitleConent,TitleTypeId,TypeId,CorrectAnswer,Score,UpdateDateTime) 
                            values (@TitleConent,@TitleTypeId,@TypeId,@CorrectAnswer,@Score,@UpdateDateTime)";
            return DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteNonQuery(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=info.TitleConent, ParameterName="@TitleConent"},
                new SQLiteParameter(){  Value=info.TitleTypeId, ParameterName="@TitleTypeId"},
                new SQLiteParameter(){  Value=info.TypeId, ParameterName="@TypeId"},
                new SQLiteParameter(){  Value=info.CorrectAnswer, ParameterName="@CorrectAnswer"},
                new SQLiteParameter(){  Value=info.Score, ParameterName="@Score"},
                new SQLiteParameter(){  Value=info.UpdateDateTime, ParameterName="@UpdateDateTime"}
            });
        }

        /// <summary>
        /// 获取考试下的所有题目
        /// </summary>
        /// <param name="ExercisesTestId">考试id</param>
        /// <returns></returns>
        public List<TitleInfo> GetExercisesAllTitle(int ExercisesTestId)
        {
            string strSql = @"select * from ExercisesTitle a left join TitleInfo b on a.TitleInfoId=b.TitleInfoId 
                            left join ExperimentType c on b.TypeId=c.TypeId
                            left join TitleType d on b.TitleTypeId=d.TitleTypeId
                            where a.ExercisesTestId=@ExercisesTestId";
            return DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=ExercisesTestId, ParameterName="@ExercisesTestId"}
            }));
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
                    info.TitleTypeName = dr["TitleTypeName"] == DBNull.Value ? "" : Convert.ToString(dr["TitleTypeName"]);
                    info.TypeName1 = dr["TypeName1"] == DBNull.Value ? "" : Convert.ToString(dr["TypeName1"]);
                    info.TypeName2 = dr["TypeName2"] == DBNull.Value ? "" : Convert.ToString(dr["TypeName2"]);
                    if (dr["UpdateDateTime"] != DBNull.Value)
                    {
                        DateTime datetime;
                        DateTime.TryParse(dr["UpdateDateTime"].ToString(), out datetime);
                        info.UpdateDateTime = datetime;
                    }

                    info.ListTitleItem = TitleItembll.GetTitleInfoAllItem(info.TitleInfoId);
                    list.Add(info);
                }
            }
            return list;

        }

    }
}
