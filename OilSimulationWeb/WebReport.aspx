<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebReport.aspx.cs" Inherits="OilSimulationWeb.WebReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>实验报告</title>
    <link href="./Content/Site.css"" rel="stylesheet" type="text/css" />
    <link href="./Content/UserDefine.css"" rel="stylesheet" type="text/css" /> 
</head>
<body>
    <div id="Index">  
        <%--<div id="TopPanel">
            <div class="top-main">
                <div class="top-logo"> 
                    <img alt="重庆科技学院油藏仿真" src="./Images/zx-logo.jpg"" width="382" height="85" border="0" /> 
                </div>
            </div>
        </div>--%>
    
        <div class="main"> 
            <%--<div class="zt">
                <div class="zt-tu"><img alt="" src="./Images/zt-tu.png" width="12" height="12" /></div>
                <div class="zt-zi"> 在线虚拟实验 <span id="cmode"> &gt; <span id="curMode">实验报告</span> </span>  </div>
            </div>--%>
            <div class="greybg" style="background-color:White;">
                <div class="main" style="width:800px;margin:0 auto;border-bottom:1px solid black;">  
                    <div style="text-align:center;"> <h1>重庆科技学院学生实验报告</h1></div>
                    <div style="float:left;width:100%;">
                        <div class="ReportDiv" style="width:120px;">课程名称</div><div class="ReportDiv" style="width:200px;"><input type="text" /></div>
                        <div class="ReportDiv" style="width:149px;">实验项目名称</div><div class="ReportDiv" style="width:326px;border-right:1px solid black;"><input type="text" /></div>
                    </div>
                    <div style="float:left;width:100%;">
                        <div class="ReportDiv" style="width:200px;">开课学院及实验室</div><div class="ReportDiv" style="width:270px;"><input type="text" /></div>
                        <div class="ReportDiv" style="width:120px;">实验日期</div><div class="ReportDiv" style="width:205px;border-right:1px solid black;"><input type="text" /></div>
                    </div>
                    <div style="float:left;width:100%;">
                        <div class="ReportDiv" style="width:120px;">学生姓名</div><div class="ReportDiv" style="width:110px;"><input type="text" /></div>
                        <div class="ReportDiv" style="width:118px;">学号</div><div class="ReportDiv" style="width:120px;"><input type="text" /></div>
                        <div class="ReportDiv" style="width:120px;">专业班级</div><div class="ReportDiv" style="width:205px;border-right:1px solid black;"><input type="text" /></div>
                    </div>
                    <div style="float:left;width:100%;">
                        <div class="ReportDiv" style="width:120px;">指导老师</div><div class="ReportDiv" style="width:229px;"><input type="text" /></div>
                        <div class="ReportDiv" style="width:200px;">实验成绩</div><div class="ReportDiv" style="width:246px;border-right:1px solid black;"><input type="text" /></div>
                    </div>
                    <div class="ReportDiv" style="float:left;width:798px;border-right:1px solid black; text-align:left; height:200px;">
                        <div style=" font-size:18px; font-weight:bold;">一、实验目的和要求</div> 
                        <textarea  style="width:792px; height:165px; margin-top:5px;" ></textarea>
                    </div>
                    <div class="ReportDiv" style="float:left;width:798px;border-right:1px solid black; text-align:left; height:330px;">
                        <div style=" font-size:18px; font-weight:bold;">二、实验内容和原理</div> 
                        <textarea  style="width:792px; height:295px; margin-top:5px;" ></textarea>
                    </div>
                    <div class="ReportDiv" style="float:left;width:798px;border-right:1px solid black; text-align:left; height:180px;">
                        <div style=" font-size:18px; font-weight:bold;">三、主要仪器设备</div> 
                        <textarea  style="width:792px; height:145px; margin-top:5px;" ></textarea>
                    </div>
                    <div class="ReportDiv" style="float:left;width:798px;border-right:1px solid black; text-align:left; height:450px;">
                        <div style=" font-size:18px; font-weight:bold;">四、实验操作方法和步骤</div> 
                        <textarea  style="width:792px; height:415px; margin-top:5px;" ></textarea>
                    </div>
                    <div class="ReportDiv" style="float:left;width:798px;border-right:1px solid black; text-align:left; height:480px;">
                        <div style=" font-size:18px; font-weight:bold;">五、实验记录和处理（数据、图表、计算等）</div> 
                        <textarea  style="width:792px; height:445px; margin-top:5px;" ></textarea>
                    </div>
                    <div class="ReportDiv" style="float:left;width:798px;border-right:1px solid black; text-align:left; height:480px;">
                        <div style=" font-size:18px; font-weight:bold;">六、实验结果及分析</div> 
                        <textarea  style="width:792px; height:445px; margin-top:5px;" ></textarea>
                    </div>
                    <div class="ReportDiv" style="float:left;width:798px;border-right:1px solid black; text-align:left; height:180px;">
                        <div style=" font-size:18px; font-weight:bold;">七、结论或体会</div> 
                        <textarea  style="width:792px; height:145px; margin-top:5px;" ></textarea>
                    </div>
                    <div class="clear"></div>
                </div> 
            </div> 
        </div> 

        <%--<div class="foot2">
            <div class="main">
            COPYRIGHT 2013 (C) CHONGQING UNIVERSITY OF SCIENCE AND TECHNOLOGY ALL RIGHTS RESERVED<br/>
            重庆沙坪坝区大学城东路20号 邮编：401331 备案号：渝ICP备13000511号 技术支持：<a href="javascript:void(0);" target="_blank">重庆昌禀科技</a>
            </div>
        </div>--%>
    </div> 
</body>
</html>
