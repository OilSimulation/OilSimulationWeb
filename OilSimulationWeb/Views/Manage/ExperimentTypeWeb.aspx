<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>

    <meta charset="utf-8">
<meta name="renderer" content="webkit|ie-comp|ie-stand">
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
<meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
<meta http-equiv="Cache-Control" content="no-siteapp" />

    <link href="<%=Url.Content("~/Scripts/Exam/static/h-ui/css/H-ui.min.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/static/h-ui.admin/css/H-ui.admin.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/lib/Hui-iconfont/1.0.7/iconfont.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/lib/icheck/icheck.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/static/h-ui.admin/skin/default/skin.css")%>" rel="stylesheet" type="text/css" /> 
    <link href="<%=Url.Content("~/Scripts/Exam/static/h-ui.admin/css/style.css")%>" rel="stylesheet" type="text/css" /> 

</head>
<body>
<nav class="breadcrumb"><i class="Hui-iconfont">&#xe67f;</i> 首页 <span class="c-gray en">&gt;</span> 实验类型管理 <span class="c-gray en">&gt;</span> 实验类型列表 <a class="btn btn-success radius r" style="line-height:1.6em;margin-top:3px" href="javascript:location.replace(location.href);" title="刷新" ><i class="Hui-iconfont">&#xe68f;</i></a></nav>
<div class="page-container">
	<div class="text-c"> <span class="select-box inline">
		<select name="" class="select">
			<option value="0">全部分类</option>
			<option value="1">分类一</option>
			<option value="2">分类二</option>
		</select>
		</span> 日期范围：
		<input type="text" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'logmax\')||\'%y-%M-%d\'}'})" id="logmin" class="input-text Wdate" style="width:120px;">
		-
		<input type="text" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'logmin\')}',maxDate:'%y-%M-%d'})" id="logmax" class="input-text Wdate" style="width:120px;">
		<input type="text" name="" id="" placeholder=" 资讯名称" style="width:250px" class="input-text">
		<button name="" id="" class="btn btn-success" type="submit"><i class="Hui-iconfont">&#xe665;</i> 搜资讯</button>
	</div>
	<div class="cl pd-5 bg-1 bk-gray mt-20"> <span class="l"><a href="javascript:;" onclick="datadel()" class="btn btn-danger radius"><i class="Hui-iconfont">&#xe6e2;</i> 批量删除</a> <a class="btn btn-primary radius" onclick="article_add('添加实验类型','AddExperimentTypeWeb')" href="javascript:;"><i class="Hui-iconfont">&#xe600;</i> 添加资讯</a></span> <span class="r">共有数据：<strong>54</strong> 条</span> </div>
	<div class="mt-20">
		<table id="datatables" class="table table-border table-bordered table-bg table-hover table-sort">
			<thead>
				<tr class="text-c">
					<th width="25"><input type="checkbox" name="" value=""></th>
					<th width="100">大类实验类型名称</th>
					<th width="100">小类实验类型名称</th>
					<th width="200">描述</th>
					<th width="120">更新时间</th>
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






    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/lib/My97DatePicker/WdatePicker.js")%>"></script>
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/lib/jquery/1.9.1/jquery.min.js")%>"></script>
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/lib/datatables/1.10.0/jquery.dataTables.min.js")%>"></script>
    
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/lib/layer/2.1/layer.js")%>"></script>
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/static/h-ui/js/H-ui.js")%>"></script>
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/Exam/static/h-ui.admin/js/H-ui.admin.js")%>"></script>



