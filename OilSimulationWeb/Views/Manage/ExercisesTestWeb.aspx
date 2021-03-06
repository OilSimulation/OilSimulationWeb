﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>考试管理</title>



    <link href="<%=Url.Content("~/Scripts/Exam/static/h-ui/css/H-ui.min.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/static/h-ui.admin/css/H-ui.admin.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/lib/Hui-iconfont/1.0.7/iconfont.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/lib/icheck/icheck.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/static/h-ui.admin/skin/default/skin.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/static/h-ui.admin/css/style.css")%>" rel="stylesheet" type="text/css" /> 

</head>
<body>
<nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 试题管理 <span class="c-gray en">&gt;</span> 考试列表 <a class="btn btn-success radius r" style="line-height:1.6em;margin-top:3px" href="javascript:location.replace(location.href);" title="刷新" ><i class="Hui-iconfont">&#xe68f;</i></a></nav>
<div class="page-container">

	<div class="cl pd-5 bg-1 bk-gray mt-20"> 
        <span class="l">
            <a href="javascript:;" onclick="datadel()" class="btn btn-danger radius"><i class="Hui-iconfont">&#xe6e2;</i> 批量删除</a> 
            <a class="btn btn-primary radius" onclick="article_add('添加考试','AddExercisesTestWeb')" href="javascript:;"><i class="Hui-iconfont">&#xe600;</i> 添加数据</a>
        </span>  
    </div>
	<div class="mt-20">
		<table id="datatables" class="table table-border table-bordered table-bg table-hover table-sort">
			<thead>
				<tr class="text-c">
					<th width="25"><input type="checkbox" name="" value=""></th>
					<th width="100">考试或练习名称</th>
					<th width="100">考试类型</th>
					<th width="100">考试描述</th>
                    <th width="100">使用标志</th>
                    <th width="120">操作时间</th>
                    <th width="120">操作</th>
				</tr>
			</thead>
			<tbody id="data" type="text">

			</tbody>
		</table>
        <input id="ExperimentTypeId"  style="display:none"/>
        <input id="AddOrUpdate"  style="display:none"/>
	</div>
</div>





    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/lib/jquery/1.9.1/jquery.min.js")%>"></script>
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/lib/My97DatePicker/WdatePicker.js")%>"></script>
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/lib/datatables/1.10.0/jquery.dataTables.min.js")%>"></script>
    
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/lib/layer/2.1/layer.js")%>"></script>
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/static/h-ui/js/H-ui.js")%>"></script>
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/static/h-ui.admin/js/H-ui.admin.js")%>"></script>



