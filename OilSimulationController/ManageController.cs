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
        public static string strConn = @"Data Source =E:\Projects\Code\Oil\OilSimulationWeb\DBFile\DB.db";

        ExercisesTestBLL ExercisesTestbll = new ExercisesTestBLL(strConn);
        ExperimentTypeBLL ExperimentTypebll = new ExperimentTypeBLL(strConn);

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

        public ActionResult GetExperimentTypeById(int id)
        {
            ExperimentType? listData = ExperimentTypebll.GetExperimentType(id);
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
            res.Data = result;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");

            return res;
        }

    }
}
