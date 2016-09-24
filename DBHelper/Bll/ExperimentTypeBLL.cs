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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CurrentPage">当前第几页</param>
        /// <param name="ShowCount">每页显示个数</param>
        /// <returns></returns>
        public List<ExperimentType> GetExperimentType(int CurrentPage, int ShowCount)
        {
            string strSql = "select * from ExperimentType order by UpdateDateTime limit @Count offset @Offset";
            return DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql
                , new DbParameter[]{
                    new SQLiteParameter(){  Value=ShowCount, ParameterName="@Count"},
                    new SQLiteParameter(){  Value=ShowCount*CurrentPage, ParameterName="@Offset"}
                }
                ));
        }


        public ExperimentType GetExperimentType(int TypeId)
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
                return new ExperimentType();
            }

        }

        public int DelExperimentType(int TypeId)
        {
            string strSql = "delete from ExperimentType where TypeId=@TypeId";
            return DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteNonQuery(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=TypeId, ParameterName="@TypeId"}});

        }

        public int UpdateExperimentType(ExperimentType data)
        {
            string strSql = "update  ExperimentType set TypeName1=@TypeName1,TypeName2=@TypeName2,TypeDescribe=@TypeDescribe,UpdateDateTime=@UpdateDateTime where TypeId=@TypeId";
            return DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteNonQuery(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=data.TypeId, ParameterName="@TypeId"},
                new SQLiteParameter(){  Value=data.TypeName1, ParameterName="@TypeName1"},
                new SQLiteParameter(){  Value=data.TypeName2, ParameterName="@TypeName2"},
                new SQLiteParameter(){  Value=data.TypeDescribe, ParameterName="@TypeDescribe"},
                new SQLiteParameter(){  Value=data.UpdateDateTime, ParameterName="@UpdateDateTime"},
            
            });
        }

        public int AddExperimentType(ExperimentType data)
        {

            string strSql = "insert into  ExperimentType(TypeName1,TypeName2,TypeDescribe) values (@TypeName1,@TypeName2,@TypeDescribe)";
            return DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteNonQuery(strSql, new DbParameter[]{
                 new SQLiteParameter(){  Value=data.TypeName1, ParameterName="@TypeName1"},
                 new SQLiteParameter(){  Value=data.TypeName2, ParameterName="@TypeName2"},
                 new SQLiteParameter(){  Value=data.TypeDescribe, ParameterName="@TypeDescribe"},
                 new SQLiteParameter(){  Value=data.UpdateDateTime, ParameterName="@UpdateDateTime"},
             
             });
        }

        /// <summary>
        /// 判断 大、小类型名称
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool IsExistData(ExperimentType data)
        {
            string strSql = "select 1 from   ExperimentType where TypeName1=@TypeName1 and TypeName2=@TypeName2";
            DataTable dt =  DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
                 new SQLiteParameter(){  Value=data.TypeName1, ParameterName="@TypeName1"},
                 new SQLiteParameter(){  Value=data.TypeName2, ParameterName="@TypeName2"},
             });
            if (dt!=null&&dt.Rows.Count>0)
            {
                return true;
            }
            else
            {
                return false;
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
