<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebReport.aspx.cs" Inherits="OilSimulationWeb.WebReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>实验报告</title>
    <link href="./Content/Site.css"" rel="stylesheet" type="text/css" />
    <link href="./Content/UserDefine.css"" rel="stylesheet" type="text/css" /> 
    <script type="text/javascript" src="Scripts/jquery-1.4.1.min.js"></script>

</head>
<body>
    <div>  
        <%--<div id="TopPanel">
            <div class="top-main">
                <div class="top-logo"> 
                    <img alt="重庆科技学院油藏仿真" src="./Images/zx-logo.jpg"" width="382" height="85" border="0" /> 
                </div>
            </div>
        </div>--%>
    
        <div > 
            <%--<div class="zt">
                <div class="zt-tu"><img alt="" src="./Images/zt-tu.png" width="12" height="12" /></div>
                <div class="zt-zi"> 在线虚拟实验 <span id="cmode"> &gt; <span id="curMode">实验报告</span> </span>  </div>
            </div>--%>
            <div  style="background-color:White;">
                <div style="width:800px;margin:0 auto;">  
                    <div style="text-align:center;"> <h1>重庆科技学院学生实验报告</h1></div>
                    <div style="float:left;width:100%;">
                        <div class="ReportDiv" style="width:120px;">课程名称</div><div class="ReportDiv" style="width:200px;"><input id="CourseName" type="text" /></div>
                        <div class="ReportDiv" style="width:149px;">实验项目名称</div><div class="ReportDiv" style="width:326px;border-right:1px solid black;"><input id="ExperimentName" type="text" /></div>
                    </div>
                    <div style="float:left;width:100%;">
                        <div class="ReportDiv" style="width:200px;">开课学院及实验室</div><div class="ReportDiv" style="width:270px;"><input id="ExperimentAddress" type="text" /></div>
                        <div class="ReportDiv" style="width:120px;">实验日期</div><div class="ReportDiv" style="width:205px;border-right:1px solid black;"><input id="ExperimentDate" type="text" /></div>
                    </div>
                    <div style="float:left;width:100%;">
                        <div class="ReportDiv" style="width:120px;">学生姓名</div><div class="ReportDiv" style="width:110px;"><input id="StudentName" type="text" /></div>
                        <div class="ReportDiv" style="width:118px;">学号</div><div class="ReportDiv" style="width:120px;"><input id="StudentNumber" type="text"  readonly="readonly"  /></div>
                        <div class="ReportDiv" style="width:120px;">专业班级</div><div class="ReportDiv" style="width:205px;border-right:1px solid black;"><input id="ClassName" type="text" /></div>
                    </div>
                    <div style="float:left;width:100%;">
                        <div class="ReportDiv" style="width:120px;">指导老师</div><div class="ReportDiv" style="width:229px;"><input id="TeacherName" type="text" /></div>
                        <div class="ReportDiv" style="width:200px;">实验成绩</div><div class="ReportDiv" style="width:246px;border-right:1px solid black;"><input id="Score" type="text" /></div>
                    </div>
                    <div class="ReportDiv" style="float:left;width:798px;border-right:1px solid black; text-align:left; height:200px;">
                        <div style=" font-size:18px; font-weight:bold;">一、实验目的和要求</div> 
                        <textarea  id="expContent1"  style="width:792px; height:165px; margin-top:5px;" ></textarea>
                    </div>
                    <div class="ReportDiv" style="float:left;width:798px;border-right:1px solid black; text-align:left; height:330px;">
                        <div style=" font-size:18px; font-weight:bold;">二、实验内容和原理</div> 
                        <textarea  id="expContent2" style="width:792px; height:295px; margin-top:5px;" ></textarea>
                    </div>
                    <div class="ReportDiv" style="float:left;width:798px;border-right:1px solid black; text-align:left; height:180px;">
                        <div style=" font-size:18px; font-weight:bold;">三、主要仪器设备</div> 
                        <textarea  id="expContent3" style="width:792px; height:145px; margin-top:5px;" ></textarea>
                    </div>
                    <div class="ReportDiv" style="float:left;width:798px;border-right:1px solid black; text-align:left; height:450px;">
                        <div style=" font-size:18px; font-weight:bold;">四、实验操作方法和步骤</div> 
                        <textarea  id="expContent4" style="width:792px; height:415px; margin-top:5px;" ></textarea>
                    </div>
                    <div class="ReportDiv" style="float:left;width:798px;border-right:1px solid black; text-align:left; height:480px;">
                        <div style=" font-size:18px; font-weight:bold;">五、实验记录和处理（数据、图表、计算等）</div> 
                        <textarea  id="expContent5" style="width:792px; height:445px; margin-top:5px;" ></textarea>
                    </div>
                    <div class="ReportDiv" style="float:left;width:798px;border-right:1px solid black; text-align:left; height:480px;">
                        <div style=" font-size:18px; font-weight:bold;">六、实验结果及分析</div> 
                        <textarea  id="expContent6" style="width:792px; height:445px; margin-top:5px;" ></textarea>
                    </div>
                    <div class="ReportDiv" style="float:left;width:798px;border-right:1px solid black;border-bottom:1px solid black; text-align:left; height:180px;">
                        <div style=" font-size:18px; font-weight:bold;">七、结论或体会</div> 
                        <textarea id="expContent7"  style="width:792px; height:145px; margin-top:5px;" ></textarea>
                    </div>
                    <div class="clear"></div>
                    <div style=" text-align:center;"><input id="save" type="button" value="保存" onclick="SaveWebReport()" /> &nbsp;&nbsp;&nbsp;&nbsp;<input id="print" type="button" onclick="window.print();" value="打印" /></div>
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
    <script type="text/javascript">
        $(document).ready(function () {

            var userid = window.opener.document.getElementById("useridDisplay").value;
            LoadWebReport(userid);
            //var userid = Request.getParameter("userid");
            $("#StudentNumber").val(userid);

        });
        function LoadWebReport(userid) {
            var jsonData = { strId: userid };
            var option = {
                url: 'Manage/GetWebReportInfo',
                type: 'POST',
                data: JSON.stringify(jsonData),
                dataType: 'html',
                async: false,
                contentType: 'application/json',
                success: function (result) {
                    if (result !=null) {
                        var resultData = JSON.parse(result);
                        $("#CourseName").val(resultData.CourseName);
                        $("#ExperimentName").val(resultData.ExperimentName);
                        $("#ExperimentAddress").val(resultData.ExperimentAddress);
                        $("#ExperimentDate").val(resultData.ExperimentDate);
                        $("#StudentNumber").val(resultData.StudentNumber);
                        $("#StudentName").val(resultData.StudentName);
                        $("#ClassName").val(resultData.ClassName);

                        $("#TeacherName").val(resultData.TeacherName);
                        $("#expContent1").val(resultData.Title1);
                        $("#expContent2").val(resultData.Title2);
                        $("#expContent3").val(resultData.Title3);
                        $("#expContent4").val(resultData.Title4);
                        $("#expContent5").val(resultData.Title5);
                        $("#expContent6").val(resultData.Title6);
                        $("#expContent7").val(resultData.Title7);
                    }
                    else {
                       
                    }
                },
                error: function (e) {
                    
                }

            }
            $.ajax(option);

        }
                 function SaveWebReport() {
                     var CourseName = $("#CourseName").val();
                     var ExperimentName = $("#ExperimentName").val();
                     var ExperimentAddress = $("#ExperimentAddress").val();
                     var ExperimentDate = $("#ExperimentDate").val();
                     var StudentNumber = $("#StudentNumber").val();
                     var StudentName = $("#StudentName").val();
                     var ClassName = $("#ClassName").val();

                     var TeacherName = $("#TeacherName").val();
                     var Score = $("#Score").val();
                     var Title1 = $("#expContent1").val();
                     var Title2 = $("#expContent2").val();
                     var Title3 = $("#expContent3").val();
                     var Title4 = $("#expContent4").val();
                     var Title5 = $("#expContent5").val();
                     var Title6 = $("#expContent6").val();
                     var Title7 = $("#expContent7").val();
         
                     var jsonData = { CourseName: CourseName, ExperimentName: ExperimentName, ExperimentAddress: ExperimentAddress, ExperimentDate: ExperimentDate,
                         StudentNumber: StudentNumber, StudentName: StudentName, ClassName: ClassName, TeacherName: TeacherName, Score: Score,
                         Title1: Title1, Title2: Title2, Title3: Title3, Title4: Title4, Title5: Title5, Title6: Title6, Title7: Title7
                     };
                     var option = {
                         url: 'Manage/EditWebReportInfo',
                         type: 'POST',
                         data: JSON.stringify(jsonData),
                         dataType: 'html',
                         async: false,
                         contentType: 'application/json',
                         success: function (result) {
                             if (result > 0) {
                                 alert("保存成功");
                             }
                             else {
                                 alert("保存失败");
                             }
                         },
                         error: function (e) {
                             alert("保存失败");
                         }
         
                     }
         
                     $.ajax(option);
         
                 }


    </script>
</body>
</html>
