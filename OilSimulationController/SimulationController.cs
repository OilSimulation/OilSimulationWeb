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
    public class SimulationController : Controller
    {
        /// <summary>
        /// 井距 200
        /// </summary>
        /// <returns></returns>
        public ActionResult SimMode3111()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(3111);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }

        /// <summary>
        /// 井距 300
        /// </summary>
        /// <returns></returns>
        public ActionResult SimMode3112()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(3112);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }

        /// <summary>
        /// 井距 500
        /// </summary>
        /// <returns></returns>
        public ActionResult SimMode3113()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(3113);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }    
        
        
        /// <summary>
        /// 自定义井路
        /// </summary>
        /// <returns></returns>
        public ActionResult SimMode3114()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(3114);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }

        /// <summary>
        /// 井网 5点
        /// </summary>
        /// <returns></returns>
        public ActionResult SimMode3121()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(3121);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }

        /// <summary>
        /// 井网 7点
        /// </summary>
        /// <returns></returns>
        public ActionResult SimMode3122()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(3122);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }

        /// <summary>
        /// 井网 9点
        /// </summary>
        /// <returns></returns>
        public ActionResult SimMode3123()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(3123);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }   
        
        /// <summary>
        /// 自定义点
        /// </summary>
        /// <returns></returns>
        public ActionResult SimMode3124()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(3124);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }

        /// <summary>
        /// 不同注水时机 早期（同采同注）
        /// </summary>
        /// <returns></returns>
        public ActionResult SimMode3211()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(3211);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }

        /// <summary>
        /// 不同注水时机 中期开发后半年
        /// </summary>
        /// <returns></returns>
        public ActionResult SimMode3212()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(3212);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }

        /// <summary>
        /// 不同注水时机 晚期开发后两年
        /// </summary>
        /// <returns></returns>
        public ActionResult SimMode3213()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(3213);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }

        /// <summary>
        /// 不同注水时机 自定义
        /// </summary>
        /// <returns></returns>
        public ActionResult SimMode3214()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(3214);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }

        /// <summary>
        /// 不同注采比 注采比0.8
        /// </summary>
        /// <returns></returns>
        public ActionResult SimMode3221()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(3221);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }

        /// <summary>
        /// 不同注采比 注采比1
        /// </summary>
        /// <returns></returns>
        public ActionResult SimMode3222()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(3222);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }

        /// <summary>
        /// 不同注采比 注采比1.2
        /// </summary>
        /// <returns></returns>
        public ActionResult SimMode3223()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(3223);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }

        /// <summary>
        /// 不同注采比 自定义
        /// </summary>
        /// <returns></returns>
        public ActionResult SimMode3224()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(3224);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }

        /// <summary>
        /// 不同最大井底注入压力 35MPA
        /// </summary>
        /// <returns></returns>
        public ActionResult SimMode3231()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(3231);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }

        /// <summary>
        /// 不同最大井底注入压力 40MPA
        /// </summary>
        /// <returns></returns>
        public ActionResult SimMode3232()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(3232);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }

        /// <summary>
        /// 不同最大井底注入压力 50MPA
        /// </summary>
        /// <returns></returns>
        public ActionResult SimMode3233()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(3233);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }

        /// <summary>
        /// 不同最大井底注入压力 自定义
        /// </summary>
        /// <returns></returns>
        public ActionResult SimMode3234()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(3234);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }

        /// <summary>
        /// 不同最小井底流压 1MPA
        /// </summary>
        /// <returns></returns>
        public ActionResult SimMode3241()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(3241);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }

        /// <summary>
        /// 不同最小井底流压 5MPA
        /// </summary>
        /// <returns></returns>
        public ActionResult SimMode3242()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(3242);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }

        /// <summary>
        /// 不同最小井底流压 10MPA
        /// </summary>
        /// <returns></returns>
        public ActionResult SimMode3243()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(3243);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }

        /// <summary>
        /// 不同最小井底流压 自定义
        /// </summary>
        /// <returns></returns>
        public ActionResult SimMode3244()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(3244);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }

    }
}