<script type="text/javascript">

    var ExercisesTestId = -1;
    $(document).ready(function () {
        LoadData();
        RefDataTables();

    });

    function RefDataTables() {

        $('.table-sort').DataTable({
            "aaSorting": [[1, "desc"]], //默认第几个排序
            "bStateSave": true, //状态保存
            "aoColumnDefs": [
            //{"bVisible": false, "aTargets": [ 3 ]} //控制列的隐藏显示
	  {"orderable": false, "aTargets": [0, 5]}// 不参与排序的列
	]
        });

    }

    function LoadData() {

        var option = {
            url: '<%:Url.Action("GetExercisesTest","Manage") %>',
            type: 'POST',
            dataType: 'html',
            async: false,
            contentType: 'application/json',
            success: function (result) {

                ShowData(result);
            },
            error: function (e) {

            }

        };
        $.ajax(option);

    }

    //    //page:第几页，count:每页显示条数
    //    function LoadData(page, count) {
    //        var jsonData = { CurrentPage: page, ShowCount: count};

    //        var option = {
    //            url: '<%:Url.Action("GetExperimentType","Manage") %>',
    //            type: 'POST',
    //            data: JSON.stringify(jsonData),
    //            dataType: 'html',
    //            async: false,
    //            contentType: 'application/json',
    //            success: function (result) {
    //                //mmColorJson[paramValue] = eval(result);
    //                ShowData(eval(result));
    //            }
    //        };
    //        $.ajax(option);
    //    }

    //dataValue,获取的实验类型JSON数据
    function ShowData(dataValue) {

        var tbodyData = $("#data");
        tbodyData.children('tr').remove(); //清空列表
        if (dataValue.length <= 0) {
            return;
        }
        var jsonData = JSON.parse(dataValue);

        for (var i = 0; i < jsonData.length; i++) {
            var tr = $("<tr></tr>").appendTo(tbodyData);
            tr.attr("id", "ExercisesTestId" + jsonData[i].ExercisesTestId);
            tr.addClass("text-c");
            //Check
            var tdCheck = $("<td></td>").appendTo(tr);
            var input = $("<input></input>").appendTo(tdCheck);
            input.attr("type", "checkbox");
            input.attr("value", "");
            input.attr("name", "");
            //习题或考试名称
            var tdType1 = $("<td></td>").appendTo(tr);
            tdType1.html(jsonData[i].ExercisesName);
            //考试类型
            var tdExercises = $("<td></td>").appendTo(tr);
            if (jsonData[i].ExercisesTypeId == -1) {
                tdExercises.html('考试');
            }
            else {
                tdExercises.html(jsonData[i].TypeName1 + "\\" + jsonData[i].TypeName2);
            }

            

            //考试描述
            var tdType2 = $("<td></td>").appendTo(tr);
            tdType2.html(jsonData[i].ExercisesDescribe);

            var bz = $("<td></td>").appendTo(tr);
            if (jsonData[i].IsUse>0) {
                bz.html("当前使用");
            }
            else {
                bz.html("");
            }

            

            //操作时间
            var tdUpdateDateTime = $("<td></td>").appendTo(tr);
            tdUpdateDateTime.html(jsonData[i].UpdateDateTime);

            //修改与删除按钮
            var tdManage = $("<td></td>").appendTo(tr);
            tdManage.addClass("f-14 td-manage");
            var aEdit = $("<a></a>").appendTo(tdManage);
            aEdit.addClass("ml-5");
            aEdit.attr("style", "text-decoration:none");
            aEdit.attr("onClick", "article_edit('题目编辑','AddExercisesTestWeb','" + jsonData[i].ExercisesTestId + "')");
            aEdit.attr("href", "javascript:;");
            aEdit.attr("title", "编辑");
            var iEdit = $("<i></i>").appendTo(aEdit);
            iEdit.addClass("Hui-iconfont");
            iEdit.html("&#xe6df;"); //&#xe6df;
            //iEdit.html("&#xe6e2;"); //&#xe6df;

            var aDel = $("<a></a>").appendTo(tdManage);
            aDel.addClass("ml-5");
            aDel.attr("style", "text-decoration:none");
            aDel.attr("onClick", "article_del(this,'" + jsonData[i].ExercisesTestId + "')");
            aDel.attr("href", "javascript:;");
            aDel.attr("title", "删除");
            var iDel = $("<i></i>").appendTo(aDel);
            iDel.addClass("Hui-iconfont");
            iDel.html("&#xe6e2;"); //&#xe6e2;

            var aCurrent = $("<a></a>").appendTo(tdManage);
            aCurrent.addClass("ml-5");
            aCurrent.attr("style", "text-decoration:none");
            aCurrent.attr("onClick", "currentExercises(this,'" + jsonData[i].ExercisesTestId + "')");
            aCurrent.attr("href", "javascript:;");
            aCurrent.attr("title", "当前使用");
            var iCurrent = $("<i></i>").appendTo(aCurrent);
            iCurrent.addClass("Hui-iconfont");
            iCurrent.html("&#xe619;"); //&#xe6e2;

            var aStart = $("<a></a>").appendTo(tdManage);
            aStart.addClass("ml-5");
            aStart.attr("style", "text-decoration:none");
            //aStart.attr("onClick", "currentExercises(this,'" + jsonData[i].ExercisesTestId + "')");
            aStart.attr("href", "javascript:;");
            var iStart = $("<i></i>").appendTo(aStart);
            iStart.addClass("Hui-iconfont");


            if (jsonData[i].IsOver>0) {
                aStart.attr("title", "开始考试");
                iStart.html("&#xe6e6;"); //开始
                //aStart.attr("onClick", "UpdateExercisesState(this,'" + jsonData[i].ExercisesTestId + "'," + iStart + ")");
                aStart.attr("onClick", "UpdateExercisesState(this,'" + jsonData[i].ExercisesTestId + "')");
                aStart.attr("state", "0");
            }
            else {
                aStart.attr("title", "结束考试");
                iStart.html("&#xe6e4;"); //结束
                //aStart.attr("onClick", "UpdateExercisesState(this,'" + jsonData[i].ExercisesTestId + "'," + iStart + ")");
                aStart.attr("onClick", "UpdateExercisesState(this,'" + jsonData[i].ExercisesTestId + "')");
                aStart.attr("state", "1");

            }

            
            


        }

    }
    //id:考试ID,state:考试状态(0 正在考试,1考试结束),iObj:icon对象
    function UpdateExercisesState(obj, id,iObj) {
        var icon = $(obj).attr("state");
        
        var state = 0;
        if (icon == "0") {//开始
            state = 0;
        }
        else {// &#xe6e4; 结束
            state = 1;
        }


        var jsonData = { Id1: id,Id2:state };
        var option = {
            url: '<%:Url.Action("UpdateExercisesState","Manage") %>',
            type: 'POST',
            data: JSON.stringify(jsonData),
            dataType: 'html',
            async: false,
            contentType: 'application/json',
            success: function (result) {
                if (state == 0) {
                    obj.children[0].innerHTML = "&#xe6e4;";
                    //iObj.html("&#xe6e4;");
                    $(obj).attr("state", "1");
                }
                else {
                    obj.children[0].innerHTML = "&#xe6e6;";
                    $(obj).attr("state", "0");
                    //iObj.html("&#xe6e6;");

                }


            }
        };
        $.ajax(option);

    }


    function currentExercises(obj,id) {
        if (ExercisesTestId != id) {
            var jsonData = { Id: id };
            var option = {
                url: '<%:Url.Action("SaveCurrentExercises","Manage") %>',
                type: 'POST',
                data: JSON.stringify(jsonData),
                dataType: 'html',
                async: false,
                contentType: 'application/json',
                success: function (result) {
                    //$(obj).parents("tr").remove().draw();
                    if (result > 0) {
                        LoadData();
                        //                        var tr = parent.$("#ExercisesTestId" + id);
                        //                        var tds = tds.chilren();
                        //                        tds.eq(4).text("当前使用");

                    }

                }
            };
            $.ajax(option);

        }

    }


    /*资讯-添加*/
    function article_add(title, url) {
        $("#AddOrUpdate").val("1");

//        var index = layer.open({
//            type: 2,
//            title: title,
//            content: url
//        });
//        layer.full(index);
        layer_show(title, url, '', 400);
    }
    /*资讯-编辑*/
    function article_edit(title, url, id, w, h) {
        $("#ExperimentTypeId").val(id);
        $("#AddOrUpdate").val("2");
//        var index = layer.open({
//            type: 2,
//            title: title,
//            content: url
//        });
//        layer.full(index);
        layer_show(title, url, '', 400);
    }
    /*资讯-删除*/
    function article_del(obj, id) {
        layer.confirm('确认要删除吗？', function (index) {
            var jsonData = { Id: id };
            //删除数据库中的数据 
            var option = {
                url: '<%:Url.Action("DelExercisesTest","Manage") %>',
                type: 'POST',
                data: JSON.stringify(jsonData),
                dataType: 'html',
                async: false,
                contentType: 'application/json',
                success: function (result) {
                    //$(obj).parents("tr").remove().draw();
                    if (result > 0) {
                        var table = $('#datatables').DataTable();
                        table.row($(obj).parents('tr')).remove().draw();
                        table.row($(obj).parents('tr')).remove();//.draw();

//                        var rowf = table.row($(obj).parent().parent());
//                        rowf.remove().draw(false);

                        layer.msg('已删除!');

                    }

                }
            };
            $.ajax(option);


        });
    }
    </script> 
</body>
</html>