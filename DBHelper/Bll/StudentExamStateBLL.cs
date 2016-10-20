using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBHelper.Model;
using System.Data.SQLite;
using System.Data.Common;
using System.Data;

namespace DBHelper.Bll
{
    public class StudentExamStateBLL
    {
        private string m_strConn;

        public StudentExamStateBLL(string strConn)
        {
            m_strConn = strConn;
        }

        /// <summary>
        /// 获取学生是否交卷
        /// </summary>
        /// <param name="StudentId"></param>
        /// <param name="ExercisesTestId"></param>
        /// <returns></returns>
        public StudentExamState? GetStudentExamState(int StudentId, int ExercisesTestId)
        {
            string strSql = "select * from StudentExamState where StudentId=@StudentId and ExercisesTestId=@ExercisesTestId";
            List<StudentExamState> listInfo =  DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=ExercisesTestId, ParameterName="@ExercisesTestId"},
                new SQLiteParameter(){  Value=StudentId, ParameterName="@StudentId"}
            }));
            if (listInfo.Count>0)
            {
                return listInfo[0];
            }
            else
            {
                return null;
            }

        }


        /// <summary>
        /// 修改学生交卷情况(若没有刚增加)
        /// </summary>
        /// <param name="StudentId"></param>
        /// <param name="ExercisesTestId"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public int EditStudentExamState(int StudentId, int ExercisesTestId, int state)
        {

            StudentExamState? info = GetStudentExamState(StudentId, ExercisesTestId);
            string strSql = "";
            if (info != null)
            {

                strSql = "update   StudentExamState set State=@State where StudentId=@StudentId and ExercisesTestId=@ExercisesTestId";
            }
            else
            {
                strSql = "insert into StudentExamState(StudentId,ExercisesTestId,State) values (@StudentId,@ExercisesTestId,@State)";
            }

            
            return DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteNonQuery(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=ExercisesTestId, ParameterName="@ExercisesTestId"},
                new SQLiteParameter(){  Value=state, ParameterName="@State"},
                new SQLiteParameter(){  Value=StudentId, ParameterName="@StudentId"}
            });
        }

        private List<StudentExamState> DataTableToList(DataTable dt)
        {
            List<StudentExamState> listInfo = new List<StudentExamState>();
            if (dt!=null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    StudentExamState info = new StudentExamState();
                    info.ExercisesTestId = dr["ExercisesTestId"] == DBNull.Value ? -100 : Convert.ToInt32(dr["ExercisesTestId"]);
                    info.State = dr["State"] == DBNull.Value ? 0 : Convert.ToInt32(dr["State"]);
                    info.StudentExamStateId = dr["StudentExamStateId"] == DBNull.Value ? -100 : Convert.ToInt32(dr["StudentExamStateId"]);
                    info.StudentId = dr["StudentId"] == DBNull.Value ? -100 :  Convert.ToInt32(dr["StudentId"]);
                    listInfo.Add(info);                    
                }
            }
            return listInfo;
        }
    }
}
