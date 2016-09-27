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
// 
//         public List<TitleItemAssoc> GetTitleItemAssoc()
//         {
//             string strSql = "select * from TitleItemAssoc";
//             return DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql));
//         }


//         public TitleItemAssoc? GetTitleItemAssoc(int TitleItemAssocId)
//         {
//             string strSql = "select * from TitleItemAssoc where TitleItemAssocId=@TitleItemAssocId";
//             List<TitleItemAssoc> list = DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
//                 new SQLiteParameter(){  Value=TitleItemAssocId, ParameterName="@TitleItemAssocId"}}));
//             if (list.Count > 0)
//             {
//                 return list[0];
//             }
//             else
//             {
//                 return null;
//             }
// 
//         }
// 
//         /// <summary>
//         /// 获取 题目下的所有选项ID
//         /// </summary>
//         /// <param name="TitleInfoId">题目ID</param>
//         /// <returns></returns>
//         public TitleItemAssoc? GetTitleInfo(int TitleInfoId)
//         {
//             string strSql = "select * from TitleItemAssoc where TitleInfoId=@TitleInfoId";
//             List<TitleItemAssoc> list = DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
//                 new SQLiteParameter(){  Value=TitleInfoId, ParameterName="@TitleInfoId"}}));
//             if (list.Count > 0)
//             {
//                 return list[0];
//             }
//             else
//             {
//                 return null;
//             }
// 
//         }


        /// <summary>
        /// 获取题目选项位置索引+1
        /// </summary>
        /// <param name="TitleInfoId"></param>
        /// <returns></returns>
        public int GetTitleItemAssocIndex(int TitleInfoId)
        {
            string strSql = "select Max(TitleItemIndex) from TitleItemAssoc where  TitleInfoId=@TitleInfoId ";
            object obj = DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteScalar(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=TitleInfoId, ParameterName="@TitleInfoId"}});
            int result = 1;
            if (obj!=null)
            {
                int.TryParse(obj.ToString(), out result);
                result++;
            }
            return result;

        }

        /// <summary>
        /// 获取 题目下的所有选项信息
        /// </summary>
        /// <param name="TitleInfoId"></param>
        /// <returns></returns>
        public List<TitleItemAssoc> GetTitleInfoItem(int TitleInfoId)
        {
            string strSql = "select * from TitleItemAssoc a,TitleItem b where  a.TitleItemId=b.TitleItemId and a.TitleInfoId=@TitleInfoId order by TitleItemIndex asc";
            return DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=TitleInfoId, ParameterName="@TitleInfoId"}}));

        }

        public int DelTitleItemAssoc(int TitleItemAssocId)
        {
            string strSql = "delete from TitleItemAssoc where TitleItemAssocId=@TitleItemAssocId";
            return DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteNonQuery(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=TitleItemAssocId, ParameterName="@TitleItemAssocId"}});
        }

        public int AddTitleItemAssoc(TitleItemAssoc info)
        {
            string strSql = "insert into TitleItemAssoc (TitleInfoId,TitleItemId,TitleItemIndex,UpdateDateTime) values (@TitleInfoId,@TitleItemId,@TitleItemIndex,@UpdateDateTime)";
            return DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteNonQuery(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=info.TitleInfoId, ParameterName="@TitleInfoId"},
                new SQLiteParameter(){  Value=info.TitleItemId, ParameterName="@TitleItemId"},
                new SQLiteParameter(){  Value=info.TitleItemIndex, ParameterName="@TitleItemIndex"},
                new SQLiteParameter(){  Value=info.UpdateDateTime, ParameterName="@UpdateDateTime"}
            
            });
        }

        /// <summary>
        /// 判断题目中有没有该选项
        /// </summary>
        /// <param name="TitleInfoId">题目ID</param>
        /// <param name="TitleItemId">选项ID</param>
        /// <returns></returns>
        public bool IsExistTitleItemAssoc(int TitleInfoId, int TitleItemId)
        {
            string strSql = "select 1 from TitleItemAssoc where TitleInfoId=@TitleInfoId and TitleItemId=@TitleItemId";
            DataTable dt = DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=TitleInfoId, ParameterName="@TitleInfoId"},
                new SQLiteParameter(){  Value=TitleItemId, ParameterName="@TitleItemId"}
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

//         public int UpdateTitleItemAssoc(TitleItemAssoc info)
//         {
// 
//         }




//         private List<TitleItem> DataTableToListItem(DataTable dt)
//         {
//             List<TitleItem> list = new List<TitleItem>();
//             if (dt != null)
//             {
//                 foreach (DataRow dr in dt.Rows)
//                 {
//                     TitleItem info = new TitleItem();
//                     info.TitleItemContent = dr["TitleItemContent"] == DBNull.Value ? "" : dr["TitleItemContent"].ToString();
//                     info.TitleItemId = dr["TitleItemId"] == DBNull.Value ? -100 : Convert.ToInt32(dr["TitleItemId"]);
//                     info.TitleItemIndex = dr["TitleItemIndex"] == DBNull.Value ? -100 : Convert.ToInt32(dr["TitleItemIndex"]);
//                     if (dr["UpdateDateTime"] != DBNull.Value)
//                     {
//                         DateTime datetime;
//                         DateTime.TryParse(dr["UpdateDateTime"].ToString(), out datetime);
//                         info.UpdateDateTime = datetime;
//                     }
//                     list.Add(info);
//                 }
//             }
//             return list;
//
//        }


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
                    info.TitleItemIndex = dr["TitleItemIndex"] == DBNull.Value ? -100 : Convert.ToInt32(dr["TitleItemIndex"]);
                    info.TitleItemContent = dr["TitleItemContent"] == DBNull.Value ? "" : Convert.ToString(dr["TitleItemContent"]);
                    if (dr["UpdateDateTime"] != DBNull.Value)
                    {
                        DateTime datetime;
                        DateTime.TryParse(dr["UpdateDateTime"].ToString(), out datetime);
                        info.UpdateDateTime = datetime;

                    }
                    list.Add(info);
                }
            }
            return list;

        }

    }
}
