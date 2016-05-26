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

        private const string WELSPECS = "WELSPECS";
        private const string COMPDAT = "COMPDAT";
        private const string WCONPROD = "WCONPROD";
        private const string WCONINJE = "WCONINJE";
        private const string TSTEP = "TSTEP";
        private const string SWOF = "SWOF";
        private const string DENSITY = "DENSITY";
        /// <summary>
        /// 井总数标志
        /// </summary>
        private const string WELLDIMS = "WELLDIMS";
        /// <summary>
        /// 注彩比 标志
        /// </summary>
        private const string GCONINJE = "GCONINJE";
        private const string PVTW = "PVTW";
        private const string PVCDO = "PVCDO";




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
        /// 运行批处理命令
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public ActionResult ExecBatCommand(PostData inputData)
        {
            int iModel = 0;
            if (ModelState.IsValid)
            {
                iModel = inputData.Mode;
            }
            string szEgridPath = CommonModel.GetModeUriPath(iModel);
            string targetDir = System.IO.Path.GetDirectoryName(szEgridPath);
            //string targetDir = szEgridPath.Substring(0, szEgridPath.LastIndexOf("\\") + 1);
            try
            {
                System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.StartInfo.WorkingDirectory = targetDir;
                myProcess.StartInfo.FileName = targetDir + "\\RUN.BAT";
                myProcess.Start();
                myProcess.WaitForExit();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return new JsonResult();
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
            Pillar p = model.GetGridAtIJK(model.nx / 2, model.ny / 2, iLevel - 1);
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
            if (lstDynamicPara.IndexOf(szProName) != -1)
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
            string eGridFile = CommonModel.GetModeUriPath(iModel);
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
            //if (iModel==15)//球面径向流
            //{
            //    float r = 0, q = 0, z = 0;
            //    for (int i = 0;i< stModeData.Data.Count;i++ )
            //    {
            //        if (stModeData.Data[i].Length<3)
            //        {
            //            break;
            //        }
            //        q = stModeData.Data[i][0];
            //        r = stModeData.Data[i][1];
            //        z = stModeData.Data[i][2];
            //        //将柱坐标转成直角坐标
            //        stModeData.Data[i][0] = r * (float)Math.Cos(q);
            //        stModeData.Data[i][0] = r * (float)Math.Sin(q);
            //    }
            //}


            stModeData.WellPoint = GetWellPoint(gridModel, strWellFilePath);

            if (iModel == 15)
            {
                string filePath = eGridFile.Substring(0, eGridFile.IndexOf("_E")) + "_ggo.INC";
                int circleCount = 0, count = 0, zCount = 0;
                GetGGO(filePath, out circleCount, out count, out zCount);
                //stModeData.Data.Clear();
                
                stModeData.Data[0][0] = circleCount;
                stModeData.Data[0][1] = count;
                stModeData.Data[0][2] = zCount;


            }

            //0 角度 1 半径 2 Z
            stModeData.lev = gridModel.nz;
            var res = new ConfigurableJsonResult();
            res.Data = stModeData;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");

            return res;
        }


        /// <summary>
        /// 修改井距离
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateWellDistance(PostData data)
        {
            int iModel = 0;

            if (ModelState.IsValid)
            {
                iModel = data.Mode;
            }
            string szEgridPath = CommonModel.GetModeUriPath(iModel);
            //DX,DY
            string strFilePath = szEgridPath.Substring(0, szEgridPath.IndexOf("_E")) + "_GGO.INC";
            string strWellCountFilePath = szEgridPath.Substring(0, szEgridPath.IndexOf("_E")) + "_E100.DATA";
            //小方格X方向距离，Y方向距离
            Box box = GetDXDY(strFilePath);
            Point pXY = GetWellDistanceCount(data.Step, box.DxWidth, box.DyWidth);
            List<WellPoint> listOil = new List<WellPoint>();
            List<WellPoint> listWater = new List<WellPoint>();
            BuildOilWaterPoint(box.BoxXCount, box.BoxYCount, pXY.X, pXY.Y, out listOil, out listWater);
            UpdateWellMaxCount(strWellCountFilePath, listOil.Count + listWater.Count);//修改油井总数
            PostDataWellPoint wellPoint = new PostDataWellPoint();
            wellPoint.modelId = iModel;
            wellPoint.P = new List<WellPoint>();
            wellPoint.P.AddRange(listOil);
            wellPoint.I = new List<WellPoint>();
            wellPoint.I.AddRange(listWater);

            UpdateWellPoint(wellPoint);

            return new JsonResult();
        }



        /// <summary>
        /// 修改油井坐标
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateWellPoint(PostDataWellPoint data)
        {
            int iModel = 0;

            if (ModelState.IsValid)
            {
                iModel = data.modelId;
            }
            //Grid文件
            string eGridFile = CommonModel.GetModeUriPath(iModel);
            //油井文件
            string strFilePath = eGridFile.Substring(0, eGridFile.IndexOf("_E")) + "_sch.INC";


            List<string> listData = CommonModel.ReadInfoFromFile(strFilePath);


            //List<string> listWELSPECS = new List<string>();
            List<string> listOIL = new List<string>();
            List<string> listWATER = new List<string>();
            int firstIndex = listData.IndexOf(WELSPECS);//第一个出现 WELSPECS标识索引
            int index = firstIndex;
            while (index >= 0)
            {
                if (listData[index + 1].Contains("OIL"))
                {
                    listOIL.Add(listData[index + 1]);
                }
                else
                {
                    listWATER.Add(listData[index + 1]);
                }

                //删除之前的生产井和注水井
                listData.RemoveRange(index, 4);
                index = listData.IndexOf(WELSPECS, index);
            }

            //增加生产井
            for (int i = 0; i < data.P.Count; i++)
            {
                //string str = "'PRO" + (i + 1) + "' 'P' " + data.P[i].x + " " + data.P[i].y + " 'OIL' 1* 'STD' 'SHUT' 'YES' 1* 'SEG' 3* 'STD' /";
                string str = "'PRO" + (i + 1) + "' 'P' " + data.P[i].y + " " + data.P[i].x + " 1* ";

                if (i < listOIL.Count)
                {
                    str += listOIL[i].Substring(listOIL[i].IndexOf("'OIL'"), listOIL[i].Length - listOIL[i].IndexOf("'OIL'"));
                }
                else
                {
                    str += listOIL[listOIL.Count - 1].Substring(listOIL[listOIL.Count - 1].IndexOf("'OIL'"), listOIL[listOIL.Count - 1].Length - listOIL[listOIL.Count - 1].IndexOf("'OIL'"));

                }
                listData.Insert(firstIndex++, WELSPECS);
                listData.Insert(firstIndex++, str);
                listData.Insert(firstIndex++, "/");
                listData.Insert(firstIndex++, " ");
            }
            //增加注水井
            for (int i = 0; i < data.I.Count; i++)
            {

                //string str = "'INJ" + (i + 1) + "' 'P' " + data.I[i].x + " " + data.I[i].y + " 'WATER' 1* 'STD' 'SHUT' 'YES' 1* 'SEG' 3* 'STD' /";
                string str = "'INJ" + (i + 1) + "' 'P' " + data.I[i].y + " " + data.I[i].x + " 1* ";
                int indexWater = 0;
                if (i < listWATER.Count)
                {
                    indexWater = listWATER[i].IndexOf("'WATER'");
                    str += listWATER[i].Substring(indexWater, listWATER[i].Length - indexWater);
                }
                else
                {
                    indexWater = listWATER[listWATER.Count - 1].IndexOf("'WATER'");
                    str += listWATER[listWATER.Count - 1].Substring(indexWater, listWATER[listWATER.Count - 1].Length - indexWater);

                }

                listData.Insert(firstIndex++, WELSPECS);
                listData.Insert(firstIndex++, str);
                listData.Insert(firstIndex++, "/");
                listData.Insert(firstIndex++, " ");
            }


            //List<string> listCOMPDAT = new List<string>();
            //删除之前的COMPDAT
            firstIndex = listData.IndexOf(COMPDAT);
            index = firstIndex;
            while (index >= 0)
            {
                //listCOMPDAT.Add(listData[index + 1]);
                listData.RemoveRange(index, 4);
                index = listData.IndexOf(COMPDAT, index);
            }


            //增加COMPDAT
            for (int i = 0; i < data.P.Count; i++)
            {

                string str = "'PRO" + (i + 1) + "' 2* 1 3 'OPEN' 2* 0.01 3* 'Z' 1* /";
                listData.Insert(firstIndex++, COMPDAT);
                listData.Insert(firstIndex++, str);
                listData.Insert(firstIndex++, "/");
                listData.Insert(firstIndex++, " ");

            }
            //COMPDAT
            for (int i = 0; i < data.I.Count; i++)
            {
                string str2 = "'INJ" + (i + 1) + "' 2* 1 3 'OPEN' 2* 0.01 3* 'Z' 1* /";
                listData.Insert(firstIndex++, COMPDAT);
                listData.Insert(firstIndex++, str2);
                listData.Insert(firstIndex++, "/");
                listData.Insert(firstIndex++, " ");
            }



            //删除之前的WCONPROD
            firstIndex = listData.IndexOf(WCONPROD);
            index = firstIndex;
            while (index >= 0)
            {
                listData.RemoveRange(index, 4);
                index = listData.IndexOf(WCONPROD, index);
            }
            //增加WCONPROD
            for (int i = 0; i < data.P.Count; i++)
            {

                string str = "'PRO" + (i + 1) + "' 'OPEN' 'LRAT' 3* 100 1* 10 3* /";
                listData.Insert(firstIndex++, WCONPROD);
                listData.Insert(firstIndex++, str);
                listData.Insert(firstIndex++, "/");
                listData.Insert(firstIndex++, " ");

            }


            List<string> listWCONINJE = new List<string>();
            //删除之前的WCONINJE
            firstIndex = listData.IndexOf(WCONINJE);
            index = firstIndex;
            while (index >= 0)
            {
                listWCONINJE.Add(listData[index + 1]);
                listData.RemoveRange(index, 4);
                index = listData.IndexOf(WCONINJE, index);
            }

            int indexWCONINJE = 0;
            //增加WCONINJE
            for (int i = 0; i < data.I.Count; i++)
            {
                //string str = "'INJ" + (i + 1) + "' 'WATER' 'OPEN' 'RATE' 133 1* 400 3* /";
                string str = "'INJ" + (i + 1) + "' ";

                if (i < listWCONINJE.Count)
                {
                    indexWCONINJE = listWCONINJE[i].IndexOf("'WATER'");
                    str += listWCONINJE[i].Substring(indexWCONINJE, listWCONINJE[i].Length - indexWCONINJE);
                }
                else
                {
                    indexWCONINJE = listWCONINJE[listWCONINJE.Count - 1].IndexOf("'WATER'");
                    str += listWCONINJE[listWCONINJE.Count - 1].Substring(indexWCONINJE, listWCONINJE[listWCONINJE.Count - 1].Length - indexWCONINJE);
                }
                listData.Insert(firstIndex++, WCONINJE);
                listData.Insert(firstIndex++, str);
                listData.Insert(firstIndex++, "/");
                listData.Insert(firstIndex++, " ");

            }

            CommonModel.WriteInfoToFile(strFilePath, listData);


            return new JsonResult();

        }




        private List<WellData> GetWellPoint(EclipseModel eModel, string filePath)
        {
            List<WellData> listWellData = new List<WellData>();
            List<string> listData = CommonModel.ReadInfoFromFile(filePath);//获取井坐标文件数据
            int wallIndex = listData.IndexOf(WELSPECS);//找到出现 WELSPECS 油井标识的行
            while (wallIndex >= 0)
            {

                listWellData.Add(ParseStrToWell(eModel, listData[wallIndex + 1]));
                wallIndex = listData.IndexOf(WELSPECS, wallIndex + 1);

            }

            return listWellData;

        }

        /// <summary>
        /// 解析字符串('PRO2' 'P' 35 65 1* 'OIL' 1* 'STD' 'SHUT' 'YES' 1* 'SEG' 3* 'STD' /)
        /// 井坐标文件中 记录是的网格位置，通过EclipseModel换算也 井坐标
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        private WellData ParseStrToWell(EclipseModel eModel, string strData)
        {
            WellData data = new WellData();
            int x, y, z;
            string[] listData = strData.Split(' ');
            List<string> listd = listData.Where(s => s.Trim() != string.Empty).ToList();
            data.name = listd[0].Replace('\'', ' ').Trim();
            int.TryParse(listd[2], out x);
            int.TryParse(listd[3], out y);
            int.TryParse(listd[4].Substring(0, 1), out z);

            PillarPoint pPoint = eModel.GetGridAtIJK(x - 1, y - 1, 0).Center;

            data.x = pPoint.x;
            data.y = pPoint.y;
            data.z = pPoint.z;
            data.type = listd[5].Replace('\'', ' ').Trim();


            return data;
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
        /// 获取X方向网格数，Y方向网格数
        /// </summary>
        /// <param name="distance">距离</param>
        /// <param name="dxWidth">x方向网格实际距离</param>
        /// <param name="dwWidth">y方向网格实际距离</param>
        /// <returns>X方向网格个数，Y方向网格个数</returns>
        private Point GetWellDistanceCount(int distance, int dxWidth, int dyWidth)
        {
            if (distance < dxWidth || distance < dyWidth)
            {
                return new Point(0, 0);
            }

            return new Point(distance / dxWidth, distance / dyWidth);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">x方向网格数(如100)</param>
        /// <param name="y">y方向网格数(如100)</param>
        /// <param name="width">井间网格个数(即多少个网格)</param>
        /// <param name="Oil">生产井列表</param>
        /// <param name="Water">注水井列表</param>
        private void BuildOilWaterPoint(int x, int y, int dxCount, int dyCount, out List<WellPoint> Oil, out List<WellPoint> Water)
        {
            Oil = new List<WellPoint>();
            Water = new List<WellPoint>();

            if (x <= 1 || y <= 1 || dxCount <= 0 || dxCount > x || dyCount > y || dyCount <= 0)
            {
                return;
            }
            //int xCount = x / dxCount - 1;
            //int yCount = y / dyCount - 1;
            int xCount = x / dxCount;
            int yCount = y / dyCount;

            for (int i = 0; i <= xCount; i++)
            {
                for (int j = 0; j <= yCount; j++)
                {
                    WellPoint pOil = new WellPoint();
                    //pOil.x = i * dxCount; 
                    pOil.x = i * dxCount + 1;
                    pOil.y = j * dyCount + 1;
                    if (pOil.x <= x && pOil.y <= y)
                    {
                        Oil.Add(pOil);
                    }

                    WellPoint pWater = new WellPoint();
                    //pWater.x = i * dxCount + dxCount / 2; 
                    pWater.x = i * dxCount + 1 + dxCount / 2;
                    //pWater.y = j * dyCount + 1 + dyCount / 2;
                    pWater.y = j * dyCount + dyCount / 2;
                    if (pWater.x <= x && pWater.y <= y)
                    {
                        Water.Add(pWater);
                    }
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="listData"></param>
        /// <param name="isOutLine">是否做轮廓过滤</param>
        /// <returns></returns>
        private List<View3DPoint> GetOutlinePoint(List<View3DPoint> listData, bool isOutLine)
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


        /// <summary>
        /// 修改可布井总数
        /// </summary>
        /// <param name="filePath">记录井总文件路径(如:JINGJU500_E100.DATA)</param>
        /// <param name="count"></param>
        private void UpdateWellMaxCount(string filePath, int count)
        {
            List<string> listData = CommonModel.ReadInfoFromFile(filePath);
            int index = listData.IndexOf(WELLDIMS);
            if (index > 0)
            {
                string[] strs = listData[index + 1].Split(' ');
                List<string> list = new List<string>();
                foreach (string str in strs)
                {
                    if (str.Trim() != "")
                    {
                        list.Add(str);
                    }
                }

                string strValue = count + " " + list[1] + " " + list[2] + " " + count + " /";
                listData[index + 1] = strValue;
            }

            CommonModel.WriteInfoToFile(filePath, listData);
        }


        /// <summary>
        /// 获取X,Y方向小方格宽度
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>小方格X方向宽度，Y方向宽度</returns>
        private Box GetDXDY(string filePath)
        {
            Box box = new Box();
            try
            {
                List<string> listData = CommonModel.ReadInfoFromFile(filePath);
                int dxIndex = listData.IndexOf("DX");
                int dyIndex = listData.IndexOf("DY");

                int dx = Convert.ToInt32(listData[dxIndex + 2].Split(' ')[0]);
                int dy = Convert.ToInt32(listData[dyIndex + 2].Split(' ')[0]);



                box.DxWidth = dx;
                box.DyWidth = dy;

                int xCount = Convert.ToInt32(listData[dxIndex - 1].Split(',')[1].Split(':')[1]);
                int yCount = Convert.ToInt32(listData[dyIndex - 1].Split(',')[1].Split(':')[1]);
                box.BoxXCount = xCount;
                box.BoxYCount = yCount;
                return box;

            }
            catch (System.Exception ex)
            {
                return box;
            }



        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="circleCount">多少圈</param>
        /// <param name="count">一个圆分割的分数</param>
        /// <param name="zCount">Z方向上个数</param>
        private void GetGGO(string filePath, out int circleCount, out int count, out int zCount)
        {
            List<string> listData = CommonModel.ReadInfoFromFile(filePath);
            int index = listData.IndexOf("DZ")-1;
            string strData = listData[index];

            int index1 = strData.IndexOf("(");
            int index2 = strData.IndexOf(")");
            strData = strData.Substring(index1 + 1, index2 - index1 - 1);
            string[] strs = strData.Split(',');
            int.TryParse(strs[0].Split(':')[1], out circleCount);
            int.TryParse(strs[1].Split(':')[1], out count);
            int.TryParse(strs[2].Split(':')[1], out zCount);
        }


        #region ************************************  仿真实训-注采系统方案设计与开发效果预测

        /// <summary>
        ///修改注水时机文件_sch.inc
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="day">第多少天进行注水</param>
        private  void UpdateInWaterfloodingOpporunity(string filePath, int day)
        {
            //List<string> listData = new List<string>();
            //listData.AddRange(CommonModel.ReadInfoFromFile(filePath));
            try
            {
                List<string> listData = CommonModel.ReadInfoFromFile(filePath);
                List<string> listWater = new List<string>();

                //多少天分割的天数
                int daySplit = 0;
                int indexTSTEP = listData.IndexOf(TSTEP);
                if (indexTSTEP > 0)
                {
                    int.TryParse(listData[indexTSTEP + 1].Replace('/', ' ').Trim(), out daySplit);
                }
                else
                {
                    return;
                }

                List<string> listWaterZero = new List<string>();
                int index = listData.IndexOf(WCONINJE);
                while (index >= 0)
                {

                    string str = listData[index + 1].Split(' ')[4];
                    if (str.Trim() == "0")
                    {
                        listWaterZero.Add(listData[index]);
                        listWaterZero.Add(listData[index + 1]);
                        listWaterZero.Add(listData[index + 2]);
                        listWaterZero.Add(listData[index + 3]);
                    }
                    else
                    {
                        listWater.Add(listData[index]);
                        listWater.Add(listData[index + 1]);
                        listWater.Add(listData[index + 2]);
                        listWater.Add(listData[index + 3]);
                    }

                    listData.RemoveRange(index, 4);
                    index = listData.IndexOf(WCONINJE, index);
                }
                //第多少行注水
                int rowDay = day / daySplit;

                //TSTEP 第一个索引位置
                int startTSTEPIndex = 0;
                int i = 0;
                indexTSTEP = listData.IndexOf(TSTEP);
                startTSTEPIndex = indexTSTEP;
                while (indexTSTEP >= 0)
                {
                    //找到数据增加的对应索引
                    if (i == rowDay)
                    {
                        if (rowDay == 0)
                        {
                            //一开始就注水
                            //listData.Insert(indexTSTEP-1,)
                            listData.InsertRange(indexTSTEP - 1, listWater);
                        }
                        else
                        {
                            //后面开始注水,需要在第一个TSTEP前面加一个'INJ9' 'WATER' 'OPEN' 'RATE' 0 1* 400 3* /  "0"的值
                            //再在对应的TSTEP下加入注水信息。
                            if (listWaterZero.Count <= 0)
                            {
                                //构造一个ZERO的数据
                                for (int k = 0; k < listWater.Count; k++)
                                {
                                    if (listWater[k].Contains("RATE"))
                                    {
                                        string[] strs = listWater[k].Split(' ');
                                        strs[4] = "0";
                                        string str = "";
                                        for (int j = 0; j < strs.Length; j++)
                                        {
                                            if (j == strs.Length - 1)
                                            {
                                                str += strs[j];
                                            }
                                            else
                                            {
                                                str += strs[j] + " ";
                                            }
                                        }
                                        listWaterZero.Add(str);
                                    }
                                    else
                                    {
                                        listWaterZero.Add(listWater[k]);
                                    }
                                }


                            }
                            //水的索引在下面,先加水,
                            listData.InsertRange(indexTSTEP, listWater);
                            //再加ZERO
                            listData.InsertRange(startTSTEPIndex, listWaterZero);

                        }

                        break;
                    }

                    i++;
                    indexTSTEP = listData.IndexOf(TSTEP, indexTSTEP + 1);
                }



                CommonModel.WriteInfoToFile(filePath, listData);

            }
            catch (System.Exception ex)
            {
                return;
            }

        }

        /// <summary>
        /// 修改注采比
        /// </summary>
        /// <param name="filePath">文件路径_sch.inc</param>
        /// <param name="percent">百分比</param>
        private void UpdateInColorPercent(string filePath, double percent)
        {
            List<string> listData = CommonModel.ReadInfoFromFile(filePath);
            int index = listData.IndexOf(GCONINJE);
            while (index >= 0)
            {
                string[] strs = listData[index + 1].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                strs[4] = percent.ToString();
                string str = "";
                for (int i = 0; i < strs.Length;i++ )
                {
                    if (str.Length-1==i)
                    {
                        str += strs[i];
                    }
                    else
                    {
                        str += strs[i] + " ";
                    }
                }
                listData[index + 1] = str;
                index = listData.IndexOf(GCONINJE, index + 1);
            }
            CommonModel.WriteInfoToFile(filePath, listData);
        }

        /// <summary>
        /// 修改 不同最大井底注入压力
        /// </summary>
        /// <param name="filePath">文件路径_sch.inc</param>
        /// <param name="pressure">压力</param>
        private void UpdateMaxWellBottomPressure(string filePath, int pressure)
        {
            List<string> listData = CommonModel.ReadInfoFromFile(filePath);
            int index = listData.IndexOf(WCONINJE);
            while (index >= 0)
            {
                string[] strs = listData[index + 1].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                strs[6] = (pressure * 10).ToString();
                string str = "";
                for (int i = 0; i < strs.Length;i++ )
                {
                    if (i==strs.Length-1)
                    {
                        str += strs[i];
                    }
                    else
                    {
                        str += strs[i] + " ";
                    }
                }

                listData[index + 1] = str;
                index = listData.IndexOf(WCONINJE, index+1);
            }

            CommonModel.WriteInfoToFile(filePath, listData);
        }

        /// <summary>
        /// 修改不同最小井底流压
        /// </summary>
        /// <param name="filePath">文件路径_sch.inc</param>
        /// <param name="pressure">压力</param>
        private void UpdateMinWellBottomPressure(string filePath, int pressure)
        {
            List<string> listData = CommonModel.ReadInfoFromFile(filePath);
            int index = listData.IndexOf(WCONPROD);
            while (index >= 0)
            {
                string[] strs = listData[index + 1].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                strs[6] = (pressure * 10).ToString();
                string str = "";
                for (int i = 0; i < strs.Length;i++ )
                {
                    if (i==strs.Length-1)
                    {
                        str += strs[i];
                    }
                    else
                    {
                        str += strs[i] + " ";
                    }
                }
                listData[index + 1] = str;
                index = listData.IndexOf(WCONPROD, index + 1);
            }
            CommonModel.WriteInfoToFile(filePath, listData);
        }
        #endregion


        #region ************************************  虚拟实验-非活塞式驱油影响因素

        /// <summary>
        /// 修改 毛管压力
        /// </summary>
        /// <param name="filePath">文件路径 _scal.inc</param>
        /// <param name="pressure"></param>
        private void UpdateCapillaryPressure(string filePath, double pressure)
        {
            List<string> listData =  CommonModel.ReadInfoFromFile(filePath);
            int index = listData.IndexOf(SWOF);
            index += 4;//移四行
            int endIndex = listData.IndexOf("/", index);
            // listData.GetRange(index, endIndex - index - 1);
            
            List<string> listEndCol = CalcCapillary(pressure, endIndex - index - 1);
            for (int i = 0; i < listEndCol.Count;i++ )
            {
                string[] strs = listData[index + i + 1].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                strs[3] = listEndCol[i];
                string str = "";
                for (int k = 0; k < strs.Length;k++ )
                {
                    if (k==strs.Length-1)
                    {
                        str += strs[k];
                    }
                    else
                    {
                        str += strs[k] + " ";
                    }
                }
                listData[index + i + 1] = str;

            }

            CommonModel.WriteInfoToFile(filePath, listData);


        }

        /// <summary>
        /// 计算毛管其它参数,还需要提供
        /// </summary>
        /// <param name="pressure"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private List<string> CalcCapillary(double pressure,int row)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < row;i++ )
            {
                list.Add((pressure * i).ToString());
            }
            return list;

        }


        /// <summary>
        /// 修改密度
        /// </summary>
        /// <param name="filePath">文件路径_pvt.inc</param>
        /// <param name="density">0.5-1.5</param>
        private void UpdateDensity(string filePath, double density)
        {
            if (density < 0.5 || density > 1.5)
            {
                return;
            }
            //油 水 汽,水定死1000,density=油/水;
            List<string> listData = CommonModel.ReadInfoFromFile(filePath);
            int index = listData.IndexOf(DENSITY);
            index += 4;
            string[] strs = listData[index].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            int oil = Convert.ToInt32(density * 1000);
            string str = oil + " " + 1000 + " " + strs[2];
            listData[index] = str;
            CommonModel.WriteInfoToFile(filePath, listData);


        }


        /// <summary>
        /// 修改粘度
        /// </summary>
        /// <param name="filePath">文件路径_pvt.inc</param>
        /// <param name="treacliness">1-600</param>
        /// <returns>oil-water</returns>
        private double UpdateTreacliness(string filePath, int treacliness)
        {
            if (treacliness < 1 || treacliness > 600)
            {
                return 0;
            }
            //treacliness=oil/water treacliness1-600,显示差值=oli-water,water定死0.5
            List<string> listData = CommonModel.ReadInfoFromFile(filePath);
            int indexPVTW = listData.IndexOf(PVTW);//water 
            int indexPVCDO = listData.IndexOf(PVCDO);//oil
            double water = 0.5;
            double oil = water * treacliness;

            indexPVTW += 4;
            indexPVCDO += 4;
            string[] strsWater = listData[indexPVTW].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            string[] strsOil = listData[indexPVCDO].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            strsWater[3] = (0.5).ToString();
            strsOil[3] = oil.ToString();
            listData[indexPVTW] = ArrayToString(strsWater);
            listData[indexPVCDO] = ArrayToString(strsOil);
            CommonModel.WriteInfoToFile(filePath, listData);

            return oil - water;
        }

        

        /// <summary>
        /// 将数组转换成用空格分割的字符串
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        private string ArrayToString(string[] strs)
        {
            string str = "";
            for (int i = 0; i < strs.Length;i++ )
            {
                if (i==strs.Length-1)
                {
                    str += strs[i];
                }
                else
                {
                    str += strs[i] + " ";
                }
            }
            return str;
        }

        #endregion

    }
}
