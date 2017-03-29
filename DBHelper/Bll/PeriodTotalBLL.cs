using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBHelper.Model;
using System.Data.Common;
using System.Data.SQLite;
using System.Management;

namespace DBHelper.Bll
{
    public class PeriodTotalBLL
    {
        private string m_strConn;
        PeriodBLL PeriodBll;

        public PeriodTotalBLL(string strConn)
        {
            m_strConn = strConn;
            PeriodBll = new PeriodBLL(strConn);
        }
        /// <summary>
        /// 获取硬盘号
        /// </summary>
        /// <returns></returns>
        public  string GetHardDiskNumber()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
                string strHardDiskID = null;
                foreach (ManagementObject mo in searcher.Get())
                {
                    strHardDiskID = mo["SerialNumber"].ToString().Trim();
                    break;
                }
                return strHardDiskID;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public bool IsPeriod(string szHardNumber)
        {
            //return false;
            //UpdatePeriodTotal(new PeriodTotalInfo() { PeriodTotalId = 2, PeriodDay = 1, PeriodBool = true });
            DateTime CurrentDateTime = DateTime.Now;
            PeriodInfo info = PeriodBll.GetPeriod(szHardNumber);

            PeriodTotalInfo tinfo = GetPeriodTotal();
            if (tinfo == null)
            {
                //默认试用30天
                AddPeriodTotal(new PeriodTotalInfo()
                {
                    PeriodBool = false,
                    PeriodDay = 30
                });



                if (info == null)
                {
                    PeriodBll.AddPeriod(new PeriodInfo()
                    {
                        PeriodAlias = "",
                        PeriodDay = 30,
                        PeriodNumber = szHardNumber,
                        StartDateTime = CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss")
                    });
                }

                return false;
            }
            else
            {
                if (tinfo.PeriodBool)
                {
                    //试用版本
                    if (info == null)
                    {
                        PeriodBll.AddPeriod(new PeriodInfo()
                        {
                            PeriodAlias = "",
                            PeriodDay = 30,
                            PeriodNumber = GetHardDiskNumber(),
                            StartDateTime = CurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss")
                        });
                        return false;   
                    }
                    else
                    {
                        DateTime dt;
                        bool result = DateTime.TryParse(info.StartDateTime, out dt);
                        if (result)
                        {
                            if (dt.AddDays(tinfo.PeriodDay) < CurrentDateTime)
                            {
                                //到期
                                return true;   
                            }
                            else
                            {
                                return false;   
                            }
                        }
                        else
                        {
                            //时间类型转换失败，直接试用时间到期
                            return true;   
                        }
                    }
                }
                else
                {
                    //未开启试用版本 
                    return false;   
                }

            }

        }

        public PeriodTotalInfo GetPeriodTotal()
        {
            string strSql = "select * from PeriodTotal";
            IList<PeriodTotalInfo> listInfos = ModelConvertHelper<PeriodTotalInfo>.ConvertTo<PeriodTotalInfo>(
                DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteStrSql(strSql));
            if (listInfos.Count<=0)
            {
                return null;
            }
            else
            {
                return listInfos[0];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int AddPeriodTotal(PeriodTotalInfo info)
        {
            string strSql = "insert into PeriodTotal(PeriodDay,PeriodBool) values (@PeriodDay,@PeriodBool)";
            return DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteNonQuery(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=info.PeriodDay, ParameterName="@PeriodDay"},
                new SQLiteParameter(){  Value=info.PeriodBool, ParameterName="@PeriodBool"}
            });

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="info">根据ID修改信息</param>
        /// <returns></returns>
        public int UpdatePeriodTotal(PeriodTotalInfo info)
        {
            string strSql = "update PeriodTotal set PeriodDay=@PeriodDay,PeriodBool=@PeriodBool where PeriodTotalId=@PeriodTotalId";
            return DBFactory.GetDB(DBType.SQLITE, m_strConn).ExecuteNonQuery(strSql, new DbParameter[]{
                new SQLiteParameter(){  Value=info.PeriodTotalId, ParameterName="@PeriodTotalId"},
                new SQLiteParameter(){  Value=info.PeriodBool, ParameterName="@PeriodBool"},
                new SQLiteParameter(){  Value=info.PeriodDay, ParameterName="@PeriodDay"}
            });

        }

   
    }
}
