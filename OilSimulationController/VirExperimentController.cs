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
    public class VirExperimentController : Controller
    {
        /// <summary>
        /// 默认参数非活塞驱油
        /// </summary>
        /// <returns></returns>
        public ActionResult VirtualMode211()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(211);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }
        /// <summary>
        /// 改变毛细管压力
        /// </summary>
        /// <returns></returns>
        public ActionResult VirtualMode212()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(212);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }
        /// <summary>
        /// 改变油水比重
        /// </summary>
        /// <returns></returns>
        public ActionResult VirtualMode213()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(213);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }
        /// <summary>
        /// 改变油水粘度
        /// </summary>
        /// <returns></returns>
        public ActionResult VirtualMode214()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(214);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }
        /// <summary>
        /// 毛细管压力 无毛管
        /// </summary>
        /// <returns></returns>
        public ActionResult VirtualMode2211()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(2211);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }
        /// <summary>
        /// 毛细管压力 低毛管
        /// </summary>
        /// <returns></returns>
        public ActionResult VirtualMode2212()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(2212);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }
        /// <summary>
        /// 毛细管压力 高毛管
        /// </summary>
        /// <returns></returns>
        public ActionResult VirtualMode2213()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(2213);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }
        /// <summary>
        /// 原油粘度级别 低粘度0.5
        /// </summary>
        /// <returns></returns>
        public ActionResult VirtualMode2221()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(2221);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }
        /// <summary>
        /// 原油粘度级别 中粘度5
        /// </summary>
        /// <returns></returns>
        public ActionResult VirtualMode2222()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(2222);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }
        /// <summary>
        /// 原油粘度级别 高密度1.15
        /// </summary>
        /// <returns></returns>
        public ActionResult VirtualMode2223()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(2223);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }
        /// <summary>
        /// 原油密度级别 低密度0.7
        /// </summary>
        /// <returns></returns>
        public ActionResult VirtualMode2231()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(2231);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }
        /// <summary>
        /// 原油密度级别 中密度0.9
        /// </summary>
        /// <returns></returns>
        public ActionResult VirtualMode2232()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(2232);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }
        /// <summary>
        /// 原油密度级别 高密度1.15
        /// </summary>
        /// <returns></returns>
        public ActionResult VirtualMode2233()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(2233);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }
        /// <summary>
        /// 束缚水饱和度 低束缚水0
        /// </summary>
        /// <returns></returns>
        public ActionResult VirtualMode2311()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(2311);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }
        /// <summary>
        /// 束缚水饱和度 中束缚水0.3
        /// </summary>
        /// <returns></returns>
        public ActionResult VirtualMode2312()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(2312);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }
        /// <summary>
        /// 束缚水饱和度 高束缚水0.5
        /// </summary>
        /// <returns></returns>
        public ActionResult VirtualMode2313()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(2313);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }
        /// <summary>
        /// 残余油饱和度 低残余油0
        /// </summary>
        /// <returns></returns>
        public ActionResult VirtualMode2321()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(2321);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }
        /// <summary>
        /// 残余油饱和度 中残余油0.3
        /// </summary>
        /// <returns></returns>
        public ActionResult VirtualMode2322()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(2322);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }
        /// <summary>
        /// 残余油饱和度 高残余油0.5
        /// </summary>
        /// <returns></returns>
        public ActionResult VirtualMode2323()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(2323);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }
        /// <summary>
        /// 油水相渗曲线 常规曲线
        /// </summary>
        /// <returns></returns>
        public ActionResult VirtualMode2331()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(2331);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }
        /// <summary>
        /// 油水相渗曲线 菱形曲线
        /// </summary>
        /// <returns></returns>
        public ActionResult VirtualMode2332()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(2332);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }
        /// <summary>
        /// 油水相渗曲线 星形曲线
        /// </summary>
        /// <returns></returns>
        public ActionResult VirtualMode2333()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = CommonModel.GetModeUriPath(2333);
            PageParams stPageParams = CommonModel.GetPageParams(szGridFilePath);

            ViewData["DynamicProps"] = stPageParams.dynamicProps;
            ViewData["TimeStep"] = stPageParams.timeSteps;
            ViewData["TotalGrids"] = stPageParams.iTotalGrid;

            return View();
        }
    }
}
