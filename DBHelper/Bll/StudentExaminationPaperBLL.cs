using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBHelper.Model;
using System.Data.Common;
using System.Data.SQLite;

namespace DBHelper.Bll
{
    public class StudentExaminationPaperBLL
    {
        private string m_strConn;
        public StudentExaminationPaperBLL(string strConn)
        {
            m_strConn = strConn;
        }


        //public void GetStudentExam()

        public List<StudentExaminationPaper> GetStudentExaminationPaper()
        {
            string strSql = "select * from StudentExaminationPaper";
            return DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql));
        }
        public StudentExaminationPaper? GetStudentExaminationPaper(int StudentExamId)
        {
            string strSql = "select * from StudentExam where StudentExamId=@StudentExamId";
            List<StudentExaminationPaper> list = DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
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



        private List<StudentExaminationPaper> DataTableToList(DataTable dt)
        {
            List<StudentExaminationPaper> listInfo = new List<StudentExaminationPaper>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    StudentExaminationPaper info = new StudentExaminationPaper();
                    info.Score = dr["Score"] == DBNull.Value ? -100 : Convert.ToDouble(dr["Score"]);
                    info.StudentAnswer = dr["StudentExamId"] == DBNull.Value ? -100 : Convert.ToInt32(dr["StudentAnswer"]);
                    info.StudentExaminationPaperId = dr["StudentExaminationPaperId"] == DBNull.Value ? -100 : Convert.ToInt32(dr["StudentExaminationPaperId"]);
                    info.StudentId = dr["StudentId"] == DBNull.Value ? -100 : Convert.ToInt32(dr["StudentId"]);
                    info.TitleInfoId = dr["TitleInfoId"] == DBNull.Value ? -100 : Convert.ToInt32(dr["TitleInfoId"]);
                    listInfo.Add(info);
                }
            }

            return listInfo;
        }



    }
}
