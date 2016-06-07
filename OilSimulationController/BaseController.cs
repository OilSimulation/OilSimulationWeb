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
    public class BaseController : Controller
    {  
        /// <summary>
        /// 演示活塞式水驱油过程
        /// </summary>
        /// <returns></returns>
        public ActionResult BaseModeOne()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(11);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps; 
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;
            ViewData["fGetPercent"] = stPageParams.fGetPercent;
            ViewData["lstDays"] = stPageParams.lstDays;
            ViewData["lstFLPT"] = stPageParams.lstFLPT;
            ViewData["lstFOPT"] = stPageParams.lstFOPT;
            ViewData["lstFPR"] = stPageParams.lstFPR;
            ViewData["lstFWIT"] = stPageParams.lstFWIT;
            ViewData["lstFWPT"] = stPageParams.lstFWPT;
             
            return View();
        }

        /// <summary>
        /// 演示非活塞式水驱油过程
        /// </summary>
        /// <returns></returns>
        public ActionResult BaseModeTwo()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(12);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;
            ViewData["fGetPercent"] = stPageParams.fGetPercent;
            ViewData["lstDays"] = stPageParams.lstDays;
            ViewData["lstFLPT"] = stPageParams.lstFLPT;
            ViewData["lstFOPT"] = stPageParams.lstFOPT;
            ViewData["lstFPR"] = stPageParams.lstFPR;
            ViewData["lstFWIT"] = stPageParams.lstFWIT;
            ViewData["lstFWPT"] = stPageParams.lstFWPT;

            return View();
        }

        /// <summary>
        /// 演示单向流动过程
        /// </summary>
        /// <returns></returns>
        public ActionResult BaseModeThree()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(13);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;
            ViewData["fGetPercent"] = stPageParams.fGetPercent;
            ViewData["lstDays"] = stPageParams.lstDays;
            ViewData["lstFLPT"] = stPageParams.lstFLPT;
            ViewData["lstFOPT"] = stPageParams.lstFOPT;
            ViewData["lstFPR"] = stPageParams.lstFPR;
            ViewData["lstFWIT"] = stPageParams.lstFWIT;
            ViewData["lstFWPT"] = stPageParams.lstFWPT;

            return View();
        }

        /// <summary>
        /// 演示径向流动过程
        /// </summary>
        /// <returns></returns>
        public ActionResult BaseModeFour()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(14);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;
            ViewData["fGetPercent"] = stPageParams.fGetPercent;
            ViewData["lstDays"] = stPageParams.lstDays;
            ViewData["lstFLPT"] = stPageParams.lstFLPT;
            ViewData["lstFOPT"] = stPageParams.lstFOPT;
            ViewData["lstFPR"] = stPageParams.lstFPR;
            ViewData["lstFWIT"] = stPageParams.lstFWIT;
            ViewData["lstFWPT"] = stPageParams.lstFWPT;

            return View();
        }

        /// <summary>
        /// 演示球面向心流动过程
        /// </summary>
        /// <returns></returns>
        public ActionResult BaseModeFive()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(15);
            
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;
            ViewData["fGetPercent"] = stPageParams.fGetPercent;
            ViewData["lstDays"] = stPageParams.lstDays;
            ViewData["lstFLPT"] = stPageParams.lstFLPT;
            ViewData["lstFOPT"] = stPageParams.lstFOPT;
            ViewData["lstFPR"] = stPageParams.lstFPR;
            ViewData["lstFWIT"] = stPageParams.lstFWIT;
            ViewData["lstFWPT"] = stPageParams.lstFWPT;

            return View();
        }

        /// <summary>
        /// 岩石水压弹性驱动过程中的压力波传播及变化规律
        /// </summary>
        /// <returns></returns>
        public ActionResult BaseModeSix()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(16);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;
            ViewData["fGetPercent"] = stPageParams.fGetPercent;
            ViewData["lstDays"] = stPageParams.lstDays;
            ViewData["lstFLPT"] = stPageParams.lstFLPT;
            ViewData["lstFOPT"] = stPageParams.lstFOPT;
            ViewData["lstFPR"] = stPageParams.lstFPR;
            ViewData["lstFWIT"] = stPageParams.lstFWIT;
            ViewData["lstFWPT"] = stPageParams.lstFWPT;

            return View();
        }

        /// <summary>
        /// 演示封闭弹性驱动过程中的压力波传播及变化规律
        /// </summary>
        /// <returns></returns>
        public ActionResult BaseModeSeven()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(17);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;
            ViewData["fGetPercent"] = stPageParams.fGetPercent;
            ViewData["lstDays"] = stPageParams.lstDays;
            ViewData["lstFLPT"] = stPageParams.lstFLPT;
            ViewData["lstFOPT"] = stPageParams.lstFOPT;
            ViewData["lstFPR"] = stPageParams.lstFPR;
            ViewData["lstFWIT"] = stPageParams.lstFWIT;
            ViewData["lstFWPT"] = stPageParams.lstFWPT;

            return View();
        }

    }
}
