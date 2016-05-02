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

namespace OilSimulationController
{
    [HandleError]
    public class InnovateController : Controller
    {
        /// <summary>
        /// 均质油藏
        /// </summary>
        /// <returns></returns>
        public ActionResult InnModeOne()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(411);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }
        /// <summary>
        /// 实际油藏
        /// </summary>
        /// <returns></returns>
        public ActionResult InnModeTwo()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(412);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }
        /// <summary>
        /// 均质气藏
        /// </summary>
        /// <returns></returns>
        public ActionResult InnModeThree()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(421);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }
        /// <summary>
        /// 实际气藏
        /// </summary>
        /// <returns></returns>
        public ActionResult InnModeFour()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(422);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }
    }
}
