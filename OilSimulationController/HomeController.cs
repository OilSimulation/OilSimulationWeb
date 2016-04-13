using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Management;
using OilSimulationModel;

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

    }
}
