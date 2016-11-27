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
    public class WebReportBLL
    {
        private string m_strConn;
        public WebReportBLL(string strConn)
        {
            m_strConn = strConn;
        }


        public int AddWebReportInfo(WebReportInfo info)
        {
            string strSql = @"insert into WebReport(CourseName,ExperimentName,ExperimentAddress,ExperimentDate,StudentNumber,StudentName,ClassName
                            ,TeacherName,Title1,Title2,Title3,Title4,Title5,Title6,Title7) values 
                            (@CourseName,@ExperimentName,@ExperimentAddress,@ExperimentDate,@StudentNumber,@StudentName,@ClassName
                            ,@TeacherName,@Title1,@Title2,@Title3,@Title4,@Title5,@Title6,@Title7)";

            int result = DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteNonQuery(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=info.CourseName, ParameterName="@CourseName"}
                ,new SQLiteParameter(){  Value=info.ExperimentName, ParameterName="@ExperimentName"}
                ,new SQLiteParameter(){  Value=info.ExperimentAddress, ParameterName="@ExperimentAddress"}
                ,new SQLiteParameter(){  Value=info.ExperimentDate, ParameterName="@ExperimentDate"}
                ,new SQLiteParameter(){  Value=info.StudentNumber, ParameterName="@StudentNumber"}
                ,new SQLiteParameter(){  Value=info.StudentName, ParameterName="@StudentName"}
                ,new SQLiteParameter(){  Value=info.ClassName, ParameterName="@ClassName"}
                ,new SQLiteParameter(){  Value=info.TeacherName, ParameterName="@TeacherName"}
                ,new SQLiteParameter(){  Value=info.Title1, ParameterName="@Title1"}
                ,new SQLiteParameter(){  Value=info.Title2, ParameterName="@Title2"}
                ,new SQLiteParameter(){  Value=info.Title3, ParameterName="@Title3"}
                ,new SQLiteParameter(){  Value=info.Title4, ParameterName="@Title4"}
                ,new SQLiteParameter(){  Value=info.Title5, ParameterName="@Title5"}
                ,new SQLiteParameter(){  Value=info.Title6, ParameterName="@Title6"}
                ,new SQLiteParameter(){  Value=info.Title7, ParameterName="@Title7"}
            
            });

            return result;

        }

        /// <summary>
        /// 根据 学号进行修改报告
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdateWebReportInfo(WebReportInfo info)
        {
            string strSql = @"update WebReport set CourseName=@CourseName,ExperimentName=@ExperimentName,ExperimentAddress=@ExperimentAddress
                            ,ExperimentDate=@ExperimentDate,StudentName=@StudentName,ClassName=@ClassName,TeacherName=@TeacherName
                            ,Title1=@Title1,Title2=@Title2,Title3=@Title3,Title4=@Title4,Title5=@Title5,Title6=@Title6,Title7=@Title7    where StudentNumber=@StudentNumber";
            int result =DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteNonQuery(strSql, new DbParameter[]{
                 new SQLiteParameter(){  Value=info.WebReportId, ParameterName="@WebReportId"}
                ,new SQLiteParameter(){  Value=info.CourseName, ParameterName="@CourseName"}
                ,new SQLiteParameter(){  Value=info.ExperimentName, ParameterName="@ExperimentName"}
                ,new SQLiteParameter(){  Value=info.ExperimentAddress, ParameterName="@ExperimentAddress"}
                ,new SQLiteParameter(){  Value=info.ExperimentDate, ParameterName="@ExperimentDate"}
                ,new SQLiteParameter(){  Value=info.StudentNumber, ParameterName="@StudentNumber"}
                ,new SQLiteParameter(){  Value=info.StudentName, ParameterName="@StudentName"}
                ,new SQLiteParameter(){  Value=info.ClassName, ParameterName="@ClassName"}
                ,new SQLiteParameter(){  Value=info.TeacherName, ParameterName="@TeacherName"}
                ,new SQLiteParameter(){  Value=info.Title1, ParameterName="@Title1"}
                ,new SQLiteParameter(){  Value=info.Title2, ParameterName="@Title2"}
                ,new SQLiteParameter(){  Value=info.Title3, ParameterName="@Title3"}
                ,new SQLiteParameter(){  Value=info.Title4, ParameterName="@Title4"}
                ,new SQLiteParameter(){  Value=info.Title5, ParameterName="@Title5"}
                ,new SQLiteParameter(){  Value=info.Title6, ParameterName="@Title6"}
                ,new SQLiteParameter(){  Value=info.Title7, ParameterName="@Title7"}
            
            });

            return result;
        }

        public int EditWebReportInfo(WebReportInfo info)
        {
            WebReportInfo? data = GetWebReportInfo(info.StudentNumber);
            if (data != null)
            {
                //修改
                return UpdateWebReportInfo(info);
            }
            else
            {
                //增加
                return AddWebReportInfo(info);
            }
        }

        public WebReportInfo? GetWebReportInfo(string StudentNumber)
        {
            string strSql = "select * from WebReport where StudentNumber=@StudentNumber";
            List<WebReportInfo> listInfo = DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=StudentNumber, ParameterName="@StudentNumber"}}));
            if (listInfo.Count > 0)
            {
                return listInfo[0];
            }
            else
            {
                return null;
            }
        }


        //public WebReportInfo? GetWebReportInfo(int WebReportId)
        //{
        //    string strSql = "select * from WebReport where WebReportId=@WebReportId";
        //    List<WebReportInfo> listInfo = DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
        //        new SQLiteParameter(){  Value=WebReportId, ParameterName="@WebReportId"}}));
        //    if (listInfo.Count>0)
        //    {
        //        return listInfo[0];
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public List<WebReportInfo> GetWebReportInfo()
        {
            string strSql = "select * from WebReport";
            return DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql));
        }


        private List<WebReportInfo> DataTableToList(DataTable dt)
        {
            List<WebReportInfo> listInfo = new List<WebReportInfo>();
            if (dt!=null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    WebReportInfo info = new WebReportInfo();
                    info.ClassName = dr["ClassName"] == DBNull.Value ? "" : dr["ClassName"].ToString();
                    info.CourseName = dr["CourseName"] == DBNull.Value ? "" : dr["CourseName"].ToString();
                    info.ExperimentAddress = dr["ExperimentAddress"] == DBNull.Value ? "" : dr["ExperimentAddress"].ToString();
                    info.ExperimentDate = dr["ExperimentDate"] == DBNull.Value ? "" : dr["ExperimentDate"].ToString();
                    info.ExperimentName = dr["ExperimentName"] == DBNull.Value ? "" : dr["ExperimentName"].ToString();
                    info.Score = dr["Score"] == DBNull.Value ? 0 : Convert.ToDouble(dr["Score"]);
                    info.StudentName = dr["StudentName"] == DBNull.Value ? "" : dr["StudentName"].ToString();
                    info.StudentNumber = dr["StudentNumber"] == DBNull.Value ? "" : dr["StudentNumber"].ToString();
                    info.TeacherName = dr["TeacherName"] == DBNull.Value ? "" : dr["TeacherName"].ToString();
                    info.Title1 = dr["Title1"] == DBNull.Value ? "" : dr["Title1"].ToString();
                    info.Title2 = dr["Title2"] == DBNull.Value ? "" : dr["Title2"].ToString();
                    info.Title3 = dr["Title3"] == DBNull.Value ? "" : dr["Title3"].ToString();
                    info.Title4 = dr["Title4"] == DBNull.Value ? "" : dr["Title4"].ToString();
                    info.Title5 = dr["Title5"] == DBNull.Value ? "" : dr["Title5"].ToString();
                    info.Title6 = dr["Title6"] == DBNull.Value ? "" : dr["Title6"].ToString();
                    info.Title7 = dr["Title7"] == DBNull.Value ? "" : dr["Title7"].ToString();
                    info.WebReportId = dr["WebReportId"] == DBNull.Value ? -1 : Convert.ToInt32(dr["WebReportId"]);
                    listInfo.Add(info);
                }
            }

            return listInfo;
        }
    }
}
