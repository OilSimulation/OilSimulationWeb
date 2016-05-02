using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc; 
using EclipseUtils;
using System.IO;
using System.Drawing;
using System.Diagnostics;

namespace OilSimulationModel
{
    public class CommonModel
    {
        public CommonModel()
        {
        } 

        /// <summary>
        /// 获取访问客户端IP
        /// </summary>
        /// <returns></returns>
        public static string GetMACID()
        {
            //获取网卡硬件地址
            string macAdress = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
             {
                 macAdress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
             }
             else
             {
                 macAdress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
             } 
            return macAdress;
        }

        /// <summary>
        /// 获取模型数据的URI路径
        /// </summary>
        /// <param name="iModeIndex"></param>
        /// <returns></returns>
        public static string GetModeUriPath(int iModeIndex)
        {
            string szUriPath = "";
            switch (iModeIndex)
            {
                //基础认知
                case 11:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/活塞式驱油/MODEL1D_E100.EGRID");
                    break;
                case 12:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/非活塞式驱油/MODEL1D_E100.EGRID");
                    break;
                case 13:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/单向流/MODEL1D_E100.EGRID");
                    break;
                case 14:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/平面径向流/PINGMIAN_E100.EGRID");
                    break;
                case 15:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/稳定水压恒压边界/MODEL2D_E100.EGRID");
                    break;
                case 16:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/稳定水压恒压边界/MODEL2D_E100.EGRID");
                    break;
                case 17:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/封闭边界/MODEL2D_E100.EGRID");
                    break;
            }
            return szUriPath;
        }

        /// <summary>
        /// HSL TO RGB
        /// </summary>
        /// <param name="h"></param>
        /// <param name="sl"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        public static ColorRGB HSL2RGB(double h, double sl, double l)
        {

            double v;

            double r, g, b;



            r = l;   // default to gray

            g = l;

            b = l;

            v = (l <= 0.5) ? (l * (1.0 + sl)) : (l + sl - l * sl);

            if (v > 0)
            {

                double m;

                double sv;

                int sextant;

                double fract, vsf, mid1, mid2;



                m = l + l - v;

                sv = (v - m) / v;

                h *= 6.0;

                sextant = (int)h;

                fract = h - sextant;

                vsf = v * sv * fract;

                mid1 = m + vsf;

                mid2 = v - vsf;

                switch (sextant)
                {

                    case 0:

                        r = v;

                        g = mid1;

                        b = m;

                        break;

                    case 1:

                        r = mid2;

                        g = v;

                        b = m;

                        break;

                    case 2:

                        r = m;

                        g = v;

                        b = mid1;

                        break;

                    case 3:

                        r = m;

                        g = mid2;

                        b = v;

                        break;

                    case 4:

                        r = mid1;

                        g = m;

                        b = v;

                        break;

                    case 5:

                        r = v;

                        g = m;

                        b = mid2;

                        break;

                }

            }

            ColorRGB rgb;

            rgb.R = Convert.ToByte(r * 255.0f);

            rgb.G = Convert.ToByte(g * 255.0f);

            rgb.B = Convert.ToByte(b * 255.0f);

            return rgb;

        }
        
        /// <summary>
        /// RGB 2 HSL
        /// </summary>
        /// <param name="rgb"></param>
        /// <param name="h"></param>
        /// <param name="s"></param>
        /// <param name="l"></param>
        public static void RGB2HSL(ColorRGB rgb, out double h, out double s, out double l)
        {

            double r = rgb.R / 255.0;

            double g = rgb.G / 255.0;

            double b = rgb.B / 255.0;

            double v;

            double m;

            double vm;

            double r2, g2, b2;



            h = 0; // default to black

            s = 0;

            l = 0;

            v = Math.Max(r, g);

            v = Math.Max(v, b);

            m = Math.Min(r, g);

            m = Math.Min(m, b);

            l = (m + v) / 2.0;

            if (l <= 0.0)
            {

                return;

            }

            vm = v - m;

            s = vm;

            if (s > 0.0)
            {

                s /= (l <= 0.5) ? (v + m) : (2.0 - v - m);

            }

            else
            {

                return;

            }

            r2 = (v - r) / vm;

            g2 = (v - g) / vm;

            b2 = (v - b) / vm;

            if (r == v)
            {

                h = (g == m ? 5.0 + b2 : 1.0 - g2);

            }

            else if (g == v)
            {

                h = (b == m ? 1.0 + r2 : 3.0 - b2);

            }

            else
            {

                h = (r == m ? 3.0 + g2 : 5.0 - r2);

            }

            h /= 6.0;

        }

        public static PageParams GetPageParams(string szGridFilePath)
        {
            PageParams stPageParams = new PageParams();

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

                if (gridMode.nx != eclModel.nx || gridMode.ny != eclModel.ny || gridMode.nz != eclModel.nz)
                {
                    return new PageParams();
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

            //参数
            stPageParams.dynamicProps = new List<string>();
            stPageParams.dynamicProps.AddRange(dynamicProps);
            stPageParams.dynamicProps.AddRange(staticProps);

            //时间步
            stPageParams.timeSteps = new string[timesteps.Length];
            for (int i = 0; i < timesteps.Length; ++i)
            {
                stPageParams.timeSteps[i] = string.Format("{0:yyyy-MM-dd}", timesteps[i]);
            }
 
            //网格数字
            stPageParams.iTotalGrid = gridMode.TotalGrids;

            return stPageParams;
        }


        public static List<string> ReadInfoFromFile(string filePath)
        {
            List<string> list = new List<string>();

            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath, Encoding.GetEncoding("GBK")))
                {
                    while (!sr.EndOfStream)
                    {
                        string sTemp = sr.ReadLine();
                        //string[] strArray = sTemp.Split(new char[] { '\t', ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                        list.Add(sTemp);
                    }
                }

                return list;
            }
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="list"></param>
        public static void WriteInfoToFile(string filePath, List<string> list)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.GetEncoding("GBK")))
            {
                foreach (string strArray in list)
                {
                    sw.WriteLine(strArray);
                }
            }
        }


        /// <summary>
        /// 获取数据中 单引号之前的值
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        public static List<string> GetSingleQuoteMarkValue(string strData)
        {
            int startIndex = 0, singleIndex = 0;
            int endIndex = 0;
            List<string> listData = new List<string>();

            startIndex = strData.IndexOf('\'');
            while (startIndex >= 0)
            {
                singleIndex++;
                endIndex = strData.IndexOf('\'', startIndex);
                if ((singleIndex % 2) == 0)
                {
                    listData.Add(strData.Substring(startIndex + 1, endIndex - startIndex - 1));
                }

                startIndex = endIndex;
            }
            //string str = "";
            //List<string> listData = new List<string>();
            //for (int i = 0; i < strData.Length;i++ )
            //{
            //    if (strData[i] == '\'')
            //    {
            //        singleIndex++;
            //        if ((singleIndex % 2) == 0)
            //        {
            //            str = "";
            //        }
            //    }
            //    else
            //    {
            //        if ((singleIndex % 2) == 0)
            //        {
            //            str += strData[i];
            //        }
            //        else
            //        {
            //            listData.Add(str);
            //        }
            //    }
            //}

            return listData;
        }

        
    }

}
