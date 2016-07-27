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
    public class StudentExamBLL
    {
        private string m_strConn;
        public StudentExamBLL(string strConn)
        {
            m_strConn = strConn;
        }

        public List<StudentExam> GetStudentExam()
        {
            string strSql = "select * from StudentExam";
            return DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql));
        }
        public StudentExam? GetStudentExam(int StudentExamId)
        {
            string strSql = "select * from StudentExam where StudentExamId=@StudentExamId";
            List<StudentExam> list = DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=StudentExamId, ParameterName="@StudentExamId"}}));
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
        /// 通过学号查询
        /// </summary>
        /// <param name="StudentNumber"></param>
        /// <returns></returns>
        public StudentExam? GetStudentExam(int StudentNumber)
        {
            string strSql = "select * from StudentExam where StudentNumber=@StudentNumber";
            List<StudentExam> list = DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=StudentNumber, ParameterName="@StudentNumber"}}));
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }

        }

        private List<StudentExam> DataTableToList(DataTable dt)
        {
            List<StudentExam> listInfo = new List<StudentExam>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    StudentExam info = new StudentExam();
                    info.StudentExamId = dr["StudentExamId"] == DBNull.Value ? -100 : Convert.ToInt32(dr["StudentExamId"]);
                    info.StudentSex = dr["StudentSex"] == DBNull.Value ? -100 : Convert.ToInt32(dr["StudentSex"]);
                    info.TotalScore = dr["TotalScore"] == DBNull.Value ? -100 : Convert.ToDouble(dr["TotalScore"]);
                    info.StudentName = dr["StudentName"] == DBNull.Value ? "" : dr["StudentName"].ToString();
                    info.StudentNumber = dr["StudentNumber"] == DBNull.Value ? "" : dr["StudentNumber"].ToString();
                    info.StudentPhone = dr["StudentPhone"] == DBNull.Value ? "" : dr["StudentPhone"].ToString();
                    listInfo.Add(info);
                }
            }

            return listInfo;
        }

    }
}
