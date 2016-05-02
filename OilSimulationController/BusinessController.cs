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
    public class BusinessController : Controller
    {
        /// <summary>
        /// 单步文件后缀
        /// </summary>
        private static string[] StepFileExt = {".X0000"
                                              , ".X0001", ".X0002", ".X0003", ".X0004", ".X0005", ".X0006", ".X0007", ".X0008", ".X0009", ".X0010" 
                                              , ".X0011", ".X0012", ".X0013", ".X0014", ".X0015", ".X0016", ".X0017", ".X0018", ".X0019", ".X0020"
                                              , ".X0021", ".X0022", ".X0023", ".X0024", ".X0025", ".X0026", ".X0027", ".X0028", ".X0029", ".X0030"
                                              , ".X0031", ".X0032", ".X0033", ".X0034", ".X0035", ".X0036", ".X0037", ".X0038", ".X0039", ".X0040"
                                              , ".X0041", ".X0042", ".X0043", ".X0044", ".X0045", ".X0046", ".X0047", ".X0048", ".X0049", ".X0050"
                                              , ".X0051", ".X0052", ".X0053", ".X0054", ".X0055", ".X0056", ".X0057", ".X0058", ".X0059", ".X0060"
                                              , ".X0061", ".X0062", ".X0063", ".X0064", ".X0065", ".X0066", ".X0067", ".X0068", ".X0069", ".X0070"
                                              , ".X0071", ".X0072", ".X0073", ".X0074", ".X0075", ".X0076", ".X0077", ".X0078", ".X0079", ".X0080"
                                              , ".X0081", ".X0082", ".X0083", ".X0084", ".X0085", ".X0086", ".X0087", ".X0088", ".X0089", ".X0090"
                                              , ".X0091", ".X0092", ".X0093", ".X0094", ".X0095", ".X0096", ".X0097", ".X0098", ".X0099", ".X0100"
                                              };
        private static string[] szDynamicPara = { "FIPOIL", "FIPWAT", "PRESSURE", "SWAT", "SOIL" };
        private static List<string> lstDynamicPara = new List<string>(szDynamicPara);

        /// <summary>
        /// 判断本地是否可用
        /// </summary>
        /// <returns></returns>
        public ActionResult IsLocal()
        {
            string strData = "本地可用";
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return Json(strData, JsonRequestBehavior.AllowGet); 
        }

        /// <summary>
        /// 打开控制台执行拼接完成的批处理命令字符串
        /// </summary>
        /// <param name="inputAction">需要执行的命令委托方法：每次调用 <paramref name="inputAction"/> 中的参数都会执行一次</param>
        private static void ExecBatCommand(Action<Action<string>> inputAction)
        {
            Process pro = null;
            StreamWriter sIn = null;
            StreamReader sOut = null;

            try
            {
                pro = new Process();
                pro.StartInfo.FileName = "cmd.exe";
                pro.StartInfo.UseShellExecute = false;
                pro.StartInfo.CreateNoWindow = true;
                pro.StartInfo.RedirectStandardInput = true;
                pro.StartInfo.RedirectStandardOutput = true;
                pro.StartInfo.RedirectStandardError = true;

                pro.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);
                pro.ErrorDataReceived += (sender, e) => Console.WriteLine(e.Data);

                pro.Start();
                sIn = pro.StandardInput;
                sIn.AutoFlush = true;

                pro.BeginOutputReadLine();
                inputAction(value => sIn.WriteLine(value));

                pro.WaitForExit();
            }
            finally
            {
                if (pro != null && !pro.HasExited)
                    pro.Kill();

                if (sIn != null)
                    sIn.Close();
                if (sOut != null)
                    sOut.Close();
                if (pro != null)
                    pro.Close();
            }
        }

        /// <summary>
        /// 获取指定帧，指定层数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="szProName"></param>
        /// <param name="iStep"></param>
        /// <param name="k"></param>
        /// <param name="fMaxValue"></param>
        /// <param name="fMinValue"></param>
        /// <param name="egridFilePath"></param>
        /// <returns></returns>
        private List<stDrawInfo> GetAllPoint(EclipseModel model, string szProName, int iStep, int k, string egridFilePath)
        {
            List<stDrawInfo> lst = new List<stDrawInfo>(); 
            if ( lstDynamicPara.IndexOf(szProName) != -1 )
            {
                //
                string initFilename = Path.ChangeExtension(egridFilePath, StepFileExt[iStep]);

                using (EclipseParser initParser = new EclipseParser(initFilename))
                {
                    // 由于eclipse模型中只记录了有效网格的数值，所以这里的propValues与(i,j,k)并不是直接对应的
                    float[] propValues;// = initParser.ParseEclipsePropertyFromInit(szProName);
                    if (szProName == "SOIL")
                    {
                        propValues = initParser.ParseEclipsePropertyFromInit("SWAT");
                        for (int z = 0; z < propValues.Length; ++z)
                        {
                            propValues[z] = 1 - propValues[z];
                        }
                    }
                    else
                    {
                        propValues = initParser.ParseEclipsePropertyFromInit(szProName);
                    }
                    if (propValues.Length == model.TotalGrids)
                    {
                        for (int j = 0; j < model.ny; j++)
                            for (int i = 0; i < model.nx; i++)
                            {
                                //这里面的所有网格都输出
                                Pillar p = model.GetGridAtIJK(i, j, k);
                                PillarPoint center = model.GetGridAtIJK(i, j, k).Center;
                                int pos = k * model.nx * model.ny + j * model.nx + i;
                                ///!!!! 这里的v才是网格(i,j,k)上的属性值
                                float v = propValues[pos];
                                stDrawInfo dwInfo = new stDrawInfo();

                                dwInfo.a = new float[3];
                                dwInfo.a[0] = p.a.x;
                                dwInfo.a[1] = p.a.y;
                                dwInfo.a[2] = p.a.z;
                                dwInfo.b = new float[3];
                                dwInfo.b[0] = p.b.x;
                                dwInfo.b[1] = p.b.y;
                                dwInfo.b[2] = p.b.z;
                                dwInfo.c = new float[3];
                                dwInfo.c[0] = p.c.x;
                                dwInfo.c[1] = p.c.y;
                                dwInfo.c[2] = p.c.z;
                                dwInfo.d = new float[3];
                                dwInfo.d[0] = p.d.x;
                                dwInfo.d[1] = p.d.y;
                                dwInfo.d[2] = p.d.z;
                                dwInfo.e = new float[3];
                                dwInfo.e[0] = p.e.x;
                                dwInfo.e[1] = p.e.y;
                                dwInfo.e[2] = p.e.z;
                                dwInfo.f = new float[3];
                                dwInfo.f[0] = p.f.x;
                                dwInfo.f[1] = p.f.y;
                                dwInfo.f[2] = p.f.z;
                                dwInfo.g = new float[3];
                                dwInfo.g[0] = p.g.x;
                                dwInfo.g[1] = p.g.y;
                                dwInfo.g[2] = p.g.z;
                                dwInfo.h = new float[3];
                                dwInfo.h[0] = p.h.x;
                                dwInfo.h[1] = p.h.y;
                                dwInfo.h[2] = p.h.z;
                                dwInfo.v = (v);

                                lst.Add(dwInfo);
                            }
                    }
                    else    //大量的属性只在有效结点上才有值，下面的代码执行的机会更多
                    {
                        for (int j = 0; j < model.ny; j++)
                            for (int i = 0; i < model.nx; i++)
                            {
                                Pillar p = model.GetGridAtIJK(i, j, k);
                                PillarPoint center = model.GetGridAtIJK(i, j, k).Center;
                                bool isActive = model.IsActive(i, j, k);
                                if (isActive)
                                {
                                    //  通过I，J，K算出在整个有效网格中的位置
                                    int indexPos = k * model.nx * model.ny + j * model.nx + i;
                                    int pos = model.IndexNode[indexPos];
                                    ///!!!! 这里的v才是网格(i,j,k)上的属性值
                                    float v = propValues[pos];
                                    stDrawInfo dwInfo = new stDrawInfo();

                                    dwInfo.a = new float[3];
                                    dwInfo.a[0] = p.a.x;
                                    dwInfo.a[1] = p.a.y;
                                    dwInfo.a[2] = p.a.z;
                                    dwInfo.b = new float[3];
                                    dwInfo.b[0] = p.b.x;
                                    dwInfo.b[1] = p.b.y;
                                    dwInfo.b[2] = p.b.z;
                                    dwInfo.c = new float[3];
                                    dwInfo.c[0] = p.c.x;
                                    dwInfo.c[1] = p.c.y;
                                    dwInfo.c[2] = p.c.z;
                                    dwInfo.d = new float[3];
                                    dwInfo.d[0] = p.d.x;
                                    dwInfo.d[1] = p.d.y;
                                    dwInfo.d[2] = p.d.z;
                                    dwInfo.e = new float[3];
                                    dwInfo.e[0] = p.e.x;
                                    dwInfo.e[1] = p.e.y;
                                    dwInfo.e[2] = p.e.z;
                                    dwInfo.f = new float[3];
                                    dwInfo.f[0] = p.f.x;
                                    dwInfo.f[1] = p.f.y;
                                    dwInfo.f[2] = p.f.z;
                                    dwInfo.g = new float[3];
                                    dwInfo.g[0] = p.g.x;
                                    dwInfo.g[1] = p.g.y;
                                    dwInfo.g[2] = p.g.z;
                                    dwInfo.h = new float[3];
                                    dwInfo.h[0] = p.h.x;
                                    dwInfo.h[1] = p.h.y;
                                    dwInfo.h[2] = p.h.z;
                                    dwInfo.v = (v);

                                    lst.Add(dwInfo);
                                }
                            }
                    }
                }
            }
            else//静态
            {
                // 下面是读取一种静态属性的例子
                // 从init文件中读出一种静态属性的数据
                String initFilename = Path.ChangeExtension(egridFilePath, ".INIT");
                using (EclipseParser initParser = new EclipseParser(initFilename))
                {
                    // 由于eclipse模型中只记录了有效网格的数值，所以这里的propValues与(i,j,k)并不是直接对应的
                    float[] propValues = initParser.ParseEclipsePropertyFromInit(szProName);
                    //fMinValue = propValues.Min();
                    //fMaxValue = propValues.Max();
                    if (propValues.Length == model.TotalGrids)
                    {
                        for (int j = 0; j < model.ny; j++)
                            for (int i = 0; i < model.nx; i++)
                            {
                                //这里面的所有网格都输出
                                Pillar p = model.GetGridAtIJK(i, j, k);
                                PillarPoint center = model.GetGridAtIJK(i, j, k).Center;
                                int pos = k * model.nx * model.ny + j * model.nx + i;
                                ///!!!! 这里的v才是网格(i,j,k)上的属性值
                                float v = propValues[pos];
                                stDrawInfo dwInfo = new stDrawInfo();

                                dwInfo.a = new float[3];
                                dwInfo.a[0] = p.a.x;
                                dwInfo.a[1] = p.a.y;
                                dwInfo.a[2] = p.a.z;
                                dwInfo.b = new float[3];
                                dwInfo.b[0] = p.b.x;
                                dwInfo.b[1] = p.b.y;
                                dwInfo.b[2] = p.b.z;
                                dwInfo.c = new float[3];
                                dwInfo.c[0] = p.c.x;
                                dwInfo.c[1] = p.c.y;
                                dwInfo.c[2] = p.c.z;
                                dwInfo.d = new float[3];
                                dwInfo.d[0] = p.d.x;
                                dwInfo.d[1] = p.d.y;
                                dwInfo.d[2] = p.d.z;
                                dwInfo.e = new float[3];
                                dwInfo.e[0] = p.e.x;
                                dwInfo.e[1] = p.e.y;
                                dwInfo.e[2] = p.e.z;
                                dwInfo.f = new float[3];
                                dwInfo.f[0] = p.f.x;
                                dwInfo.f[1] = p.f.y;
                                dwInfo.f[2] = p.f.z;
                                dwInfo.g = new float[3];
                                dwInfo.g[0] = p.g.x;
                                dwInfo.g[1] = p.g.y;
                                dwInfo.g[2] = p.g.z;
                                dwInfo.h = new float[3];
                                dwInfo.h[0] = p.h.x;
                                dwInfo.h[1] = p.h.y;
                                dwInfo.h[2] = p.h.z;
                                dwInfo.v = (v);

                                lst.Add(dwInfo);
                            }
                    }
                    else    //大量的属性只在有效结点上才有值，下面的代码执行的机会更多
                    {
                        for (int j = 0; j < model.ny; j++)
                            for (int i = 0; i < model.nx; i++)
                            {
                                Pillar p = model.GetGridAtIJK(i, j, k);
                                PillarPoint center = model.GetGridAtIJK(i, j, k).Center;
                                bool isActive = model.IsActive(i, j, k);
                                if (isActive)
                                {
                                    //  通过I，J，K算出在整个有效网格中的位置
                                    int indexPos = k * model.nx * model.ny + j * model.nx + i;
                                    int pos = model.IndexNode[indexPos];
                                    ///!!!! 这里的v才是网格(i,j,k)上的属性值
                                    float v = propValues[pos];
                                    stDrawInfo dwInfo = new stDrawInfo();

                                    dwInfo.a = new float[3];
                                    dwInfo.a[0] = p.a.x;
                                    dwInfo.a[1] = p.a.y;
                                    dwInfo.a[2] = p.a.z;
                                    dwInfo.b = new float[3];
                                    dwInfo.b[0] = p.b.x;
                                    dwInfo.b[1] = p.b.y;
                                    dwInfo.b[2] = p.b.z;
                                    dwInfo.c = new float[3];
                                    dwInfo.c[0] = p.c.x;
                                    dwInfo.c[1] = p.c.y;
                                    dwInfo.c[2] = p.c.z;
                                    dwInfo.d = new float[3];
                                    dwInfo.d[0] = p.d.x;
                                    dwInfo.d[1] = p.d.y;
                                    dwInfo.d[2] = p.d.z;
                                    dwInfo.e = new float[3];
                                    dwInfo.e[0] = p.e.x;
                                    dwInfo.e[1] = p.e.y;
                                    dwInfo.e[2] = p.e.z;
                                    dwInfo.f = new float[3];
                                    dwInfo.f[0] = p.f.x;
                                    dwInfo.f[1] = p.f.y;
                                    dwInfo.f[2] = p.f.z;
                                    dwInfo.g = new float[3];
                                    dwInfo.g[0] = p.g.x;
                                    dwInfo.g[1] = p.g.y;
                                    dwInfo.g[2] = p.g.z;
                                    dwInfo.h = new float[3];
                                    dwInfo.h[0] = p.h.x;
                                    dwInfo.h[1] = p.h.y;
                                    dwInfo.h[2] = p.h.z;
                                    dwInfo.v = (v);

                                    lst.Add(dwInfo);
                                }
                            }
                    }
                }
            }

            return lst;

        }

        /// <summary>
        /// 获取X000扩展文件
        /// </summary>
        /// <param name="iStep"></param>
        /// <returns></returns>
        private string GetStepFileExt(int iStep)
        {
            string szRetValue = iStep.ToString();
            ///左边补0
            szRetValue = szRetValue.PadLeft(4, '0');   
            ///.X
            szRetValue = ".X" + szRetValue;
            //返回字符串
            return szRetValue;
        }

        /// <summary>
        /// 计算模型数据最大最小值
        /// </summary>
        /// <param name="szProName"></param>
        /// <param name="iStepAll"></param>
        /// <param name="egridFilePath"></param>
        /// <returns></returns>
        private float[] CaculateMaxMinValue(string szProName, int iStepAll, string egridFilePath)
        {
            //List<float[]> lst = new List<float[]>();
            float fMinValue = 1000.0f;
            float fMaxValue = 0.0f;
            for (int i = 0; i < iStepAll; ++i)
            {
                string initFilename = Path.ChangeExtension(egridFilePath, GetStepFileExt(i));

                using (EclipseParser initParser = new EclipseParser(initFilename))
                {
                    float[] propValues;
                    if (szProName == "SOIL")
                    {
                        propValues = initParser.ParseEclipsePropertyFromInit("SWAT");
                        for (int z = 0; z < propValues.Length; ++z)
                        {
                            propValues[z] = 1 - propValues[z];
                        }
                    }
                    else
                    {
                        propValues = initParser.ParseEclipsePropertyFromInit(szProName);
                    }
                    if (propValues.Min() < fMinValue)
                    {
                        fMinValue = propValues.Min();
                    }
                    if (propValues.Max() > fMaxValue)
                    {
                        fMaxValue = propValues.Max();
                    }
                }
            }
            return new float[2] { fMinValue, fMaxValue };
        }

        /// <summary>
        /// 获取模型中心点坐标
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private float[] GetCenterCoordinates(EclipseModel model)
        {
            float coordinateX = 0.0f;
            float coordinateY = 0.0f;
            float coordinateZ = 0.0f;

            int iLevel = (int)(model.nz / 2.0 + 0.5);
            Pillar p = model.GetGridAtIJK(model.nx / 2, model.ny / 2, iLevel-1);
            coordinateX = p.Center.x;
            coordinateY = p.Center.y;
            coordinateZ = p.Center.z;

            return new float[3] { coordinateX, coordinateY, coordinateZ }; 
        }

        /// <summary>
        /// 获取指定帧，指定层数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="szProName"></param>
        /// <param name="iStep"></param>
        /// <param name="k"></param>
        /// <param name="fMaxValue"></param>
        /// <param name="fMinValue"></param>
        /// <param name="egridFilePath"></param>
        /// <returns></returns>
        private List<float[]> GetCenterPointData(EclipseModel model, string szProName, int iStep, int k, string egridFilePath)
        { 
            List<float[]> lst = new List<float[]>();
            if ( lstDynamicPara.IndexOf(szProName) != -1 )
            {
                //
                string initFilename = Path.ChangeExtension(egridFilePath, GetStepFileExt(iStep));

                using (EclipseParser initParser = new EclipseParser(initFilename))
                {
                    // 由于eclipse模型中只记录了有效网格的数值，所以这里的propValues与(i,j,k)并不是直接对应的
                    float[] propValues;// = initParser.ParseEclipsePropertyFromInit(szProName);
                    if (szProName == "SOIL")
                    {
                        propValues = initParser.ParseEclipsePropertyFromInit("SWAT");
                        for (int z = 0; z < propValues.Length; ++z)
                        {
                            propValues[z] = 1 - propValues[z];
                        }
                    }
                    else
                    {
                        propValues = initParser.ParseEclipsePropertyFromInit(szProName);
                    }
                    if (propValues.Length == model.TotalGrids)
                    {
                        for (int j = 0; j < model.ny; j++)
                            for (int i = 0; i < model.nx; i++)
                            {
                                //这里面的所有网格都输出
                                //Pillar p = model.GetGridAtIJK(i, j, k);
                                PillarPoint center = model.GetGridAtIJK(i, j, k).Center;
                                int pos = k * model.nx * model.ny + j * model.nx + i;
                                ///!!!! 这里的v才是网格(i,j,k)上的属性值
                                float v = propValues[pos];
                                stCubeInfo dwInfo = new stCubeInfo();

                                dwInfo.ct = new float[4];
                                dwInfo.ct[0] = (center.x);
                                dwInfo.ct[1] = (center.y);
                                dwInfo.ct[2] = (center.z);
                                dwInfo.ct[3] = (v); 
                                lst.Add(new float[4] { center.x, center.y, center.z, v });
                            }
                    }
                    else    //大量的属性只在有效结点上才有值，下面的代码执行的机会更多
                    {
                        for (int j = 0; j < model.ny; j++)
                            for (int i = 0; i < model.nx; i++)
                            {
                                //Pillar p = model.GetGridAtIJK(i, j, k);
                                PillarPoint center = model.GetGridAtIJK(i, j, k).Center;
                                bool isActive = model.IsActive(i, j, k);
                                if (isActive)
                                {
                                    //  通过I，J，K算出在整个有效网格中的位置
                                    int indexPos = k * model.nx * model.ny + j * model.nx + i;
                                    int pos = model.IndexNode[indexPos];
                                    ///!!!! 这里的v才是网格(i,j,k)上的属性值
                                    float v = propValues[pos];
                                    stCubeInfo dwInfo = new stCubeInfo();

                                    dwInfo.ct = new float[4];
                                    dwInfo.ct[0] = (center.x);
                                    dwInfo.ct[1] = (center.y);
                                    dwInfo.ct[2] = (center.z);
                                    dwInfo.ct[3] = (v);
                                    //listColors.Add(v);
                                    lst.Add(new float[4] { center.x, center.y, center.z, v });
                                }
                            }
                    }
                }
            }
            else//静态
            {
                // 下面是读取一种静态属性的例子
                // 从init文件中读出一种静态属性的数据
                String initFilename = Path.ChangeExtension(egridFilePath, ".INIT");
                using (EclipseParser initParser = new EclipseParser(initFilename))
                {
                    // 由于eclipse模型中只记录了有效网格的数值，所以这里的propValues与(i,j,k)并不是直接对应的
                    float[] propValues = initParser.ParseEclipsePropertyFromInit(szProName);
                    //fMinValue = propValues.Min();
                    //fMaxValue = propValues.Max();
                    if (propValues.Length == model.TotalGrids)
                    {
                        for (int j = 0; j < model.ny; j++)
                            for (int i = 0; i < model.nx; i++)
                            {
                                //这里面的所有网格都输出
                                //Pillar p = model.GetGridAtIJK(i, j, k);
                                PillarPoint center = model.GetGridAtIJK(i, j, k).Center;
                                int pos = k * model.nx * model.ny + j * model.nx + i;
                                ///!!!! 这里的v才是网格(i,j,k)上的属性值
                                float v = propValues[pos];
                                stCubeInfo dwInfo = new stCubeInfo();

                                dwInfo.ct = new float[4];
                                dwInfo.ct[0] = (center.x);
                                dwInfo.ct[1] = (center.y);
                                dwInfo.ct[2] = (center.z);
                                dwInfo.ct[3] = (v);
                                //listColors.Add(v);
                                lst.Add(new float[4] { center.x, center.y, center.z, v });
                            }
                    }
                    else    //大量的属性只在有效结点上才有值，下面的代码执行的机会更多
                    {
                        for (int j = 0; j < model.ny; j++)
                            for (int i = 0; i < model.nx; i++)
                            {
                                //Pillar p = model.GetGridAtIJK(i, j, k);
                                PillarPoint center = model.GetGridAtIJK(i, j, k).Center;
                                bool isActive = model.IsActive(i, j, k);
                                if (isActive)
                                {
                                    //  通过I，J，K算出在整个有效网格中的位置
                                    int indexPos = k * model.nx * model.ny + j * model.nx + i;
                                    int pos = model.IndexNode[indexPos];
                                    ///!!!! 这里的v才是网格(i,j,k)上的属性值
                                    float v = propValues[pos];
                                    stCubeInfo dwInfo = new stCubeInfo();

                                    dwInfo.ct = new float[4];
                                    dwInfo.ct[0] = (center.x);
                                    dwInfo.ct[1] = (center.y);
                                    dwInfo.ct[2] = (center.z);
                                    dwInfo.ct[3] = (v);
                                    //listColors.Add(v);
                                    lst.Add(new float[4] { center.x, center.y, center.z, v });
                                }
                            }
                    }
                }
            }

            return lst;

        }

       /// <summary>
       /// 获取颜色属性
       /// </summary>
       /// <param name="model"></param>
       /// <param name="szProName"></param>
       /// <param name="iStep"></param>
       /// <param name="k"></param>
       /// <param name="egridFilePath"></param>
       /// <returns></returns>
        private List<float[]> GetCenterPointColor(EclipseModel model, string szProName, int iStep, int k, string egridFilePath)
        {
            List<float[]> lst = new List<float[]>();
            if (lstDynamicPara.IndexOf(szProName) != -1)
            {
                //
                string initFilename = Path.ChangeExtension(egridFilePath, StepFileExt[iStep]);

                using (EclipseParser initParser = new EclipseParser(initFilename))
                {
                    // 由于eclipse模型中只记录了有效网格的数值，所以这里的propValues与(i,j,k)并不是直接对应的
                    float[] propValues;// = initParser.ParseEclipsePropertyFromInit(szProName);
                    if (szProName == "SOIL")
                    {
                        propValues = initParser.ParseEclipsePropertyFromInit("SWAT");
                        for (int z = 0; z < propValues.Length; ++z)
                        {
                            propValues[z] = 1 - propValues[z];
                        }
                    }
                    else
                    {
                        propValues = initParser.ParseEclipsePropertyFromInit(szProName);
                    }
                    if (propValues.Length == model.TotalGrids)
                    {
                        for (int j = 0; j < model.ny; j++)
                            for (int i = 0; i < model.nx; i++)
                            {
                                //这里面的所有网格都输出 
                                int pos = k * model.nx * model.ny + j * model.nx + i;
                                ///!!!! 这里的v才是网格(i,j,k)上的属性值
                                float v = propValues[pos];
                                lst.Add(new float[] { v });
                            }
                    }
                    else    //大量的属性只在有效结点上才有值，下面的代码执行的机会更多
                    {
                        for (int j = 0; j < model.ny; j++)
                            for (int i = 0; i < model.nx; i++)
                            { 
                                bool isActive = model.IsActive(i, j, k);
                                if (isActive)
                                {
                                    //  通过I，J，K算出在整个有效网格中的位置
                                    int indexPos = k * model.nx * model.ny + j * model.nx + i;
                                    int pos = model.IndexNode[indexPos];
                                    ///!!!! 这里的v才是网格(i,j,k)上的属性值
                                    float v = propValues[pos]; 
                                    //listColors.Add(v);
                                    lst.Add(new float[] { v });
                                }
                            }
                    }
                }
            }
            else//静态
            {
                // 下面是读取一种静态属性的例子
                // 从init文件中读出一种静态属性的数据
                String initFilename = Path.ChangeExtension(egridFilePath, ".INIT");
                using (EclipseParser initParser = new EclipseParser(initFilename))
                {
                    // 由于eclipse模型中只记录了有效网格的数值，所以这里的propValues与(i,j,k)并不是直接对应的
                    float[] propValues = initParser.ParseEclipsePropertyFromInit(szProName);
                    //fMinValue = propValues.Min();
                    //fMaxValue = propValues.Max();
                    if (propValues.Length == model.TotalGrids)
                    {
                        for (int j = 0; j < model.ny; j++)
                            for (int i = 0; i < model.nx; i++)
                            {
                                //这里面的所有网格都输出 
                                int pos = k * model.nx * model.ny + j * model.nx + i;
                                ///!!!! 这里的v才是网格(i,j,k)上的属性值
                                float v = propValues[pos];
                                lst.Add(new float[] { v });
                            }
                    }
                    else    //大量的属性只在有效结点上才有值，下面的代码执行的机会更多
                    {
                        for (int j = 0; j < model.ny; j++)
                            for (int i = 0; i < model.nx; i++)
                            { 
                                bool isActive = model.IsActive(i, j, k);
                                if (isActive)
                                {
                                    //  通过I，J，K算出在整个有效网格中的位置
                                    int indexPos = k * model.nx * model.ny + j * model.nx + i;
                                    int pos = model.IndexNode[indexPos];
                                    ///!!!! 这里的v才是网格(i,j,k)上的属性值
                                    float v = propValues[pos];
                                    lst.Add(new float[] { v });
                                }
                            }
                    }
                }
            }

            return lst;

        }
          
        private List<View3DPoint> Get3DAllPoint(EclipseModel model, string szProName, int iStep, int k, string egridFilePath)
        {
            List<View3DPoint> lst = new List<View3DPoint>();
            if (szProName == "FIPOIL" || szProName == "FIPWAT" || szProName == "PRESSURE" || szProName == "SWAT" || szProName == "SOIL")
            {
                //
                string initFilename = Path.ChangeExtension(egridFilePath, StepFileExt[iStep]);

                using (EclipseParser initParser = new EclipseParser(initFilename))
                {
                    // 由于eclipse模型中只记录了有效网格的数值，所以这里的propValues与(i,j,k)并不是直接对应的
                    float[] propValues;// = initParser.ParseEclipsePropertyFromInit(szProName);
                    if (szProName == "SOIL")
                    {
                        propValues = initParser.ParseEclipsePropertyFromInit("SWAT");
                        for (int z = 0; z < propValues.Length; ++z)
                        {
                            propValues[z] = 1 - propValues[z];
                        }
                    }
                    else
                    {
                        propValues = initParser.ParseEclipsePropertyFromInit(szProName);
                    }
                    if (propValues.Length == model.TotalGrids)
                    {
                        for (int j = 0; j < model.ny; j++)
                            for (int i = 0; i < model.nx; i++)
                            {
                                //这里面的所有网格都输出
                                Pillar p = model.GetGridAtIJK(i, j, k);
                                PillarPoint center = p.Center;
                                int pos = k * model.nx * model.ny + j * model.nx + i;
                                ///!!!! 这里的v才是网格(i,j,k)上的属性值
                                float v = propValues[pos];
                                View3DPoint point = new View3DPoint();
                                point.X = center.x;
                                point.Y = center.y;
                                point.Z = center.z;
                                point.Color = v;
                                point.XWidth = Math.Abs(p.a.x - point.X) * 2;
                                point.YWidth = Math.Abs(p.a.y - point.Y) * 2;
                                point.ZWidth = Math.Abs(p.a.z - point.Z) * 2;
                                lst.Add(point);

                                //stCubeInfo dwInfo = new stCubeInfo();

                                //dwInfo.ct = new float[4];
                                //dwInfo.ct[0] = (center.x);
                                //dwInfo.ct[1] = (center.y);
                                //dwInfo.ct[2] = (center.z);
                                //dwInfo.ct[3] = (v);

                                //lst.Add(new float[4] { center.x, center.y, center.z, v });
                            }
                    }
                    else    //大量的属性只在有效结点上才有值，下面的代码执行的机会更多
                    {
                        for (int j = 0; j < model.ny; j++)
                            for (int i = 0; i < model.nx; i++)
                            {
                                Pillar p = model.GetGridAtIJK(i, j, k);
                                PillarPoint center = p.Center;
                                bool isActive = model.IsActive(i, j, k);
                                if (isActive)
                                {
                                    //  通过I，J，K算出在整个有效网格中的位置
                                    int indexPos = k * model.nx * model.ny + j * model.nx + i;
                                    int pos = model.IndexNode[indexPos];
                                    ///!!!! 这里的v才是网格(i,j,k)上的属性值
                                    float v = propValues[pos];
                                    View3DPoint point = new View3DPoint();
                                    point.X = center.x;
                                    point.Y = center.y;
                                    point.Z = center.z;
                                    point.Color = v;
                                    point.XWidth = Math.Abs(p.a.x - point.X) * 2;
                                    point.YWidth = Math.Abs(p.a.y - point.Y) * 2;
                                    point.ZWidth = Math.Abs(p.a.z - point.Z) * 2;

                                    lst.Add(point);
                                    //stCubeInfo dwInfo = new stCubeInfo();

                                    //dwInfo.ct = new float[4];
                                    //dwInfo.ct[0] = (center.x);
                                    //dwInfo.ct[1] = (center.y);
                                    //dwInfo.ct[2] = (center.z);
                                    //dwInfo.ct[3] = (v);

                                    //lst.Add(new float[4] { center.x, center.y, center.z, v });
                                }
                            }
                    }
                }
            }
            else//静态
            {
                // 下面是读取一种静态属性的例子
                // 从init文件中读出一种静态属性的数据
                String initFilename = Path.ChangeExtension(egridFilePath, ".INIT");
                using (EclipseParser initParser = new EclipseParser(initFilename))
                {
                    // 由于eclipse模型中只记录了有效网格的数值，所以这里的propValues与(i,j,k)并不是直接对应的
                    float[] propValues = initParser.ParseEclipsePropertyFromInit(szProName);
                    //fMinValue = propValues.Min();
                    //fMaxValue = propValues.Max();
                    if (propValues.Length == model.TotalGrids)
                    {
                        for (int j = 0; j < model.ny; j++)
                            for (int i = 0; i < model.nx; i++)
                            {
                                //这里面的所有网格都输出
                                Pillar p = model.GetGridAtIJK(i, j, k);
                                PillarPoint center = p.Center;
                                int pos = k * model.nx * model.ny + j * model.nx + i;
                                ///!!!! 这里的v才是网格(i,j,k)上的属性值
                                float v = propValues[pos];
                                View3DPoint point = new View3DPoint();
                                point.X = center.x;
                                point.Y = center.y;
                                point.Z = center.z;
                                point.Color = v;
                                point.XWidth = Math.Abs(p.a.x - point.X) * 2;
                                point.YWidth = Math.Abs(p.a.y - point.Y) * 2;
                                point.ZWidth = Math.Abs(p.a.z - point.Z) * 2;


                                lst.Add(point);

                                //stCubeInfo dwInfo = new stCubeInfo();

                                //dwInfo.ct = new float[4];
                                //dwInfo.ct[0] = (center.x);
                                //dwInfo.ct[1] = (center.y);
                                //dwInfo.ct[2] = (center.z);
                                //dwInfo.ct[3] = (v);

                                //lst.Add(new float[4] { center.x, center.y, center.z, v });
                            }
                    }
                    else    //大量的属性只在有效结点上才有值，下面的代码执行的机会更多
                    {
                        for (int j = 0; j < model.ny; j++)
                            for (int i = 0; i < model.nx; i++)
                            {
                                Pillar p = model.GetGridAtIJK(i, j, k);
                                PillarPoint center = p.Center;
                                bool isActive = model.IsActive(i, j, k);
                                if (isActive)
                                {
                                    //  通过I，J，K算出在整个有效网格中的位置
                                    int indexPos = k * model.nx * model.ny + j * model.nx + i;
                                    int pos = model.IndexNode[indexPos];
                                    ///!!!! 这里的v才是网格(i,j,k)上的属性值
                                    float v = propValues[pos];
                                    View3DPoint point = new View3DPoint();
                                    point.X = center.x;
                                    point.Y = center.y;
                                    point.Z = center.z;
                                    point.Color = v;
                                    point.XWidth = Math.Abs(p.a.x - point.X) * 2;
                                    point.YWidth = Math.Abs(p.a.y - point.Y) * 2;
                                    point.ZWidth = Math.Abs(p.a.z - point.Z) * 2;


                                    lst.Add(point);

                                    //stCubeInfo dwInfo = new stCubeInfo();

                                    //dwInfo.ct = new float[4];
                                    //dwInfo.ct[0] = (center.x);
                                    //dwInfo.ct[1] = (center.y);
                                    //dwInfo.ct[2] = (center.z);
                                    //dwInfo.ct[3] = (v);

                                    //lst.Add(new float[4] { center.x, center.y, center.z, v });
                                }
                            }
                    }
                }
            }

            return lst;

        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetData(PostData inputData)
        {
            int iModel = 0;
            string szPara = "";
            int iStep = 0;
            int iLoadFirst = -1;
            if (ModelState.IsValid)
            {
                iModel = inputData.Mode;
                szPara = inputData.Para;
                iStep = inputData.Step;
                iLoadFirst = inputData.iLoadFirst;
            } 
            //Grid文件
            string eGridFile = CommonModel.GetModeUriPath(iModel); ; 
            //油井文件
            string strWellFilePath = eGridFile.Substring(0, eGridFile.IndexOf("_E")) + "_sch.INC";
             

            EclipseModel gridModel = EclipseParser.ParseEgrid(eGridFile);

            int countXFiles = EclipseParser.CountXFiles(eGridFile); 
              
            ModeData stModeData = new ModeData();
            //获取最大最小值 
            stModeData.mm = CaculateMaxMinValue(szPara, countXFiles, eGridFile);
            if (iLoadFirst == 0)
            {
                //获取中心点坐标
                stModeData.ct = GetCenterCoordinates(gridModel);
                //获取数据
                stModeData.Data = new List<float[]>();
                stModeData.xyz = new List<float[]>();
                for (int i = 0; i < gridModel.nz; i++)
                {
                    stModeData.Data.AddRange(GetCenterPointData(gridModel, szPara, iStep, i, eGridFile)); 
                    //计算XYZ的距离
                    Pillar p = gridModel.GetGridAtIJK(0, 0, i);
                    stModeData.xyz.Add(new float[] { (p.Center.x - p.a.x) * 2, (p.Center.y - p.a.y) * 2, (p.Center.z - p.a.z) * 2 }); 
                }
            }
            else
            {
                stModeData.Data = new List<float[]>();
                for (int i = 0; i < gridModel.nz; i++)
                {
                    stModeData.Data.AddRange(GetCenterPointColor(gridModel, szPara, iStep, i, eGridFile));  
                }

            }


            stModeData.WellPoint = GetWellPoint(gridModel,strWellFilePath);

            
            stModeData.lev = gridModel.nz;
            var res = new ConfigurableJsonResult();
            res.Data = stModeData; 
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;
        }




        /// <summary>
        /// 获取风格数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetNetPointData(PostData inputData)
        {
            int iModel = 0;
            string szPara = "";
            int iStep = 0;
            int iLoadFirst = -1;
            if (ModelState.IsValid)
            {
                iModel = inputData.Mode;
                szPara = inputData.Para;
                iStep = inputData.Step;
                iLoadFirst = inputData.iLoadFirst;
            }
            //string eGridFile = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/水驱油效率实验/不同原油密度/gao1.15/GAOMI_E100.EGRID");
            //string eGridFile = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/创新实践/气藏开发/均质/QICANG/123-1_E100.EGRID");

            //string eGridFile = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/仿真实训/井网井距/井距/200/JINGJU200_E100.EGRID");

            //string eGridFile = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/非活塞式驱油/MODEL1D_E100.EGRID");
            //string strWellFilePath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/非活塞式驱油/Model1D_sch.INC");
            //string eGridFile = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/采收率实验/不同残余油/sor0/CANYUYOU_E100.EGRID");
            // string strWellFilePath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/采收率实验/不同残余油/sor0/CANYUYOU_SCH.INC");

            string eGridFile = "";
            string strWellFilePath = "";//油井文件
            switch (iModel)
            {
                case 11:
                    //eGridFile = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/活塞式驱油/MODEL1D_E100.EGRID");
                    //strWellFilePath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/活塞式驱油/MODEL1D_sch.INC"); 
                    //eGridFile = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/水驱油效率实验/不同原油密度/gao1.15/GAOMI_E100.EGRID");
                    //strWellFilePath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/水驱油效率实验/不同原油密度/gao1.15/GAOMI_sch.INC"); 
                    eGridFile = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/仿真实训/井网井距/井距/300/JINGJU300_E100.EGRID");
                    strWellFilePath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/仿真实训/井网井距/井距/300/JINGJU300_SCH.INC");
                    break;
                case 12:
                    eGridFile = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/非活塞式驱油/MODEL1D_E100.EGRID");
                    strWellFilePath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/非活塞式驱油/MODEL1D_sch.INC");
                    break;
                case 13:
                    eGridFile = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/单向流/MODEL1D_E100.EGRID");
                    strWellFilePath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/活塞式驱油/MODEL1D_sch.INC");
                    break;
                case 14:
                    eGridFile = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/活塞式驱油/MODEL1D_E100.EGRID");
                    strWellFilePath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/活塞式驱油/MODEL1D_sch.INC");
                    break;
                case 15:
                    eGridFile = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/活塞式驱油/MODEL1D_E100.EGRID");
                    strWellFilePath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/活塞式驱油/MODEL1D_sch.INC");
                    break;
                case 16:
                    eGridFile = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/活塞式驱油/MODEL1D_E100.EGRID");
                    strWellFilePath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/活塞式驱油/MODEL1D_sch.INC");
                    break;
                case 17:
                    eGridFile = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/活塞式驱油/MODEL1D_E100.EGRID");
                    strWellFilePath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/活塞式驱油/MODEL1D_sch.INC");
                    break;
            }

            EclipseModel gridModel = EclipseParser.ParseEgrid(eGridFile);

            int countXFiles = EclipseParser.CountXFiles(eGridFile);

            ModeData stModeData = new ModeData();
            //获取最大最小值 
            stModeData.mm = CaculateMaxMinValue(szPara, countXFiles, eGridFile);
            if (iLoadFirst == 0)
            {
                //获取中心点坐标
                stModeData.ct = GetCenterCoordinates(gridModel);
                //获取数据
                stModeData.Data = new List<float[]>();
                stModeData.xyz = new List<float[]>();
                for (int i = 0; i < gridModel.nz; i++)
                {
                    stModeData.Data.AddRange(GetCenterPointData(gridModel, szPara, iStep, i, eGridFile));
                    //计算XYZ的距离
                    Pillar p = gridModel.GetGridAtIJK(0, 0, i);
                    stModeData.xyz.Add(new float[] { (p.Center.x - p.a.x) * 2, (p.Center.y - p.a.y) * 2, (p.Center.z - p.a.z) * 2 });
                }
            }
            else
            {
                stModeData.Data = new List<float[]>();
                for (int i = 0; i < gridModel.nz; i++)
                {
                    stModeData.Data.AddRange(GetCenterPointColor(gridModel, szPara, iStep, i, eGridFile));
                }

            }


            stModeData.WellPoint = GetWellPoint(gridModel, strWellFilePath);


            stModeData.lev = gridModel.nz;
            var res = new ConfigurableJsonResult();
            res.Data = stModeData;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;
        }


        /// <summary>
        /// 获取油井坐标
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetWellPoint(PostData inputData)
        {
            int iModel = 0;
            string szPara = "";
            int iStep = 0;
            int iLoadFirst = -1;
            if (ModelState.IsValid)
            {
                iModel = inputData.Mode;
                //szPara = inputData.Para;
                //iStep = inputData.Step;
                //iLoadFirst = inputData.iLoadFirst;
            }

            List<WellData> listWellData = new List<WellData>();
            //string strWellFilePath = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/非活塞式驱油/Model1D_sch.INC");
            //List<string> listData = CommonModel.ReadInfoFromFile(strWellFilePath);//获取井坐标文件数据
            //int wallIndex = listData.IndexOf("WELSPECS");//找到出现 WELSPECS 油井标识的行
            //while (wallIndex >= 0)
            //{

            //    listWellData.Add(ParseStrToWell(listData[wallIndex + 1]));
            //    wallIndex = listData.IndexOf("WELSPECS", wallIndex+1);

            //}

            var res = new ConfigurableJsonResult();
            res.Data = listWellData;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return res;


        }

        private List<WellData> GetWellPoint(EclipseModel eModel,string filePath)
        {
            List<WellData> listWellData = new List<WellData>();
            List<string> listData = CommonModel.ReadInfoFromFile(filePath);//获取井坐标文件数据
            int wallIndex = listData.IndexOf("WELSPECS");//找到出现 WELSPECS 油井标识的行
            while (wallIndex >= 0)
            {

                listWellData.Add(ParseStrToWell(eModel,listData[wallIndex + 1]));
                wallIndex = listData.IndexOf("WELSPECS", wallIndex + 1);

            }

            return listWellData;

        }

        /// <summary>
        /// 解析字符串('PRO2' 'P' 35 65 1* 'OIL' 1* 'STD' 'SHUT' 'YES' 1* 'SEG' 3* 'STD' /)
        /// 井坐标文件中 记录是的网格位置，通过EclipseModel换算也 井坐标
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        private WellData ParseStrToWell(EclipseModel eModel,string strData)
        {
            WellData data = new WellData();
            int x, y, z;
            string[] listData = strData.Split(' ');
            List<string> listd = listData.Where(s => s.Trim() != string.Empty).ToList();
            data.name = listd[0].Replace('\'', ' ').Trim();
            int.TryParse(listd[2], out x);
            int.TryParse(listd[3], out y);
            int.TryParse(listd[4].Substring(0, 1), out z);

            PillarPoint pPoint = eModel.GetGridAtIJK(x-1, y-1, 0).Center;

            data.x = pPoint.x;
            data.y = pPoint.y;
            data.z = pPoint.z;
            data.type = listd[5].Replace('\'', ' ').Trim();


            return data;
        }

        [HttpPost]
        public ActionResult GetModelData(int step)
        {
            //int step;
            //int.TryParse(HttpContext.Request.QueryString["Step"], out step);
            //string eGridFile = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/基础认知/活塞式驱油/MODEL1D_E100.EGRID");
            string eGridFile = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/创新实践/气藏开发/均质/QICANG/123_E100.EGRID");
            EclipseModel gridModel = EclipseParser.ParseEgrid(eGridFile);

            List<View3DPoint> lstData = new List<View3DPoint>();
            for (int i = 0; i < gridModel.nz; i++)
            {

                lstData.AddRange(GetOutlinePoint(Get3DAllPoint(gridModel, "SWAT", step, i, eGridFile), false));
            }

            return Json(new
            {
                list = lstData
            });
        }

        /// <summary>
        /// 计算出轮廓点
        /// </summary>
        /// <param name="listData"></param>
        /// <returns></returns>
        private List<View3DPoint> GetOutlinePoint(List<View3DPoint> listData)
        {
            List<View3DPoint> listResult = new List<View3DPoint>();
            var varX = listData.GroupBy(s => s.X);//相同X坐标的分一组
            var varY = listData.GroupBy(s => s.Y);//相同Y坐标的分一组

            float maxColor = listData.Max(s => s.Color);
            float minColor = listData.Min(s => s.Color);
            //var varZ = listData.GroupBy(s => s.Z);//相同Z坐标的分一组
            foreach (var varM in varX)//取X坐标最大值和最小值对应的坐标
            {
                var varMin = varM.OrderByDescending(s => s.Y).First();
                varMin.MinColor = minColor;
                varMin.MaxColor = maxColor;
                var varMax = varM.OrderBy(s => s.Y).First();
                varMax.MinColor = minColor;
                varMax.MaxColor = maxColor;
                listResult.Add(varMin);
                listResult.Add(varMax);
            }

            foreach (var varM in varY)//取Y坐标最大值和最小值对应的坐标
            {
                var varMin = varM.OrderByDescending(s => s.X).First();
                var varMax = varM.OrderBy(s => s.X).First();
                varMin.MinColor = minColor;
                varMin.MaxColor = maxColor;
                varMax.MinColor = minColor;
                varMax.MaxColor = maxColor;
                listResult.Add(varMin);
                listResult.Add(varMax);
            }


            return listResult;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="listData"></param>
        /// <param name="isOutLine">是否做轮廓过滤</param>
        /// <returns></returns>
        private List<View3DPoint> GetOutlinePoint(List<View3DPoint> listData,bool isOutLine)
        {
            if (listData.Count <= 0)
            {
                return new List<View3DPoint>();
            }
            float maxColor = listData.Max(s => s.Color);
            float minColor = listData.Min(s => s.Color);
            float xx = listData.Max(s => s.X);
            float yy = listData.Max(s => s.Y);
            float zz = listData.Max(s => s.Z);
            float xx1 = listData.Min(s => s.X);
            float yy2 = listData.Min(s => s.Y);
            float zz3 = listData.Min(s => s.Z);

            //
            float movex = (xx - xx1) / 2 + xx1;
            float movey = (yy - yy2) / 2 + yy2;
            float movez = (zz - zz3) / 2 + zz3;

            List<View3DPoint> listResult = new List<View3DPoint>();
            if (!isOutLine)//不做轮廓过滤
            {
                foreach (View3DPoint point in listData)
                {
                    point.MaxColor = maxColor;
                    point.MinColor = minColor;
                    point.X -= movex;//将坐标点移动到以0,0,0为中心
                    point.Y -= movey;
                    point.Z -= movez;
                }
                return listData;
            }
            var varX = listData.GroupBy(s => s.X);//相同X坐标的分一组
            var varY = listData.GroupBy(s => s.Y);//相同Y坐标的分一组

            //var varZ = listData.GroupBy(s => s.Z);//相同Z坐标的分一组
            foreach (var varM in varX)//取X坐标最大值和最小值对应的坐标
            {
                var varMin = varM.OrderByDescending(s => s.Y).First();
                varMin.MinColor = minColor;
                varMin.MaxColor = maxColor;
                var varMax = varM.OrderBy(s => s.Y).First();
                varMax.MinColor = minColor;
                varMax.MaxColor = maxColor;
                listResult.Add(varMin);
                listResult.Add(varMax);
            }

            foreach (var varM in varY)//取Y坐标最大值和最小值对应的坐标
            {
                var varMin = varM.OrderByDescending(s => s.X).First();
                var varMax = varM.OrderBy(s => s.X).First();
                varMin.MinColor = minColor;
                varMin.MaxColor = maxColor;
                varMax.MinColor = minColor;
                varMax.MaxColor = maxColor;
                listResult.Add(varMin);
                listResult.Add(varMax);
            }


            return listResult;
        }

    }
}
