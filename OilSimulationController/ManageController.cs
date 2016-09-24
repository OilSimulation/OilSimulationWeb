using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using OilSimulationModel;
using EclipseUtils;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using DBHelper.Model;
using DBHelper.Bll;

namespace OilSimulationController
{
    [HandleError]
    public class ManageController : Controller
    {
        //public static string strConn = "Data Source =../DBFile/DB.db";
        public static string strConn = @"Data Source =E:\Projects\Code\XXX\OilSimulationWeb\DBFile\DB.db";

        ExercisesTestBLL ExercisesTestbll = new ExercisesTestBLL(strConn);
        ExperimentTypeBLL ExperimentTypebll = new ExperimentTypeBLL(strConn);
        TitleInfoBLL TitleInfobll = new TitleInfoBLL(strConn);
        TitleTypeBLL TitleTypebll = new TitleTypeBLL(strConn);

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult ExamManage()
        {
            return View();
        }

        public ActionResult Welcome()
        {
            return View();
        }

        


        #region *************************************** 实验类型


        public ActionResult ExperimentTypeWeb()
        {
            return View();
        }

        public ActionResult AddExperimentTypeWeb()
        {
            return View();
        }

        //[HttpPost]
        public ActionResult GetExperimentType()
        {
            List<ExperimentType> listData = ExperimentTypebll.GetExperimentType();
            var res = new ConfigurableJsonResult();
            res.Data = listData;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");

            return res;

        }



        [HttpPost]
        public ActionResult GetExperiment(stId ExperimentTypeId)
        {
            ExperimentType listData = ExperimentTypebll.GetExperimentType(ExperimentTypeId.Id);
            var res = new ConfigurableJsonResult();
            res.Data = listData;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");

            return res;

        }

        //[HttpPost]
//         public ActionResult GetExperimentType(stGetExperimentType data)
//         {
//             List<ExperimentType> listData = ExperimentTypebll.GetExperimentType(data.CurrentPage, data.ShowCount);
//             var res = new ConfigurableJsonResult();
//             res.Data = listData;
//             HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
// 
//             return res;
// 
//         }

        [HttpPost]
        public ActionResult DelExperimentType(stId ExperimentTypeId)
        {
            int result = ExperimentTypebll.DelExperimentType(ExperimentTypeId.Id);
            var res = new ConfigurableJsonResult();
            res.Data = result;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");

            return res;

        }

        [HttpPost]
        public ActionResult UpdateExperimentType(ExperimentType data)
        {

            data.UpdateDateTime = DateTime.Now;
            ExperimentTypebll.UpdateExperimentType(data);

            var res = new ConfigurableJsonResult();
            res.Data = 1;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");

            return res;

        }

        [HttpPost]
        public ActionResult AddExperimentType(ExperimentType data)
        {
            //data.UpdateDateTime = DateTime.Now;
            int result =  ExperimentTypebll.AddExperimentType(data);
            var res = new ConfigurableJsonResult();
            res.Data = result;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");

            return res;

        }
        [HttpPost]
        public ActionResult IsExistData(ExperimentType data)
        {
            bool result = ExperimentTypebll.IsExistData(data);
            var res = new ConfigurableJsonResult();
            res.Data = result == true ? 1 : 0;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");

            return res;
        }
        #endregion

        #region *************************************** 题目管理
        

        public ActionResult TitleInfoWeb()
        {
            return View();
        }

        public ActionResult AddTitleInfoWeb()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetTitleInfo()
        {
            List<TitleInfo> listData = TitleInfobll.GetTitleInfo();
            var res = new ConfigurableJsonResult();
            res.Data = listData;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;
        }

        [HttpPost]
        public ActionResult GetTitleInfoId(stId TitleInfoId)
        {
            TitleInfo data = TitleInfobll.GetTitleInfo(TitleInfoId.Id);
            var res = new ConfigurableJsonResult();
            res.Data = data;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;
        }

        [HttpPost]
        public ActionResult AddTitleInfo(TitleInfo info)
        {
            int result = TitleInfobll.AddTitleInfo(info);
            var res = new ConfigurableJsonResult();
            res.Data = result;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;
        }


        [HttpPost]
        public ActionResult DelTitleInfo(stId info)
        {
            int result = TitleInfobll.DelTitleInfo(info.Id);
            var res = new ConfigurableJsonResult();
            res.Data = result;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;
        }

        [HttpPost]
        public ActionResult UpdateTitleInfo(TitleInfo info)
        {
            int result = TitleInfobll.UpdateTitleInfo(info);
            var res = new ConfigurableJsonResult();
            res.Data = result;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;
        }
        #endregion

        #region *************************************** 题目类型(选择判断)
        [HttpPost]
        public ActionResult GetTitleType()
        {
            List<TitleType> listData = TitleTypebll.GetTitleType();
            var res = new ConfigurableJsonResult();
            res.Data = listData;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;

        }
        #endregion
        #region *************************************** 题目与选项TitleItemAssocWeb

        public ActionResult TitleItemAssocWeb()
        {
            return View();
        }


        #endregion
        //

        #region *************************************** 习题或考试

        public ActionResult ExercisesTestWeb()
        {
            return View();
        }
        public ActionResult AddExercisesTestWeb()
        {
            return View();
        }

        public ActionResult AddExercisesTest(ExercisesTest info)
        {
            int result = ExercisesTestbll.AddExercisesTest(info);
            var res = new ConfigurableJsonResult();
            res.Data = result;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;

        }
        public ActionResult GetExercisesTest()
        {
            List<ExercisesTest> result = ExercisesTestbll.GetExercisesTest();
            var res = new ConfigurableJsonResult();
            res.Data = result;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;
        }
        public ActionResult GetExercisesTestId(stId ExercisesTestId)
        {
            ExercisesTest result = ExercisesTestbll.GetExercisesTest(ExercisesTestId.Id);
            var res = new ConfigurableJsonResult();
            res.Data = result;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;
        }


        public ActionResult DelExercisesTest(stId ExercisesTestId)
        {
            int result = ExercisesTestbll.DelExercisesTest(ExercisesTestId.Id);
            var res = new ConfigurableJsonResult();
            res.Data = result;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;

        }
        public ActionResult UpdateExercisesTest(ExercisesTest info)
        {
            int result = ExercisesTestbll.UpdateExercisesTest(info);
            var res = new ConfigurableJsonResult();
            res.Data = result;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;

        }

        #endregion


    }
}
