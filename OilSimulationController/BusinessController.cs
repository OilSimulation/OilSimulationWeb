using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using OilSimulationModel;
using EclipseUtils;
using System.IO;
using System.Drawing;

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
        /// <summary>
        /// 判断本地是否可用
        /// </summary>
        /// <returns></returns>
        public ActionResult IsLocal()
        {  
            string strData = "本地可用"; 
            return Json(strData, JsonRequestBehavior.AllowGet); 
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
        private List<float[]> GetAllPoint(EclipseModel model, string szProName, int iStep, int k, string egridFilePath)
        {
            List<float[]> lst = new List<float[]>();
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

                                    lst.Add(new float[4] { center.x, center.y, center.z, v });
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
                                //Pillar p = model.GetGridAtIJK(i, j, k);
                                PillarPoint center = model.GetGridAtIJK(i, j, k).Center;
                                int pos = k * model.nx * model.ny + j * model.nx + i;
                                ///!!!! 这里的v才是网格(i,j,k)上的属性值
                                float v = propValues[pos];
                                View3DPoint point = new View3DPoint();
                                point.X = center.x;
                                point.Y = center.y;
                                point.Z = center.z;
                                point.Color = v;
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
                                    View3DPoint point = new View3DPoint();
                                    point.X = center.x;
                                    point.Y = center.y;
                                    point.Z = center.z;
                                    point.Color = v;
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
                                //Pillar p = model.GetGridAtIJK(i, j, k);
                                PillarPoint center = model.GetGridAtIJK(i, j, k).Center;
                                int pos = k * model.nx * model.ny + j * model.nx + i;
                                ///!!!! 这里的v才是网格(i,j,k)上的属性值
                                float v = propValues[pos];
                                View3DPoint point = new View3DPoint();
                                point.X = center.x;
                                point.Y = center.y;
                                point.Z = center.z;
                                point.Color = v;
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
                                    View3DPoint point = new View3DPoint();
                                    point.X = center.x;
                                    point.Y = center.y;
                                    point.Z = center.z;
                                    point.Color = v;
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
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetData()
        { 
            string eGridFile = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/水驱油效率实验/不同原油密度/gao1.15/GAOMI_E100.EGRID");
            EclipseModel gridModel = EclipseParser.ParseEgrid(eGridFile);

            List<float[]> lstData = new List<float[]>();
            for ( int i = 0; i < gridModel.nz; i++ )
            {
                lstData.AddRange(GetAllPoint(gridModel, "SOIL", 1, i, eGridFile));
            }
            var res = new ConfigurableJsonResult();
            res.Data = lstData;
            return res;
        }



        [HttpPost]
        public ActionResult GetModelData()
        {
            string eGridFile = System.Web.HttpContext.Current.Server.MapPath("~/DataModel/虚拟实验/水驱油效率实验/不同原油密度/gao1.15/GAOMI_E100.EGRID");
            EclipseModel gridModel = EclipseParser.ParseEgrid(eGridFile);

            List<View3DPoint> lstData = new List<View3DPoint>();
            for (int i = 0; i < gridModel.nz; i++)
            {
                lstData.AddRange(GetOutlinePoint(Get3DAllPoint(gridModel, "SOIL", 1, i, eGridFile)));
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
            //var varZ = listData.GroupBy(s => s.Z);//相同Z坐标的分一组
            foreach (var varM in varX)//取X坐标最大值和最小值对应的坐标
            {
                var varMin = varM.OrderByDescending(s => s.Y).First();
                var varMax = varM.OrderBy(s => s.Y).First();
                listResult.Add(varMin);
                listResult.Add(varMax);
            }

            foreach (var varM in varY)//取Y坐标最大值和最小值对应的坐标
            {
                var varMin = varM.OrderByDescending(s => s.X).First();
                var varMax = varM.OrderBy(s => s.X).First();
                listResult.Add(varMin);
                listResult.Add(varMax);
            }


            return listResult;
        }
    }
}
