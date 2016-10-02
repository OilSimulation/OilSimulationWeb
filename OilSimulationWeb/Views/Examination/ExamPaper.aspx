﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">
      table
      {
        width:800px;
        text-align:center;
        margin: auto;
        color:White;
        }
        .td1{width:800px;}
        .td2{width:400px;}
        .td4{width:200px;}
        


    </style>
        <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/lib/jquery/1.9.1/jquery.min.js")%>"></script>

</head>
<body>
    <div>
    <table id="table1" border="1" cellspacing="0" cellpadding="0" >
<%--        <tr><td  colspan="4"  style="text-align:left"><input onclick="Save()" type="button" value="交卷" /></td></tr>
--%>
<%--    	<tr >
    		<td  class="td1" colspan="4">1什么考试</td>
    	</tr>
    	<tr>
    		<td  class="td4">姓名</td><td  class="td4">张三</td><td  class="td4">学号</td><td  class="td4">1002005</td>
    	</tr>
    	<tr>
    		<td class="td4">总分</td><td class="td4">100</td><td class="td4">成绩</td><td class="td4">60</td>
    	</tr>
--%>    </table>
    <table id="table2" border="1" cellspacing="0" cellpadding="0" >
<%--    	<tr>
    		<td colspan="4" style="text-align:left">1、如果岩石是亲油的，那么毛管力是水驱油的阻力</td>
    	</tr>
    	<tr>
    		<td class="td2" colspan="2"><input type ="radio" value="1" name="a"><span>正确</span></td><td colspan="2" class="td2"><input type ="radio" value="0"  name="a"/><span>错误</span></td>
    	</tr>
    	<tr>
    		<td>1</td>
    	</tr>
    	<tr>
    		<td colspan="2"><input type ="radio" value="0" name="a"><span>正确</span></td><td colspan="2"><input type ="radio" value="0"  name="a"/><span>错误</span></td>
    	</tr>
    	<tr>
    		<td class="td4"><input type ="radio" value="0" name="a" checked="checked" onclick="onclickitem('1')"><span>正确33</span></td><td  class="td4"><input type ="radio" value="0" name="a"><span>正确</span></td><td class="td4"><input type ="radio" value="0" name="a"><span>正确</span></td><td class="td4"><input type ="radio" value="0" name="a"><span>正确</span></td>
    	</tr>
    	<tr>
    		<td >a</td><td>b</td><td>c</td><td>d</td>
    	</tr>
