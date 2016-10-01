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
        public ActionResult ExamPaper()
        {
            return View();
        }

        public ActionResult StudentLogin()
        {
            return View();
        }

        #region *************************************** 试卷信息

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


        #endregion

    }
}
