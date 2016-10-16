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

        ExercisesTestBLL ExercisesTestbll;
        StudentExamBLL StudentExambll;

        private string m_strConn;
        public StudentExaminationPaperBLL(string strConn)
        {
            m_strConn = strConn;
            ExercisesTestbll = new ExercisesTestBLL(m_strConn);
            StudentExambll = new StudentExamBLL(m_strConn);
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



        public bool IsExist(int ExercisesTestId, int StudentExamId, int TitleInfoId)
        {
            string strSql = @"select 1 from StudentExaminationPaper  where 
            ExercisesTestId=@ExercisesTestId and StudentExamId=@StudentExamId and TitleInfoId=@TitleInfoId ";
            object obj =DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteScalar(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=ExercisesTestId, ParameterName="@ExercisesTestId"},
                new SQLiteParameter(){  Value=StudentExamId, ParameterName="@StudentExamId"},
                new SQLiteParameter(){  Value=TitleInfoId, ParameterName="@TitleInfoId"}
            });
            return obj != null ? true : false;
        }

        public int AddExamInfo(int ExercisesTestId, int StudentExamId, int TitleInfoId, int StudentAnswer)
        {
            string strSql = "";
            ExercisesTest? t =ExercisesTestbll.GetExercisesTest(ExercisesTestId);
            if (t != null && t.Value.IsOver > 0)
            {
                //考试已经结束
                return -200;
            }
            if (IsExist(ExercisesTestId, StudentExamId, TitleInfoId))
            {
                //修改
                strSql = "update StudentExaminationPaper set StudentAnswer=@StudentAnswer,UpdateDateTime=@UpdateDateTime where ExercisesTestId=@ExercisesTestId and StudentExamId=@StudentExamId  and TitleInfoId=@TitleInfoId";
                int result = DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteNonQuery(strSql, new DbParameter[]{
                    new SQLiteParameter(){  Value=ExercisesTestId, ParameterName="@ExercisesTestId"},
                    new SQLiteParameter(){  Value=StudentExamId, ParameterName="@StudentExamId"},
                    new SQLiteParameter(){  Value=TitleInfoId, ParameterName="@TitleInfoId"},
                    new SQLiteParameter(){  Value=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ParameterName="@UpdateDateTime"},
                    new SQLiteParameter(){  Value=StudentAnswer, ParameterName="@StudentAnswer"}
                });
                return result;
            }
            else
            {
                //增加
                strSql = "insert into StudentExaminationPaper(StudentAnswer,UpdateDateTime,ExercisesTestId,StudentExamId,TitleInfoId) values(@StudentAnswer,@UpdateDateTime,@ExercisesTestId,@StudentExamId,@TitleInfoId)";
                int result = DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteNonQuery(strSql, new DbParameter[]{
                    new SQLiteParameter(){  Value=ExercisesTestId, ParameterName="@ExercisesTestId"},
                    new SQLiteParameter(){  Value=StudentExamId, ParameterName="@StudentExamId"},
                    new SQLiteParameter(){  Value=TitleInfoId, ParameterName="@TitleInfoId"},
                    new SQLiteParameter(){  Value=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ParameterName="@UpdateDateTime"},
                    new SQLiteParameter(){  Value=StudentAnswer, ParameterName="@StudentAnswer"}
                });
                return result;
            }

        }


        /// <summary>
        /// 获取考试信息(包括学生信息 和学生答题情况)
        /// </summary>
        /// <param name="ExercisesTestId"></param>
        /// <param name="StudentId"></param>
        /// <returns></returns>
        public ExamInfo GetExamInfo(int ExercisesTestId, int StudentId)
        {

            ExercisesTest? info1 = ExercisesTestbll.GetExercisesTest(ExercisesTestId);
            StudentExam? info2 = StudentExambll.GetStudentExam(StudentId);
            ExamInfo info = new ExamInfo();
            if (info1 != null && info2!=null)
            {
                info.ExercisesDescribe = info1.Value.ExercisesDescribe;
                info.ExercisesName = info1.Value.ExercisesName;
                info.ExercisesTestId = info1.Value.ExercisesTestId;
                info.TotleScore = ExercisesTestbll.GetExercisesTestTotleScore(info.ExercisesTestId);
                info.IsOver = info1.Value.IsOver;


                info.StudentExamId = info2.Value.StudentExamId;
                info.StudentName = info2.Value.StudentName;
                info.StudentNumber = info2.Value.StudentNumber;
                info.StudentScore = StudentExambll.GetStudentScore(info.StudentExamId, info.ExercisesTestId);
                info.ListExamTitle = GetExamTitleInfo(info.ExercisesTestId, info.StudentExamId);
            }

            
            return info;

//            string strSql = @"select a.ExercisesTestId,a.ExercisesName,a.ExercisesDescribe,c.StudentExamId,c.StudentNumber,c.StudentName from ExercisesTest a 
//                            left join StudentExaminationPaper b on a.ExercisesTestId=b.ExercisesTestId 
//                            left join StudentExam c on b.StudentExamId=c.StudentExamId 
//                            where a.ExercisesTestId=@ExercisesTestId and c.StudentExamId=@StudentExamId";
//            return DataTableToExamInfo(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
//                new SQLiteParameter(){  Value=ExercisesTestId, ParameterName="@ExercisesTestId"},
//                new SQLiteParameter(){  Value=StudentId, ParameterName="@StudentExamId"}
//            }));

        }


        public List<ExamTitleInfo> GetExamTitleInfo(int ExercisesTestId, int StudentId)
        {
            //c.Score 该题分数非学生得分
//            string strSql = @"select c.TitleInfoId,c.TitleConent,c.CorrectAnswer,c.Score,c.ExercisesTitleIndex,c.ExercisesTestId,d.StudentAnswer from 
//(select b.TitleInfoId,b.TitleConent,b.CorrectAnswer,b.Score,a.ExercisesTitleIndex,a.ExercisesTestId from ExercisesTitle a,TitleInfo b 
//where a.ExercisesTestId=@ExercisesTestId and a.TitleInfoId=b.TitleInfoId order by a.ExercisesTitleIndex asc) c 
//left join StudentExaminationPaper d on c.ExercisesTestId=d.ExercisesTestId and c.TitleInfoId=d.TitleInfoId and d.StudentExamId=@StudentExamId";
            string strSql = @"select c.TitleInfoId,c.TitleConent,c.CorrectAnswer,c.Score,c.ExercisesTitleIndex,c.ExercisesTestId,d.StudentAnswer,e.TitleTypeName from 
(select b.TitleInfoId,b.TitleConent,b.CorrectAnswer,b.Score,a.ExercisesTitleIndex,a.ExercisesTestId,b.TitleTypeId from ExercisesTitle a,TitleInfo b 
where a.ExercisesTestId=@ExercisesTestId and a.TitleInfoId=b.TitleInfoId order by a.ExercisesTitleIndex asc) c 
left join TitleType e on c.TitleTypeId=e.TiteTypeId
left join StudentExaminationPaper d on c.ExercisesTestId=d.ExercisesTestId and c.TitleInfoId=d.TitleInfoId and d.StudentExamId=@StudentExamId";
            return DataTableToExamTitleInfo(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=ExercisesTestId, ParameterName="@ExercisesTestId"},
                new SQLiteParameter(){  Value=StudentId, ParameterName="@StudentExamId"}
            }));
        }


        public List<ExamItemInfo> GetExamItemInfo(int TitleInfoId)
        {
            string strSql = @"select * from TitleItemAssoc a,TitleItem b where a.TitleInfoId=@TitleInfoId and a.TitleItemId=b.TitleItemId order by a.TitleItemIndex asc";
            return DataTableToExamItemInfo(DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=TitleInfoId, ParameterName="@TitleInfoId"}
            }));
        }

        private List<ExamItemInfo> DataTableToExamItemInfo(DataTable dt)
        {
            List<ExamItemInfo> listInfo = new List<ExamItemInfo>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ExamItemInfo info = new ExamItemInfo();
                    info.ExamItemId = dr["TitleItemId"] == DBNull.Value ? -100 : Convert.ToInt32(dr["TitleItemId"]);
                    info.TitleItemIndex = dr["TitleItemIndex"] == DBNull.Value ? -100 : Convert.ToInt32(dr["TitleItemIndex"]);
                    info.ExamItemName = dr["TitleItemContent"] == DBNull.Value ? "" : dr["TitleItemContent"].ToString();
                    listInfo.Add(info);
                }
            }
            return listInfo;
        }

        private List<ExamTitleInfo> DataTableToExamTitleInfo(DataTable dt)
        {
            List<ExamTitleInfo> listInfo = new List<ExamTitleInfo>();
            if (dt!=null)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    ExamTitleInfo info = new ExamTitleInfo();

                    info.TitleInfoId = dr.ItemArray[0] == DBNull.Value ? -100 : Convert.ToInt32(dr.ItemArray[0]);
                    info.TitleConent = dr.ItemArray[1] == DBNull.Value ? "" : dr.ItemArray[1].ToString();
                    info.CorrectAnswer = dr.ItemArray[2] == DBNull.Value ? -100 : Convert.ToInt32(dr.ItemArray[2]);
                    info.StudentAnswer = dr.ItemArray[6] == DBNull.Value ? -100 : Convert.ToInt32(dr.ItemArray[6]);
                    info.TitleTypeName = dr.ItemArray[7] == DBNull.Value ? "" : dr.ItemArray[7].ToString();
                    if (info.CorrectAnswer != -100 && info.StudentAnswer != -100 && info.CorrectAnswer == info.StudentAnswer)
                    {
                        info.Score = dr.ItemArray[3] == DBNull.Value ? -100 : Convert.ToInt32(dr.ItemArray[3]);
                    }
                    info.ListExamItem = GetExamItemInfo(info.TitleInfoId);

                    //info.TitleInfoId = dr["TitleInfoId"] == DBNull.Value ? -100 : Convert.ToInt32(dr["TitleInfoId"]);
                    //info.TitleConent = dr["TitleConent"] == DBNull.Value ? "" : dr["TitleConent"].ToString();
                    //info.CorrectAnswer = dr["CorrectAnswer"] == DBNull.Value ? -100 : Convert.ToInt32(dr["CorrectAnswer"]);
                    //info.StudentAnswer = dr["StudentAnswer"] == DBNull.Value ? -100 : Convert.ToInt32(dr["StudentAnswer"]);
                    //if (info.CorrectAnswer != -100 && info.StudentAnswer != -100 && info.CorrectAnswer == info.StudentAnswer)
                    //{
                    //    info.Score = dr["Score"] == DBNull.Value ? -100 : Convert.ToInt32(dr["Score"]);
                    //}
                    //info.ListExamItem = GetExamItemInfo(info.TitleInfoId);
                    listInfo.Add(info);
                }
            }
            return listInfo;
        }


        private ExamInfo DataTableToExamInfo(DataTable dt)
        {
            ExamInfo info = new ExamInfo();

            if (dt!=null&&dt.Rows.Count>0)
            {
                info.ExercisesDescribe = dt.Rows[0]["ExercisesDescribe"] == DBNull.Value ? "" : dt.Rows[0]["ExercisesDescribe"].ToString();
                info.ExercisesTestId = dt.Rows[0]["ExercisesTestId"] == DBNull.Value ? -100 : Convert.ToInt32(dt.Rows[0]["ExercisesTestId"]);
                info.ExercisesName = dt.Rows[0]["ExercisesName"] == DBNull.Value ? "" : dt.Rows[0]["ExercisesName"].ToString();
                info.StudentExamId = dt.Rows[0]["StudentExamId"] == DBNull.Value ? -100 : Convert.ToInt32(dt.Rows[0]["StudentExamId"]);
                info.StudentName = dt.Rows[0]["StudentName"] == DBNull.Value ? "" : dt.Rows[0]["StudentName"].ToString();
                info.StudentNumber = dt.Rows[0]["StudentNumber"] == DBNull.Value ? "" : dt.Rows[0]["StudentNumber"].ToString();
                info.StudentScore = StudentExambll.GetStudentScore(info.StudentExamId, info.ExercisesTestId);
                info.TotleScore = ExercisesTestbll.GetExercisesTestTotleScore(info.ExercisesTestId);

                info.ListExamTitle = GetExamTitleInfo(info.ExercisesTestId, info.StudentExamId);
            }


            return info;
        }


    }
}
