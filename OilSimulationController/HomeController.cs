using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Management;
using OilSimulationModel;
using EclipseUtils;
using DBHelper.Bll;
using DBHelper.Model;

namespace OilSimulationController
{
    [HandleError]
    public class HomeController : Controller
    {
        public static string strConn = @"Data Source =" + HttpRuntime.AppDomainAppPath + "DBFile\\DB.db;password = zhizaiz,xmcnvb";
        //计算机逻辑内核数量
        int iCoreCount = Environment.ProcessorCount;
        private static List<stMultiTread> lstThread = new List<stMultiTread>();
        private static string szVerInfo = "WXP1AC5ETYZ4";
        PeriodTotalBLL PeriodTotalBll = new PeriodTotalBLL(strConn); 
        /// <summary>
        /// 试用到期页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ExpireInfo()
        {
            return View();
        }
          

        public ActionResult Index()
        {
            if ( PeriodTotalBll.IsPeriod(szVerInfo) ) return RedirectToAction("ExpireInfo", "Home");
            //存储用户名
            Session["userid"] = ""; 
            return View();
        }
        public ActionResult BaseIndex()
        {
            if ( PeriodTotalBll.IsPeriod(szVerInfo) ) return RedirectToAction("ExpireInfo", "Home");
            return View();
        }
        public ActionResult VirtualIndex()
        {
            if ( PeriodTotalBll.IsPeriod(szVerInfo) ) return RedirectToAction("ExpireInfo", "Home");
            return View();
        }
        public ActionResult SimulationIndex()
        {
            if (  PeriodTotalBll.IsPeriod(szVerInfo) ) return RedirectToAction("ExpireInfo", "Home");
            return View();
        }
        public ActionResult InnovateIndex()
        {
            if ( PeriodTotalBll.IsPeriod(szVerInfo) ) return RedirectToAction("ExpireInfo", "Home");
            return View();
        }
        /// <summary>
        /// 水驱油模拟
        /// </summary>
        /// <returns></returns>
        public ActionResult BaseOne()
        { 
            if ( PeriodTotalBll.IsPeriod(szVerInfo) ) return RedirectToAction("ExpireInfo", "Home");
            return View();
        }
        /// <summary>
        /// 渗流方式模拟
        /// </summary>
        /// <returns></returns>
        public ActionResult BaseTwo()
        { 
            if ( PeriodTotalBll.IsPeriod(szVerInfo) ) return RedirectToAction("ExpireInfo", "Home");
            return View();
        }
        /// <summary>
        /// 弹性不稳定渗流模拟
        /// </summary>
        /// <returns></returns>
        public ActionResult BaseThree()
        {
            if ( PeriodTotalBll.IsPeriod(szVerInfo) ) return RedirectToAction("ExpireInfo", "Home");
            return View();
        }



        /// <summary>
        /// 非活塞式水驱油影响因素实验
        /// </summary>
        /// <returns></returns>
        public ActionResult VirtualOne()
        {
            if ( PeriodTotalBll.IsPeriod(szVerInfo) ) return RedirectToAction("ExpireInfo", "Home");
            return View();
        }
        /// <summary>
        /// 水驱油效率实验
        /// </summary>
        /// <returns></returns>
        public ActionResult VirtualTwo()
        {
            if ( PeriodTotalBll.IsPeriod(szVerInfo) ) return RedirectToAction("ExpireInfo", "Home");
            return View();
        }
        /// <summary>
        /// 采收率实验
        /// </summary>
        /// <returns></returns>
        public ActionResult VirtualThree()
        {
            if ( PeriodTotalBll.IsPeriod(szVerInfo) ) return RedirectToAction("ExpireInfo", "Home");
            return View();
        }


        /// <summary>
        /// 井网井距方案设计与开发效果预测
        /// </summary>
        /// <returns></returns>
        public ActionResult EmulateOne()
        {
            if ( PeriodTotalBll.IsPeriod(szVerInfo) ) return RedirectToAction("ExpireInfo", "Home");
            return View();
        }
        /// <summary>
        /// 注采系统方案设计与开发效果预测
        /// </summary>
        /// <returns></returns>
        public ActionResult EmulateTwo()
        {
            if ( PeriodTotalBll.IsPeriod(szVerInfo) ) return RedirectToAction("ExpireInfo", "Home");
            return View();
        }


        /// <summary>
        /// 油藏开发方案实践
        /// </summary>
        /// <returns></returns>
        public ActionResult InnovateOne()
        {
            if ( PeriodTotalBll.IsPeriod(szVerInfo) ) return RedirectToAction("ExpireInfo", "Home");
            return View();
        }
        /// <summary>
        /// 气藏开发方案实践
        /// </summary>
        /// <returns></returns>
        public ActionResult InnovateTwo()
        {
            if ( PeriodTotalBll.IsPeriod(szVerInfo) ) return RedirectToAction("ExpireInfo", "Home");
            return View();
        }
    }
}
