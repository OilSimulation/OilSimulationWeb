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
        public ActionResult ViewPageFunc1()
        {
            //Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
             
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult BaseModeOne()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            string szGridFilePath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/水驱油效率实验/不同原油密度/gao1.15/GAOMI_E100.EGRID");
            EclipseModel gridMode = EclipseParser.ParseEgrid(szGridFilePath);
            string initFilename = Path.ChangeExtension(szGridFilePath, ".INIT");
            EclipseParser.ValidateEclipseFile(initFilename);
            string[] staticProps = EclipseParser.GetPropKeywordsFromInit(initFilename);


            //动态关键字
            string[] dynamicPropsOld;
            DateTime[] timesteps;

            string unrstFilename = Path.ChangeExtension(szGridFilePath, ".UNRST");
            if (System.IO.File.Exists(unrstFilename))   // 从UNRST文件里读动态属性
            {
                dynamicPropsOld = EclipseParser.GetPropKeywordsFromUnrst(unrstFilename);
                timesteps = EclipseParser.GetTimeStepsFromUnrst(unrstFilename);
                // 从unrst中也可以读出网格信息，应该与EGRID文件中的网格信息一致
                EclipseModel eclModel = EclipseParser.GetGridInfoFromUnrst(unrstFilename);

                if (eclModel.nx != eclModel.nx || eclModel.ny != eclModel.ny || eclModel.nz != eclModel.nz)
                {
                    return null;
                }
            }
            else   // 从一堆X0000, X0001 ...这样的文件中读取动态属性关键字和时间步
            {
                dynamicPropsOld = EclipseParser.GetPropKeywordsFromX(szGridFilePath);
                timesteps = EclipseParser.GetTimeStepsFromX(szGridFilePath);
                // 统计一下类似*.X0000, *.X0001 ...这样文件的个数，此个数就是时间步的个数
                int countXFiles = EclipseParser.CountXFiles(szGridFilePath);
            }
            string[] dynamicProps = new string[dynamicPropsOld.Length + 1];
            for (int x = 0; x < dynamicPropsOld.Length; ++x)
            {
                dynamicProps[x] = dynamicPropsOld[x];
            }
            dynamicProps[dynamicPropsOld.Length] = "SOIL";
            //iDynPareCount = dynamicProps.Length; 

            List<string> lst = new List<string>();
            lst.AddRange(dynamicProps);
            lst.AddRange(staticProps); 
            ViewData["DynamicProps"] = lst;
             
            //ViewData["StaticProps"] = staticProps;

            string[] szTimeStep = new string[timesteps.Length];
            for (int i = 0; i < timesteps.Length; ++i)
            {
                szTimeStep[i] = string.Format("{0:yyyy-MM-dd}", timesteps[i]);
            }
            ViewData["TimeStep"] = szTimeStep;


            ViewData["TotalGrids"] = gridMode.TotalGrids;
             
            return View();
        }
    }
}
