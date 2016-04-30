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
            string szGridFilePath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/活塞式驱油/MODEL1D_E100.EGRID");
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps; 
            ViewData["TimeStep"] = stPageParams.timeSteps; 
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;
             
            return View();
        }

        /// <summary>
        /// 演示非活塞式水驱油过程
        /// </summary>
        /// <returns></returns>
        public ActionResult BaseModeTwo()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/非活塞式驱油/MODEL1D_E100.EGRID");
            //string szGridFilePath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/单向流/MODEL1D_E100.EGRID");
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }

        /// <summary>
        /// 演示单向流动过程
        /// </summary>
        /// <returns></returns>
        public ActionResult BaseModeThree()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/单向流/MODEL1D_E100.EGRID");
            //string szGridFilePath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/稳定水压恒压边界/MODEL2D_E100.EGRID");
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }

        /// <summary>
        /// 演示径向流动过程
        /// </summary>
        /// <returns></returns>
        public ActionResult BaseModeFour()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/平面径向流/PINGMIAN_E100.EGRID");
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }

        /// <summary>
        /// 演示球面向心流动过程
        /// </summary>
        /// <returns></returns>
        public ActionResult BaseModeFive()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache); 
            string szGridFilePath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/单向流/MODEL1D_E100.EGRID");
            
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }

        /// <summary>
        /// 岩石水压弹性驱动过程中的压力波传播及变化规律
        /// </summary>
        /// <returns></returns>
        public ActionResult BaseModeSix()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/稳定水压恒压边界/MODEL2D_E100.EGRID");
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }

        /// <summary>
        /// 演示封闭弹性驱动过程中的压力波传播及变化规律
        /// </summary>
        /// <returns></returns>
        public ActionResult BaseModeSeven()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/封闭边界/MODEL2D_E100.EGRID");
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }

    }
}