--%>    </table>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            parent.postMessage("HideLoading()", "*");

            //parent.ExperimentTypeId

            //ExercisesTestId = parent.ExercisesTestId;
            //StudentExamId = parent.StudentExamId;
            LoadPaper(parent.ExercisesTestId, parent.StudentExamId);
        });

        //加载试卷
        function LoadPaper(examid, studentid) {
            var jsonData = {Id1:examid,Id2:studentid};
            var option = {
                url: '<%:Url.Action("GetExamInfo","Examination") %>',
                data: JSON.stringify(jsonData),
                dataType: 'html',
                type: 'POST',
                async: false,
                contentType: 'application/json',
                success: function (result) {
                    var resultData = JSON.parse(result);
                    if (resultData.ExercisesTestId <= 0) {
                        return;
                    }
                    if (resultData.IsOver > 0) {
                        //考试已经结束
                        $("#table2").attr("disabled", "disabled");
                    }


                    //start table1
                    var varTr1 = $("<tr></tr>");
                    var varTd1 = $("<td></td>");
                    varTd1.addClass("td1");
                    varTd1.attr("colspan", "4");
                    varTd1.html(resultData.ExercisesName);
                    varTd1.appendTo(varTr1);
                    varTr1.appendTo($("#table1"));


                    var varTr2 = $("<tr></tr>");
                    var varTd21 = $("<td></td>");
                    varTd21.addClass("td4");
                    varTd21.html("姓名");
                    varTd21.appendTo(varTr2);


                    var varTd22 = $("<td></td>");
                    varTd22.addClass("td4");
                    varTd22.html(resultData.StudentName);
                    varTd22.appendTo(varTr2);

                    var varTd23 = $("<td></td>");
                    varTd23.addClass("td4");
                    varTd23.html("学号");
                    varTd23.appendTo(varTr2);


                    var varTd24 = $("<td></td>");
                    varTd24.addClass("td4");
                    varTd24.html(resultData.StudentNumber);
                    varTd24.appendTo(varTr2);

                    varTr2.appendTo($("#table1"));


                    var varTr3 = $("<tr></tr>");
                    var varTd31 = $("<td></td>");
                    varTd31.addClass("td4");
                    varTd31.html("总分");
                    varTd31.appendTo(varTr3);


                    var varTd32 = $("<td></td>");
                    varTd32.addClass("td4");
                    varTd32.html(resultData.TotleScore);
                    varTd32.appendTo(varTr3);

                    var varTd33 = $("<td></td>");
                    varTd33.addClass("td4");
                    varTd33.html("学生成绩");
                    varTd33.appendTo(varTr3);


                    var varTd34 = $("<td></td>");
                    varTd34.attr("id", "StudentScore");
                    varTd34.addClass("td4");
                    if (resultData.IsOver > 0) {
                        varTd34.html(resultData.StudentScore);
                    }
                    
                    varTd34.appendTo(varTr3);

                    varTr3.appendTo($("#table1"));
                    //end table1

                    //start table2
                    for (var i = 0; i < resultData.ListExamTitle.length; i++) {
                        var vTr = $("<tr></tr>");
                        var vTd = $("<td></td>");
                        vTd.attr("colspan", "4");
                        vTd.attr("style", "text-align:left");
                        vTd.html((i + 1) + "、" + resultData.ListExamTitle[i].TitleConent);
                        vTd.appendTo(vTr)
                        vTr.appendTo($("#table2"));

                        var itemTr = $("<tr></tr>");
                        for (var j = 0; j < resultData.ListExamTitle[i].ListExamItem.length; j++) {


                            var itemTd = $("<td></td>");
                            if (resultData.ListExamTitle[i].ListExamItem.length == 2) {
                                itemTd.addClass("td2");
                                itemTd.attr("colspan", "2");
                            }
                            else {
                                itemTd.addClass("td4");

                            }
                            itemTd.appendTo(itemTr);

                            var varInput = $("<input></input>");
                            varInput.attr("type", "radio");
                            varInput.attr("name", i);
                            varInput.attr("onclick", "onclickitem('" + resultData.ListExamTitle[i].TitleInfoId + "','" + resultData.ListExamTitle[i].ListExamItem[j].TitleItemIndex + "')");
                            if (resultData.ListExamTitle[i].StudentAnswer == resultData.ListExamTitle[i].ListExamItem[j].TitleItemIndex && resultData.ListExamTitle[i].StudentAnswer != 100) {
                                varInput.attr("checked", "checked");
                            }
                            varInput.appendTo(itemTd);

                            var span = $("<span></span>");
                            span.html(resultData.ListExamTitle[i].ListExamItem[j].ExamItemName);
                            span.appendTo(itemTd);



                        }
                        itemTr.appendTo($("#table2"));
                    }
                    //end table2



                }

            };
            $.ajax(option);
        }

        //TitleInfoId:题目ID,TitleItemIndex:选择的题目索引
        function onclickitem(TitleInfoId, TitleItemIndex) {
            var jsonData = { ExercisesTestId: parent.ExercisesTestId, StudentExamId: parent.StudentExamId, TitleInfoId: TitleInfoId, StudentAnswer: TitleItemIndex };
            var option = {
                url: '<%:Url.Action("AddExamInfo","Examination") %>',
                data: JSON.stringify(jsonData),
                dataType: 'html',
                type: 'POST',
                async: false,
                contentType: 'application/json',
                success: function (result) {
                    if (result > 0) {
                    }
                    else {
                        if (result == -200) {
                            alert("考试已结束！不可再修改！");
                        }
                        else {
                            alert("操作失败！");
                        }

                    }


                }
            }
            $.ajax(option);
        }

        //交卷，生成成绩
        function Save() {
            var jsonData = { Id1: parent.StudentExamId, Id2: parent.ExercisesTestId };
            var option = {
                url: '<%:Url.Action("GetStudentScore","Manage") %>',
                data: JSON.stringify(jsonData),
                dataType: 'html',
                type: 'POST',
                async: false,
                contentType: 'application/json',
                success: function (result) {
                    //$("#StudentScore").html(result);
                    $("#table2").attr("disabled", "disabled");
                }
            }
            $.ajax(option);

        }
    </script>
</body>


</html>
