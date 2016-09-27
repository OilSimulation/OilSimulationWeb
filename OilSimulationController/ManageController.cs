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
        public static string strConn = @"Data Source =" + HttpRuntime.AppDomainAppPath + "DBFile\\DB.db";

        ExercisesTestBLL ExercisesTestbll = new ExercisesTestBLL(strConn);
        ExperimentTypeBLL ExperimentTypebll = new ExperimentTypeBLL(strConn);
        TitleInfoBLL TitleInfobll = new TitleInfoBLL(strConn);
        TitleTypeBLL TitleTypebll = new TitleTypeBLL(strConn);
        TitleItemBLL TitleItembll = new TitleItemBLL(strConn);
        TitleItemAssocBLL TitleItemAssocbll = new TitleItemAssocBLL(strConn);
        ExercisesTitleBLL ExercisesTitlebll = new ExercisesTitleBLL(strConn);

        public ManageController()
        {

        }


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


        /// <summary>
        /// 根据实验类型获取题目
        /// </summary>
        /// <param name="TypeId">实验类型ID</param>
        /// <returns></returns>
        public ActionResult GetTitleInfoTypeId(stId TypeId)
        {
            
            List<TitleInfo> listData = TitleInfobll.GetTitleInfoTypeId(TypeId.Id);
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

        #region *************************************** 选项管理TitleItem

        public ActionResult AddTitleItemWeb()
        {
            return View();
        }

        public ActionResult TitleItemWeb()
        {
            return View();
        }

        public ActionResult AddTitleItem(TitleItem info)
        {
            int result = TitleItembll.AddTitleItem(info);
            var res = new ConfigurableJsonResult();
            res.Data = result;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;
        }

        public ActionResult DelTitleItem(stId info)
        {
            int result = TitleItembll.DelTitleItem(info.Id);
            var res = new ConfigurableJsonResult();
            res.Data = result;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;
        }

        public ActionResult UpdateTitleItem(TitleItem info)
        {
            int result = TitleItembll.UpdateTitleItem(info);
            var res = new ConfigurableJsonResult();
            res.Data = result;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;
        }

        public ActionResult GetTitleItem()
        {
            List<TitleItem>  result = TitleItembll.GetTitleItem();
            var res = new ConfigurableJsonResult();
            res.Data = result;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;
        }
        public ActionResult GetTitleItemId(stId info)
        {
            TitleItem result = TitleItembll.GetTitleItem(info.Id);
            var res = new ConfigurableJsonResult();
            res.Data = result;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;
        }

        public ActionResult IsExistTitleItem(TitleItem info)
        {
            bool result = TitleItembll.IsExistTitleItem(info);
            var res = new ConfigurableJsonResult();
            res.Data = result == true ? 1 : 0;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;

        }



        #endregion


        #region *************************************** 题目与选项TitleItemAssocWeb


        public ActionResult AddTitleItemAssocWeb()
        {
            return View();
        }


        public ActionResult TitleItemAssocWeb()
        {
            return View();
        }


        /// <summary>
        /// 获取题目下的所有选项
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult GetTitleInfoItem(stId info)
        {
            List<TitleItemAssoc> result = TitleItemAssocbll.GetTitleInfoItem(info.Id);
            var res = new ConfigurableJsonResult();
            res.Data = result;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;

        }


        /// <summary>
        /// 获取题目中最大的索引+1
        /// </summary>
        /// <param name="info">TitleInfoId(题目ID)</param>
        /// <returns></returns>
        public ActionResult GetTitleItemAssocIndex(stId info)
        {
            int result = TitleItemAssocbll.GetTitleItemAssocIndex(info.Id);
            var res = new ConfigurableJsonResult();
            res.Data = result;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;
        }

        /// <summary>
        /// 删除题目下的选项
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult DelTitleItemAssoc(stId info)
        {
            int result = TitleItemAssocbll.DelTitleItemAssoc(info.Id);
            var res = new ConfigurableJsonResult();
            res.Data = result;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;

        }

        /// <summary>
        /// 增加题目下的选项
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult AddTitleItemAssoc(TitleItemAssoc info)
        {
            int result = TitleItemAssocbll.AddTitleItemAssoc(info);
            var res = new ConfigurableJsonResult();
            res.Data = result;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;

        }

        public ActionResult IsExistTitleItemAssoc(TitleItemAssoc info)
        {
            bool result = TitleItemAssocbll.IsExistTitleItemAssoc(info.TitleInfoId, info.TitleItemId);
            var res = new ConfigurableJsonResult();
            res.Data = result == true ? 1 : 0;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;

        }

        //public ActionResult 

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

        #region *************************************** 习题或考试与题目对照表(ExercisesTitle)

        public ActionResult AddExercisesTitleWeb()
        {
            return View();
        }

        public ActionResult ExercisesTitleWeb()
        {
            return View();
        }


        /// <summary>
        /// 获取考试 中 题目位置的最大位置+1
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult GetExercisesTitleMaxIndex(stId info)
        {
            int result = ExercisesTitlebll.GetExercisesTitleMaxIndex(info.Id);
            var res = new ConfigurableJsonResult();
            res.Data = result;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;

        }


        /// <summary>
        /// 判断 考试中是否存在某题目
        /// </summary>
        /// <param name="info">只取考试ID与题目ID</param>
        /// <returns></returns>
        public ActionResult IsExistExercisesTitle(ExercisesTitle info)
        {
            bool result = ExercisesTitlebll.IsExistExercisesTitle(info.ExercisesTestId,info.TitleInfoId);
            var res = new ConfigurableJsonResult();
            res.Data = result == true ? 1 : 0;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;
        }

        /// <summary>
        /// 获取某场考试下的所有题目
        /// </summary>
        /// <param name="info">考试ID</param>
        /// <returns></returns>
        public ActionResult GetExercisesTitle(stId info)
        {
            List<ExercisesTitle> result = ExercisesTitlebll.GetExercisesTitle(info.Id);
            var res = new ConfigurableJsonResult();
            res.Data = result;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;

        }

        public ActionResult DelExercisesTitle(stId info)
        {
            int result = ExercisesTitlebll.DelExercisesTitle(info.Id);
            var res = new ConfigurableJsonResult();
            res.Data = result;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;
        }

        public ActionResult UpdateExercisesTitle(ExercisesTitle info)
        {
            int result = ExercisesTitlebll.UpdateExercisesTitle(info);
            var res = new ConfigurableJsonResult();
            res.Data = result;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;
        }

        public ActionResult AddExercisesTitle(ExercisesTitle info)
        {
            int result = ExercisesTitlebll.AddExercisesTitle(info);
            var res = new ConfigurableJsonResult();
            res.Data = result;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;
        }

        #endregion


    }
}
