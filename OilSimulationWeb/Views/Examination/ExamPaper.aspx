<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">
        
        #table1{text-align:center; border:1px solid #bbbbbb;}
        #table1 td{border-bottom:1px dashed #bbbbbb;}
        div{color:White}
        .td1{width:800px;}
        .td2{width:400px;}
        .td4{width:200px;}
        span{color:Red}
        
        .div{height：aout;}
        .boderpadding{border: 1px dashed #BBBBBB;padding:10px;}
        .boderpadding div{padding:10px;}
        .boderx{border-top: 1px dashed #BBBBBB;}
        html{overflow:auto}

        .box {
            height: 40px;
            line-height: 40px;
            text-align: center;
            cursor:pointer;
            border:1px;
            }


        .wrapper {

        border: 1px solid white;
        padding: 20px;
        grid-row-gap: 10px;
        grid-column-gap: 10px;
        display: grid;
        grid-template-columns: repeat(15, 40px);
        }


.myButton {
	-moz-box-shadow:inset 0px 1px 0px 0px #bee2f9;
	-webkit-box-shadow:inset 0px 1px 0px 0px #bee2f9;
	box-shadow:inset 0px 1px 0px 0px #bee2f9;
	background:-webkit-gradient(linear, left top, left bottom, color-stop(0.05, #63b8ee), color-stop(1, #468ccf));
	background:-moz-linear-gradient(top, #63b8ee 5%, #468ccf 100%);
	background:-webkit-linear-gradient(top, #63b8ee 5%, #468ccf 100%);
	background:-o-linear-gradient(top, #63b8ee 5%, #468ccf 100%);
	background:-ms-linear-gradient(top, #63b8ee 5%, #468ccf 100%);
	background:linear-gradient(to bottom, #63b8ee 5%, #468ccf 100%);
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#63b8ee', endColorstr='#468ccf',GradientType=0);
	background-color:#63b8ee;
	-moz-border-radius:6px;
	-webkit-border-radius:6px;
	border-radius:6px;
	border:1px solid #3866a3;
	display:inline-block;
	cursor:pointer;
	color:white;
	font-family:Arial;
	font-size:15px;
	font-weight:bold;
	padding:6px 24px;
	text-decoration:none;
	text-shadow:0px 1px 0px #7cacde;
}
.myButton:hover {
	background:-webkit-gradient(linear, left top, left bottom, color-stop(0.05, #468ccf), color-stop(1, #63b8ee));
	background:-moz-linear-gradient(top, #468ccf 5%, #63b8ee 100%);
	background:-webkit-linear-gradient(top, #468ccf 5%, #63b8ee 100%);
	background:-o-linear-gradient(top, #468ccf 5%, #63b8ee 100%);
	background:-ms-linear-gradient(top, #468ccf 5%, #63b8ee 100%);
	background:linear-gradient(to bottom, #468ccf 5%, #63b8ee 100%);
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#468ccf', endColorstr='#63b8ee',GradientType=0);
	background-color:#468ccf;
}
.myButton:active {
	position:relative;
	top:1px;
}


    </style>
        <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/lib/jquery/1.9.1/jquery.min.js")%>"></script>

</head>
<body>  

    <div style="width:800px;margin: auto">
        <div id="exam" style="color:Red;font-size:25px;text-align:center" ></div>
        <div style="height:20px"></div>
        <div id="student"><table id="table1"  cellspacing="0" cellpadding="0" ></table> </div>
        <div style="height:20px"></div>
        <div id="wrapper" class="wrapper"></div>
        <div style="height:20px"></div>
        <div id="titlelist"></div>
        <div id="submit"><input class="myButton" type="button" value="交卷" onclick="submitPaper()" /></div>
    </div>
    <script type="text/javascript">
        var oldTitle;
        $(document).ready(function () {
            parent.postMessage("HideLoading()", "*");

            //parent.ExperimentTypeId

            //ExercisesTestId = parent.ExercisesTestId;
            //StudentExamId = parent.StudentExamId;
            //CreateDiv();
            LoadPaper(parent.ExercisesTestId, parent.StudentExamId);
            //选中第一题
            ontitle(1);
        });

        function ontitle(id) {
            if (oldTitle != undefined) {
                oldTitle.css("display", "none");    
            }
            $("#id" + id).css("display", "block");
            oldTitle = $("#id" + id);
        }

        //交卷
        function submitPaper() {
            
        }


        //加载试卷
        function LoadPaper(examid, studentid) {
            var jsonData = { Id1: examid, Id2: studentid };
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
                    //设置考试标题
                    $("#exam").html(resultData.ExercisesName);

                    //start table1


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

                    //         <div class="boderpadding">
                    //             <div class="div"><span>第1题</span>：一晨百d</div>
                    //             <div class="boderx">
                    //                 <div class="div">A、百在在d</div>
                    //                 <div class="div">B、百在在d</div>
                    //                 <div class="div">C、百在在d</div>
                    //             </div>
                    //             <div class="div">选择答题：正确<input type="radio"/>   错误<input type="radio"/></div>
                    //         </div>
                    //         
                    //         <div style="height:20px"></div>


                    //start table2

                    //var divwrapper = $("#wrapper");
                    var divwrapper = document.getElementById('wrapper');

                    var divCol = "";
                    //                     for (var i = 0; i < 20; i++) {
                    // 
                    //                         divCol += '<div title="第' + (i + 1) + '题" class="box"  onclick="ontitle(' + (i + 1) + ')" >' + (i + 1) + '</div>';
                    //                     }
                    //divwrapper.innerHTML = divCol;

                    for (var i = 0; i < resultData.ListExamTitle.length; i++) {

                        var varDiv = $("<div></div>");
                        varDiv.attr("id", "id" + (i + 1));
                        varDiv.addClass("boderpadding");
                        varDiv.css("display", "none");
                        //标题
                        var varTitle = $("<div><div/>");
                        varTitle.addClass("div");

                        var varSpanTitle = "<span>第" + (i + 1) + "题</span>：" + resultData.ListExamTitle[i].TitleConent;
                        varTitle.html(varSpanTitle);
                        varTitle.appendTo(varDiv);

                        //选项
                        var varItem = $("<div></div>");
                        varItem.addClass("boderx");
                        var vanswer = $("<div></div>");
                        vanswer.addClass("div");
                        var answerHtml = "选择答案：";
                        var IsSelect = false; //是否答题
                        if (resultData.ListExamTitle[i].TitleTypeName == "选择题") {
                            for (var j = 0; j < resultData.ListExamTitle[i].ListExamItem.length; j++) {
                                var vitem = $("<div></div>");
                                vitem.addClass("div");
                                vitem.html(String.fromCharCode(65 + parseInt(j)) + "、" + resultData.ListExamTitle[i].ListExamItem[j].ExamItemName);
                                vitem.appendTo(varItem);

                                if (resultData.ListExamTitle[i].StudentAnswer == resultData.ListExamTitle[i].ListExamItem[j].TitleItemIndex && resultData.ListExamTitle[i].StudentAnswer != 100) {
                                    answerHtml += String.fromCharCode(65 + parseInt(j)) + "<input type='radio' name='" + i + "' checked='checked' onclick=onclickitem('" + (i + 1) + "','" + resultData.ListExamTitle[i].TitleInfoId + "','" + resultData.ListExamTitle[i].ListExamItem[j].TitleItemIndex + "') />" + "   ";
                                    IsSelect = true;
                                }
                                else {
                                    answerHtml += String.fromCharCode(65 + parseInt(j)) + "<input type='radio' name='" + i + "' onclick=onclickitem('" + (i + 1) + "','" + resultData.ListExamTitle[i].TitleInfoId + "','" + resultData.ListExamTitle[i].ListExamItem[j].TitleItemIndex + "') />" + "   ";

                                }
                            }
                        }
                        else {
                            for (var j = 0; j < resultData.ListExamTitle[i].ListExamItem.length; j++) {

                                if (resultData.ListExamTitle[i].StudentAnswer == resultData.ListExamTitle[i].ListExamItem[j].TitleItemIndex && resultData.ListExamTitle[i].StudentAnswer != 100) {
                                    answerHtml += resultData.ListExamTitle[i].ListExamItem[j].ExamItemName + "<input type='radio' name='" + i + "' checked=checked' onclick='onclickitem('" + (i + 1) + "','" + resultData.ListExamTitle[i].TitleInfoId + "','" + resultData.ListExamTitle[i].ListExamItem[j].TitleItemIndex + "') />" + "   ";
                                    IsSelect = true;
                                }
                                else {
                                    answerHtml += resultData.ListExamTitle[i].ListExamItem[j].ExamItemName + "<input type='radio' name='" + i + "' onclick=onclickitem('" + (i + 1) + "','" + resultData.ListExamTitle[i].TitleInfoId + "','" + resultData.ListExamTitle[i].ListExamItem[j].TitleItemIndex + "') />" + "   ";

                                }
                            }
                        }

                        if (IsSelect) {
                            divCol += '<div id="div' + (i + 1) + '" title="第' + (i + 1) + '题" style="color:blue" class="box"  onclick="ontitle(' + (i + 1) + ')" >' + (i + 1) + '</div>';
                        }
                        else {
                            divCol += '<div id="div' + (i + 1) + '" title="第' + (i + 1) + '题" class="box"  onclick="ontitle(' + (i + 1) + ')" >' + (i + 1) + '</div>';
                        }


                        vanswer.html(answerHtml);
                        varItem.appendTo(varDiv);
                        vanswer.appendTo(varDiv);


                        varDiv.appendTo($("#titlelist"));
                        //var vDivRow = $("<div></div>"); //占位
                        //vDivRow.attr("style", "height:20px");
                        //vDivRow.appendTo($("#titlelist"));


                    }
                    divwrapper.innerHTML = divCol;


                }
                //end 
            };
            $.ajax(option);
        }



//        function LoadPaper(examid, studentid) {
//            var jsonData = {Id1:examid,Id2:studentid};
//            var option = {
//                url: '<%:Url.Action("GetExamInfo","Examination") %>',
//                data: JSON.stringify(jsonData),
//                dataType: 'html',
//                type: 'POST',
//                async: false,
//                contentType: 'application/json',
//                success: function (result) {
//                    var resultData = JSON.parse(result);
//                    if (resultData.ExercisesTestId <= 0) {
//                        return;
//                    }
//                    if (resultData.IsOver > 0) {
//                        //考试已经结束
//                        $("#table2").attr("disabled", "disabled");
//                    }


//                    //start table1
//                    var varTr1 = $("<tr></tr>");
//                    var varTd1 = $("<td></td>");
//                    varTd1.addClass("td1");
//                    varTd1.attr("colspan", "4");
//                    varTd1.html(resultData.ExercisesName);
//                    varTd1.appendTo(varTr1);
//                    varTr1.appendTo($("#table1"));


//                    var varTr2 = $("<tr></tr>");
//                    var varTd21 = $("<td></td>");
//                    varTd21.addClass("td4");
//                    varTd21.html("姓名");
//                    varTd21.appendTo(varTr2);


//                    var varTd22 = $("<td></td>");
//                    varTd22.addClass("td4");
//                    varTd22.html(resultData.StudentName);
//                    varTd22.appendTo(varTr2);

//                    var varTd23 = $("<td></td>");
//                    varTd23.addClass("td4");
//                    varTd23.html("学号");
//                    varTd23.appendTo(varTr2);


//                    var varTd24 = $("<td></td>");
//                    varTd24.addClass("td4");
//                    varTd24.html(resultData.StudentNumber);
//                    varTd24.appendTo(varTr2);

//                    varTr2.appendTo($("#table1"));


//                    var varTr3 = $("<tr></tr>");
//                    var varTd31 = $("<td></td>");
//                    varTd31.addClass("td4");
//                    varTd31.html("总分");
//                    varTd31.appendTo(varTr3);


//                    var varTd32 = $("<td></td>");
//                    varTd32.addClass("td4");
//                    varTd32.html(resultData.TotleScore);
//                    varTd32.appendTo(varTr3);

//                    var varTd33 = $("<td></td>");
//                    varTd33.addClass("td4");
//                    varTd33.html("学生成绩");
//                    varTd33.appendTo(varTr3);


//                    var varTd34 = $("<td></td>");
//                    varTd34.attr("id", "StudentScore");
//                    varTd34.addClass("td4");
//                    if (resultData.IsOver > 0) {
//                        varTd34.html(resultData.StudentScore);
//                    }
//                    
//                    varTd34.appendTo(varTr3);

//                    varTr3.appendTo($("#table1"));
//                    //end table1

//                    //start table2
//                    for (var i = 0; i < resultData.ListExamTitle.length; i++) {
//                        var vTr = $("<tr></tr>");
//                        var vTd = $("<td></td>");
//                        vTd.attr("colspan", "4");
//                        vTd.attr("style", "text-align:left");
//                        vTd.html((i + 1) + "、" + resultData.ListExamTitle[i].TitleConent);
//                        vTd.appendTo(vTr)
//                        vTr.appendTo($("#table2"));

//                        var itemTr = $("<tr></tr>");
//                        for (var j = 0; j < resultData.ListExamTitle[i].ListExamItem.length; j++) {


//                            var itemTd = $("<td></td>");
//                            if (resultData.ListExamTitle[i].ListExamItem.length == 2) {
//                                itemTd.addClass("td2");
//                                itemTd.attr("colspan", "2");
//                            }
//                            else {
//                                itemTd.addClass("td4");

//                            }
//                            itemTd.appendTo(itemTr);

//                            var varInput = $("<input></input>");
//                            varInput.attr("type", "radio");
//                            varInput.attr("name", i);
//                            varInput.attr("onclick", "onclickitem('" + resultData.ListExamTitle[i].TitleInfoId + "','" + resultData.ListExamTitle[i].ListExamItem[j].TitleItemIndex + "')");
//                            if (resultData.ListExamTitle[i].StudentAnswer == resultData.ListExamTitle[i].ListExamItem[j].TitleItemIndex && resultData.ListExamTitle[i].StudentAnswer != 100) {
//                                varInput.attr("checked", "checked");
//                            }
//                            varInput.appendTo(itemTd);

//                            var span = $("<span></span>");
//                            span.html(resultData.ListExamTitle[i].ListExamItem[j].ExamItemName);
//                            span.appendTo(itemTd);



//                        }
//                        itemTr.appendTo($("#table2"));
//                    }
//                    //end table2



//                }

//            };
//            $.ajax(option);
//        }

        //TitleInfoId:题目ID,TitleItemIndex:选择的题目索引
        function onclickitem(id,TitleInfoId, TitleItemIndex) {
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
                        $("#div" + id).css("color","blue");
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
