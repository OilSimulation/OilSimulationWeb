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

        private  const string WELSPECS = "WELSPECS";
        private  const string COMPDAT = "COMPDAT";
        private  const string WCONPROD = "WCONPROD";
        private  const string WCONINJE = "WCONINJE";



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
            string targetDir = szEgridPath.Substring(0, szEgridPath.LastIndexOf("\\"));
            try
            {
                System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.StartInfo.WorkingDirectory = targetDir;
                myProcess.StartInfo.FileName = "RUN.BAT";
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


            stModeData.WellPoint = GetWellPoint(gridModel,strWellFilePath);

            
            stModeData.lev = gridModel.nz;
            var res = new ConfigurableJsonResult();
            res.Data = stModeData; 
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
           
            return res;
        }

        /// <summary>
        /// 修改油井坐标
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateWellPoint(List<PostDataWellPoint> data)
        {
            string strFilePath = @"E:\Code\OilSimulationWeb\OilSimulationWeb\DataModel\仿真实训\注采系统方案设计与开发效果预测\不同注采比\1\ZHUCAIBI1_SCH.INC";
            if (ModelState.IsValid)
            {

            }
            List<string> listData = CommonModel.ReadInfoFromFile(strFilePath);

            int firstIndex = listData.IndexOf(WELSPECS);//第一个出现 WELSPECS标识索引
            int index = firstIndex;
            while (index >= 0)
            {
                //删除之前的生产井和注水井
                listData.RemoveRange(index, 4);
                index = listData.IndexOf(WELSPECS, index);
            }

            //增加生产井和注水井
            for (int i=0;i<data.Count;i++)
            {
                if (data[i].Type=="OIL")
                {
                    for (int j = 0; j < data[i].wellPoint.Count; j++)
                    {
                        string str = "'PRO" + (j + 1) + "' 'P' " + data[i].wellPoint[j].X + " " + data[i].wellPoint[j].Y + " 'OIL' 1* 'STD' 'SHUT' 'YES' 1* 'SEG' 3* 'STD' /";
                        listData.Insert(firstIndex++, WELSPECS);
                        listData.Insert(firstIndex++, str);
                        listData.Insert(firstIndex++, "/");
                        listData.Insert(firstIndex++, " ");
                        
                    }

                }
                else if (data[i].Type=="WATER")
                {
                    for (int j = 0; j < data[i].wellPoint.Count; j++)
                    {
                        string str = "'INJ" + (j + 1) + "' 'P' " + data[i].wellPoint[j].X + " " + data[i].wellPoint[j].Y + " 'WATER' 1* 'STD' 'SHUT' 'YES' 1* 'SEG' 3* 'STD' /";
                        listData.Insert(firstIndex++, WELSPECS);
                        listData.Insert(firstIndex++, str);
                        listData.Insert(firstIndex++, "/");
                        listData.Insert(firstIndex++, " ");
                        
                    }

                }
            }


            //删除之前的COMPDAT
            firstIndex = listData.IndexOf(COMPDAT);
            index = firstIndex;
            while (index >= 0)
            {
                listData.RemoveRange(index, 4);
                index = listData.IndexOf(COMPDAT, index);
            }


            //增加COMPDAT
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].Type == "OIL")
                {

                    for (int j = 0; j < data[i].wellPoint.Count; j++)
                    {
                        string str = "'PRO" + (j + 1) + "' 2* 1 3 'OPEN' 2* 0.01 3* 'Z' 1* /";
                        listData.Insert(firstIndex++, COMPDAT);
                        listData.Insert(firstIndex++, str);
                        listData.Insert(firstIndex++, "/");
                        listData.Insert(firstIndex++, " ");

                    }


                }
                else if (data[i].Type == "WATER")
                {



                    for (int j = 0; j < data[i].wellPoint.Count; j++)
                    {
                        string str = "'INJ" + (j + 1) + "' 2* 1 3 'OPEN' 2* 0.01 3* 'Z' 1* /";
                        listData.Insert(firstIndex++, COMPDAT);
                        listData.Insert(firstIndex++, str);
                        listData.Insert(firstIndex++, "/");
                        listData.Insert(firstIndex++, " ");

                    }


                }
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
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].Type == "OIL")
                {

                    for (int j = 0; j < data[i].wellPoint.Count; j++)
                    {
                        string str = "'PRO" + (j + 1) + "' 'OPEN' 'LRAT' 3* 100 1* 10 3* /";
                        listData.Insert(firstIndex++, WCONPROD);
                        listData.Insert(firstIndex++, str);
                        listData.Insert(firstIndex++, "/");
                        listData.Insert(firstIndex++, " ");

                    }


                }
               
            }

            //删除之前的WCONINJE
            firstIndex = listData.IndexOf(WCONINJE);
            index = firstIndex;
            while (index >= 0)
            {
                listData.RemoveRange(index, 4);
                index = listData.IndexOf(WCONINJE, index);
            }


            //增加WCONINJE
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].Type == "WATER")
                {

                    for (int j = 0; j < data[i].wellPoint.Count; j++)
                    {
                        string str = "'INJ" + (j + 1) + "' 'WATER' 'OPEN' 'RATE' 133 1* 400 3* /";
                        listData.Insert(firstIndex++, WCONINJE);
                        listData.Insert(firstIndex++, str);
                        listData.Insert(firstIndex++, "/");
                        listData.Insert(firstIndex++, " ");

                    }
                }
            }

            CommonModel.WriteInfoToFile(strFilePath, listData);


            return 1;
            //调用CMD.exe
            //ExecBatCommand(s=>s())
            
        }




        private List<WellData> GetWellPoint(EclipseModel eModel,string filePath)
        {
            List<WellData> listWellData = new List<WellData>();
            List<string> listData = CommonModel.ReadInfoFromFile(filePath);//获取井坐标文件数据
            int wallIndex = listData.IndexOf(WELSPECS);//找到出现 WELSPECS 油井标识的行
            while (wallIndex >= 0)
            {

                listWellData.Add(ParseStrToWell(eModel,listData[wallIndex + 1]));
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
