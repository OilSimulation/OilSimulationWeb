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
        ExercisesTestBLL ExercisesTestbll;
        private string m_strConn;
        public StudentExamBLL(string strConn)
        {
            m_strConn = strConn;
            ExercisesTestbll = new ExercisesTestBLL(strConn);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password"></param>
        /// <param name="type">角色 1:学生,2:教师</param>
        /// <returns>用户名,是否是第一次登录</returns>
        public LoginResult? Login(string userid, string password, int type)
        {
            string strSql = "select IsFirstLogin,StudentNumber,StudentName,LoginDateTime from StudentExam where StudentNumber=@StudentNumber and Password=@Password and Type=@Type";
            LoginResult? result =  DataTableToLoginResult(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=userid, ParameterName="@StudentNumber"},
                new SQLiteParameter(){  Value=password, ParameterName="@Password"},
                new SQLiteParameter(){  Value=type, ParameterName="@Type"}
            }));

            if (result!=null)
            {
                //登录成功，修改登录时间和是否是第一次登录
                strSql = "update StudentExam set LoginDateTime=@LoginDateTime,IsFirstLogin=2 where  StudentNumber=@StudentNumber and Password=@Password and Type=@Type";
                DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteNonQuery(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=userid, ParameterName="@StudentNumber"},
                new SQLiteParameter(){  Value=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ParameterName="@LoginDateTime"},
                new SQLiteParameter(){  Value=password, ParameterName="@Password"},
                new SQLiteParameter(){  Value=type, ParameterName="@Type"}
                });
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid">用户名</param>
        /// <param name="oldpassword"></param>
        /// <param name="newpassword"></param>
        /// <param name="type">角色类型(学生、教师)</param>
        /// <returns></returns>
        public int UpdatePassword(string userid, string oldpassword, string newpassword, int type)
        {
            string strSql = "";
            //验证旧密码是否正确
            strSql = "select 1 from StudentExam where StudentNumber=@StudentNumber and Password=@Password and Type=@Type";
            object obj = DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteScalar(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=userid, ParameterName="@StudentNumber"},
                new SQLiteParameter(){  Value=oldpassword, ParameterName="@Password"},
                new SQLiteParameter(){  Value=type, ParameterName="@Type"}
            });
            if (obj != null)//旧密码正确
            {
                //修改密码
                strSql = "update StudentExam set Password=@NewPassword where StudentNumber=@StudentNumber and Password=@Password and Type=@Type";
                int result = DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteNonQuery(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=userid, ParameterName="@StudentNumber"},
                new SQLiteParameter(){  Value=oldpassword, ParameterName="@Password"},
                new SQLiteParameter(){  Value=newpassword, ParameterName="@NewPassword"},
                new SQLiteParameter(){  Value=type, ParameterName="@Type"}
                });
                if (result>0)
                {
                    //修改成功
                    return 1;
                }
                else
                {
                    //修改失败
                    return -101;
                }

            }
            else
            {
                //旧密码不正确
                return -100;
            }

        }

        /// <summary>
        /// 获取用户所有信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public StudentExam? GetUserInfo(string userid)
        {
            string strSql = "select * from StudentExam where StudentNumber=@StudentNumber";
            return DataTableToStudentExam(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=userid, ParameterName="@StudentNumber"}
            }));

        }


        private StudentExam? DataTableToStudentExam(DataTable dt)
        {
            if (dt!=null&&dt.Rows.Count>0)
            {
                StudentExam info = new StudentExam();
                info.IsFirstLogin = dt.Rows[0]["IsFirstLogin"] == DBNull.Value ? 2 : Convert.ToInt32(dt.Rows[0]["IsFirstLogin"]);
                info.StudentName = dt.Rows[0]["StudentName"] == DBNull.Value ? "" : dt.Rows[0]["StudentName"].ToString();
                info.StudentExamId = dt.Rows[0]["StudentExamId"] == DBNull.Value ? -100 : Convert.ToInt32(dt.Rows[0]["StudentExamId"]);
                info.StudentNumber = dt.Rows[0]["StudentNumber"] == DBNull.Value ? "" : dt.Rows[0]["StudentNumber"].ToString();
                info.StudentSex = dt.Rows[0]["StudentSex"] == DBNull.Value ? -100 : Convert.ToInt32(dt.Rows[0]["StudentSex"]);
                info.StudentPhone = dt.Rows[0]["StudentPhone"] == DBNull.Value ? "" : dt.Rows[0]["StudentPhone"].ToString();
                info.Type = dt.Rows[0]["Type"] == DBNull.Value ? -100 : Convert.ToInt32(dt.Rows[0]["Type"]);
                if (dt.Rows[0]["LoginDateTime"] != DBNull.Value)
                {
                    DateTime datetime;
                    if (DateTime.TryParse(dt.Rows[0]["LoginDateTime"].ToString(), out datetime))
                    {
                        info.LoginDateTime = datetime.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                }
                return info;

            }
            else
            {
                return null;
            }
        }


        private LoginResult? DataTableToLoginResult(DataTable dt)
        {
            if (dt!=null&&dt.Rows.Count>0)
            {
                LoginResult info = new LoginResult();
                info.IsFirstLogin = dt.Rows[0]["IsFirstLogin"] == DBNull.Value ? 2 : Convert.ToInt32(dt.Rows[0]["IsFirstLogin"]);
                info.UserName = dt.Rows[0]["StudentName"] == DBNull.Value ? "" : dt.Rows[0]["StudentName"].ToString();
                info.UserID = dt.Rows[0]["StudentNumber"] == DBNull.Value ? "" : dt.Rows[0]["StudentNumber"].ToString();
                if (dt.Rows[0]["LoginDateTime"] != DBNull.Value)
                {
                    DateTime datetime;
                    if (DateTime.TryParse(dt.Rows[0]["LoginDateTime"].ToString(), out datetime))
                    {
                        info.LoginDateTime = datetime.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                }
                return info;
            }
            else
            {
                return null;
            }
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
        /// 查询参加了该考试的学生
        /// </summary>
        /// <param name="ExercisesTestId"></param>
        /// <returns></returns>
        public List<StudentExam> GetStudentExamByExercisesTest(int ExercisesTestId)
        {
            string strSql = "select DISTINCT b.StudentExamId,b.StudentSex,b.StudentName,b.StudentNumber,b.StudentPhone from StudentExaminationPaper a,StudentExam b where a.ExercisesTestId=@ExercisesTestId and a.StudentExamId=b.StudentExamId";
            return DataTableToList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=ExercisesTestId, ParameterName="@ExercisesTestId"}}));

        }


        /// <summary>
        /// 查询学生参加的考试
        /// </summary>
        /// <param name="StudentId"></param>
        /// <returns></returns>
        public List<ExamList> GetExercisesTestStudent(int StudentId)
        {
            //学生参加的考试 
            string strSql = "select DISTINCT a.StudentExamId,b.ExercisesTestId,b.ExercisesName,b.ExercisesDescribe from StudentExaminationPaper a,ExercisesTest   b where a.StudentExamId=@StudentExamId and a.ExercisesTestId=b.ExercisesTestId";


            return DataTableToExamList(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=StudentId, ParameterName="@StudentExamId"}}));

        }

        /// <summary>
        /// 获取学生总成绩
        /// </summary>
        /// <param name="StudentId">学生ID</param>
        /// <param name="ExercisesTestId">考试ID</param>
        /// <returns></returns>
        public double GetStudentScore(int StudentId, int ExercisesTestId)
        {
            string strSql = @"select sum(d.Score) from (
                                select * from StudentExaminationPaper  a where a.ExercisesTestId=@ExercisesTestId and a.StudentExamId=@StudentExamId) c
                                left join TitleInfo d on c.TitleInfoId=d.TitleInfoId where c.StudentAnswer=d.CorrectAnswer";
            object obj = DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteScalar(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=ExercisesTestId, ParameterName="@ExercisesTestId"},
                new SQLiteParameter(){  Value=StudentId, ParameterName="@StudentExamId"}
            });
            double score = 0.0;
            if (obj != null)
            {
                double.TryParse(obj.ToString(), out score);
            }


            return score;
        }

        /// <summary>
        /// 通过学号查询
        /// </summary>
        /// <param name="StudentNumber"></param>
        /// <returns></returns>
        public StudentExam? GetStudentExamNumber(string StudentNumber)
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

        public bool IsExistStudentNumber(string StudentNumber)
        {
            string strSql = "select 1 from StudentExam where StudentNumber=@StudentNumber";
            object obj = DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteScalar(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=StudentNumber, ParameterName="@StudentNumber"}});
            return obj == null ? false : true;

        }

        public StudentExam? OptStudentNumber(StudentExam info)
        {
            if (!IsExistStudentNumber(info.StudentNumber))
            {
                string strSql = "insert into StudentExam(StudentNumber,StudentName) values(@StudentNumber,@StudentName)";
                int obj = DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteNonQuery(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=info.StudentNumber, ParameterName="@StudentNumber"},
                new SQLiteParameter(){  Value=info.StudentName==null?"":info.StudentName,ParameterName="@StudentName"}
                });
                if (obj > 0)
                {
                    return GetStudentExamNumber(info.StudentNumber);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return GetStudentExamNumber(info.StudentNumber);
            }

        }

        private List<ExamList> DataTableToExamList(DataTable dt)
        {
            List<ExamList> listInfo = new List<ExamList>();
            if (dt!=null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    int StudentExamId = -1;
                    ExamList info = new ExamList();
                    info.ExercisesTestId = dr["ExercisesTestId"] == DBNull.Value ? -100 : Convert.ToInt32(dr["ExercisesTestId"]);
                    info.ExercisesName = dr["ExercisesName"] == DBNull.Value ? "" : dr["ExercisesName"].ToString();
                    info.ExercisesDescribe = dr["ExercisesDescribe"] == DBNull.Value ? "" : dr["ExercisesDescribe"].ToString();
                    StudentExamId = dr["StudentExamId"] == DBNull.Value ? -100 : Convert.ToInt32(dr["StudentExamId"]);
                    info.TotleScore = ExercisesTestbll.GetExercisesTestTotleScore(info.ExercisesTestId);
                    info.StudentScore = GetStudentScore(StudentExamId, info.ExercisesTestId);

                    listInfo.Add(info);

                }
            }
            return listInfo;
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
                    //info.TotalScore = dr["TotalScore"] == DBNull.Value ? -100 : Convert.ToDouble(dr["TotalScore"]);
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
