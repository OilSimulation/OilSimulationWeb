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
        /// 均质气藏
        /// </summary>
        /// <returns></returns>
        public ActionResult InnovateModeOne()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/活塞式驱油/MODEL1D_E100.EGRID");
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }
    }
}
