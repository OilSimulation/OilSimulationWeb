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
        public ActionResult ExamPaper()
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




        #endregion

    }
}