<script type="text/javascript">

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
//                url: '<%:Url.Action("GetExperimentType","Manage") %>',
            //url: '<%:Url.Action("IsLocal","Business") %>',
        var option = {
            url: '<%:Url.Action("GetExperimentType","Manage") %>',
            type: 'POST',
            dataType: 'html',
            async: false,
            contentType: 'application/json',
            success: function (result) {
                //mmColorJson[paramValue] = eval(result);
                //ShowData(eval(result));
                ShowData(result);
            },
            error: function (e) {
                //mmColorJson[paramValue] = eval(result);
                var k = 0;
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
// <tr class="text-c">
// 	<td><input type="checkbox" value="" name=""></td>
// 	<td>10001</td>
// 	<td>行业动态</td>
// 	<td>H-ui</td>
// 	<td>2014-6-11 11:11:42</td>
// 	<td class="f-14 td-manage">
//     <a style="text-decoration:none" class="ml-5" onClick="article_edit('资讯编辑','article-add.html','10001')" href="javascript:;" title="编辑"><i class="Hui-iconfont">&#xe6df;</i></a> 
//     <a style="text-decoration:none" class="ml-5" onClick="article_del(this,'10001')" href="javascript:;" title="删除"><i class="Hui-iconfont">&#xe6e2;</i></a></td>
        // </tr>
        var tbodyData = $("#data");
        tbodyData.children('tr').remove(); //清空列表
        if (dataValue.length<=0) {
            return;
        }
        var jsonData = JSON.parse(dataValue);

        for (var i = 0; i < jsonData.length; i++) {
            var tr = $("<tr></tr>").appendTo(tbodyData);
            tr.attr("id","TypeId"+jsonData[i].TypeId);
            tr.addClass("text-c");
            //Check
            var tdCheck = $("<td></td>").appendTo(tr);
            var input = $("<input></input>").appendTo(tdCheck);
            input.attr("type", "checkbox");
            input.attr("value", "");
            input.attr("name", "");
            //大类实验类型名称
            var tdType1 = $("<td></td>").appendTo(tr);
            tdType1.html(jsonData[i].TypeName1);
            //小类实验类型名称
            var tdType2 = $("<td></td>").appendTo(tr);
            tdType2.html(jsonData[i].TypeName2);

            var tdDescribe = $("<td></td>").appendTo(tr);
            tdDescribe.html(jsonData[i].TypeDescribe);

            var tdUpdateDateTime = $("<td></td>").appendTo(tr);
            tdUpdateDateTime.html(jsonData[i].tdUpdateDateTime);

            //修改与删除按钮
            var tdManage = $("<td></td>").appendTo(tr);
            tdManage.addClass("f-14 td-manage");
            var aEdit = $("<a></a>").appendTo(tdManage);
            aEdit.addClass("ml-5");
            aEdit.attr("style", "text-decoration:none");
            aEdit.attr("onClick", "article_edit('实验类型编辑','AddExperimentTypeWeb','" + jsonData[i].TypeId + "')");
            aEdit.attr("href", "javascript:;");
            aEdit.attr("title", "编辑");
            var iEdit = $("<i></i>").appendTo(aEdit);
            iEdit.addClass("Hui-iconfont");
            iEdit.html("&#xe6df;"); //&#xe6df;
            //iEdit.html("&#xe6e2;"); //&#xe6df;

            var aDel = $("<a></a>").appendTo(tdManage);
            aDel.addClass("ml-5");
            aDel.attr("style", "text-decoration:none");
            aDel.attr("onClick", "article_del(this,'" + jsonData[i].TypeId + "')");
            aDel.attr("href", "javascript:;");
            aDel.attr("title", "删除");
            var iDel = $("<i></i>").appendTo(aDel);
            iDel.addClass("Hui-iconfont");
            iDel.html("&#xe6e2;"); //&#xe6e2;
            //iDel.html("&#xe6df;");

        }

    }


    /*资讯-添加*/
    function article_add(title, url) {
        $("#AddOrUpdate").val("1");

        var index = layer.open({
            type: 2,
            title: title,
            content: url
        });
        layer.full(index);
    }
    /*资讯-编辑*/
    function article_edit(title, url, id, w, h) {
        $("#ExperimentTypeId").val(id);
        $("#AddOrUpdate").val("2");
        var index = layer.open({
            type: 2,
            title: title,
            content: url
        });
        layer.full(index);
    }
    /*资讯-删除*/
    function article_del(obj, id) {
        layer.confirm('确认要删除吗？', function (index) {
            var jsonData = { Id: id };
            //删除数据库中的数据 
            var option = {
                url: '<%:Url.Action("DelExperimentType","Manage") %>',
                type: 'POST',
                data: JSON.stringify(jsonData),
                dataType: 'text',
                async: false,
                contentType: 'application/json',
                success: function (result) {
                    $(obj).parents("tr").remove().draw();


//                    var table = $('#datatables').DataTable();
//                    var ff = $(obj).tagName;
//                    var a = $(obj).parent();
//                    var ac = a.tagName;
//                    var b = a.parent();
//                    var bc = b.tagName;
//                    var rowf = table.row(b);
//                    rowf.remove().draw(false);

                    //                    var tr = $(obj).parents("tr");
                    //                    var classv = tr.attr("class");
                    //                    //                    table
                    //                    //        .row($(this).parents('tr'))
                    //                    //        .remove()
                    //                    //        .draw();
                    //                    var rowf = table.row(tr);
                    //                    var x = rowf.remove();
                    //                    x.draw();
                    layer.msg('已删除!');
                }
            };
            $.ajax(option);


        });
    }
    /*资讯-审核*/
    function article_shenhe(obj, id) {
        layer.confirm('审核文章？', {
            btn: ['通过', '不通过', '取消'],
            shade: false,
            closeBtn: 0
        },
	function () {
	    $(obj).parents("tr").find(".td-manage").prepend('<a class="c-primary" onClick="article_start(this,id)" href="javascript:;" title="申请上线">申请上线</a>');
	    $(obj).parents("tr").find(".td-status").html('<span class="label label-success radius">已发布</span>');
	    $(obj).remove();
	    layer.msg('已发布', { icon: 6, time: 1000 });
	},
	function () {
	    $(obj).parents("tr").find(".td-manage").prepend('<a class="c-primary" onClick="article_shenqing(this,id)" href="javascript:;" title="申请上线">申请上线</a>');
	    $(obj).parents("tr").find(".td-status").html('<span class="label label-danger radius">未通过</span>');
	    $(obj).remove();
	    layer.msg('未通过', { icon: 5, time: 1000 });
	});
    }
    /*资讯-下架*/
    function article_stop(obj, id) {
        layer.confirm('确认要下架吗？', function (index) {
            $(obj).parents("tr").find(".td-manage").prepend('<a style="text-decoration:none" onClick="article_start(this,id)" href="javascript:;" title="发布"><i class="Hui-iconfont">&#xe603;</i></a>');
            $(obj).parents("tr").find(".td-status").html('<span class="label label-defaunt radius">已下架</span>');
            $(obj).remove();
            layer.msg('已下架!', { icon: 5, time: 1000 });
        });
    }

    /*资讯-发布*/
    function article_start(obj, id) {
        layer.confirm('确认要发布吗？', function (index) {
            $(obj).parents("tr").find(".td-manage").prepend('<a style="text-decoration:none" onClick="article_stop(this,id)" href="javascript:;" title="下架"><i class="Hui-iconfont">&#xe6de;</i></a>');
            $(obj).parents("tr").find(".td-status").html('<span class="label label-success radius">已发布</span>');
            $(obj).remove();
            layer.msg('已发布!', { icon: 6, time: 1000 });
        });
    }
    /*资讯-申请上线*/
    function article_shenqing(obj, id) {
        $(obj).parents("tr").find(".td-status").html('<span class="label label-default radius">待审核</span>');
        $(obj).parents("tr").find(".td-manage").html("");
        layer.msg('已提交申请，耐心等待审核!', { icon: 1, time: 2000 });
    }

</script> 
</body>
</html>
