using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Management;
using OilSimulationModel;
using EclipseUtils;

namespace OilSimulationController
{
    [HandleError]
    public class HomeController : Controller
    {
        //计算机逻辑内核数量
        int iCoreCount = Environment.ProcessorCount;
        private static List<stMultiTread> lstThread = new List<stMultiTread>(); 
        
        public ActionResult Index()
        {
            //存储用户名
            Session["userid"] = "";
            string szMac = CommonModel.GetMACID();
            ViewData["szMac"] = szMac;
            if (lstThread.Count >= iCoreCount)
            {
                int a = 0;
            }
            else
            {
                lstThread.Add(new stMultiTread());
            } 
            
            return View();
        }
        public ActionResult BaseIndex()
        {
            string szMac = CommonModel.GetMACID();
            ViewData["szMac"] = szMac;
            if (lstThread.Count >= iCoreCount)
            {
                int a = 0;
            }
            else
            {
                lstThread.Add(new stMultiTread());
            }

            return View();
        }
        public ActionResult VirtualIndex()
        {
            string szMac = CommonModel.GetMACID();
            ViewData["szMac"] = szMac;
            if (lstThread.Count >= iCoreCount)
            {
                int a = 0;
            }
            else
            {
                lstThread.Add(new stMultiTread());
            }

            return View();
        }
        public ActionResult SimulationIndex()
        {
            string szMac = CommonModel.GetMACID();
            ViewData["szMac"] = szMac;
            if (lstThread.Count >= iCoreCount)
            {
                int a = 0;
            }
            else
            {
                lstThread.Add(new stMultiTread());
            }

            return View();
        }
        public ActionResult InnovateIndex()
        {
            string szMac = CommonModel.GetMACID();
            ViewData["szMac"] = szMac;
            if (lstThread.Count >= iCoreCount)
            {
                int a = 0;
            }
            else
            {
                lstThread.Add(new stMultiTread());
            }

            return View();
        }
        /// <summary>
        /// 水驱油模拟
        /// </summary>
        /// <returns></returns>
        public ActionResult BaseOne()
        { 

            return View();
        }
        /// <summary>
        /// 渗流方式模拟
        /// </summary>
        /// <returns></returns>
        public ActionResult BaseTwo()
        {

            return View();
        }
        /// <summary>
        /// 弹性不稳定渗流模拟
        /// </summary>
        /// <returns></returns>
        public ActionResult BaseThree()
        {

            return View();
        }



        /// <summary>
        /// 非活塞式水驱油影响因素实验
        /// </summary>
        /// <returns></returns>
        public ActionResult VirtualOne()
        {

            return View();
        }
        /// <summary>
        /// 水驱油效率实验
        /// </summary>
        /// <returns></returns>
        public ActionResult VirtualTwo()
        {

            return View();
        }
        /// <summary>
        /// 采收率实验
        /// </summary>
        /// <returns></returns>
        public ActionResult VirtualThree()
        {

            return View();
        }


        /// <summary>
        /// 井网井距方案设计与开发效果预测
        /// </summary>
        /// <returns></returns>
        public ActionResult EmulateOne()
        {

            return View();
        }
        /// <summary>
        /// 注采系统方案设计与开发效果预测
        /// </summary>
        /// <returns></returns>
        public ActionResult EmulateTwo()
        {

            return View();
        }


        /// <summary>
        /// 油藏开发方案实践
        /// </summary>
        /// <returns></returns>
        public ActionResult InnovateOne()
        {

            return View();
        }
        /// <summary>
        /// 气藏开发方案实践
        /// </summary>
        /// <returns></returns>
        public ActionResult InnovateTwo()
        {

            return View();
        }
    }
}
