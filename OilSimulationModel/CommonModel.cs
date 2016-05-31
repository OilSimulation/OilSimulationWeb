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
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/平面径向流/JINGXIANGLIU_E100.EGRID");
                    break;
                case 15:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/球面向心流/QIUMIAN_E100.EGRID");
                    break;
                case 16:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/稳定水压弹性驱动/WENDINGSHUIYA_E100.EGRID");
                    break;
                case 17:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/封闭边界/FENGBI_E100.EGRID");
                    break;
                //虚拟实验
                case 2111:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/非活塞式驱油影响因素/毛管力/0.5/MODEL1D_E100.EGRID");
                    break;
                case 2112:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/非活塞式驱油影响因素/毛管力/1/MODEL1D_E100.EGRID");
                    break;
                case 2113:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/非活塞式驱油影响因素/毛管力/2/MODEL1D_E100.EGRID");
                    break;
                case 2114:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/非活塞式驱油影响因素/毛管力/自定义/MODEL1D_E100.EGRID");
                    break;
                case 2121:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/非活塞式驱油影响因素/密度/0.7/MODEL1D_E100.EGRID");
                    break;
                case 2122:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/非活塞式驱油影响因素/密度/0.8/MODEL1D_E100.EGRID");
                    break;
                case 2123:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/非活塞式驱油影响因素/密度/0.9/MODEL1D_E100.EGRID");
                    break;
                case 2124:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/非活塞式驱油影响因素/密度/自定义/MODEL1D_E100.EGRID");
                    break;
                case 2131:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/非活塞式驱油影响因素/粘度/0.5/MODEL1D_E100.EGRID");
                    break;
                case 2132:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/非活塞式驱油影响因素/粘度/10/MODEL1D_E100.EGRID");
                    break;
                case 2133:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/非活塞式驱油影响因素/粘度/50/MODEL1D_E100.EGRID");
                    break;
                case 2134:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/非活塞式驱油影响因素/粘度/自定义/MODEL1D_E100.EGRID");
                    break; 
                //--水驱油效率实验
                case 2211:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/水驱油效率实验/三种类型毛管力/wumaoguan/MAOGUANLI_E100.EGRID");
                    break;
                case 2212:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/水驱油效率实验/三种类型毛管力/dimaoguan/MAOGUANLI_E100.EGRID");
                    break;
                case 2213:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/水驱油效率实验/三种类型毛管力/gaomaoguan/MAOGUANLI_E100.EGRID");
                    break;
                case 2221:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/水驱油效率实验/不同原油粘度/dinian-0.5/DINIAN_E100.EGRID");
                    break;
                case 2222:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/水驱油效率实验/不同原油粘度/zhongnian-5/ZHONGNIAN_E100.EGRID");
                    break;
                case 2223:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/水驱油效率实验/不同原油粘度/gaonian-50/MOXING_E100.EGRID");
                    break;
                case 2231:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/水驱油效率实验/不同原油密度/dimidu0.7/DIMI_E100.EGRID");
                    break;
                case 2232:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/水驱油效率实验/不同原油密度/zhong0.9/ZHONGMI_E100.EGRID");
                    break;
                case 2233:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/水驱油效率实验/不同原油密度/gao1.15/GAOMI_E100.EGRID");
                    break;
                //--采收率实验
                case 2311:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/采收率实验/不同束缚水/0/SHUFUSHUI_E100.EGRID");
                    break;
                case 2312:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/采收率实验/不同束缚水/0.3/SHUFUSHUI_E100.EGRID");
                    break;
                case 2313:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/采收率实验/不同束缚水/0.5/SHUFUSHUI_E100.EGRID");
                    break;
                case 2321:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/采收率实验/不同残余油/sor0/CANYUYOU_E100.EGRID");
                    break;
                case 2322:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/采收率实验/不同残余油/sor0.3/CANYUYOU_E100.EGRID");
                    break;
                case 2323:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/采收率实验/不同残余油/sor0.5/CANYUYOU_E100.EGRID");
                    break;
                case 2331:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/采收率实验/不同相渗曲线/changgui/XIANGSHEN_E100.EGRID");
                    break;
                case 2332:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/采收率实验/不同相渗曲线/lixing/XIANGSHEN_E100.EGRID");
                    break;
                case 2333:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/采收率实验/不同相渗曲线/xxing/XIANGSHEN_E100.EGRID");
                    break;
                //仿真实训
                case 3111:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/仿真实训/井网井距/井距/200/JINGJU200_E100.EGRID");
                    break;
                case 3112:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/仿真实训/井网井距/井距/300/JINGJU300_E100.EGRID");
                    break;
                case 3113:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/仿真实训/井网井距/井距/500/JINGJU500_E100.EGRID");
                    break;
                case 3114:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/仿真实训/井网井距/井距/自定义/JINGJU500_E100.EGRID");
                    break;
                case 3121:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/仿真实训/井网井距/井网/五点/WUDIAN_E100.EGRID");
                    break;
                case 3122:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/仿真实训/井网井距/井网/七点/QIDIAN1_E100.EGRID");
                    break;
                case 3123:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/仿真实训/井网井距/井网/九点/JIUDIAN_E100.EGRID");
                    break;
                case 3124:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/仿真实训/井网井距/井网/自定义/JIUDIAN_E100.EGRID");
                    break;
                //--注采系统方案设计与开发效果预测
                case 3211:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/仿真实训/注采系统方案设计与开发效果预测/不同注水时机/早期（同采同注）/ZAOZHU_E100.EGRID");
                    break;
                case 3212:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/仿真实训/注采系统方案设计与开发效果预测/不同注水时机/中期开发后半年/ZHONG_E100.EGRID");
                    break;
                case 3213:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/仿真实训/注采系统方案设计与开发效果预测/不同注水时机/晚期开发后两年/WAN_E100.EGRID");
                    break;
                case 3214:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/仿真实训/注采系统方案设计与开发效果预测/不同注水时机/自定义/ZHONG_E100.EGRID");
                    break;
                case 3221:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/仿真实训/注采系统方案设计与开发效果预测/不同注采比/0.8/ZHUCAIBI_E100.EGRID");
                    break;
                case 3222:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/仿真实训/注采系统方案设计与开发效果预测/不同注采比/1/ZHUCAIBI1_E100.EGRID");
                    break;
                case 3223:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/仿真实训/注采系统方案设计与开发效果预测/不同注采比/1.2/ZHUCAIBI1_E100.EGRID");
                    break;
                case 3224:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/仿真实训/注采系统方案设计与开发效果预测/不同注采比/自定义/ZHUCAIBI2006_E100.EGRID");
                    break;
                case 3231:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/仿真实训/注采系统方案设计与开发效果预测/不同最大井底注入压力/35MPA/ZHURUYALI35_E100.EGRID");
                    break;
                case 3232:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/仿真实训/注采系统方案设计与开发效果预测/不同最大井底注入压力/40MPA/ZHURUYALI40_E100.EGRID");
                    break;
                case 3233:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/仿真实训/注采系统方案设计与开发效果预测/不同最大井底注入压力/50MPA/ZHURUYALI50_E100.EGRID");
                    break;
                case 3234:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/仿真实训/注采系统方案设计与开发效果预测/不同最大井底注入压力/自定义/ZHURUYALI50_E100.EGRID");
                    break;
                case 3241:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/仿真实训/注采系统方案设计与开发效果预测/不同最小井底流压/1MPA/PWF1_E100.EGRID");
                    break;
                case 3242:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/仿真实训/注采系统方案设计与开发效果预测/不同最小井底流压/5MPA/PWF5_E100.EGRID");
                    break;
                case 3243:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/仿真实训/注采系统方案设计与开发效果预测/不同最小井底流压/10MPA/PWF10_E100.EGRID");
                    break;
                case 3244:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/仿真实训/注采系统方案设计与开发效果预测/不同最小井底流压/自定义/PWF10_E100.EGRID");
                    break;

                //创新实践
                case 411:
                case 412:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/创新实践/油藏开发/均质/JUNZHI_E100.EGRID");
                    break;
                case 421:
                case 422:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/创新实践/气藏开发/均质/QICANG/1234_E100.EGRID");
                    break;
                //默认基础认知非活塞驱油
                default:
                    szUriPath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/非活塞式驱油/MODEL1D_E100.EGRID");
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
            //采油天数
            stPageParams.lstDays = new List<float>();
            //累产液
            stPageParams.lstFLPT = new List<float>();
            //累采油
            stPageParams.lstFOPT = new List<float>();
            //地层压力
            stPageParams.lstFPR = new List<float>();
            //累注水
            stPageParams.lstFWIT = new List<float>();
            //累产水
            stPageParams.lstFWPT = new List<float>(); 
            //生存RSM文件 （IF） RSM文件不存在
            AutoCreateRsmFile(szGridFilePath);
            //地层原始储油量
            float fMaxOilInclude = GetOilTotal(szGridFilePath);
            //RSM文件
            string rsmFilename = Path.ChangeExtension(szGridFilePath, ".RSM");
            List<string> lst = ReadInfoFromFile(rsmFilename);
            //从第七行开始
            for (int i = 6; i < lst.Count; i++ )
            {
                string[] strArray = lst[i].Split(new char[] {'\t', ' '}, StringSplitOptions.RemoveEmptyEntries);
                //采出程度计算--百分比
                float fCurGetPercent = Convert.ToSingle(strArray[3]) * 100 / fMaxOilInclude;
                stPageParams.lstDays.Add(Convert.ToSingle(strArray[0]));
                stPageParams.lstFLPT.Add(Convert.ToSingle(strArray[2]));
                stPageParams.lstFOPT.Add(fCurGetPercent);
                stPageParams.lstFPR.Add(Convert.ToSingle(strArray[4]));
                stPageParams.lstFWIT.Add(Convert.ToSingle(strArray[5]));
                stPageParams.lstFWPT.Add(Convert.ToSingle(strArray[6]));
            }
            //采油效率-采收率 == 最后的采出程度
            stPageParams.fGetPercent = stPageParams.lstFOPT[stPageParams.lstFOPT.Count-1];
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


        /// <summary>
        /// 获取地层原始储油量
        /// </summary>
        /// <param name="iModeIndex"></param>
        /// <returns></returns>
        public static float GetOilTotal(string szGridFilePath)
        {
            string prtFilename = Path.ChangeExtension(szGridFilePath, ".PRT");  
            if (File.Exists(prtFilename))
            {
                using (StreamReader sr = new StreamReader(prtFilename, Encoding.GetEncoding("GBK")))
                {
                    while (!sr.EndOfStream)
                    {
                        string sTemp = sr.ReadLine();
                        if (sTemp.IndexOf("ORIGINALLY IN PLACE") != -1)
                        { 
                            string[] strArray = sTemp.Split(new char[] { ':', '\t', ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                            //返回地层油藏原始储量
                            return Convert.ToSingle(strArray[3]);
                        }
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// 自动生成RSM文件
        /// </summary>
        /// <param name="iModeIndex"></param>
        public static void AutoCreateRsmFile(string szGridFilePath)
        { 
            //模型名称  
            string szModeName = Path.GetFileNameWithoutExtension(szGridFilePath);
            //油井文件
            string sumFileName = szGridFilePath.Substring(0, szGridFilePath.IndexOf("_E")) + "_SUM.INC";
            //RSM文件
            string rsmFilename = Path.ChangeExtension(szGridFilePath, ".RSM");
            //模块目录
            string targetDir = System.IO.Path.GetDirectoryName(szGridFilePath);
            //批处理文件
            string szBatFile = targetDir + "\\RUN.BAT";
            //批处理命令 
            string szBatCommand = String.Format("CAll $eclipse -file {0}  -ver 2006.1", szModeName);

            if (!File.Exists(rsmFilename))
            {
                //批处理文件生成  
                if (!File.Exists(szBatFile))
                {
                    FileStream fs = new FileStream(szBatFile, FileMode.OpenOrCreate, FileAccess.ReadWrite); //可以指定盘符，也可以指定任意文件名，还可以为word等文件
                    StreamWriter sw = new StreamWriter(fs); // 创建写入流
                    sw.WriteLine(szBatCommand); // 写入Hello World
                    sw.Close(); //关闭文件
                }
                //修改SUM文件
                {
                    List<string> listData = new List<string>();
                    listData.Add("EXCEL");
                    listData.Add("--FLPR 日产液");
                    listData.Add("FLPT");
                    listData.Add("--FOPR  日产油");
                    listData.Add("FOPT");
                    listData.Add("FPR");
                    listData.Add("--FWIR   日注水");
                    listData.Add("FWIT");
                    listData.Add("--FWPR   日产水");
                    listData.Add("FWPT"); 
                    WriteInfoToFile(sumFileName, listData);    
                }
                try
                {
                    System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
                    myProcess.StartInfo.UseShellExecute = false;
                    myProcess.StartInfo.CreateNoWindow = true;
                    myProcess.StartInfo.WorkingDirectory = targetDir;
                    myProcess.StartInfo.FileName = szBatFile;
                    myProcess.Start();
                    myProcess.WaitForExit();
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
        }

    }

}
