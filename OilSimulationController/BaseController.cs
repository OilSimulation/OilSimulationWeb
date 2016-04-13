using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; 
using System.IO;


namespace OilSimulationController 
{
    [HandleError]
    public class BaseController : Controller
    {
        public ActionResult ViewPageFunc1()
        {
            //Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
             
            return View();
        }
    }
}
