using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

using System.IO;
using System.Drawing;
using System.Diagnostics;
using DBHelper.Bll;
using DBHelper.Model;
using OilSimulationModel;


namespace OilSimulationController
{
    public class ExaminationController : Controller
    {
        public static string strConn = @"Data Source =" + HttpRuntime.AppDomainAppPath + "DBFile\\DB.db";

        StudentExaminationPaperBLL StudentExaminationPaperbll = new StudentExaminationPaperBLL(strConn);
        StudentExamBLL StudentExambll = new StudentExamBLL(strConn);
        StudentExamStateBLL StudentExamStatebll = new StudentExamStateBLL(strConn);
        public ActionResult ExamPaper()
        {
            return View();
        }

        public ActionResult StudentLogin()
        {
            return View();
        }

        #region *************************************** 试卷信息

        /// <summary>
        /// 获取学生交卷状态
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult GetStudentExamState(stId2 info)
        {
            StudentExamState? result = StudentExamStatebll.GetStudentExamState(info.Id1, info.Id2);
            var res = new ConfigurableJsonResult();
            res.Data = result;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;

        }


        public ActionResult GetExamInfo(stId2 info)
        {
            ExamInfo result = StudentExaminationPaperbll.GetExamInfo(info.Id1, info.Id2);
            var res = new ConfigurableJsonResult();
            res.Data = result;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;

        }

        public ActionResult GetStudentExamNumber(StudentExam info)
        {
            StudentExam? result = StudentExambll.GetStudentExamNumber(info.StudentNumber);
            var res = new ConfigurableJsonResult();
            res.Data = result;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;
        }



        /// <summary>
        /// 获取学生信息，若学号不存在刚增加，
        /// </summary>
        /// <param name="info">学号，姓名有效</param>
        /// <returns></returns>
        public ActionResult OptStudentNumber(StudentExam info)
        {
            StudentExam? result = StudentExambll.OptStudentNumber(info);
            var res = new ConfigurableJsonResult();
            res.Data = result;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;

        }

        public ActionResult AddExamInfo(StudentItemAnswer info)
        {
            int result = StudentExaminationPaperbll.AddExamInfo(info.ExercisesTestId, info.StudentExamId, info.TitleInfoId, info.StudentAnswer);
            var res = new ConfigurableJsonResult();
            res.Data = result;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;

        }

        public ActionResult EditStudentExamState(StudentExamState info)
        {
            int result = StudentExamStatebll.EditStudentExamState(info.StudentId, info.ExercisesTestId, info.State);
            var res = new ConfigurableJsonResult();
            res.Data = result;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;

        }

        #endregion


        #region *************************************** 登录

        public ActionResult Login(LoginInfo info)
        {
            LoginResult? result = StudentExambll.Login(info.UserName, info.Password, info.Type);
            if (result!=null)
            {
                Session["userid"] = result.Value.UserName;
            }
            var res = new ConfigurableJsonResult();
            res.Data = result;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;
        }

        #endregion

    }
}
